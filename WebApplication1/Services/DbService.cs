using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTOs.Req;
using WebApplication1.DTOs.Res;
using WebApplication1.Exceptions;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DbService :IDbService
    {

        private readonly MainDbContext _context;
        private readonly IConfiguration _configuration;

        public DbService(MainDbContext mainDbContext,IConfiguration configuration)
        {
            _context = mainDbContext;
            _configuration = configuration;
        }

        public async Task<IEnumerable<GetDoctorRes>> GetDoctors()
        {
            return  _context.Doctors.Select(
                s=>new GetDoctorRes
            { 
                IdDoctor = s.IdDoctor,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email
            });
        }
        public async Task<bool> AddDoctor(AddDoctorReq doctor)
        {

            try
            {
                var doctorTmp = new Doctor
                {
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Email = doctor.Email,
                };
                if (_context.Doctors.Where(s => (s.FirstName == doctor.FirstName) && (s.Email == doctor.Email) && (s.LastName == doctor.LastName)).Any())
                    throw new Exception();
                await _context.AddAsync(doctorTmp);               
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            try
            {
                var doctor = _context.Doctors.Where(s => s.IdDoctor == id).Single();
                if (doctor == null)
                    throw new NoDoctorWithIdException("Doctor with this id does not exist");
                if (!_context.Prescriptions.Any())
                {
                    _context.Doctors.Remove(doctor);
                    return true;
                }

                var prescriptions = _context.Prescriptions.Where(s => s.IdDoctor == id).ToList();
                foreach (var prescription in prescriptions)
                {
                    _context.Prescriptions.Remove(prescription);
                    if (_context.Prescriptions_Medicaments.Any())
                    {
                        var PrescriptionsMedicaments = _context.Prescriptions_Medicaments.Where(e => e.IdPrescription == prescription.IdPrescription);
                        if (PrescriptionsMedicaments != null && PrescriptionsMedicaments.Any())
                        {
                            foreach (var pre_Med in PrescriptionsMedicaments)
                                _context.Remove(pre_Med);
                        }
                    }
                }
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ModifyDoctor(ModifyDoctorReq modifyDoctorReq, int id)
        {
            Doctor doctorToModify = _context.Doctors.Where(s => s.IdDoctor == id).Single();
            if (doctorToModify == null)      
               throw new NoDoctorWithIdException("Doctor with this id does not exist");
            try
            {
                doctorToModify.FirstName = modifyDoctorReq.FirstName;
                doctorToModify.LastName = modifyDoctorReq.LastName;
                doctorToModify.Email = modifyDoctorReq.Email;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }          
        }

        public async Task<GetPrescriptionRes> GetPrescription(int id)
        {
            try
            {
                Prescription prescription = _context.Prescriptions.Where(s => s.IdPrescription == id).Single();
                Patient patient = _context.Patients.Where(s => prescription.IdPatient == s.IdPatient).Single();
                Doctor doctor = _context.Doctors.Where(s => prescription.IdDoctor == s.IdDoctor).Single();
                List<Prescription_Medicament> medicaments = _context.Prescriptions_Medicaments.Where(s => s.IdPrescription == prescription.IdPrescription).ToList();
                List<Medicament> medicamentList = new();

                foreach (Prescription_Medicament m in medicaments)
                {
                    medicamentList.Add(_context.Medicaments.Where(s => s.IdMedicament == m.IdMedicament).Single());
                }

                return new GetPrescriptionRes
                {
                    IdPrescription = prescription.IdPrescription,
                    IdPatient = patient.IdPatient,
                    FirstNamePatient = patient.FirstName,
                    LastNamePatient = patient.LastName,
                    BirthDatePatient = patient.BirthDate,
                    IdDoctor = doctor.IdDoctor,
                    FirstNameDoctor = doctor.FirstName,
                    LastNameDoctor = doctor.LastName,
                    EmailDoctor = doctor.Email,
                    Medicaments = medicamentList
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Tuple<bool,object>> Login(LoginRequest request)
        {
           User user = _context.Users.Where(u => u.Login == request.Login).FirstOrDefault();

            string passwordHash = user.Password;
            string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(request.Password, user.Salt);

            if (passwordHash != curHashedPassword)
            {
                return new (false,null);
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://localhost:44324",
                audience: "https://localhost:44324",
                claims: null,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );
            user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
            user.RefreshTokenExp = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return  new (true,new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = user.RefreshToken
            });
        }

        public async Task<Tuple<bool, object>> RefreshToken(string token, RefreshToken refreshToken)
        {
            User user = _context.Users.Where(u => u.RefreshToken == refreshToken.Token).FirstOrDefault();
            if (user == null)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            if (user.RefreshTokenExp < DateTime.Now)
            {
                throw new SecurityTokenException("Refresh token expired");
            }

            var login = SecurityHelpers.GetUserIdFromAccessToken(token.Replace("Bearer ", ""), _configuration["SecretKey"]);



            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: "https://localhost:44324",
                audience: "https://localhost:44324",
                claims: null,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
            user.RefreshTokenExp = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return new (true ,new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                refreshToken = user.RefreshToken
            });
        }
    }
}

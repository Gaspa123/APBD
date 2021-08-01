using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Req;
using WebApplication1.DTOs.Res;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IDbService 
    {
       public Task<IEnumerable<GetDoctorRes>> GetDoctors();
       public Task<bool> AddDoctor(AddDoctorReq doctor);
       public Task<bool> ModifyDoctor(ModifyDoctorReq modifyDoctorReq , int id);
       public Task<bool> DeleteDoctor(int id);
       public Task<GetPrescriptionRes> GetPrescription(int id);
       public Task<Tuple<bool, object>> Login(LoginRequest request);
       public Task<Tuple<bool, object>> RefreshToken(string token, RefreshToken refreshToken);
    }
}

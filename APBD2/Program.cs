using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace APBD2
{
    class Program
    {
        public static void Main(string[] args)
        {

            try
            {
                var path = args[0];
                var data = File.ReadAllLines(path);
                HashSet<Student> students = new(new MyComparer());
                FileStream fs = new FileStream(@"E:\!PJATK\log.txt", FileMode.OpenOrCreate);
                StreamWriter streamWriter = new StreamWriter(fs);  

                foreach (var line in data)
                {
                    string [] tmpData = line.Split(',');
                    bool flag = true;
                    foreach (string s in tmpData)
                    {
                        if (string.IsNullOrEmpty(s))
                        {
                            streamWriter.WriteLine("There is a null: " + line);
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        if (tmpData.Length != 9)
                        {
                            streamWriter.WriteLine("Not enough columnes: " + line);
                        }
                        else
                        {
                            int count = 0;
                            if (!students.Add(new Student
                            {
                                Fname = tmpData[count++],
                                Lname = tmpData[count++],
                                NameStudies = tmpData[count++],
                                Mode = tmpData[count++],
                                IndexNumber = tmpData[count++],
                                Birthdate = DateTime.Parse(tmpData[count++]),
                                Email = tmpData[count++],
                                MothersName = tmpData[count++],
                                FathersName = tmpData[count],
                            }))
                            {
                                streamWriter.WriteLine("Duplicate: " + line);
                            }

                        }
                    }

                }

                streamWriter.Close();

                Dictionary<string, int> countCourse = new Dictionary<string, int>();

                foreach (Student s in students)
                {
                    if (!countCourse.ContainsKey(s.NameStudies))
                    {
                        countCourse.Add(s.NameStudies, 1);
                    }
                    else
                    {
                        countCourse[s.NameStudies]++;
                    }
                }

                var json = JsonSerializer.Serialize(students);

                Console.WriteLine(json);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Input file does not exist :( ");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid path ");
            }     
        }
    }
}

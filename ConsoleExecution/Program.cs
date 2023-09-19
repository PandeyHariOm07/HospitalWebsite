using SOTI.Curewell.DAL;
using SOTI.Curewell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExecution
{
    public class Program
    {
        static void Main(string[] args)
        {
            CureWellRepository cs = new CureWellRepository();


            //To Get All Doctor

            List<Doctor> ds = cs.GetAllDoctors();
            foreach (Doctor d in ds)
            {
                Console.WriteLine($"Doctor Id: {d.DoctorId}\t\t DoctorName: {d.DoctorName}\n");
            }


            //To Get All Specialization

            //List<Specialization> sp = cs.GetAllSpecializations();
            //foreach(Specialization s in sp)
            //{
            //    Console.WriteLine($"SpecializationCode: {s.SpecializationCode}\t\t SpecializationName: {s.SpecializationName}\n");
            //}



            //To Get Present Surgery

            //List < Surgery > today = cs.GetAllSurgeryTypeForToday();
            //foreach(Surgery sp in today)
            //{
            //    Console.WriteLine($"SurgeryId: {sp.SurgeryId}\t DoctorId: {sp.DoctorId}\t SurgeryDate: {sp.SurgeryDate}\t StartTime: {sp.StartTime}\t EndTime: {sp.EndTime} \t SurgeryCategory: {sp.SurgeryCategory}");
            //}



            //To Get Doctor By Specialization Code

            //List<DoctorSpecialization> sp = cs.GetDoctorsBySpecialization("ANE");
            //foreach(DoctorSpecialization s in sp)
            //{
            //    Console.WriteLine($"DoctorId: {s.DoctorId}\t\tSpecializationCode: {s.SpecializationCode}\t\t SpecializationDate: {s.SpecializationDate}");
            //}



            //To Add Doctor

            //Doctor dObj = new Doctor() { DoctorName = "Rampreet" };
            //Console.WriteLine($"New Doctor Added : {cs.AddDoctor(dObj)}");

            //To Update Doctor

            //Doctor dObj = new Doctor() { DoctorId = 1005, DoctorName = "Jack Maa" };
            //Console.WriteLine($"Doctor,s Details Updated : {cs.UpdateDoctorDetails(dObj)}");


            //To Update Surgery

            //Surgery sObj = new Surgery() { SurgeryId = 5000, DoctorId = 1002, StartTime = Convert.ToDecimal("01.54"), EndTime = Convert.ToDecimal("05.54"), SurgeryDate = DateTime.Now, SurgeryCategory = "ANE" };
            //Console.WriteLine($"Surgery's Details Updated : {cs.UpdateSurgery(sObj)}");
        }
    }
}

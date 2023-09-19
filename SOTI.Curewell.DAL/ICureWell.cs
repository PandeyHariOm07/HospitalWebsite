using SOTI.Curewell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Curewell.DAL
{
    public interface ICureWell
    {
        List<Doctor> GetAllDoctors();
        List<Specialization> GetAllSpecializations();
        List<Surgery> GetAllSurgeryTypeForToday();
        List<DoctorSpecialization> GetDoctorsBySpecialization(string specializationCode);
        bool AddDoctor(Doctor dObj);
        bool AddSurgery(Surgery SObj);
        bool UpdateDoctorDetails(Doctor dObj);
        bool UpdateSurgery(Surgery SObj);

    }
}

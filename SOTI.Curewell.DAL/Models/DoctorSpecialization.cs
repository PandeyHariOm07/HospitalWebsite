using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Curewell.DAL.Models
{
    public class DoctorSpecialization
    {
        public int DoctorId { get; set; }
        public string SpecializationCode { get; set; }
        public DateTime SpecializationDate { get; set; }

    }
}

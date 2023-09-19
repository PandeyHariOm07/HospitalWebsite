using SOTI.Curewell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Curewell.DAL
{
    public class CureWellRepository : ICureWell
    {
        private SqlConnection _conObj= null;
        SqlCommand _command = null;
        SqlDataReader _reader = null;
        public int Context { get; set; }
        public CureWellRepository()
        {

        }

        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> answer = new List<Doctor>();
            using(_conObj = new SqlConnection(SqlConnectionString.getConnectionString()))
            {
                _conObj.Open();
                using(_command = new SqlCommand("Select * from Doctor", _conObj))
                {
                    using(_reader = _command.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            int id = Convert.ToInt32(_reader["DoctorId"]);
                            string doctorName = Convert.ToString(_reader["DoctorName"]);
                            Doctor doc = new Doctor()
                            {
                                DoctorId = id,
                                DoctorName = doctorName
                            };
                        answer.Add(doc);
                        }
                    }
                }
            }
            return answer;
        }
        public List<Specialization> GetAllSpecializations()
        {

            List<Specialization> answer = new List<Specialization>();
            using (_conObj = new SqlConnection(SqlConnectionString.getConnectionString()))
            {
                _conObj.Open();
                using (_command = new SqlCommand("Select * from Specialization", _conObj))
                {
                    using (_reader = _command.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            string code = Convert.ToString(_reader["SpecializationCode"]);
                            string Name = Convert.ToString(_reader["SpecializationName"]);
                            Specialization doc = new Specialization()
                            {
                                SpecializationCode = code,
                                SpecializationName = Name
                            };
                            answer.Add(doc);
                        }
                    }
                }
            }
            return answer;
        }
        public List<Surgery> GetAllSurgeryTypeForToday()
        {
            List<Surgery> answer = new List<Surgery>();
            using (_conObj = new SqlConnection(SqlConnectionString.getConnectionString()))
            {
                _conObj.Open();
                using (_command = new SqlCommand("Select * from Surgery", _conObj))
                {
                    using (_reader = _command.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            int id = Convert.ToInt32(_reader["SurgeryId"]);
                            int DocId = Convert.ToInt32(_reader["DoctorId"]);
                            DateTime date = Convert.ToDateTime(_reader["SurgeryDate"]);
                            decimal start = Convert.ToDecimal(_reader["StartTime"]);
                            decimal end = Convert.ToDecimal(_reader["EndTime"]);
                            string cat = Convert.ToString(_reader["SurgeryCategory"]);
                            Surgery doc = new Surgery()
                            {
                                SurgeryId = id,
                                DoctorId = DocId,
                                SurgeryDate = date,
                                StartTime = start,
                                EndTime = end,
                                SurgeryCategory = cat,
                            };
                            answer.Add(doc);
                        }
                    }
                }
            }
            return answer;
        }
        public List<DoctorSpecialization> GetDoctorsBySpecialization(string specializationCode)
        {
            List<DoctorSpecialization> answer = new List<DoctorSpecialization>();
            using(_conObj = new SqlConnection(SqlConnectionString.getConnectionString()))
            {
                using(SqlDataAdapter adapter = new SqlDataAdapter("Select * from DoctorSpecialization where SpecializationCode = @code",_conObj))
                {
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.SelectCommand.Parameters.AddWithValue("@code", specializationCode);
                    using(DataTable dt = new DataTable())
                    {
                        adapter.Fill(dt);
                        foreach(DataRow row in dt.Rows)
                        {
                            DoctorSpecialization doctorSpecialization = new DoctorSpecialization()
                            {
                                DoctorId = Convert.ToInt32(row["DoctorId"]),
                                SpecializationCode = Convert.ToString(row["SpecializationCode"]),
                                SpecializationDate = Convert.ToDateTime(row["SpecailizationDate"])
                            };
                            answer.Add(doctorSpecialization);
                        }
                    }

                }
            }
            return answer;
        }
        public bool AddDoctor(Doctor dObj)
        {
            using(_conObj = new SqlConnection(SqlConnectionString.getConnectionString()))
            {
                using(SqlDataAdapter adapter = new SqlDataAdapter("Select DoctorName from Doctor", _conObj))
                {
                    using (DataTable dt = new DataTable())
                    {
                        adapter.Fill(dt);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        adapter.InsertCommand = builder.GetInsertCommand();
                        DataRow dr = dt.NewRow();
                        dr["DoctorName"] = dObj.DoctorName;
                        dt.Rows.Add(dr);
                        return adapter.Update(dt) > 0 ? true : false;

                    }
                }
            }
        }

        public bool AddSurgery(Surgery SObj)
        {
            using (_conObj = new SqlConnection(SqlConnectionString.getConnectionString()))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("Select * from Surgery", _conObj))
                {
                    using (DataTable dt = new DataTable())
                    {
                        adapter.Fill(dt);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        adapter.InsertCommand = builder.GetInsertCommand();
                        DataRow dr = dt.NewRow();
                        dr["DoctorId"] = SObj.DoctorId;
                        dr["SurgeryDate"] = SObj.SurgeryDate;
                        dr["StartTime"] = SObj.StartTime;
                        dr["EndTime"] = SObj.EndTime;
                        dr["SurgeryCategory"] = SObj.SurgeryCategory;
                        dt.Rows.Add(dr);
                        return adapter.Update(dt) > 0 ? true : false;

                    }
                }
            }
        }
        public bool UpdateDoctorDetails(Doctor dObj)
        {
            using (_conObj = new SqlConnection(SqlConnectionString.getConnectionString()))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("Select * from Doctor", _conObj))
                {
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", dObj.DoctorId);
                    using (DataTable dt = new DataTable())
                    {
                        if (dt != null)
                        {
                            adapter.Fill(dt);
                            DataRow dr =dt.AsEnumerable().FirstOrDefault(x => x.Field<int>("DoctorId") == dObj.DoctorId);
                            if (dr != null)
                            {
                                dr.BeginEdit();
                                dr["DoctorName"] = dObj.DoctorName;
                                dr.EndEdit();
                                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                                adapter.UpdateCommand = builder.GetUpdateCommand();
                                adapter.Update(dt);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool UpdateSurgery(Surgery SObj)
        {
            using (_conObj = new SqlConnection(SqlConnectionString.getConnectionString()))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("Select * from Surgery", _conObj))
                {
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", SObj.SurgeryId);
                    using (DataTable dt = new DataTable())
                    {
                        if (dt != null)
                        {
                            adapter.Fill(dt);
                            DataRow dr = dt.AsEnumerable().FirstOrDefault(x => x.Field<int>("SurgeryId") == SObj.SurgeryId);
                            if (dr != null)
                            {
                                dr.BeginEdit();
                                dr["DoctorId"] = SObj.DoctorId;
                                dr["SurgeryDate"] = SObj.SurgeryDate;
                                dr["StartTime"] = SObj.StartTime;
                                dr["EndTime"] = SObj.EndTime;
                                dr["SurgeryCategory"] = SObj.SurgeryCategory;
                                dr.EndEdit();
                                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                                adapter.UpdateCommand = builder.GetUpdateCommand();
                                adapter.Update(dt);

                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}

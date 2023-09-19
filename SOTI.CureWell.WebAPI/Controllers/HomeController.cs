﻿using SOTI.Curewell.DAL;
using SOTI.Curewell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOTI.CureWell.WebAPI.Controllers
{
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        private readonly ICureWell _cureWell = null;
        public HomeController()
        {
            
        }
        public HomeController(ICureWell cureWell)
        {
            _cureWell = cureWell;
        }

        [HttpGet]
        [Route("GetDoctor")]
        public IHttpActionResult GetDoctors()
        {
            var dt = _cureWell.GetAllDoctors();
            if (dt == null) return BadRequest();
            return Ok(dt);
        }


        [HttpGet]
        [Route("GetSpecialization")]
        public IHttpActionResult GetSpecialization()
        {
            var dt = _cureWell.GetAllSpecializations();
            if (dt == null) return BadRequest();
            return Ok(dt);
        }


        [HttpGet]
        [Route("GetSurgeryType")]
        public IHttpActionResult GetSurgeryType()
        {
            var dt = _cureWell.GetAllSurgeryTypeForToday();
            if (dt == null) return BadRequest();
            return Ok(dt);
        }

        [HttpGet]
        [Route("GetDoctorBySpecialization/{code}")]
        public IHttpActionResult GetDoctorBySpecialization([FromUri]string code)
        {
            var dt = _cureWell.GetDoctorsBySpecialization(code);
            if (dt == null) return BadRequest();
            return Ok(dt);
        }

        [HttpPost]
        [Route("AddDoctor")]
        public IHttpActionResult AddDoctor([FromBody] Doctor doctor)
        {
            var res = _cureWell.AddDoctor(doctor);
            if (res) return Ok();
            return BadRequest();
        }


        [HttpPost]
        [Route("AddSurgery")]
        public IHttpActionResult AddSurgery([FromBody] Surgery surgery)
        {
            var res = _cureWell.AddSurgery(surgery);
            if (res) return Ok();
            return BadRequest();
        }


        [HttpPut]
        [Route("UpdateDoctorDetails/{id}")]
        public IHttpActionResult UpdateDoctorDetails([FromUri] int id, [FromBody] Doctor doctor)
        {
            if (id != doctor.DoctorId) return BadRequest();
            var res = _cureWell.UpdateDoctorDetails(doctor);
            if (res) return Ok();
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateSurgery/{id}")]
        public IHttpActionResult UpdateSurgery([FromUri] int id,[FromBody] Surgery surgery)
        {
            if (id != surgery.SurgeryId) return BadRequest();
            var res = _cureWell.UpdateSurgery(surgery);
            if (res) return Ok();
            return BadRequest();
        }

    }
}
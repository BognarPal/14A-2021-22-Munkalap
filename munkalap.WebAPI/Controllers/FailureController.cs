using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using munkalap.service;
using munkalap.service.models;
using munkalap.service.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace munkalap.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FailureController : ControllerBase
    {
        private readonly FailureRepository failureRepository;

        public FailureController(ApplicationDbContext dbContext)
        {
            this.failureRepository = new FailureRepository(dbContext);
        }

        //TODO search???
        [HttpGet]
        public ActionResult GetAll()
        {
            return this.Run(() => Ok(failureRepository.GetAll()));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return this.Run(() =>
            {
                return Ok(failureRepository.GetById(id));
            });
        }

        [HttpPut]
        public ActionResult Create(Failure failure)
        {
            //TODO ellenőrzések
            return this.Run(() => Ok(failureRepository.Create(failure)));
        }

        [HttpPost("update")]
        public ActionResult Update(Failure failure)
        {
            //TODO ellenőrzések
            return this.Run(() => Ok(failureRepository.Update(failure)));
        }

        [HttpDelete]
        public ActionResult Delete(Failure failure)
        {
            return this.Run(() =>
            {
                if (failureRepository.GetById(failure.Id).WorkFinished != null)
                {
                    return BadRequest(new
                    {
                        ErrorMessage = "Kész munka nem törölhető"
                    });
                }
                failureRepository.Delete(failure);
                return Ok();
            });
        }

        [HttpPost("assign")]
        public ActionResult Assign(Failure failure)
        {
            //TODO ellenőrzések
            return this.Run(() => Ok(failureRepository.Assign(failure)));
        }

        [HttpPost("finish")]
        public ActionResult Finish(Failure failure)
        {
            //TODO ellenőrzések
            return this.Run(() => Ok(failureRepository.Finish(failure)));
        }

        [HttpPost("start")]
        public ActionResult Start(Failure failure)
        {
            //TODO ellenőrzések
            return this.Run(() => Ok(failureRepository.Start(failure)));
        }

        [HttpPost("check")]
        public ActionResult Check(Failure failure)
        {
            //TODO ellenőrzések
            return this.Run(() => Ok(failureRepository.Check(failure)));
        }




    }
}

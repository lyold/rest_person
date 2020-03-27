using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RegisterPerson.Domain.Model.Entities;
using RegisterPerson.Domain.Services.Interfaces;

namespace RegisterPerson.API.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            this._personService = personService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var person = _personService.Find(id);

            if(person != null)
                return Ok(_personService.FindAll());

            return BadRequest();
        }

        [HttpPost]
        public ActionResult Post(Person person)
        {
            try
            {
                return Ok(_personService.Create(person));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("{id}")]
        public ActionResult Put(Person person)
        {
            try
            {
                return Ok(_personService.Update(person));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _personService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

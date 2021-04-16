using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PedidosWeb_API.Data;
using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PedidosWeb_API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult<List<Client>> Get()
        {
            try
            {
                return _unitOfWork.ClientRepository.Get().ToList();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}", Name = "GetClientById")]
        public ActionResult<Client> Get(int id)
        {
            try
            {
                Client client = _unitOfWork.ClientRepository.GetById(x => x.ClientId == id);

                if (client == null)
                {
                    return NotFound();
                }

                return client;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Create
        [HttpPost]
        public ActionResult<Client> Post([FromBody] Client client)
        {
            try
            {
                _unitOfWork.ClientRepository.Add(client);
                _unitOfWork.Commit();

                return new CreatedAtRouteResult("GetClientById", new { id = client.ClientId }, client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Update
        [HttpPut("{id}")]
        public ActionResult<Client> Put(int id, [FromBody] Client client)
        {
            try
            {
                if (id != client.ClientId)
                {
                    return BadRequest("Ids Diferentes");
                }

                _unitOfWork.ClientRepository.Update(client);
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Client> Delete(int id)
        {
            try
            {
                Client client = _unitOfWork.ClientRepository.GetById(x => x.ClientId == id);

                if (client == null)
                {
                    return NotFound();
                }

                _unitOfWork.ClientRepository.Delete(client);
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

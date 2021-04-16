using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PedidosWeb_API.Data;
using PedidosWeb_API.Models;

namespace PedidosWeb_API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult<List<Order>> Get(){
            try
            { 
                return _unitOfWork.OrderRepository.GetOrderAndClients().ToList();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}", Name = "GetOrderById")]
        public ActionResult<Order> Get(int id){
            try
            {
                Order order = _unitOfWork.OrderRepository.GetByIdAndIncludeClient(x => x.OrderId == id);

                if(order == null)
                {
                    return NotFound();
                }

                return order;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Create
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                _unitOfWork.OrderRepository.Add(order);
                _unitOfWork.Commit();

                return new CreatedAtRouteResult("GetOrderById", new { id = order.OrderId }, order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Update
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            try
            {
                if(id != order.OrderId)
                {
                    return BadRequest("Ids Diferentes");
                }

                _unitOfWork.OrderRepository.Update(order);
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            try
            {
                Order order = _unitOfWork.OrderRepository.GetById(x => x.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }

                _unitOfWork.OrderRepository.Delete(order);
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
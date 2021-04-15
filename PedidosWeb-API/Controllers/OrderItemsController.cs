using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidosWeb_API.Data;
using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult<List<OrderItem>> Get()
        {
            try
            {
                return _unitOfWork.OrderItemRepository.Get().ToList();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}", Name = "GetOrderItemById")]
        public ActionResult<OrderItem> Get(int id)
        {
            try
            {
                OrderItem OrderItem = _unitOfWork.OrderItemRepository.GetById(x => x.OrderItemId == id);

                if (OrderItem == null)
                {
                    return NotFound();
                }

                return OrderItem;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Create
        [HttpPost]
        public ActionResult<OrderItem> Post([FromBody] OrderItem orderItem)
        {
            try
            {
                _unitOfWork.OrderItemRepository.Add(orderItem);
                _unitOfWork.Commit();

                return new CreatedAtRouteResult("GetOrderItemById", new { id = orderItem.OrderItemId }, orderItem);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Update
        [HttpPut("{id}")]
        public ActionResult<OrderItem> Put(int id, [FromBody] OrderItem orderItem)
        {
            try
            {
                if (id != orderItem.OrderItemId)
                {
                    return BadRequest("Ids Diferentes");
                }

                _unitOfWork.OrderItemRepository.Update(orderItem);
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<OrderItem> Delete(int id)
        {
            try
            {
                OrderItem orderItem = _unitOfWork.OrderItemRepository.GetById(x => x.OrderItemId == id);

                if (orderItem == null)
                {
                    return NotFound();
                }

                _unitOfWork.OrderItemRepository.Delete(orderItem);
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

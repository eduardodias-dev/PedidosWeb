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
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            try
            {
                return _unitOfWork.ProductRepository.Get().ToList();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                Product product = _unitOfWork.ProductRepository.GetById(x => x.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Create
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            try
            {
                _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.Commit();

                return new CreatedAtRouteResult("GetProductById", new { id = product.ProductId }, product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Update
        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return BadRequest("Ids Diferentes");
                }

                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            try
            {
                Product product = _unitOfWork.ProductRepository.GetById(x => x.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }

                _unitOfWork.ProductRepository.Delete(product);
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

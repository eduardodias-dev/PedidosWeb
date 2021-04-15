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
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            try
            {
                return _unitOfWork.CategoryRepository.Get().ToList();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                Category category = _unitOfWork.CategoryRepository.GetById(x => x.CategoryId == id);

                if (category == null)
                {
                    return NotFound();
                }

                return category;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Create
        [HttpPost]
        public ActionResult<Category> Post([FromBody] Category category)
        {
            try
            {
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Commit();

                return new CreatedAtRouteResult("GetCategoryById", new { id = category.CategoryId }, category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Update
        [HttpPut("{id}")]
        public ActionResult<Category> Put(int id, [FromBody] Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest("Ids Diferentes");
                }

                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> Delete(int id)
        {
            try
            {
                Category category = _unitOfWork.CategoryRepository.GetById(x => x.CategoryId == id);

                if (category == null)
                {
                    return NotFound();
                }

                _unitOfWork.CategoryRepository.Delete(category);
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

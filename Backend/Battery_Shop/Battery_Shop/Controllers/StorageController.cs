using AutoMapper;
using Battery_Shop.Data;
using Battery_Shop.Dtos;
using Battery_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StorageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Storages = await _unitOfWork.IStorageRepo.GetAllStorages();

            return Ok(Storages);
        }

        [HttpGet(":id")]
        public async Task<IActionResult> Get(int Id)
        {
            var Storage = await _unitOfWork.IStorageRepo.GetStorage(Id);

            if (Storage != null)
            {
                return Ok(Storage);
            }

            return NotFound(new { Message = $"Storage with Id:{Id} not found!" });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddStorageDto Storage)
        {
            if (ModelState.IsValid)
            {
                var AddedStorage = _mapper.Map<Storage>(Storage);

                await _unitOfWork.IStorageRepo.AddStorage(AddedStorage);
                await _unitOfWork.Complete();

                return Ok(AddedStorage);
            }

            return BadRequest(new { Message = "Invalid info!" });
        }

        [HttpPut(":id")]
        public async Task<IActionResult> Put(int Id, [FromBody] AddStorageDto Storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid info!" });
            }

            var OldStorage = await _unitOfWork.IStorageRepo.GetStorage(Id);

            if (OldStorage == null)
            {
                return NotFound(new { Message = $"Storage with Id:{Id} not found." });
            }

            _mapper.Map<AddStorageDto, Storage>(Storage, OldStorage);

            await _unitOfWork.Complete();

            return Ok(OldStorage);
        }

        [HttpDelete(":id")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Storage = await _unitOfWork.IStorageRepo.GetStorage(Id);

            if (Storage == null)
            {
                return NotFound(new { Message = $"Storage with Id:{Id} not found." });
            }

            _unitOfWork.IStorageRepo.DeleteStorage(Storage);
            await _unitOfWork.Complete();

            return Ok(Storage);
        }
    }
}

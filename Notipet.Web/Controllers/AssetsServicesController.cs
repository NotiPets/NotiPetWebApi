#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notipet.Data;
using Notipet.Domain;
using Notipet.Web.DataWrapper;
using Notipet.Web.DTO;
using Utilities;

namespace Notipet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsServicesController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public AssetsServicesController(NotiPetBdContext context)
        {
            _context = context;
        }

        // GET: api/AssetsServices
        [HttpGet]
        public async Task<ActionResult<JsendWrapper>> GetAssetsServices() => Ok(new JsendSuccess(await _context.AssetsServices.ToListAsync()));

        [HttpGet("{id}")]
        public async Task<ActionResult<JsendWrapper>> GetAssetsServicesById(int id) => Ok(new JsendSuccess(await _context.AssetsServices.FindAsync(id)));

        [HttpGet("ByBusiness/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> GetAssetsServicesByBusinessId(int businessId) => Ok(new JsendSuccess(await _context.AssetsServices.Where(x => x.BusinessId == businessId).ToListAsync()));

        [HttpPut("{id}")]
        public async Task<ActionResult<JsendWrapper>> PutAssetsServices(int id, AssetServiceDto assetServiceDto)
        {
            try
            {
                if (await _context.AssetsServices.AnyAsync(x => x.Id == id))
                {
                    var assetsServices = assetServiceDto.ConvertToType();
                    assetsServices.Id = id;
                    _context.Entry(assetsServices).State = EntityState.Modified;
                }
                else
                {
                    return BadRequest(new JsendFail(new { appointment = "AssetService not found" }));
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.AssetsServices.AnyAsync(x => x.Id == id))
                    {
                        return BadRequest(new JsendFail(new { appointment = "AssetService not found" }));
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(new JsendSuccess());
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        // POST: api/AssetsServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JsendWrapper>> PostAssetsServices(AssetServiceDto assetsServicesDto)
        {
            try
            {
                var assetsServices = assetsServicesDto.ConvertToType();
                _context.AssetsServices.Add(assetsServices);
                await _context.SaveChangesAsync();
                return Ok(new JsendSuccess(assetsServices));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }

        }
    }
}

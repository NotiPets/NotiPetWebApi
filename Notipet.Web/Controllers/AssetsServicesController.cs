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
        public async Task<ActionResult<IEnumerable<AssetsServices>>> GetAssetsServices() => await _context.AssetsServices.ToListAsync();

        [HttpGet("ByBusiness/{businessId}")]
        public async Task<ActionResult<IEnumerable<AssetsServices>>> GetAssetsServicesByBusinessId(int businessId) => await _context.AssetsServices.Where(x => x.BusinessId == businessId).ToListAsync();

        // POST: api/AssetsServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AssetsServices>> PostAssetsServices(AssetServiceDto assetsServicesDto)
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

using Microsoft.AspNetCore.Mvc;
using AccountService.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using AccountService.WebApi.Controller;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtensionsController : BaseApiController
    {
        [HttpGet("cargo-types")]
        public IActionResult GetCargoTypes()
        {
            var cargoTypes = Enum.GetValues(typeof(CargoType))
                .Cast<CargoType>()
                .Select(ct => new KeyValuePair<int, string>((int)ct, ct.ToString()))
                .ToList();

            return Ok(cargoTypes);
        }

        [HttpGet("cargo-types/{key}")]
        public IActionResult GetCargoTypeByKey(int key)
        {
            if (!Enum.IsDefined(typeof(CargoType), key))
                return NotFound();

            var cargoType = (CargoType)key;
            return Ok(new KeyValuePair<int, string>(key, cargoType.ToString()));
        }

        [HttpGet("vehicle-types")]
        public IActionResult GetVehicleTypes()
        {
            var vehicleTypes = Enum.GetValues(typeof(VehicleType))
                .Cast<VehicleType>()
                .Select(vt => new KeyValuePair<int, string>((int)vt, vt.ToString()))
                .ToList();

            return Ok(vehicleTypes);
        }

        [HttpGet("vehicle-types/{key}")]
        public IActionResult GetVehicleTypeByKey(int key)
        {
            if (!Enum.IsDefined(typeof(VehicleType), key))
                return NotFound();

            var vehicleType = (VehicleType)key;
            return Ok(new KeyValuePair<int, string>(key, vehicleType.ToString()));
        }
    }
} 
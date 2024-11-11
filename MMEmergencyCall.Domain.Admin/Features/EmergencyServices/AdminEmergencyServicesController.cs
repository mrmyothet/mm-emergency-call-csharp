﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MMEmergencyCall.Domain.Admin.Features.EmergencyServices;

[Route("api/Admin/EmergencyServices")]
[ApiController]
public class AdminEmergencyServicesController : ControllerBase
{
    private readonly AdminEmergencyServicesService _adminEmergencyServicesService;

    public AdminEmergencyServicesController(
        AdminEmergencyServicesService emergencyServiceService
    )
    {
        _adminEmergencyServicesService = emergencyServiceService;
    }

    [HttpGet("ServiceStatus/{serviceStatus}")]
    public async Task<IActionResult> GetEmergencyServicesByStatus(string serviceStatus)
    {
        var response = await _adminEmergencyServicesService.GetEmergencyServicesByStatus(
            serviceStatus
        );
        return Ok(response);
    }
}

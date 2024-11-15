﻿namespace MMEmergencyCall.Domain.Client.Features.EmergencyRequests;

[Route("api/[controller]")]
[ApiController]
//[CustomAuthorize]
public class EmergencyRequestController : BaseController
{
    private readonly EmergencyRequestService _emergencyRequestService;

    public EmergencyRequestController(EmergencyRequestService emergencyRequestService)
    {
        _emergencyRequestService = emergencyRequestService;
    }

    [HttpGet("pageNo/{pageNo}/pageSize/{pageSize}")]
    public async Task<IActionResult> GetEmergencyRequests(string? userId , string? serviceId, string? providerId,
        string? status, string? townshipCode, int pageNo = 1, int pageSize = 10)
    {
        if (pageNo <= 0 || pageSize <= 0)
        {
            return BadRequest("Page number and page size must be greater than zero.");
        }

        var model = await _emergencyRequestService.GetEmergencyRequests(pageNo, pageSize,userId,serviceId,providerId,status,townshipCode);
        return Execute(model);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmergencyRequestById(int id)
    {
        var model = await _emergencyRequestService.GetEmergencyRequestById(id);
        return Execute(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmergencyRequest(EmergencyRequestRequestModel request)
    {
        var validationResult = ValidateEmergencyRequest(request);
        if (validationResult != null)
        {
            return validationResult;
        }

        var model = await _emergencyRequestService.AddEmergencyRequest(request);
        return Execute(model);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateEmergencyRequest(int id,EmergencyRequestRequestModel request)
    //{
    //    var validationResult = ValidateEmergencyRequest(request);
    //    if (validationResult != null)
    //    {
    //        return validationResult;
    //    }

    //    var model = await _emergencyRequestService.UpdateEmergencyRequest(id, request);
    //    return Execute(model);
    //}

    private IActionResult? ValidateEmergencyRequest(EmergencyRequestRequestModel? request)
    {
        if(request is null)
        {
            return BadRequest("Request model cannot be null");
        }
        if(request.UserId < 1)
        {
            return BadRequest("Invalid User Id");
        }
        if (request.ProviderId < 1)
        {
            return BadRequest("Invalid Provider Id");
        }
        if (request.ServiceId < 1)
        {
            return BadRequest("Invalid Service Id");
        }

        if(request.Status != "Pending" && request.Status != "Completed")
        {
            return BadRequest("Status should be either Pending or Completed");
        }

        return null;
    }
}

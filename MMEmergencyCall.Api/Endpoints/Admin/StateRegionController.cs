﻿using Microsoft.AspNetCore.Mvc;
using MMEmergencyCall.Domain.Admin.Features.StateRegion;
using MMEmergencyCall.Shared;

namespace MMEmergencyCall.Api.Endpoints.Admin;

[Route("api/[controller]")]
[ApiController]
public class StateRegionController : ControllerBase
{
    private readonly StateRegionService _stateRegionService;

    public StateRegionController(StateRegionService stateRegionService)
    {
        _stateRegionService = stateRegionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var model = await _stateRegionService.GetAllAsync();
        if (!model.IsSuccess)
            return NotFound(model);

        return Ok(model);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var model = await _stateRegionService.GetByIdAsync(id);
        if (!model.IsSuccess)
            return NotFound(model);

        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StateRegionRequestModel requestModel)
    {
        Result<StateRegionResponseModel> model = null;
        if (string.IsNullOrEmpty(requestModel.StateRegionCode))
        {
            model = Result<StateRegionResponseModel>.Failure("StateRegionCode is required.");
            goto badRequest;
        }
        if (string.IsNullOrEmpty(requestModel.StateRegionNameEn))
        {
            model = Result<StateRegionResponseModel>.Failure("StateRegionNameEn is required.");
            goto badRequest;
        }
        if (string.IsNullOrEmpty(requestModel.StateRegionNameMm))
        {
            model = Result<StateRegionResponseModel>.Failure("StateRegionNameMm is required.");
            goto badRequest;
        }

        model = await _stateRegionService.CreateAsync(requestModel);
        if (model.IsSuccess)
            return Ok(model);

        badRequest:
        return BadRequest(model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] StateRegionRequestModel requestModel)
    {
        Result<StateRegionResponseModel> model = null;
        if (string.IsNullOrEmpty(requestModel.StateRegionCode))
        {
            model = Result<StateRegionResponseModel>.Failure("StateRegionCode is required.");
            goto badRequest;
        }
        if (string.IsNullOrEmpty(requestModel.StateRegionNameEn))
        {
            model = Result<StateRegionResponseModel>.Failure("StateRegionNameEn is required.");
            goto badRequest;
        }
        if (string.IsNullOrEmpty(requestModel.StateRegionNameMm))
        {
            model = Result<StateRegionResponseModel>.Failure("StateRegionNameMm is required.");
            goto badRequest;
        }

        model = await _stateRegionService.UpdateAsync(id, requestModel);
        if (!model.IsSuccess)
            return NotFound(model);

        return Ok(model);

    badRequest:
        return BadRequest(model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await _stateRegionService.DeleteAsync(id);
        if (!model.IsSuccess)
            return NotFound(model);

        return Ok(model);
    }
}
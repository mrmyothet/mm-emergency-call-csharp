﻿namespace MMEmergencyCall.Domain.Client;

public class BaseController : ControllerBase
{
    private IActionResult InternalServerError(object? obj = null)
    {
        return StatusCode(500, obj);
    }

    protected IActionResult Execute<T>(Result<T> model)
    {
        if (model.IsValidationError)
            return BadRequest(model);

        if (model.IsNotFoundError)
            return NotFound(model);

        if (model.IsError)
            return InternalServerError(model);

        return Ok(model);
    }
}

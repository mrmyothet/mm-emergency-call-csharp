﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMEmergencyCall.Domain.Client.Features.EmergencyServices;

public static class EmergencyServiceExtension
{
    public static void AddEmergencyServiceService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<EmergencyServiceService>();
    }
}

﻿namespace MMEmergencyCall.Domain.Admin.Features.TownshipTopTenService;

public class TownshipTopTenServiceService
{
	private readonly AppDbContext _db;
	private readonly ILogger<TownshipTopTenServiceService> _logger;
	public TownshipTopTenServiceService(ILogger<TownshipTopTenServiceService> logger, AppDbContext db)
	{
		_logger = logger;
		_db = db;
	}

	public async Task<Result<List<TownshipTopTenServiceResponseModel>>> GetTownshipTopTenService(string townshipCode)
	{
		try
		{

			string query = $@"WITH RankedServices AS (
    SELECT 
        ServiceId,
        UserId,
        ServiceGroup,
        ServiceType,
        ServiceName,
        PhoneNumber,
        Location,
        Availability,
        TownshipCode,
        ServiceStatus,
        Lng,
        Ltd,
        ROW_NUMBER() OVER (PARTITION BY TownshipCode ORDER BY ServiceId) AS RowNum
    FROM EmergencyService
)
SELECT 
    ServiceId,
    UserId,
    ServiceGroup,
    ServiceType,
    ServiceName,
    PhoneNumber,
    Location,
    Availability,
    TownshipCode,
    ServiceStatus,
    Lng,
    Ltd
FROM RankedServices
WHERE RowNum <= 10;";

			var responseData = await _db.Database.SqlQueryRaw<TownshipTopTenServiceResponseModel>(query).ToListAsync();

            _logger.LogInformation(responseData.ToString());

			return Result<List<TownshipTopTenServiceResponseModel>>.Success(responseData);
		}
		catch (Exception ex)
		{
			return Result<List<TownshipTopTenServiceResponseModel>>.Failure(ex.ToString());
		}

	}
}

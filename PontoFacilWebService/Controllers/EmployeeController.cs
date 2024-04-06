using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PontoFacilSharedData.Data.Dtos;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController
{

    private IEmployeeService _employeeService;
    private ITimeRecordService _timeRecordService;
    
    public EmployeeController(ITimeRecordService timeRecordService, IEmployeeService employeeService)
    {
        _timeRecordService = timeRecordService;
        _employeeService = employeeService;
    }
    
    [HttpPost("RecordEntry/{employeeId}")]
    public async Task<TimeRecordResponse> RecordEntry([FromRoute] int employeeId)
    {
        TimeRecordResponse response = await _timeRecordService.AddTimeRecord(employeeId);
        
        return response;
    }
    
    [HttpPatch("RecordLeave/{employeeId}")]
    public async Task<TimeRecordResponse> RecordLeave([FromRoute] int employeeId)
    {
        TimeRecordResponse response = await _timeRecordService.AddLeaveRecord(employeeId);
        
        return response;
    }
}
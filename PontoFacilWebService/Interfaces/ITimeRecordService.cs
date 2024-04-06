using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;

namespace PontoFacilWebService.Interfaces;

public interface ITimeRecordService
{
    Task<TimeRecordResponse> AddTimeRecord(int employeeId);
    Task<TimeRecordResponse> AddLeaveRecord(int employeeId);
}
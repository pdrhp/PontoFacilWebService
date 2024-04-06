using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;

namespace PontoFacilWebService.Interfaces;

public interface ITimeRecordRepository
{
    Task<TimeRecord> NewEntryRecord(TimeRecord entryTimeRecord);
    Task<TimeRecord> NewLeaveRecord(DateTime leaveTime, int employeeId);
    Task<bool> HasOpenTimeRecord(int employeeId);
}
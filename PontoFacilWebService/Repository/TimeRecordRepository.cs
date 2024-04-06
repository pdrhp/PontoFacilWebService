using Microsoft.EntityFrameworkCore;
using PontoFacilSharedData.Data;
using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Repository;

public class TimeRecordRepository : ITimeRecordRepository
{
    private PontoFacilDbContext _context;

    public TimeRecordRepository(PontoFacilDbContext context)
    {
        _context = context;
    }

    public async Task<TimeRecord> NewEntryRecord(TimeRecord entryRecord)
    {
        var createdEntry = _context.TimeRecords.Add(entryRecord);
        await _context.SaveChangesAsync();
        return createdEntry.Entity;
    }
    
    public async Task<TimeRecord> NewLeaveRecord(DateTime leaveTime, int employeeId)
    {
        var timeRecord = _context.TimeRecords.First(tr => tr.EmployeeId == employeeId && tr.LeaveTime == null);
        timeRecord.LeaveTime = leaveTime;
        await _context.SaveChangesAsync();
        return timeRecord;
    }
    
    
    public async Task<Boolean> HasOpenTimeRecord(int employeeId)
    {
        return await _context.TimeRecords.AnyAsync(tr => tr.EmployeeId == employeeId && tr.LeaveTime == null);
    }
}
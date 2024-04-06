using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Interfaces;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Services;

public class TimeRecordService : ITimeRecordService
{
    private ITimeRecordRepository _recordRepository;
    private IMapperService _mapperService;

    public TimeRecordService(ITimeRecordRepository recordRepository, IMapperService mapperService)
    {
        _recordRepository = recordRepository;
        _mapperService = mapperService;
    }
    public async Task<TimeRecordResponse> AddTimeRecord(int employeeId)
    {
        var checkRecord = await _recordRepository.HasOpenTimeRecord(employeeId);
        if (checkRecord)
        {
            throw new BadHttpRequestException("An entry record already exists for this employee without a leave time.");
        }

        var newEntryTime = _mapperService.CreateEntryTimeRecord(DateTime.Now, employeeId);
        var createdEntryRecord = await _recordRepository.NewEntryRecord(newEntryTime);

        return new TimeRecordResponse(true, "Horario de entrada registrado com sucesso!", createdEntryRecord);
    }

    public async Task<TimeRecordResponse> AddLeaveRecord(int employeeId)
    {
        var checkRecord = await _recordRepository.HasOpenTimeRecord(employeeId);
        if (!checkRecord)
        {
            throw new BadHttpRequestException("No entry record exists for this employee without a leave time.");
        }

        var createdLeaveRecord = await _recordRepository.NewLeaveRecord(DateTime.Now, employeeId);

        return new TimeRecordResponse(true, "Horario de saída registrado com sucesso!", createdLeaveRecord);
    }
}
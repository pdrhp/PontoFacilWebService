using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoFacilSharedData.Models;

public class TimeRecord
{
    [Key]
    [Required]
    public int RecordId { get; set; }
    [ForeignKey("Employee")]
    [Required]
    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? LeaveTime { get; set; }
}
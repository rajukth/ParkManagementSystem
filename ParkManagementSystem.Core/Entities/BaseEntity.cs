namespace ParkManagementSystem.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime RecDate { get; set; } = DateTime.UtcNow;
    public int RecVersion { get; set; }
    public string RecStatus { get; set; } = Constants.RecStatus.Active;

}
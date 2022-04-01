namespace LecturesAttendanceSystem.Services.Interfaces
{
    public interface IClaimDecorator
    {
        public long Id { get; }
        
        public string Name { get; }
        
        public string Role { get; }
    }
}
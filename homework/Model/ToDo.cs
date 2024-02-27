namespace homework.Model
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Label { get; set; } = default!;
        public bool IsDone { get; set; } = default!;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedTime { get; set; } = default!;
    }
}

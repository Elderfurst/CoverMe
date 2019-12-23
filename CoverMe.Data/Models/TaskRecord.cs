using System;

namespace CoverMe.Data.Models
{
    public class TaskRecord
    {
        public int Id { get; set; }
        public DateTime TimeProcessed { get; set; }
        public int RecordsProcessed { get; set; }
    }
}

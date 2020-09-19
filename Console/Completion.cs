using System.ComponentModel.DataAnnotations.Schema;

namespace Prom_IT
{
    [Table("completions")]
    public class Completion
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int Frequency { get; set; }
    }
}

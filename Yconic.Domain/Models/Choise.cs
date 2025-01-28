using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class Choice
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public int? Index { get; set; }
        [NotMapped]
        public object? Logprobs { get; set; }
        public string? FinishReason { get; set; }
    }
}

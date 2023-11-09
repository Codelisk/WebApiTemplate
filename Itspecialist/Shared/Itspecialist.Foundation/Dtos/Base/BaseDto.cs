using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Codelisk.GeneratorAttributes.WebAttributes.Dto;

namespace Itspecialist.Foundation.Dtos.Base
{
    public class BaseDto
    {
        [Id]
        [Key]
        [JsonPropertyName("id")]
        public Guid id { get; set; }
    }
}

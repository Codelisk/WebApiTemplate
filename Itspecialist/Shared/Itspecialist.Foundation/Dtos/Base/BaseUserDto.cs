using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Itspecialist.Foundation.Dtos.Base
{
    public class BaseUserDto : BaseDto
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        public bool IsUser(object userId)
        {
            return (Guid)userId == UserId;
        }
    }
}

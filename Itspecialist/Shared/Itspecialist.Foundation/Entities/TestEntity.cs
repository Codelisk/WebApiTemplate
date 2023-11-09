using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codelisk.GeneratorAttributes.WebAttributes.Repository;
using Itspecialist.Foundation.Dtos;
using Itspecialist.Foundation.Dtos.User;

namespace Itspecialist.Foundation.Entities
{
    [Entity(typeof(TestDto))]
    public class TestEntity : TestDto
    {
    }
}

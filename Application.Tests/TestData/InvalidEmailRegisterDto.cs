using Application.Tests.ValidatorsTests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.TestData
{
    internal class InvalidEmailRegisterDto : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { RegisterDtoValidatorTests.GetValidRegisterDto() with { Email = null } };
            yield return new object[] { RegisterDtoValidatorTests.GetValidRegisterDto() with { Email = string.Empty } };
            yield return new object[] { RegisterDtoValidatorTests.GetValidRegisterDto() with { Email = "Invalid" } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

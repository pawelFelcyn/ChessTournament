using Application.Tests.ValidatorsTests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.TestData
{
    public class InvalidFirstNameRegisterDto : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { RegisterDtoValidatorTests.GetValidRegisterDto() with { FirstName = null } };
            yield return new object[] { RegisterDtoValidatorTests.GetValidRegisterDto() with { FirstName = string.Empty } };
            yield return new object[] { RegisterDtoValidatorTests.GetValidRegisterDto() with { FirstName = "Toooooooo looong name" } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

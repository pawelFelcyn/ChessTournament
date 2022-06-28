using Application.Tests.ValidatorsTests;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.TestData
{
    public class InvalidLastNameRegisterDto : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { RegisterDtoValidatorTests.GetValidRegisterDto() with { LastName = null } };
            yield return new object[] { RegisterDtoValidatorTests.GetValidRegisterDto() with { LastName = string.Empty } };
            yield return new object[] { RegisterDtoValidatorTests.GetValidRegisterDto() with { LastName = "Toooooooo looong name" } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

using Xunit;
using Microsoft.Ilasm;

namespace Microsoft.Ilasm.Tests
{
    public class ScannerTests
    {
        [Fact]
        public void Test()
        {
            Assert.Equal(42, new Scanner().Test());
        }
    }
}

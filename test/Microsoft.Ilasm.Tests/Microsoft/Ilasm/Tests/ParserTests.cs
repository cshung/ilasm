namespace Microsoft.Ilasm.Tests
{
    using System;
    using Xunit;

    public class ParserTests
    {
        [Fact]
        public void TestConstructingParserWithNull()
        {
            bool argNullThrown = false;
            try
            {
                Parser Parser = new Parser(null);
            }
            catch (ArgumentNullException argNull)
            {
                Assert.Equal("scanner", argNull.ParamName);
                argNullThrown = true;
            }
            Assert.True(argNullThrown);
        }
    }
}

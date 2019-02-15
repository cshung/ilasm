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

        [Fact]
        public void DottedNameWithSpace()
        {
            Scanner scanner = new Scanner("a. b");
            Parser parser = new Parser(scanner);
            parser.ParseDottedName();
            Assert.Single(parser.Errors);
            Assert.Equal(1, parser.Errors[0].Line);
            Assert.Equal(4, parser.Errors[0].Column);
            Assert.Equal("TODO: Localization", parser.Errors[0].Message);
        }

        [Fact]
        public void ParseSample()
        {
            Scanner scanner = new Scanner(TestData.SampleFile);
            Parser parser = new Parser(scanner);
            parser.ParseDecl();
            parser.ParseDecl();
        }
    }
}

namespace Microsoft.Ilasm.Tests
{
    using Xunit;
    using Microsoft.Ilasm;
    using System;

    public class ScannerTests
    {
        [Fact]
        public void TestConstructingScannerWithNull()
        {
            bool argNullThrown = false;
            try
            {
                Scanner scanner = new Scanner(null);
            }
            catch (ArgumentNullException argNull)
            {
                Assert.Equal("text", argNull.ParamName);
                argNullThrown = true;
            }
            Assert.True(argNullThrown);
        }

        [Fact]
        public void TestLeadingSpace()
        {
            Scanner scanner = new Scanner("    .assembly");
            Assert.Equal(TokenType.Assembly, scanner.Token.TokenType);
        }

        [Fact]
        public void TestScanningSample()
        {
            Scanner scanner = new Scanner(TestData.SampleFile);
            Assert.Equal(TokenType.Assembly, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Extern, scanner.Token.TokenType);
            scanner.Scan();            
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("mscorlib", scanner.Token.TokenText);
            scanner.Scan();
            Assert.Equal(TokenType.Lbrace, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Rbrace, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Assembly, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("HelloWorld", scanner.Token.TokenText);
            scanner.Scan();
            Assert.Equal(TokenType.Lbrace, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Rbrace, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Module, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("HelloWorld", scanner.Token.TokenText);
            scanner.Scan();
            Assert.Equal(TokenType.Dot, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("exe", scanner.Token.TokenText);
            scanner.Scan();
            Assert.Equal(TokenType.NameSpace, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("Hello", scanner.Token.TokenText);
            scanner.Scan();
            Assert.Equal(TokenType.Lbrace, scanner.Token.TokenType);
            scanner.Scan();
            Assert.Equal(TokenType.Class, scanner.Token.TokenType);
        }

    }
}

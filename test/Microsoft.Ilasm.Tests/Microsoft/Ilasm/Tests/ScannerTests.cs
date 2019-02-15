namespace Microsoft.Ilasm.Tests
{
    using Xunit;
    using Microsoft.Ilasm;
    using System;
    using Xunit.Abstractions;

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
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Assembly, scanner.Token.TokenType);
        }

        [Fact]
        public void TestScanningSample()
        {
            Scanner scanner = new Scanner(TestData.SampleFile);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Assembly, scanner.Token.TokenType);
            Assert.Equal(2, scanner.Line);
            Assert.Equal(10, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Extern, scanner.Token.TokenType);
            Assert.Equal(2, scanner.Line);
            Assert.Equal(17, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("mscorlib", scanner.Token.TokenText);
            Assert.Equal(2, scanner.Line);
            Assert.Equal(26, scanner.Column); 
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Lbrace, scanner.Token.TokenType);
            Assert.Equal(2, scanner.Line);
            Assert.Equal(28, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Rbrace, scanner.Token.TokenType);
            Assert.Equal(2, scanner.Line);
            Assert.Equal(29, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Assembly, scanner.Token.TokenType);
            Assert.Equal(3, scanner.Line);
            Assert.Equal(10, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("HelloWorld", scanner.Token.TokenText);
            Assert.Equal(3, scanner.Line);
            Assert.Equal(21, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Lbrace, scanner.Token.TokenType);
            Assert.Equal(3, scanner.Line);
            Assert.Equal(23, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Rbrace, scanner.Token.TokenType);
            Assert.Equal(3, scanner.Line);
            Assert.Equal(24, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Module, scanner.Token.TokenType);
            Assert.Equal(4, scanner.Line);
            Assert.Equal(8, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("HelloWorld", scanner.Token.TokenText);
            Assert.Equal(4, scanner.Line);
            Assert.Equal(19, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Dot, scanner.Token.TokenType);
            Assert.Equal(4, scanner.Line);
            Assert.Equal(20, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("exe", scanner.Token.TokenText);
            Assert.Equal(4, scanner.Line);
            Assert.Equal(23, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.NameSpace, scanner.Token.TokenType);
            Assert.Equal(5, scanner.Line);
            Assert.Equal(11, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Id, scanner.Token.TokenType);
            Assert.Equal("Hello", scanner.Token.TokenText);
            Assert.Equal(5, scanner.Line);
            Assert.Equal(17, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Lbrace, scanner.Token.TokenType);
            Assert.Equal(6, scanner.Line);
            Assert.Equal(2, scanner.Column);
            scanner.Scan(isWhitespaceAccepted: true);
            Assert.Equal(TokenType.Class, scanner.Token.TokenType);
        }

    }
}

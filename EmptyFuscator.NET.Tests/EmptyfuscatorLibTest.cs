namespace EmptyFuscator.NET.Tests;

public class EmptyfuscatorLibTest
{
    private readonly EmptyfuscatorLib _emptyfuscator = new();
    private static readonly string HangulFiller = "ﾠ";
    private static readonly string HangulJungseongFiller = "ᅠ";

    [Fact]
    public void Obfuscate_WithValidInput_ReturnsObfuscatedString()
    {
        // Arrange
        const string input = "Hello, World!";

        // Act
        var result = _emptyfuscator.Obfuscate(input);

        // Assert
        Assert.NotEqual(input, result);
        Assert.Contains(HangulFiller, result);
        Assert.Contains(HangulJungseongFiller, result);
    }

    [Fact]
    public void Obfuscate_WithEmptyInput_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _emptyfuscator.Obfuscate(string.Empty));
    }

    [Fact]
    public void Deobfuscate_WithValidInput_ReturnsOriginalString()
    {
        // Arrange
        var original = "Test123!@#";
        var obfuscated = _emptyfuscator.Obfuscate(original);

        // Act
        var result = _emptyfuscator.Deobfuscate(obfuscated);

        // Assert
        Assert.Equal(original, result);
    }

    [Fact]
    public void RoundTrip_WithVariousInputs_WorksCorrectly()
    {
        // Arrange
        var testInputs = new[]
        {
            "Hello, World!",
            "function test() { return 42; }",
            "Special characters: áéíóú ñ € ¥ © ®",
            "1234567890!@#$%^&*()"
        };

        foreach (var input in testInputs)
        {
            // Act
            var obfuscated = _emptyfuscator.Obfuscate(input);
            var deobfuscated = _emptyfuscator.Deobfuscate(obfuscated);

            // Assert
            Assert.Equal(input, deobfuscated);
        }
    }

    [Fact]
    public void StringToBinary_WithValidInput_ReturnsBinaryString()
    {
        // Arrange
        const string input = "ABC";

        // Act
        var result = _emptyfuscator.StringToBinary(input);

        // Assert
        Assert.Equal("010000010100001001000011", result);
    }

    [Fact]
    public void BinaryToString_WithValidInput_ReturnsOriginalString()
    {
        // Arrange
        const string binary = "010000010100001001000011";

        // Act
        var result = _emptyfuscator.BinaryToString(binary);

        // Assert
        Assert.Equal("ABC", result);
    }

    [Fact]
    public void ObfuscateDeobfuscate_WithUnicode_PreservesCharacters()
    {
        // Arrange
        const string input = "Unicode test: 你好 😀 öäü";

        // Act
        var obfuscated = _emptyfuscator.Obfuscate(input);
        var result = _emptyfuscator.Deobfuscate(obfuscated);

        // Assert
        Assert.Equal(input, result);
    }

    [Fact]
    public void MyTest()
    {
        // Arrange
        const string obfuscated = "ﾠᅠᅠﾠﾠﾠﾠᅠﾠᅠᅠﾠᅠᅠﾠﾠﾠᅠᅠﾠﾠᅠﾠᅠﾠᅠᅠᅠﾠﾠᅠﾠﾠᅠᅠᅠﾠᅠﾠﾠﾠﾠᅠﾠᅠﾠﾠﾠﾠﾠᅠﾠﾠﾠᅠﾠﾠᅠﾠﾠﾠﾠﾠᅠﾠᅠᅠﾠﾠᅠᅠᅠﾠᅠᅠᅠﾠﾠᅠﾠﾠᅠᅠﾠﾠᅠﾠᅠﾠᅠᅠﾠﾠﾠﾠᅠﾠᅠᅠᅠﾠᅠﾠﾠﾠﾠᅠﾠﾠﾠﾠᅠﾠﾠᅠﾠﾠﾠᅠﾠﾠﾠᅠﾠᅠﾠﾠᅠ";
        const string original = "alert(\"Agreat!\")";

        // Act
        var result = _emptyfuscator.Deobfuscate(obfuscated);

        // Assert
        Assert.Equal(original, result);
    }
}
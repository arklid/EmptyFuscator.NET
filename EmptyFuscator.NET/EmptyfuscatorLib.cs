using System.Text;

namespace EmptyFuscator.NET;

/// <summary>
/// Library for obfuscating and deobfuscating text using special Unicode characters
/// </summary>
public class EmptyfuscatorLib
{
    // Special Unicode characters used for obfuscation
    private static readonly string HangulFiller = "ﾠ";
    private static readonly string HangulJungseongFiller = "ᅠ";

    /// <summary>
    /// Obfuscates text using Hangul Filler characters
    /// </summary>
    /// <param name="text">Text to obfuscate</param>
    /// <returns>Obfuscated text</returns>
    public string Obfuscate(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Input text cannot be empty", nameof(text));

        // Convert text to binary and replace with special characters
        var binaryString = StringToBinary(text);
        var obfuscated = binaryString
            .Replace("0", HangulFiller)
            .Replace("1", HangulJungseongFiller);

        return obfuscated;
    }

    /// <summary>
    /// Deobfuscates text that was obfuscated using Hangul Filler characters
    /// </summary>
    /// <param name="obfuscatedText">Obfuscated text to deobfuscate</param>
    /// <returns>Original deobfuscated text</returns>
    public string Deobfuscate(string obfuscatedText)
    {
        if (string.IsNullOrEmpty(obfuscatedText))
            throw new ArgumentException("Input text cannot be empty", nameof(obfuscatedText));

        // Replace special characters with binary digits
        var binaryString = obfuscatedText
            .Replace(HangulFiller, "0")
            .Replace(HangulJungseongFiller, "1");

        // Convert binary back to text
        return BinaryToString(binaryString);
    }

    /// <summary>
    /// Converts a string to its binary representation
    /// </summary>
    /// <param name="input">String to convert</param>
    /// <returns>Binary representation of the string</returns>
    public string StringToBinary(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);

        var binaryString = new StringBuilder();
        foreach (var b in bytes)
        {
            var binary = Convert.ToString(b, 2).PadLeft(8, '0');
            binaryString.Append(binary);
        }

        return binaryString.ToString();
    }

    /// <summary>
    /// Converts a binary string back to a text string
    /// </summary>
    /// <param name="binaryString">Binary string to convert</param>
    /// <returns>Decoded text</returns>
    public string BinaryToString(string binaryString)
    {
        var bytes = new List<byte>();

        // Process each 8 bits (1 byte) at a time
        for (var i = 0; i < binaryString.Length; i += 8)
        {
            if (i + 8 <= binaryString.Length)
            {
                var chunk = binaryString.Substring(i, 8);
                var b = Convert.ToByte(chunk, 2);
                bytes.Add(b);
            }
        }

        return Encoding.UTF8.GetString(bytes.ToArray());
    }
}
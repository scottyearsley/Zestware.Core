using System;
using System.Data.HashFunction.xxHash;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Zestware.Data;

/// <summary>
/// Hashing methods.
/// </sumary>
public static class Hasher
{
    /// <summary>
    /// Generates a unique xxhash from the provided <see cref="string"/>.
    /// </summary>
    /// <param name="text">The <see cref="string"/> to be hashed.</param>
    /// <returns>A hexadecimal hash as a <see cref="string"/>.</returns>
    public static string XxHash(string? text)
    {
        ArgumentNullException.ThrowIfNull(text);
        
        var stringBytes = Encoding.UTF8.GetBytes(text);
        var hasher = xxHashFactory.Instance.Create();
        var hashedValue = hasher.ComputeHash(stringBytes);

        return hashedValue.AsHexString();
    }
    
    /// <summary>
    /// Generates a unique xxhash for the specified <see cref="string"/> asynchronously.
    /// </summary>
    /// <param name="text">The <see cref="string"/> to be hashed.</param>
    /// <returns>A hexadecimal hash as a <see cref="string"/></returns>
    public static async Task<string> XxHashAsync(string? text)
    {
        ArgumentNullException.ThrowIfNull(text);
        
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
        var hasher = xxHashFactory.Instance.Create();
        var hashedValue = await hasher.ComputeHashAsync(stream);
    
        return hashedValue.AsHexString();
    }
}
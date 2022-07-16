﻿using System;
using System.Data.HashFunction.xxHash;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Zestware.Data;

public static class Hasher
{
    /// <summary>
    /// Generates a unique xxhash for the specified <see cref="string"/>.
    /// </summary>
    /// <param name="text">The <see cref="string"/> to be hashed.</param>
    /// <returns>A hexadecimal hash as a <see cref="string"/></returns>
    public static string XxHash(string? text)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }
        
        var stringBytes = Encoding.UTF8.GetBytes(text);
        var hasher = xxHashFactory.Instance.Create();
        var hashedValue = hasher.ComputeHash(stringBytes);

        return hashedValue.AsHexString();
    }
    
    /// <summary>
    /// Generates a unique xxhash for the specified <see cref="string"/>.
    /// </summary>
    /// <param name="text">The <see cref="string"/> to be hashed.</param>
    /// <returns>A hexadecimal hash as a <see cref="string"/></returns>
    public static async Task<string> XxHashAsync(string? text)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }
        
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
        var hasher = xxHashFactory.Instance.Create();
        var hashedValue = await hasher.ComputeHashAsync(stream);
    
        return hashedValue.AsHexString();
    }
}
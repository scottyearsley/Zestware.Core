using System;
using System.Collections.Generic;

namespace Zestware.Collections;

/// <summary>
/// A case-insensitive string-keyed dictionary of T.
/// </summary>
/// <typeparam name="T">The value type.</typeparam>
public class CaseInsensitiveDictionary<T>: Dictionary<string, T>
{
    public CaseInsensitiveDictionary()
        : base(StringComparer.OrdinalIgnoreCase)
    {
    }
    
    public CaseInsensitiveDictionary(IDictionary<string, T> dictionary)
        : base(dictionary, StringComparer.OrdinalIgnoreCase)
    {
    }
    
    public CaseInsensitiveDictionary(IEnumerable<KeyValuePair<string, T>> collection)
        : base(collection, StringComparer.OrdinalIgnoreCase)
    {
    }
}
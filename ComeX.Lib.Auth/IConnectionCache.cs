using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ComeX.Lib.Auth
{
    interface IConnectionCache : IReadOnlyDictionary<string, TokenData>
    {
    }
}
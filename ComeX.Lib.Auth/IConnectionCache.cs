using System.Collections.Generic;

namespace ComeX.Lib.Auth
{
    interface IConnectionCache : IReadOnlyDictionary<string, TokenData>
    {
    }
}
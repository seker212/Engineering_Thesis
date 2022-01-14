using System.Collections.Generic;

namespace ComeX.Lib.Auth
{
    public interface IConnectionCache : IReadOnlyDictionary<string, TokenData>
    {
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting
{
    public interface IUserRefreshTokenStore
    {
        IQueryable<UserRefreshToken> UserRefreshTokens { get; }
    }
}

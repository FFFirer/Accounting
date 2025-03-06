using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting
{
    public class UserRefreshTokenStore : IUserRefreshTokenStore
    {
        protected virtual AccountingDbContext Context { get; set; }

        public UserRefreshTokenStore(AccountingDbContext context)
        {
            Context = context;
        }

        public IQueryable<UserRefreshToken> UserRefreshTokens => Context.UserRefreshTokens;
    }
}

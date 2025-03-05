using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting
{
    public class UserRefreshToken
    {
        public long Id { get; set; }
        public string UserId { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }

        public bool Revoked { get; set; } = false;
    }
}

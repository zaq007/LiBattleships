using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LiBattleship.Identity
{
    public class Claims
    {
        const string AUTH_ID_CLAIM = "nameid";


        public static Claim BuildGuestClaim(bool isGuest = true)
        {
            return new Claim(ClaimTypes.Anonymous, isGuest.ToString(), ClaimValueTypes.Boolean);
        }

        public static Claim BuildUserIdClaim()
        {
            Guid guid = Guid.NewGuid();
            return new Claim(AUTH_ID_CLAIM, guid.ToString(), ClaimValueTypes.Sid);
        }
    }
}

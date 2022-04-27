using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using SimbioMed.Authorization.Users;
using SimbioMed.MultiTenancy;

namespace SimbioMed.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
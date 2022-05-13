using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SimbioMed.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));
            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));
            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));
          
            var book = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Book, L("Book"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_Book_EditBook, L("EditBook"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_Book_MenuBook, L("MenuBook"), multiTenancySides: MultiTenancySides.Tenant);

            var sale = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Sale, L("Sale"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_Sale_MenuSale, L("MenuSale"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Customer, L("Customer"), multiTenancySides: MultiTenancySides.Tenant);
            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Customer_EditCustomer, L("EditCustomer"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_Customer_MenuCustomer, L("MenuCustomer"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Publisher, L("Publisher"), multiTenancySides: MultiTenancySides.Tenant);
            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Publisher_EditPublisher, L("EditPublisher"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_Publisher_MenuPublisher, L("MenuPublisher"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Author, L("Author"), multiTenancySides: MultiTenancySides.Tenant);
            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Author_EditAuthor, L("EditAuthor"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_Author_MenuAuthor, L("MenuAuthor"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Store, L("Store"), multiTenancySides: MultiTenancySides.Tenant);
            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Store_EditStore, L("EditStore"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_Store_MenuStore, L("MenuStore"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_City, L("City"), multiTenancySides: MultiTenancySides.Tenant);
            pages.CreateChildPermission(AppPermissions.Pages_Tenant_City_EditCity, L("EditCity"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_City_MenuCity, L("MenuCity"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Categories, L("Categories"), multiTenancySides: MultiTenancySides.Tenant);
            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Categories_EditCategories, L("EditCategories"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_Categories_MenuCategories, L("MenuCategories"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Discount, L("Discount"), multiTenancySides: MultiTenancySides.Tenant);
            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Discount_EditDiscount, L("EditDiscount"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_Discount_MenuDiscount, L("MenuDiscount"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_BookUnit, L("BookUnit"), multiTenancySides: MultiTenancySides.Tenant);
            pages.CreateChildPermission(AppPermissions.Pages_Tenant_BookUnit_EditBookUnit, L("EditBookUnit"), multiTenancySides: MultiTenancySides.Tenant);
            book.CreateChildPermission(AppPermissions.Pages_Tenant_BookUnit_MenuBookUnit, L("MenuBookUnit"), multiTenancySides: MultiTenancySides.Tenant);




            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeDefaultLanguage, L("ChangeDefaultLanguage"));
            
            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            var webhooks = administration.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription, L("Webhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Create, L("CreatingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Edit, L("EditingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_ChangeActivity, L("ChangingWebhookActivity"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Detail, L("DetailingSubscription"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ListSendAttempts, L("ListingSendAttempts"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ResendWebhook, L("ResendingWebhook"));

            var dynamicProperties = administration.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties, L("DynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Create, L("CreatingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Edit, L("EditingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Delete, L("DeletingDynamicProperties"));

            var dynamicPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue, L("DynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Create, L("CreatingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Edit, L("EditingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Delete, L("DeletingDynamicPropertyValue"));

            var dynamicEntityProperties = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties, L("DynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Create, L("CreatingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Edit, L("EditingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Delete, L("DeletingDynamicEntityProperties"));

            var dynamicEntityPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue, L("EntityDynamicPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Create, L("CreatingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Edit, L("EditingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Delete, L("DeletingDynamicEntityPropertyValue"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SimbioMedConsts.LocalizationSourceName);
        }
    }
}

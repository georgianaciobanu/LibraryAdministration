using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using IdentityServer4.Extensions;
using SimbioMed.Auditing.Dto;
using SimbioMed.Author.Dto;
using SimbioMed.Authorization.Accounts.Dto;
using SimbioMed.Authorization.Delegation;
using SimbioMed.Authorization.Permissions.Dto;
using SimbioMed.Authorization.Roles;
using SimbioMed.Authorization.Roles.Dto;
using SimbioMed.Authorization.Users;
using SimbioMed.Authorization.Users.Delegation.Dto;
using SimbioMed.Authorization.Users.Dto;
using SimbioMed.Authorization.Users.Importing.Dto;
using SimbioMed.Authorization.Users.Profile.Dto;
using SimbioMed.Book.Dto;
using SimbioMed.Book.DtoBookCategory;
using SimbioMed.BookUnit.Dto;
using SimbioMed.Category.Dto;
using SimbioMed.Chat;
using SimbioMed.Chat.Dto;
using SimbioMed.City.Dto;
using SimbioMed.Customer.Dto;
using SimbioMed.Discount.Dto;
using SimbioMed.DynamicEntityProperties.Dto;
using SimbioMed.Editions;
using SimbioMed.Editions.Dto;
using SimbioMed.Friendships;
using SimbioMed.Friendships.Cache;
using SimbioMed.Friendships.Dto;
using SimbioMed.Localization.Dto;
using SimbioMed.MultiTenancy;
using SimbioMed.MultiTenancy.Dto;
using SimbioMed.MultiTenancy.HostDashboard.Dto;
using SimbioMed.MultiTenancy.Payments;
using SimbioMed.MultiTenancy.Payments.Dto;
using SimbioMed.Notifications.Dto;
using SimbioMed.Organizations.Dto;
using SimbioMed.Publisher.Dto;
using SimbioMed.Sale.Dto;
using SimbioMed.Sale.DtoSaleDetail;
using SimbioMed.Sessions.Dto;
using SimbioMed.Store.Dto;
using SimbioMed.WebHooks.Dto;

namespace SimbioMed
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Book
            configuration.CreateMap<Book.Book, BookListDto>();
            configuration.CreateMap<CreateBookInput, Book.Book>();
            configuration.CreateMap<Book.Book, GetBookForEditOutput>();
            configuration.CreateMap<Author.Author, AuthorInBookListDto>().ReverseMap();
            configuration.CreateMap<Book.BookCategory, BookCategoryListDto>().ReverseMap();
            configuration.CreateMap<Book.BookCategory, BookCategoryListDto>();
            configuration.CreateMap<CreateBookCategoryInput, Book.BookCategory>();
            configuration.CreateMap<BookCategoryListDto,Book.BookCategory >();




            //Category
            configuration.CreateMap<Category.Category, CategoryListDto>();
            configuration.CreateMap<CreateCategoryInput, Category.Category>();
            configuration.CreateMap<Category.Category, GetCategoryForEditOutput>();
            configuration.CreateMap<Category.Category, CategoryFromBookListDto>();

            

            //City
            configuration.CreateMap<City.City, CityListDto>();
            configuration.CreateMap<CreateCityInput, City.City>();
            configuration.CreateMap<City.City, GetCityForEditOutput>();

            //Discount
            configuration.CreateMap<Discount.Discount, DiscountListDto>();
            configuration.CreateMap<CreateDiscountInput, Discount.Discount>();
            configuration.CreateMap<Discount.Discount, GetDiscountForEditOutput>();

            //Store
            configuration.CreateMap<Store.Store, StoreListDto>();
            configuration.CreateMap<CreateStoreInput, Store.Store>();
            configuration.CreateMap<Store.Store, GetStoreForEditOutput>();


            //Author
            configuration.CreateMap<Author.Author, AuthorListDto>();
            configuration.CreateMap<CreateAuthorInput, Author.Author>();
            configuration.CreateMap<Author.Author, GetAuthorForEditOutput>();

            //BookUnit
            configuration.CreateMap<BookUnit.BookUnit, BookUnitListDto>();
            configuration.CreateMap<CreateBookUnitInput, BookUnit.BookUnit>();
            configuration.CreateMap<BookUnit.BookUnit, GetBookInputForEditOutput>();

            //Sale
            configuration.CreateMap<Sale.Sale, SaleListDto>();
            configuration.CreateMap<CreateSaleInput, Sale.Sale>();
            configuration.CreateMap<Sale.Sale, GetSaleForEditOutput>();

            configuration.CreateMap<Sale.SaleDetail, SaleDetailListDto>().ReverseMap();
            configuration.CreateMap<Sale.SaleDetail, SaleDetailListDto>();
            configuration.CreateMap<CreateSaleDetailInput, Sale.SaleDetail>();
            configuration.CreateMap<SaleDetailListDto, Sale.SaleDetail>();

            //Customer
            configuration.CreateMap<Customer.Customer, CustomerListDto>();
            configuration.CreateMap<CreateCustomerInput, Customer.Customer>();
            configuration.CreateMap<Customer.Customer, GetCustomerForEditOutput>();

            //Publisher
            configuration.CreateMap<Publisher.Publisher, PublisherListDto>();
            configuration.CreateMap<CreatePublisherInput, Publisher.Publisher>();
            configuration.CreateMap<Publisher.Publisher, GetPublisherForEditOutput>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(dto => dto.DynamicPropertyName,
                    options => options.MapFrom(entity => entity.DynamicProperty.DisplayName.IsNullOrEmpty() ? entity.DynamicProperty.PropertyName : entity.DynamicProperty.DisplayName));
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();
            
            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}

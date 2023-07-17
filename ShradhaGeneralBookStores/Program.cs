using BaiThiWEBAPI.Converters;
using ShradhaGeneralBookStores.Controllers;
using ShradhaGeneralBookStores.Converters;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Imp;
using ShradhaGeneralBookStores.Service.Interface;
using ShradhaGeneralBookStores.Service.ServiceClassImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new DateTimeCoverter());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//di container
builder.Services.AddScoped<IServiceCRUD<Account>,AccoutServiceCRUD>();
builder.Services.AddScoped<IServiceCRUD<AccountRole>, AccountRoleCRUD>();
builder.Services.AddScoped<IServiceCRUD<AddressProfile>, AddressProfileCRUD>();
builder.Services.AddScoped<IServiceCRUD<Author>, AuthorCRUD>();
builder.Services.AddScoped<IServiceCRUD<Category>, CategoryCRUD>();
builder.Services.AddScoped<IServiceCRUD<Event>, EventCRUD>();
builder.Services.AddScoped<IServiceCRUD<EventDetail>, EventDetailCRUD>();
builder.Services.AddScoped<IServiceCRUD<Invoice>, InvoiceCRUD>();
builder.Services.AddScoped<IServiceCRUD<Order>, OrderCRUD>();
builder.Services.AddScoped<IServiceCRUD<OrderDetail>, OrderDetailCRUD>();
builder.Services.AddScoped<IServiceCRUD<OrderStatus>, OrderStatusCRUD>();
builder.Services.AddScoped<IServiceCRUD<PaymentMethod>, PaymentMethodCRUD>();
builder.Services.AddScoped<IServiceCRUD<Product>, ProductCRUD>();
builder.Services.AddScoped<IServiceCRUD<ProductAuthor>, ProductAuthorCRUD>();
builder.Services.AddScoped<IServiceCRUD<ProductCategory>, ProductCategoryCRUD>();
builder.Services.AddScoped<IServiceCRUD<ProductImage>, ProductImageCRUD>();
builder.Services.AddScoped<IServiceCRUD<Publisher>, PublisherCRUD>();
builder.Services.AddScoped<IServiceCRUD<Review>, ReviewCRUD>();
builder.Services.AddScoped<IServiceCRUD<Role>, RoleCRUD>();
builder.Services.AddScoped<IServiceCRUD<Voucher>, VoucherCRUD>();
builder.Services.AddScoped<IServiceCRUD<VoucherAccount>, VoucherAccountServiceCRUD>();

//service binhf thuongwf
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRoleService, AccountRoleService>();
builder.Services.AddScoped<IAccountAdmin, AccountAdmin>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();


builder.Services.AddScoped<DatabaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );
app.UseStaticFiles();

app.MapControllers();
app.Run();

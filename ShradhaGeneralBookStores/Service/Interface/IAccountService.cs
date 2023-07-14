using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Models.ModelTemp;

namespace ShradhaGeneralBookStores.Service.Interface;

public interface IAccountService
{
    public bool Login(string email, string password);
    public bool CheckExistsForEmail(string email);
    public Account GetAccountOfEmailForgetPassword(string email);
    public Account GetAccountForForgetPassword(string email,string securityCode);
    public bool ActiveAccount(string email,string security);
    public bool DisableAccount(int id);
    public dynamic Read();
    public dynamic Get(int id);
    public dynamic GetByEmail(string email);

    public dynamic UpdateProfile(Profile profile, IFormFile? avatar = null);
}

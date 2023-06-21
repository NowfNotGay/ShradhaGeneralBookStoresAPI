﻿using ShradhaGeneralBookStores.Models;

namespace ShradhaGeneralBookStores.Service.Interface;

public interface IAccountService
{
    public bool Login(string email, string password);
    public bool CheckExistsForEmail(string email);
    public Account GetAccountOfEmailForgetPassword(string email);
    public Account GetAccountForForgetPassword(string email,string securityCode);
    public bool ActiveAccount(string email,string security);
    public bool DisableAccount(int id);
}
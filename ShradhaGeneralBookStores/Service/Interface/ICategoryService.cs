﻿namespace ShradhaGeneralBookStores.Service.Interface;

public interface ICategoryService
{
    public dynamic GetAllCategoryByLevel();
    public dynamic GetAllCategoryByLevelOnlyId(int id);
}

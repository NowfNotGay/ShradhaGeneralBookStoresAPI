﻿using ShradhaGeneralBookStores.Models;

namespace ShradhaGeneralBookStores.Service.Interface;

public interface IProductService
{
    public int AddProduct(ProductAPI product);
    public dynamic Read();
}

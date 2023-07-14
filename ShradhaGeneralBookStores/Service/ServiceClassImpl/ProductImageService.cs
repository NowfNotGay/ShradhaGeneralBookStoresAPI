using Microsoft.AspNetCore.Hosting;
using ShradhaGeneralBookStores.Helpers;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;
using System;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class ProductImageService : IProductImageService
{
    private readonly IWebHostEnvironment _webHostEnvironmentl;
    private readonly DatabaseContext _databaseContext;

    public ProductImageService(IWebHostEnvironment webHostEnvironmentl, DatabaseContext databaseContext)
    {
        _webHostEnvironmentl = webHostEnvironmentl;
        _databaseContext = databaseContext;
    }

    public bool Add(int productId, IFormFile[] photo)
    {
        try
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                for (var i = 0; i < photo.Length; i++)
                {
                    var fileName = productId + i+ DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + photo[i].FileName.Substring(photo[i].FileName.LastIndexOf('.'));
                    var path = Path.Combine(_webHostEnvironmentl.WebRootPath, "Images/ProductImages", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        photo[i].CopyTo(fileStream);
                    }
                    var image = new ProductImage() 
                    {
                        ProductId = productId,
                        ImagePath = fileName,
                        CreatedAt= DateTime.Now,
                        UpdatedAt= DateTime.Now,
                    };
                    _databaseContext.ProductImages.Add(image);
                    
                }
                _databaseContext.SaveChanges();
                transaction.Commit();
                return true;
            }


        }
        catch
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                transaction.Rollback();
                return false;

            }
        }
    }

    public bool Update(int productId, IFormFile[] photo)
    {
        try
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                foreach(var image in _databaseContext.ProductImages.Where(pi => pi.ProductId == productId).ToList()) 
                {
                    var pathold = Path.Combine(_webHostEnvironmentl.WebRootPath, "Images/ProductImages",image.ImagePath);
                    if (System.IO.File.Exists(pathold))
                    {
                        System.IO.File.Delete(pathold);
                    }
                }
                _databaseContext.ProductImages.RemoveRange(_databaseContext.ProductImages.Where(pi => pi.ProductId == productId));
                _databaseContext.SaveChanges();
                for (var i = 0; i < photo.Length; i++)
                {
                    var fileName = productId + i + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + photo[i].FileName.Substring(photo[i].FileName.LastIndexOf('.'));
                    var path = Path.Combine(_webHostEnvironmentl.WebRootPath, "Images/ProductImages", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        photo[i].CopyTo(fileStream);
                    }
                    var image = new ProductImage()
                    {
                        ProductId = productId,
                        ImagePath = fileName,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };
                    _databaseContext.ProductImages.Add(image);

                }
                _databaseContext.SaveChanges();
                transaction.Commit();
                return true;
            }


        }
        catch
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                transaction.Rollback();
                return false;

            }
        }
    }
}

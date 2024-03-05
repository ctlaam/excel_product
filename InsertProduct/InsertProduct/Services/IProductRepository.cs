using System;
using InsertProduct.Models;

namespace InsertProduct.Services
{
    public interface IProductRepository
    {
        public interface IInsertProductService
        {
            bool ValidateProduct(Product product);
            void InsertProduct(Product product);
        }
        public class InsertProductService : IInsertProductService
        {
            public bool ValidateProduct(Product product)
            {
                if (string.IsNullOrWhiteSpace(product.productCode) || string.IsNullOrWhiteSpace(product.productName))
                {
                    return false;
                }
                return true;
            }

            public void InsertProduct(Product product)
            {

                if (!ValidateProduct(product))
                {
                    throw new InvalidOperationException("Dữ liệu sản phẩm không hợp lệ.");
                }
            }

        }
    }
}



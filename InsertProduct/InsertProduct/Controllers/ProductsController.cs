using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using InsertProduct.Models;
using Microsoft.AspNetCore.Mvc;
using static InsertProduct.Services.IProductRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace InsertProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IInsertProductService _insertProductService;
        public ProductsController(IInsertProductService insertProductService)
        {
            _insertProductService = insertProductService;
        }
        [HttpPost("insert")]
        public IActionResult InsertProductToExcel([FromBody] Product product)
        {
            try
            {
                var productInsert = new Product
                {
                    id = Guid.NewGuid(),
                    productCode = product.productCode,
                    productName = product.productName,
                    timeProduct = product.timeProduct,
                    description = product.description,
                };
                _insertProductService.InsertProduct(product);
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string currentDirectory = Directory.GetCurrentDirectory();
                string excelFilePath = Path.Combine(currentDirectory, "Product.xlsx");
                if (System.IO.File.Exists(excelFilePath))
                {
                    using (XLWorkbook wb = new XLWorkbook(excelFilePath))
                    {
                        IXLWorksheet ws = wb.Worksheet(1);

                        int lastRow = ws.LastRowUsed().RowNumber();

                        ws.Cell(lastRow + 1, 1).Value = productInsert.id.ToString();
                        ws.Cell(lastRow + 1, 2).Value = productInsert.productCode;
                        ws.Cell(lastRow + 1, 3).Value = productInsert.productName;
                        ws.Cell(lastRow + 1, 4).Value = productInsert.timeProduct;
                        ws.Cell(lastRow + 1, 5).Value = productInsert.description;
                        wb.Save();
                    }
                } else
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var prodData = GetProductdata();
                        wb.AddWorksheet(prodData, "Product Record");
                        wb.SaveAs(excelFilePath);
                        IXLWorksheet ws = wb.Worksheet(1);
                        int totalColumns = 5;
                        int columnWidth = 30;
                        for (int col = 1; col <= totalColumns; col++)
                        {
                            ws.Column(col).Width = columnWidth;
                        }
                        ws.Cell(2, 1).Value = productInsert.id.ToString();
                        ws.Cell(2, 2).Value = productInsert.productCode;
                        ws.Cell(2, 3).Value = productInsert.productName;
                        ws.Cell(2, 4).Value = productInsert.timeProduct;
                        ws.Cell(2, 5).Value = productInsert.description;
                        wb.Save();
                    }
                }
                return Ok(new { success = true, message = "Thêm sản phẩm thành công" });

            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                return BadRequest($"Không thể thêm sản phẩm.");
            }
        }
        private DataTable GetProductdata()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Empdata";
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("productCode", typeof(string));
            dt.Columns.Add("productName", typeof(string));
            dt.Columns.Add("timeProduct", typeof(DateTime));
            dt.Columns.Add("description", typeof(string));
            return dt;
        }
    }
}



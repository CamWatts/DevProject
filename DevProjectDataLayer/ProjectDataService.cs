using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

using DevProjectDataModels;

namespace DevProjectDataLayer
{
    public class ProjectDataService : IProject
    {
        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<ProductDTO> GetAllProducts()
        {
            List<ProductDTO> productDtoList = new List<ProductDTO>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAllProducts", con);


                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                ProductDTO productDto = new ProductDTO();
                                productDto.ProductId = rdr.GetString(0);
                                productDto.Name = rdr.GetString(1);
                                productDto.Description = rdr.GetString(2);
                                productDto.StockAmount = rdr.GetInt32(3);
                                productDto.CheckAmount = rdr.GetInt32(4);
                                productDto.SellPrice = rdr.GetFloat(5);
                                productDtoList.Add(productDto);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found");
                        }
                        rdr.Close();
                    }
                } catch (Exception ex)
                {
                    throw ex;
                }

            }
            return productDtoList;
        }

        public TransactionStatusModel AddNewProduct(ProductDTO product)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();

            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AddProduct", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@productId", product.ProductId));
                    cmd.Parameters.Add(new SqlParameter("@name", product.Name));
                    cmd.Parameters.Add(new SqlParameter("@description", product.Description));
                    cmd.Parameters.Add(new SqlParameter("@stockAmount", product.StockAmount));
                    cmd.Parameters.Add(new SqlParameter("@checkAmount", product.CheckAmount));
                    cmd.Parameters.Add(new SqlParameter("@sellPrice", product.SellPrice));

                    cmd.ExecuteNonQuery();
                } catch (Exception ex)
                {
                    transStatus.ReturnMessage.Add("Add Product failed");
                    transStatus.ReturnStatus = false;
                    return transStatus;
                }
            }

            transStatus.ReturnMessage.Add("Product successfully added");
            transStatus.ReturnStatus = true;
            return transStatus;
        }
    }
}


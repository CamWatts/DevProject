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

                    con.Open();

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
                                productDto.SellPrice = rdr.GetDouble(5);
                                productDtoList.Add(productDto);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found");
                        }
                        rdr.Close();
                    }
                }
                catch (Exception ex)
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

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
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

        public TransactionStatusModel UpdateProduct(ProductDTO product)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();

            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UpdateProduct", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@productId", product.ProductId));
                    cmd.Parameters.Add(new SqlParameter("@name", product.Name));
                    cmd.Parameters.Add(new SqlParameter("@description", product.Description));
                    cmd.Parameters.Add(new SqlParameter("@stockAmount", product.StockAmount));
                    cmd.Parameters.Add(new SqlParameter("@checkAmount", product.CheckAmount));
                    cmd.Parameters.Add(new SqlParameter("@sellPrice", product.SellPrice));

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    transStatus.ReturnMessage.Add("Update Product failed");
                    transStatus.ReturnStatus = false;
                    return transStatus;
                }
            }

            transStatus.ReturnMessage.Add("Product successfully updated");
            transStatus.ReturnStatus = true;
            return transStatus;
        }

        public TransactionStatusModel UpdateStock(ProductDTO product)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();

            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UpdateStock", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@productId", product.ProductId));
                    cmd.Parameters.Add(new SqlParameter("@stockLevel", product.StockAmount));

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    transStatus.ReturnMessage.Add("Update Stock failed");
                    transStatus.ReturnStatus = false;
                    return transStatus;
                }
            }

            transStatus.ReturnMessage.Add("Product stock updated");
            transStatus.ReturnStatus = true;
            return transStatus;
        }

        public TransactionStatusModel AddTransaction(TransactionDTO transaction)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();

            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AddTransaction", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@transactionId", transaction.TransactionId));
                    cmd.Parameters.Add(new SqlParameter("@userId", transaction.UserId));
                    cmd.Parameters.Add(new SqlParameter("@datetime", transaction.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@totalPrice", transaction.TotalPrice));

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    transStatus.ReturnMessage.Add("Add Transaction failed");
                    transStatus.ReturnStatus = false;
                    return transStatus;
                }
            }

            transStatus.ReturnMessage.Add("Transaction successfully added");
            transStatus.ReturnStatus = true;
            return transStatus;

        }

        public TransactionStatusModel AddSale(SaleDTO sale)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();

            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AddSale", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@transactionId", sale.TransactionId));
                    cmd.Parameters.Add(new SqlParameter("@productId", sale.ProductDto.ProductId));
                    cmd.Parameters.Add(new SqlParameter("@totalPrice", sale.TotalPrice));

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    transStatus.ReturnMessage.Add("Add Sale failed");
                    transStatus.ReturnStatus = false;
                    return transStatus;
                }
            }

            transStatus.ReturnMessage.Add("Sale successfully added");
            transStatus.ReturnStatus = true;
            return transStatus;
        }
    }
}


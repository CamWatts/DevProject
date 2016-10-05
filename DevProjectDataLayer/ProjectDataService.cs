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

        public TransactionStatusModel AddNewUser(UserDTO user)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();

            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AddUser", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userId", user.UserID));
                    cmd.Parameters.Add(new SqlParameter("@firstName", user.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", user.LastName));
                    cmd.Parameters.Add(new SqlParameter("@address", user.Address));
                    cmd.Parameters.Add(new SqlParameter("@mobile", user.Mobile));
                    cmd.Parameters.Add(new SqlParameter("@username", user.Username));
                    cmd.Parameters.Add(new SqlParameter("@password", user.Password));

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    transStatus.ReturnMessage.Add("Add User failed");
                    transStatus.ReturnStatus = false;
                    return transStatus;
                }
            }

            transStatus.ReturnMessage.Add("User successfully added");
            transStatus.ReturnStatus = true;
            return transStatus;
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> userDtoList = new List<UserDTO>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAllUsers", con);


                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                      
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                UserDTO userDto = new UserDTO();
                                userDto.UserID = rdr.GetString(0);
                                userDto.FirstName = rdr.GetString(1);
                                userDto.LastName = rdr.GetString(2);
                                userDto.Address = rdr.GetString(3);
                                userDto.Username = rdr.GetString(4);
                                userDto.Password = rdr.GetString(5);
                                userDtoList.Add(userDto);
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
            return userDtoList;
        }

        public List<ReportDTO> GetMonthlyReports (int months, int productCount)
        {
            List<ReportDTO> reportDtoList = new List<ReportDTO>();


                using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetMonthlyReports", con);


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@count", months));
                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            ReportDTO reportDto = new ReportDTO();
                            reportDto.Sales = new List<int>();
                            while (rdr.Read())
                            {
                                int year = rdr.GetInt32(0);
                                int month = rdr.GetInt32(1);
                                String date = month.ToString() + "-" + year.ToString();
                                double total = rdr.GetDouble(4);
                                double sellPrice = rdr.GetDouble(5);
                                int totalSold = Convert.ToInt32(total / sellPrice);

                                if (reportDto.Date == date)
                                {
                                    
                                    reportDto.Sales.Add(totalSold);
                                }
                                else
                                {
                                    if (reportDto.Date != null)
                                    {
                                        reportDtoList.Add(reportDto);
                                    }

                                    reportDto = new ReportDTO();
                                    reportDto.Sales = new List<int>();
                                    reportDto.Date = date;
                                    reportDto.Sales.Add(totalSold);
                                }
                            }
                            reportDtoList.Add(reportDto); // adds final month

                            reportDto = new ReportDTO();
                            reportDto.Sales = new List<int>();
                            reportDto.Date = "Prediction next month";
                            for (int i = 0; i < reportDtoList[0].Sales.Count; i++)
                            {
                                int totalSold = 0;
                                foreach (var item in reportDtoList)
                                {
                                    totalSold += item.Sales[i];
                                }
                                totalSold /= reportDtoList.Count;
                                reportDto.Sales.Add(totalSold);
                            }
                            reportDtoList.Add(reportDto);
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
            return reportDtoList;
        }

        public List<ReportDTO> GetWeeklyReports(int months, int productCount)
        {
            List<ReportDTO> reportDtoList = new List<ReportDTO>();


            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetWeeklyReports", con);


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@count", months));
                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            ReportDTO reportDto = new ReportDTO();
                            reportDto.Sales = new List<int>();
                            
                            while (rdr.Read())
                            {
                                DateTime datetime = rdr.GetDateTime(2);
                                String date = datetime.ToString();
                                date = date.Substring(0, 10);
                                double total = rdr.GetDouble(5);
                                double sellPrice = rdr.GetDouble(6);
                                int totalSold = Convert.ToInt32(total / sellPrice);

                                if (reportDto.Date == date)
                                {
                                    if (reportDto.Sales.Count < 8) {
                                        reportDto.Sales.Add(totalSold);
                                    }
                                    else
                                    {
                                        reportDto.Sales[reportDto.Sales.Count - 8] += totalSold;
                                    }
                                }
                                else
                                {
                                    if (reportDto.Date != null)
                                    {
                                        reportDtoList.Add(reportDto);
                                    }

                                    reportDto = new ReportDTO();
                                    reportDto.Sales = new List<int>();
                                    reportDto.Date = date;
                                    reportDto.Sales.Add(totalSold);
                                }
                            }
                            reportDtoList.Add(reportDto); // adds final month

                            reportDto = new ReportDTO();
                            reportDto.Sales = new List<int>();
                            reportDto.Date = "Prediction next week";
                            for (int i = 0; i < reportDtoList[0].Sales.Count; i++)
                            {
                                int totalSold = 0;
                                foreach (var item in reportDtoList)
                                {
                                    if (i < item.Sales.Count)
                                    {
                                        totalSold += item.Sales[i];
                                    }
                                }
                                totalSold /= reportDtoList.Count;
                                reportDto.Sales.Add(totalSold);
                            }
                            reportDtoList.Add(reportDto);
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
            return reportDtoList;
        }
    }
}


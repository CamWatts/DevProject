using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DevProjectDataLayer;
using DevProjectDataModels;

namespace DevProjectBusinessLayer
{
    public class ProjectService
    {
        private ProjectDataService projectDataService;

        IProject iProject;

        public ProjectService()
        {

        }

        public List<ProductDTO> GetAllProducts()
        {
            iProject = new ProjectDataService();
            List<ProductDTO> productDtoList = new List<ProductDTO>();

            try
            {
                productDtoList = iProject.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return productDtoList;
        }

        public TransactionStatusModel AddNewProduct(ProductDTO product)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();
            ProjectDataService dataService = new ProjectDataService();
            
            using(TransactionScope tran = new TransactionScope())
            {
                try
                {
                    transStatus = dataService.AddNewProduct(product);
                } catch (Exception ex)
                {
                    transStatus.ReturnStatus = false;
                    transStatus.ReturnMessage.Add("Add Product Failed");
                    return transStatus;
                }

                tran.Complete();
            }

            return transStatus;
        }

        public TransactionStatusModel UpdateProduct(ProductDTO product)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();
            ProjectDataService dataService = new ProjectDataService();

            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    transStatus = dataService.UpdateProduct(product);
                }
                catch (Exception ex)
                {
                    transStatus.ReturnStatus = false;
                    transStatus.ReturnMessage.Add("Update Product Failed");
                    return transStatus;
                }

                tran.Complete();
            }

            return transStatus;
        }

        public TransactionStatusModel AddTransaction(TransactionDTO transaction)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();
            ProjectDataService dataService = new ProjectDataService();

            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    transStatus = dataService.AddTransaction(transaction);
                }
                catch (Exception ex)
                {
                    transStatus.ReturnStatus = false;
                    transStatus.ReturnMessage.Add("Add Transaction Failed");
                    return transStatus;
                }

                try
                {
                    foreach (var sale in transaction.SaleDtoList)
                    {
                        transStatus = dataService.AddSale(sale);
                        transStatus = dataService.UpdateStock(sale.ProductDto); //comment out this line when using Sale Generator
                    }
                }
                catch (Exception ex)
                {
                    transStatus.ReturnStatus = false;
                    transStatus.ReturnMessage.Add("Add Sale Failed");
                    return transStatus;
                }

                tran.Complete();
            }

            return transStatus;
        }

        public List<UserDTO> GetAllUsers()
        {
            iProject = new ProjectDataService();
            List<UserDTO> userDtoList = new List<UserDTO>();

            try
            {
                userDtoList = iProject.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userDtoList;
        }

        public TransactionStatusModel AddNewUser(UserDTO user)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();
            ProjectDataService dataService = new ProjectDataService();

            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    transStatus = dataService.AddNewUser(user);
                }
                catch (Exception ex)
                {
                    transStatus.ReturnStatus = false;
                    transStatus.ReturnMessage.Add("Add user Failed");
                    return transStatus;
                }

                tran.Complete();
            }

            return transStatus;
        }

        public List<ReportDTO> GetMonthlyReports (int months, int productCount)
        {
            iProject = new ProjectDataService();
            List<ReportDTO> reportDtoList = new List<ReportDTO>();

            try
            {
                reportDtoList = iProject.GetMonthlyReports(months, productCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reportDtoList;
        }

        public List<ReportDTO> GetWeeklyReports(int months, int productCount)
        {
            iProject = new ProjectDataService();
            List<ReportDTO> reportDtoList = new List<ReportDTO>();

            try
            {
                reportDtoList = iProject.GetWeeklyReports(months, productCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reportDtoList;
        }
    }
}

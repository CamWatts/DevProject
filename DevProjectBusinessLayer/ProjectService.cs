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
    }
}

using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using DevProjectDataModels;
using DevProjectBusinessLayer;
using DevProject.Models;

namespace DevProject.Controllers.api
{
    [RoutePrefix("api/project")]
    public class ProjectController : ApiController
    {
        [Route("getallproducts")]
        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            ProductViewModel viewModel = new ProductViewModel();
            ProjectService projectService = new ProjectService();
            List<ProductDTO> productDtoList = new List<ProductDTO>();

            try
            {
                productDtoList = projectService.GetAllProducts();
                viewModel.ProductDtoList = productDtoList;
                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Get Products successful");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            var response = Request.CreateResponse<ProductViewModel>(HttpStatusCode.OK, viewModel);
            return response;
        }

        [Route("addnewproduct")]
        [HttpPost]
        public HttpResponseMessage AddNewProduct(HttpRequestMessage request, [FromBody] ProductViewModel viewModel)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();
            ProjectService projectService = new ProjectService();

            if (viewModel == null)
            {
                transStatus.ReturnStatus = false;
                transStatus.ReturnMessage.Add("Empty ProductViewModel!");
            }
            else
            {
                viewModel.ProductDto.ProductId = Guid.NewGuid().ToString();

                transStatus = projectService.AddNewProduct(viewModel.ProductDto);
            }

            if (transStatus.ReturnStatus == false)
            {
                viewModel.ReturnMessage = transStatus.ReturnMessage;
                viewModel.ReturnStatus = transStatus.ReturnStatus;
                var badresponse = Request.CreateResponse<ProductViewModel>(HttpStatusCode.BadRequest, viewModel);
                return badresponse;
            }
            else
            {
                viewModel.ReturnMessage = transStatus.ReturnMessage;
                viewModel.ReturnStatus = transStatus.ReturnStatus;
                viewModel.ReturnMessage.Add("Product successfully added");
                var response = Request.CreateResponse<ProductViewModel>(HttpStatusCode.Created, viewModel);
                return response; 
            }
        }

        [Route("updateproduct")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(HttpRequestMessage request, [FromBody] ProductViewModel viewModel)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();
            ProjectService projectService = new ProjectService();

            if (viewModel == null)
            {
                transStatus.ReturnStatus = false;
                transStatus.ReturnMessage.Add("Empty ProductViewModel!");
            }
            else
            {
                 transStatus = projectService.UpdateProduct(viewModel.ProductDto);
            }

            if (transStatus.ReturnStatus == false)
            {
                viewModel.ReturnMessage = transStatus.ReturnMessage;
                viewModel.ReturnStatus = transStatus.ReturnStatus;
                var badresponse = Request.CreateResponse<ProductViewModel>(HttpStatusCode.BadRequest, viewModel);
                return badresponse;
            }
            else
            {
                viewModel.ReturnMessage = transStatus.ReturnMessage;
                viewModel.ReturnStatus = transStatus.ReturnStatus;
                viewModel.ReturnMessage.Add("Product successfully updated");
                var response = Request.CreateResponse<ProductViewModel>(HttpStatusCode.Created, viewModel);
                return response;
            }
        }

        [Route("addtransaction")]
        [HttpPost]
        public HttpResponseMessage AddTransaction(HttpRequestMessage request, [FromBody] TransactionViewModel viewModel)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();
            ProjectService projectService = new ProjectService();

            if (viewModel == null)
            {
                transStatus.ReturnStatus = false;
                transStatus.ReturnMessage.Add("Empty TransactionViewModel!");
            }
            else
            {
               viewModel.TransactionDto.TransactionId = Guid.NewGuid().ToString();
               viewModel.TransactionDto.DateTime = DateTime.Now; // uncomment for normal usability
                //var rnd = new Random();

                //comment out these next two lines when not using Sale Generator button
                //DateTime today = DateTime.Now;
                //viewModel.TransactionDto.DateTime = today.AddDays(- rnd.Next(0, 120));

                foreach(var sale in viewModel.TransactionDto.SaleDtoList)
                {
                    sale.TransactionId = viewModel.TransactionDto.TransactionId;
                }

                transStatus = projectService.AddTransaction(viewModel.TransactionDto);
            }

            if (transStatus.ReturnStatus == false)
            {
                viewModel.ReturnMessage = transStatus.ReturnMessage;
                viewModel.ReturnStatus = transStatus.ReturnStatus;
                var badresponse = Request.CreateResponse<TransactionViewModel>(HttpStatusCode.BadRequest, viewModel);
                return badresponse;
            }
            else
            {
                viewModel.ReturnMessage = transStatus.ReturnMessage;
                viewModel.ReturnStatus = transStatus.ReturnStatus;
                viewModel.ReturnMessage.Add("Transaction successfully added");
                var response = Request.CreateResponse<TransactionViewModel>(HttpStatusCode.Created, viewModel);
                return response;
            }
        }

        [Route("addnewuser")]
        [HttpPost]
        public HttpResponseMessage AddNewUser(HttpRequestMessage request, [FromBody] UserViewModel viewModel)
        {
            TransactionStatusModel transStatus = new TransactionStatusModel();
            ProjectService projectService = new ProjectService();

            if (viewModel == null)
            {
                transStatus.ReturnStatus = false;
                transStatus.ReturnMessage.Add("Empty UserViewModel!");
            }
            else
            {
                viewModel.UserDto.UserID = Guid.NewGuid().ToString();

                transStatus = projectService.AddNewUser(viewModel.UserDto);
            }

            if (transStatus.ReturnStatus == false)
            {
                viewModel.ReturnMessage = transStatus.ReturnMessage;
                viewModel.ReturnStatus = transStatus.ReturnStatus;
                var badresponse = Request.CreateResponse<UserViewModel>(HttpStatusCode.BadRequest, viewModel);
                return badresponse;
            }
            else
            {
                viewModel.ReturnMessage = transStatus.ReturnMessage;
                viewModel.ReturnStatus = transStatus.ReturnStatus;
                viewModel.ReturnMessage.Add("User successfully added");
                var response = Request.CreateResponse<UserViewModel>(HttpStatusCode.Created, viewModel);
                return response;
            }
        }

        [Route("getallusers")]
        [HttpGet]
        public HttpResponseMessage GetAllUsers()
        {
            UserViewModel viewModel = new UserViewModel();
            ProjectService projectService = new ProjectService();
            List<UserDTO> userDtoList = new List<UserDTO>();

            try
            {
                userDtoList = projectService.GetAllUsers();
                viewModel.UserDtoList = userDtoList;
                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Get users successful");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            var response = Request.CreateResponse<UserViewModel>(HttpStatusCode.OK, viewModel);
            return response;
        }

        [Route("getmonthlyreports")]
        [HttpGet]
        public HttpResponseMessage GetMonthlyReports(int months, int productCount)
        {
            ReportViewModel viewModel = new ReportViewModel();
            ProjectService projectService = new ProjectService();
            List<ReportDTO> reportDtoList = new List<ReportDTO>();

            try
            {
                reportDtoList = projectService.GetMonthlyReports(months, productCount);
                viewModel.ReportDtoList = reportDtoList;
                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Get monthly reports successful");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            var response = Request.CreateResponse<ReportViewModel>(HttpStatusCode.OK, viewModel);
            return response;
        }

        [Route("getweeklyreports")]
        [HttpGet]
        public HttpResponseMessage GetWeeklyReports(int months, int productCount)
        {
            ReportViewModel viewModel = new ReportViewModel();
            ProjectService projectService = new ProjectService();
            List<ReportDTO> reportDtoList = new List<ReportDTO>();

            try
            {
                reportDtoList = projectService.GetWeeklyReports(months, productCount);
                viewModel.ReportDtoList = reportDtoList;
                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Get weekly reports successful");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            var response = Request.CreateResponse<ReportViewModel>(HttpStatusCode.OK, viewModel);
            return response;
        }
    }
}
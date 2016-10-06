var app = angular.module("myapp", []);

app.controller("projectController", function ($scope) {

    $scope.SoldProducts = [];
    $scope.SaleTotalPrice = 0.00;

    GetProducts($scope);
    GetUsers($scope);

    $scope.addToSale = function (product, soldProducts) {
        var isFound = false;

        console.log(product.ProductId);

        if (product.StockAmount == 0) {
            window.alert("That item has no more stock");
        }
        else {

            if (soldProducts != null) {
                soldProducts.forEach(function (soldProduct) {
                    if (product.ProductId == soldProduct.ProductId) {
                        soldProduct.StockAmount += 1;
                        soldProduct.SellPrice = product.SellPrice * soldProduct.StockAmount
                        isFound = true;
                    }
                });
            }

            if (isFound == false) {
                var sellProduct = new Object();
                sellProduct.ProductId = product.ProductId;
                sellProduct.Name = product.Name;
                sellProduct.Description = product.Description;
                sellProduct.StockAmount = 1;
                sellProduct.CheckAmount = product.CheckAmount;
                sellProduct.SellPrice = product.SellPrice;
                soldProducts.push(sellProduct);
            }

            product.StockAmount -= 1;

            $scope.SaleTotalPrice += product.SellPrice;

        }
        
    };

    $scope.removeFromSale = function (soldProduct, Products) {
        var newSellPrice = soldProduct.SellPrice / soldProduct.StockAmount;
        $scope.SaleTotalPrice -= newSellPrice;
        soldProduct.SellPrice -= newSellPrice;
        soldProduct.StockAmount -= 1

        if (soldProduct.StockAmount == 0) {
            var index = $scope.SoldProducts.indexOf(soldProduct);
            if (index > -1) {
                $scope.SoldProducts.splice(index, 1);
            }
        }

        Products.forEach(function (product) {
            if (product.ProductId == soldProduct.ProductId) {
                product.StockAmount += 1;
            }
        });
    };

    $scope.addTransaction = function (soldProducts) {

        if (soldProducts.length == 0) {
            window.alert("Please select sale products");
        }
        else
        {
            AddTransaction(soldProducts, $scope);
        }

        

    }

    $scope.addProduct = function (name, stock, check, price, desc) {
        if (angular.isUndefined(name) || name == null || angular.isUndefined(stock) || stock == null || angular.isUndefined(check) || check == null || angular.isUndefined(price) || price == null || angular.isUndefined(desc) || desc == null) {
            window.alert("Please ensure all product fields have been filled")
        }
        else if (check >= stock) {
            window.alert("Check amount must be lower than the stock amount")
        }
        else {
            AddProduct(name, stock, check, price, desc, $scope)
        }
    };

    $scope.editProduct = function (id, name, stock, check, price, desc) {
        if (angular.isUndefined(id) || id == null) {
            window.alert("Please select a product")
        }
        else if (angular.isUndefined(name) || name == null || angular.isUndefined(stock) || stock == null || angular.isUndefined(check) || check == null || angular.isUndefined(price) || price == null || angular.isUndefined(desc) || desc == null) {
            window.alert("Please ensure all product fields have been filled")
        }
        else if (check >= stock) {
            window.alert("Check amount must be lower than the stock amount")
        }
        else {
            UpdateProduct(id, name, stock, check, price, desc, $scope) 
        }
    }

    $scope.fillTextboxes = function (product) {
        $scope.editID = product.ProductId;
        $scope.editName = product.Name;
        $scope.editStock = product.StockAmount;
        $scope.editCheck = product.CheckAmount;
        $scope.editPrice = product.SellPrice;
        $scope.editDesc = product.Description;
    };

    $scope.addUser = function (firstName, lastName, address, mobile, userName, password) {
        if (angular.isUndefined(firstName) || firstName == null || angular.isUndefined(lastName) || lastName == null || angular.isUndefined(address) || address == null || angular.isUndefined(mobile) || mobile == null || angular.isUndefined(userName) || userName == null || angular.isUndefined(userName) || userName == null) {
            window.alert("Please ensure all product fields have been filled")
        }
        else {
            AddNewUser(firstName, lastName, address, mobile, userName, password, $scope)
        }
    };

    $scope.editUser = function (id, fisrtname, lastname, address, mobile, username, password) {
        if (angular.isUndefined(id) || id == null) {
            window.alert("Please select a user")
        }
        else if (angular.isUndefined(firstname) || firstname == null || angular.isUndefined(lastname) || lastname == null || angular.isUndefined(address) || address == null || angular.isUndefined(mobile) || mobile == null || angular.isUndefined(username) || username == null || angular.isUndefined(password) || password == null) {
            window.alert("Please ensure all user fields have been filled")
        }
        else {
            UpdateUser(id, firstname, lastname, address, mobile, username, password, $scope)
        }
    }

    $scope.fillTextboxes2 = function (user) {
        $scope.editID = user.UserID;
        $scope.editFirstName = user.FirstName;
        $scope.editLastName = user.LastName;
        $scope.editAddress = user.Address;
        $scope.editMobile = user.Mobile;
        $scope.editUsername = user.Username;
        $scope.editPassword = user.Password;
    };
});


function UpdateProduct(id, name, stock, check, price, desc, $scope) {
    $scope.editID = "";
    $scope.editName = "";
    $scope.editStock = "";
    $scope.editCheck = "";
    $scope.editPrice = "";
    $scope.editDesc = "";

    var viewModel = new Object();
    var ProductDto = new Object();
    ProductDto.ProductId = id;
    ProductDto.Name = name;
    ProductDto.Description = desc;
    ProductDto.StockAmount = stock;
    ProductDto.CheckAmount = check;
    ProductDto.SellPrice = price;

    viewModel.ProductDto = ProductDto;

    var jqxhr = $.post("/api/project/updateproduct", viewModel, function (response) {
    },
        "json").success(function (response) {
            window.alert(name + " updated!");
            GetProducts($scope)
            $('#editProductModal').modal('hide');
        })
        .fail(function (response) {
            window.alert("Product could not be updated");
        });
}

function AddProduct(name, stock, check, price, desc, $scope) {
    $scope.addName = "";
    $scope.addStock = "";
    $scope.addCheck = "";
    $scope.addPrice = "";
    $scope.addDesc = "";

    var viewModel = new Object();
    var ProductDto = new Object();
    ProductDto.ProductId = "";
    ProductDto.Name = name;
    ProductDto.Description = desc;
    ProductDto.StockAmount = stock;
    ProductDto.CheckAmount = check;
    ProductDto.SellPrice = price;

    viewModel.ProductDto = ProductDto;

    var jqxhr = $.post("/api/project/addnewproduct", viewModel, function (response) {
    },
        "json").success(function (response) {
            window.alert(name + " added to Database!");
            GetProducts($scope)
            $('#seeProductModal').modal('hide');
        })
        .fail(function (response) {
            window.alert("Product could not be added");
        });
};

function GetProducts($scope) {
    
    var jqxhr = $.get("/api/project/getallproducts", function (response) {
    },
        "json").success(function (response) {
            $scope.Products = response.ProductDtoList;
            $scope.$apply();
        }).fail(function (response) {

        });
};

function AddTransaction(soldProducts, $scope) {

    var viewModel = new Object();

    var TransactionDto = new Object();
    TransactionDto.TransactionId = "";
    TransactionDto.UserId = "Not Yet Implemented";
    TransactionDto.DateTime = null;
    TransactionDto.TotalPrice = $scope.SaleTotalPrice;
    TransactionDto.SaleDtoList = [];

    soldProducts.forEach(function (product) {
        var SaleDto = new Object();
        var ProductDto = new Object();
        SaleDto.TransactionId = "";
        ProductDto.ProductId = product.ProductId;
        ProductDto.Name = product.Name;
        ProductDto.Description = product.Description;
        ProductDto.StockAmount = 1;
        ProductDto.CheckAmount = product.CheckAmount;
        ProductDto.SellPrice = product.SellPrice;
        SaleDto.ProductDto = ProductDto;
        SaleDto.TotalPrice = product.SellPrice;

        TransactionDto.SaleDtoList.push(SaleDto);
    });

    viewModel.TransactionDto = TransactionDto;
    $scope.SoldProducts = [];
    $scope.SaleTotalPrice = 0.00;

    var jqxhr = $.post("/api/project/addtransaction", viewModel, function (response) {
    },
        "json").success(function (response) {
            window.alert("Transaction successful!");
            GetProducts($scope)
            $('#saleModal').modal('hide');
        })
        .fail(function (response) {
            window.alert("Transaction failed");
        });
}

function AddNewUser(firstName, lastName, address, mobile, userName, password, $scope) {
    $scope.addFirstName = "";
    $scope.addLastName = "";
    $scope.addAddress = "";
    $scope.addMobile = "";
    $scope.addUserName = "";
    $scope.addPassword = "";

    var viewModel = new Object();
    var UserDto = new Object();
    UserDto.UserID = "";
    UserDto.FirstName = firstName;
    UserDto.LastName = lastName;
    UserDto.Address = address;
    UserDto.Mobile = mobile;
    UserDto.Username = userName;
    UserDto.Password = password;

    viewModel.UserDto = UserDto;
    viewModel.UserDtoList = [];

    var jqxhr = $.post("/api/project/addnewuser", viewModel, function (response) {
    },
        "json").success(function (response) {
            window.alert(firstName + " added to Database!");
            //GetUsers($scope)
            $('#addUserModal').modal('hide');
        })
        .fail(function (response) {
            window.alert("Product could not be added");
        });
};

function GetUsers($scope) {

    var jqxhr = $.get("/api/project/getallusers", function (response) {
    },
        "json").success(function (response) {
            $scope.Users = response.UserDtoList;
            $scope.$apply();
        }).fail(function (response) {

        });
};

function UpdateUser(id, firstname, lastname, address, mobile, username, password, $scope) {
    $scope.editID = "";
    $scope.editFirtName = "";
    $scope.editLastName = "";
    $scope.editAddress = "";
    $scope.editMobile = "";
    $scope.editUserName = "";
    $scope.editPassword = "";

    var viewModel = new Object();
    var UserDto = new Object();
    UserDto.UserID = id;
    UserDto.FirstName = firstname;
    UserDto.LastName = lastname;
    UserDto.Address = address;
    UserDto.Mobile = mobile;
    UserDto.Username = username;
    UserDto.Password = password;

    viewModel.UserDto = UserDto;

    var jqxhr = $.post("/api/project/updateuser", viewModel, function (response) {
    },
        "json").success(function (response) {
            window.alert(firstname + " updated!");
            GetUsers($scope)
            $('#editUserModal').modal('hide');
        })
        .fail(function (response) {
            window.alert("User could not be updated");
        });
}
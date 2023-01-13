
# E-Commerce Application 

This is a simple Web-API for managing some functions of an e-commerce site.

## Tech Stack


**Server:** MsSQL Express

**Web API:** .NET 6




## Installation / Tools

 - [ASP.NET Web API](https://dotnet.microsoft.com/en-us/download)
 - [SQL Server 2022 Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
 - [VS 2022](https://visualstudio.microsoft.com/vs/community/)


## Install the Project



```bash
Open Visual Studio Community Edition 2022 and click on 'clone a repository'.
Then paste the project link and click clone.
```
    
## Start Project

Click the build and run button, and then Swagger Editor will pop up in the browser.


## Domain model

![App Screenshot](https://github.com/kavinduM97/eCommerceWebAPI_4/blob/demo_mor/qq.drawio%20(1).png)



## Key Features 

There are two types of users in the system. They are User and Admin

1.	Customers can do the user registration using the system
2.	Admin added through the DB (no registration needed, direct insert)
3.	Admin can create/update/delete Product Categories and Product
4.	The customer can log in to the system.
5.	Customers can search Products (search products by name/ category
6.	Customers can place Orderss


## Usage

-First Customer will register in the system.

-Customer will log in to the system.

-A JWT token will generate and store in the database when a user login to the system.

-JWT tokens are used to achieve authentication and authorization.

-Customers can view all products, search for a product by Id, name or category and place orders.

-Admin is added through the DB.

-Admin will log in to the system.

-Admin can create/update/delete Product Category and Product









## API Reference

#### Get all items

Place orders

```http
  Post /api/Order/PlaceOrder/{id}
```

Get all product
```http
  GET /api/product
```


Get product by Id

```http
  GET /api/product/{id}
```

Update product

```http
  PUT /api/Product/{id}
```

Delete product
```http
   DELETE /api/Product/{id}
```


Add product

```http
  POST /api/Product/{id}
```

Search Product

```http
   POST /api/Product/SearchProduct
```

Get all product catergories
```http
  GET /api/ProductCatergories
```


Get product catergory by Id

```http
 GET /api/ProductCatergories/{id}
```
Update product catergory

```http
 PUT /api/ProductCatergories/{id}
```

Delete product catergory
```http
 DELETE /api/ProductCatergories/{id}
```


User Registration

```http
  POST /api/User/Register
```

User login

```http
  POST /api/User/login
```
## API Reference

-Created RESTful ASP.NET Core APIs according to described requirements.
-Use Entity Framework Core as ORM 
-Used MSSQL Express to store data. Used code first approach to develop the database 
-Covered code by writing necessary unit tests (xUnit).
-Use Swagger to document and test the APIs. 
-Used dependency injection
-Used a logger for logging any error/information to a local text file (Serilog)


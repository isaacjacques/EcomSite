# E-commerce Site API and Database
In this project I designed and implemented the backend for an online shop in C# and SQL.

## API Overview
The API serves as a bridge between the online shop's front-end and database, performing CRUD operations while maintaining strict security measures.

I implemented JSON Web Tokens (JWT) as a key component of the authentication and authorization mechanism. 
JWT is an open standard for securely transmitting information between parties as a compact and self-contained token.
When a user logs into the online shop, the API generates a JWT that encapsulates the user's identity and any relevant permissions or roles. 
![TokenRequest](https://github.com/isaacjacques/EcomSite/assets/137218652/9086ece5-0ce4-4867-93c7-9a21ec986a29)

This token is then securely transmitted to the client and stored either in local storage or as an HTTP-only cookie, depending on the specific implementation requirements.
Each subsequent request from the client to the API includes the JWT in the authorization header or as a parameter. 
![AuthorizedRequest](https://github.com/isaacjacques/EcomSite/assets/137218652/5b4ca02b-1b61-485e-ace0-a72e0e4a9b3c)

In order to streamline the testing and documentation process, I incorporated Swagger UI as a tool for visualizing and testing the API.
Some adjustments to swagger's options allowed me to easily add a token to the API requests so I could test the authentication.
![TokenUpdate](https://github.com/isaacjacques/EcomSite/assets/137218652/4f62d42f-9be8-4b54-a24b-861516723d4e)

To simplify the process of data retrieval and mapping between the database and the API, I incorporated the usage of Dapper, a lightweight object-relational mapping (ORM) framework. 
Dapper facilitated the translation of relational data into object-oriented models, making it easier to handle and manipulate data within the application.



## Database Overview
* [Customers] - Stores Customer login and contact info. Passwords are stored as hashes. 
  * [CustomerCart] - Stores the contents of customers shopping cart.
    
* [Inventory] - Stores warehouse stock, each record is a 'SKU Pure' container of product
  * [InventoryHistory] - Stores each transaction that occurs to a container and can be aggregated to get remaining amount of product in a container.
    
* [Orders] - Stores high level information about a customers order 
  * [OrderDetail] - Stores each product included in an Order
    
* [Products] - Stores each unique product sold by the shop. Uses foreign keys for product attributes like color, size and brand.

![DatabaseDiagram](https://github.com/isaacjacques/EcomSite/assets/137218652/a9617f3c-b3f5-4ad2-aec0-d222b7a1fe5c)


  
## Technologies Used
* SQL Server 2022 
* ASP.NET Core
* Dapper ORM
* Swagger UI



## Authors
* **Isaac Jacques** - *Initial work* - [isaacjacques](https://isaacjacques.com)

 
## License
This project is licensed under the terms of the MIT license, see LICENSE.

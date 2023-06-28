# E-commerce Site API and Database
In this project I designed and implemented the backend for an online shop.


## Database Overview
![DatabaseDiagram](https://github.com/isaacjacques/EcomSite/assets/137218652/a9617f3c-b3f5-4ad2-aec0-d222b7a1fe5c)

* [Customers] - Stores Customer login and contact info. Passwords are stored as hashes. 
  * [CustomerCart] - Stores the contents of customers shopping cart.
    
* [Inventory] - Stores warehouse stock, each record is a 'SKU Pure' container of product
  * [InventoryHistory] - Stores each transaction that occurs to a container and can be aggregated to get remaining amount of product in a container.
    
* [Orders] - Stores high level information about a customers order 
  * [OrderDetail] - Stores each product included in an Order
    
* [Products] - Stores each unique product sold by the shop. Uses foreign keys for product attributes like color, size and brand.





## Technologies Used
* SQL Server 2022 
* ASP.NET Core
* Dapper ORM
* Swagger UI


## Authors
* **Isaac Jacques** - *Initial work* - [isaacjacques](https://isaacjacques.com)

 
## License
This project is licensed under the terms of the MIT license, see LICENSE.

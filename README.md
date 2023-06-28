# E-commerce Site API and Database
In this project I designed and implemented the backend for an online shop.


## Database Overview
For my minimum viable product, I needed a way to track Customers, Inventory and Orders.
Additionally, I wanted product in the shop to still exist when out of stock.

![Image description](https://github.com/isaacjacques/EcomSite/images/DatabaseDiagram.png)

* [Customers] - Stores Customer login and contact info. Passwords are stored as hashes. 
  * Sub [CustomerCart] - Stores the contents of customers shopping cart. 	
* [Inventory] - Stores warehouse stock, each record is a 'SKU Pure' container of product
  * Sub [InventoryHistory] - Stores each transaction that occurs to a container and can be aggregated to get remaining amount of product in a container.
* [Orders] - Stores high level information about a customers order 
  * Sub [OrderDetail] - Stores each product included in an Order
* [Products] - Stores each unique product sold by the shop. Uses foreign keys for product attributes like color, size and brand.


![Image description](https://github.com/isaacjacques/EcomSite/images/UnauthorizedRequest.png)
![Image description](https://github.com/isaacjacques/EcomSite/images/TokenRequest.png)
![Image description](https://github.com/isaacjacques/EcomSite/images/TokenResponse.png)
![Image description](https://github.com/isaacjacques/EcomSite/images/TokenUpdate.png)
![Image description](https://github.com/isaacjacques/EcomSite/images/AuthorizedRequest.png)

## Technologies Used
SQL Server 2022 - the database of choice
ASP.NET Core - framework for the API
Dapper - object–relational mapping
Swagger - API documentation and testing

## Authors
* **Isaac Jacques** - *Initial work* - [isaacjacques](https://isaacjacques.com)
 
## License
This project is licensed under the terms of the MIT license, see LICENSE.

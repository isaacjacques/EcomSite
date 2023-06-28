
INSERT INTO dbo.Colors 
(Color)
VALUES
('blue')
,('red')
,('green')
,('yellow')
,('black')
,('grey')
,('white')


INSERT INTO dbo.Sizes 
(Size)
VALUES
('xxs')
,('xs')
,('s')
,('m')
,('l')
,('xl')
,('xxl')

INSERT INTO dbo.Brands
(
	Brand				
)
VALUES
('hanes')
,('levi')
,('razer')
,('steelseries')
,('corsair')
,('adidas')
,('hp')
,('dell')
,('apple')

INSERT INTO dbo.Products
(
	SKU				
	,UPC				
	,ColorID			
	,SizeID				
	,BrandID			
	,PackSize			
	,Description		
	,CreationTime		
)
SELECT TOP(100)
	LEFT(REPLACE(CONVERT(VARCHAR(255), NEWID()),'-',''),12) SKU
	,LEFT(REPLACE(CONVERT(VARCHAR(255), NEWID()),'-',''),18) UPC
	,ColorID
	,SizeID
	,BrandID
	,ABS(CHECKSUM(NEWID()) % 6) + 1 PackSize
	,(Color + ' ' + Size + ' ' + Brand) Description
	,GETDATE()
FROM
	dbo.Colors c
	INNER JOIN dbo.Sizes s
		ON 1=1
	INNER JOIN dbo.Brands b
		ON 1=1
ORDER BY
	NEWID()

INSERT INTO dbo.Customers
(
	FirstName			
	,LastName			
	,Email				
	,PasswordSalt		
	,PasswordHash		
	,CreationTime		
)
VALUES
('isaac','jacques','test@gmail.com','vQa0yv/uD2R6AkN0Bl8nmw==','6zEVvFSxlDXNXMq8yyjHPAMFILZ5i2PYMYi+vwzWS5I=',GETDATE())

INSERT INTO dbo.CustomerCart
(
	CustomerID			
	,ProductID			
	,ProductQty			
	,CreationTime		
)
VALUES
(1,1,3,GETDATE())
,(1,2,4,GETDATE())
,(1,3,2,GETDATE())
,(1,4,1,GETDATE())
,(1,5,6,GETDATE())

INSERT INTO dbo.Orders
(
	OrderStatus		
	,CustomerID			
	,CreationTime		
)
VALUES
('RDY',1,GETDATE())
,('ACT',1,GETDATE())
,('CMP',1,GETDATE())


INSERT INTO dbo.OrderDetails
(
	OrderID			
	,ProductID			
	,ProductQty			
	,CreationTime		
)
VALUES
 (1,1,3,GETDATE())
,(2,1,3,GETDATE())
,(3,1,3,GETDATE())

INSERT INTO dbo.Inventory
(
	InventoryStatus	
	,ProductID			
	,ProductQty			
	,LPN				
	,CreationTime		
)	
SELECT
	'ACT'
	,ProductID
	,100
	,LEFT(REPLACE(CONVERT(VARCHAR(255), NEWID()),'-',''),20)
	,GETDATE()
FROM
	dbo.Products

INSERT INTO dbo.InventoryHistory
(
	InventoryID		
	,OrderID			
	,OrderQty			
	,CreationTime		
)
SELECT
	i.InventoryID
	,od.OrderID			
	,od.ProductQty			
	,GETDATE()	
FROM
	dbo.OrderDetails od
	INNER JOIN dbo.Inventory i
		ON od.ProductID = i.ProductID

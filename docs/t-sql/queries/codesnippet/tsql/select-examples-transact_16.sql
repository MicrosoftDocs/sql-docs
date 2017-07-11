USE AdventureWorks2012;
GO
SELECT AVG(OrderQty) AS [Average Quantity], 
NonDiscountSales = (OrderQty * UnitPrice)
FROM Sales.SalesOrderDetail
GROUP BY (OrderQty * UnitPrice)
ORDER BY (OrderQty * UnitPrice) DESC;
GO
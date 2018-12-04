---
title: "Format Nested JSON Output with PATH Mode (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: 032761b0-6358-42e4-b05c-dbfd663ac881
author: "jovanpop-msft"
ms.author: "jovanpop"
ms.reviewer: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Format Nested JSON Output with PATH Mode (SQL Server)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

To maintain full control over the output of the **FOR JSON** clause, specify the **PATH** option.  
  
**PATH** mode lets you create wrapper objects and nest complex properties. The results are formatted as an array of JSON objects.  
  
The alternative is to use the **AUTO** option to format the output automatically based on the structure of the **SELECT** statement.
 -   For more info about the **AUTO** option, see [Format JSON Output Automatically with AUTO Mode](../../relational-databases/json/format-json-output-automatically-with-auto-mode-sql-server.md) .
 -   For an overview of both options, see [Format Query Results as JSON with FOR JSON](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md).
 
Here are some examples of the **FOR JSON** clause with the **PATH** option. Format nested results by using dot-separated column names or by using nested queries, as shown in the following examples. By default, null values are not included in **FOR JSON** output.  

## Example - Dot-separated column names  
The following query formats the first five rows from the AdventureWorks `Person` table as JSON.  

The **FOR JSON PATH** clause uses the column alias or column name to determine the key name in the JSON output. If an alias contains dots, the PATH option creates nested objects.  

 **Query**  
  
```sql  
SELECT TOP 5   
       BusinessEntityID As Id,  
       FirstName, LastName,  
       Title As 'Info.Title',  
       MiddleName As 'Info.MiddleName'  
   FROM Person.Person  
   FOR JSON PATH   
```  
  
 **Result**  
  
```json  
[{
	"Id": 1,
	"FirstName": "Ken",
	"LastName": "Sanchez",
	"Info": {
		"MiddleName": "J"
	}
}, {
	"Id": 2,
	"FirstName": "Terri",
	"LastName": "Duffy",
	"Info": {
		"MiddleName": "Lee"
	}
}, {
	"Id": 3,
	"FirstName": "Roberto",
	"LastName": "Tamburello"
}, {
	"Id": 4,
	"FirstName": "Rob",
	"LastName": "Walters"
}, {
	"Id": 5,
	"FirstName": "Gail",
	"LastName": "Erickson",
	"Info": {
		"Title": "Ms.",
		"MiddleName": "A"
	}
}]
```  
   
## Example - Multiple tables  
If you reference more than one table in a query, **FOR JSON PATH** nests each column using its alias. The following query creates one JSON object per (OrderHeader, OrderDetails) pair joined in the query. 
  
 **Query**  
  
```sql  
SELECT TOP 2 SalesOrderNumber AS 'Order.Number',  
        OrderDate AS 'Order.Date',  
        UnitPrice AS 'Product.Price',  
        OrderQty AS 'Product.Quantity'  
FROM Sales.SalesOrderHeader H  
   INNER JOIN Sales.SalesOrderDetail D  
     ON H.SalesOrderID = D.SalesOrderID  
FOR JSON PATH   
```  
  
 **Result**  
  
```json  
[{
	"Order": {
		"Number": "SO43659",
		"Date": "2011-05-31T00:00:00"
	},
	"Product": {
		"Price": 2024.9940,
		"Quantity": 1
	}
}, {
	"Order": {
		"Number": "SO43659"
	},
	"Product": {
		"Price": 2024.9940
	}
}]
```  

## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [SQL Server 2016 and JSON Support](https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2016-and-JSON-Support)

-   [Using JSON in SQL Server 2016 and Azure SQL Database](https://channel9.msdn.com/Shows/Data-Exposed/Using-JSON-in-SQL-Server-2016-and-Azure-SQL-Database)

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven/SQLServer2016/JSON-as-a-bridge-betwen-NoSQL-and-relational-worlds)

## See Also  
 [FOR Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-for-clause-transact-sql.md)  

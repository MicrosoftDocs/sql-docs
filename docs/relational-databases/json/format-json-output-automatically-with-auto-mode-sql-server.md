---
title: "Format JSON Output Automatically with AUTO Mode (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "FOR JSON AUTO"
ms.assetid: 178a2a4e-e0f6-49b9-9895-396956d3c7d9
author: "jovanpop-msft"
ms.author: "jovanpop"
ms.reviewer: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Format JSON Output Automatically with AUTO Mode (SQL Server)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

To format the output of the **FOR JSON** clause automatically based on the structure of the **SELECT** statement, specify the **AUTO** option.  
  
When you specify the **AUTO** option, the format of the JSON output is automatically determined based on the order of columns in the SELECT list and their source tables. You can't change this format.
 
The alternative is to use the **PATH** option to maintain control over the output.
-   For more info about the **PATH** option, see [Format Nested JSON Output with PATH Mode](../../relational-databases/json/format-nested-json-output-with-path-mode-sql-server.md).
-   For an overview of both options, see [Format Query Results as JSON with FOR JSON](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md).

A query that uses the **FOR JSON AUTO** option must have a **FROM** clause.  
  
Here are some examples of the **FOR JSON** clause with the **AUTO** option.  
  
## Examples

### Example 1
 **Query**  
  
When a query references only one table, the results of the FOR JSON AUTO clause are similar to the results of FOR JSON PATH . In this case, FOR JSON AUTO doesn't create nested objects. The only difference is that FOR JSON AUTO outputs dot-separated aliases (for example, `Info.MiddleName` in the following example) as keys with dots, not as nested objects.  
  
```sql  
SELECT TOP 5   
       BusinessEntityID As Id,  
       FirstName, LastName,  
       Title As 'Info.Title',  
       MiddleName As 'Info.MiddleName'  
   FROM Person.Person  
   FOR JSON AUTO  
```  
  
 **Result**  
  
```json  
[{
	"Id": 1,
	"FirstName": "Ken",
	"LastName": "Sánchez",
	"Info.MiddleName": "J"
}, {
	"Id": 2,
	"FirstName": "Terri",
	"LastName": "Duffy",
	"Info.MiddleName": "Lee"
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
	"Info.Title": "Ms.",
	"Info.MiddleName": "A"
}]
```  

### Example 2

**Query**  
  
When you join tables, columns in the first table are generated as properties of the root object. Columns in the second table are generated as properties of a nested object. The table name or alias of the second table (for example, `D` in the following example) is used as the name of the nested array.  
  
```sql  
SELECT TOP 2 SalesOrderNumber,  
        OrderDate,  
        UnitPrice,  
        OrderQty  
FROM Sales.SalesOrderHeader H  
   INNER JOIN Sales.SalesOrderDetail D  
     ON H.SalesOrderID = D.SalesOrderID  
FOR JSON AUTO   
```  
  
**Result**  
  
```json  
[{
	"SalesOrderNumber": "SO43659",
	"OrderDate": "2011-05-31T00:00:00",
	"D": [{
		"UnitPrice": 24.99,
		"OrderQty": 1
	}]
}, {
	"SalesOrderNumber": "SO43659",
	"D": [{
		"UnitPrice": 34.40
	}, {
		"UnitPrice": 134.24,
		"OrderQty": 5
	}]
}]
```  

### Example 3
 
**Query**  
Instead of using FOR JSON AUTO, you can nest a FOR JSON PATH subquery in the SELECT statement, as shown in the following example. This example outputs the same result as the preceding example.  
  
```sql  
SELECT TOP 2  
    SalesOrderNumber,  
    OrderDate,  
    (SELECT UnitPrice, OrderQty  
      FROM Sales.SalesOrderDetail AS D  
      WHERE H.SalesOrderID = D.SalesOrderID  
     FOR JSON PATH) AS D  
FROM Sales.SalesOrderHeader AS H  
FOR JSON PATH  
```  
  
**Result**  
  
```json  
[{
	"SalesOrderNumber": "SO43659",
	"OrderDate": "2011-05-31T00:00:00",
	"D": [{
		"UnitPrice": 24.99,
		"OrderQty": 1
	}]
}, {
	"SalesOrderNumber": "SO4390",
	"D": [{
		"UnitPrice": 24.99
	}]
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

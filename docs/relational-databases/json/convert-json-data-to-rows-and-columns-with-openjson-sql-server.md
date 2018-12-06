---
title: "Parse and Transform JSON Data with OPENJSON (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "OPENJSON"
  - "JSON, importing"
  - "importing JSON"
ms.assetid: 0c139901-01e2-49ef-9d62-57e08e32c68e
author: "jovanpop-msft"
ms.author: "jovanpop"
ms.reviewer: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Parse and Transform JSON Data with OPENJSON (SQL Server)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

The **OPENJSON** rowset function converts JSON text into a set of rows and columns. After you transform a JSON collection into a rowset with **OPENJSON**, you can run any SQL query on the returned data or insert it into a SQL Server table. 
  
The **OPENJSON** function takes a single JSON object or a collection of JSON objects and transforms them into one or more rows. By default, the **OPENJSON** function returns the following data:
-   From a JSON object, the function returns all the key/value pairs that it finds at the first level.
-   From a JSON array, the function returns all the elements of the array with their indexes.  

You can add an optional **WITH** clause to provide a schema that explicitly defines the structure of the output.  
  
## Option 1 - OPENJSON with the default output
When you use the **OPENJSON** function without providing an explicit schema for the results - that is, without a **WITH** clause after **OPENJSON** - the function returns a table with the following three columns:
1.  The **name** of the property in the input object (or the index of the element in the input array).
2.  The **value** of the property or the array element.
3.  The **type** (for example, string, number, boolean, array, or object).

**OPENJSON** returns each property of the JSON object, or each element of the array, as a separate row.  

Here's a quick example that uses **OPENJSON** with the default schema - that is, without the optional **WITH** clause - and returns one row for each property of the JSON object.  
 
**Example**
```sql  
DECLARE @json NVARCHAR(MAX)

SET @json='{"name":"John","surname":"Doe","age":45,"skills":["SQL","C#","MVC"]}';

SELECT *
FROM OPENJSON(@json);
```  
  
**Results**  
  
|key|value|type|  
|---------|-----------|----------|  
|name|John|1|  
|surname|Doe|1|  
|age|45|2|  
|skills|["SQL","C#","MVC"]|4|

### More info about OPENJSON with the default schema

For more info and examples, see [Use OPENJSON with the Default Schema &#40;SQL Server&#41;](../../relational-databases/json/use-openjson-with-the-default-schema-sql-server.md).

For syntax and usage, see [OPENJSON &#40;Transact-SQL&#41;](../../t-sql/functions/openjson-transact-sql.md). 

    
## Option 2 - OPENJSON output with an explicit structure
When you specify a schema for the results by using the **WITH** clause of the **OPENJSON** function, the function returns a table with only the columns that you define in the **WITH** clause. In the optional **WITH** clause, you specify a set of output columns, their types, and the paths of the JSON source properties for each output value. **OPENJSON** iterates through the array of JSON objects, reads the value on the specified path for each column, and converts the value to the specified type.  

Here's a quick example that uses **OPENJSON** with a schema for the output that you explicitly specify in the **WITH** clause.  
  
**Example**
  
```sql  
DECLARE @json NVARCHAR(MAX)
SET @json =   
  N'[  
       {  
         "Order": {  
           "Number":"SO43659",  
           "Date":"2011-05-31T00:00:00"  
         },  
         "AccountNumber":"AW29825",  
         "Item": {  
           "Price":2024.9940,  
           "Quantity":1  
         }  
       },  
       {  
         "Order": {  
           "Number":"SO43661",  
           "Date":"2011-06-01T00:00:00"  
         },  
         "AccountNumber":"AW73565",  
         "Item": {  
           "Price":2024.9940,  
           "Quantity":3  
         }  
      }  
 ]'  
   
SELECT * FROM  
 OPENJSON ( @json )  
WITH (   
              Number   varchar(200) '$.Order.Number' ,  
              Date     datetime     '$.Order.Date',  
              Customer varchar(200) '$.AccountNumber',  
              Quantity int          '$.Item.Quantity'  
 ) 
```  
  
**Results**  
  
|Number|Date|Customer|Quantity|  
|------------|----------|--------------|--------------|  
|SO43659|2011-05-31T00:00:00|AW29825|1|  
|SO43661|2011-06-01T00:00:00|AW73565|3|  
  
This function returns and formats the elements of a JSON array.  
  
-   For each element in the JSON array, **OPENJSON** generates a new row in the output table. The two elements in the JSON array are converted into two rows in the returned table.  
  
-   For each column, specified by using the `colName type json_path` syntax, **OPENJSON** converts the value found in each array element on the specified path to the specified type. In this example, values for the `Date` column are taken from each element on the path `$.Order.Date` and converted to datetime values.  
  
### More info about OPENJSON with an explicit schema
For more info and examples, see [Use OPENJSON with an Explicit Schema &#40;SQL Server&#41;](../../relational-databases/json/use-openjson-with-an-explicit-schema-sql-server.md).

For syntax and usage, see [OPENJSON &#40;Transact-SQL&#41;](../../t-sql/functions/openjson-transact-sql.md).

## OPENJSON requires Compatibility Level 130
The **OPENJSON** function is available only under **compatibility level 130**. If your database compatibility level is lower than 130, SQL Server can't find and run the **OPENJSON** function. Other built-in JSON functions are available at all compatibility levels.

You can check compatibility level in the `sys.databases` view or in database properties.

You can change the compatibility level of a database by using the following command:   
`ALTER DATABASE <DatabaseName> SET COMPATIBILITY_LEVEL = 130`  

## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [SQL Server 2016 and JSON Support](https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2016-and-JSON-Support)

-   [Using JSON in SQL Server 2016 and Azure SQL Database](https://channel9.msdn.com/Shows/Data-Exposed/Using-JSON-in-SQL-Server-2016-and-Azure-SQL-Database)

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven/SQLServer2016/JSON-as-a-bridge-betwen-NoSQL-and-relational-worlds)
  
## See Also  
 [OPENJSON &#40;Transact-SQL&#41;](../../t-sql/functions/openjson-transact-sql.md)  
  
  

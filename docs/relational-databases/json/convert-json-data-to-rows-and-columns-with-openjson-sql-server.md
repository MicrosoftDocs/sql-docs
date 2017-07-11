---
title: "Convert JSON Data to Rows and Columns with OPENJSON (SQL Server) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "01/31/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-json"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "OPENJSON"
  - "JSON, importing"
  - "importing JSON"
ms.assetid: 0c139901-01e2-49ef-9d62-57e08e32c68e
caps.latest.revision: 31
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Convert JSON Data to Rows and Columns with OPENJSON (SQL Server)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

The **OPENJSON** rowset function converts JSON text into a set of rows and columns. Use **OPENJSON** to run SQL queries on JSON collections or to import JSON text into SQL Server tables.  
  
 The **OPENJSON** function takes a single JSON object or a collection of JSON objects and transforms them into one or more rows. By default, **OPENJSON** function returns the following.
-   From a JSON object, all the key:value pairs that it finds at the first level.
-   From a JSON array, all the elements of the array with their indexes.  
  
Optionally add a **WITH** clause to specify the schema of the rows that the **OPENJSON** function returns. This explicit schema defines the structure of the output.  
  
## Use OPENJSON without an explicit schema for the output
When you use the **OPENJSON** function without providing an explicit schema for the results - that is, without a **WITH** clause after OPENJSON - the function returns a table with the following three columns.
1.  The name of the property in the input object (or the index of the element in the input array).
2.  The value of the property or the array element.
3.  The type (for example, string, number, boolean, array or object).

Each property of the JSON object, or each element of the array, is returned as a separate row.  

Here's a quick example that uses **OPENJSON** with the default schema and returns one row for each property of the JSON object.  
 
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

### More info

For more info and examples, see [Use OPENJSON with the Default Schema &#40;SQL Server&#41;](../../relational-databases/json/use-openjson-with-the-default-schema-sql-server.md).

For syntax and usage, see [OPENJSON &#40;Transact-SQL&#41;](../../t-sql/functions/openjson-transact-sql.md). 

    
## Use OPENJSON with an explicit schema for the output
When you specify a schema for the results by using the **WITH** clause of the **OPENJSON** function, the function returns a table with only the columns that you define in the **WITH** clause. In the **WITH** clause, you specify a set output columns, their types, and the paths of the JSON source properties for each output value. **OPENJSON** iterates through the array of JSON objects, reads the value on the specified path for each column, and converts the value to the specified type.  

Here's a quick example that uses **OPENJSON** with a schema for the results that you explicitly specify.  
  
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
  
-   For each column, specified by using the `colName type json_path` syntax, **OPENJSON** function converts the value found in each array element on the specified path to specified type and populates a cell in the output table. In this example, values for the `Date` column are taken from each object on the path `$.Order.Date` and converted to datetime values.  
  
After you transform a JSON collection into a rowset with **OPENJSON**, you can run any SQL query on the returned data or insert it into a table.  

### More info
For more info and examples, see [Use OPENJSON with an Explicit Schema &#40;SQL Server&#41;](../../relational-databases/json/use-openjson-with-an-explicit-schema-sql-server.md).

For syntax and usage, see [OPENJSON &#40;Transact-SQL&#41;](../../t-sql/functions/openjson-transact-sql.md).

## OPENJSON requires Compatibility Level 130
The **OPENJSON** function is available only under **compatibility level 130**. If your database compatibility level is lower than 130, SQL Server can't find and run **OPENJSON** function. Other built-in JSON functions are available at all compatibility levels. You can check compatibility level in sys.databases view or in database properties.

You can change a compatibility level of database using the following command:   
`ALTER DATABASE <DatabaseName> SET COMPATIBILITY_LEVEL = 130`  

## Learn more about the built-in JSON support in SQL Server  
For lots of specific solutions, use cases, and recommendations, see the [blog posts about the built-in JSON support](http://blogs.msdn.com/b/sqlserverstorageengine/archive/tags/json/) in SQL Server and in Azure SQL Database by Microsoft Program Manager Jovan Popovic.
  
## See Also  
 [OPENJSON &#40;Transact-SQL&#41;](../../t-sql/functions/openjson-transact-sql.md)  
  
  

---
title: "JSON Path Expressions (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "01/23/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "JSON, path expressions"
  - "path expressions (JSON)"
ms.assetid: 25ea679c-84cc-4977-867c-2cbe9d192553
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# JSON Path Expressions (SQL Server)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

 Use JSON path expressions to reference the properties of JSON objects.  
  
 You have to provide a path expression when you call the following functions.  
  
-   When you call **OPENJSON** to create a relational view of JSON data. For more info, see [OPENJSON &#40;Transact-SQL&#41;](../../t-sql/functions/openjson-transact-sql.md).  
  
-   When you call **JSON_VALUE** to extract a value from JSON text. For more info, see [JSON_VALUE &#40;Transact-SQL&#41;](../../t-sql/functions/json-value-transact-sql.md).  
  
-   When you call **JSON_QUERY** to extract a JSON object or an array. For more info, see [JSON_QUERY &#40;Transact-SQL&#41;](../../t-sql/functions/json-query-transact-sql.md).  
  
-   When you call **JSON_MODIFY** to update the value of a property in a JSON string. For more info, see [JSON_MODIFY &#40;Transact-SQL&#41;](../../t-sql/functions/json-modify-transact-sql.md).  

## Parts of a path expression
 A path expression has two components.  
  
1.  The optional [path mode](#PATHMODE), with a value of **lax** or **strict**.  
  
2.  The [path](#PATH) itself.  

##  <a name="PATHMODE"></a> Path mode  
 At the beginning of the path expression, optionally declare the path mode by specifying the keyword **lax** or **strict**. The default is **lax**.  
  
-   In **lax** mode, the function returns empty values if the path expression contains an error. For example, if you request the value **$.name**, and the JSON text doesn't contain a **name** key, the function returns null, but does not raise an error.  
  
-   In **strict** mode, the function raises an error if the path expression contains an error.  

The following query explicitly specifies `lax` mode in the path expression.

```sql  
DECLARE @json NVARCHAR(MAX)
SET @json=N'{ ... }'

SELECT * FROM OPENJSON(@json, N'lax $.info')
```  
  
##  <a name="PATH"></a> Path  
 After the optional path mode declaration, specify the path itself.  
  
-   The dollar sign (`$`) represents the context item.  
  
-   The property path is a set of path steps. Path steps can contain the following elements and operators.  
  
    -   Key names. For example, `$.name` and `$."first name"`. If the key name starts with a dollar sign or contains special characters such as spaces, surround it with quotes.   
  
    -   Array elements. For example, `$.product[3]`. Arrays are zero-based.  
  
    -   The dot operator (`.`) indicates a member of an object. For example, in `$.people[1].surname`, `surname` is a child of `people`.
  
## Examples  
 The examples in this section reference the following JSON text.  
  
```json  
{
	"people": [{
		"name": "John",
		"surname": "Doe"
	}, {
		"name": "Jane",
		"surname": null,
		"active": true
	}]
}
```  
  
 The following table shows some examples of path expressions.  
  
|Path expression|Value|  
|---------------------|-----------|  
|$.people[0].name|John|  
|$.people[1]|{ "name": "Jane",  "surname": null, "active": true }|  
|$.people[1].surname|null|  
|$|{ "people": [ { "name": "John",  "surname": "Doe" },<br />   { "name": "Jane",  "surname": null, "active": true } ] }|  
  
## How built-in functions handle duplicate paths  
 If the JSON text contains duplicate properties - for example, two keys with the same name on the same level - the **JSON_VALUE** and **JSON_QUERY** functions return only the first value that matches the path. To parse a JSON object that contains duplicate keys and return all values, use **OPENJSON**, as shown in the following example.  
  
```sql  
DECLARE @json NVARCHAR(MAX)
SET @json=N'{"person":{"info":{"name":"John", "name":"Jack"}}}'

SELECT value
FROM OPENJSON(@json,'$.person.info') 
```  

## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [SQL Server 2016 and JSON Support](https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2016-and-JSON-Support)

-   [Using JSON in SQL Server 2016 and Azure SQL Database](https://channel9.msdn.com/Shows/Data-Exposed/Using-JSON-in-SQL-Server-2016-and-Azure-SQL-Database)

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven/SQLServer2016/JSON-as-a-bridge-betwen-NoSQL-and-relational-worlds)
  
## See Also  
 [OPENJSON &#40;Transact-SQL&#41;](../../t-sql/functions/openjson-transact-sql.md)   
 [JSON_VALUE &#40;Transact-SQL&#41;](../../t-sql/functions/json-value-transact-sql.md)   
 [JSON_QUERY &#40;Transact-SQL&#41;](../../t-sql/functions/json-query-transact-sql.md)  
  
  

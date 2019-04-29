---
title: "JSON_MODIFY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/02/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
ms.assetid: 96bc8255-a037-4907-aec4-1a9c30814651
author: "jovanpop-msft"
ms.author: "jovanpop"
ms.reviewer: genemi
manager: craigg
---
# JSON_MODIFY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Updates the value of a property in a JSON string and returns the updated JSON string.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
JSON_MODIFY ( expression , path , newValue )  
```  
  
## Arguments  
 *expression*  
 An expression. Typically the name of a variable or a column that contains JSON text.  
  
 **JSON_MODIFY** returns an error if *expression* doesn't contain valid JSON.  
  
 *path*  
 A JSON path expression that specifies the property to update.

 *path* has the following syntax:  
  
 `[append] [ lax | strict ] $.<json path>`  
  
-   *append*  
    Optional modifier that specifies that the new value should be appended to the array referenced by *\<json path>*.  
  
-   *lax*  
    Specifies that the property referenced by *\<json path>* does not have to exist. If the property is not present, JSON_MODIFY tries to insert the new value on the specified path. Insertion may fail if the property can't be inserted on the path. If you don't specify *lax* or *strict*,  *lax* is the default mode.  
  
-   *strict*  
    Specifies that the property referenced by *\<json path>* must be in the JSON expression. If the property is not present, JSON_MODIFY returns an error.  
  
-   *\<json path>*  
    Specifies the path for the property to update. For more info, see [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md).  
  
In [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] and in [!INCLUDE[ssSDSfull_md](../../includes/sssdsfull-md.md)], you can provide a variable as the value of *path*.

**JSON_MODIFY** returns an error if the format of *path* isn't valid.  
  
 *newValue*  
 The new value for the property specified by *path*.  
  
 In lax mode, JSON_MODIFY deletes the specified key if the new value is NULL.  
  
JSON_MODIFY escapes all special characters in the new value if the type of the value is NVARCHAR or VARCHAR. A text value is not escaped if it is properly formatted JSON produced by FOR JSON, JSON_QUERY, or JSON_MODIFY.  
  
## Return Value  
 Returns the updated value of *expression* as properly formatted JSON text.  
  
## Remarks  
 The JSON_MODIFY function lets you  either update the value of an existing property, insert a new key:value pair, or delete a key based on a combination of modes and provided values.  
  
 The following table compares the behavior of **JSON_MODIFY** in lax mode and in strict mode. For more info about the optional path mode specification (lax or strict), see [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md).  
  
|Existing value|Path exists|Lax mode|Strict mode|  
|--------------------|-----------------|--------------|-----------------|  
|Not NULL|Yes|Update the existing value.|Update the existing value.|  
|Not NULL|No|Try to create a new key:value pair on the specified path.<br /><br /> This may fail. For example, if you specify the path `$.user.setting.theme`, JSON_MODIFY does not insert the key `theme` if the `$.user` or `$.user.settings` objects do not exist, or if settings is an array or a scalar value.|Error - INVALID_PROPERTY|  
|NULL|Yes|Delete the existing property.|Set the existing value to null.|  
|NULL|No|No action. The first argument is returned as the result.|Error - INVALID_PROPERTY|  
  
 In lax mode, JSON_MODIFY tries to create a new key:value pair, but in some cases it might fail.  
  
## Examples  
  
### Example - Basic operations  
 The following example shows basic operations that can be done with JSON text.  
  
 **Query**  
  
```sql  

DECLARE @info NVARCHAR(100)='{"name":"John","skills":["C#","SQL"]}'

PRINT @info

-- Update name  

SET @info=JSON_MODIFY(@info,'$.name','Mike')

PRINT @info

-- Insert surname  

SET @info=JSON_MODIFY(@info,'$.surname','Smith')

PRINT @info

-- Delete name  

SET @info=JSON_MODIFY(@info,'$.name',NULL)

PRINT @info

-- Add skill  

SET @info=JSON_MODIFY(@info,'append $.skills','Azure')

PRINT @info
```  
  
 **Results**  
  
```json  
{
	"name": "John",
	"skills": ["C#", "SQL"]
} {
	"name": "Mike",
	"skills": ["C#", "SQL"]
} {
	"name": "Mike",
	"skills": ["C#", "SQL"],
	"surname": "Smith"
} {
	"skills": ["C#", "SQL"],
	"surname": "Smith"
} {
	"skills": ["C#", "SQL", "Azure"],
	"surname": "Smith"
}
```  
  
### Example - Multiple updates  
 With JSON_MODIFY you can update only one property. If you have to do multiple updates, you can use multiple JSON_MODIFY calls.  
  
 **Query**  
  
```sql  
DECLARE @info NVARCHAR(100)='{"name":"John","skills":["C#","SQL"]}'

PRINT @info

-- Multiple updates  

SET @info=JSON_MODIFY(JSON_MODIFY(JSON_MODIFY(@info,'$.name','Mike'),'$.surname','Smith'),'append $.skills','Azure')

PRINT @info
```  
  
 **Results**  
  
```json  
{
	"name": "John",
	"skills": ["C#", "SQL"]
} {
	"name": "Mike",
	"skills": ["C#", "SQL", "Azure"],
	"surname": "Smith"
}
```  
  
### Example - Rename a key  
 The following example shows how to rename a property in JSON text with the JSON_MODIFY function. First you can take the value of an existing property and insert it as a new key:value pair. Then you can delete the old key by setting the value of the old property to NULL.  
  
 **Query**  
  
```sql  
DECLARE @product NVARCHAR(100)='{"price":49.99}'

PRINT @product

-- Rename property  

SET @product=
 JSON_MODIFY(
  JSON_MODIFY(@product,'$.Price',CAST(JSON_VALUE(@product,'$.price') AS NUMERIC(4,2))),
  '$.price',
  NULL
 )

PRINT @product
```  
  
 **Results**  
  
```json  
{
	"price": 49.99
} {
	"Price": 49.99
}
```  
  
 If you don't cast the new value to a numeric type, JSON_MODIFY treats it as text and surrounds it with double quotes.  
  
### Example - Increment a value  
 The following example shows how to increment the value of a property in JSON text with the JSON_MODIFY function. First you can take the value of the existing property and insert it as a new key:value pair. Then you can delete the old key by setting the value of the old property to NULL.  
  
 **Query**  
  
```sql  
DECLARE @stats NVARCHAR(100)='{"click_count": 173}'

PRINT @stats

-- Increment value  

SET @stats=JSON_MODIFY(@stats,'$.click_count',
 CAST(JSON_VALUE(@stats,'$.click_count') AS INT)+1)

PRINT @stats
```  
  
 **Results**  
  
```json  
{
	"click_count": 173
} {
	"click_count": 174
}
```  
  
### Example - Modify a JSON object  
 JSON_MODIFY treats the *newValue* argument as plain text even if it contains properly formatted JSON text. As a result, the JSON output of the function is surrounded with double quotes and all special characters are escaped, as shown in the following example.  
  
 **Query**  
  
```sql  
DECLARE @info NVARCHAR(100)='{"name":"John","skills":["C#","SQL"]}'

PRINT @info

-- Update skills array

SET @info=JSON_MODIFY(@info,'$.skills','["C#","T-SQL","Azure"]')

PRINT @info
```  
  
 **Results**  
  
```json  
{
	"name": "John",
	"skills": ["C#", "SQL"]
} {
	"name": "John",
	"skills": "["C#","T-SQL","Azure"]"
}
```  
  
 To avoid automatic escaping, provide *newValue* by using the JSON_QUERY function. JSON_MODIFY knows that the value returned by JSON_MODIFY is properly formatted JSON, so it doesn't escape the value.  
  
 **Query**  
  
```sql  
DECLARE @info NVARCHAR(100)='{"name":"John","skills":["C#","SQL"]}'

PRINT @info

-- Update skills array  

SET @info=JSON_MODIFY(@info,'$.skills',JSON_QUERY('["C#","T-SQL","Azure"]'))

PRINT @info
```  
  
 **Results**  
  
```json  
{
	"name": "John",
	"skills": ["C#", "SQL"]
} {
	"name": "John",
	"skills": ["C#", "T-SQL", "Azure"]
}
```  
  
### Example - Update a JSON column  
 The following example updates the value of a property in a table column that contains JSON.  
  
```sql  
UPDATE Employee
SET jsonCol=JSON_MODIFY(jsonCol,"$.info.address.town",'London')
WHERE EmployeeID=17
 
```  
  
## See Also  
 [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md)   
 [JSON Data &#40;SQL Server&#41;](../../relational-databases/json/json-data-sql-server.md)  
  
  

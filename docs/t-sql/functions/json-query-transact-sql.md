---
title: "JSON_QUERY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/02/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "JSON_QUERY"
  - "JSON_QUERY_TSQL"
helpviewer_keywords: 
  - "JSON, extracting"
  - "JSON, querying"
  - "JSON_QUERY function"
ms.assetid: 1ab0d90f-19b6-4988-ab4f-22fdf28b7c79
author: "jovanpop-msft"
ms.author: "jovanpop"
ms.reviewer: genemi
manager: craigg
---
# JSON_QUERY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

 Extracts an object or an array from a JSON string.  
  
 To extract a scalar value from a JSON string instead of an object or an array, see [JSON_VALUE &#40;Transact-SQL&#41;](../../t-sql/functions/json-value-transact-sql.md). For info about the differences between **JSON_VALUE** and **JSON_QUERY**, see [Compare JSON_VALUE and JSON_QUERY](../../relational-databases/json/validate-query-and-change-json-data-with-built-in-functions-sql-server.md#JSONCompare).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
JSON_QUERY ( expression [ , path ] )  
```  
  
## Arguments  
 *expression*  
 An expression. Typically the name of a variable or a column that contains JSON text.  
  
 If **JSON_QUERY** finds JSON that is not valid in *expression* before it finds the value identified by *path*, the function returns an error. If **JSON_QUERY** doesn't find the value identified by *path*, it scans the entire text and returns an error if it finds JSON that is not valid anywhere in *expression*.  
  
 *path*  
 A JSON path that specifies the object or the array to extract.

In [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] and in [!INCLUDE[ssSDSfull_md](../../includes/sssdsfull-md.md)], you can provide a variable as the value of *path*.

The JSON path can specify lax or strict mode for parsing. If you don't specify the parsing mode, lax mode is the default. For more info, see [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md).  

The default value for *path* is '$'. As a result, if you don't provide a value for *path*, **JSON_QUERY** returns the input *expression*.

If the format of *path* isn't valid, **JSON_QUERY** returns an error.  
  
## Return Value  
 Returns a JSON fragment of type nvarchar(max). The collation of the returned value is the same as the collation of the input expression.  
  
 If the value is not an object or an array:  
  
-   In lax mode, **JSON_QUERY** returns null.  
  
-   In strict mode, **JSON_QUERY** returns an error.  
  
## Remarks  

### Lax mode and strict mode

 Consider the following JSON text:  
  
```json  
{
	"info": {
		"type": 1,
		"address": {
			"town": "Bristol",
			"county": "Avon",
			"country": "England"
		},
		"tags": ["Sport", "Water polo"]
	},
	"type": "Basic"
} 
```  
  
 The following table compares the behavior of **JSON_QUERY** in lax mode and in strict mode. For more info about the optional path mode specification (lax or strict), see [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md).  
  
|Path|Return value in lax mode|Return value in strict mode|More info|  
|----------|------------------------------|---------------------------------|---------------|  
|$|Returns the entire JSON text.|Returns the entire JSON text.|N/a|  
|$.info.type|NULL|Error|Not an object or array.<br /><br /> Use **JSON_VALUE** instead.|  
|$.info.address.town|NULL|Error|Not an object or array.<br /><br /> Use **JSON_VALUE** instead.|  
|$.info."address"|N'{ "town":"Bristol", "county":"Avon", "country":"England" }'|N'{ "town":"Bristol", "county":"Avon", "country":"England" }'|N/a|  
|$.info.tags|N'[ "Sport", "Water polo"]'|N'[ "Sport", "Water polo"]'|N/a|  
|$.info.type[0]|NULL|Error|Not an array.|  
|$.info.none|NULL|Error|Property does not exist.|  

### Using JSON_QUERY with FOR JSON

**JSON_QUERY** returns a valid JSON fragment. As a result, **FOR JSON** doesn't escape special characters in the **JSON_QUERY** return value.

If you're returning results with FOR JSON, and you're including data that's already in JSON format (in a column or as the result of an expression), wrap the JSON data with **JSON_QUERY** without the *path* parameter.

## Examples  
  
### Example 1  
 The following example shows how to return a JSON fragment from a `CustomFields` column in query results.  
  
```sql  
SELECT PersonID,FullName,
 JSON_QUERY(CustomFields,'$.OtherLanguages') AS Languages
FROM Application.People
```  
  
### Example 2  
The following example shows how to include JSON fragments in the output of the FOR JSON clause.  
  
```sql  
SELECT StockItemID, StockItemName,
         JSON_QUERY(Tags) as Tags,
         JSON_QUERY(CONCAT('["',ValidFrom,'","',ValidTo,'"]')) ValidityPeriod
FROM Warehouse.StockItems
FOR JSON PATH
```  
  
## See Also  
 [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md)   
 [JSON Data &#40;SQL Server&#41;](../../relational-databases/json/json-data-sql-server.md)  

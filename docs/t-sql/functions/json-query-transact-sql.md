---
title: "JSON_QUERY (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/02/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-json"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "JSON_QUERY"
  - "JSON_QUERY_TSQL"
helpviewer_keywords: 
  - "JSON, extracting"
  - "JSON, querying"
  - "JSON_QUERY function"
ms.assetid: 1ab0d90f-19b6-4988-ab4f-22fdf28b7c79
caps.latest.revision: 19
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# JSON_QUERY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Extracts an object or an array from a JSON string.  
  
 To extract a scalar value from a JSON string, see [JSON_VALUE &#40;Transact-SQL&#41;](../../t-sql/functions/json-value-transact-sql.md). For info about the differences between **JSON_VALUE** and **JSON_QUERY**, see [Compare JSON_VALUE and JSON_QUERY](../../relational-databases/json/validate-query-and-change-json-data-with-built-in-functions-sql-server.md#JSONCompare).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```tsql  
JSON_QUERY ( expression [ , path ] )  
```  
  
## Arguments  
 *expression*  
 An expression. Typically the name of a variable or a column that contains JSON text.  
  
 **JSON_QUERY** returns an error if it finds JSON that is not valid in *expression* before it finds the value identified by *path*. If **JSON_QUERY** doesn't find the value identified by *path*, it scans the entire text and returns an error if it finds JSON that is not valid anywhere in *expression*.  
  
 *path*  
 A JSON path that specifies the object or the array to extract. If it is not specified, JSON from the input will be returned (default value for *path* is '$'). The JSON path can specify lax or strict mode for parsing. Lax mode is the default and is assumed if the parsing mode is not specified. For more info, see [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md).  

In [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] and in [!INCLUDE[ssSDSfull_md](../../includes/sssdsfull-md.md)], you can provide a variable as the value of *path*.
  
 **JSON_QUERY** returns an error if the format of *path* isn't valid.  
  
## Return Value  
 Returns a JSON fragment of type nvarchar(max). The collation of the returned value is the same as the collation of the input expression.  
  
 If the value is not an object or an array:  
  
-   In lax mode, **JSON_QUERY** returns null.  
  
-   In strict mode, **JSON_QUERY** returns an error.  
  
## Remarks  
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
  
## Examples  
  
### Example 1  
 The following example shows how to return a JSON fragment from CustomFields column in query results.  
  
```tsql  
SELECT PersonID,FullName,
 JSON_QUERY(CustomFields,'$.OtherLanguages') AS Languages
FROM Application.People
```  
  
### Example 2  
 The following example shows how to include JSON fragments in the output of the FOR JSON clause.  
  
```tsql  
SELECT StockItemID, StockItemName,
         JSON_QUERY(Tags) as Tags,
         JSON_QUERY(CONCAT('["',ValidFrom,'","',ValidTo,'"]')) ValidityPeriod
FROM Warehouse.StockItems
FOR JSON PATH
```  
> [!NOTE] 
> JSON_QUERY returns a valid JSON fragment so FOR JSON will not treat result of JSON_QUERY as a plain text and it will **not escape characters** returned by JSON_QUERY using [JSON escaping rules](../../relational-databases/json/how-for-json-escapes-special-characters-and-control-characters-sql-server.md). If you have JSON stored in column (Tags column in this example) or if you need to dynamically create JSON using some expression, you should *wrap it with JSON_QUERY without path parameter* if you are returning results using FOR JSON clause.
  
## See Also  
 [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md)   
 [JSON Data &#40;SQL Server&#41;](../../relational-databases/json/json-data-sql-server.md)  
  
  

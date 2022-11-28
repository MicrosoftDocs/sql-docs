---
description: "Solve common issues with JSON in SQL Server"
title: "Solve common issues with JSON in SQL Server"
ms.date: 06/03/2020
ms.service: sql
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "JSON, FAQ"
ms.assetid: feae120b-55cc-4601-a811-278ef1c551f9
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: jroth
ms.custom: seo-dt-2019
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Solve common issues with JSON in SQL Server
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sqlserver2016-asdb.md)]

 Find answers here to some common questions about the built-in JSON support in SQL Server.  
 
## FOR JSON and JSON output

### FOR JSON PATH or FOR JSON AUTO?  
 **Question.** I want to create a JSON text result from a simple SQL query on a single table. FOR JSON PATH and FOR JSON AUTO produce the same output. Which of these two options should I use?  
  
 **Answer.** Use FOR JSON PATH. Although there is no difference in the JSON output, AUTO mode applies some additional logic that checks whether columns should be nested. Consider PATH the default option.  

### Create a nested JSON structure  
 **Question.** I want to produce complex JSON with several arrays on the same level. FOR JSON PATH can create nested objects using paths, and FOR JSON AUTO creates additional nesting level for each table. Neither one of these two options lets me generate the output I want. How can I create a custom JSON format that the existing options don't directly support?  
  
 **Answer.** You can create any data structure by adding FOR JSON queries as column expressions that return JSON text. You can also create JSON manually by using the JSON_QUERY function. The following example demonstrates these techniques.  
  
```sql  
SELECT col1, col2, col3,  
     (SELECT col11, col12, col13 FROM t11 WHERE t11.FK = t1.PK FOR JSON PATH) as t11,  
     (SELECT col21, col22, col23 FROM t21 WHERE t21.FK = t1.PK FOR JSON PATH) as t21,  
     (SELECT col31, col32, col33 FROM t31 WHERE t31.FK = t1.PK FOR JSON PATH) as t31,  
     JSON_QUERY('{"'+col4+'":"'+col5+'"}') as t41  
FROM t1  
FOR JSON PATH  
```  
  
Every result of a FOR JSON query or the JSON_QUERY function in the column expressions is formatted as a separate nested JSON sub-object and included in the main result.  

### Prevent double-escaped JSON in FOR JSON output  
 **Question.** I have JSON text stored in a table column. I want to include it in the output of FOR JSON. But FOR JSON escapes all characters in the JSON, so I'm getting a JSON string instead of a nested object, as shown in the following example.  
  
```sql  
SELECT 'Text' AS myText, '{"day":23}' AS myJson  
FOR JSON PATH  
```  
  
 This query produces the following output.  
  
```json  
[{"myText":"Text", "myJson":"{\"day\":23}"}]  
```  
  
 How can I prevent this behavior? I want `{"day":23}` to be returned as a JSON object and not as escaped text.  
  
 **Answer.** JSON stored in a text column or a literal is treated like any text. That is, it's surrounded with double quotes and escaped. If you want to return an unescaped JSON object, pass the JSON column as an argument to the JSON_QUERY function, as shown in the following example.  
  
```sql  
SELECT col1, col2, col3, JSON_QUERY(jsoncol1) AS jsoncol1  
FROM tab1  
FOR JSON PATH  
```  
  
 JSON_QUERY without its optional second parameter returns only the first argument as a result. Since JSON_QUERY always returns valid JSON, FOR JSON knows that this result does not have to be escaped.

### JSON generated with the WITHOUT_ARRAY_WRAPPER clause is escaped in FOR JSON output  
 **Question.** I'm trying to format a column expression by using FOR JSON and the WITHOUT_ARRAY_WRAPPER option.  
  
```sql  
SELECT 'Text' as myText,  
   (SELECT 12 day, 8 mon FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) as myJson  
FOR JSON PATH   
```  
  
 It seems that the text returned by the FOR JSON query is escaped as plain text. This happens only if WITHOUT_ARRAY_WRAPPER is specified. Why isn't it treated as a JSON object and included unescaped in the result?  
  
 **Answer.** If you specify the `WITHOUT_ARRAY_WRAPPER` option in the inner `FOR JSON`, the resulting JSON text is not necessarily valid JSON. Therefore the outer `FOR JSON` assumes that this is plain text and escapes the string. If you are sure that the JSON output is valid, wrap it with the `JSON_QUERY` function to promote it to properly formatted JSON, as shown in the following example.  
  
```sql  
SELECT 'Text' as myText,  
      JSON_QUERY((SELECT 12 day, 8 mon FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)) as myJson  
FOR JSON PATH    
```  

## OPENJSON and JSON input

### Return a nested JSON sub-object from JSON text with OPENJSON  
 **Question.** I can't open an array of complex JSON objects that contains both scalar values, objects, and arrays using OPENJSON with an explicit schema. When I reference a key in the WITH clause, only scalar values are returned. Objects and arrays are returned as null values. How can I extract objects or arrays as JSON objects?  
  
 **Answer.** If you want to return an object or an array as a column, use the AS JSON option in the column definition, as shown in the following example.  
  
```sql  
SELECT scalar1, scalar2, obj1, obj2, arr1  
FROM OPENJSON(@json)  
    WITH ( scalar1 int,  
        scalar2 datetime2,  
        obj1 NVARCHAR(MAX) AS JSON,  
        obj2 NVARCHAR(MAX) AS JSON,  
        arr1 NVARCHAR(MAX) AS JSON)  
```  

### Return long text value with OPENJSON instead of JSON_VALUE
 **Question.** I have description key in JSON text that contains long text. `JSON_VALUE(@json, '$.description')` returns NULL instead of a value.  
  
 **Answer.** JSON_VALUE is designed to return small scalar values. Generally the function returns NULL instead of an overflow error. If you want to return longer values, use OPENJSON, which supports NVARCHAR(MAX) values, as shown in the following example.  
  
```sql  
SELECT myText FROM OPENJSON(@json) WITH (myText NVARCHAR(MAX) '$.description')  
```  

### Handle duplicate keys with OPENJSON instead of JSON_VALUE
 **Question.** I have duplicate keys in the JSON text. JSON_VALUE returns only the first key found on the path. How can I return all keys that have the same name?  
  
 **Answer.** The built-in JSON scalar functions return only the first occurrence of the referenced object. If you need more than one key, use the OPENJSON table-valued function, as shown in the following example.  
  
```sql  
SELECT value FROM OPENJSON(@json, '$.info.settings')  
WHERE [key] = 'color'  
```  

### OPENJSON requires compatibility level 130  
 **Question.** I'm trying to run  OPENJSON in SQL Server 2016 and I'm getting the following error.  
  
 `Msg 208, Level 16, State 1 'Invalid object name OPENJSON'`  
  
 **Answer.** The OPENJSON function is available only under compatibility level 130. If your DB compatibility level is lower than 130, OPENJSON is hidden. Other JSON functions are available at all compatibility levels.  
 
## Other questions

### Reference keys that contain non-alphanumeric characters in JSON text  
 **Question.** I have non-alphanumeric characters in keys in my JSON text. How can I reference these properties?  
  
 **Answer.** You have to surround them with quotes in JSON paths. For example, `JSON_VALUE(@json, '$."$info"."First Name".value')`.
 
## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

> [!NOTE]
> Some of the video links in this section may not work at this time. Microsoft is migrating content formerly on Channel 9 to a new platform. We will update the links as the videos are migrated to the new platform.

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven-SQLServer2016/JSON-as-bridge-betwen-NoSQL-relational-worlds)

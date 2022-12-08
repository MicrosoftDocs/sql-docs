---
description: "Validate, Query, and Change JSON Data with Built-in Functions (SQL Server)"
title: "Validate, Query, and Change JSON Data with Built-in Functions"
ms.date: 06/03/2020
ms.service: sql
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "JSON, built-in functions"
  - "functions (JSON)"
ms.assetid: 6b6c7673-d818-4fa9-8708-b4ed79cb1b41
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: jroth
ms.custom: seo-dt-2019
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Validate, Query, and Change JSON Data with Built-in Functions (SQL Server)
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sqlserver2016-asdb.md)]

The built-in support for JSON includes the following built-in functions described briefly in this topic.  
  
-   [ISJSON](#ISJSON) tests whether a string contains valid JSON.  
  
-   [JSON_VALUE](#VALUE) extracts a scalar value from a JSON string.  
  
-   [JSON_QUERY](#QUERY) extracts an object or an array from a JSON string.  
  
-   [JSON_MODIFY](#MODIFY) updates the value of a property in a JSON string and returns the updated JSON string.  
 
## JSON text for the examples on this page

The examples on this page use the JSON text similar to the content shown in the following example:

```json
{
  "id": "WakefieldFamily",
  "parents": [
      { "familyName": "Wakefield", "givenName": "Robin" },
      { "familyName": "Miller", "givenName": "Ben" }
  ],
  "children": [
      {
        "familyName": "Merriam",
        "givenName": "Jesse",
        "gender": "female",
        "grade": 1,
        "pets": [
            { "givenName": "Goofy" },
            { "givenName": "Shadow" }
        ]
      },
      { 
        "familyName": "Miller",
         "givenName": "Lisa",
         "gender": "female",
         "grade": 8 }
  ],
  "address": { "state": "NY", "county": "Manhattan", "city": "NY" },
  "creationDate": 1431620462,
  "isRegistered": false
}
```

This JSON document, which contains nested complex elements, is stored in the following sample table:

```sql
CREATE TABLE Families (
   id int identity constraint PK_JSON_ID primary key,
   doc nvarchar(max)
)
``` 

##  <a name="ISJSON"></a> Validate JSON text by using the ISJSON function  
 The **ISJSON** function tests whether a string contains valid JSON.  
  
The following example returns rows in which the JSON column contains valid JSON text. Note that without explicit JSON constraint, you can enter any text in the NVARCHAR column:  
  
```sql  
SELECT *
FROM Families
WHERE ISJSON(doc) > 0 
```  

For more info, see [ISJSON &#40;Transact-SQL&#41;](../../t-sql/functions/isjson-transact-sql.md).  
  
##  <a name="VALUE"></a> Extract a value from JSON text by using the JSON_VALUE function  
The **JSON_VALUE** function extracts a scalar value from a JSON string. The following query will return the documents where the `id` JSON field matches the value `AndersenFamily`, ordered by `city` and `state` JSON fields:

```sql  
SELECT JSON_VALUE(f.doc, '$.id')  AS Name, 
       JSON_VALUE(f.doc, '$.address.city') AS City,
       JSON_VALUE(f.doc, '$.address.county') AS County
FROM Families f 
WHERE JSON_VALUE(f.doc, '$.id') = N'AndersenFamily'
ORDER BY JSON_VALUE(f.doc, '$.address.city') DESC, JSON_VALUE(f.doc, '$.address.state') ASC
```  

The results of this query are shown in the following table:

| Name | City | County |
| --- | --- | --- |
| AndersenFamily | NY | Manhattan |

For more info, see [JSON_VALUE &#40;Transact-SQL&#41;](../../t-sql/functions/json-value-transact-sql.md).  
  
##  <a name="QUERY"></a> Extract an object or an array from JSON text by using the JSON_QUERY function  

The **JSON_QUERY** function extracts an object or an array from a JSON string. The following example shows how to return a JSON fragment in query results.  
  
```sql
SELECT JSON_QUERY(f.doc, '$.address') AS Address,
       JSON_QUERY(f.doc, '$.parents') AS Parents,
       JSON_QUERY(f.doc, '$.parents[0]') AS Parent0
FROM Families f 
WHERE JSON_VALUE(f.doc, '$.id') = N'AndersenFamily'
```  
The results of this query are shown in the following table:

| Address | Parents | Parent0 |
| --- | --- | --- |
| { "state": "NY", "county": "Manhattan", "city": "NY" } | [{ "familyName": "Wakefield", "givenName": "Robin" }, {"familyName": "Miller", "givenName": "Ben" } ]| { "familyName": "Wakefield", "givenName": "Robin" } |

For more info, see [JSON_QUERY &#40;Transact-SQL&#41;](../../t-sql/functions/json-query-transact-sql.md).  

## Parse nested JSON collections

`OPENJSON` function enables you to transform JSON sub-array into the rowset and then join it with the parent element. As an example, you can return all family documents, and "join" them with their `children` objects that are stored as an inner JSON array:

```sql
SELECT JSON_VALUE(f.doc, '$.id')  AS Name, 
       JSON_VALUE(f.doc, '$.address.city') AS City,
       c.givenName, c.grade
FROM Families f
		CROSS APPLY OPENJSON(f.doc, '$.children')
			WITH(grade int, givenName nvarchar(100))  c
```

The results of this query are shown in the following table:

| Name | City | givenName | grade |
| --- | --- | --- | --- |
| AndersenFamily | NY | Jesse | 1 |
| AndersenFamily | NY | Lisa | 8 |

We are getting two rows as a result because one parent row is joined with two child rows produced by parsing two elements of the children subarray. `OPENJSON` function parses `children` fragment from the `doc` column and returns `grade` and `givenName` from each element as a set of rows. This rowset can be joined with the parent document.
 
## Query nested hierarchical JSON sub-arrays

You can apply multiple `CROSS APPLY OPENJSON` calls in order to query nested JSON structures. The JSON document used in this example has a nested array called `children`, where each child has nested array of `pets`. The following query will parse children from each document, return each array object as row, and then parse `pets` array:

```sql
SELECT	familyName,
	c.givenName AS childGivenName,
	c.firstName AS childFirstName,
	p.givenName AS petName 
FROM Families f 
	CROSS APPLY OPENJSON(f.doc) 
		WITH (familyName nvarchar(100), children nvarchar(max) AS JSON)
		CROSS APPLY OPENJSON(children) 
		WITH (givenName nvarchar(100), firstName nvarchar(100), pets nvarchar(max) AS JSON) as c
			OUTER APPLY OPENJSON (pets)
			WITH (givenName nvarchar(100))  as p
```

The first `OPENJSON` call will return fragment of `children` array using AS JSON clause. This array fragment will be provided to the second `OPENJSON` function that will return `givenName`, `firstName` of each child, as well as the array of `pets`. The array of `pets` will be provided to the third `OPENJSON` function that will return the `givenName` of the pet.
The results of this query are shown in the following table:

| familyName | childGivenName | childFirstName | petName |
| --- | --- | --- | --- |
| AndersenFamily | Jesse | Merriam | Goofy |
| AndersenFamily | Jesse | Merriam | Shadow |
| AndersenFamily | Lisa | Miller| `NULL` |

The root document is joined with two `children` rows returned by first `OPENJSON(children)` call making two rows (or tuples). Then each row is joined with the new rows generated by `OPENJSON(pets)` using `OUTER APPLY` operator. Jesse has two pets, so `(AndersenFamily, Jesse, Merriam)` is joined with two rows generated for Goofy and Shadow. Lisa doesn't have the pets, so there are no rows returned by  `OPENJSON(pets)` for this tuple. However, since we are using `OUTER APPLY` we are getting `NULL` in the column. If we put `CROSS APPLY` instead of `OUTER APPLY`, Lisa would not be returned in the result because there are no pets rows that could be joined with this tuple.

##  <a name="JSONCompare"></a> Compare JSON_VALUE and JSON_QUERY  
The key difference between **JSON_VALUE** and **JSON_QUERY** is that **JSON_VALUE** returns a scalar value, while **JSON_QUERY** returns an object or an array.  
  
Consider the following sample JSON text.  
  
```json  
{
	"a": "[1,2]",
	"b": [1, 2],
	"c": "hi"
}  
```  
  
In this sample JSON text, data members "a" and "c" are string values, while data member "b" is an array. **JSON_VALUE** and **JSON_QUERY** return the following results:  
  
|Path|**JSON_VALUE** returns|**JSON_QUERY** returns|  
|-----------|-----------------------------|-----------------------------|  
|**$**|NULL or error|`{ "a": "[1,2]", "b": [1,2], "c":"hi"}`|  
|**$.a**|[1,2]|NULL or error|  
|**$.b**|NULL or error|[1,2]|  
|**$.b[0]**|1|NULL or error|  
|**$.c**|hi|NULL or error|  
  
## Test JSON_VALUE and JSON_QUERY with the AdventureWorks sample database  
Test the built-in functions described in this topic by running the following examples with the AdventureWorks sample database. For info about where to get AdventureWorks, and about how to add JSON data for testing by running a script, see [Test drive built-in JSON support](json-data-sql-server.md#test-drive-built-in-json-support-with-the-adventureworks-sample-database).
  
In the following examples, the `Info` column in the `SalesOrder_json` table contains JSON text.  
  
### Example 1 - Return both standard columns and JSON data  
The following query returns values from both standard relational columns and from a JSON column.  
  
```sql  
SELECT SalesOrderNumber, OrderDate, Status, ShipDate, Status, AccountNumber, TotalDue,
 JSON_QUERY(Info,'$.ShippingInfo') ShippingInfo,
 JSON_QUERY(Info,'$.BillingInfo') BillingInfo,
 JSON_VALUE(Info,'$.SalesPerson.Name') SalesPerson,
 JSON_VALUE(Info,'$.ShippingInfo.City') City,
 JSON_VALUE(Info,'$.Customer.Name') Customer,
 JSON_QUERY(OrderItems,'$') OrderItems
FROM Sales.SalesOrder_json
WHERE ISJSON(Info) > 0
```  
  
### Example 2- Aggregate and filter JSON values  
The following query aggregates subtotals by customer name (stored in JSON) and status (stored in an ordinary column). Then it filters the results by city (stored in JSON) and OrderDate (stored in an ordinary column).  
  
```sql  
DECLARE @territoryid INT;
DECLARE @city NVARCHAR(32);

SET @territoryid=3;

SET @city=N'Seattle';

SELECT JSON_VALUE(Info, '$.Customer.Name') AS Customer, Status, SUM(SubTotal) AS Total
FROM Sales.SalesOrder_json
WHERE TerritoryID=@territoryid
 AND JSON_VALUE(Info, '$.ShippingInfo.City') = @city
 AND OrderDate > '1/1/2015'
GROUP BY JSON_VALUE(Info, '$.Customer.Name'), Status
HAVING SUM(SubTotal)>1000
```  
  
##  <a name="MODIFY"></a> Update property values in JSON text by using the JSON_MODIFY function  
The **JSON_MODIFY** function updates the value of a property in a JSON string and returns the updated JSON string.  
  
The following example updates the value of a JSON property in a variable that contains JSON.  
  
```sql  
SET @info = JSON_MODIFY(@jsonInfo, "$.info.address[0].town", 'London')    
```  
  
 For more info, see [JSON_MODIFY &#40;Transact-SQL&#41;](../../t-sql/functions/json-modify-transact-sql.md).  
  
## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

> [!NOTE]
> Some of the video links in this section may not work at this time. Microsoft is migrating content formerly on Channel 9 to a new platform. We will update the links as the videos are migrated to the new platform.

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven-SQLServer2016/JSON-as-bridge-betwen-NoSQL-relational-worlds)
  
## See Also  
 [ISJSON &#40;Transact-SQL&#41;](../../t-sql/functions/isjson-transact-sql.md)   
 [JSON_VALUE &#40;Transact-SQL&#41;](../../t-sql/functions/json-value-transact-sql.md)   
 [JSON_QUERY &#40;Transact-SQL&#41;](../../t-sql/functions/json-query-transact-sql.md)   
 [JSON_MODIFY &#40;Transact-SQL&#41;](../../t-sql/functions/json-modify-transact-sql.md)   
 [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md)  
  
  

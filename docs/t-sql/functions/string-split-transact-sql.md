---
title: "STRING_SPLIT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/28/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: "jrasnick"
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STRING_SPLIT"
  - "STRING_SPLIT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STRING_SPLIT function"
ms.assetid: 3273dbf3-0b4f-41e1-b97e-b4f67ad370b9
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: "= azuresqldb-current||=azure-sqldw-latest||>= sql-server-2016 || >= sql-server-linux-2017 || = sqlallproducts-allversions" 
---
# STRING_SPLIT (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md.md](../../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]

A table-valued function that splits a string into rows of substrings, based on a specified separator character.

#### Compatibility level 130

STRING_SPLIT requires the compatibility level to be at least 130. When the level is less than 130, SQL Server is unable to find the STRING_SPLIT function.

To change the compatibility level of a database, refer to [View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md).

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  

```sql
STRING_SPLIT ( string , separator )  
```

## Arguments

 *string*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any character type (for example, **nvarchar**, **varchar**, **nchar**, or **char**).  
  
 *separator*  
 Is a single character [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any character type (for example, **nvarchar(1)**, **varchar(1)**, **nchar(1)**, or **char(1)**) that is used as separator for concatenated substrings.  
  
## Return Types  

Returns a single-column table whose rows are the substrings. The name of the column is **value**. Returns **nvarchar** if any of the input arguments are either **nvarchar** or **nchar**. Otherwise returns **varchar**. The length of the return type is the same as the length of the string argument.  
  
## Remarks  

**STRING_SPLIT** inputs a string that has delimited substrings, and inputs one character to use as the delimiter or separator. STRING_SPLIT outputs a single-column table whose rows contain the substrings. The name of the output column is **value**.

The output rows might be in any order. The order is _not_ guaranteed to match the order of the substrings in the input string. You can override the final sort order by using an ORDER BY clause on the SELECT statement (`ORDER BY value`).

Empty zero-length substrings are present when the input string contains two or more consecutive occurrences of the delimiter character. Empty substrings are treated the same as are plain substrings. You can filter out any rows that contain the empty substring by using the WHERE clause (`WHERE value <> ''`). If the input string is NULL, the STRING_SPLIT table-valued function returns an empty table.  

As an example, the following SELECT statement uses the space character as the separator:

```sql
SELECT value FROM STRING_SPLIT('Lorem ipsum dolor sit amet.', ' ');
```

In a practice run, the preceding SELECT returned following result table:  
  
|value|  
| :-- |  
|Lorem|  
|ipsum|  
|dolor|  
|sit|  
|amet.|  
| &nbsp; |

## Examples  
  
### A. Split comma-separated value string

Parse a comma-separated list of values and return all non-empty tokens:  

```sql
DECLARE @tags NVARCHAR(400) = 'clothing,road,,touring,bike'  
  
SELECT value  
FROM STRING_SPLIT(@tags, ',')  
WHERE RTRIM(value) <> '';
```

STRING_SPLIT will return empty string if there is nothing between separator. Condition RTRIM(value) <> '' will remove empty tokens.  
  
### B. Split comma-separated value string in a column

Product table has a column with comma-separate list of tags shown in the following example:  
  
|ProductId|Name|Tags|  
|---------------|----------|----------|  
|1|Full-Finger Gloves|clothing,road,touring,bike|  
|2|LL Headset|bike|  
|3|HL Mountain Frame|bike,mountain|  
  
Following query transforms each list of tags and joins them with the original row:  

```sql  
SELECT ProductId, Name, value  
FROM Product  
    CROSS APPLY STRING_SPLIT(Tags, ',');  
```

 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
|ProductId|Name|value|  
|---------------|----------|-----------|  
|1|Full-Finger Gloves|clothing|  
|1|Full-Finger Gloves|road|  
|1|Full-Finger Gloves|touring|  
|1|Full-Finger Gloves|bike|  
|2|LL Headset|bike|  
|3|HL Mountain Frame|bike|  
|3|HL Mountain Frame|mountain|  

  >[!NOTE]
  > The order of the output may vary as the order is _not_ guaranteed to match the order of the substrings in the input string.
  
### C. Aggregation by values

Users must create a report that shows the number of products per each tag, ordered by number of products, and to filter only the tags with more than two products.  

```sql  
SELECT value as tag, COUNT(*) AS [Number of articles]  
FROM Product  
    CROSS APPLY STRING_SPLIT(Tags, ',')  
GROUP BY value  
HAVING COUNT(*) > 2  
ORDER BY COUNT(*) DESC;  
```

### D. Search by tag value

Developers must create queries that find articles by keywords. They can use following queries:  
  
To find products with a single tag (clothing):  

```sql
SELECT ProductId, Name, Tags  
FROM Product  
WHERE 'clothing' IN (SELECT value FROM STRING_SPLIT(Tags, ','));  
```

Find products with two specified tags (clothing and road):  

```sql  
SELECT ProductId, Name, Tags  
FROM Product  
WHERE EXISTS (SELECT *  
    FROM STRING_SPLIT(Tags, ',')  
    WHERE value IN ('clothing', 'road'));  
```

### E. Find rows by list of values

Developers must create a query that finds articles by a list of IDs. They can use following query:  

```sql  
SELECT ProductId, Name, Tags  
FROM Product  
JOIN STRING_SPLIT('1,2,3',',')
    ON value = ProductId;  
```  

The preceding STRING_SPLIT usage is a replacement for a common anti-pattern. Such an anti-pattern can involve the creation of a dynamic SQL string in the application layer or in Transact-SQL. Or an anti-pattern can be achieved by using the LIKE operator. See the following example SELECT statement:

```sql  
SELECT ProductId, Name, Tags  
FROM Product  
WHERE ',1,2,3,' LIKE '%,' + CAST(ProductId AS VARCHAR(20)) + ',%';  
```

## See Also

[LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)<br />
[LTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/ltrim-transact-sql.md)<br />
[RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)<br />
[RTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/rtrim-transact-sql.md)<br />
[SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)<br />
[TRIM &#40;Transact-SQL&#41;](../../t-sql/functions/trim-transact-sql.md)<br />
[String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)

---
title: "STRING_SPLIT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/15/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
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
---
# STRING_SPLIT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

> [!div class="nextstepaction"]
> [Please help improve SQL Server docs!](https://80s3ignv.optimalworkshop.com/optimalsort/36yyw5kq-0)

Splits the character expression using specified separator.  
  
> [!NOTE]  
> The **STRING_SPLIT** function is available only under compatibility level 130 and above. If your database compatibility level is lower than 130, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will not be able to find and execute **STRING_SPLIT** function. To change the compatibility level of a database, refer to [View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md).
> Note that compatibility level 120 might be default even in new [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
STRING_SPLIT ( string , separator )  
```  
  
## Arguments  
 *string*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any character type (for example, **nvarchar**, **varchar**, **nchar**, or **char**).  
  
 *separator*  
 Is a single character [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any character type (for example, **nvarchar(1)**, **varchar(1)**, **nchar(1)**, or **char(1)**) that is used as separator for concatenated strings.  
  
## Return Types  
 Returns a single-column table with fragments. The name of the column is **value**. Returns **nvarchar** if any of the input arguments are either **nvarchar** or **nchar**. Otherwise returns **varchar**. The length of the return type is the same as the length of the string argument.  
  
## Remarks  
**STRING_SPLIT** takes a string that should be divided and the separator that will be used to divide string. It returns a single-column table with substrings. For example, the following  statement `SELECT value FROM STRING_SPLIT('Lorem ipsum dolor sit amet.', ' ');` using the space character as the separator, returns following result table:  
  
|value|  
|-----------|  
|Lorem|  
|ipsum|  
|dolor|  
|sit|  
|amet.|  
  
If the input string is **NULL**, the **STRING_SPLIT** table-valued function returns an empty table.  

If the input string is not **NULL**, the **STRING_SPLIT** table-valued function returns a result set in {the order of | no predetermined order} with respect to the delimited substrings as found in the input string
  
**STRING_SPLIT** requires at least compatibility mode 130.  
  
## Examples  
  
### A. Split comma-separated value string  
Parse a comma separated list of values and return all non-empty tokens:  
  
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
    WHERE value IN ('clothing', 'road');  
```  
  
### E. Find rows by list of values  
Developers must create a query that finds articles by a list of IDs. They can use following query:  
  
```sql  
SELECT ProductId, Name, Tags  
FROM Product  
JOIN STRING_SPLIT('1,2,3',',')   
    ON value = ProductId;  
```  
  
This is replacement for common anti-pattern such as creating a dynamic SQL string in application layer or [!INCLUDE[tsql](../../includes/tsql-md.md)], or by using LIKE operator:  
  
```sql  
SELECT ProductId, Name, Tags  
FROM Product  
WHERE ',1,2,3,' LIKE '%,' + CAST(ProductId AS VARCHAR(20)) + ',%';  
```  
  
## See Also  
[LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)     
[LTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/ltrim-transact-sql.md)     
[RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)    
[RTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/rtrim-transact-sql.md)     
[SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)     
[TRIM &#40;Transact-SQL&#41;](../../t-sql/functions/trim-transact-sql.md)     
[String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)      
  
  

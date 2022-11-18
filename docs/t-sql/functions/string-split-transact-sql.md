---
title: "STRING_SPLIT (Transact-SQL)"
description: "Transact-SQL reference for the STRING_SPLIT function. This table-valued function splits a string into substrings based on a character delimiter."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - "event-tier1-build-2022"
f1_keywords:
  - "STRING_SPLIT"
  - "STRING_SPLIT_TSQL"
helpviewer_keywords:
  - "STRING_SPLIT function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017"
---
# STRING_SPLIT (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

A table-valued function that splits a string into rows of substrings, based on a specified separator character.

#### Compatibility level 130

STRING_SPLIT requires the compatibility level to be at least 130. When the level is less than 130, SQL Server is unable to find the STRING_SPLIT function.

To change the compatibility level of a database, refer to [View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md).

> [!NOTE]
> Compatibility configuration is not needed for `STRING_SPLIT` in Azure Synapse Analytics.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  

```syntaxsql
STRING_SPLIT ( string , separator [ , enable_ordinal ] )  
```

## Arguments

#### *string*
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any character type (for example, **nvarchar**, **varchar**, **nchar**, or **char**).  
  
#### *separator*  
 Is a single character [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any character type (for example, **nvarchar(1)**, **varchar(1)**, **nchar(1)**, or **char(1)**) that is used as separator for concatenated substrings.  

#### *enable_ordinal*  
An **int** or **bit** [expression](../../t-sql/language-elements/expressions-transact-sql.md) that serves as a flag to enable or disable the `ordinal` output column. A value of 1 enables the `ordinal` column. If *enable_ordinal* is omitted, `NULL`, or has a value of 0, the `ordinal` column is disabled.  

> [!NOTE]
> The *enable_ordinal* argument and `ordinal` output column are currently supported in Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics (serverless SQL pool only). Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the argument and output column are available in SQL Server.

## Return Types  

If the `ordinal` output column is not enabled, STRING_SPLIT returns a single-column table whose rows are the substrings. The name of the column is `value`. It returns **nvarchar** if any of the input arguments are either **nvarchar** or **nchar**. Otherwise, it returns **varchar**. The length of the return type is the same as the length of the *string* argument.  

If the *enable_ordinal* argument is passed a value of 1, a second column named `ordinal` is returned that consists of the 1-based index values of each substring's position in the input string. The return type is **bigint**.  

  
## Remarks  

STRING_SPLIT inputs a string that has delimited substrings and inputs one character to use as the delimiter or separator. Optionally, the function supports a third argument with a value of 0 or 1 that disables or enables, respectively, the `ordinal` output column.  

STRING_SPLIT outputs a single-column or double-column table, depending on the *enable_ordinal* argument.  

 - If *enable_ordinal* is `NULL`, omitted, or has a value of 0, STRING_SPLIT returns a single-column table whose rows contain the substrings. The name of the output column is `value`.  

 - If *enable_ordinal* has a value of 1, the function returns a two-column table, including the `ordinal` column that consists of the 1-based index values of the substrings in the original input string.  

Note that the *enable_ordinal* argument must be a constant value, not a column or variable. It must also be either a **bit** or **int** data type with a value of 0 or 1. Otherwise, the function will raise an error.  

The output rows might be in any order. The order is _not_ guaranteed to match the order of the substrings in the input string. You can override the final sort order by using an ORDER BY clause on the SELECT statement, for example, `ORDER BY value` or `ORDER BY ordinal`.

0x0000 (**char(0)**) is an undefined character in Windows collations and cannot be included in STRING_SPLIT.

Empty zero-length substrings are present when the input string contains two or more consecutive occurrences of the delimiter character. Empty substrings are treated the same as are plain substrings. You can filter out any rows that contain the empty substring by using the WHERE clause, for example `WHERE value <> ''`. If the input string is `NULL`, the STRING_SPLIT table-valued function returns an empty table.  

As an example, the following SELECT statement uses the space character as the separator:

```sql
SELECT value FROM STRING_SPLIT('Lorem ipsum dolor sit amet.', ' ');
```

In a practice run, the preceding SELECT returned following result table:  
  
|**value**|  
| :-- |  
|Lorem|  
|ipsum|  
|dolor|  
|sit|  
|amet.|  

The following example enables the `ordinal` column by passing 1 for the optional third argument:  

```sql
SELECT * FROM STRING_SPLIT('Lorem ipsum dolor sit amet.', ' ', 1);
```

This statement then returns the following result table:  

|**value**|**ordinal**|  
| :-- | :-- |  
|Lorem|1|  
|ipsum|2|  
|dolor|3|  
|sit|4|  
|amet.|5|  

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
  
|**ProductId**|**Name**|**Tags**|  
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
  
|**ProductId**|**Name**|**value**|  
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
SELECT value as tag, COUNT(*) AS [number_of_articles]  
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

### F. Find rows by ordinal values  

The following statement finds all rows with an even index value:  

```sql
SELECT *
FROM STRING_SPLIT('Austin,Texas,Seattle,Washington,Denver,Colorado', ',', 1)
WHERE ordinal % 2 = 0;  
```

The above statement returns the following table:  

|**value**|**ordinal**|  
|----------|--------|  
|Texas|2|  
|Washington|4|  
|Colorado|6|  

### G. Order rows by ordinal values  

The following statement returns the split substring values of the input string and their ordinal values, ordered by the `ordinal` column:  

```sql
SELECT * FROM STRING_SPLIT('E-D-C-B-A', '-', 1) ORDER BY ordinal DESC;  
```

The above statement returns the following table:

|**value**|**ordinal**|  
|-----|--------|  
|A|5|  
|B|4|  
|C|3|  
|D|2|  
|E|1|  


## Next Steps

- [LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)
- [LTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/ltrim-transact-sql.md)
- [RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)
- [RTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/rtrim-transact-sql.md)
- [SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)
- [TRIM &#40;Transact-SQL&#41;](../../t-sql/functions/trim-transact-sql.md)
- [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)

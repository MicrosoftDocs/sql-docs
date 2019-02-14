---
title: "SUBSTRING (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/21/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SUBSTRING"
  - "SUBSTRING_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "portion of expression returned [SQL Server]"
  - "part of expression returned [SQL Server]"
  - "SUBSTRING function"
  - "offsets [SQL Server]"
  - "binary [SQL Server], returning part of"
  - "expressions [SQL Server], part returned"
  - "characters [SQL Server], returning part of"
ms.assetid: a19c808f-aaf9-4a69-af59-b1a5fc3e5c4c
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SUBSTRING (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

> [!div class="nextstepaction"]
> [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

Returns part of a character, binary, text, or image expression in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
SUBSTRING ( expression ,start , length )  
```  
  
## Arguments  
 *expression*  
 Is a **character**, **binary**, **text**, **ntext**, or **image** [expression](../../t-sql/language-elements/expressions-transact-sql.md).  
  
 *start*  
 Is an integer or **bigint** expression that specifies where the returned characters start. (The numbering is 1 based, meaning that the first character in the expression is 1). If *start* is less than 1, the returned expression will begin at the first character that is specified in *expression*. In this case, the number of characters that are returned is the largest value of either the sum of *start* + *length*- 1 or 0. If *start* is greater than the number of characters in the value expression, a zero-length expression is returned.  
  
 *length*  
 Is a positive integer or **bigint** expression that specifies how many characters of the *expression* will be returned. If *length* is negative, an error is generated and the statement is terminated. If the sum of *start* and *length* is greater than the number of characters in *expression*, the whole value expression beginning at *start* is returned.  
  
## Return Types  
 Returns character data if *expression* is one of the supported character data types. Returns binary data if *expression* is one of the supported **binary** data types. The returned string is the same type as the specified expression with the exceptions shown in the table.  
  
|Specified expression|Return type|  
|--------------------------|-----------------|  
|**char**/**varchar**/**text**|**varchar**|  
|**nchar**/**nvarchar**/**ntext**|**nvarchar**|  
|**binary**/**varbinary**/**image**|**varbinary**|  
  
## Remarks  
 The values for *start* and *length* must be specified in number of characters for **ntext**, **char**, or **varchar** data types and bytes for **text**, **image**, **binary**, or **varbinary** data types.  
  
 The *expression* must be **varchar(max)** or **varbinary(max)** when the *start* or *length* contains a value larger than 2147483647.  
  
## Supplementary Characters (Surrogate Pairs)  
 When using supplementary character (SC) collations, both *start* and *length* count each surrogate pair in *expression* as a single character. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
## Examples  
  
### A. Using SUBSTRING with a character string  
 The following example shows how to return only a part of a character string. From the `sys.databases` table, this query returns the system database names in the first column, the first letter of the database in the second column, and the third and fourth characters in the final column.  
  
```  
SELECT name, SUBSTRING(name, 1, 1) AS Initial ,
SUBSTRING(name, 3, 2) AS ThirdAndFourthCharacters
FROM sys.databases  
WHERE database_id < 5;   
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  

|name |Initial |ThirdAndFourthCharacters|
|---|--|--|
|master	 |m	 |st |
|tempdb	 |t	 |mp |
|model	 |m	 |de |
|msdb	 |m	 |db |


  
 Here is how to display the second, third, and fourth characters of the string constant `abcdef`.  
  
```  
SELECT x = SUBSTRING('abcdef', 2, 3);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
x  
----------  
bcd  
  
(1 row(s) affected)
```  
  
### B. Using SUBSTRING with text, ntext, and image data  
  
> [!NOTE]  
>  To run the following examples, you must install the **pubs** database.  
  
 The following example shows how to return the first 10 characters from each of a **text** and **image** data column in the `pub_info` table of the `pubs` database. **text** data is returned as **varchar**, and **image** data is returned as **varbinary**.  
  
```  
USE pubs;  
SELECT pub_id, SUBSTRING(logo, 1, 10) AS logo,   
   SUBSTRING(pr_info, 1, 10) AS pr_info  
FROM pub_info  
WHERE pub_id = '1756';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 pub_id logo    pr_info
------ ---------------------- ----------
1756   0x474946383961E3002500 This is sa

(1 row(s) affected)
```  
  
 The following example shows the effect of SUBSTRING on both **text** and **ntext** data. First, this example creates a new table in the `pubs` database named `npub_info`. Second, the example creates the `pr_info` column in the `npub_info` table from the first 80 characters of the `pub_info.pr_info` column and adds an `ü` as the first character. Lastly, an `INNER JOIN` retrieves all publisher identification numbers and the `SUBSTRING` of both the **text** and **ntext** publisher information columns.  
  
```  
IF EXISTS (SELECT table_name FROM INFORMATION_SCHEMA.TABLES   
      WHERE table_name = 'npub_info')  
   DROP TABLE npub_info;  
GO  
-- Create npub_info table in pubs database. Borrowed from instpubs.sql.  
USE pubs;  
GO  
CREATE TABLE npub_info  
(  
 pub_id char(4) NOT NULL  
    REFERENCES publishers(pub_id)  
    CONSTRAINT UPKCL_npubinfo PRIMARY KEY CLUSTERED,  
pr_info ntext NULL  
);  
  
GO  
  
-- Fill the pr_info column in npub_info with international data.  
RAISERROR('Now at the inserts to pub_info...',0,1);  
  
GO  
  
INSERT npub_info VALUES('0736', N'üThis is sample text data for New Moon Books, publisher 0736 in the pubs database')  
,('0877', N'üThis is sample text data for Binnet & Hardley, publisher 0877 in the pubs databa')  
,('1389', N'üThis is sample text data for Algodata Infosystems, publisher 1389 in the pubs da')  
,('9952', N'üThis is sample text data for Scootney Books, publisher 9952 in the pubs database')  
,('1622', N'üThis is sample text data for Five Lakes Publishing, publisher 1622 in the pubs d')  
,('1756', N'üThis is sample text data for Ramona Publishers, publisher 1756 in the pubs datab')  
,('9901', N'üThis is sample text data for GGG&G, publisher 9901 in the pubs database. GGG&G i')  
,('9999', N'üThis is sample text data for Lucerne Publishing, publisher 9999 in the pubs data');  
GO  
-- Join between npub_info and pub_info on pub_id.  
SELECT pr.pub_id, SUBSTRING(pr.pr_info, 1, 35) AS pr_info,  
   SUBSTRING(npr.pr_info, 1, 35) AS npr_info  
FROM pub_info pr INNER JOIN npub_info npr  
   ON pr.pub_id = npr.pub_id  
ORDER BY pr.pub_id ASC;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Using SUBSTRING with a character string  
 The following example shows how to return only a part of a character string. From the `dbo.DimEmployee` table, this query returns the last name in one column with only the first initial in the second column.  
  
```  
-- Uses AdventureWorks  
  
SELECT LastName, SUBSTRING(FirstName, 1, 1) AS Initial  
FROM dbo.DimEmployee  
WHERE LastName LIKE 'Bar%'  
ORDER BY LastName;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
LastName             Initial
-------------------- -------
Barbariol            A
Barber               D
Barreto de Mattos    P
```  
  
 The following example shows how to return the second, third, and fourth characters of the string constant `abcdef`.  
  
```  
USE ssawPDW;  
  
SELECT TOP 1 SUBSTRING('abcdef', 2, 3) AS x FROM dbo.DimCustomer;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
x
-----
bcd
```  
  
## See Also  
 [LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)  
 [LTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/ltrim-transact-sql.md)  
 [RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)  
 [RTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/rtrim-transact-sql.md)  
 [STRING_SPLIT &#40;Transact-SQL&#41;](../../t-sql/functions/string-split-transact-sql.md)  
 [TRIM &#40;Transact-SQL&#41;](../../t-sql/functions/trim-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  
  



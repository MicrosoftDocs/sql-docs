---
title: "uniqueidentifier (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/01/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, database-engine, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "uniqueidentifier"
  - "uniqueidentifier_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "uniqueidentifier data type"
  - "globally unique identifiers [SQL Server]"
  - "GUIDs [SQL Server]"
ms.assetid: b026035b-f3d2-4d70-989d-3884b4ca0233
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# uniqueidentifier (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Is a 16-byte GUID.
  
## Remarks  
A column or local variable of **uniqueidentifier** data type can be initialized to a value in the following ways:
-   By using the [NEWID](../../t-sql/functions/newid-transact-sql.md) or [NEWSEQUENTIALID](../../t-sql/functions/newsequentialid-transact-sql.md) functions.    
-   By converting from a string constant in the form *xxxxxxxx*-*xxxx*-*xxxx*-*xxxx*-*xxxxxxxxxxxx*, in which each *x* is a hexadecimal digit in the range 0-9 or a-f. For example, 6F9619FF-8B86-D011-B42D-00C04FC964FF is a valid **uniqueidentifier** value.  
  
Comparison operators can be used with **uniqueidentifier** values. However, ordering is not implemented by comparing the bit patterns of the two values. The only operations that can be performed against a **uniqueidentifier** value are comparisons (=, <>, \<, >, \<=, >=) and checking for NULL (IS NULL and IS NOT NULL). No other arithmetic operators can be used. All column constraints and properties, except IDENTITY, can be used on the **uniqueidentifier** data type.
  
Merge replication and transactional replication with updating subscriptions use **uniqueidentifier** columns to guarantee that rows are uniquely identified across multiple copies of the table.
  
## Converting uniqueidentifier Data  
The **uniqueidentifier** type is considered a character type for the purposes of conversion from a character expression, and therefore is subject to the truncation rules for converting to a character type. That is, when character expressions are converted to a character data type of a different size, values that are too long for the new data type are truncated. See the Examples section.
  
## Limitations and restrictions

These tools and features do not support the `uniqueidentifier` data type:
- PolyBase
- [dwloader loading tool](https://msdn.microsoft.com/sql/analytics-platform-system/dwloader) for Parallel Data Warehouse

## Examples  
The following example converts a `uniqueidentifier` value to a `char` data type.
  
```sql
DECLARE @myid uniqueidentifier = NEWID();  
SELECT CONVERT(char(255), @myid) AS 'char';  
```  
  
The following example demonstrates the truncation of data when the value is too long for the data type being converted to. Because the **uniqueidentifier** type is limited to 36 characters, the characters that exceed that length are truncated.
  
```sql
DECLARE @ID nvarchar(max) = N'0E984725-C51C-4BF4-9960-E1C80E27ABA0wrong';  
SELECT @ID, CONVERT(uniqueidentifier, @ID) AS TruncatedValue;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
String                                       TruncatedValue  
-------------------------------------------- ------------------------------------  
0E984725-C51C-4BF4-9960-E1C80E27ABA0wrong    0E984725-C51C-4BF4-9960-E1C80E27ABA0  
  
(1 row(s) affected)  
```  
  
## See also
[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)  
[NEWID &#40;Transact-SQL&#41;](../../t-sql/functions/newid-transact-sql.md)  
[NEWSEQUENTIALID &#40;Transact-SQL&#41;](../../t-sql/functions/newsequentialid-transact-sql.md)    
[SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)  
[Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)
  
  

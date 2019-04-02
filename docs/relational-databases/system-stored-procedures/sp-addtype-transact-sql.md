---
title: "sp_addtype (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addtype"
  - "sp_addtype_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addtype"
ms.assetid: ed72cd8e-5ff7-4084-8458-2d8ed279d817
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addtype (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates an alias data type.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE TYPE](../../t-sql/statements/create-type-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addtype [ @typename = ] type,   
    [ @phystype = ] system_data_type   
    [ , [ @nulltype = ] 'null_type' ] ;  
```  
  
## Arguments  
`[ @typename = ] type`
 Is the name of the alias data type. Alias data type names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md) and must be unique in each database. *type* is **sysname**, with no default.  
  
`[ @phystype = ] system_data_type`
 Is the physical, or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supplied, data type on which the alias data type is based.*system_data_type* is **sysname**, with no default, and can be one of these values:  
  
||||  
|-|-|-|  
|**bigint**|**binary(n)**|**bit**|  
|**char(n)**|**datetime**|**decimal**|  
|**float**|**image**|**int**|  
|**money**|**nchar(n)**|**ntext**|  
|**numeric**|**nvarchar(n)**|**real**|  
|**smalldatetime**|**smallint**|**smallmoney**|  
|**sql_variant**|**text**|**tinyint**|  
|**uniqueidentifier**|**varbinary(n)**|**varchar(n)**|  
  
 Quotation marks are required around all parameters that have embedded blank spaces or punctuation marks. For more information about available data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).  
  
 *n*  
 Is a nonnegative integer that indicates the length for the chosen data type.  
  
 *P*  
 Is a nonnegative integer that indicates the maximum total number of decimal digits that can be stored, both to the left and to the right of the decimal point. For more information, see [decimal and numeric &#40;Transact-SQL&#41;](../../t-sql/data-types/decimal-and-numeric-transact-sql.md).  
  
 *s*  
 Is a nonnegative integer that indicates the maximum number of decimal digits that can be stored to the right of the decimal point, and it must be less than or equal to the precision. For more information, see [decimal and numeric &#40;Transact-SQL&#41;](../../t-sql/data-types/decimal-and-numeric-transact-sql.md).  
  
`[ @nulltype = ] 'null_type'`
 Indicates the way the alias data type handles null values. *null_type* is **varchar(**8**)**, with a default of NULL, and must be enclosed in single quotation marks ('NULL', 'NOT NULL', or 'NONULL'). If *null_type* is not explicitly defined by **sp_addtype**, it is set to the current default nullability. Use the GETANSINULL system function to determine the current default nullability. This can be adjusted by using the SET statement or ALTER DATABASE. Nullability should be explicitly defined. If **@phystype** is **bit**, and **@nulltype** is not specified, the default is NOT NULL.  
  
> [!NOTE]  
>  The *null_type* parameter only defines the default nullability for this data type. If nullability is explicitly defined when the alias data type is used during table creation, it takes precedence over the defined nullability. For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md) and [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md).  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 An alias data type name must be unique in the database, but alias data types with different names can have the same definition.  
  
 Executing **sp_addtype** creates an alias data type that appears in the **sys.types** catalog view for a specific database. If the alias data type must be available in all new user-defined databases, add it to **model**. After an alias data type is created, you can use it in CREATE TABLE or ALTER TABLE, and also bind defaults and rules to the alias data type. All scalar alias data types that are created by using **sp_addtype** are contained in the **dbo** schema.  
  
 Alias data types inherit the default collation of the database. The collations of columns and variables of alias types are defined in the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE TABLE, ALTER TABLE and DECLARE @*local_variable* statements. Changing the default collation of the database applies only to new columns and variables of the type; it does not change the collation of existing ones.  
  
> [!IMPORTANT]  
>  For backward compatibility purposes, the **public** database role is automatically granted REFERENCES permission on alias data types that are created by using **sp_addtype**. Note when alias data types are created by using the CREATE TYPE statement instead of **sp_addtype**, no such automatic grant occurs.  
  
 Alias data types cannot be defined by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **timestamp**, **table**, **xml**, **varchar(max)**, **nvarchar(max)** or **varbinary(max)** data types.  
  
## Permissions  
 Requires membership in the **db_owner** or **db_ddladmin** fixed database role.  
  
## Examples  
  
### A. Creating an alias data type that does not allow for null values  
 The following example creates an alias data type named `ssn` (social security number) that is based on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-supplied **varchar** data type. The `ssn` data type is used for columns holding 11-digit social security numbers (999-99-9999). The column cannot be NULL.  
  
 Notice that `varchar(11)` is enclosed in single quotation marks because it contains punctuation (parentheses).  
  
```  
USE master;  
GO  
EXEC sp_addtype ssn, 'varchar(11)', 'NOT NULL';  
GO  
```  
  
### B. Creating an alias data type that allows for null values  
 The following example creates an alias data type (based on `datetime`) named `birthday` that allows for null values.  
  
```  
USE master;  
GO  
EXEC sp_addtype birthday, datetime, 'NULL';  
```  
  
### C. Creating additional alias data types  
 The following example creates two additional alias data types, `telephone` and `fax`, for both domestic and international telephone and fax numbers.  
  
```  
USE master;  
GO  
EXEC sp_addtype telephone, 'varchar(24)', 'NOT NULL';  
GO  
EXEC sp_addtype fax, 'varchar(24)', 'NULL';  
GO  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md)   
 [CREATE DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/create-default-transact-sql.md)   
 [CREATE RULE &#40;Transact-SQL&#41;](../../t-sql/statements/create-rule-transact-sql.md)   
 [sp_bindefault &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-bindefault-transact-sql.md)   
 [sp_bindrule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-bindrule-transact-sql.md)   
 [sp_droptype &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droptype-transact-sql.md)   
 [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)   
 [sp_unbindefault &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unbindefault-transact-sql.md)   
 [sp_unbindrule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unbindrule-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  

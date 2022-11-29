---
title: "Data type synonyms (Transact-SQL)"
description: "Data type synonyms (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/23/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
helpviewer_keywords:
  - "data types [SQL Server], synonyms"
  - "alternate names [SQL Server]"
  - "synonyms [SQL Server], data types"
dev_langs:
  - "TSQL"
---
# Data type synonyms (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Data type synonyms are included in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for ISO compatibility. The following table lists the synonyms and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data types that they map to.
  
|Synonym|SQL Server system data type|  
|---|---|
|**Binary varying**|**varbinary**|  
|**char varying**|**varchar**|  
|**character**|**char**|  
|**character**|**char(1)**|  
|**character(**_n_**)**|**char(n)**|  
|**character varying(**_n_**)**|**varchar(n)**|  
|**Dec**|**decimal**|  
|**Double precision**|**float**|  
|**float**[**(**_n_**)**] for _n_ = 1-7|**real**|  
|**float**[**(**_n_**)**] for _n_ = 8-15|**float**|  
|**integer**|**int**|  
|**national character(**_n_**)**|**nchar(n)**|  
|**national char(**_n_**)**|**nchar(n)**|  
|**national character varying(**_n_**)**|**nvarchar(n)**|  
|**national char varying(**_n_**)**|**nvarchar(n)**|  
|**national text**|**ntext**|  
|**rowversion**|**timestamp**|  
  
Data type synonyms can be used instead of the corresponding base data type name in data definition language (DDL) statements. These statements include CREATE TABLE, CREATE PROCEDURE, and DECLARE *\@variable*. However, after the object is created, the synonyms have no visibility. When the object is created, the object is assigned the base data type that is associated with the synonym. There's no record that the synonym was specified in the statement that created the object.
  
Objects that are derived from the original object, such as result set columns or expressions, are assigned the base data type. Any metadata functions that use the original object or any derived objects will report the base data type, not the synonym, including:

* Metadata operations, such as **sp_help** and other system stored procedures,
* Information schema views, and
* Data access API metadata operations that report the data types of table or result set columns.
  
For example, you can create a table by specifying `national character varying`:
  
```sql
CREATE TABLE ExampleTable (PriKey int PRIMARY KEY, VarCharCol national character varying(10))  
```  
  
`VarCharCol` is assigned an **nvarchar(10)** data type, and all following metadata functions will report the column as an **nvarchar(10)** column. The metadata functions will never report them as a **national character varying(10)** column.
  
## See also
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
  
  

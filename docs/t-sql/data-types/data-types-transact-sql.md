---
title: "Data types (Transact-SQL)"
description: "This article provides a summary of the different data types available in SQL Server."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "09/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
helpviewer_keywords:
  - "system data types [SQL Server]"
  - "data types [SQL Server]"
  - "data types [SQL Server], about data types"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Data types (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], each column, local variable, expression, and parameter has a related data type. A data type is an attribute that specifies the type of data that the object can hold: integer data, character data, monetary data, date and time data, binary strings, and so on.
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supplies a set of system data types that define all the types of data that can be used with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can also define your own data types in [!INCLUDE[tsql](../../includes/tsql-md.md)] or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. Alias data types are based on the system-supplied data types. For more information about alias data types, see [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md). User-defined types obtain their characteristics from the methods and operators of a class that you create by using one of the programming languages supported by the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)].
  
When two expressions that have different data types, collations, precision, scale, or length are combined by an operator, the characteristics of result are determined by the following:
-   The data type of the result is determined by applying the rules of data type precedence to the data types of the input expressions. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
-   The collation of the result is determined by the rules of collation precedence when the result data type is **char**, **varchar**, **text**, **nchar**, **nvarchar**, or **ntext**. For more information, see [Collation Precedence &#40;Transact-SQL&#41;](../../t-sql/statements/collation-precedence-transact-sql.md).  
-   The precision, scale, and length of the result depend on the precision, scale, and length of the input expressions. For more information, see [Precision, Scale, and Length &#40;Transact-SQL&#41;](../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides data type synonyms for ISO compatibility. For more information, see [Data Type Synonyms &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-synonyms-transact-sql.md).
  
## Data type categories
Data types in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are organized into the following categories:
  
:::row:::
    :::column:::
        Exact numerics
    :::column-end:::
    :::column:::
        Unicode character strings
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        Approximate numerics
    :::column-end:::
    :::column:::
        Binary strings
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        Date and time
    :::column-end:::
    :::column:::
        Other data types
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        Character strings
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
 
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], based on their storage characteristics, some data types are designated as belonging to the following groups:
-   Large value data types: **varchar(max)**, and **nvarchar(max)**  
-   Large object data types: **text**, **ntext**, **image**, **varbinary(max)**, and **xml**  
  
    > [!NOTE]  
    >  sp_help returns -1 as the length for the large-value and **xml** data types.  
  
### Exact numerics
  
:::row:::
    :::column:::
        [bigint](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)
    :::column-end:::
    :::column:::
        [numeric](../../t-sql/data-types/decimal-and-numeric-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [bit](../../t-sql/data-types/bit-transact-sql.md)
    :::column-end:::
    :::column:::
        [smallint](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [decimal](../../t-sql/data-types/decimal-and-numeric-transact-sql.md)
    :::column-end:::
    :::column:::
        [smallmoney](../../t-sql/data-types/money-and-smallmoney-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [int](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)
    :::column-end:::
    :::column:::
        [tinyint](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [money](../../t-sql/data-types/money-and-smallmoney-transact-sql.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

### Approximate numerics
  
:::row:::
    :::column:::
        [float](../../t-sql/data-types/float-and-real-transact-sql.md)
    :::column-end:::
    :::column:::
        [real](../../t-sql/data-types/float-and-real-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

### Date and time
  
:::row:::
    :::column:::
        [date](../../t-sql/data-types/date-transact-sql.md)
    :::column-end:::
    :::column:::
        [datetimeoffset](../../t-sql/data-types/datetimeoffset-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [datetime2](../../t-sql/data-types/datetime2-transact-sql.md)
    :::column-end:::
    :::column:::
        [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [datetime](../../t-sql/data-types/datetime-transact-sql.md)
    :::column-end:::
    :::column:::
        [time](../../t-sql/data-types/time-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
  
### Character strings
  
:::row:::
    :::column:::
        [char](../../t-sql/data-types/char-and-varchar-transact-sql.md)
    :::column-end:::
    :::column:::
        [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [text](../../t-sql/data-types/ntext-text-and-image-transact-sql.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
 
### Unicode character strings
  
:::row:::
    :::column:::
        [nchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md)
    :::column-end:::
    :::column:::
        [nvarchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [ntext](../../t-sql/data-types/ntext-text-and-image-transact-sql.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
  

### Binary strings
  
:::row:::
    :::column:::
        [binary](../../t-sql/data-types/binary-and-varbinary-transact-sql.md)
    :::column-end:::
    :::column:::
        [varbinary](../../t-sql/data-types/binary-and-varbinary-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [image](../../t-sql/data-types/ntext-text-and-image-transact-sql.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

### Other data types

:::row:::
    :::column:::
        [cursor](../../t-sql/data-types/cursor-transact-sql.md)
    :::column-end:::
    :::column:::
        [rowversion](../../t-sql/data-types/rowversion-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [hierarchyid](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
    :::column-end:::
    :::column:::
        [uniqueidentifier](../../t-sql/data-types/uniqueidentifier-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md)
    :::column-end:::
    :::column:::
        [xml](../../t-sql/xml/xml-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [Spatial Geometry Types](../../t-sql/spatial-geometry/spatial-types-geometry-transact-sql.md) 
    :::column-end:::
    :::column:::
        [Spatial Geography Types](../../t-sql/spatial-geography/spatial-types-geography.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [table](../../t-sql/data-types/table-transact-sql.md) 
    :::column-end:::
    :::column:::
         
    :::column-end:::
:::row-end:::

  
## See also
[CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)
[EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)  
[Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
[Functions &#40;Transact-SQL&#41;](../../t-sql/functions/functions.md)  
[LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)  
[sp_droptype &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droptype-transact-sql.md)  
[sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)  
[sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)
  
  

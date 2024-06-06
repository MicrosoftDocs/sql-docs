---
title: "Data types (Transact-SQL)"
description: This article provides a summary of the different data types available in the SQL Server Database Engine.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/21/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - build-2024
helpviewer_keywords:
  - "system data types [SQL Server]"
  - "data types [SQL Server]"
  - "data types [SQL Server], about data types"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# Data types (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw.md)]

In the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)], each column, local variable, expression, and parameter has a related data type. A data type is an attribute that specifies the type of data that the object can hold: integer data, character data, monetary data, date and time data, binary strings, and so on.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supplies a set of system data types that define all the types of data that can be used with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can also define your own data types in [!INCLUDE [tsql](../../includes/tsql-md.md)] or the [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)]. Alias data types are based on the system-supplied data types. For more information about alias data types, see [CREATE TYPE](../statements/create-type-transact-sql.md). User-defined types obtain their characteristics from the methods and operators of a class that you create by using one of the programming languages supported by the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)].

When two expressions combined by an operator have different data types, collations, precision, scale, or length, the characteristics of the result are determined by the following conditions:

- The data type of the result is determined by applying the rules of [data type precedence](data-type-precedence-transact-sql.md) to the data types of the input expressions.

- The collation of the result is determined by the rules of collation precedence when the result data type is **char**, **varchar**, **text**, **nchar**, **nvarchar**, or **ntext**. For more information, see [Collation precedence](../statements/collation-precedence-transact-sql.md).

- The precision, scale, and length of the result depend on the precision, scale, and length of the input expressions. For more information, see [Precision, scale, and length (Transact-SQL)](precision-scale-and-length-transact-sql.md).

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides data type synonyms for ISO compatibility. For more information, see [Data type synonyms](data-type-synonyms-transact-sql.md).

For more specific information on data types in [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)], see [Table data types in Synapse SQL](/azure/synapse-analytics/sql/develop-tables-data-types).

For more specific information on data types in [!INCLUDE [fabric](../../includes/fabric.md)], see [Data type](../statements/create-table-azure-sql-data-warehouse.md?view=fabric&preserve-view=true#DataTypesFabric).

## Data type categories

Data types in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are organized into the following categories:

- [Exact numerics](#exact-numerics)
- [Approximate numerics](#approximate-numerics)
- [Date and time](#date-and-time)
- [Character strings](#character-strings)
- [Unicode character strings](#unicode-character-strings)
- [Binary strings](#binary-strings)
- [Other data types](#other-data-types)

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], based on their storage characteristics, some data types are designated as belonging to the following groups:

- Large value data types: **varchar(max)**, and **nvarchar(max)**
- Large object data types: **text**, **ntext**, **image**, **varbinary(max)**, and **xml**

  > [!NOTE]  
  > [sp_help](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md) returns `-1` as the length for the large-value and **xml** data types.

### Exact numerics

- [tinyint](int-bigint-smallint-and-tinyint-transact-sql.md)
- [smallint](int-bigint-smallint-and-tinyint-transact-sql.md)
- [int](int-bigint-smallint-and-tinyint-transact-sql.md)
- [bigint](int-bigint-smallint-and-tinyint-transact-sql.md)
- [bit](bit-transact-sql.md) <sup>1</sup>
- [decimal](decimal-and-numeric-transact-sql.md) <sup>2</sup>
- [numeric](decimal-and-numeric-transact-sql.md) <sup>2</sup>
- [money](money-and-smallmoney-transact-sql.md)
- [smallmoney](money-and-smallmoney-transact-sql.md)

<sup>1</sup> The **bit** data type is used for storing Boolean values.

<sup>2</sup> The **decimal** and **numeric** data types are identical.

### Approximate numerics

- [float](float-and-real-transact-sql.md)
- [real](float-and-real-transact-sql.md)

### Date and time

- [date](date-transact-sql.md)
- [time](time-transact-sql.md)
- [datetime2](datetime2-transact-sql.md)
- [datetimeoffset](datetimeoffset-transact-sql.md)
- [datetime](datetime-transact-sql.md)
- [smalldatetime](smalldatetime-transact-sql.md)

### Character strings

- [char](char-and-varchar-transact-sql.md)
- [varchar](char-and-varchar-transact-sql.md)
- [text](ntext-text-and-image-transact-sql.md)

### Unicode character strings

- [nchar](nchar-and-nvarchar-transact-sql.md)
- [nvarchar](nchar-and-nvarchar-transact-sql.md)
- [ntext](ntext-text-and-image-transact-sql.md)

### Binary strings

- [binary](binary-and-varbinary-transact-sql.md)
- [varbinary](binary-and-varbinary-transact-sql.md)
- [image](ntext-text-and-image-transact-sql.md)

### Other data types

- [cursor](cursor-transact-sql.md)
- [geography](../spatial-geography/spatial-types-geography.md) <sup>1</sup>
- [geometry](../spatial-geometry/spatial-types-geometry-transact-sql.md) <sup>1</sup>
- [hierarchyid](hierarchyid-data-type-method-reference.md)
- [json](json-data-type.md)
- [rowversion](rowversion-transact-sql.md)
- [sql_variant](sql-variant-transact-sql.md)
- [table](table-transact-sql.md)
- [uniqueidentifier](uniqueidentifier-transact-sql.md)
- [xml](../xml/xml-transact-sql.md)

<sup>1</sup> The **geography** and **geometry** data types are *spatial types*.

## Related content

- [CREATE PROCEDURE (Transact-SQL)](../statements/create-procedure-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../statements/create-table-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](../language-elements/declare-local-variable-transact-sql.md)
- [EXECUTE (Transact-SQL)](../language-elements/execute-transact-sql.md)
- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [What are the SQL database functions?](../functions/functions.md)
- [LIKE (Transact-SQL)](../language-elements/like-transact-sql.md)
- [sp_droptype (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-droptype-transact-sql.md)
- [sp_help (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)
- [sp_rename (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)

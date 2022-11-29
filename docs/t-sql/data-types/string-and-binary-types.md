---
title: "String and Binary Types"
description: "Learn about the string and binary types in the Database Engine, including binary, varbinary, char, nchar, varchar, and nvarchar."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/22/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
helpviewer_keywords:
  - "data types [SQL Server]"
  - "LOB data [SQL Server]"
dev_langs:
  - "TSQL"
---
# String and binary types

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports the following string and binary types.

|Type|Description|
|---|---|
|[binary and varbinary](../../t-sql/data-types/binary-and-varbinary-transact-sql.md)|Binary data types of either fixed length or variable length. Converting data to the **binary** and **varbinary** data types is useful if **binary** data is the easiest way to move around data.|
|[char and varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md)|Character data types that are either fixed-size, **char**, or variable-size, **varchar**.<br /><br />Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], when a UTF-8 enabled collation is used, these data types store the full range of Unicode character data and use the UTF-8 character encoding.|
|[nchar and nvarchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md)|Unicode character data types that are either fixed-size, **nchar**, or variable-size, **nvarchar**.<br /><br />Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], when a Supplementary Character (SC) enabled collation is used, these data types store the full range of Unicode character data and use the UTF-16 character encoding.|
|[ntext, text, and image](../../t-sql/data-types/ntext-text-and-image-transact-sql.md)|Fixed and variable-length data types for storing large non-Unicode and Unicode character and binary data. Unicode data uses the Unicode UCS-2 character set.<br /><br />The **ntext**, **text**, and **image** data types will be removed in a future version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Avoid using these data types in new development work, and plan to modify applications that currently use them.|

## See also

- [Data types (Transact-SQL)](data-types-transact-sql.md)
- [Numeric types](numeric-types.md)
- [String Functions](../../odbc/reference/appendixes/string-functions.md)
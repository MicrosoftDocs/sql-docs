---
title: "Specify Field Length by Using bcp (SQL Server)"
description: In SQL Server, if necessary, bcp prompts for field length, default field lengths, and impact of field-length on data storage in files that contain char data.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mahyon
ms.date: 02/10/2026
ms.service: sql
ms.subservice: data-movement
ms.topic: concept-article
helpviewer_keywords:
  - "native data format [SQL Server]"
  - "default field lengths"
  - "field length [SQL Server]"
  - "data formats [SQL Server], field length"
  - "bcp utility [SQL Server], field length"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Specify field length by using bcp (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

The field length indicates the maximum number of characters that are required to represent data in character format. The field length is already known if the data is stored in the native format; for example, the **int** data type takes 4 bytes. If you have indicated 0 for the prefix length, the **bcp** command prompts you for field length, the default field lengths, and the impact of field-length on data storage in data files that contain **char** data.

## The bcp prompt for field length

If an interactive **bcp** command contains the `in` or `out` option without either the format file switch (`-f`) or a data-format switch (`-n`, `-c`, `-w`, or `-N`), the command prompts for the field length of each data field, as follows:

`Enter length of field <field_name> [<default>]:`

For an example that shows this prompt in context, see [Specify Data Formats for Compatibility when Using bcp (SQL Server)](../../relational-databases/import-export/specify-data-formats-for-compatibility-when-using-bcp-sql-server.md).

> [!NOTE]  
> After you interactively specify all of the fields in a **bcp** command, the command prompts you save your responses for each field in a non-XML format file. For more information on non-XML format files, see [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md).

Whether a **bcp** command prompts for field length depends on several factors, as follows:

- When you copy data types that aren't of fixed length and you specify a prefix length of 0, **bcp** prompts for a field length.

- When converting noncharacter data to character data, **bcp** suggests a default field length large enough to store the data.

- If the file storage type is noncharacter, the **bcp** command doesn't prompt for a field length. The data is stored in the [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] native data representation (native format).

<a id="using-default-field-lengths"></a>

## Use default field lengths

Generally, [!INCLUDE [msCoName](../../includes/msconame-md.md)] recommends that you accept the **bcp**-suggested default values for the field length. When a character mode data file is created, using the default field length ensures that data isn't truncated and that numeric overflow errors don't occur.

If you specify a field length that is incorrect, problems can occur. For instance, if you copy numeric data and you specify a field length that is too short for the data, the **bcp** utility prints an overflow message and doesn't copy the data. Also, if you export **datetime** data and specify a field length of less than 26 bytes for the character string, the **bcp** utility truncates the data without an error message.

> [!IMPORTANT]  
> When the default size option is used, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] expects to read an entire string. In some situations, use of a default field length can lead to an "unexpected end of file" error. Typically, this error occurs with the **money** and **datetime** data types when only part of the expected field occurs in the data file; for example, when a **datetime** value of *mm*/*dd*/*yy* is specified without the time component and is, therefore, shorter than the expected 24 character length of a **datetime** value in **char** format. To avoid this type of error, use field terminators or fixed-length data fields, or change the default field length by specifying another value.

### Default field lengths for character file storage

The following table lists the default field lengths for data to be stored as a character-file storage type. Nullable data is the same length as nonnull data.

| Data type | Default length (characters) |
| --- | --- |
| **char** | Length defined for the column |
| **varchar** | Length defined for the column |
| **nchar** | Twice the length defined for the column |
| **nvarchar** | Twice the length defined for the column |
| **Text** | 0 |
| **ntext** | 0 |
| **bit** | 1 |
| **binary** | Twice the length defined for the column + 1 |
| **varbinary** | Twice the length defined for the column + 1 |
| **image** | 0 |
| **datetime** | 24 |
| **smalldatetime** | 24 |
| **float** | 30 |
| **real** | 30 |
| **int** | 12 |
| **bigint** | 19 |
| **smallint** | 7 |
| **tinyint** | 5 |
| **money** | 30 |
| **smallmoney** | 30 |
| **decimal** | 41 <sup>1</sup> |
| **numeric** | 41 <sup>1</sup> |
| **uniqueidentifier** | 37 |
| **timestamp** | 17 |
| **varchar(max)** | 0 |
| **varbinary(max)** | 0 |
| **nvarchar(max)** | 0 |
| **UDT** | Length of the user-defined term (UDT) column |
| **XML** | 0 |
| **vector** | N/A <sup>2</sup> |

<sup>1</sup> For more information about the **decimal** and **numeric** data types, see [Decimal and numeric (Transact-SQL)](../../t-sql/data-types/decimal-and-numeric-transact-sql.md).

<sup>2</sup> Conversion of **vector** data type to and from character isn't allowed.

> [!NOTE]  
> A column of type **tinyint** can have values from 0 through 255; the maximum number of characters that are needed to represent any number in that range is three (representing values 100 through 255).

### Default field lengths for native file storage

The following table lists the default field lengths for data to be stored as native file storage type. Nullable data is the same length as nonnull data, and character data is always stored in character format.

| Data type | Default length (characters) |
| --- | --- |
| **bit** | 1 |
| **binary** | Length defined for the column |
| **varbinary** | Length defined for the column |
| **image** | 0 |
| **datetime** | 8 |
| **smalldatetime** | 4 |
| **float** | 8 |
| **real** | 4 |
| **int** | 4 |
| **bigint** | 8 |
| **smallint** | 2 |
| **tinyint** | 1 |
| **money** | 8 |
| **smallmoney** | 4 |
| **decimal** | <sup>1</sup> |
| **numeric** | <sup>1</sup> |
| **uniqueidentifier** | 16 |
| **timestamp** | 8 |
| **vector** | 8-byte vector header + float array size |

<sup>1</sup> For more information about the **decimal** and **numeric** data types, see [decimal and numeric (Transact-SQL)](../../t-sql/data-types/decimal-and-numeric-transact-sql.md).

In all of the preceding cases, to create a data file for later reloading into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that keeps the storage space to a minimum, use a length prefix with the default file storage type and the default field length.

## Related content

- [bcp utility](../../tools/bcp/bcp-utility.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [Specify field and row terminators (SQL Server)](specify-field-and-row-terminators-sql-server.md)
- [Specify prefix length in data files using bcp (SQL Server)](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)
- [Specify file storage type using bcp (SQL Server)](specify-file-storage-type-by-using-bcp-sql-server.md)
- [Keep nulls or default values during bulk import (SQL Server)](keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)

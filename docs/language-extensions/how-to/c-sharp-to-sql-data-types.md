---
title: C# data types
titleSuffix: SQL Server Language Extensions
description: Map data types from C# .NET to SQL Server for input and output data structures, and for input parameters on the sp_execute_external_script.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: conceptual
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---
# C# .NET and SQL Server supported data types

[!INCLUDE [sqlserver2019-and-later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

This article maps SQL Server data types to .NET data types (used by C#) for data structures and parameters on [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

The following SQL and .NET data types are currently supported for input/output data sets and input/output parameters.

| SQL Server data type | .NET data type | Comment |
| --- | --- | --- |
| **bit** | `bool` | |
| **tinyint** | `byte` | |
| **smallint** | `short` | |
| **int** | `int` | |
| **real** | `float` | |
| **bigint** | `long` | |
| **float** | `double` | |
| **nchar(*n*)** | `string` | |
| **nvarchar(*n*)** | `string` | |
| **binary(*n*)** | `byte[]` | |
| **varbinary(*n*)** | `byte[]` | |
| **nvarchar(max)** | `string` | |
| **varbinary(max)** | `byte[]` | |
| **uniqueidentifier** | `Guid` | |
| **char(*n*)** | `string` | |
| **varchar(*n*)** | `string` | |
| **varchar(max)** | `string` | |
| **date** | `DateOnly` | .NET 6 and later versions |
| **time** | `TimeOnly` | .NET 6 and later versions |
| **numeric** | `decimal` | |
| **decimal** | `decimal` | |
| **money** | `decimal` | |
| **smallmoney** | `decimal` | |
| **smalldatetime** | `DateTime` | |
| **datetime** | `DateTime` | |
| **datetime2** | `DateTime` | |

## Related content

- [How to call the .NET runtime in SQL Server Language Extensions](call-c-sharp-from-sql.md)

---
title: Java data types
titleSuffix: SQL Server Language Extensions
description: Map data types from Java to SQL Server for input and output data structures, and for input parameters on the sp_execute_external_script.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: conceptual
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---
# Java and SQL Server supported data types

[!INCLUDE [sqlserver2019-and-later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

This article maps SQL Server data types to Java data types for data structures and parameters on [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

The following SQL and Java data types are currently supported for input/output data sets and input/output parameters.

| SQL Server data type | Java data type | Comment |
| --- | --- | --- |
| **bit** | `boolean` | |
| **tinyint** | `short` | |
| **smallint** | `short` | |
| **int** | `int` | |
| **real** | `float` | |
| **bigint** | `long` | |
| **float** | `double` | |
| **nchar(*n*)** | `String` | |
| **nvarchar(*n*)** | `String` | |
| **binary(*n*)** | `byte[]` | |
| **varbinary(*n*)** | `byte[]` | |
| **nvarchar(max)** | `String` | |
| **varbinary(max)** | `byte[]` | |
| **uniqueidentifier** | `String` | |
| **char(*n*)** | `String` | Only UTF-8 Strings supported |
| **varchar(*n*)** | `String` | Only UTF-8 Strings supported |
| **varchar(max)** | `String` | Only UTF-8 Strings supported |
| **date** | `java.sql.date` | |
| **numeric** | `java.math.BigDecimal` | |
| **decimal** | `java.math.BigDecimal` | |
| **money** | `java.math.BigDecimal` | |
| **smallmoney** | `java.math.BigDecimal` | |
| **smalldatetime** | `java.sql.timestamp` | |
| **datetime** | `java.sql.timestamp` | |
| **datetime2** | `java.sql.timestamp` | |

## Related content

- [How to call the Java runtime in SQL Server Language Extensions](call-java-from-sql.md)

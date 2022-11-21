---
title: Java data types
titleSuffix: SQL Server Language Extensions
description: Map data types from Java to SQL Server for input and output data structures, and for input parameters on the sp_execute_external_script.
author: rothja
ms.author: jroth 
ms.date: 11/05/2019
ms.topic: conceptual
ms.service: sql
ms.subservice: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---
# Java and SQL Server supported data types
[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

This article maps SQL Server data types to Java data types for data structures and parameters on [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

The following SQL and Java data types are currently supported for Input/Output data sets and Input/Output parameters.

| SQL data type        | Java data type | Comment |
| ------------- |-------------|-|
| Bit      | boolean | |
| Tinyint      | short      | |
| Smallint | short      | |
| Int | int      | |
| Real | float      | |
| Bigint | long      | |
| float | double      | |
| nchar(n) | String      | |
| nvarchar(n) | String      | |
| binary(n) | byte[]      | |
| varbinary(n) | byte[]      | |
| nvarchar(max) | String      | |
| varbinary(max) | byte[]      | |
| uniqueidentifier | String | |
| char(n) | String | Only UTF8 Strings supported |
| varchar(n) | String | Only UTF8 Strings supported |
| varchar(max) | String | Only UTF8 Strings supported |
| date | java.sql.date  | |
| numeric | java.math.BigDecimal  | |
| decimal | java.math.BigDecimal  | |
| money | java.math.BigDecimal  | |
| smallmoney | java.math.BigDecimal  | |
| smalldatetime | java.sql.timestamp  | |
| datetime | java.sql.timestamp  | |
| datetime2 | java.sql.timestamp  | |


## Next steps

+ [How to call Java in SQL Server](../how-to/call-java-from-sql.md)
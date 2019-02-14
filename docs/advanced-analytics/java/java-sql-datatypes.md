---
title: Java data types supported in SQL Server 2019 - SQL Server Machine Learning Services
description: Map data types from Java to SQL Server for input and output data structures, and for input parameters on the sp_execute_external_script.
ms.prod: sql
ms.technology: machine-learning

ms.date: 02/14/2019  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# Java and SQL Server supported data types

This article maps SQL Server data types to Java data types for data structures and parameters on [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql).

## Data types for data sets

The following SQL and Java data types are currently supported for Input and Output data sets.

| SQL data type        | Java data type | Comment | |
| ------------- |-------------|-|-|
| Bit      | boolean | | |
| Tinyint      | short      | | |
| Smallint | short      | | |
| Int | int      | | |
| Real | float      | | |
| Bigint | long      | | |
| float | double      | | |
| nchar(n) | String      | | |
| nvarchar(n) | String  | | |
| binary(n) | byte[]      | | |
| varbinary(n) | byte[]      | | |
| nvarchar(max) | String | | |
| varbinary(max) | byte[] | | |
| uniqueidentifier | String | | |
| char(n) | String | Only UTF8 Strings supported | |
| varchar(n) | String | Only UTF8 Strings supported | |
| varchar(max) | String | Only UTF8 Strings supported | |

## Data types for input parameters

The following SQL and Java data types are currently supported for input parameters.

| SQL data type        | Java data type | Comment | |
| ------------- |-------------|-|-|
| Bit      | boolean | | |
| Tinyint      | short      | | |
| Smallint | short      | | |
| Int | int      | | |
| Real | float      | | |
| Bigint | long      | | |
| float | double      | | |
| nchar(n) | String      | | |
| nvarchar(n) | String      | | |
| binary(n) | byte[]      | | |
| varbinary(n) | byte[]      | | |
| nvarchar(max) | String      | | |
| varbinary(max) | byte[]      | | |
| uniqueidentifier | String | | |
| char(n) | String | Only UTF8 Strings supported | |
| varchar(n) | String | Only UTF8 Strings supported | |
| varchar(max) | String | Only UTF8 Strings supported | |

## See also

+ [How to call Java in SQL Server](howto-call-java-from-sql.md)
+ [Java sample in SQL Server](java-first-sample.md)
+ [Java language extension in SQL Server](extension-java.md)
---
title: Java data types supported in SQL Server 2019 - SQL Server Machine Learning Services
description: Map data types from Java to SQL Server for input and output data structures, and for input parameters on the sp_execute_external_script.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/24/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# Java and SQL Server supported data types

This article maps SQL Server data types to Java data types for data structures and parameters on [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql).

## Data types for data sets

The following SQL and Java data types are currently supported for Input and Output data sets.

| SQL Type        | Java Type | | |
| ------------- |-------------|-|-|
| Bit      | boolean | | |
| Tinyint      | short      | | |
| Smallint | short      | | |
| Int | int      | | |
| Real | float      | | |
| Bigint | long      | | |
| float | double      | | |
| nchar(n) | String (unicode)      | | |
| nvarchar(n) | String (unicode)      | | |
| binary(n) | byte[]      | | |
| varbinary(n) | byte[]      | | |

## Data types for input parameters

The following SQL and Java data types are currently supported for input parameters.

| SQL Type        | Java Type | | |
| ------------- |-------------|-|-|
| Bit      | boolean | | |
| Tinyint      | short      | | |
| Smallint | short      | | |
| Int | int      | | |
| Real | float      | | |
| Bigint | long      | | |
| float | double      | | |
| nchar(n) | String (unicode)      | | |
| nvarchar(n) | String (unicode)      | | |
| binary(n) | byte[]      | | |
| varbinary(n) | byte[]      | | |
| nvarchar(max) | String (unicode)      | | |
| varbinary(max) | byte[]      | | |

## See also

+ [How to call Java in SQL Server](howto-call-java-from-sql.md)
+ [Java sample in SQL Server](java-first-sample.md)
+ [Java language extension in SQL Server](extension-java.md)
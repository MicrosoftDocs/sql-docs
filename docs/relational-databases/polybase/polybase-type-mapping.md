---
title: "Type mapping with PolyBase | Microsoft Docs"
ms.custom: ""
ms.date: "09/24/2018"
ms.prod: sql
ms.reviewer: ""
ms.suite: "sql"
ms.technology: polybase
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: rothja
ms.author: jroth
manager: craigg
---
# Type mapping with PolyBase

[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the mapping between PolyBase external data sources and SQL Server. You can use this information to correctly define external tables with the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) Transact-SQL command.

## Overview

When creating and external table with PolyBase, the column definitions, including the data types and number of columns, must match the data in the external files. If there is a mismatch, the file rows is rejected when querying the actual data.  
  
For external tables that reference files in external data sources, the column and type definitions must map to the exact schema of the external file. When defining data types that reference data stored in Hadoop/Hive, use the following mappings between SQL and Hive data types and cast the type into a SQL data type when selecting from it. The types include all versions of Hive unless stated otherwise.

> [!NOTE]  
> SQL Server does not support the Hive *infinity* data value in any conversion. PolyBase will fail with a data type conversion error.

## Type mapping reference

| SQL Data Type | .NET Data Type            | Hive Data Type | Hadoop/Java Data Type | Comments                       |
| ------------- | ------------------------- | -------------- | --------------------- | ------------------------------ |
| tinyint       | Byte                      | tinyint        | ByteWritable          | For unsigned numbers only.     |
| smallint      | Int16                     | smallint       | ShortWritable         |
| int           | Int32                     | int            | IntWritable           |
| bigint        | Int64                     | bigint         | LongWritable          |
| bit           | Boolean                   | boolean        | BooleanWritable       |
| float         | Double                    | double         | DoubleWritable        |
| real          | Single                    | float          | FloatWritable         |
| money         | Decimal                   | double         | DoubleWritable        |
| smallmoney    | Decimal                   | double         | DoubleWritable        |
| nchar         | String<br /><br /> Char[] | string         | text                  |
| nvarchar      | String<br /><br /> Char[] | string         | Text                  |
| char          | String<br /><br /> Char[] | string         | Text                  |
| varchar       | String<br /><br /> Char[] | string         | Text                  |
| binary        | Byte[]                    | binary         | BytesWritable         | Applies to Hive 0.8 and later. |
| varbinary     | Byte[]                    | binary         | BytesWritable         | Applies to Hive 0.8 and later. |
| date          | DateTime                  | timestamp      | TimestampWritable     |
| smalldatetime | DateTime                  | timestamp      | TimestampWritable     |
| datetime2     | DateTime                  | timestamp      | TimestampWritable     |
| datetime      | DateTime                  | timestamp      | TimestampWritable     |
| time          | TimeSpan                  | timestamp      | TimestampWritable     |
| decimal       | Decimal                   | decimal        | BigDecimalWritable    | Applies to Hive0.11 and later. |

## Next steps

For more information on how this is used, see Transact-SQL reference article for [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).

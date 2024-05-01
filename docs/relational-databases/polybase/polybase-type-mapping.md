---
title: "Type mapping with PolyBase"
description: Refer to these tables for mapping between PolyBase external data sources and SQL Server. Define external tables with Transact-SQL CREATE EXTERNAL TABLE.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei, nathansc
ms.date: 12/06/2023
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
---
# Type mapping with PolyBase

[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the mapping between PolyBase external data sources and SQL Server. You can use this information to correctly define external tables with the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) Transact-SQL command.

## Overview

When you are creating an external table with PolyBase, the column definitions, including the data types and number of columns, must match the data in the external files. If there's a mismatch, the file rows are rejected when querying the actual data.

For external tables that reference files in external data sources, the column and type definitions must map to the exact schema of the external file. When defining data types that reference data stored in Hadoop/Hive, use the following mappings between SQL and Hive data types and cast the type into a SQL data type when selecting from it. The types include all versions of Hive unless stated otherwise.

> [!NOTE]  
> SQL Server does not support the Hive *infinity* data value in any conversion. PolyBase will fail with a data type conversion error.

## Hadoop Type mapping reference

| SQL Data Type | .NET Data Type            | Hive Data Type | Hadoop/Java Data Type<sup>1</sup> | Comments                       |
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
| nchar         | String<br /><br /> Char[] | string         | Varchar               |
| nvarchar      | String<br /><br /> Char[] | string         | Varchar               |
| char          | String<br /><br /> Char[] | string         | Varchar               |
| varchar       | String<br /><br /> Char[] | string         | Varchar               |
| binary        | Byte[]                    | binary         | BytesWritable         | Applies to Hive 0.8 and later. |
| varbinary     | Byte[]                    | binary         | BytesWritable         | Applies to Hive 0.8 and later. |
| date          | DateTime                  | timestamp      | TimestampWritable     |
| smalldatetime | DateTime                  | timestamp      | TimestampWritable     |
| datetime2     | DateTime                  | timestamp      | TimestampWritable     |
| datetime      | DateTime                  | timestamp      | TimestampWritable     |
| time          | TimeSpan                  | timestamp      | TimestampWritable     |
| decimal       | Decimal                   | decimal        | BigDecimalWritable    | Applies to Hive0.11 and later. |

<sup>1</sup> Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] Hadoop is no longer supported.

## Parquet and Delta Type mapping reference

Parquet and Delta external table type mapping to SQL Server datatypes are listed below. 

Parquet and Delta Lake files contain type descriptions for every column. The following table describes how Parquet types are mapped to SQL native types.

<!-- If updating, see also azure-docs /azure/synapse-analytics/sql/develop-openrowset#type-mapping-for-parquet.md -->

| Parquet type | Parquet logical type (annotation) | SQL data type |
| --- | --- | --- |
| BOOLEAN | | bit |
| BINARY / BYTE_ARRAY | | varbinary |
| DOUBLE | | float |
| FLOAT | | real |
| INT32 | | int |
| INT64 | | bigint |
| INT96 | |datetime2 |
| FIXED_LEN_BYTE_ARRAY | |binary |
| BINARY |UTF8 |varchar \*(UTF8 collation) |
| BINARY |STRING |varchar \*(UTF8 collation) |
| BINARY |ENUM|varchar \*(UTF8 collation) |
| FIXED_LEN_BYTE_ARRAY |UUID |uniqueidentifier |
| BINARY |DECIMAL |decimal |
| BINARY |JSON |varchar(8000) \*(UTF8 collation) |
| BINARY |BSON | Not supported |
| FIXED_LEN_BYTE_ARRAY |DECIMAL |decimal |
| BYTE_ARRAY |INTERVAL | Not supported |
| INT32 |INT(8, true) |smallint |
| INT32 |INT(16, true) |smallint |
| INT32 |INT(32, true) |int |
| INT32 |INT(8, false) |tinyint |
| INT32 |INT(16, false) |int |
| INT32 |INT(32, false) |bigint |
| INT32 |DATE |date |
| INT32 |DECIMAL |decimal |
| INT32 |TIME (MILLIS)|time |
| INT64 |INT(64, true) |bigint |
| INT64 |INT(64, false) |decimal(20,0) |
| INT64 |DECIMAL |decimal |
| INT64 |TIME (MICROS) | time |
| INT64 |TIME (NANOS) | Not supported |
| INT64 |TIMESTAMP ([normalized to utc](https://github.com/apache/parquet-format/blob/master/LogicalTypes.md#instant-semantics-timestamps-normalized-to-utc)) (MILLIS / MICROS) | datetime2 |
| INT64 |TIMESTAMP ([not normalized to utc](https://github.com/apache/parquet-format/blob/master/LogicalTypes.md#local-semantics-timestamps-not-normalized-to-utc)) (MILLIS / MICROS) | bigint - make sure that you explicitly adjust `bigint` value with the timezone offset before converting it to a datetime value. |
| INT64 |TIMESTAMP (NANOS) | Not supported |
|[Complex type](https://github.com/apache/parquet-format/blob/master/LogicalTypes.md#lists) |LIST |varchar(8000), serialized into JSON |
|[Complex type](https://github.com/apache/parquet-format/blob/master/LogicalTypes.md#maps)|MAP|varchar(8000), serialized into JSON |

<!--SQL Server 2019-->
::: moniker range=">= sql-server-ver15 "

## Oracle Type mapping reference

| Oracle data type | SQL Server type |  
| -------------    | --------------- |
|Float             |Float            |
|NUMBER            |Float            |
|NUMBER (p,s)      |Decimal (p, s)   |
|LONG              |Nvarchar         |
|BINARY_FLOAT      |Real             |  
|BINARY_DOUBLE     |Float            |  
|CHAR              |Char             |
|VARCHAR2          |Varchar          |  
|NVARCHAR2         |Nvarchar         |  
|RAW               |Varbinary        |
|LONG RAW          |Varbinary        |  
|BLOB              |Varbinary        |
|CLOB              |Varchar          |
|NCLOB             |Nvarchar         |  
|ROWID             |Varchar          |
|UROWID            |Varchar          |  
|DATE              |Datetime2        |
|TIMESTAMP         |Datetime2        |

**Type mismatch**

**Float:**
Oracle supports floating point precision of 126, which is lower than what SQL Server supports (53). Therefore, **Float (1-53)** can be mapped directly, but beyond that, there is data loss due to truncation.

**Timestamp:**  
Timestamp and Timestamp with local timezone in Oracle supports 9 fractional seconds precision whereas, SQL Server DateTime2 supports only 7 fractional seconds precision.


## MongoDB Type Mapping

| BSON data type     | SQL Server type |
| ------------------ | --------------- |
| Double             | Float           |
| String             | Nvarchar        |
| Binary data        | Nvarchar        |
| Object ID          | Nvarchar        |
| Boolean            | Bit             |
| Date               | Datetime2       |
| 32-bit integer     | Int             |
| Timestamp          | Nvarchar        |
| 64-bit integer     | BigInt          |
| Decimal 128        | Decimal         |  
| DBPointer          | Nvarchar        |
| JavaScript         | Nvarchar        |
| Max Key            | Nvarchar        |
| Min Key            | Nvarchar        |
| Symbol             | Nvarchar        |
| Regular Expression | Nvarchar        |
| Undefined/NULL     | Nvarchar        |

MongoDB uses BSON documents to store data records. Unlike the previous scenarios, BSON is schema-less and supports embedding of documents and arrays within other documents. This provides flexibility to the user.

## Teradata Type mapping reference

| Teradata data type | SQL Server type |  
| -------------      | -------------   |
|INTEGER             |Int              |
|SMALLINT            |SmallInt         |
|BIGINT              |BigInt           |
|BYTEINT             |SmallInt         |
|DECIMAL             |Decimal          |
|FLOAT               |Decimal          |
|BYTE                |Binary           |
|VARBYTE             |Varbinary        |
|BLOB                |varbinary        |
|CHAR                |Nchar            |
|CLOB                |Nvarchar         |
|VARCHAR             |Nvarchar         |
|Graphic             |Nchar            |
|JSON                |Nvarchar         |
|VARGRAPHIC          |Nvarchar         |
|DATE                |Date             |
|TIMESTAMP           |Datetime2        |
|TIME                |Time             |
|TIME WITH TIME ZONE |Time             |
|TIMESTAMP WITH TIME ZONE|Time         |

::: moniker-end

## Next steps

For more information on how this is used, see Transact-SQL reference article for [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).

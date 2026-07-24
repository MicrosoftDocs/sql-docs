---
title: "Type Mapping with PolyBase"
description: Refer to these tables for mapping between PolyBase external data sources and SQL Server. Define external tables with Transact-SQL CREATE EXTERNAL TABLE.
author: markingmyname
ms.author: maghan
ms.reviewer: hudequei, nathansc, randolphwest
ms.date: 12/03/2025
ms.service: sql
ms.subservice: polybase
ms.topic: concept-article
---
# Type mapping with PolyBase

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

This article describes the mapping between PolyBase external data sources and SQL Server. You can use this information to correctly define external tables with the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) Transact-SQL command.

## Overview

When you create an external table with PolyBase, the column definitions, including the data types and number of columns, must match the data in the external files. If there's a mismatch, the file rows are rejected when querying the actual data.

For external tables that reference files in external data sources, the column and type definitions must map to the exact schema of the external file. When defining data types that reference data stored in Hadoop/Hive, use the following mappings between SQL and Hive data types and cast the type into a SQL data type when selecting from it. The types include all versions of Hive unless stated otherwise.

> [!NOTE]  
> SQL Server doesn't support the Hive *infinity* data value in any conversion. PolyBase fails with a data type conversion error.

## Hadoop Type mapping reference

| SQL data type | .NET data type | Hive data type | Hadoop/Java data type <sup>1</sup> | Comments |
| --- | --- | --- | --- | --- |
| **tinyint** | `Byte` | `tinyint` | `ByteWritable` | For unsigned numbers only. |
| **smallint** | `Int16` | `smallint` | `ShortWritable` | |
| **int** | `Int32` | `int` | `IntWritable` | |
| **bigint** | `Int64` | `bigint` | `LongWritable` | |
| **bit** | `Boolean` | `boolean` | `BooleanWritable` | |
| **float** | `Double` | `double` | `DoubleWritable` | |
| **real** | `Single` | `float` | `FloatWritable` | |
| **money** | `Decimal` | `double` | `DoubleWritable` | |
| **smallmoney** | `Decimal` | `double` | `DoubleWritable` | |
| **nchar** | `String`<br />`Char[]` | `string` | `Varchar` | |
| **nvarchar** | `String`<br />`Char[]` | `string` | `Varchar` | |
| **char** | `String`<br />`Char[]` | `string` | `Varchar` | |
| **varchar** | `String`<br />`Char[]` | `string` | `Varchar` | |
| **binary** | `Byte[]` | `binary` | `BytesWritable` | Applies to Hive 0.8 and later versions. |
| **varbinary** | `Byte[]` | `binary` | `BytesWritable` | Applies to Hive 0.8 and later versions. |
| **date** | `DateTime` | `timestamp` | `TimestampWritable` | |
| **smalldatetime** | `DateTime` | `timestamp` | `TimestampWritable` | |
| **datetime2** | `DateTime` | `timestamp` | `TimestampWritable` | |
| **datetime** | `DateTime` | `timestamp` | `TimestampWritable` | |
| **time** | `TimeSpan` | `timestamp` | `TimestampWritable` | |
| **decimal** | `Decimal` | `decimal` | `BigDecimalWritable` | Applies to Hive 0.11 and later versions. |

<sup>1</sup> Hadoop is no longer supported in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

## Parquet and Delta type mapping reference

Parquet and Delta external table type mapping to SQL Server data types are listed in this section.

Parquet and Delta Lake files contain type descriptions for every column. The following table describes how Parquet types are mapped to SQL native types.

<!-- If updating, see also azure-docs /azure/synapse-analytics/sql/develop-openrowset#type-mapping-for-parquet.md -->

| Parquet type | Parquet logical type (annotation) | SQL data type |
| --- | --- | --- |
| `BOOLEAN` | | **bit** |
| `BINARY / BYTE_ARRAY` | | **varbinary** |
| `DOUBLE` | | **float** |
| `FLOAT` | | **real** |
| `INT32` | | **int** |
| `INT64` | | **bigint** |
| `INT96` | | **datetime2** |
| `FIXED_LEN_BYTE_ARRAY` | | **binary** |
| `BINARY` | `UTF8` | **varchar** <sup>1</sup> |
| `BINARY` | `STRING` | **varchar** <sup>1</sup> |
| `BINARY` | `ENUM` | **varchar** <sup>1</sup> |
| `FIXED_LEN_BYTE_ARRAY` | `UUID` | uniqueidentifier |
| `BINARY` | `DECIMAL` | **decimal** |
| `BINARY` | `JSON` | **varchar(8000)** <sup>1</sup> |
| `BINARY` | `BSON` | Not supported |
| `FIXED_LEN_BYTE_ARRAY` | `DECIMAL` | **decimal** |
| `BYTE_ARRAY` | `INTERVAL` | Not supported |
| `INT32` | `INT(8, true)` | **smallint** |
| `INT32` | `INT(16, true)` | **smallint** |
| `INT32` | `INT(32, true)` | **int** |
| `INT32` | `INT(8, false)` | **tinyint** |
| `INT32` | `INT(16, false)` | **int** |
| `INT32` | `INT(32, false)` | **bigint** |
| `INT32` | `DATE` | **date** |
| `INT32` | `DECIMAL` | **decimal** |
| `INT32` | `TIME` (`MILLIS`) | **time** |
| `INT64` | `INT(64, true)` | **bigint** |
| `INT64` | `INT(64, false)` | **decimal(20,0)** |
| `INT64` | `DECIMAL` | **decimal** |
| `INT64` | `TIME` (`MICROS`) | **time** |
| `INT64` | `TIME` (`NANOS`) | Not supported |
| `INT64` | `TIMESTAMP` ([normalized to UTC](https://github.com/apache/parquet-format/blob/master/LogicalTypes.md#instant-semantics-timestamps-normalized-to-utc)) (MILLIS / MICROS) | **datetime2** |
| `INT64` | `TIMESTAMP` ([not normalized to UTC](https://github.com/apache/parquet-format/blob/master/LogicalTypes.md#local-semantics-timestamps-not-normalized-to-utc)) (MILLIS / MICROS) | bigint - make sure that you explicitly adjust `bigint` value with the timezone offset before converting it to a datetime value. |
| `INT64` | `TIMESTAMP` (`NANOS`) | Not supported |
| [Complex type](https://github.com/apache/parquet-format/blob/master/LogicalTypes.md#lists) | LIST | varchar(8000), serialized into JSON |
| [Complex type](https://github.com/apache/parquet-format/blob/master/LogicalTypes.md#maps) | MAP | varchar(8000), serialized into JSON |

<sup>1</sup> UTF-8 collation.

<!--SQL Server 2019-->
::: moniker range=">=sql-server-ver15"

## Oracle Type mapping reference

| Oracle data type | SQL Server data type |
| --- | --- |
| `FLOAT` | **float** |
| `NUMBER` | **float** |
| `NUMBER (p,s)` | **decimal(*p*, *s*)** |
| `LONG` | **nvarchar** |
| `BINARY_FLOAT` | **real** |
| `BINARY_DOUBLE` | **float** |
| `CHAR` | **char** |
| `VARCHAR2` | **varchar** |
| `NVARCHAR2` | **nvarchar** |
| `RAW` | **varbinary** |
| `LONG RAW` | **varbinary** |
| `BLOB` | **varbinary** |
| `CLOB` | **varchar** |
| `NCLOB` | **nvarchar** |
| `ROWID` | **varchar** |
| `UROWID` | **varchar** |
| `DATE` | **datetime2** |
| `TIMESTAMP` | **datetime2** |

### Type mismatch

#### Float

Oracle supports floating point precision of 126, which is lower than what SQL Server supports (53). Therefore, **Float (1-53)** can be mapped directly, but beyond that, there's data loss due to truncation.

#### Timestamp  

Timestamp and Timestamp with local timezone in Oracle supports 9 fractional seconds precision whereas, SQL Server DateTime2 supports only 7 fractional seconds precision.

## MongoDB type mapping

| BSON data type | SQL Server data type |
| --- | --- |
| Double | **float** |
| String | **nvarchar** |
| Binary data | **nvarchar** |
| Object ID | **nvarchar** |
| Boolean | **bit** |
| Date | **datetime2** |
| 32-bit integer | **int** |
| Timestamp | **nvarchar** |
| 64-bit integer | **bigint** |
| Decimal 128 | **decimal** |
| DBPointer | **nvarchar** |
| JavaScript | **nvarchar** |
| Max Key | **nvarchar** |
| Min Key | **nvarchar** |
| Symbol | **nvarchar** |
| Regular Expression | **nvarchar** |
| Undefined/NULL | **nvarchar** |

MongoDB uses BSON documents to store data records. Unlike the previous scenarios, BSON is schema-less and supports embedding of documents and arrays within other documents. This provides flexibility to the user.

## Teradata type mapping reference

| Teradata data type | SQL Server data type |
| --- | --- |
| `INTEGER` | **int** |
| `SMALLINT` | **smallint** |
| `BIGINT` | **bigint** |
| `BYTEINT` | **smallint** |
| `DECIMAL` | **decimal** |
| `FLOAT` | **decimal** |
| `BYTE` | **binary** |
| `VARBYTE` | **varbinary** |
| `BLOB` | **varbinary** |
| `CHAR` | **nchar** |
| `CLOB` | **nvarchar** |
| `VARCHAR` | **nvarchar** |
| `Graphic` | **nchar** |
| `JSON` | **nvarchar** |
| `VARGRAPHIC` | **nvarchar** |
| `DATE` | **date** |
| `TIMESTAMP` | **datetime2** |
| `TIME` | **time** |
| `TIME WITH TIME ZONE` | **time** |
| `TIMESTAMP WITH TIME ZONE` | **time** |

::: moniker-end

## Related content

- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)

 ---
title: "Type mapping with PolyBase | Microsoft Docs"
ms.date: 09/24/2018
ms.prod: sql
ms.reviewer: ""
ms.technology: polybase
ms.topic: conceptual
author: rothja
ms.author: jroth
manager: craigg
---
# Type mapping with PolyBase

[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the mapping between PolyBase external data sources and SQL Server. You can use this information to correctly define external tables with the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) Transact-SQL command.

## Overview

When creating and external table with PolyBase, the column definitions, including the data types and number of columns, must match the data in the external files. If there is a mismatch, the file rows are rejected when querying the actual data.  
  
For external tables that reference files in external data sources, the column and type definitions must map to the exact schema of the external file. When defining data types that reference data stored in Hadoop/Hive, use the following mappings between SQL and Hive data types and cast the type into a SQL data type when selecting from it. The types include all versions of Hive unless stated otherwise.

> [!NOTE]  
> SQL Server does not support the Hive *infinity* data value in any conversion. PolyBase will fail with a data type conversion error.

## Hadoop Type mapping reference

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

<!--SQL Server 2019-->
::: moniker range=">= sql-server-ver15 || =sqlallproducts-allversions"

## Oracle Type mapping reference

| Oracle data type | SQL Server type | 
| -------------    | --------------- |
|Float             |Float            |
|NUMBER            |Decimal          |
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
|NCLOB             | Nvarchar        | 
|ROWID             |Varchar          |
|UROWID            |Varchar          | 
|DATE              |Datetime2        |
|TIMESTAMP         |Datetime2        | 

**Type mismatch** 

**Float:**
Oracle supports floating point precision of 126, which is lower than what SQL server supports (53). Therefore, **Float (1-53)** can be mapped directly, but beyond that, there is data loss due to truncation.

**Timestamp:** 
Timestamp and Timestamp with local timezone in Oracle supports 9 fractional seconds precision whereas, SQL server DateTime2 supports only 7 fractional seconds precision. 




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
|Decimal 128         | Decimal         | 
| DBPointer          | Nvarchar        |
| Javascript         | Nvarchar        |
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


## Next steps

For more information on how this is used, see Transact-SQL reference article for [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).

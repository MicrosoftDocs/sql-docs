---
title: "Type mapping with PolyBase | Microsoft Docs"
ms.custom: ""
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
| nchar         | String<br /><br /> Char[] | string         | --text                  |
| nvarchar      | String<br /><br /> Char[] | string         | --Text                  |
| char          | String<br /><br /> Char[] | string         | --Text                  |
| varchar       | String<br /><br /> Char[] | string         | --Text                  |
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
| ------------- | ------------------------- |
|Float |Float|
|Decimal|Decimal|
|Int |Int|
|Long |--Ntext|
|Binary Float|Real| 
|Char |Nchar|
|Varchar2|Nvarchar| 
|Raw |Varbinary|
|Long Raw|--Image| 
|Bfile |--Image|
|Blob |--Image--|
|Clob | --Image-- |
|Rowid |Varchar|
|Date |Datetime2|
|Timestamp|Datetime2| 
|Timestamp with local timezone|Datetime2| 

**Type mismatch:** 

Float:
Oracle supports floating point precision of 126, which is considerably lower than what SQL server supports (53). Thus, Float (1-53) can be mapped directly and beyond that, we will experience data loss/truncation. 

Character types: 
For types, such as Long and Bfile, Oracle supports data type size of 4GB. Wherease, SQL server supports only 2GB. We should expect data truncation. Similarly, for oracle data types Blob and clob, oracle can support upto 128 TB, whereas SQL server LONGVARBINARY or WLONGVARBINARY cannot support more than 2GB.  

Timestamp: 
Timestamp and Timestamp with local timezone in Oracle supports 9 fractional seconds precision whereas, SQL server DateTime2 supports only 7 fractional seconds precision. 

Driver Configuration option:
Default buffer size for Long/LOB columns (in Kb): 1024 – Configurable. 

Timestamp:
Simba maps Date, Timestamp and Timestamp with local zone to ODBC type SQL_TIMESTAMP and eventually SQL Server type Timestamp. Timestamp is an automatically generated unique row id. Ideally, SQL_TIMESTAMP should map to SQL server type DateTime/DateTime2 or Oracle types Date, Timestamp and Timestamp with local zone should map to ODBC type SQL_TYPE_TIMESTAMP. 

### Type mismatch (Float)

Oracle supports floating point precision of 126, which is lower than what SQL server supports (53). Therefore, **Float (1-53)** can be mapped directly, but beyond that, there is data loss due to truncation.

### Type mismatch (Character types)

For types, such as **Long** and **Bfile**, Oracle supports a data type size of 4 GB. SQL server supports a size of 2 GB. Similarly, for oracle data types **Blob** and **clob**, Oracle can support up to 128 TB, whereas SQL server **LONGVARBINARY** or **WLONGVARBINARY** cannot support more than 2 GB. Mapping types with a data type size larger than 2 GB may result in data truncation.  

### Type mismatch (Timestamp)

| BSON data type | SQL Server type         | 
| ------------- | ------------------------- |
|Double |Float|
|String|Nvarchar|
|Object||
|Array ||
|Binary data|Nvarchar| 
|Object ID|Nvarchar|
|Boolean|Bit| 
|Date |Datetime2|
|32-bit integer|Int| 
|Timestamp |Nvarchar|
|64-bit integer |BigInt|
|DBPointer|Nvarchar|
|Javascript|Nvarchar|
|Max Key|Nvarchar|
|Min Key|Nvarchar|
|Symbol|Nvarchar|
|Regular Expression|Nvarchar|
|Undefined/NULL|Nvarchar|

**Javascript (with scope):** 

The driver returns the value as a string containing “Unsupported Javascript with scope”.

**Type mismatch - string:**

MongoDB strings are converted into SQL_WVARCHAR type with a default column size of 255. This column size is tunable as a part of the driver configuration. Despite the ability to configure, SQL_WVARCHAR or SQL Server type Varchar supports only 2GB max and MongoDB can hold data upto 4GB. This might lead to truncation.

**MongoDB driver options:*
- Enable double buffering - Selected by default
- Documents to fetch per block - Default 4096
- Expose strings as SQL_WVARCHAR – selected by default. If unselected, strings are exposed as SQL_VARCHAR.
- String column size – Default 255
- Expose Binary as SQL_LONGVARBINARY – Default selected. If unselected, Binary is exposed as SQL_VARBINARY. 
- Binary Column Size – Default 32767.
- Enable passdown – Default selected.

**Schema related driver configurations:**

- LoadMetadataTable - default from database. Can ask the driver to load the schema definition from JSON file specified.
- LocalMetadataFile - If we are reading from file, full path must be specified here.
- Sampling method –  
     - default forward (Driver samples data from first record)
     - backword - driver samples data starting from last record
- SamplingLimit - default 100 (Max no of records the driver can sample to generate temp schema def). When set to 0, driver samples every document
- SamplingStepSize - default 1 (Interval at which driver samples records when scanning database to generate temp schema def). E.g., when set to 2, it samples every second record in database

| BSON data type     | SQL Server type |
| ------------------ | --------------- |
| Double             | Float           |
| String             | Nvarchar        |
| Object             |
| Array              |
| Binary data        | Nvarchar        |
| Object ID          | Nvarchar        |
| Boolean            | Bit             |
| Date               | Datetime2       |
| 32-bit integer     | Int             |
| Timestamp          | Nvarchar        |
| 64-bit integer     | BigInt          |
| DBPointer          | Nvarchar        |
| Javascript         | Nvarchar        |
| Max Key            | Nvarchar        |
| Min Key            | Nvarchar        |
| Symbol             | Nvarchar        |
| Regular Expression | Nvarchar        |
| Undefined/NULL     | Nvarchar        |

### Javascript (with scope)

The driver returns the value as a string containing "Unsupported Javascript with scope".

### Type mismatch (string)

MongoDB strings are converted into **SQL_WVARCHAR** type with a default column size of 255. This column size is tunable as a part of the driver configuration. Despite the ability to configure, **SQL_WVARCHAR** or SQL Server type **Varchar** supports only 2 GB max, whereas MongoDB can hold data up to 4 GB. This might lead to data truncation.

### MongoDB driver options

- **Enable double buffering**: Selected by default.
- **Documents to fetch per block**: Default 4096.
- **Expose strings as SQL_WVARCHAR**: selected by default. If unselected, strings are exposed as SQL_VARCHAR.
- **String column size**: Default 255.
- **Expose Binary as SQL_LONGVARBINARY**: Default selected. If unselected, Binary is exposed as SQL_VARBINARY.
- **Binary Column Size**: Default 32767.
- **Enable passdown**: Default selected.

### Schema-related driver configurations

- **LoadMetadataTable**: Default from database. Asks the driver to load the schema definition from JSON file specified.
- **LocalMetadataFile**: If reading from a file, the full path must be specified.
- **Sampling method**:  
  - **default forward**: Driver samples data from first record.
  - **backward**: Driver samples data starting from last record.
- **SamplingLimit**: Default 100 (Max number of records the driver can sample to generate temp schema definition). When set to 0, driver samples every document.
- **SamplingStepSize**: Default 1 (Interval at which driver samples records when scanning database to generate temp schema definition). For example, when set to 2, it samples every other record in the database.

## Teradata Type mapping reference

| Teradata data type | SQL Server type | 
| ------------- | ------------------------- |
|Int|Int|
|Small Int|SmallInt|
|Big Int|BigInt|
|Byte Int |TinyInt|
|Decimal|Decimal|
|Float|Float|
|Byte|Binary|
|Varbyte|Varbinary|
|Blob|--Image|
|Char|Nchar|
|Clob|--Ntext|
|Varchar|Nvarchar|
|Graphic|Nchar|
|JSON|--Ntext|
|Vargraphic|Nvarchar|
|Date|Date|
|Time|Time|
|Time with Time Zone|Time|
|Timestamp|Datetime2|
|Interval Year||
|Interval year to month||
|Interval Month||
|Interval Day||
|Interval Day to Hour||
|Interval Day to Minute||
|Interval Day to Second||
|Interval Hour||
|Interval Hour to Minute||
|Interval Hour to Second||
|Interval Minute||
|Interval Minute to Second||
|Interval Second||
|Period (DATE)||
|Period (TIME) ||
|Period (TIME with Timezone)||
|Period (Timestamp)||
|Period (Timestamp with Timezone)||

**Period data type:**

Period data type represents a duration marked by a beginning bound and ending bound. Essentially, it is a tuple. There is no SQL server equivalent type for period. 

**Time with Timezone and Timestamp:**

Time with time zone and timestamp contains the timezone offset, which is lost during translation. This can be fixed if we map SQL_Type_Time/SQL_Type_Timestamp to datetimeoffset instead of Time/DateTime2. 

**ByteInt:**

Simba maps Teradata type ByteInt that can hold values between 0-255 into TinyInt that can hold values between -127 to 127. We might expect truncation here.  

**CLOB:**

CLOB data type with LATIN charset should be able to accept 2097088000 characters. However, beyond 1073741823, the buffer size becomes negative.  

**Driver configuration options:**

- Use DATE data for TIMESTAMP parameters
- Enable custom catalog mode for 2.X applications
- Return Empty string in CREATE_PARAMS column for SQL_TIMESTAMP
- Return Max. CHAR/VARCHAR length as 32K

## Next steps

For more information on how this is used, see Transact-SQL reference article for [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).

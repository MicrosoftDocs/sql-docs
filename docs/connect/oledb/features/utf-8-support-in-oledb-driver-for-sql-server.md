---
title: "UTF-8 Support in OLE DB Driver for SQL Server| Microsoft Docs"
description: "UTF-8 Support in OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: 03/27/2019
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: reference
author: v-kaywon
ms.author: v-kaywon
---
# UTF-8 Support in OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

Microsoft OLE DB Driver for SQL Server (version 18.2.1) adds support for the UTF-8 server encoding. For information about the SQL Server UTF-8 support, see:
- [Collation and Unicode Support](../../../relational-databases/collations/collation-and-unicode-support.md)
- [UTF-8 support](#ctp23)

## Data insertion into a UTF-8 encoded CHAR or VARCHAR column
When creating an input parameter buffer for insertion, the buffer is described by using an array of [DBBINDING structures](https://go.microsoft.com/fwlink/?linkid=2071182). Each DBBINDING structure associates a single parameter to the consumer's buffer and contains information such as the length and type of the data value. For an input parameter buffer of type CHAR, the *wType* of the DBBINDING structure should be set to DBTYPE_STR. For an input parameter buffer of type WCHAR, the *wType* of the DBBINDING structure should be set to DBTYPE_WSTR.

When executing a command with parameters, the driver constructs parameter data type information. If the input buffer type and the parameter data type match, no conversion is done in the driver. Otherwise, the driver converts the input parameter buffer to the parameter data type. The parameter data type can be set explicitly by the user by calling [ICommandWithParameters::SetParameterInfo](https://go.microsoft.com/fwlink/?linkid=2071577). If the information isn't supplied, the driver derives the parameter data type information by (a) retrieving column metadata from the server when the statement is prepared, or (b) attempting a default conversion from the input parameter data type.

The input parameter buffer may be converted to the server column collation by the driver or by the server depending on the input buffer data type and the parameter data type. During the conversion, data loss may occur if the client code page or the database collation code page can't represent all the characters in the input buffer. The following table describes the conversion process when inserting data into a UTF-8 enabled column:

|Buffer data type|Parameter data type|Conversion|User precaution|
|---             |---                |---       |---            |
|DBTYPE_STR|DBTYPE_STR|Server conversion from client code page to database collation code page; server conversion from database collation code page to column collation code page.|Ensure the client code page and the database collation code page can represent all characters in the input data. For example, to insert a Polish character, the client code page could be set to 1250 (ANSI Central European), and the database collation could use Polish as the collation designator (for example, Polish_100_CI_AS_SC) or be UTF-8 enabled.|
|DBTYPE_STR|DBTYPE_WSTR|Driver conversion from client code page to UTF-16 encoding; server conversion from UTF-16 encoding to column collation code page.|Ensure the client code page can represent all characters in the input data. For example, to insert a Polish character, the client code page could be set to 1250 (ANSI Central European).|
|DBTYPE_WSTR|DBTYPE_STR|Driver conversion from UTF-16 encoding to database collation code page; server conversion from database collation code page to column collation code page.|Ensure the database collation code page can represent all characters in the input data. For example, to insert a Polish character, the database collation code page could use Polish as the collation designator (for example, Polish_100_CI_AS_SC) or be UTF-8 enabled.|
|DBTYPE_WSTR|DBTYPE_WSTR|Server conversion from UTF-16 to column collation code page.|None.|

## Data retrieval from a UTF-8 encoded CHAR or VARCHAR column
When creating a buffer for retrieved data, the buffer is described by using an array of [DBBINDING structures](https://go.microsoft.com/fwlink/?linkid=2071182). Each DBBINDING structure associates a single column in the retrieved row. To retrieve the column data as CHAR, set the *wType* of the DBBINDING structure to DBTYPE_STR. To retrieve the column data as WCHAR, set the *wType* of the DBBINDING structure to DBTYPE_WSTR.

For the result buffer type indicator DBTYPE_STR, the driver converts the UTF-8 encoded data to the client encoding. The user should make sure the client encoding can represent the data from the UTF-8 column, otherwise, data loss may occur.

For the result buffer type indicator DBTYPE_WSTR, the driver converts the UTF-8 encoded data to the UTF-16 encoding.

<a name="ctp23"></a>

### UTF-8 support (SQL Server 2019 CTP 2.3)

[!INCLUDE[ss2019](../../../includes/sssqlv15-md.md)] introduces full support for the widely used UTF-8 character encoding as an import or export encoding, or as database-level or column-level collation for text data. UTF-8 is allowed in the `CHAR` and `VARCHAR` datatypes, and is enabled when creating or changing an object's collation to a collation with the `UTF8` suffix.

For example,`LATIN1_GENERAL_100_CI_AS_SC` to `LATIN1_GENERAL_100_CI_AS_SC_UTF8`. UTF-8 is only available to Windows collations that support supplementary characters, as introduced in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]. `NCHAR` and `NVARCHAR` allow UTF-16 encoding only, and remain unchanged.

This feature may provide significant storage savings, depending on the character set in use. For example, changing an existing column data type with ASCII (Latin) strings from `NCHAR(10)` to `CHAR(10)` using an UTF-8 enabled collation, translates into 50% reduction in storage requirements. This reduction is because `NCHAR(10)` requires 20 bytes for storage, whereas `CHAR(10)` requires 10 bytes for the same Unicode string.

For more information, see [Collation and Unicode Support](../../../relational-databases/collations/collation-and-unicode-support.md).

**CTP 2.1** Adds support to select UTF-8 collation as default during [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)] setup.

**CTP 2.2** Adds support to use UTF-8 character encoding with SQL Server Replication.

**CTP 2.3** Adds support to use UTF-8 character encoding with a BIN2 collation (UTF8_BIN2).

## See Also  
[OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md) 

[UTF-16 Support in OLE DB Driver for SQL Server](../../oledb/features/utf-16-support-in-oledb-driver-for-sql-server.md)    
  
  

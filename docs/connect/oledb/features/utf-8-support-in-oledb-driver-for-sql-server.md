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
- [UTF-8 support](../../../sql-server/what-s-new-in-sql-server-ver15.md#utf-8-support-ctp-23)

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
  
## See Also  
[OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md) 

[UTF-16 Support in OLE DB Driver for SQL Server](../../oledb/features/utf-16-support-in-oledb-driver-for-sql-server.md)    
  
  

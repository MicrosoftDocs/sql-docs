---
title: "UTF-8 Support in OLE DB Driver for SQL Server| Microsoft Docs"
description: "UTF-8 Support in OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "02/04/2018"
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
- [UTF-8 support (CTP 2.2)](../../../sql-server/what-s-new-in-sql-server-ver15.md#utf-8-support-ctp-22)

## Data insertion into a UTF-8 encoded CHAR or VARCHAR column
When creating an input parameter buffer for insertion, the buffer is described by using an array of [DBBINDING structures](https://go.microsoft.com/fwlink/?linkid=2071182). Each DBBINDING structure associates a single parameter in the buffer and contains information such as the length and type of the data value. For an input parameter buffer of narrow characters, the *wType* of the DBBINDING structure should be set to DBTYPE_STR. For an input parameter buffer of wide characters, the *wType* of the DBBINDING structure should be set to DBTYPE_WSTR.

The conversion of the input parameter buffer from the client code page or input buffer encoding to the server column collation may occur at the driver side or the server side depending on the input buffer data type. During the conversion, data loss may occur if the client code page or the database collation code page is unable to represent all the characters in the input buffer.

If the input buffer type indicator is DBTYPE_STR, no conversion is done in the driver. The conversion from the client code page to the UTF-8 encoding is done on the server side. The user should ensure the client code page and the code page used by the database collation can represent all the characters in the input data. For example, to insert a Polish character, the client code page could be set to 1250 (ANSI Central European), and the database collation could use Polish as the collation designator (for example, Polish_100_CI_AS_SC) or be UTF-8 enabled.

If the input buffer type indicator is DBTYPE_WSTR and a prepared statement is being executed, the driver converts the input data from the UTF-16 encoding to the encoding used by the database collation. If the database collation isn't UTF-8 enabled, then the server converts the input data from the database collation encoding to the UTF-8 encoding. The user should ensure the code page used by the database collation can represent all the characters in the input data. If the insert statement isn't prepared, no conversion is done in the driver. The server converts the supplied data from the UTF-16 encoding to the UTF-8 encoding.

## Data retrieval from a UTF-8 encoded CHAR or VARCHAR column
When creating a buffer for retrieved data, the buffer is described by using an array of [DBBINDING structures](https://go.microsoft.com/fwlink/?linkid=2071182). Each DBBINDING structure associates a single column in the retrieved row. For retrieving the column data as narrow characters, the *wType* of the DBBINDING structure should be set to DBTYPE_STR. For retrieving the column data as wide characters, the *wType* of the DBBINDING structure should be set to DBTYPE_WSTR.

If the buffer has the type indicator DBTYPE_STR, then the driver converts the UTF-8 encoded data to the client encoding. The user should make sure the client encoding can represent the data from the UTF-8 column, otherwise, data loss may occur.

If the buffer has the type indicator DBTYPE_WSTR, then the driver converts the UTF-8 encoded data to the UTF-16 encoding.
  
## See Also  
[OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md) 

[UTF-16 Support in OLE DB Driver for SQL Server](../../oledb/features/utf-16-support-in-oledb-driver-for-sql-server.md)    
  
  

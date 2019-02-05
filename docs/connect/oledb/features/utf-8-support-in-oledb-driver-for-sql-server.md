---
title: "UTF-8 Support in OLE DB Driver for SQL Server| Microsoft Docs"
description: "UTF-8 Support in OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "02/04/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
author: pmasl
ms.author: pelopes
manager: craigg
---
# UTF-8 Support in OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

Beginning in [!INCLUDE[sssqlv15](../../../includes/sssqlv15-md.md)], support for the UTF-8 character encoding as database-level or column-level collation for text data is added. UTF-8 is allowed in the `CHAR` and `VARCHAR` datatypes, `NCHAR` and `NVARCHAR` remain unchanged and only allow UTF-16 encoding.

## Data insertion into a UTF-8 collated CHAR or VARCHAR column
If the input parameter type is DBTYPE_STR, no conversion is performed in the driver. Conversion from the client code page to the UTF-8 encoding is performed on the server side. The user should ensure the client code page and the database collation can represent all the characters in the input parameter. For example for inserting a Polish character, the client code page should be set to Polish, and the database collation either has Polish locale or is UTF-8 enabled.

If the input parameter type is DBTYPE_WSTR, the driver converts the supplied data from UTF-16 to the database collation. If the database collation is not UTF-8 enabled, the driver converts the supplied data to DBTYPE_STR using the database collation, then the server converts the data from the database collation to the UTF-8 encoding. If the database collation is also UTF-8 enabled, the driver converts the UTF-16 encoded data to the UTF-8 encoding.

For avoiding data loss, the user should either (1) specify an input parameter of DBTYPE_STR and ensure both the client code page and the database collation can represent all the characters from the source data, or (2) specify an input parameter of DBTYPE_WSTR.

## Data retrieval from a UTF-8 collated CHAR or VARCHAR column
If the binding type of the retrieved data is DBTYPE_STR, the driver converts the UTF-8 encoded data to the client encoding. Thus, make sure the client encoding can represent the data from the UTF-8 column, otherwise, data may be loss.

If the binding type is DBTYPE_WSTR, the driver converts the UTF-8 encoded data to UTF-16.
  
## See Also  
 [OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md)   
  
  

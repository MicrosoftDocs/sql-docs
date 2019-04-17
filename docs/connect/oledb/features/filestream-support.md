---
title: "FILESTREAM Support | Microsoft Docs"
description: "FILESTREAM support in OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], OLE DB Driver for SQL Server"
  - "OLE DB Driver for SQL Server [FILESTREAM support]"
author: pmasl
ms.author: pelopes
manager: craigg
---
# FILESTREAM Support
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

Beginning with [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], OLE DB Driver for SQL Server supports the enhanced FILESTREAM feature. For samples, see [Filestream and OLE DB](../../oledb/ole-db-how-to/filestream/filestream-and-ole-db.md).  

FILESTREAM provides a way to store and access large binary values, either through [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or by direct access to the Windows file system. A large binary value is a value larger than 2 gigabytes (GB). For more information about enhanced FILESTREAM support, see [FILESTREAM &#40;SQL Server&#41;](../../../relational-databases/blob/filestream-sql-server.md).  
  
When a database connection is opened, **@@TEXTSIZE** will be set to -1 ("unlimited"), by default.  
  
It is also possible to access and update FILESTREAM columns using Windows file system APIs.  
  
For more information, see [Access FILESTREAM Data with OpenSqlFilestream](../../../relational-databases/blob/access-filestream-data-with-opensqlfilestream.md)  
  
## Querying for FILESTREAM Columns  
Schema rowsets in OLE DB will not report whether a column is a FILESTREAM column. ITableDefinition in OLE DB cannot be used to create a FILESTREAM column.    
  
To create FILESTREAM columns or to detect which existing columns are FILESTREAM columns, you can use the **is_filestream** column of the [sys.columns](../../../relational-databases/system-catalog-views/sys-columns-transact-sql.md) catalog view.  
  
The following is an example:  
  
```sql  
-- Create a table with a FILESTREAM column.  
CREATE TABLE Bob_01 (GuidCol1 uniqueidentifier ROWGUIDCOL NOT NULL UNIQUE DEFAULT NEWID(), IntCol2 int, varbinaryCol3 varbinary(max) FILESTREAM);  
  
-- Find FILESTREAM columns.  
SELECT name FROM sys.columns WHERE is_filestream=1;  
  
-- Determine whether a column is a FILESTREAM column.  
SELECT is_filestream FROM sys.columns WHERE name = 'varbinaryCol3' AND object_id IN (SELECT object_id FROM sys.tables WHERE name='Bob_01');  
```  
  
## Down-Level Compatibility  
If your client was compiled using OLE DB Driver for SQL Server, and the application connects to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]), then **varbinary(max)** behavior will be compatible with the behavior introduced by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. That is, the maximum size of returned data will be limited to 2 GB. For result values larger that 2 GB, truncation will occur and a "string data right truncation" warning will be returned. 
  
When data-type compatibility is set to 80, client behavior will be consistent with down-level client behavior.  
  
For clients that use SQLOLEDB or other providers that were released before the [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], **varbinary(max)** will be mapped to image.  
  
## Comments
- To send and receive **varbinary(max)** values greater than 2 GB, an application uses **DBTYPE_IUNKNOWN** in parameter and result bindings. For parameters the provider must call IUnknown::QueryInterface for ISequentialStream and for results that return ISequentialStream.  

-  For OLE DB, checking related to ISequentialStream values will be relaxed. When *wType* is **DBTYPE_IUNKNOWN** in the **DBBINDING** struct, length checking can be disabled either by omitting **DBPART_LENGTH** from *dwPart* or by setting the length of the data (at offset *obLength* in the data buffer) to ~0. In this case, the provider will not check the length of the value and will request and return all of the data available through the stream. This change will be applied to all large object (LOB) types and XML, but only when connected to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] (or later) servers. This will provide greater flexibility for developers, while maintaining consistency and backwards compatibility for existing applications and downlevel servers.  This change affects all interfaces that transfer data, principally IRowset::GetData, ICommand::Execute, and IRowsetFastLoad::InsertRow.
 

## See Also  
 [OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md)  
  
  

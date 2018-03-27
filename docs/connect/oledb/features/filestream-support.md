---
title: "FILESTREAM Support | Microsoft Docs"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "oledb|features"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], OLE DB Driver for SQL Server"
  - "OLE DB Driver for SQL Server [FILESTREAM support]"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Inactive"
---
# FILESTREAM Support
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  FILESTREAM provides a way to store and access large binary values, either through [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or by direct access to the Windows file system. A large binary value is a value larger than 2 gigabytes (GB). For more information about enhanced FILESTREAM support, see [FILESTREAM &#40;SQL Server&#41;](../../../relational-databases/blob/filestream-sql-server.md).  
  
 When a database connection is opened, **@@TEXTSIZE** will be set to -1 ("unlimited"), by default.  
  
 It is also possible to access and update FILESTREAM columns using Windows file system APIs.  
  
 For more information, see the following topics:  
  
-   [FILESTREAM Support &#40;OLE DB&#41;](../../oledb/ole-db/filestream-support-ole-db.md)    
  
-   [Access FILESTREAM Data with OpenSqlFilestream](../../../relational-databases/blob/access-filestream-data-with-opensqlfilestream.md)  
  
## Querying for FILESTREAM Columns  
 Schema rowsets in OLE DB will not report whether a column is a FILESTREAM column. ITableDefinition in OLE DB cannot be used to create a FILESTREAM column.    
  
 To create FILESTREAM columns or to detect which existing columns are FILESTREAM columns, you can use the **is_filestream** column of the [sys.columns](../../../relational-databases/system-catalog-views/sys-columns-transact-sql.md) catalog view.  
  
 The following is an example:  
  
```  
-- Create a table with a FILESTREAM column.  
CREATE TABLE Bob_01 (GuidCol1 uniqueidentifier ROWGUIDCOL NOT NULL UNIQUE DEFAULT NEWID(), IntCol2 int, varbinaryCol3 varbinary(max) FILESTREAM);  
  
-- Find FILESTREAM columns.  
SELECT name FROM sys.columns WHERE is_filestream=1;  
  
-- Determine whether a column is a FILESTREAM column.  
SELECT is_filestream FROM sys.columns WHERE name = 'varbinaryCol3' AND object_id IN (SELECT object_id FROM sys.tables WHERE name='Bob_01');  
```  
  
## Down-Level Compatibility  
 If your client was compiled using OLE DB Driver for SQL Server, and the application connects to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], **varbinary(max)** behavior will be compatible with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. That is, the maximum size of returned data will be limited to 2 GB. For result values larger that 2 GB, truncation will occur and a "string data right truncation" warning will be returned.  
  
 When data-type compatibility is set to 80, client behavior will be consistent with down-level client behavior.  
  
 For clients that use SQLOLEDB or other providers that were released before the [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], **varbinary(max)** will be mapped to image.  
  
## See Also  
 [OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md)  
  
  

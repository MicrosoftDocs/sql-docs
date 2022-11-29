---
description: "FILESTREAM Support"
title: "FILESTREAM Support | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], SQL Server Native Client"
  - "SQL Server Native Client [FILESTREAM support]"
ms.assetid: 1ad3400d-7fcd-40c9-87ae-f5afc61e0374
author: markingmyname
ms.author: maghan
---
# FILESTREAM Support
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  FILESTREAM provides a way to store and access large binary values, either through [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or by direct access to the Windows file system. A large binary value is a value larger than 2 gigabytes (GB). For more information about enhanced FILESTREAM support, see [FILESTREAM &#40;SQL Server&#41;](../../../relational-databases/blob/filestream-sql-server.md).  
  
 When a database connection is opened, **\@\@TEXTSIZE** will be set to -1 ("unlimited"), by default.  
  
 It is also possible to access and update FILESTREAM columns using Windows file system APIs.  
  
 For more information, see the following topics:  
  
-   [FILESTREAM Support &#40;OLE DB&#41;](../../../relational-databases/native-client/ole-db/filestream-support-ole-db.md)  
  
-   [FILESTREAM Support &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/filestream-support-odbc.md)  
  
-   [Access FILESTREAM Data with OpenSqlFilestream](../../../relational-databases/blob/access-filestream-data-with-opensqlfilestream.md)  
  
## Querying for FILESTREAM Columns  
 Schema rowsets in OLE DB will not report whether a column is a FILESTREAM column. ITableDefinition in OLE DB cannot be used to create a FILESTREAM column.  
  
 Catalog functions such as SQLColumns in ODBC will not report whether a column is a FILESTREAM column.  
  
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
 If your client was compiled using the version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client that was included with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], and the application connects to a newer version of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], **varbinary(max)** behavior will be compatible with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. That is, the maximum size of returned data will be limited to 2 GB. For result values larger that 2 GB, truncation will occur and a "string data right truncation" warning will be returned.  
  
 When data-type compatibility is set to 80, client behavior will be consistent with down-level client behavior.  
  
 For clients that use SQLOLEDB or other providers that were released before the [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, **varbinary(max)** will be mapped to image.  
  
## See Also  
 [SQL Server Native Client Features](../../../relational-databases/native-client/features/sql-server-native-client-features.md)  
  
  

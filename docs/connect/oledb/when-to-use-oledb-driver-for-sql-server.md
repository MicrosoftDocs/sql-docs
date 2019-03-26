---
title: "When to Use OLE DB Driver for SQL Server | Microsoft Docs"
description: "When to use OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB Driver for SQL Server"
  - "MSOLEDBSQL, about OLE DB Driver for SQL Server"
  - "data access [OLE DB Driver for SQL Server], about OLE DB Driver for SQL Server"
author: pmasl
ms.author: pelopes
manager: craigg
---
# When to Use OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../includes/driver_oledb_download.md)]

  OLE DB Driver for SQL Server is one technology that you can use to access data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  For a discussion of the different data-access technologies, see [Data Access Technologies Road Map](https://go.microsoft.com/fwlink/?LinkID=179186)  
  
 When deciding whether to use OLE DB Driver for SQL Server as the data access technology of your application, you should consider several factors.  
  
 For new applications, if you're using a managed programming language such as Microsoft Visual C# or Visual Basic, and you need to access the new features in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you should use the .NET Framework Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], which is part of the .NET Framework.  
  
 If you are developing a COM-based application and need to access the new features introduced in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you should use OLE DB Driver for SQL Server. If you don't need access to the new features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can continue to use Windows Data Access Components (WDAC).  
  
 For existing OLE DB applications, the primary issue is whether you need to access the new features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you have a mature application that does not need the new features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can continue to use WDAC. But if you do need to access those new features, such as the [xml data type](../../t-sql/xml/xml-transact-sql.md), you should use OLE DB Driver for SQL Server.  
  
 Both OLE DB Driver for SQL Server and MDAC support read committed transaction isolation using row versioning, but only OLE DB Driver for SQL Server supports snapshot transaction isolation. (In programming terms, read committed transaction isolation with row versioning is the same as Read-Committed transaction.)  
  
 For information about the differences between OLE DB Driver for SQL Server and MDAC, see [Updating an Application to OLE DB Driver for SQL Server from MDAC](../oledb/applications/updating-an-application-to-oledb-driver-for-sql-server-from-mdac.md).  
  
## See Also  
 [OLE DB Driver for SQL Server](../oledb/oledb-driver-for-sql-server.md)     
 [OLE DB How-to Topics](../oledb/ole-db-how-to/ole-db-how-to-topics.md)  
  
  

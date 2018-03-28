---
title: "FILESTREAM Support (OLE DB) | Microsoft Docs"
description: "FILESTREAM Support (OLE DB)"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "ole-db"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB [FILESTREAM support]"
  - "FILESTREAM [SQL Server], OLE DB"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Inactive"
---
# FILESTREAM Support (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  Beginning with [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] and OLE DB Driver for SQL Server, OLE DB supports the enhanced FILESTREAM feature. For more information about this feature, see [FILESTREAM Support](../../oledb/features/filestream-support.md). For samples, see [Filestream and OLE DB](../../oledb/ole-db-how-to/filestream/filestream-and-ole-db.md).  
  
 To send and receive **varbinary(max)** values greater than 2 GB, an application uses **DBTYPE_IUNKNOWN** in parameter and result bindings. For parameters the provider must call IUnknown::QueryInterface for ISequentialStream and for results that return ISequentialStream.  
  
 For OLE DB, checking related to ISequentialStream values will be relaxed. When *wType* is **DBTYPE_IUNKNOWN** in the **DBBINDING** struct, length checking can be disabled either by omitting **DBPART_LENGTH** from *dwPart* or by setting the length of the data (at offset *obLength* in the data buffer) to ~0. In this case, the provider will not check the length of the value and will request and return all of the data available through the stream. This change will be applied to all large object (LOB) types and XML, but only when connected to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] (or later) servers. This will provide greater flexibility for developers, while maintaining consistency and backwards compatibility for existing applications and downlevel servers.  
  
 This change affects all interfaces that transfer data, principally IRowset::GetData, ICommand::Execute, and IRowsetFastLoad::InsertRow.  
  
## See Also  
 [OLE DB Driver for SQL Server Programming](../../oledb/oledb-driver-for-sql-server-programming.md)  
  
  

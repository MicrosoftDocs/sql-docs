---
description: "FILESTREAM Support (OLE DB)"
title: "FILESTREAM Support (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB [FILESTREAM support]"
  - "FILESTREAM [SQL Server], OLE DB"
ms.assetid: c2bd3dfd-6103-43d1-859e-8ed8d19c58d3
author: markingmyname
ms.author: maghan
---
# FILESTREAM Support (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-only.md)]

  Beginning with [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 10.0, OLE DB supports the enhanced FILESTREAM feature. For more information about this feature, see [FILESTREAM Support](../../../relational-databases/native-client/features/filestream-support.md). For samples, see [Filestream and OLE DB](../../../relational-databases/native-client-ole-db-how-to/filestream/filestream-and-ole-db.md).  
  
 To send and receive **varbinary(max)** values greater than 2 GB, an application uses **DBTYPE_IUNKNOWN** in parameter and result bindings. For parameters the provider must call IUnknown::QueryInterface for ISequentialStream and for results that return ISequentialStream.  
  
 For OLE DB, checking related to ISequentialStream values will be relaxed. When *wType* is **DBTYPE_IUNKNOWN** in the **DBBINDING** struct, length checking can be disabled either by omitting **DBPART_LENGTH** from *dwPart* or by setting the length of the data (at offset *obLength* in the data buffer) to ~0. In this case, the provider will not check the length of the value and will request and return all of the data available through the stream. This change will be applied to all large object (LOB) types and XML, but only when connected to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] (or later) servers. This will provide greater flexibility for developers, while maintaining consistency and backwards compatibility for existing applications and downlevel servers.  
  
 This change affects all interfaces that transfer data, principally IRowset::GetData, ICommand::Execute, and IRowsetFastLoad::InsertRow.  
  
## See Also  
 [SQL Server Native Client Programming](../../../relational-databases/native-client/sql-server-native-client-programming.md)  
  
  

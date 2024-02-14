---
title: "Next fetch position (Native Client OLE DB provider)"
description: "Fetching Rows - Next Fetch  (Native Client OLE DB Provider)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "fetching rows"
  - "OLE DB rowsets, fetching"
  - "next fetch position"
  - "rowsets [OLE DB], fetching"
---
# Fetching Rows - Next Fetch  (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider keeps track of the next fetch position so that a sequence of calls to the **GetNextRows** method (without skips, changes of direction, or intervening calls to the **FindNextRow**, **Seek**, or **RestartPosition** methods) reads the whole rowset without skipping or repeating any row. The next fetch position is changed either by calling **IRowset::GetNextRows**, **IRowset::RestartPosition**, or **IRowsetIndex::Seek**, or by calling **FindNextRow** with a null *pBookmark* value. Calling **FindNextRow** with a nonnull *pBookmark* value does not affect the next fetch position.  
  
## See Also  
 [Fetching Rows](../../relational-databases/native-client-ole-db-rowsets/fetching-rows.md)  
  
  

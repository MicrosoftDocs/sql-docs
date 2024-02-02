---
title: Resynchronizing rows (Native Client OLE DB provider)
description: "Updating Data in Rowsets - Resynchronizing Rows in SQL Server Native Client"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "synchronization [OLE DB]"
  - "IRowsetResynch interface"
  - "resynchronizing rows"
  - "data updates [SQL Server], OLE DB"
---
# Updating Data in Rowsets - Resynchronizing Rows in SQL Server Native Client
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports **IRowsetResynch** on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cursor-supported rowsets only. **IRowsetResynch** is not available on demand. The consumer must request the interface before opening the rowset.  
  
## See Also  
 [Updating Data in Rowsets](../../relational-databases/native-client-ole-db-rowsets/updating-data-in-rowsets.md)  
  
  

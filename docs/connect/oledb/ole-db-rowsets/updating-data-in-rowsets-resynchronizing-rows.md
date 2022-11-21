---
title: Resynchronizing rows (OLE DB driver)
description: For updating data in rowsets, the OLE DB Driver for SQL Server supports IRowsetResynch on SQL Server cursor-supported rowsets only.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "synchronization [OLE DB]"
  - "IRowsetResynch interface"
  - "resynchronizing rows"
  - "data updates [SQL Server], OLE DB"
---
# Updating Data in Rowsets - Resynchronizing Rows
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server supports **IRowsetResynch** on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cursor-supported rowsets only. **IRowsetResynch** is not available on demand. The consumer must request the interface before opening the rowset.  
  
## See Also  
 [Updating Data in Rowsets](../../oledb/ole-db-rowsets/updating-data-in-rowsets.md)  
  
  

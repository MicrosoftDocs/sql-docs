---
title: "IDBProperties (OLE DB driver)"
description: "Learn about the IDBProperties interface in OLE DB Driver for SQL Server, including the IDBProperties::GetPropertyInfo method."
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# IDBProperties (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB standard specification allows providers to specify VT_EMPTY for **DBPROPINFO::vValues**. However, OLE DB Driver for SQL Server OLE DB always returns VT_EMPTY when you call **IDBProperties::GetPropertyInfo** with **DBPROPSET_ROWSETALL** to retrieve rowset properties.  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md) 
  
  

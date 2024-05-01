---
title: "IDBProperties (Native Client OLE DB provider)"
description: "IDBProperties (Native Client OLE DB provider)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
---
# IDBProperties (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  The OLE DB standard specification allows providers to specify VT_EMPTY for **DBPROPINFO::vValues**. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB always returns VT_EMPTY when you call **IDBProperties::GetPropertyInfo** with **DBPROPSET_ROWSETALL** to retrieve rowset properties.  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](./sql-server-native-client-ole-db-interfaces.md)  
  

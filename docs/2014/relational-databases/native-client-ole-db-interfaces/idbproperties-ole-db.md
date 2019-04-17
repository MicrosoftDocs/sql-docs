---
title: "IDBProperties (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
ms.assetid: 2e5a4fd8-5164-495a-9986-3477aef8d8a5
author: MightyPen
ms.author: genemi
manager: craigg
---
# IDBProperties (OLE DB)
  The OLE DB standard specification allows providers to specify VT_EMPTY for `DBPROPINFO::vValues`. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB always returns VT_EMPTY when you call `IDBProperties::GetPropertyInfo` with `DBPROPSET_ROWSETALL` to retrieve rowset properties.  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../database-engine/dev-guide/interfaces-ole-db.md)  
  
  

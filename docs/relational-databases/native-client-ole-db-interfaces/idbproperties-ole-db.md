---
description: "IDBProperties (Native Client OLE DB provider)"
title: "IDBProperties (Native Client OLE DB provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
ms.assetid: 2e5a4fd8-5164-495a-9986-3477aef8d8a5
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# IDBProperties (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  The OLE DB standard specification allows providers to specify VT_EMPTY for **DBPROPINFO::vValues**. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB always returns VT_EMPTY when you call **IDBProperties::GetPropertyInfo** with **DBPROPSET_ROWSETALL** to retrieve rowset properties.  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](./sql-server-native-client-ole-db-interfaces.md)  
  

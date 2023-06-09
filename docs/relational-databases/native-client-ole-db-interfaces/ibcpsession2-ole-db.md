---
title: "IBCPSession2 (Native Client OLE DB provider)"
description: "IBCPSession2 (Native Client OLE DB provider)"
author: markingmyname
ms.author: maghan
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "IBCPSession2 interface"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# IBCPSession2 (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

The IBCPSession2 interface is an extension to IBCPSession that provides a member function that is an alternative to calling IBCPSession::BCPColFmt for each column.  IBCPSession2 inherits from IBCPSession and adds one new method: [IBCPSession2::BCPSetBulkMode](../../relational-databases/native-client-ole-db-interfaces/ibcpsession2-bcpsetbulkmode.md).  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](./sql-server-native-client-ole-db-interfaces.md)  
  

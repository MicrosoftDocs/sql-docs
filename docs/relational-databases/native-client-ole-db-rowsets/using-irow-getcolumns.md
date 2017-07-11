---
title: "Using IRow::GetColumns | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "fetching rows"
  - "IRow interface"
  - "single row fetching [SQL Server Native Client]"
  - "OLE DB rowsets, fetching"
  - "rowsets [OLE DB], fetching"
  - "GetColumns method"
ms.assetid: 1f5d2e03-e6fe-4ea1-b71d-55d02b5d59ae
caps.latest.revision: 30
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Using IRow::GetColumns
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The **IRow** implementation allows forward-only sequential access to the columns. You can either access all the columns in the row with a single call to **IRow::GetColumns** or call **IRow::GetColumns** multiple times every time that you access several columns in the row.  
  
 The multiple calls to **IRow::GetColumns** should not overlap. For example, if the first call to **IRow::GetColumns** retrieves columns 1, 2, and 3, the second call to **IRow::GetColumns** should call for columns 4, 5, and 6. If later calls to **IRow::GetColumns** overlap, the status flag (dwstatus field in DBCOLUMNACCESS) is set to DBSTATUS_E_UNAVAILABLE.  
  
## See Also  
 [Fetching a Single Row with IRow](../../relational-databases/native-client-ole-db-rowsets/fetching-a-single-row-with-irow.md)  
  
  
---
title: "Using IRow::GetColumns | Microsoft Docs"
description: "Using IRow::GetColumns to access all the columns in a row"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "fetching rows"
  - "IRow interface"
  - "single row fetching [OLE DB Driver for SQL Server]"
  - "OLE DB rowsets, fetching"
  - "rowsets [OLE DB], fetching"
  - "GetColumns method"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Using IRow::GetColumns
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The **IRow** implementation allows forward-only sequential access to the columns. You can either access all the columns in the row with a single call to **IRow::GetColumns** or call **IRow::GetColumns** multiple times every time that you access several columns in the row.  
  
 The multiple calls to **IRow::GetColumns** should not overlap. For example, if the first call to **IRow::GetColumns** retrieves columns 1, 2, and 3, the second call to **IRow::GetColumns** should call for columns 4, 5, and 6. If later calls to **IRow::GetColumns** overlap, the status flag (dwstatus field in DBCOLUMNACCESS) is set to DBSTATUS_E_UNAVAILABLE.  
  
## See Also  
 [Fetching a Single Row with IRow](../../oledb/ole-db-rowsets/fetching-a-single-row-with-irow.md)  
  
  

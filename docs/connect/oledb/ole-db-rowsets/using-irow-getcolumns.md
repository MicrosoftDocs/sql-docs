---
title: "Using IRow::GetColumns (OLE DB driver)"
description: "Learn how to use IRow::GetColumns to access all columns in a row in OLE DB Driver for SQL Server. IRow allows forward-only sequential access to columns."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "fetching rows"
  - "IRow interface"
  - "single row fetching [OLE DB Driver for SQL Server]"
  - "OLE DB rowsets, fetching"
  - "rowsets [OLE DB], fetching"
  - "GetColumns method"
---
# Using IRow::GetColumns
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The **IRow** implementation allows forward-only sequential access to the columns. You can either access all the columns in the row with a single call to **IRow::GetColumns** or call **IRow::GetColumns** multiple times every time that you access several columns in the row.  
  
 The multiple calls to **IRow::GetColumns** should not overlap. For example, if the first call to **IRow::GetColumns** retrieves columns 1, 2, and 3, the second call to **IRow::GetColumns** should call for columns 4, 5, and 6. If later calls to **IRow::GetColumns** overlap, the status flag (dwstatus field in DBCOLUMNACCESS) is set to DBSTATUS_E_UNAVAILABLE.  
  
## Related content

- [Fetching a Single Row with IRow (OLE DB Driver)](fetching-a-single-row-with-irow.md)

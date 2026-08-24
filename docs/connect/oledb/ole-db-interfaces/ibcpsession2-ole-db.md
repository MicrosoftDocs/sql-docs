---
title: "IBCPSession2 (OLE DB driver)"
description: "Learn about the IBCPSession2 interface in OLE DB Driver for SQL Server, which provides BCPSetBulkMode, an alternative to IBCPSession::BCPColFmt per column."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "IBCPSession2 interface"
---
# IBCPSession2 (OLE DB)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The IBCPSession2 interface is an extension to IBCPSession that provides a member function that is an alternative to calling IBCPSession::BCPColFmt for each column.  IBCPSession2 inherits from IBCPSession and adds one new method: [IBCPSession2::BCPSetBulkMode](../../oledb/ole-db-interfaces/ibcpsession2-bcpsetbulkmode.md).  
  
## Related content

- [OLE DB Driver for SQL Server (OLE DB) Interfaces](oledb-driver-for-sql-server-ole-db-interfaces.md)

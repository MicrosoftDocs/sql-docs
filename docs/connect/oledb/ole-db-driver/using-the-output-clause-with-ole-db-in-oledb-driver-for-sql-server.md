---
title: "Using the OUTPUT Clause with OLE DB in OLE DB Driver for SQL Server"
description: Learn about using the OUTPUT clause in an INSERT, UPDATE, DELETE, or MERGE command in OLE DB Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2025
---
# Using the OUTPUT Clause with OLE DB in OLE DB Driver for SQL Server
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  If you use an OUTPUT clause in an INSERT, UPDATE, DELETE, or MERGE command, the count of affected rows is not available. The application must count the number of rows in the rowset that is returned by the OUTPUT clause.  
  
## Related content

- [Creating an OLE DB Driver for SQL Server Application](creating-a-oledb-driver-for-sql-server-application.md)

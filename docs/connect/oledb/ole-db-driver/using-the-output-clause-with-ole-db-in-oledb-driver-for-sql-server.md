---
title: "Using the OUTPUT Clause with OLE DB in OLE DB Driver for SQL Server"
description: Learn about using the OUTPUT clause in an INSERT, UPDATE, DELETE, or MERGE command in OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
---
# Using the OUTPUT Clause with OLE DB in OLE DB Driver for SQL Server
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  If you use an OUTPUT clause in an INSERT, UPDATE, DELETE, or MERGE command, the count of affected rows is not available. The application must count the number of rows in the rowset that is returned by the OUTPUT clause.  
  
## See Also  
 [Creating an OLE DB Driver for SQL Server Application](../../oledb/ole-db-driver/creating-a-oledb-driver-for-sql-server-application.md) 
  
  

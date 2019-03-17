---
title: "Using the OUTPUT Clause with OLE DB in OLE DB Driver for SQL Server | Microsoft Docs"
description: "Using the OUTPUT clause with OLE DB in OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Using the OUTPUT Clause with OLE DB in OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  If you use an OUTPUT clause in an INSERT, UPDATE, DELETE, or MERGE command, the count of affected rows is not available. The application must count the number of rows in the rowset that is returned by the OUTPUT clause.  
  
## See Also  
 [Creating an OLE DB Driver for SQL Server Application](../../oledb/ole-db-driver/creating-a-oledb-driver-for-sql-server-application.md) 
  
  

---
title: "Disconnecting from a Data Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC data sources, connections"
  - "data sources [SQL Server Native Client]"
  - "disconnecting [ODBC]"
  - "ODBC applications, disconnecting"
  - "SQLDisconnect function"
  - "ODBC applications, data sources"
  - "connections [SQL Server Native Client]"
  - "SQLFreeHandle function"
  - "ODBC data sources, disconnecting"
  - "SQL Server Native Client ODBC driver, data sources"
  - "ODBC functions"
  - "SQL Server Native Client ODBC driver, connections"
ms.assetid: 65b0267d-b2ab-4a59-83f2-436d90cfbf79
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Disconnecting from a Data Source
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  When an application has finished using a data source, it calls **SQLDisconnect**. **SQLDisconnect** frees any statements that are allocated on the connection and disconnects the driver from the data source. After disconnecting, the application can call [SQLFreeHandle](../../relational-databases/native-client-odbc-api/sqlfreehandle.md) to free the connection handle. Before exiting, an application also calls **SQLFreeHandle** to free the environment handle.  
  
 After disconnecting, an application can reuse the allocated connection handle, either to connect to a different data source or reconnect to the same data source. The decision to remain connected instead of disconnecting and reconnecting later requires that the application writer consider the relative costs of each option. Connecting to a data source and remaining connected can be relatively costly, depending on the connection medium. In making a trade-off, the application must also make assumptions about the probability and timing of additional operations on the same data source. An application may also have to use more than one connection.  
  
## See Also  
 [Communicating with SQL Server &#40;ODBC&#41;](../../relational-databases/native-client-odbc-communication/communicating-with-sql-server-odbc.md)  
  
  

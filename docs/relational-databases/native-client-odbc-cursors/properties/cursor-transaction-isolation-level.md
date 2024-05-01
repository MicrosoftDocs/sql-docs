---
title: "Cursor Transaction Isolation Level"
description: "Cursor Transaction Isolation Level"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "isolation levels [ODBC]"
  - "ODBC applications, row versioning"
  - "cursors [ODBC], isolation levels"
  - "ODBC cursors, isolation levels"
  - "row versioning [SQL Server], ODBC"
---
# Cursor Transaction Isolation Level
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The complete locking behavior of cursors is based on an interaction between concurrency attributes and the transaction isolation level set by the client. ODBC clients set the transaction isolation level using the [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md) SQL_ATTR_TXN_ISOLATION or SQL_COPT_SS_TXN_ISOLATION attributes. The locking behavior of a specific cursor environment is determined by combining the locking behaviors of the concurrency and transaction isolation level options.  
  
 The following cursor transaction isolation levels are supported by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver:  
  
-   Read committed (SQL_TXN_READ_COMMITTED)  
  
-   Read uncommitted (SQL_TXN_READ_UNCOMMITTED)  
  
-   Repeatable read (SQL_TXN_REPEATABLE_READ)  
  
-   Serializable (SQL_TXN_SERIALIZABLE)  
  
-   Snapshot (SQL_TXN_SS_SNAPSHOT)  
  
 Note that the ODBC API specifies additional transaction isolation levels, but these are not supported by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
## See Also  
 [Cursor Properties](../../../relational-databases/native-client-odbc-cursors/properties/cursor-properties.md)  
  
  

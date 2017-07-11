---
title: "Cursor Transaction Isolation Level | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "isolation levels [ODBC]"
  - "ODBC applications, row versioning"
  - "cursors [ODBC], isolation levels"
  - "ODBC cursors, isolation levels"
  - "row versioning [SQL Server], ODBC"
ms.assetid: 0c6663a4-5a25-44aa-8fe4-e35af9bf4a83
caps.latest.revision: 32
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Cursor Transaction Isolation Level
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

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
  
  
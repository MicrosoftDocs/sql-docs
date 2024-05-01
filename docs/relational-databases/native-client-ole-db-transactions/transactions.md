---
title: Transactions (Native Client OLE DB provider)
description: "Transactions in SQL Server Native Client"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "OLE DB, transactions"
  - "transactions [OLE DB]"
  - "SQL Server Native Client OLE DB provider, transactions"
---
# Transactions in SQL Server Native Client
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider implements local transaction support. The consumer can use distributed or coordinated transactions by using Microsoft Distributed Transaction Coordinator (MS DTC). For consumers requiring transaction control that spans multiple sessions, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider can join transactions initiated and maintained by MS DTC.  
  
 By default, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider uses an autocommit transaction mode, where each discrete action on a consumer session comprises a complete transaction against an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider autocommit mode is local, and autocommit transactions never span more than a single session.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider exposes the **ITransactionLocal** interface, allowing the consumer to use explicitly and implicitly start transactions on a single connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider does not support nested local transactions.  
  
## In This Section  
  
-   [Supporting Local Transactions](../../relational-databases/native-client-ole-db-transactions/supporting-local-transactions.md)  
  
-   [Supporting Distributed Transactions](../../relational-databases/native-client-ole-db-transactions/supporting-distributed-transactions.md)  
  
-   [Isolation Levels &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-transactions/isolation-levels-ole-db.md)  
  
## See Also  
 [SQL Server Native Client &#40;OLE DB&#41;](../../relational-databases/native-client/ole-db/sql-server-native-client-ole-db.md)  
  
  

---
title: "Isolation Levels (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB, transactions"
  - "isolation levels [OLE DB]"
  - "transactions [OLE DB]"
  - "SQL Server Native Client OLE DB provider, transactions"
ms.assetid: d70ee72c-6e2a-4bcd-9456-4a697a866361
author: MightyPen
ms.author: genemi
manager: craigg
---
# Isolation Levels (OLE DB)
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients can control transaction-isolation levels for a connection. To control transaction-isolation level, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider consumer uses:  
  
-   DBPROPSET_SESSION property DBPROP_SESS_AUTOCOMMITISOLEVELS for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider default autocommit mode.  
  
     The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider default for the level is DBPROPVAL_TI_READCOMMITTED.  
  
-   The *isoLevel* parameter of the **ITransactionLocal::StartTransaction** method for local manual-commit transactions.  
  
-   The *isoLevel* parameter of the **ITransactionDispenser::BeginTransaction** method for MS DTC-coordinated distributed transactions.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows read-only access at the dirty read isolation level. All other levels restrict concurrency by applying locks to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects. As the client requires greater concurrency levels, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applies greater restrictions on concurrent access to data. To maintain the highest level of concurrent access to data, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider consumer should intelligently control its requests for specific concurrency levels.  
  
> [!NOTE]  
>  [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] introduced snapshot isolation level. For more information, see [Working with Snapshot Isolation](../native-client/features/working-with-snapshot-isolation.md).  
  
## See Also  
 [Transactions](transactions.md)  
  
  

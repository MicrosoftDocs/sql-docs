---
title: "Isolation Levels (OLE DB) | Microsoft Docs"
description: "Isolation Levels (OLE DB)"
ms.custom: ""
ms.date: "02/28/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "ole-db-transactions"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB, transactions"
  - "isolation levels [OLE DB]"
  - "transactions [OLE DB]"
  - "OLE DB Driver for SQL Server, transactions"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Isolation Levels (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] clients can control transaction-isolation levels for a connection. To control transaction-isolation level, the OLE DB Driver for SQL Server consumer uses:  
  
-   DBPROPSET_SESSION property DBPROP_SESS_AUTOCOMMITISOLEVELS for the OLE DB Driver for SQL Server default autocommit mode.  
  
     The OLE DB Driver for SQL Server default for the level is DBPROPVAL_TI_READCOMMITTED.  
  
-   The *isoLevel* parameter of the **ITransactionLocal::StartTransaction** method for local manual-commit transactions.  
  
-   The *isoLevel* parameter of the **ITransactionDispenser::BeginTransaction** method for MS DTC-coordinated distributed transactions.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] allows read-only access at the dirty read isolation level. All other levels restrict concurrency by applying locks to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] objects. As the client requires greater concurrency levels, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] applies greater restrictions on concurrent access to data. To maintain the highest level of concurrent access to data, the OLE DB Driver for SQL Server consumer should intelligently control its requests for specific concurrency levels.  
  
> [!NOTE]  
>  [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] introduced snapshot isolation level. For more information, see [Working with Snapshot Isolation](../../oledb/features/working-with-snapshot-isolation.md).  
  
## See Also  
 [Transactions](../../oledb/ole-db-transactions/transactions.md)  
  
  

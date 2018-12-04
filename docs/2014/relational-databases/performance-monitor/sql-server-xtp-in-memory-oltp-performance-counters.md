---
title: "XTP (In-Memory OLTP) Performance Counters | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
ms.assetid: fe3cbaf4-65f4-44c5-acc6-7b735cda0c5d
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# XTP (In-Memory OLTP) Performance Counters
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides objects and counters that can be used by Performance Monitor to monitor In-Memory OLTP activity.  
  
##  <a name="SQLServerPOs"></a> XTP (In-Memory OLTP) Performance Objects  
 The following table describes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects.  
  
|Performance object|Description|  
|------------------------|-----------------|  
|[XTP Cursors](../cursors.md)|The XTP Cursors performance object contains counters related to internal XTP engine cursors. Cursors are the low-level building blocks the XTP engine uses to process [!INCLUDE[tsql](../../includes/tsql-md.md)] queries. As such, you do not typically have direct control over them.|  
|[XTP Garbage Collection](sql-server-xtp-garbage-collection.md)|The XTP Garbage Collection performance object contains counters related to the XTP engine's garbage collector.|  
|[XTP Phantom Processor](sql-server-xtp-phantom-processor.md)|The XTP Phantom Processor performance object contains counters related to the XTP engine's phantom processing subsystem. This component is responsible for detecting phantom rows in transactions running at the SERIALIZABLE isolation level.|  
|[XTP Storage](sql-server-xtp-storage.md)|The XTP Storage performance object contains counters related to XTP storage in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[XTP Transaction Log](sql-server-xtp-transaction-log.md)|The XTP Transaction Log performance object contains counters related to XTP transaction logging in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[XTP Transactions](sql-server-xtp-transactions.md)|The XTP Transactions performance object contains counters related to XTP engine transactions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
  

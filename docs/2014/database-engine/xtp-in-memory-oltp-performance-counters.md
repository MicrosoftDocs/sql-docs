---
title: "XTP (In-Memory OLTP) Performance Counters | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-cross-instance"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: fe3cbaf4-65f4-44c5-acc6-7b735cda0c5d
caps.latest.revision: 10
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# XTP (In-Memory OLTP) Performance Counters
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides objects and counters that can be used by Performance Monitor to monitor In-Memory OLTP activity.  
  
##  <a name="SQLServerPOs"></a> XTP (In-Memory OLTP) Performance Objects  
 The following table describes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects.  
  
|Performance object|Description|  
|------------------------|-----------------|  
|[XTP Cursors](../../2014/database-engine/xtp-cursors.md)|The XTP Cursors performance object contains counters related to internal XTP engine cursors. Cursors are the low-level building blocks the XTP engine uses to process [!INCLUDE[tsql](../../includes/tsql-md.md)] queries. As such, you do not typically have direct control over them.|  
|[XTP Garbage Collection](../../2014/database-engine/xtp-garbage-collection.md)|The XTP Garbage Collection performance object contains counters related to the XTP engine's garbage collector.|  
|[XTP Phantom Processor](../../2014/database-engine/xtp-phantom-processor.md)|The XTP Phantom Processor performance object contains counters related to the XTP engine's phantom processing subsystem. This component is responsible for detecting phantom rows in transactions running at the SERIALIZABLE isolation level.|  
|[XTP Storage](../../2014/database-engine/xtp-storage.md)|The XTP Storage performance object contains counters related to XTP storage in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[XTP Transaction Log](../../2014/database-engine/xtp-transaction-log.md)|The XTP Transaction Log performance object contains counters related to XTP transaction logging in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[XTP Transactions](../../2014/database-engine/xtp-transactions.md)|The XTP Transactions performance object contains counters related to XTP engine transactions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
  
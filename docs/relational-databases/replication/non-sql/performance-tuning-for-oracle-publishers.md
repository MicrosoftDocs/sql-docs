---
description: "Performance Tuning for Oracle Publishers"
title: "Performance Tuning for Oracle Publishers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle publishing [SQL Server replication], performance tuning"
ms.assetid: 32c0b4ec-c166-45a3-b41e-38a30fd56813
author: "MashaMSFT"
ms.author: "mathoma"
---
# Performance Tuning for Oracle Publishers
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  The Oracle publishing architecture is similar to the [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] publishing architecture; therefore the first step in tuning Oracle replication for performance requires following the general tuning recommendations found in [Enhance General Replication Performance](../../../relational-databases/replication/administration/enhance-general-replication-performance.md).  
  
 In addition there are two options for Oracle Publishers that are performance related:  
  
-   Specifying the appropriate publishing option: Oracle or Oracle Gateway.  
  
-   Configuring the transaction set job to process changes on the Publisher at an appropriate interval.  
  
## Specifying the Appropriate Publishing Option  
 The Oracle Gateway option provides improved performance over the Oracle Complete option; however, this option cannot be used to publish the same table in multiple transactional publications. A table can appear in at most one transactional publication and any number of snapshot publications. If you need to publish the same table in multiple transactional publications, choose the Oracle Complete option. Specify this option when identifying the Oracle Publisher at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor. For more information, see [Create a Publication from an Oracle Database](../../../relational-databases/replication/publish/create-a-publication-from-an-oracle-database.md).  
  
## Configuring the Transaction Set Job  
 Changes to published Oracle tables are processed in groups called transaction sets. To ensure transactional consistency, each transaction set is committed as a single transaction at the distribution database. If the transaction set becomes too large, it cannot be processed efficiently as a single transaction.  
  
 By default, transaction sets are created only by the Log Reader Agent. If, during periods of high change activity, the Log Reader Agent does not run or cannot connect from the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor to the Oracle Publisher, transaction sets can become unmanageably large. To prevent this problem, ensure that transaction sets are created at regular intervals, even if the Log Reader Agent does not run or cannot connect to the Oracle Publisher.  
  
 Transaction sets can be created with the Xactset job (an Oracle database job installed by replication), which uses the same mechanism that the Log Reader Agent does to create sets. Each time the job runs, a new transaction set is created. The next time that the Log Reader Agent runs, the agent processes any sets that have been created. If there are still pending changes after all existing transaction sets have been processed, the Log Reader Agent creates and processes one or more additional transaction sets.  
  
 To configure the transaction set job, see [Configure the Transaction Set Job for an Oracle Publisher &#40;Replication Transact-SQL Programming&#41;](../../../relational-databases/replication/administration/configure-the-transaction-set-job-for-an-oracle-publisher.md).  
  
## See Also  
 [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md)   
 [Oracle Publishing Overview](../../../relational-databases/replication/non-sql/oracle-publishing-overview.md)  
  
  

---
description: "Oracle Publishing Overview"
title: "Oracle Publishing Overview | Microsoft Docs"
ms.custom: ""
ms.date: "08/29/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "publishing [SQL Server replication], Oracle publishing"
  - "snapshot replication [SQL Server], Oracle publishing"
  - "Oracle publishing [SQL Server replication]"
  - "transactional replication, Oracle publishing"
  - "Oracle publishing [SQL Server replication], about Oracle publishing"
ms.assetid: 2e013259-0022-4897-a08d-5f8deb880fa8
author: "MashaMSFT"
ms.author: "mathoma"
---
# Oracle Publishing Overview  
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Beginning with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], you can include Oracle Publishers in your replication topology, starting with Oracle version 9i. Publishing servers can be deployed on any Oracle supported hardware and operating system. The feature is built on the well-established foundation of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] snapshot replication and transactional replication, providing similar performance and usability.  
  
[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports the following heterogeneous scenarios for transactional and snapshot replication:  
  
-   Publishing data from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers.  

-   Publishing data to and from Oracle has the following restrictions:  

  |Scenario| 2016 or earlier |2017 or later |
  |-------|-------|--------|
  |Replication from Oracle |Only support Oracle 10g or earlier |Only support Oracle 10g or earlier |
  |Replication to Oracle |Up to Oracle 12c |Not supported |


 Heterogeneous replication to non-SQL Server subscribers is deprecated. Oracle Publishing is deprecated. To move data, create solutions using change data capture and [!INCLUDE[ssIS](../../../includes/ssis-md.md)].  

  
## Snapshot Replication for Oracle  
 Oracle snapshot publications are implemented in a manner similar to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] snapshot publications. When the Snapshot Agent runs for an Oracle publication, it connects to the Oracle Publisher and processes each table in the publication. When processing each table, the agent retrieves the table rows and creates schema scripts, which are then stored on the publication's snapshot share. The entire set of data is created each time the Snapshot Agent runs, so change tracking triggers are not added to the Oracle tables as they are with transactional replication. Snapshot replication provides a convenient way to migrate data with minimal impact on the publishing system.  
  
## Transactional Replication for Oracle  
 Oracle transactional publications are implemented using the transactional publishing architecture of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]; however, changes are tracked using a combination of database triggers on the Oracle database and the Log Reader Agent. Subscribers to an Oracle transactional publication are automatically initialized using snapshot replication; subsequent changes are tracked and delivered to Subscribers as they occur via the Log Reader Agent.  
  
 When an Oracle publication is created, triggers and tracking tables are created for each published table within the Oracle database. When data changes are made to the published tables, the database triggers on the tables fire and insert information into the replication tracking tables for each modified row. The Log Reader Agent on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor then moves the data change information from the tracking tables to the distribution database on the Distributor. Finally, as in standard transactional replication, the Distribution Agent moves changes from the Distributor to the Subscribers.  
  
## See Also  
 [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md)   
 [Glossary of Terms for Oracle Publishing](../../../relational-databases/replication/non-sql/glossary-of-terms-for-oracle-publishing.md)   
 [Heterogeneous Database Replication](../../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)  
  
  

---
description: "Heterogeneous Database Replication"
title: "Heterogeneous Database Replication | Microsoft Docs"
ms.custom: ""
ms.date: "08/28/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "heterogeneous database replication, about heterogeneous database replication"
  - "replication [SQL Server], heterogeneous database replication"
  - "heterogeneous database replication"
ms.assetid: 3fd983ad-e206-45db-9054-417c9b5bb815
author: "MashaMSFT"
ms.author: "mathoma"
---
# Heterogeneous Database Replication  
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports the following heterogeneous scenarios for transactional and snapshot replication:  
  
-   Publishing data from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers.  

-   Publishing data to and from Oracle has the following restrictions:  

  |Scenario|2016 or earlier |2017 or later |
  |-------|-------|--------|
  |Replication from Oracle |Only support Oracle 10g or earlier |Only support Oracle 10g or earlier |
  |Replication to Oracle |All versions prior to Oracle 12c |Not supported |


 Heterogeneous replication to non-SQL Server subscribers is deprecated. Oracle Publishing is deprecated. To move data, create solutions using change data capture and [!INCLUDE[ssIS](../../../includes/ssis-md.md)].  
  
> [!CAUTION]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
## Publishing Data from Oracle  
 You can use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to publish data from Oracle with most of the same features and ease-of-use as [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] snapshot and transactional replication. This feature requires Oracle version 10G or earlier. Publishing data from Oracle is ideal for the following scenarios:  
  
|Scenario|Description|  
|--------------|-----------------|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework application deployments|Develop with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Studio and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] while operating on data replicated from a non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.|  
|Data warehousing staging servers|Keep [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] staging databases synchronized with a non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.|  
|Migration to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]|Test your application in real time against [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] while replicating the source system's changes. Switch to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] when satisfied with the migration.|  
  
 For more information, see [Oracle Publishing Overview](../../../relational-databases/replication/non-sql/oracle-publishing-overview.md).  
  
## Publishing Data to Non-SQL Server Subscribers  
 The following non- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases are supported as Subscribers to snapshot and transactional publications:  
  
-   Oracle for all platforms that Oracle supports.  
  
-   IBM DB2 for AS400, MVS, Unix, Linux, and Windows.  
  
 For more information, see [Non-SQL Server Subscribers](../../../relational-databases/replication/non-sql/non-sql-server-subscribers.md).  
  
  

---
title: "Non-SQL Server Publishers | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "heterogeneous database replication, non-SQL Server Publishers"
  - "non-SQL Server Publishers"
  - "heterogeneous data sources, non-SQL Server Publishers"
  - "Publishers [SQL Server replication], Oracle"
ms.assetid: 08a160a6-33be-46b5-bc7b-d53180d8bdf1
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Non-SQL Server Publishers
  Publishing data from non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] sources allows you to consolidate data in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can subscribe to snapshot or transactional data published from an Oracle database. For more information about publishing from Oracle, see [Oracle Publishing Overview](oracle-publishing-overview.md).  
  
 Heterogeneous replication to non-SQL Server subscribers is deprecated. Oracle Publishing is deprecated. To move data, create solutions using change data capture and [!INCLUDE[ssIS](../../../includes/ssis-md.md)].  
  
> [!CAUTION]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
 Publishing from non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases is ideal for the following scenarios:  
  
|Scenario|Description|  
|--------------|-----------------|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework application deployments|Develop with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Studio and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] while operating on data replicated from a non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.|  
|Data warehousing staging servers|Keep [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] staging databases synchronized with a non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.|  
|Migration to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]|Test your application in real time against [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] while replicating the source system's changes. Switch to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] when satisfied with the migration.|  
  
## See Also  
 [Heterogeneous Database Replication](heterogeneous-database-replication.md)  
  
  

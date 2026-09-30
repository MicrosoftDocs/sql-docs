---
title: Interactive Conflict Resolution
description: Interactive conflict resolution in SQL Server Merge Replication lets you review and resolve each conflict manually. Learn how to enable the Interactive Resolver.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "interactive conflict resolution [SQL Server replication]"
  - "interactive resolver [SQL Server replication]"
  - "articles [SQL Server replication], conflict resolution"
  - "conflict resolution [SQL Server replication], merge replication"
---
# Advanced merge replication conflict - Interactive resolution
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication provides an Interactive Resolver, which you can use to resolve conflicts manually during on-demand synchronization in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows Synchronization Manager. When you activate it at run time, the Interactive Resolver provides a graphical interface that displays data for each conflicting row. It offers options for viewing and editing the conflict data, and resolving each conflict individually.  
  
 The Interactive Resolver resembles the Conflict Viewer. However, the Conflict Viewer displays the results of conflicts that are already resolved after merge synchronization. The Interactive Resolver displays each conflict prior to resolution, so you can determine the outcome of each conflict during merge synchronization. Someone should be available to monitor the Interactive Resolver when a conflict occurs.  
  
> [!NOTE]  
>  Interactive Resolution requires Windows Synchronization Manager. If you perform a synchronization outside of Windows Synchronization Manager (as a scheduled synchronization or an on demand synchronization in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or Replication Monitor), the system resolves conflicts automatically without user intervention, according to the resolver specified for the article. The Interactive Resolver doesn't display conflicts that involve logical records. To view information about these conflicts, use replication stored procedures. For more information, see [View Conflict Information for Merge Publications &#40;Replication Transact-SQL Programming&#41;](../view-and-resolve-data-conflicts-for-merge-publications.md).  
  
## Article resolvers and the Interactive Resolver  
 When you create a publication, you assign conflict resolvers (either the default resolver, a business logic handler, or a custom resolver) to specific articles. They use a set of rules to determine which set of data to use when conflicting row data is entered. The Interactive Resolver isn't a separate conflict resolver with rules for determining conflict winners and losers, but a tool used in conjunction with default and custom resolvers. The article resolver still determines the winning and losing row, but the Interactive Resolver allows user intervention to accept, reject, or modify the results.  
  
 To use the Interactive Resolver, you must enable interactive resolution for each article and subscription that requires it. After you enable it for one or more articles and subscriptions, the Interactive Resolver is used when a conflict is detected during merge synchronization.  
  
 To use the Interactive Resolver, see [Specify Merge Replication options](../../../relational-databases/replication/merge/specify-merge-replication-properties.md) and [Synchronize a Subscription Using Windows Synchronization Manager &#40;Windows Synchronization Manager&#41;](../../../relational-databases/replication/synchronize-a-subscription-using-windows-synchronization-manager.md).  
  
## Related content

- [Advanced Merge Replication - Conflict Detection and Resolution](advanced-merge-replication-conflict-detection-and-resolution.md)

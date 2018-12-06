---
title: "Transactions (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "transactions [Master Data Services], about transactions"
  - "transactions [Master Data Services]"
ms.assetid: 4cd2fa6f-9c76-4b7a-ae18-d4e5fd2f03f5
author: leolimsft
ms.author: lle
manager: craigg
---
# Transactions (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], a transaction is recorded each time action is taken on a member. Transactions can be viewed by all users and reversed by administrators. Transactions show the date, time, and user who took the action, along with other details. Users can add an annotation to a transaction, to indicate why a transaction took place.  
  
## When Transaction Are Recorded  
 Transactions are recorded when members:  
  
-   Are created, deleted, or reactivated.  
  
-   Have attribute values changed.  
  
-   Are moved in a hierarchy.  
  
 Transactions are not recorded when business rules change attribute values.  
  
## View and Manage Transactions  
 In the **Explorer** functional area, you can view and annotate (add comments to) the transactions that you made yourself.  
  
 In the **Version Management** functional area, administrators can view all transactions for all users for the models they have access to, and reverse any of these transactions.  
  
> [!NOTE]  
>  Administrators can view all transactions for all users as long as they don't have the read-only permission level applied in the **Version Management** functional area . For example, if the read-only permission and update permission level is set for the administrator, the administrator will not be able to see other user transactions because the read-only permission will take precedence over the update permission.

## System Settings  
 There is a setting in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] that affects whether or not transactions are recorded when records are staged. This setting affects SQL Server 2008 R2 only. You can adjust this setting in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] or directly in the System Settings table in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. For more information, see [System Settings &#40;Master Data Services&#41;](system-settings-master-data-services.md).  
  
 When importing data in this version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can specify whether or not to log transactions when initiating the stored procedure. For more information, see [Staging Stored Procedure &#40;Master Data Services&#41;](../../2014/master-data-services/staging-stored-procedure-master-data-services.md).  
  
## Concurrency  
 If a particular entity value is shown simultaneously in more than one Explorer session, concurrent edits to the same value are possible. Concurrent edits will not be detected automatically by MDS. This can occur when multiple users use the MDS Explorer in the Web browser from multiple sessions, for example from multiple computers, multiple browser tabs or windows, or multiple user accounts.  
  
 More than one user can update the same entity values without error despite transactions being enabled. Typically the last edit to the value in a sequence of time will take precedence. The duplicate edit conflict can be manually observed in the transaction history and can be reversed manually by the administrator. The transaction history will show the individual transactions for the **Prior value** and **New value** for the attribute in question from each session, but will not automatically resolve the conflict when multiple **New Values** exist for the same old value.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Undo an action by reversing a transaction (administrators only).|[Reverse a Transaction &#40;Master Data Services&#41;](../../2014/master-data-services/reverse-a-transaction-master-data-services.md)|  
  
## Related Content  
  
-   [Administrators &#40;Master Data Services&#41;](../../2014/master-data-services/administrators-master-data-services.md)  
  
-   [Annotations &#40;Master Data Services&#41;](../../2014/master-data-services/annotations-master-data-services.md)  
  
  

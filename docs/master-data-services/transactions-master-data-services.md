---
description: "Transactions (Master Data Services)"
title: Transactions
ms.custom: ""
ms.date: "01/10/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "transactions [Master Data Services], about transactions"
  - "transactions [Master Data Services]"
ms.assetid: 4cd2fa6f-9c76-4b7a-ae18-d4e5fd2f03f5
author: CordeliaGrey
ms.author: jiwang6
---
# Transactions (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]



--------------------------------------------------
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
  
 You can configure how long transaction log data is retained by setting the **Log retention in Days** property in system settings for the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database, and by setting **Log Retention Days** when you create or edit a model. For more information, see [System Settings &#40;Master Data Services&#41;](../master-data-services/system-settings-master-data-services.md) and [Create a Model &#40;Master Data Services&#41;](../master-data-services/create-a-model-master-data-services.md).  
  
 The SQL Server Agent job, MDS_MDM_Sample_Log_Maintenace, triggers cleanup of the transaction logs and runs every night. You can use SQL Server Agent to modify the schedule for this job.  
  
 You can also call the following stored procedures to clean the transaction logs.  
  
|Stored Procedure|Description|  
|----------------------|-----------------|  
|mdm.udpTransactionsCleanup|Cleans transaction history|  
|mdm.udpValidationsCleanup|Cleans validation History|  
|mdm.udpEntityStagingBatchTableCleanup|Cleans staging table|  
  
 **Sample**  
  
```  
DECLARE @CleanupOlderThanDate date = '2014-11-11',  
@ModelID INT = 7  
--Clean up Transaction Logs  
EXEC mdm.udpTransactionsCleanup @ModelID, @CleanupOlderThanDate;  
  
--Clean up Validation History  
EXEC mdm.udpValidationsCleanup @ModelID, @CleanupOlderThanDate;  
  
--Clean up EBS tables  
EXEC mdm.udpEntityStagingBatchTableCleanup @ModelID, @CleanupOlderThanDate;  
  
```  
  
## System Settings  
 There is a setting in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] that affects whether or not transactions are recorded when records are staged. You can adjust this setting in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] or directly in the System Settings table in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. For more information, see [System Settings &#40;Master Data Services&#41;](../master-data-services/system-settings-master-data-services.md).  
  
 When importing data in this version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can specify whether or not to log transactions when initiating the stored procedure. For more information, see [Staging Stored Procedure &#40;Master Data Services&#41;](../master-data-services/staging-stored-procedure-master-data-services.md).  
  
## Concurrency  
 If a particular entity value is shown simultaneously in more than one Explorer session, concurrent edits to the same value are possible. Concurrent edits will not be detected automatically by MDS. This can occur when multiple users use the MDS Explorer in the Web browser from multiple sessions, for example from multiple computers, multiple browser tabs or windows, or multiple user accounts.  
  
 More than one user can update the same entity values without error despite transactions being enabled. Typically the last edit to the value in a sequence of time will take precedence. The duplicate edit conflict can be manually observed in the transaction history and can be reversed manually by the administrator. The transaction history will show the individual transactions for the **Prior value** and **New value** for the attribute in question from each session, but will not automatically resolve the conflict when multiple **New Values** exist for the same old value.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Undo an action by reversing a transaction (administrators only).|[Reverse a Transaction &#40;Master Data Services&#41;](../master-data-services/reverse-a-transaction-master-data-services.md)|  
  
## External Resources  
 Blog post, [Transactions, Validation Issue and Staging table cleanup](https://techcommunity.microsoft.com/t5/sql-server-integration-services/transactions-validation-issue-and-staging-table-cleanup/ba-p/388209), on msdn.com.  
  
## Related Content  
  
-   [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md)  
  
-   [Annotations &#40;Master Data Services&#41;](../master-data-services/annotations-master-data-services.md)  
  
  

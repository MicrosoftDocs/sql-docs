---
title: "Outdated Backup | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 307a4ad0-675a-4f97-9a3c-cedd61bdfae5
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Outdated Backup
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This rule checks that a database has recent backups. Scheduling regular backups is important for protecting your databases against data loss from many different failures. The appropriate frequency for backing up data depends on the recovery model of the database, on business requirements about potential data loss, and on how frequently the database is updated. In a frequently updated database, the work-loss exposure increases fairly quickly between backups.  
  
## Best Practices Recommendations  
 We recommend that you perform backups frequently enough to protect databases against data loss.  
  
 The simple recovery model and full recovery model both require data backups. For either recovery model, you can supplement your full backups with differential backups to efficiently reduce the risk of data loss.  
  
 For a database that uses the full recovery model, we recommend that you take frequent log backups. For a production database that contains very important data, log backups would typically be taken every one to fifteen minutes.  
  
> [!NOTE]  
>  The recommended method for scheduling backups is a database maintenance plan.  
  
## For More Information  
 [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md)  
  
 [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md)  
  
 [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md)  
  
 [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)  
  
 [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  

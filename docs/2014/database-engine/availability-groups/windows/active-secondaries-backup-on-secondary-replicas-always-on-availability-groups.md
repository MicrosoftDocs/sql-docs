---
title: "Active Secondaries: Backup on Secondary Replicas (Always On Availability Groups) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "backup priority"
  - "backup on secondary replicas"
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], backup on secondary replicas"
  - "active secondary replicas [SQL Server], backup on"
  - "automated backup preference"
  - "Availability Groups [SQL Server], active secondary replicas"
ms.assetid: 82afe51b-71d1-4d5b-b20a-b57afc002405
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Active Secondaries: Backup on Secondary Replicas (Always On Availability Groups)
  The [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] active secondary capabilities include support for performing backup operations on secondary replicas. Backup operations can put significant strain on I/O and CPU (with backup compression). Offloading backups to a synchronized or synchronizing secondary replica allows you to use the resources on server instance that hosts the primary replica for your tier-1 workloads.  
  
> [!NOTE]  
>  RESTORE statements are not allowed on either the primary or secondary databases of an availability group.  
  
  
  
##  <a name="SupportedBuTypes"></a> Backup Types Supported on Secondary Replicas  
  
-   `BACKUP DATABASE` supports only copy-only full backups of databases, files, or filegroups when it is executed on secondary replicas. Note that copy-only backups do not impact the log chain or clear the differential bitmap.  
  
-   Differential backups are not supported on secondary replicas.  
  
-   **BACKUP LOG** supports only regular log backups (the COPY_ONLY option is not supported for log backups on secondary replicas).  
  
     A consistent log chain is ensured across log backups taken on any of the replicas (primary or secondary), irrespective of their availability mode (synchronous-commit or asynchronous-commit).  
  
-   To back up a secondary database, a secondary replica must be able to communicate with the primary replica and must be `SYNCHRONIZED` or `SYNCHRONIZING`.  
  
##  <a name="WhereBuJobsRun"></a> Configuring Where Backup Jobs Run  
 Performing backups on a secondary replica to offload the backup workload from the primary production server is a great benefit. However, performing backups on secondary replicas introduce significant complexity to the process of determining where backup jobs should run. To address this, configure where backup jobs run as follows:  
  
1.  Configure the availability group to specify which availability replicas where you would prefer backups to be performed. For more information, see *AUTOMATED_BACKUP_PREFERENCE* and *BACKUP_PRIORITY* parameters in [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-availability-group-transact-sql) or [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-availability-group-transact-sql).  
  
2.  Create scripted backup jobs for every availability database on every server instance that hosts an availability replica that is a candidate for performing backups. For more information, see the "Follow Up: After Configuring Backup on Secondary Replicas" section of [Configure Backup on Availability Replicas &#40;SQL Server&#41;](configure-backup-on-availability-replicas-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To configure backup on secondary replicas**  
  
-   [Configure Backup on Availability Replicas &#40;SQL Server&#41;](configure-backup-on-availability-replicas-sql-server.md)  
  
 **To determine whether the current replica is the preferred backup replica**  
  
-   [sys.fn_hadr_backup_is_preferred_replica](/sql/relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql)  
  
 **To create a backup job**  
  
-   [Use the Maintenance Plan Wizard](../../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md)  
  
-   [Implement Jobs](../../../ssms/agent/implement-jobs.md)  
  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Copy-Only Backups &#40;SQL Server&#41;](../../../relational-databases/backup-restore/copy-only-backups-sql-server.md)   
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-availability-group-transact-sql)   
 [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-availability-group-transact-sql)  
  
  

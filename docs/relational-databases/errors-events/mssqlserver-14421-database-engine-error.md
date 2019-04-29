---
title: "MSSQLSERVER_14421 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "14421 (Database Engine error)"
ms.assetid: 03e76d4a-d463-4673-8843-08e4ecaefe27
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_14421
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14421|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum14421|  
|Message Text|The log shipping secondary database %s.%s has restore threshold of %d minutes and is out of sync. No restore was performed for %d minutes. Restored latency is %d minutes. Check agent log and logshipping monitor information.|  
  
## Explanation  
This message indicates that log shipping is out of synchronization beyond the restore threshold. The restore threshold is the number of minutes that can elapse between restore operations before a message is generated.  
  
### Possible Causes  
This message does not necessarily indicate a problem with log shipping. Instead, this message might indicate one of the following problems:  
  
-   The restore job is not running.  
  
    Possible causes of the job not running include the following: the SQL Server Agent service on the secondary server instance is not running, the job is disabled, or the schedule of the job has been changed.  
  
-   The restore job is failing.  
  
    Possible causes of the job failing include the following: the restore folder path is not valid, the disk is full, or any other reason that the RESTORE statement could fail.  
  
## User Action  
To troubleshoot this message:  
  
-   Make sure that the SQL Server Agent service is running for the secondary server instance and that the restore job for this secondary database is enabled and is scheduled to run at the appropriate frequency.  
  
-   The restore job on the secondary server might be failing. In this case, check the job history for the restore job to look for the cause.  
  
-   The log shipping restore job, which runs on the secondary server instance, might not be able to connect to the monitor server instance to update the **log_shipping_monitor_secondary** table. This might be caused by an authentication problem between the monitor server instance and the secondary server instance.  
  
-   The backup alert threshold might have an incorrect value. Ideally, this value is set to at least three times the frequency of the restore job. If you change the frequency of the restore job after log shipping is configured and functional, you must update the value of the backup alert threshold accordingly.  
  
-   When the monitor server instance goes offline and then comes back online, the **log_shipping_monitor_secondary** table is not updated with the current values before the alert message job runs. Error 14421 can be raised when a restore job succeeds with, "Could not find a log backup file that could be applied to secondary database." When this occurs, the restore time is not updated. The cause of the error in this case might be an issue with the copy job.  
  
    To update the monitor tables with the latest data for the secondary database, run **sp_refresh_log_shipping_monitor** on the secondary server instance.  
  
-   On the secondary or monitor server instance, the date or time is incorrect. This may also generate alert messages. Possibly the system date or time was modified on the one of them.  
  
    > [!NOTE]  
    > Different time zones for the two server instances should not cause a problem.  
  
## See Also  
[log_shipping_monitor_secondary &#40;Transact-SQL&#41;](~/relational-databases/system-tables/log-shipping-monitor-secondary-transact-sql.md)  
[About Log Shipping &#40;SQL Server&#41;](~/database-engine/log-shipping/about-log-shipping-sql-server.md)  
[sp_help_log_shipping_monitor_secondary &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-secondary-transact-sql.md)  
[sp_refresh_log_shipping_monitor &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-transact-sql.md)  
[About Log Shipping &#40;SQL Server&#41;](~/database-engine/log-shipping/about-log-shipping-sql-server.md)  
  

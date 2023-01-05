---
description: "MSSQLSERVER_14420"
title: "MSSQLSERVER_14420 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "14420 (Database Engine error)"
ms.assetid: 4a1d72b1-ab1b-4119-a11b-a8a05c6fdb45
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_14420
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|14420|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum14420|  
|Message Text|The log shipping primary database %s.%s has backup threshold of %d minutes and has not performed a backup log operation for %d minutes. Check agent log and logshipping monitor information.|  
  
## Explanation  
Log shipping is out of synchronization beyond the backup threshold. The backup threshold is the number of minutes that are allowed to elapse between log-shipping backup jobs before an alert is generated. This message does not necessarily indicate a problem with log shipping. Instead, this message might indicate one of the following problems:  
  
-   The backup job is not running. Possible causes for this include the following: the SQL Server Agent service on the primary server instance is not running, the job is disabled, or the job's schedule has been changed.  
  
-   The backup job is failing. Possible causes for this include the following: the backup folder path is not valid, the disk is full, or any other reason that the BACKUP statement could fail.  
  
## User Action  
To troubleshoot this message:  
  
-   Make sure that the SQL Server Agent service is running for the primary server instance and that the backup job for this primary database is enabled and is scheduled to run at the appropriate frequency.  
  
-   The backup job on the primary server might be failing. In this case, examine the job history for the backup job to look for the cause.  
  
-   The log shipping backup job, which runs on the primary server instance, might not be able to connect to the monitor server instance to update the **log_shipping_monitor_primary** table. This could be caused by an authentication problem between the monitor server instance and the primary server instance.  
  
-   The backup alert threshold might have an incorrect value. Ideally, this value is set to at least three times the frequency of the backup job. If you change the frequency of the backup job after log shipping is configured and functional, you must update the value of the backup alert threshold accordingly.  
  
-   When the monitor server instance goes offline and then comes back online, the **log_shipping_monitor_primary** table is not updated with the current values before the alert message job runs. To update the monitor tables with the latest data for the primary database, run **sp_refresh_log_shipping_monitor** on the primary server instance.  
  
-   On the primary or monitor server instance, the date or time is incorrect. This may also generate alert messages. Possibly the system date or time was modified on the one of them.  
  
    > [!NOTE]  
    > Different time zones for the two server instances should not cause a problem.  
  
## See Also  
[log_shipping_monitor_primary &#40;Transact-SQL&#41;](~/relational-databases/system-tables/log-shipping-monitor-primary-transact-sql.md)  
[About Log Shipping &#40;SQL Server&#41;](~/database-engine/log-shipping/about-log-shipping-sql-server.md)  
[sp_help_log_shipping_monitor_primary &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-primary-transact-sql.md)  
[sp_refresh_log_shipping_monitor &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-transact-sql.md)  
[About Log Shipping &#40;SQL Server&#41;](~/database-engine/log-shipping/about-log-shipping-sql-server.md)  
  

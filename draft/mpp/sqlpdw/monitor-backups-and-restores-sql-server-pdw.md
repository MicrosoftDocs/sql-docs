---
title: "Monitor Backups and Restores (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0e9e89ca-4f9c-4538-9c38-1134d356e0db
caps.latest.revision: 8
author: BarbKess
---
# Monitor Backups and Restores (SQL Server PDW)
Active and recent backups and restores can be monitored by using either the [Monitor the Appliance by Using the Admin Console &#40;Analytics Platform System&#41;](../management/monitor-the-appliance-by-using-the-admin-console-analytics-platform-system.md) or the [System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md). See [Backup and Restore &#40;SQL Server PDW&#41;](../sqlpdw/backup-and-restore-sql-server-pdw.md) for information on how to use backup and restore on SQL Server PDW.  
  
## Prerequisites  
Regardless of the method used to monitor a backup or restore, the login must have permission to access the underlying data sources. See “Use All of the Admin Console” in [Grant Permissions to Use the Admin Console &#40;SQL Server PDW&#41;](../sqlpdw/grant-permissions-to-use-the-admin-console-sql-server-pdw.md) for the permissions to grant.  
  
## Monitoring Backups and Restores  
The following sections describe how to monitor both backups and restores.  
  
### To monitor backups and restores by using the Admin Console  
  
1.  Log on to the Admin Console. See [Monitor the Appliance by Using the Admin Console &#40;Analytics Platform System&#41;](../management/monitor-the-appliance-by-using-the-admin-console-analytics-platform-system.md) for instructions.  
  
2.  On the top menu, click **Backups/Restores**. You will see a sortable table showing all recent and active backups and restores plus additional information, such as whether the action has completed or is still active. Click the column headers to sort the rows.  
  
3.  To view additional details for a specific backup or restore, click **ID** in the left column. In the detailed view, you can see progress on each step of the backup or restore.  
  
See these system views for information on the metadata about the backup or restore that is shown in the Admin Console:  
  
-   [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md)  
  
-   [sys.pdw_loader_run_stages &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-run-stages-sql-server-pdw.md)  
  
-   [sys.pdw_loader_backup_runs &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-backup-runs-sql-server-pdw.md)  
  
-   [sys.pdw_loader_backup_run_details &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-backup-run-details-sql-server-pdw.md)  
  
### To monitor backups and restores by using system views  
For each system view used, see the documentation for that view for information on the columns and potential values returned by the view.  
  
1.  Find the `request_id` for the backup or restore in the [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md) view by finding the backup or restore command in the `command` column for this view.  
  
    For example,  
  
    ```  
    SELECT request_id, status, command FROM sys.dm_pdw_exec_requests;  
    ```  
  
    The preceding command returns the command text and the current status, plus `request_id`.  
  
2.  Use the `request_id` to retrieve additional information for the backup or restore, using the [sys.pdw_loader_run_stages &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-run-stages-sql-server-pdw.md), [sys.pdw_loader_backup_runs &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-backup-runs-sql-server-pdw.md), and [sys.pdw_loader_backup_run_details &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-backup-run-details-sql-server-pdw.md) views. For example, this query returns the `run_id` and information on the start, end, and duration times of the backup or restore, plus any errors, and information on the number of rows processed:  
  
    ```  
    SELECT lbr.run_id,   
    er.submit_time, er.end_time, er.total_elapsed_time, er.error_id, lbr.rows_processed   
    FROM sys.dm_pdw_exec_requests er   
    LEFT OUTER JOIN   
    sys.pdw_loader_backup_runs lbr   
    ON (er.request_id=lbr.requst_id)   
    WHERE er.request_id=’12738’;  
    ```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  

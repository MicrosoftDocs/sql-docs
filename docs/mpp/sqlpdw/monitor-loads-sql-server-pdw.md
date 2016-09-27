---
title: "Monitor Loads (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c0c55c16-00bc-4676-8970-a8e10b3e9408
caps.latest.revision: 6
author: BarbKess
---
# Monitor Loads (SQL Server PDW)
Active and recent dwloader loads can be monitored by using either the [Monitor the Appliance by Using the Admin Console &#40;Analytics Platform System&#41;](../management/monitor-the-appliance-by-using-the-admin-console-analytics-platform-system.md) or the [System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md). See [dwloader Command-Line Loader &#40;SQL Server PDW&#41;](../sqlpdw/dwloader-command-line-loader-sql-server-pdw.md) for information on the dwloader tool.  
  
> [!TIP]  
> Some loads are initiated by using INSERT statements or business intelligence tools that use SQL statements to perform the load. To monitor this type of load, see [Monitoring Active Queries &#40;SQL Server PDW&#41;](../sqlpdw/monitoring-active-queries-sql-server-pdw.md).  
  
## Prerequisites  
Regardless of the method used to monitor a load, the login must have permission to access the underlying data sources. For the permissions to grant, see “Use All of the Admin Console” in [Grant Permissions to Use the Admin Console &#40;SQL Server PDW&#41;](../sqlpdw/grant-permissions-to-use-the-admin-console-sql-server-pdw.md).  
  
## Monitoring Loads  
The following sections describe how to monitor loads.  
  
### To monitor loads by using the Admin Console  
  
1.  Log on to the Admin Console. See [Monitor the Appliance by Using the Admin Console &#40;Analytics Platform System&#41;](../management/monitor-the-appliance-by-using-the-admin-console-analytics-platform-system.md) for instructions.  
  
2.  On the top menu, click **Loads**. You will see a sortable table showing all recent and active loads plus additional information, such as whether the load has completed or is still active. Click the column headers to sort the rows.  
  
3.  To view additional details for a specific load, click the load **ID** in the left column. In the detailed view, you can see progress on each step of the load.  
  
See these system views for information on the metadata about the load that is shown in the Admin Console:  
  
-   [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md)  
  
-   [sys.pdw_loader_run_stages &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-run-stages-sql-server-pdw.md)  
  
-   [sys.pdw_loader_backup_runs &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-backup-runs-sql-server-pdw.md)  
  
-   [sys.pdw_loader_backup_run_details &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-backup-run-details-sql-server-pdw.md)  
  
### To monitor loads by using system views  
To monitor active and recent loads by using SQL Server PDW views, follow the steps below. For each system view used, see the documentation for that view for information on the columns and potential values returned by the view.  
  
1.  Find the `request_id` for the load in the [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md) view by finding the loader command line in the `command` column for this view.  
  
    For example, the following command returns the command text and current status, plus the `request_id`.  
  
    ```  
    SELECT request_id, status, command FROM sys.dm_pdw_exec_requests;  
    ```  
  
2.  Use the `request_id` to retrieve additional information for the load by using the [sys.pdw_loader_run_stages &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-run-stages-sql-server-pdw.md), [sys.pdw_loader_backup_runs &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-backup-runs-sql-server-pdw.md), and [sys.pdw_loader_backup_run_details &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-backup-run-details-sql-server-pdw.md) views. For example, the following query returns the `run_id` and information on the start, end, and duration times of the load, plus any errors, and information on the number of rows processed:  
  
    ```  
    SELECT lbr.run_id,   
    er.submit_time, er.end_time, er.total_elapsed_time, er.error_id, lbr.rows_processed, lbr.rows_rejected, lbr.rows_inserted   
    FROM sys.dm_pdw_exec_requests er   
    LEFT OUTER JOIN   
    sys.pdw_loader_backup_runs lbr   
    ON (er.request_id=lbr.requst_id)   
    WHERE er.request_id=’12738’;  
    ```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  

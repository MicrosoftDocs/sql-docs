---

title: "Monitor Loads for Parallel Data Warehouse"
description: "You can monitor active and recent [dwloader](dwloader.md) loads by using the Analytics Platform System (APS) Admin Console or the Parallel Data Warehouse (PDW) System Views."

author: "barbkess" 
ms.author: "barbkess"
ms.date: "10/20/2016"
ms.topic: "article"

ms.assetid: c0c55c16-00bc-4676-8970-a8e10b3e9408
caps.latest.revision: 6

---
# Monitor loads
You can monitor active and recent [dwloader](dwloader.md) loads by using the Analytics Platform System (APS) Admin Console or the Parallel Data Warehouse (PDW) [System Views](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-reference-tsql-system-views/). 
  
> [!TIP]  
> Some loads are initiated by using INSERT statements or business intelligence tools that use SQL statements to perform the load. 

<!-- MISSING LINKS
To monitor this type of load, see [Monitoring Active Queries](monitor-active-queries.md).  
-->
  
## Prerequisites  
Regardless of the method used to monitor a load, the login must have permission to access the underlying data sources. 

<!-- MISSING LINKS
For the permissions to grant, see “Use All of the Admin Console” in [Grant Permissions to Use the Admin Console](grant-permissions-admin-console.md). 

--> 
  
## Monitoring Loads  
The following sections describe how to monitor loads.  
  
### To monitor loads by using the Admin Console  
  
1.  Log on to the Admin Console. <!-- MISSING LINKS See [Monitor the Appliance by Using the Admin Console;](monitor-admin-console.md) for instructions. --> 
  
2.  On the top menu, click **Loads**. You will see a sortable table showing all recent and active loads plus additional information, such as whether the load has completed or is still active. Click the column headers to sort the rows.  
  
3.  To view additional details for a specific load, click the load **ID** in the left column. In the detailed view, you can see progress on each step of the load.  
  
See these system views for information on the metadata about the load that is shown in the Admin Console:  
  
-   [sys.dm_pdw_exec_requests](https://msdn.microsoft.com/library/mt203887.aspx)  
  
-   [sys.pdw_loader_run_stages](https://msdn.microsoft.com/library/mt203879.aspx.md)  
  
-   [sys.pdw_loader_backup_runs](https://msdn.microsoft.com/library/mt203884.aspx)  
  
-   [sys.pdw_loader_backup_run_details](https://msdn.microsoft.com/library/mt203877.aspx)  
  
### To monitor loads by using system views  
To monitor active and recent loads by using SQL Server PDW views, follow the steps below. For each system view used, see the documentation for that view for information on the columns and potential values returned by the view.  
  
1.  Find the `request_id` for the load in the [sys.dm_pdw_exec_requests](https://msdn.microsoft.com/library/mt203887.aspx) view by finding the loader command line in the `command` column for this view.  
  
    For example, the following command returns the command text and current status, plus the `request_id`.  
  
    ```  
    SELECT request_id, status, command FROM sys.dm_pdw_exec_requests;  
    ```  
  
2.  Use the `request_id` to retrieve additional information for the load by using the [sys.pdw_loader_run_stages](https://msdn.microsoft.com/library/mt203879.aspx.md), [sys.pdw_loader_backup_runs](https://msdn.microsoft.com/library/mt203884.aspx) , and [sys.pdw_loader_backup_run_details](https://msdn.microsoft.com/library/mt203877.aspx) views. For example, the following query returns the `run_id` and information on the start, end, and duration times of the load, plus any errors, and information on the number of rows processed:  
  
    ```  
    SELECT lbr.run_id,   
    er.submit_time, er.end_time, er.total_elapsed_time, er.error_id, lbr.rows_processed, lbr.rows_rejected, lbr.rows_inserted   
    FROM sys.dm_pdw_exec_requests er   
    LEFT OUTER JOIN   
    sys.pdw_loader_backup_runs lbr   
    ON (er.request_id=lbr.requst_id)   
    WHERE er.request_id=’12738’;  
    ```  
  
<!-- MISSING LINKS

## See Also  
[Common metadata query examples](metadata-query-examples.md)
-->  
  

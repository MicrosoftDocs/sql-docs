---
title: Monitor active queries
description: Use the Admin Console and Parallel Data Warehouse system views to monitor active queries on Analytics Platform System.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Monitoring active queries - Parallel Data Warehouse
This article shows how to use the Admin Console and the SQL Server PDW system views to monitor active queries. See [Monitor the Appliance by Using the Admin Console](monitor-the-appliance-by-using-the-admin-console.md) and [System Views](tsql-system-views.md) for information on these tools.  
  
## Prerequisites  
Regardless of the method used to monitor active queries, the login must have the permissions described in "Use All of the Admin Console" in [Grant Permissions to Use the Admin Console](grant-permissions.md#grant-permissions-to-use-the-admin-console).  
  
## <a name="PermsAdminConsole"></a>Monitor Active Queries  
Both the Admin Console and the SQL Server PDW system views can be used to monitor active queries. Follow the instructions below.  
  
### To monitor active queries by using the Admin Console  
  
1.  Log on to the Admin Console. See [Monitor the Appliance by Using the Admin Console](monitor-the-appliance-by-using-the-admin-console.md) for instructions.  
  
2.  On the top menu, click **Queries**. You will see a table with basic information about the most recent queries on the appliance, including the login who submitted the query, the start and end times for the query, and the current status of the query.  
  
3.  To see the query command, hover the mouse pointer over the query ID in the left column for that row.  
  
    To see more detailed information for a particular query, click the query ID. You will see information including the full query and the query plan, with status information for each step in the query execution. If any errors were returned, you can also see detailed information on the errors. <!-- MISSING LINKS See [Understanding Query Plans &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-plans-sql-server-pdw.md) for information on how to interpret the query plan information available in the Admin Console.  -->
  
### To monitor active queries by using the system views  
The primary system view used to monitor queries is [sys.dm_pdw_exec_requests](../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md). Use this system view to find the `request_id` for an active or recent query, based on the query text.  
  
For example, the following query finds the `request_id` and the current `status` for any query that selects all columns from the `memberAddresses` table.  
  
```sql  
SELECT request_id, command, status   
FROM sys.dm_pdw_exec_requests   
WHERE command   
LIKE '%SELECT * FROM db1..memberAddresses%';  
```  
  
After the `request_id` has been identified for a query, use the other information in the `dm_pdw_exec_requests` table to find out about the processing of the query, or use [sys.dm_pdw_request_steps](../relational-databases/system-dynamic-management-views/sys-dm-pdw-request-steps-transact-sql.md) to view the status of the individual query steps for the query execution.  
  
<!-- MISSING LINKS 
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
  

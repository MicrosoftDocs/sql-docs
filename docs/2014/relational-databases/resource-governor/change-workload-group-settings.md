---
title: "Change Workload Group Settings | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "workload groups [SQL Server], alter"
  - "Resource Governor, workload group alter"
ms.assetid: 73b6109c-2ad0-4915-b11b-d40d5a0fdc25
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Change Workload Group Settings
  You can change workload group settings by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To change the settings for a workload group, using:**  [SQL Server Management Studio](#ChgWGProp), [Transact-SQL](#ChgWGTSQL)  
  
## Before You Begin  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 You can change the settings of the default workload group and user-defined workload groups.  
  
 **REQUEST_MAX_MEMORY_GRANT_PERCENT**  
  
 The memory consumed by index creation on a non-aligned partitioned table is proportional to the number of partitions involved. If the total required memory exceeds the per-query limit, (REQUEST_MAX_MEMORY_GRANT_PERCENT) imposed by the workload group setting, this index creation may fail. Because the default workload group allows a query to exceed the per-query limit with the minimum required memory to start for SQL Server 2005 compatibility, the user may be able to run the same index creation in the default workload group, if the default resource pool has enough total memory configured to run such a query.  
  
 Index creation is allowed to use more memory workspace than initially granted for performance. This special handling is supported by Resource Governor, however, the initial grant and any additional memory grant are limited by the workload group and resource pool settings.  
  
###  <a name="Permissions"></a> Permissions  
 Changing workload group settings requires CONTROL SERVER permission.  
  
##  <a name="ChgWGProp"></a> Change Workload Group Settings Using SQL Server Management Studio  
 **To change workload group settings by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]**  
  
1.  In Object Explorer, recursively expand the **Management** node down to and including the **Workload Groups** folder that contains the workload group to be modified.  
  
2.  Right-click the workload group to be modified, and then click **Properties**.  
  
3.  In the **Resource Governor Properties** page, select the row for the workload group in the **Workload groups for resource pool** grid if it is not automatically selected.  
  
4.  Click or double-click the cells in the row to be changed, and enter the new values.  
  
5.  To save the changes, click **OK**  
  
##  <a name="ChgWGTSQL"></a> Change Workload Group Settings Using Transact-SQL  
 **To change workload group settings by using Transact-SQL**  
  
1.  Run the ALTER WORKLOAD GROUP statement specifying the property values to be changed.  
  
2.  Run the ALTER RESOURCE GOVERNOR RECONFIGURE statement.  
  
### Example (Transact-SQL)  
 The following example changes the max memory grant percent setting for the workload group named `groupAdhoc`.  
  
```  
ALTER WORKLOAD GROUP groupAdhoc  
WITH (REQUEST_MAX_MEMORY_GRANT_PERCENT = 30);  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also  
 [Resource Governor](resource-governor.md)   
 [Create a Workload Group](create-a-workload-group.md)   
 [Create a Resource Pool](create-a-resource-pool.md)   
 [Change Resource Pool Settings](change-resource-pool-settings.md)   
 [ALTER WORKLOAD GROUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-workload-group-transact-sql)   
 [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-resource-pool-transact-sql)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-resource-governor-transact-sql)  
  
  

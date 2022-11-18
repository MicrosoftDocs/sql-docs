---
title: "Create a Workload Group | Microsoft Docs"
description: Learn how to create a workload group by using SQL Server Management Studio or Transact-SQL. You must have the CONTROL SERVER permission.
ms.custom: ""
ms.date: "03/17/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Resource Governor, workload group create"
  - "workload groups [SQL Server], create"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Create a Workload Group

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  You can create a workload group by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To create a workload group, using:**  [SQL Server Management Studio](#CreRPProp), [Transact-SQL](#CreRPTSQL)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions

 **REQUEST_MAX_MEMORY_GRANT_PERCENT**  
  
 The memory consumed by index creation on a non-aligned partitioned table is proportional to the number of partitions involved. If the total required memory exceeds the per-query limit, (REQUEST_MAX_MEMORY_GRANT_PERCENT) imposed by the workload group setting, this index creation may fail. Because the default workload group allows a query to exceed the per-query limit with the minimum required memory to start for SQL Server 2005 compatibility, the user may be able to run the same index creation in the default workload group, if the default resource pool has enough total memory configured to run such a query.  
  
 Index creation is allowed to use more memory workspace than initially granted for performance. This special handling is supported by Resource Governor, however, the initial grant and any additional memory grant are limited by the workload group and resource pool settings.  
  
###  <a name="Permissions"></a> Permissions

 Creating a workload group requires CONTROL SERVER permission.  
  
##  <a name="CreRPProp"></a> Create a Workload Group Using SQL Server Management Studio

 **To create a workload group by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]**  
  
1.  In Object Explorer, recursively expand the **Management** node down to and including the resource pool that contains the workload group to be modified.  
  
2.  Right-click the **Workload Groups** folder, and then click **New Workload Group**.  
  
3.  In the **Resource pools** grid, ensure the resource pool where you want to add the workload group is highlighted.  
  
4.  The **Workload groups for resource pool** grid will have a new line with a blank name and default values in the other columns.  
  
5.  Click the **Name** cell and enter a name for the workload group.  
  
6.  Click or double-click any other cells in the row you want to change from their default settings, and enter the new values.  
  
7.  To save the changes, click **OK**  

##  <a name="CreRPTSQL"></a> Create a Workload Group Using Transact-SQL  
 **To create a workload group by using [!INCLUDE[tsql](../../includes/tsql-md.md)]**  
  
1.  Run the CREATE WORKLOAD GROUP statement specifying the property values to be set.  
  
2.  Run the ALTER RESOURCE GOVERNOR RECONFIGURE statement.  
  
### Example (Transact-SQL)

 The following example creates a workload group named `groupAdhoc` in the resource pool named `poolAdhoc`.  
  
```sql
CREATE WORKLOAD GROUP groupAdhoc  
USING poolAdhoc;  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also

 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [Enable Resource Governor](../../relational-databases/resource-governor/enable-resource-governor.md)   
 [Create a Resource Pool](../../relational-databases/resource-governor/create-a-resource-pool.md)   
 [Change Workload Group Settings](../../relational-databases/resource-governor/change-workload-group-settings.md)   
 [Create and Test a Classifier User-Defined Function](../../relational-databases/resource-governor/create-and-test-a-classifier-user-defined-function.md)   
 [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)   
 [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md)  
  
  

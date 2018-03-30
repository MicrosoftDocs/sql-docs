---
title: "Create a Resource Pool | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "resource pools [SQL Server], create"
  - "Resource Governor, resource pool create"
ms.assetid: 44dd0567-a4c8-4c72-89ff-e76f6ddef344
caps.latest.revision: 18
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Create a Resource Pool
  You can create a resource pool by using [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../includes/tsql-md.md)].  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To create a resource pool, using:**  [SQL Server Management Studio](#CreRPProp), [Transact-SQL](#CreRPTSQL)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 The maximum CPU percentage must be equal to or higher than the minimum CPU percentage. The maximum memory percentage must be equal to or higher than the minimum memory percentage.  
  
 The sums of the minimum CPU percentages and minimum memory percentages for all resource pools must not exceed 100.  
  
###  <a name="Permissions"></a> Permissions  
 Creating a resource pool requires CONTROL SERVER permission.  
  
##  <a name="CreRPProp"></a> Create a Resource Pool Using SQL Server Management Studio  
 **To create a resource pool by using [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]**  
  
1.  In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to and including **Resource Governor**.  
  
2.  Right-click **Resource Governor**, and then click **Properties**.  
  
3.  In the **Resource pools** grid, click the first column in the empty row. This column is labeled with an asterisk (*).  
  
4.  Double-click the empty cell in the **Name** column. Type in the name that you want to use for the resource pool.  
  
5.  Click or double-click any other cells in the row you want to change, and enter the new values.  
  
6.  To save the changes, click **OK**  
  
##  <a name="CreRPTSQL"></a> Create a Resource Pool Using Transact-SQL  
 **To create a resource pool by using [!INCLUDE[tsql](../includes/tsql-md.md)]**  
  
1.  Run the `CREATE RESOURCE POOL` statement specifying the property values to be set.  
  
2.  Run the **ALTER RESOURCE GOVERNOR RECONFIGURE** statement.  
  
### Example (Transact-SQL)  
 The following example creates a resource pool named `poolAdhoc`.  
  
```  
CREATE RESOURCE POOL poolAdhoc  
WITH (MAX_CPU_PERCENT = 20);  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also  
 [Resource Governor](../../2014/database-engine/resource-governor.md)   
 [Enable Resource Governor](../../2014/database-engine/enable-resource-governor.md)   
 [Resource Governor Resource Pool](../../2014/database-engine/resource-governor-resource-pool.md)   
 [Change Resource Pool Settings](../../2014/database-engine/change-resource-pool-settings.md)   
 [Delete a Resource Pool](../../2014/database-engine/delete-a-resource-pool.md)   
 [Configure Resource Governor Using a Template](../../2014/database-engine/configure-resource-governor-using-a-template.md)   
 [Resource Governor Workload Group](../../2014/database-engine/resource-governor-workload-group.md)   
 [Resource Governor Classifier Function](../../2014/database-engine/resource-governor-classifier-function.md)   
 [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](~/t-sql/statements/create-resource-pool-transact-sql.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](~/t-sql/statements/alter-resource-governor-transact-sql.md)  
  
  
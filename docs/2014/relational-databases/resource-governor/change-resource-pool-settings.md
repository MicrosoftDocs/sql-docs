---
title: "Change Resource Pool Settings | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Resource Governor, resource pool alter"
  - "resource pools [SQL Server], alter"
ms.assetid: 49438285-a011-4dac-bd4f-f35cd90fda61
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Change Resource Pool Settings
  You can change resource pool settings by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To change the settings for a resource pool, using:**  [SQL Server Management Studio](#ChgRPProp), [Transact-SQL](#ChgRPTSQL)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 The maximum CPU percentage must be equal to or higher than the minimum CPU percentage. The maximum memory percentage must be equal to or higher than the minimum memory percentage.  
  
 The sums of the minimum CPU percentages and minimum memory percentages for all resource pools must not exceed 100.  
  
###  <a name="Permissions"></a> Permissions  
 Changing resource pool settings requires CONTROL SERVER permission.  
  
##  <a name="ChgRPProp"></a> Change Resource Pool Settings Using SQL Server Management Studio  
 **To change resource pool settings by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]**  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to and including **Resource Pools**.  
  
2.  Right-click the resource pool to be modified, and then click **Properties**.  
  
3.  In the **Resource Governor Properties** page, select the row for the resource pool in the **Resource pools** grid if it is not automatically selected.  
  
4.  Click or double-click the cells in the row to be changed, and enter the new values.  
  
5.  To save the changes, click **OK**  
  
##  <a name="ChgRPTSQL"></a> Change Resource Pool Settings Using Transact-SQL  
 **To change resource pool settings by using [!INCLUDE[tsql](../../includes/tsql-md.md)]**  
  
1.  Run the `ALTER RESOURCE POOL` statement specifying the property values to be changed.  
  
2.  Run the **ALTER RESOURCE GOVERNOR RECONFIGURE** statement.  
  
### Example (Transact-SQL)  
 The following example changes the max CPU percentage setting for the resource pool named `poolAdhoc`.  
  
```  
ALTER RESOURCE POOL poolAdhoc  
WITH (MAX_CPU_PERCENT = 25);  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also  
 [Resource Governor](resource-governor.md)   
 [Enable Resource Governor](enable-resource-governor.md)   
 [Create a Resource Pool](create-a-resource-pool.md)   
 [Delete a Resource Pool](delete-a-resource-pool.md)   
 [Resource Governor Workload Group](resource-governor-workload-group.md)   
 [Resource Governor Classifier Function](resource-governor-classifier-function.md)   
 [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-resource-pool-transact-sql)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-resource-governor-transact-sql)  
  
  

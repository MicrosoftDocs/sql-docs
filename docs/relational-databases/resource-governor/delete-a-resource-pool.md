---
title: "Delete a Resource Pool | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Resource Governor, resource pool delete"
  - "resource pools [SQL Server], delete"
ms.assetid: 3bdd348b-6582-4ffa-80ef-d49e50596ce5
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Delete a Resource Pool
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  You can delete a resource pool by using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or Transact-SQL.  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To delete a resource pool, using:** [SQL Server Management Studio](#DelRPSSMS), [Transact-SQL](#DelRPTSQL)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 You cannot delete a resource pool if it contains workload groups.  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 You cannot delete the Resource Governor default or internal resource pools. You cannot delete a resource pool if it contains workload groups. For more information, see [Delete a Workload Group](../../relational-databases/resource-governor/delete-a-workload-group.md).  
  
###  <a name="Permissions"></a> Permissions  
 Deleting a resource pool requires CONTROL SERVER permission.  
  
##  <a name="DelRPSSMS"></a> Delete a Resource Pool Using Object Explorer  
 **To delete a resource pool by using SQL Server Management Studio**  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to and including **Resource Governor**.  
  
2.  Right-click the resource pool to be deleted, and then click **Delete**.  
  
3.  In the **Delete Object** window, the resource pool is listed in the **Object to be deleted** list. To delete the resource pool, click **OK**.  
  
    > [!NOTE]  
    >  If the resource pool that you are trying to delete contains a workload group, this action will fail.  
  
##  <a name="DelRPTSQL"></a> Delete a Resource Pool Using Transact-SQL  
 **To delete a resource pool by using Transact-SQL**  
  
1.  Run the **DROP RESOURCE POOL** or **DROP EXTERNAL RESOURCE POOL** statement specifying the name of the resource pool to delete.  
  
2.  Run the **ALTER RESOURCE GOVERNOR RECONFIGURE** statement.  
  
### Example (Transact-SQL)  
 The following example drops a workload group named `poolAdhoc`.  
  
```  
DROP RESOURCE POOL poolAdhoc;  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also  
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)   
 [Create a Resource Pool](../../relational-databases/resource-governor/create-a-resource-pool.md)   
 [Change Resource Pool Settings](../../relational-databases/resource-governor/change-resource-pool-settings.md)   
 [Resource Governor Workload Group](../../relational-databases/resource-governor/resource-governor-workload-group.md)   
 [Resource Governor Classifier Function](../../relational-databases/resource-governor/resource-governor-classifier-function.md)   
 [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/drop-workload-group-transact-sql.md)   
 [DROP RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-resource-pool-transact-sql.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)   
 [DROP EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-external-resource-pool-transact-sql.md)   
 [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md)   
 [ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)  
  
  

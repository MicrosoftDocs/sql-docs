---
title: "Delete a Workload Group | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "workload groups [SQL Server], delete"
  - "Resource Governor, workload group delete"
ms.assetid: d5902c46-5c28-4ac1-8b56-cb4ca2b072d0
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Delete a Workload Group
  You can delete a workload group or resource pool by using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or Transact-SQL.  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To delete a workload group, using:**  [Object Explorer](#DelWGObjEx), [Resource Governor Properties](#DelWGRGProp), [Transact-SQL](#DelWGTSQL)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 You cannot delete a workload group if it contains active sessions.  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 If a workload group contains active sessions, deleting or moving the workload group to a different resource pool will fail when the ALTER RESOURCE GOVERNOR RECONFIGURE statement is called to apply the change. To avoid this problem, you can take one of the following actions:  
  
-   Wait until all the sessions from the affected group have disconnected, and then rerun the ALTER RESOURCE GOVERNOR RECONFIGURE statement.  
  
-   Explicitly stop sessions in the affected group by using the KILL command, and then rerun the ALTER RESOURCE GOVERNOR RECONFIGURE statement. If you decide that you do not want to explicitly stop sessions after you use **Delete** but before you stop active sessions, re-create the group by using the original name and move the group to the original resource pool.  
  
-   Restart the server. After the restart process is completed, the deleted group will not be created, and a moved group will use the new resource pool assignment.  
  
###  <a name="Permissions"></a> Permissions  
 Deleting a workload group requires CONTROL SERVER permission.  
  
##  <a name="DelWGObjEx"></a> Delete a Workload Group Using Object Explorer  
 **To delete a workload group by using Object Explorer**  
  
1.  In[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to and including **Resource Pools**.  
  
2.  Recursively expand **Resource Pools** down to and including the **Workload Groups** node in the resource pool that contains the workload group to be deleted.  
  
3.  Right-click the workload group, and then click **Delete**.  
  
4.  In the **Delete Object** window, the workload group is listed in the **Object to be deleted** list. To delete the workload group, click **OK**.  
  
##  <a name="DelWGRGProp"></a> Delete a Workload Group Using Resource Governor Properties  
 **To delete a workload group by using the Resource Governor Properties page**  
  
1.  In Object Explorer, recursively expand the **Management** node down to and including **Resource Pools**.  
  
2.  Right-click the resource pool that contains the workload group to be deleted, and then click **Properties**. This opens the **Resource Governor Properties** page.  
  
3.  In the **Workload groups for resource pool** window, click the line for the workload group to be deleted, then right-click the right arrow on the left side of the line, and then click **Delete**.  
  
4.  To delete the workload group, click **OK**.  
  
##  <a name="DelWGTSQL"></a> Delete a Workload Group Using Transact-SQL  
 **To delete a workload group by using Transact-SQL**  
  
1.  Run the `DROP WORKLOAD GROUP` statement specifying the name of the workload group to delete.  
  
2.  Before you issue the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement, verify that there are no active requests in the workload group being deleted. If there are active requests, `ALTER RESOURCE GOVERNOR` will fail. To avoid this issue, you can take one of the following actions:  
  
    -   Wait until all the sessions from the workload group have disconnected.  
  
    -   Explicitly stop sessions in the workload group by using the `KILL` command.  
  
    -   Restart the server. The workload group will not be re-created.  
  
    -   In a scenario in which you have issued the `DROP WORKLOAD GROUP` statement but decide that you do not want to explicitly stop sessions to apply the change, you can re-create the group by using the same name that it had before you issued the DROP statement, and then move the group to the original resource pool.  
  
3.  Run the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement.  
  
### Example (Transact-SQL)  
 The following example drops a workload group named `groupAdhoc`.  
  
```  
DROP WORKLOAD GROUP groupAdhoc;  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also  
 [Resource Governor](resource-governor.md)   
 [Create a Resource Pool](create-a-resource-pool.md)   
 [Create a Workload Group](create-a-workload-group.md)   
 [Delete a Resource Pool](delete-a-resource-pool.md)   
 [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-workload-group-transact-sql)   
 [DROP RESOURCE POOL &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-resource-pool-transact-sql)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-resource-governor-transact-sql)  
  
  

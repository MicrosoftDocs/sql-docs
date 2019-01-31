---
title: "Disable Resource Governor | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Resource Governor, disabling"
ms.assetid: 2c2d2db0-34a5-4f50-b783-17693e3ce3f1
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Disable Resource Governor
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You can disable the Resource Governor by using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or Transact-SQL.  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To disable Resource Governorn, using:**  [Object Explorer](#RGOffObjEx), [Resource Governor Properties](#RGOffProp), [Transact-SQL](#RGOffTSQL)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 Disabling the Resource Governor has the following results:  
  
-   The classifier function is not run.  
  
-   All new connections are automatically classified into the default workload group.  
  
-   System-initiated requests are classified into the internal workload group.  
  
-   All existing workload group and resource pool settings are reset to their default values. In this case, no events are fired when limits are reached.  
  
-   Normal system monitoring is not affected.  
  
-   Configuration changes can be made, but the changes do not take effect until the Resource Governor is enabled.  
  
-   Upon restarting SQL Server, the Resource Governor will not load its configuration, but instead will have only the default and internal workload groups and resource pools.  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 You cannot use the **ALTER RESOURCE GOVERNOR** statement to disable Resource Governor when in a user transaction.  
  
###  <a name="Permissions"></a> Permissions  
 Disabling the Resource Governor requires CONTROL SERVER permission.  
  
##  <a name="RGOffObjEx"></a> Disable Resource Governor Using Object Explorer  
 **To disable the Resource Governor by using Object Explorer**  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to **Resource Governor**.  
  
2.  Right-click **Resource Governor**, and then click **Disable**.  
  
##  <a name="RGOffProp"></a> Disable Resource Governor Using Resource Governor Properties  
 **To disable the Resource Governor by using the Resource Governor Properties page**  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to **Resource Governor**.  
  
2.  Right-click **Resource Governor** and then click **Properties**, this opens the **Resource Governor Properties** page.  
  
3.  Click the **Enable Resource Governor** check box, ensure that the box is not selected, and then click **OK**.  
  
##  <a name="RGOffTSQL"></a> Disable Resource Governor Using Transact-SQL  
 **To disable the Resource Governor by using Transact-SQL**  
  
1.  Run the **ALTER RESOURCE GOVERNOR DISABLE** statement.  
  
### Example (Transact-SQL)  
 The following example enables the Resource Governor.  
  
```  
ALTER RESOURCE GOVERNOR DISABLE;  
GO  
```  
  
## See Also  
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [Enable Resource Governor](../../relational-databases/resource-governor/enable-resource-governor.md)   
 [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)   
 [Resource Governor Workload Group](../../relational-databases/resource-governor/resource-governor-workload-group.md)   
 [Resource Governor Classifier Function](../../relational-databases/resource-governor/resource-governor-classifier-function.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)  
  
  

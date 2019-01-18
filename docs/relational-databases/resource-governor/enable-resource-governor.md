---
title: "Enable Resource Governor | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Resource Governor, enabling"
ms.assetid: 4d17af53-cf11-4ce4-aab4-deda94a49836
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Enable Resource Governor
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The Resource Governor is turned off by default. You can enable the Resource Governor by using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or Transact-SQL.  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To enable Resource Governorn, using:**  [Object Explorer](#RGOnObjEx), [Resource Governor Properties](#RGOnProp), [Transact-SQL](#RGOnTSQL)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 Enabling the resource governor has the following results:  
  
-   The classifier function is run for new connections so that their workloads can be assigned to workload groups.  
  
-   The resource limits that are specified in the Resource Governor configuration are honored and enforced.  
  
-   Requests that existed before enabling Resource Governor are affected by any configuration changes that were made when the Resource Governor was disabled.  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 You cannot use the **ALTER RESOURCE GOVERNOR** statement to enable Resource Governor when in a user transaction.  
  
###  <a name="Permissions"></a> Permissions  
 Enabling the Resource Governor requires CONTROL SERVER permission.  
  
##  <a name="RGOnObjEx"></a> Enable Resource Governor Using Object Explorer  
 **To enable the Resource Governor by using Object Explorer**  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to **Resource Governor**.  
  
2.  Right-click **Resource Governor**, and then click **Enable**.  
  
##  <a name="RGOnProp"></a> Enable Resource Governor Using Resource Governor Properties  
 **To enable the Resource Governor by using the Resource Governor Properties page**  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to **Resource Governor**.  
  
2.  Right-click **Resource Governor** and then click **Properties**, this opens the **Resource Governor Properties** page.  
  
3.  Click the **Enable Resource Governor** check box, and then click **OK**.  
  
##  <a name="RGOnTSQL"></a> Enable Resource Governor Using Transact-SQL  
 **To enable the Resource Governor by using Transact-SQL**  
  
1.  Run the **ALTER RESOURCE GOVERNOR RECONFIGURE** statement.  
  
### Example (Transact-SQL)  
 The following example enables the Resource Governor.  
  
```  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also  
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [Disable Resource Governor](../../relational-databases/resource-governor/disable-resource-governor.md)   
 [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)   
 [Resource Governor Workload Group](../../relational-databases/resource-governor/resource-governor-workload-group.md)   
 [Resource Governor Classifier Function](../../relational-databases/resource-governor/resource-governor-classifier-function.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)  
  
  

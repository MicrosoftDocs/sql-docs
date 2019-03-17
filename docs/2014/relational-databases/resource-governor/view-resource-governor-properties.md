---
title: "View Resource Governor Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.rg.properties.f1"
helpviewer_keywords: 
  - "Resource Governor, properties"
ms.assetid: de3510df-f792-4a9d-80fa-f198fd36cdc8
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# View Resource Governor Properties
  You can create or configure Resource Governor entities, such as resource pools and workload groups, by using the Resource Governor Properties page in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
1.  **Before you begin:**  [Permissions](#Permissions)  
  
2.  **To view Resource Governor properties, using:**  [Resource Governor Properties page](#ViewRGProp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 In addition to viewing the properties of Resource Governor entities, you can perform several configuration tasks using the **Resource Governor Properties** page. For more information, see these topics:  
  
-   [Enable Resource Governor](enable-resource-governor.md)  
  
-   [Disable Resource Governor](disable-resource-governor.md)  
  
-   [Create a Resource Pool](create-a-resource-pool.md)  
  
-   [Create a Workload Group](create-a-workload-group.md)  
  
-   [Change Resource Pool Settings](change-resource-pool-settings.md)  
  
-   [Change Workload Group Settings](change-workload-group-settings.md)  
  
-   [Move a Workload Group](move-a-workload-group.md)  
  
 When you click **OK** after adding, deleting, or moving a workload group or resource pool, the ALTER RESOURCE GOVERNOR RECONFIGURE statement is executed.  
  
 If the create or reconfigure operation for the resource pool or workload group fails, a summary error message appears below the title of the property page. To see a detailed error message, click the down arrow on the error message.  
  
 You can determine whether there is a configuration pending by querying the [sys.dm_resource_governor_configuration](/sql/relational-databases/system-dynamic-management-views/sys-dm-resource-governor-configuration-transact-sql) dynamic management view to get the current status of is_configuration_pending.  
  
###  <a name="Permissions"></a> Permissions  
 Viewing resource governor properties requires VIEW SERVER STATER permission. The resource governor configuration tasks require CONTROL SERVER permission.  
  
##  <a name="ViewRGProp"></a> View the Resource Governor Properties Page  
 **To view resource governor properties by using the Resource Governor Properties page in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]**  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to **Resource Governor**.  
  
2.  Right-click **Resource Governor** and then click **Properties**, this opens the **Resource Governor Properties** page.  
  
3.  For descriptions of the fields in the page, see [Resource Governor Properties](#RGProp).  
  
4.  To save any changes, click **OK**.  
  
##  <a name="RGProp"></a> Resource Governor Properties  
 **Classifier function name**  
 Specify the classifier function by selecting from the list.  
  
 **Enable Resource Governor**  
 Enable or disable Resource Governor by selecting or clearing the check box.  
  
 **Resource pools**  
 Create or change resource pool configuration by using the grid that is provided. This grid is populated with information for the predefined internal and default pools. Select a pool to work with by clicking the first column in the row for the pool. To create a new resource pool, click the row that is prefixed by the asterisk (**&#42;**).  
  
 **Name**  
 Specify the name of the resource pool.  
  
 **Minimum CPU %**  
 Specify the guaranteed average CPU bandwidth for all requests in the resource pool when there is CPU contention. Range is 0 to 100.  
  
 **Maximum CPU %**  
 Specify the maximum average CPU bandwidth that all requests in this resource pool will receive when there is CPU contention. Range is 0 to 100. The default setting is 100.  
  
 **Minimum Memory %**  
 Specify the minimum amount of memory reserved for this resource pool that can not be shared with other resource pools. Range is 0 to 100.  
  
 **Maximum Memory %**  
 Specify the total server memory that can be used by requests in this resource pool. Range is 0 to 100. The default setting is 100.  
  
 For more information, see [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-resource-pool-transact-sql).  
  
 **Workload groups for resource pool**  
 Create or change the workload group configuration by using the grid that is provided. This grid is populated with information for the predefined internal and default groups. Select a group to work with by clicking the first column in the row for the pool. To create a new workload group, click the row that is prefixed by the asterisk (**&#42;**).  
  
 **Name**  
 Specify the name of the workload group.  
  
 **Importance**  
 Specify the relative importance of a request in the workload group. Available settings are Low, Medium, and High.  
  
 **Maximum Requests**  
 Specify the maximum number of simultaneous requests that are allowed to execute in the workload group. Must be 0 or a positive integer.  
  
 **CPU Time (sec)**  
 Specify the maximum amount of CPU time that a request can use. Must be 0 or a positive integer. If 0, the time is unlimited.  
  
 **Memory Grant %**  
 Specify the maximum amount of memory that a single request can take from the pool. Range is 0 to 100.  
  
 **Grant Time-out (sec)**  
 Specify the maximum time that a query can wait for a resource to become available before the query fails. Must be 0 or a positive integer.  
  
 **Degree of Parallelism**  
 Specify the maximum degree of parallelism (DOP) for parallel requests. Range is 0 to 64.  
  
 For more information, see [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-workload-group-transact-sql).  
  
## View Resource Governor Properties by Using Transact-SQL  
 **View resource governor properties by using Transact-SQL**  
  
1.  To view the definitions of resource governor entities, use the [Resource Governor Catalog Views &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/resource-governor-catalog-views-transact-sql).  
  
2.  To view the current configuration of resource governor entities, use the [Resource Governor Related Dynamic Management Views &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql).  
  
## See Also  
 [Resource Governor](resource-governor.md)   
 [Enable Resource Governor](enable-resource-governor.md)   
 [Resource Governor Resource Pool](resource-governor-resource-pool.md)   
 [Resource Governor Workload Group](resource-governor-workload-group.md)   
 [Resource Governor Classifier Function](resource-governor-classifier-function.md)  
  
  

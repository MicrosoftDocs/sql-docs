---
title: "Create an Entity Administrator (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 717be1e8-488e-4219-8d1e-ca9084b864cd
author: leolimsft
ms.author: lle
manager: craigg
---
# Create an Entity Administrator (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create an entity administrator when you want a group or user to have all permissions to all objects in one or more entities, or have the permission to approve pending change sets.  
  
> [!TIP]  
>  To simplify administration, create a Windows or local group and configure it as an entity administrator. You can then add and remove users from the group without accessing [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)].  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **User and Group Permissions** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
## To create an Entity Administrator  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **User and Group Permissions**.  
  
2.  Select the row for the user or group that you want to edit, and then click **Edit selected user**.  
  
3.  Click the **Models** tab, optionally select a model from the **Models** list and then click **Edit**.  
  
4.  Click the entity you want to grant permissions to, and then click **Admin** on the menu.  
  
5.  Complete step 4 for each entity that you want the group or user to be an administrator for.  
  
6.  Click **Save**.  
  
## See Also  
 [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md)   
 [Assign Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/assign-model-object-permissions-master-data-services.md)   
 [Assign Hierarchy Member Permissions &#40;Master Data Services&#41;](../master-data-services/assign-hierarchy-member-permissions-master-data-services.md)   
 [Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/model-object-permissions-master-data-services.md)   
 [Hierarchy Member Permissions &#40;Master Data Services&#41;](../master-data-services/hierarchy-member-permissions-master-data-services.md)  
  
  

---
title: "Model Permissions (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "models [Master Data Services], permissions"
  - "permissions [Master Data Services], models"
ms.assetid: 210f440b-2cc1-4c49-94b1-3a97e2af7bc3
author: leolimsft
ms.author: lle
manager: craigg
---
# Model Permissions (Master Data Services)
  Model permissions apply to all entities, derived hierarchies, explicit hierarchies, and collections that exist within the model. Permissions assigned to the model can be overridden for any individual object.  
  
> [!NOTE]  
>  If a user is a model administrator, the model is displayed in all functional areas of the user interface. Otherwise, the model is displayed in the **Explorer** functional area only. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read-only**|In **Explorer**, the model is displayed but the user cannot add or remove members, and cannot update attribute values, hierarchy memberships, or collection memberships.|  
|**Update**|In **Explorer**, the model is displayed and the user can add and remove members, can update attribute values, hierarchy memberships, and collection memberships.|  
|**Deny**|The model is not displayed.|  
  
 When you assign permission to a model, the user gets access to all versions of the model. You cannot assign permission to an individual version.  
  
## See Also  
 [Assign Model Object Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/assign-model-object-permissions-master-data-services.md)   
 [Model Object Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/model-object-permissions-master-data-services.md)   
 [Entity Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/entity-permissions-master-data-services.md)   
 [Collection Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/collection-permissions-master-data-services.md)  
  
  

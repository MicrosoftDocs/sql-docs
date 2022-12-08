---
description: "Model Permissions (Master Data Services)"
title: Model Permissions
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "models [Master Data Services], permissions"
  - "permissions [Master Data Services], models"
ms.assetid: 210f440b-2cc1-4c49-94b1-3a97e2af7bc3
author: CordeliaGrey
ms.author: jiwang6
---
# Model Permissions (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Model permissions apply to all entities, derived hierarchies, explicit hierarchies, and collections that exist within the model. Permissions assigned to the model can be overridden for any individual object.  
  
> [!NOTE]  
>  If a user is a model administrator, the model is displayed in all functional areas of the user interface. Otherwise, the model is displayed in the **Explorer** functional area only. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read**|User can read members, attributes, hierarchy memberships, or collection memberships.|  
|**Create**|User can create members, and assign attribute values during create.|  
|**Update**|User can update members, attributes, hierarchy memberships, or collection memberships.|  
|**Delete**|User can delete members|  
|**Deny**|Deny all access to the model|  
|**Admin**|Administrator permission on the model. Admin permission is only available at the model level.|  
  
 The Read, Create, Update, and Delete permissions can be combined with each other. When Create, Update and Delete permissions are assigned, the read permission will be assigned automatically.  
  
## See Also  
 [Assign Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/assign-model-object-permissions-master-data-services.md)   
 [Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/model-object-permissions-master-data-services.md)   
 [Entity Permissions &#40;Master Data Services&#41;](../master-data-services/entity-permissions-master-data-services.md)   
 [Collection Permissions &#40;Master Data Services&#41;](../master-data-services/collection-permissions-master-data-services.md)  
  
  

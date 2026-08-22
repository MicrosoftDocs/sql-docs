---
title: Model Object Permissions
description: Model Object Permissions (Master Data Services)
author: meetdeepak
ms.author: dkhare
ms.date: 03/05/2026
ms.service: sql
ms.subservice: master-data-services
ms.topic: concept-article
ms.custom:
  - build-2025
helpviewer_keywords:
  - "permissions [Master Data Services], model objects"
  - "models [Master Data Services], object permissions"
---
# Model Object Permissions (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

[!INCLUDE [support-notice](includes/support-notice.md)]

  Model object permissions are mandatory. They determine the attributes a user can access in the **Explorer** functional area of the UI.  
  
 For example, if you assign a user **Update** permission to the Product entity, the user can update all of the attributes of the Product entity. If you assign **Update** permission to a single attribute, the user can update that attribute only.  
  
 To determine security assigned on each individual attribute value, model object permissions are combined with hierarchy member permissions, which determine the members a user can access.  
  
 To give a user access to a functional area other than **Explorer**, the user must be a model administrator, which also involves assigning Admin permissions on object model. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
 Model object permissions are assigned in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] user interface (UI), in the **User and Group Permissions** functional area on the **Models** tab. On this tab, the model is represented as a tree structure. When you assign permission to an object in the tree, all objects below inherit that permission. You can override that inheritance by assigning permission to individual objects.  
  
 You can assign a combination of Read, Create, Update and Delete or Deny permissions to model objects. If you do not assign any permissions on the **Models** tab, the user cannot view any models or data in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)].  
  
## Best Practice  
 In general, you should assign **ALL** permission to the model object, and then explicitly assign permission to objects underneath.  
  
## Related content

- [Security Improvements](/archive/blogs/e7/improvements-to-autoplay)
- [Assign Model Object Permissions (Master Data Services)](assign-model-object-permissions-master-data-services.md)
- [Model Permissions (Master Data Services)](model-permissions-master-data-services.md)
- [Functional Area Permissions (Master Data Services)](functional-area-permissions-master-data-services.md)
- [Hierarchy Member Permissions (Master Data Services)](hierarchy-member-permissions-master-data-services.md)
- [How Permissions Are Determined (Master Data Services)](how-permissions-are-determined-master-data-services.md)

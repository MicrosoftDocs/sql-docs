---
description: "Assign Model Object Permissions (Master Data Services)"
title: Assign Model Object Permissions
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "models [Master Data Services], assigning object permissions"
  - "permissions [Master Data Services], assigning model object permissions"
ms.assetid: 4b80148d-2318-415c-9479-28c240e48bcd
author: CordeliaGrey
ms.author: jiwang6
---
# Assign Model Object Permissions (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], assign permissions to model objects when you need to give a user or group access to data in the **Explorer** functional area of [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], or when you need to make a user or group an administrator.  
  
> [!NOTE]  
>  When you assign permission to a model, permission to all other models is implicitly denied. If you do not assign model object permissions, the user or group cannot access any data in **Explorer**.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Users and Group Permissions** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To assign model object permissions  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **User and Group Permissions**.  
  
2.  On the **Users** or **Groups** page, select the row for the user or group that you want to edit.  
  
3.  Click **Edit selected user**.  
  
4.  Click the **Models** tab.  
  
5.  Optionally, select a model from the **Model** list.  
  
6.  Click **Edit**.  
  
7.  Expand the tree, and click the model object you want to assign permissions to.  
  
8.  From the menu, select a combination of Read, Create, Update and Delete, or Deny.  
  
9. On the top model level, select **Admin** if you need to make a user model administrator.  
  
10. Click **Save**.  
  
## Next Steps  
  
-   (Optional) [Assign Hierarchy Member Permissions &#40;Master Data Services&#41;](../master-data-services/assign-hierarchy-member-permissions-master-data-services.md)  
  
## See Also  
 [Delete Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/delete-model-object-permissions-master-data-services.md)   
 [Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/model-object-permissions-master-data-services.md)   
 [Create a Model Administrator &#40;Master Data Services&#41;](../master-data-services/create-a-model-administrator-master-data-services.md)  
  
  

---
description: "Delete Hierarchy Member Permissions (Master Data Services)"
title: Delete Hierarchy Member Permissions
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "deleting member permissions [Master Data Services]"
  - "members [Master Data Services], deleting permissions"
  - "permissions [Master Data Services], deleting member permissions"
ms.assetid: 7f22d5e2-70c1-422c-99c2-e995a47d812a
author: CordeliaGrey
ms.author: jiwang6
---
# Delete Hierarchy Member Permissions (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], delete model object permissions to remove any assignments that have been made.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Users and Group Permissions** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To delete hierarchy member permissions  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **User and Group Permissions**.  
  
2.  On the **Users** or **Groups** page, select the row for the user or group that you want to edit.  
  
3.  Click **Edit selected user**.  
  
4.  Click the **Hierarchy Members** tab.  
  
5.  From the **Model** list, select a model.  
  
6.  From the **Version** list, select a version.  
  
7.  Click **Edit**.  
  
8.  Find the tree node with the permission, in the **Hierarchy Member Permissions** panel.  
  
9. Click the tree node, and click **None** in the context menu.  
  
    > [!NOTE]  
    >  You cannot remove a permission from a user if the permission is inherited from a group. You must remove the permission from the group instead.  
  
10. Click **Save**.  
  
## See Also  
 [Hierarchy Member Permissions &#40;Master Data Services&#41;](../master-data-services/hierarchy-member-permissions-master-data-services.md)   
 [Assign Hierarchy Member Permissions &#40;Master Data Services&#41;](../master-data-services/assign-hierarchy-member-permissions-master-data-services.md)  
  
  

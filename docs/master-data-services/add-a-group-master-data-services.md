---
description: "Add a Group (Master Data Services)"
title: Add a Group
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "groups [Master Data Services], adding"
  - "adding groups [Master Data Services]"
ms.assetid: c7a88381-3b2c-4af7-9cf7-3a930c1abdee
author: CordeliaGrey
ms.author: jiwang6
---
# Add a Group (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Add a group to the **Groups** list in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] to begin the process of assigning permission to the Web application. Before a user in the group can access [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], you must give the group permission to one or more functional areas and model objects.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Users and Group Permissions** functional area.  
  
### To add a group  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **User and Group Permissions**.  
  
2.  On the **Users** page, from the menu bar, click **Manage Groups**.  
  
3.  Click **Add groups**.  
  
4.  Type the group's name preceded by the Active Directory domain name or by the server computer's name, as in *domain\group_name* or *computer\group_name*.  
  
5.  Optionally, click **Check names**.  
  
6.  Click **OK**.  
  
    > [!NOTE]  
    >  When the user first accesses [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], the user's name is added to the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] list of users.  
  
## Next Steps  
  
-   [Assign Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/assign-functional-area-permissions-master-data-services.md)  
  
## See Also  
 [Security &#40;Master Data Services&#41;](../master-data-services/security-master-data-services.md)  
  
  

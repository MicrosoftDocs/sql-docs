---
title: Users and Groups
description: Find out how to grant access to the Master Data Manager web application. A user must have a suitable account.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "users [Master Data Services]"
  - "groups [Master Data Services]"
  - "users [Master Data Services], about users"
  - "groups [Master Data Services], about groups"
ms.assetid: ed08dd2d-248e-4b68-91d4-e9961cb50eed
author: CordeliaGrey
ms.author: jiwang6
---
# Users and Groups (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  To access the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application a user must have a Windows domain account or an account on the server computer where [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] is installed. To grant access to [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] you can either:  
  
-   Add the user account to a domain or local group and then add the group to the list of groups in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)].  
  
-   Add the user account to the list of users in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)].  
  
    > [!NOTE]  
    >  When a user belongs to a group that has access to [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], the user's name is automatically added to the list of users the first time the user accesses [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] or the MDS [!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)].  
  
 To take action within the **Explorer** functional area of the UI, the group or user must be assigned access to the **Explorer** functional area and assigned permission to model objects.  
  
 If a user or group needs access to other functional areas, the user or group must be assigned access to the specific functional area.  
  
## Best Practice  
 To simplify administration, create groups and assign each group permission to functional areas and model objects. You can then add and remove users from the groups without accessing the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] UI.  
  
 Do not assign additional permissions to an individual user, and do not include a user in multiple groups that have access to [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)]. In addition, do not use hierarchy member permissions unless you want a group to have limited access to specific members.  
  
## See Also  
 [Add a User &#40;Master Data Services&#41;](../master-data-services/add-a-user-master-data-services.md)   
 [Add a Group &#40;Master Data Services&#41;](../master-data-services/add-a-group-master-data-services.md)   
 [Delete Users or Groups &#40;Master Data Services&#41;](../master-data-services/delete-users-or-groups-master-data-services.md)   
 [Test a User's Permissions &#40;Master Data Services&#41;](../master-data-services/test-a-user-s-permissions-master-data-services.md)  
  
  

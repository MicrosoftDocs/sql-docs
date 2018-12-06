---
title: "Overlapping User and Group Permissions (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "users [Master Data Services], resolving permissions"
  - "permissions [Master Data Services], user and group overlaps"
  - "groups [Master Data Services], resolving permissions"
ms.assetid: 31c3cf7d-17d4-4474-b6a7-ffcb9fc45b37
author: leolimsft
ms.author: lle
manager: craigg
---
# Overlapping User and Group Permissions (Master Data Services)
  A user's permissions are based on:  
  
-   Permissions from group memberships.  
  
-   Permissions assigned explicitly to the user.  
  
 If a user is a member of multiple groups, and those groups have access to Master Data Manager, the following rules apply:  
  
-   **Deny** overrides all other permissions.  
  
-   **Update** overrides **Read-only**.  
  
 These rules apply to both the **Models** and **Hierarchy Members** tabs. Permissions are resolved for each tab and then combined. For more information, see [How Permissions Are Determined &#40;Master Data Services&#41;](how-permissions-are-determined-master-data-services.md).  
  
> [!NOTE]  
>  You can view the resolution of user and group overlapping permissions in the user interface. Both the **Models** and **Hierarchy Members** tab have a drop-down list from which you can choose **Effective** to view effective permissions.  
  
## Example 1  
 ![mds_conc_user_group_ex_1](../../2014/master-data-services/media/mds-conc-user-group-ex-1.gif "mds_conc_user_group_ex_1")  
  
 The user belongs to Group 1 and Group 2.  
  
 The user has **Read-only** permission to the Product entity.  
  
 Group 1 has **Update** permission to the Product entity.  
  
 Group 2 has **Read-only** permission to the Product entity.  
  
 Result: The user's effective permission is **Update** to the Product entity.  
  
## Example 2  
 ![mds_conc_user_group_ex_2](../../2014/master-data-services/media/mds-conc-user-group-ex-2.gif "mds_conc_user_group_ex_2")  
  
 The user belongs to Group 1 and Group 2.  
  
 The user has **Read-only** permission to the Product entity.  
  
 Group 1 has **Update** permission to Product entity.  
  
 Group 2 has **Deny** permission to the Product entity.  
  
 Result: The user's effective permission is **Deny** to the Product entity.  
  
## Example 3  
 ![mds_conc_user_group_ex_3](../../2014/master-data-services/media/mds-conc-user-group-ex-3.gif "mds_conc_user_group_ex_3")  
  
 The user belongs to Group 1 and Group 2.  
  
 The user has **Update** permission to a group of members in a hierarchy node.  
  
 Group 1 has **Read-only** permission to a group of members in a hierarchy node.  
  
 Group 2 has **Read-only** permission to a group of members in a hierarchy node.  
  
 Result: The user's effective permission is **Update** to the members.  
  
## See Also  
 [How Permissions Are Determined &#40;Master Data Services&#41;](how-permissions-are-determined-master-data-services.md)   
 [Overlapping Model and Member Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/overlapping-model-and-member-permissions-master-data-services.md)  
  
  

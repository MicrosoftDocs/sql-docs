---
title: "Hierarchy Member Permissions (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "members [Master Data Services], permissions"
  - "permissions [Master Data Services], members"
ms.assetid: b3880eed-1bf6-4f65-ab23-b08c194cc858
author: leolimsft
ms.author: lle
manager: craigg
---
# Hierarchy Member Permissions (Master Data Services)
  Hierarchy member permissions are optional and should be used only when you want a user to have limited access to specific members. If you do not assign permissions on the **Hierarchy Members** tab, then the user's permissions are based solely on the permissions assigned on the **Models** tab.  
  
 Hierarchy member permissions are assigned in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] user interface (UI), in the **User and Group Permissions** functional area on the **Hierarchy Members** tab. These permissions determine which members a user can access in the **Explorer** functional area of the UI.  
  
 On the **Hierarchy Members** tab, each hierarchy is represented as a tree structure. When you assign permission to a node in the tree, all children inherit that permission unless permission is explicitly assigned at a lower level.  
  
> [!NOTE]  
>  When you assign permission to a node in a hierarchy, all members in other nodes at the same level or higher are implicitly denied.  
  
 In **Explorer**, the member permissions are applied everywhere the member is displayed. For example, a member with **Read-only** permission is read-only in any entities, hierarchies, and collections to which it belongs.  
  
 Hierarchy member permissions apply to the model version you assign them to, and to any future copies of the version. They do not apply to versions earlier than the one you assign them to.  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read-only**|The members are displayed but the user cannot change them. The user also cannot move the members in any explicit hierarchies or collections that the members belong to.<br /><br /> Note: If you assign **Read-only** permission to **Root**, the members under **Root** are read-only; however, in explicit hierarchies and collections, the user can move members to **Root** and can add new members to **Root**.|  
|**Update**|The members are displayed and the user can change them. The user can also move the members in any explicit hierarchies or collections the members belong to.|  
|**Deny**|The members are not displayed.|  
  
 On the **Hierarchy Members** tab, the permissions you assign do not take effect immediately. The frequency that the permissions are applied depends on the **Member security processing interval setting** in the System Settings table in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. You can apply member permissions immediately by following the steps in [Immediately Apply Member Permissions &#40;Master Data Services&#41;](immediately-apply-member-permissions-master-data-services.md).  
  
> [!NOTE]  
>  You cannot assign hierarchy member permissions to recursive hierarchies, derived hierarchies with explicit caps, and derived hierarchies with hidden levels.  
  
## Possible Overlapping Permissions  
 When assigning permission to members, you may have to resolve overlapping permissions.  
  
### When a member belongs to multiple hierarchies  
 Two or more hierarchies can contain the same member.  
  
-   If one hierarchy node is assigned **Update** permission and another is assigned **Read-only**, then the members in the node are **Read-only**.  
  
-   If one hierarchy node is assigned **Update** or **Read-only** permission and another node is assigned **Deny**, then the members in the node are not displayed.  
  
## See Also  
 [Assign Hierarchy Member Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/assign-hierarchy-member-permissions-master-data-services.md)   
 [How Permissions Are Determined &#40;Master Data Services&#41;](../../2014/master-data-services/how-permissions-are-determined-master-data-services.md)   
 [Members &#40;Master Data Services&#41;](../../2014/master-data-services/members-master-data-services.md)   
 [Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/hierarchies-master-data-services.md)   
 [Immediately Apply Member Permissions &#40;Master Data Services&#41;](immediately-apply-member-permissions-master-data-services.md)  
  
  

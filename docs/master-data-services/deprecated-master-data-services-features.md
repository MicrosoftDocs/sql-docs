---
description: "Deprecated Master Data Services Features"
title: Deprecated Master Data Services Features
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: d8506bda-66dd-45a4-bfc9-3a10fa665acc
author: CordeliaGrey
ms.author: jiwang6
---
# Deprecated Master Data Services Features

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  This topic describes the deprecated [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] features. These features are scheduled to be removed in a future release. Deprecated features should not be used in new applications.  
  
## Explicit Hierarchies, Collections, and Related Components  
 Explicit hierarchies, collections and related components are deprecated. Members that before were modeled as consolidated member types (explicit hierarchy parent) and collection member types will be modeled as leaf members in derived hierarchies. The following new features enable derived hierarchies to take the place of explicit hierarchies.  
  
-   Recursive Derived Hierarchies can now be used to assign member security permissions.  
  
     An explicit hierarchy is analogous to a recursive derived hierarchy with a single, non-recursive level below the recursive level. A recursive derived hierarchy can be complex by including level(s) below and/or above a recursive level.  
  
-   In Explorer, the derived hierarchy page now shows unassigned (unused) members for each hierarchy level. Unused nodes are grouped by hierarchy level. Members can be moved between the Unused and Root nodes, by dragging and dropping or by cutting and pasting.  
  
     In System Administration, unused nodes are visible in the **Preview** pane. In Security, the unused nodes are visible in the **Hierarchy Member Permissions** pane. Any member whether under the **Root** node or **Unused** node, can be assigned a permission. The **Root**, **Unused**, and **Unused** pseudo members can also be assigned permissions.  
  
-   The stored procedure, mdm.udpConvertCollectionAndConsolidatedMembersToLeaf, converts explicit hierarchies to recursive derived hierarchies, and coverts consolidated and collection members to leaf members.  
  
     Explicit hierarchies and consolidated and collection member types are still supported, so running the stored procedure is optional. However, if you use these items it is recommended that you run the stored procedure because they are deprecated.  
  
 For information about explicit hierarchies, collections, and consolidated members see the following topics.  
  
-   [Explicit Hierarchies &#40;Master Data Services&#41;](../master-data-services/explicit-hierarchies-master-data-services.md)  
  
-   [Collections &#40;Master Data Services&#41;](../master-data-services/collections-master-data-services.md)  
  
-   [Members &#40;Master Data Services&#41;](../master-data-services/members-master-data-services.md)  
  
## Attribute Entity Transaction Log Type  
Entity transaction log type "Attribute" is deprecated, please migrate to the "Member" entity transaction log type. For information about entity transaction log types, see the following topic:
* [Change the Entity Transaction Log Type (Master Data Services)](../master-data-services/change-the-entity-transaction-log-type-master-data-services.md)
* [Member Revision History](../master-data-services/member-revision-history-master-data-services.md)
  
## External Resources  
 Blog post, [Deprecated: Explicit Hierarchies and Collections](https://techcommunity.microsoft.com/t5/sql-server-integration-services/deprecated-explicit-hierarchies-and-collections/ba-p/388221), on msdn.com.  
  
## See Also  
 [Discontinued Master Data Services Features](../master-data-services/discontinued-master-data-services-features.md)  
  
  

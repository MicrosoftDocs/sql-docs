---
title: "Hierarchies (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "hierarchies [Master Data Services]"
  - "hierarchies [Master Data Services], about hierarchies"
ms.assetid: 70dbb1fc-ead7-45be-9552-a45e3ccd8d21
author: leolimsft
ms.author: lle
manager: craigg
---
# Hierarchies (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], a hierarchy is a tree structure that you can use to:  
  
-   Group similar members for organizational purposes.  
  
-   Consolidate and summarize members for reporting and analysis.  
  
## What Hierarchies Contain  
 Each hierarchy contains members from one or more entities. When a member is added, changed, or deleted, all hierarchies are updated. This ensures that the data is accurate in all hierarchies. Hierarchies also help ensure that each member is counted once and only once.  
  
 If you want to create a grouping of a subset of members, consider using a collection. For more information, see [Collections &#40;Master Data Services&#41;](collections-master-data-services.md).  
  
## Kinds of Hierarchies  
 You can create multiple hierarchies to view and organize your members in different ways. You can create:  
  
-   Ragged hierarchies from a single entity, which are called explicit hierarchies. For more information, see [Explicit Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/explicit-hierarchies-master-data-services.md).  
  
-   Level-based hierarchies from multiple entities, based on the existing relationships between entities and their attributes, which are called derived hierarchies. For more information, see [Derived Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/derived-hierarchies-master-data-services.md).  
  
> [!NOTE]  
>  All members in a hierarchy must be within the same model.  
  
## Hierarchies Are Not Taxonomies  
 A hierarchy is different from a taxonomy. A taxonomy organizes members by multiple attributes at the same time, while a hierarchy organizes members by one attribute at a time. A taxonomy can include the same member multiple times, while a hierarchy includes a member only once.  
  
 For example, the same bike can be included in a taxonomy twice: once because it's red, and once because it's a size 38. In a hierarchy, the bike is included only once, so you must decide whether to show it in relation to its color or its size.  
  
## Hierarchy Example  
 In the following example, product members are grouped by subcategory members.  
  
 ![Hierarchy Grouped by Subcategory Example](../../2014/master-data-services/media/mds-conc-hierarchy.gif "Hierarchy Grouped by Subcategory Example")  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Enable an entity for explicit hierarchies and collections.|[Enable an Entity for Explicit Hierarchies and Collections &#40;Master Data Services&#41;](../../2014/master-data-services/enable-an-entity-for-explicit-hierarchies-and-collections-master-data-services.md)|  
|Create a explicit hierarchy.|[Create an Explicit Hierarchy &#40;Master Data Services&#41;](../../2014/master-data-services/create-an-explicit-hierarchy-master-data-services.md)|  
|Create a derived hierarchy.|[Create a Derived Hierarchy &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-derived-hierarchy-master-data-services.md)|  
|Hide or delete levels in an existing derived hierarchy.|[Hide or Delete Levels in a Derived Hierarchy &#40;Master Data Services&#41;](../../2014/master-data-services/hide-or-delete-levels-in-a-derived-hierarchy-master-data-services.md)|  
  
## Related Content  
  
-   [Explicit Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/explicit-hierarchies-master-data-services.md)  
  
-   [Derived Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/derived-hierarchies-master-data-services.md)  
  
-   [Recursive Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/recursive-hierarchies-master-data-services.md)  
  
-   [Derived Hierarchies with Explicit Caps &#40;Master Data Services&#41;](../../2014/master-data-services/derived-hierarchies-with-explicit-caps-master-data-services.md)  
  
-   [Collections &#40;Master Data Services&#41;](collections-master-data-services.md)  
  
  

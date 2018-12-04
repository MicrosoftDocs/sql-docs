---
title: "Collections (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "collections [Master Data Services]"
  - "collections [Master Data Services], about collections"
ms.assetid: 5aa1d1e0-b4e5-4897-8e74-01dcf418df73
author: leolimsft
ms.author: lle
manager: craigg
---
# Collections (Master Data Services)
  A collection is a group of leaf and consolidated members from a single entity. Use collections when you do not need a complete hierarchy and you want to view different groupings of members for reporting or analysis, or when you need to create a taxonomy.  
  
## What Collections Contain  
 Collections do not limit the number or type of members you can include, as long as the members are within the same entity. A collection can contain leaf and consolidated members from multiple mandatory and non-mandatory explicit hierarchies.  
  
 When you create a collection, you are not creating a hierarchical structure; you are creating a flat list of members. When you select a node from a hierarchy and add it to the collection, the consolidated member you selected is the only member added to the collection.  
  
 A collection can also contain other collections. You can use collections of collections to create taxonomies.  
  
 When you create a collection you are automatically listed as the owner. If you are an administrator, you can create other attributes for your collection as needed.  
  
> [!NOTE]  
>  Before you can create a collection, the entity must be enabled for explicit hierarchies. For more information, see [Enable an Entity for Explicit Hierarchies and Collections &#40;Master Data Services&#41;](enable-an-entity-for-explicit-hierarchies-and-collections-master-data-services.md).  
  
## Subscription Views for Collections  
 There are two types of subscription views that show collections. The **Collection attributes** format shows a list of collections and any attributes related to the collections (like description or owner). The **Collections** format shows all members in all collections, as well as each members weight and sort order. For more information, see [Exporting Data &#40;Master Data Services&#41;](overview-exporting-data-master-data-services.md).  
  
 If you set weight values for specific members in a collection, these values are available in related subscription views.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Enable an entity for explicit hierarchies and collections.|[Enable an Entity for Explicit Hierarchies and Collections &#40;Master Data Services&#41;](enable-an-entity-for-explicit-hierarchies-and-collections-master-data-services.md)|  
|Create a new collection.|[Create a Collection &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-collection-master-data-services.md)|  
|Add members to an existing collection.|[Add Members to a Collection &#40;Master Data Services&#41;](../../2014/master-data-services/add-members-to-a-collection-master-data-services.md)|  
  
## Related Content  
  
-   [Explicit Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/explicit-hierarchies-master-data-services.md)  
  
-   [Exporting Data &#40;Master Data Services&#41;](overview-exporting-data-master-data-services.md)  
  
  

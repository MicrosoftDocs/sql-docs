---
description: "Collections (Master Data Services)"
title: Collections
ms.custom: ""
ms.date: "04/01/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "collections [Master Data Services]"
  - "collections [Master Data Services], about collections"
ms.assetid: 5aa1d1e0-b4e5-4897-8e74-01dcf418df73
author: CordeliaGrey
ms.author: jiwang6
---
# Collections (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  A collection is a group of leaf and consolidated members from a single entity. Use collections when you do not need a complete hierarchy and you want to view different groupings of members for reporting or analysis, or when you need to create a taxonomy.  
  
> [!NOTE]  
>  Collection is deprecated.  
  
## What Collections Contain  
 Collections do not limit the number or type of members you can include, as long as the members are within the same entity. A collection can contain leaf and consolidated members from multiple mandatory and non-mandatory explicit hierarchies.  
  
 When you create a collection, you are not creating a hierarchical structure; you are creating a flat list of members. When you select a node from a hierarchy and add it to the collection, the consolidated member you selected is the only member added to the collection.  
  
 A collection can also contain other collections. You can use collections of collections to create taxonomies.  
  
 When you create a collection you are automatically listed as the owner. If you are an administrator, you can create other attributes for your collection as needed.  
  
## Subscription Views for Collections  
 There are two types of subscription views that show collections. The **Collection attributes** format shows a list of collections and any attributes related to the collections (like description or owner). The **Collections** format shows all members in all collections, as well as each members weight and sort order. For more information, see [Overview: Exporting Data &#40;Master Data Services&#41;](../master-data-services/overview-exporting-data-master-data-services.md).  
  
 If you set weight values for specific members in a collection, these values are available in related subscription views.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a new collection.|[Create a Collection &#40;Master Data Services&#41;](../master-data-services/create-a-collection-master-data-services.md)|  
|Add members to an existing collection.|[Add Members to a Collection &#40;Master Data Services&#41;](../master-data-services/add-members-to-a-collection-master-data-services.md)|  
  
## Related Content  
  
-   [Explicit Hierarchies &#40;Master Data Services&#41;](../master-data-services/explicit-hierarchies-master-data-services.md)  
  
-   [Overview: Exporting Data &#40;Master Data Services&#41;](../master-data-services/overview-exporting-data-master-data-services.md)  
  
  

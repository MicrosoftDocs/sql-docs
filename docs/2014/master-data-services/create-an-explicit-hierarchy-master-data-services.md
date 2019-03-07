---
title: "Create an Explicit Hierarchy (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "creating explicit hierarchies [Master Data Services]"
  - "explicit hierarchies, creating"
ms.assetid: ba768393-6990-4eda-8cb0-d58cb3cfc2e2
author: leolimsft
ms.author: lle
manager: craigg
---
# Create an Explicit Hierarchy (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create an explicit hierarchy when you need a ragged hierarchy in which members can exist at any level. Explicit hierarchies contain members from a single entity.  
  
 After you create an explicit hierarchy, you can add members to it in the **Explorer** functional area.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   The entity must be enabled for explicit hierarchies and collections. For more information, see [Enable an Entity for Explicit Hierarchies and Collections &#40;Master Data Services&#41;](../../2014/master-data-services/enable-an-entity-for-explicit-hierarchies-and-collections-master-data-services.md).  
  
### To create an explicit hierarchy  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **Manage** and click **Entities**.  
  
3.  On the **Entity Maintenance** page, from the **Model** list, select a model.  
  
4.  Select the row for the entity that you want to create an explicit hierarchy for.  
  
5.  Click **Edit selected entity**.  
  
6.  On the **Edit Entity** page, in the **Explicit hierarchies** pane, click **Add explicit hierarchy**.  
  
7.  On the **Add Explicit Hierarchy** page, in the **Explicit hierarchy name** box, type a name for the hierarchy.  
  
8.  Optionally, clear the **Mandatory hierarchy** check box to create the hierarchy as a non-mandatory hierarchy. For more information about hierarchy types, see [Explicit Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/explicit-hierarchies-master-data-services.md).  
  
9. Click **Save hierarchy**.  
  
10. On the **Edit Entity** page, click **Save entity**.  
  
## Next Steps  
  
-   [Create a Consolidated Member &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-consolidated-member-master-data-services.md)  
  
-   [Move Members within a Hierarchy &#40;Master Data Services&#41;](../../2014/master-data-services/move-members-within-a-hierarchy-master-data-services.md)  
  
## See Also  
 [Explicit Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/explicit-hierarchies-master-data-services.md)   
 [Derived Hierarchies with Explicit Caps &#40;Master Data Services&#41;](../../2014/master-data-services/derived-hierarchies-with-explicit-caps-master-data-services.md)   
 [Change an Explicit Hierarchy Name &#40;Master Data Services&#41;](../../2014/master-data-services/change-an-explicit-hierarchy-name-master-data-services.md)  
  
  

---
title: "Create a Domain-Based Attribute (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "domain-based attributes [Master Data Services], creating"
  - "creating domain-based attributes [Master Data Services]"
  - "attributes [Master Data Services], creating domain-based attributes"
ms.assetid: 11c31c9f-e6cc-47b7-b76a-d691f84c93c6
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Domain-Based Attribute (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create a domain-based attribute to populate an attribute's values with members from an entity.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   An entity must exist to use as the source of the attribute values. For example, to create a domain-based attribute based on the Color entity, you must first create the Color entity. For more information, see [Create an Entity &#40;Master Data Services&#41;](../../2014/master-data-services/create-an-entity-master-data-services.md).  
  
-   An entity must exist to create the attribute for. For more information, see [Create an Entity &#40;Master Data Services&#41;](../../2014/master-data-services/create-an-entity-master-data-services.md).  
  
### To create a domain-based attribute  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **Manage** and click **Entities**.  
  
3.  On the **Entity Maintenance** page, from the **Model** list, select a model.  
  
4.  Select the row for the entity that you want to create an attribute for.  
  
5.  Click **Edit selected entity**.  
  
6.  On the **Edit Entity** page:  
  
    -   If the attribute is for leaf members, in the **Leaf member attributes** pane, click **Add leaf attribute**.  
  
    -   If the attribute is for consolidated members, in the **Consolidated member attributes** pane, click **Add consolidated attribute**.  
  
    -   If the attribute is for collections, in the **Collection attributes** pane, click **Add collection attribute**.  
  
7.  On the **Add Attribute** page, select the **Domain-based** option.  
  
8.  In the **Name** box, type a name for the attribute. It does not have to be the same name as the entity that you use for the source of the attribute values.  
  
9. In the **Display pixel width** box, type the width of the attribute column to be displayed in the **Explorer** grid.  
  
10. From the **Entity** list, choose the entity to be used to populate the attribute values.  
  
11. Optional. Select **Enable change tracking** to track changes to groups of attributes. For more information, see [Add Attributes to a Change Tracking Group &#40;Master Data Services&#41;](../../2014/master-data-services/add-attributes-to-a-change-tracking-group-master-data-services.md).  
  
12. Click **Save attribute**.  
  
13. On the **Entity Maintenance** page, click **Save entity**.  
  
## See Also  
 [Domain-Based Attributes &#40;Master Data Services&#41;](../../2014/master-data-services/domain-based-attributes-master-data-services.md)   
 [Create a Derived Hierarchy &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-derived-hierarchy-master-data-services.md)   
 [Change an Attribute Name &#40;Master Data Services&#41;](change-an-attribute-name-and-data-type-master-data-services.md)   
 [Delete an Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/delete-an-attribute-master-data-services.md)  
  
  

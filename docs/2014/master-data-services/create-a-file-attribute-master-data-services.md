---
title: "Create a File Attribute (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "creating file attributes [Master Data Services]"
  - "attributes [Master Data Services], creating file attributes"
ms.assetid: d224886b-2ef1-4658-8b01-2213cc4b8df6
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a File Attribute (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create a file attribute to populate attribute values with files.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   An entity must exist to create the attribute for. For more information, see [Create an Entity &#40;Master Data Services&#41;](../../2014/master-data-services/create-an-entity-master-data-services.md).  
  
### To create a file attribute  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **Manage** and click **Entities**.  
  
3.  On the **Entity Maintenance** page, from the **Model** list, select a model.  
  
4.  Select the row for the entity that you want to create an attribute for.  
  
5.  Click **Edit selected entity**.  
  
6.  On the **Edit Entity** page:  
  
    -   If the attribute is for leaf members, in the **Leaf member attributes** pane, click **Add leaf attribute**.  
  
    -   If the attribute is for consolidated members, in the **Consolidated member attributes** pane, click **Add consolidated attribute**.  
  
    -   If the attribute is for collections, in the **Collection attributes** pane, click **Add collection attribute**.  
  
7.  On the **Add Attribute** page, select the **File** option.  
  
8.  In the **Name** box, type a name for the attribute. For a list of words that should not be used as attribute names, see [Reserved Words &#40;Master Data Services&#41;](../../2014/master-data-services/reserved-words-master-data-services.md).  
  
9. In the **Display pixel width** box, type the width of the attribute column to be displayed in the **Explorer** grid.  
  
10. From the **File extension** list, select one or more file types that a user can upload, or leave the default (*.\*) to allow all file types.  
  
11. Optionally, select **Enable change tracking** to track changes to groups of attributes. For more information, see [Add Attributes to a Change Tracking Group &#40;Master Data Services&#41;](../../2014/master-data-services/add-attributes-to-a-change-tracking-group-master-data-services.md).  
  
12. Click **Save attribute**.  
  
13. On the **Entity Maintenance** page, click **Save entity**.  
  
## See Also  
 [Attributes &#40;Master Data Services&#41;](../../2014/master-data-services/attributes-master-data-services.md)   
 [Change an Attribute Name &#40;Master Data Services&#41;](change-an-attribute-name-and-data-type-master-data-services.md)   
 [Create a Domain-Based Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-domain-based-attribute-master-data-services.md)   
 [Create a Text Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-text-attribute-master-data-services.md)  
  
  

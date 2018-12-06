---
title: "Create a Link Attribute (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "attributes [Master Data Services], creating link attributes"
  - "creating link attributes [Master Data Services]"
ms.assetid: e6658e9c-5b08-4b8d-b556-17ec2dd041d2
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Link Attribute (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create a link attribute when you want users to enter a hyperlink as an attribute value, such as http://www.contoso.com.  
  
> [!NOTE]  
>  When users enter a value for a link attribute, the string must begin with **http://** or an error will be displayed.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   An entity must exist to create the attribute for. For more information, see [Create an Entity &#40;Master Data Services&#41;](../../2014/master-data-services/create-an-entity-master-data-services.md).  
  
### To create a link attribute  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **Manage** and click **Entities**.  
  
3.  On the **Entity Maintenance** page, from the **Model** list, select a model.  
  
4.  Select the row for the entity that you want to create an attribute for.  
  
5.  Click **Edit selected entity**.  
  
6.  On the **Edit Entity** page:  
  
    -   If the attribute is for leaf members, in the **Leaf member attributes** pane, click **Add leaf attribute**.  
  
    -   If the attribute is for consolidated members, in the **Consolidated member attributes** pane, click **Add consolidated attribute**.  
  
    -   If the attribute is for collections, in the **Collection attributes** pane, click **Add collection attribute**.  
  
7.  On the **Add Attribute** page, select the **Free-form** option.  
  
8.  In the **Name** box, type a name for the attribute. For a list of words that should not be used as attribute names, see [Reserved Words &#40;Master Data Services&#41;](../../2014/master-data-services/reserved-words-master-data-services.md).  
  
9. In the **Display pixel width** box, type the width of the attribute column to be displayed in the **Explorer** grid.  
  
10. From the **Data type** list, select **Link**.  
  
11. In the **Length** box, type the maximum number of characters allowed.  
  
12. Optionally, select **Enable change tracking** to track changes to groups of attributes. For more information, see [Add Attributes to a Change Tracking Group &#40;Master Data Services&#41;](../../2014/master-data-services/add-attributes-to-a-change-tracking-group-master-data-services.md).  
  
13. Click **Save attribute**.  
  
14. On the **Entity Maintenance** page, click **Save entity**.  
  
## See Also  
 [Attributes &#40;Master Data Services&#41;](../../2014/master-data-services/attributes-master-data-services.md)   
 [Change an Attribute Name &#40;Master Data Services&#41;](change-an-attribute-name-and-data-type-master-data-services.md)   
 [Create a Domain-Based Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-domain-based-attribute-master-data-services.md)   
 [Create a File Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-file-attribute-master-data-services.md)  
  
  

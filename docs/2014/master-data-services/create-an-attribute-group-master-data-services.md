---
title: "Create an Attribute Group (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "attribute groups [Master Data Services], creating"
  - "creating attribute groups [Master Data Services]"
ms.assetid: 798c325e-e8d8-412a-b02e-118f2741d1c7
author: leolimsft
ms.author: lle
manager: craigg
---
# Create an Attribute Group (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create attribute groups when you want to display attributes on individual tabs in the **Explorer** grid.  
  
> [!NOTE]  
>  When you create an attribute group, it is automatically hidden from all users except the one who created it. For more information about making the group visible, see [Make an Attribute Group Visible to Users &#40;Master Data Services&#41;](make-an-attribute-group-visible-to-users-master-data-services.md).  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../../2014/master-data-services/administrators-master-data-services.md).  
  
-   At least one attribute must exist. For more information, see [Create a Text Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-text-attribute-master-data-services.md).  
  
### To create an attribute group  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **Manage** and click **Attribute Groups**.  
  
3.  From the **Model** list, select a model.  
  
4.  From the **Entity** list, select an entity.  
  
5.  Click **Leaf Groups**, **Consolidated Groups**, or **Collection Groups** to create an attribute group of leaf members, consolidated members, or collections respectively.  
  
6.  Click **Add attribute group**.  
  
7.  In the **Leaf group name** box, type a name for the group. This is the name displayed on the tab in **Explorer**.  
  
    > [!NOTE]  
    >  If you selected **Consolidated Groups** or **Collection Groups** in step 5, this box is **Consolidated group name** or **Collection group name**, respectively.  
  
8.  Click **Save group**.  
  
9. Expand the folder for the group.  
  
10. Click **Attributes**.  
  
11. Click **Edit selected item**.  
  
12. Click attributes in the **Available** box and click the **Add** arrow. To add all, click the **Add All** arrow.  
  
13. Optionally, click the **Up** and **Down** arrows to change the left-to-right order of the attributes.  
  
14. Click **Save**.  
  
## Next Steps  
  
-   [Make an Attribute Group Visible to Users &#40;Master Data Services&#41;](make-an-attribute-group-visible-to-users-master-data-services.md)  
  
## See Also  
 [Attribute Groups &#40;Master Data Services&#41;](../../2014/master-data-services/attribute-groups-master-data-services.md)   
 [Attributes &#40;Master Data Services&#41;](../../2014/master-data-services/attributes-master-data-services.md)   
 [Change an Attribute Group Name &#40;Master Data Services&#41;](../../2014/master-data-services/change-an-attribute-group-name-master-data-services.md)   
 [Delete an Attribute Group &#40;Master Data Services&#41;](../../2014/master-data-services/delete-an-attribute-group-master-data-services.md)   
 [Leaf Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/leaf-permissions-master-data-services.md)   
 [Consolidated Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/consolidated-permissions-master-data-services.md)  
  
  

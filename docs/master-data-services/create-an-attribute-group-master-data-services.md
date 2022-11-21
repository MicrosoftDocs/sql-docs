---
description: "Create an Attribute Group (Master Data Services)"
title: Create an Attribute Group
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "attribute groups [Master Data Services], creating"
  - "creating attribute groups [Master Data Services]"
ms.assetid: 798c325e-e8d8-412a-b02e-118f2741d1c7
author: CordeliaGrey
ms.author: jiwang6
---
# Create an Attribute Group (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create attribute groups when you want to display attributes on individual tabs in the **Explorer** grid.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   At least one attribute must exist. For more information, see [Create a Text Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-text-attribute-master-data-services.md).  
  
### To create an attribute group  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Manage Model** page, select a model from the grid and then click **Entities**.  
  
3.  On the **Manage Entity** page, from the grid, select the row for the entity that you want to create an attribute group for.  
  
4.  Click **Attribute Groups**.  
  
5.  On the Manage Attribute Groups page, do one of the following and then click **Add**.  
  
     If the attribute group is for leaf members, select **Leaf** from the **Member Types** drop-down list at the top of the page.  
  
     If the attribute group is for consolidated members, select **Consolidated** from the **Member Types** drop-down list.  
  
     If the attribute group is for collections, select **Collection** from the **Member Types** drop-down list.  
  
6.  Click **Leaf Groups**, **Consolidated Groups**, or **Collection Groups** to create an attribute group of leaf members, consolidated members, or collections respectively.  
  
7.  In the **Name** box, type a name for the attribute group. This name is displayed on the tab in **Explorer**.  
  
8.  To add an attribute, click the attribute in the **Available Attributes** box, and then click the **Add** arrow. To add all attributes, click the **Add All** arrow.  
  
9. Click the **Up** and **Down** arrows to change the left-to-right order of the attributes.  
  
10. Click users in the **Available Users** box, and then click the **Add** arrow. To add all users, click the **Add All** arrow.  
  
11. Click groups in the **Available Groups** box, and then click the **Add** arrow. To add all groups, click the **Add All** arrow.  
  
12. Click **Save**.  
  
## Next Steps  
  
-   [Make an Attribute Group Visible to Users &#40;Master Data Services&#41;](../master-data-services/make-an-attribute-group-visible-to-users-master-data-services.md)  
  
## See Also  
 [Attribute Groups &#40;Master Data Services&#41;](../master-data-services/attribute-groups-master-data-services.md)   
 [Attributes &#40;Master Data Services&#41;](../master-data-services/attributes-master-data-services.md)   
 [Change an Attribute Group Name &#40;Master Data Services&#41;](../master-data-services/change-an-attribute-group-name-master-data-services.md)   
 [Delete an Attribute Group &#40;Master Data Services&#41;](../master-data-services/delete-an-attribute-group-master-data-services.md)   
 [Leaf Permissions &#40;Master Data Services&#41;](../master-data-services/leaf-permissions-master-data-services.md)   
   
  
  

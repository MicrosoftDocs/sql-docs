---
description: "Make an Attribute Group Visible to Users (Master Data Services)"
title: Make an Attribute Group Visible to Users
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: b2f6cc27-dbc9-4f3f-961e-e81e76375248
author: CordeliaGrey
ms.author: jiwang6
---
# Make an Attribute Group Visible to Users (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], make an attribute group visible to users or groups when you want users to have tabs above the grid in the **Explorer** functional area.  
  
 When you create an attribute group, it is automatically hidden from all users except the one who created it.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   At least one attribute group must exist. For more information, see [Create an Attribute Group &#40;Master Data Services&#41;](../master-data-services/create-an-attribute-group-master-data-services.md).  
  
### To make an attribute group visible to users  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Manage Model** page, select a model from the grid and then click **Entities**.  
  
3.  On the **Manage Entity** page, from the grid, select the row for the entity that you want to edit the attribute group.  
  
4.  Click **Attribute Groups**.  
  
5.  On the **Manage Attribute Groups** page, select member type from the **Member Types** drop-down list box to expand **Leaf**, **Consolidated** or **Collection**, depending on the type of group you want to make visible.  
  
6.  Select the attribute group you want to edit from the grid, and then click **Edit**.  
  
7.  Click a user or group in the **Available** box and click the **Add** arrow. To add all, click the **Add All** arrow.  
  
8.  Click **Save**.  
  
## See Also  
 [Attribute Groups &#40;Master Data Services&#41;](../master-data-services/attribute-groups-master-data-services.md)   
 [Create an Attribute Group &#40;Master Data Services&#41;](../master-data-services/create-an-attribute-group-master-data-services.md)  
  
  

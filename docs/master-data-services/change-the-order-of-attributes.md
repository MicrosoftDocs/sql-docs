---
description: "Change the Order of Attributes"
title: Change the Order of Attributes
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 835a032c-e37c-4f35-8ab0-5e4ae25c2e9b
author: CordeliaGrey
ms.author: jiwang6
---
# Change the Order of Attributes

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], you can change the order of attributes.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To change the order of an attribute  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Manage Model** page, select a model from the grid and then click **Entities**.  
  
3.  On the **Manage Entity** page, select the row for the entity that you want to create an attribute for.  
  
4.  Click **Attributes**.  
  
5.  On the **Manage Attributes** page, do one of the following.  
  
    -   If the attribute is for leaf members, select **Leaf** from the **Member Types** list box.  
  
    -   If the attribute is for consolidated members, select **Consolidated** from the **Member Types** list box.  
  
    -   If the attribute is for collections, select **Collection** from the **Member Types** list box.  
  
6.  Select the row for the attribute you want to change the order of.  
  
    > [!NOTE]  
    >  You cannot change the order of the Name or Code attributes.  
  
7.  Click **Move Up** or **Move Down**.  
  
## See Also  
 [Attributes &#40;Master Data Services&#41;](../master-data-services/attributes-master-data-services.md)  
  
  

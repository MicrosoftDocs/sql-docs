---
description: "Delete an Explicit Hierarchy (Master Data Services)"
title: Delete an Explicit Hierarchy
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "explicit hierarchies, deleting"
  - "deleting explicit hierarchies [Master Data Services]"
ms.assetid: 4ce177b0-9884-47a2-9cea-212e845dd762
author: CordeliaGrey
ms.author: jiwang6
---
# Delete an Explicit Hierarchy (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], delete an explicit hierarchy when you no longer need it.  
  
> [!WARNING]  
>  When you delete an explicit hierarchy, all consolidated members in the hierarchy are deleted also. If you delete all explicit hierarchies for an entity, all collections for the entity are also deleted and the entity is no longer enabled for explicit hierarchies and collections.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To delete an explicit hierarchy  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Manage Model** page, select a model from the grid and then click **Entities**.  
  
3.  On the **Manage Entity** page, from the grid, select the row for the entity that contains the explicit hierarchy you want to delete.  
  
4.  Click **Explicit Hierarchies**.  
  
5.  On the **Manage Explicit Hierarchy** page, click the explicit hierarchy you want to delete.  
  
6.  Click **Edit**.  
  
7.  In the confirmation dialog box, click **OK**.  
  
## See Also  
 [Create an Explicit Hierarchy &#40;Master Data Services&#41;](../master-data-services/create-an-explicit-hierarchy-master-data-services.md)   
 [Explicit Hierarchies &#40;Master Data Services&#41;](../master-data-services/explicit-hierarchies-master-data-services.md)  
  
  

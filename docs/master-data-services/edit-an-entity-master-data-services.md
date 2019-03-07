---
title: "Edit an Entity (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "entities [Master Data Services], changing name"
ms.assetid: 6a5b9f14-6dfc-49d7-a771-e96461d4feae
author: leolimsft
ms.author: lle
manager: craigg
---
# Edit an Entity (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], you can edit an entity.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To edit an entity  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Manage Model** page, select a model from the grid and then click **Entities**.  
  
3.  On the **Manage Entity** page, from the grid, select the row for the entity that you want to change and then click **Edit**.  
  
4.  In the **Name** box, type the updated name of the entity..  
  
5.  In the **Description** field, type the updated description of the entity.  
  
6.  In the **Name for staging tables** field, type the updated name for the staging table.  
  
7.  For the **Transaction Log Type** field, choose the updated transaction log type in the drop-down list.  
  
     For more information, see [Change the Entity Transaction Log Type &#40;Master Data Services&#41;](../master-data-services/change-the-entity-transaction-log-type-master-data-services.md)  
  
8.  Select or unselect the **Create code values automatically** checkbox.  
  
     For more information, see [Automatic Code Creation &#40;Master Data Services&#41;](../master-data-services/automatic-code-creation-master-data-services.md)  
  
9. Select or unselect the **Enable data Compression** checkbox. By default the row compression is turned on.  
  
     For more information, see [Data Compression](../relational-databases/data-compression/data-compression.md)  
  
## Status  
 The status column in the grid shows the status of the operation on the entity. When you click **Save entity**, the following image displays that indicates that the entity is updating.  
  
 ![Icon for updating status](../master-data-services/media/mds-statusicon-updating.png "Icon for updating status")  
  
 If there are errors when creating or editing an entity, the following image displays.  
  
 ![Icon for error status](../master-data-services/media/mds-statusicon-error.png "Icon for error status")  
  
 When the status is OK, the following image displays.  
  
 ![Icon for OK status](../master-data-services/media/mds-statusicon-ok.png "Icon for OK status")  
  
## See Also  
 [Explicit Hierarchies &#40;Master Data Services&#41;](../master-data-services/explicit-hierarchies-master-data-services.md)   
 [Delete an Entity &#40;Master Data Services&#41;](../master-data-services/delete-an-entity-master-data-services.md)   
 [Entities &#40;Master Data Services&#41;](../master-data-services/entities-master-data-services.md)  
  
  

---
description: "Delete a Model (Master Data Services)"
title: Delete a Model
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "deleting models [Master Data Services]"
  - "models [Master Data Services], deleting models"
ms.assetid: f0ad3cc4-aed7-47c8-94bc-2971fe9fe871
author: CordeliaGrey
ms.author: jiwang6
---
# Delete a Model (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Delete a model to remove the model and all of its data from [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)].  
  
> [!NOTE]  
>  When you complete this procedure, all objects and all data from all versions of the model will be permanently deleted.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To delete a model  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **Manage** and click **Models**.  
  
3.  On the **Manage Models** page, from the grid, select the row for the model that you want to delete.  
  
4.  Click **Delete**.  
  
5.  In the confirmation dialog box, click **OK**.  
  
6.  In the additional confirmation dialog box, click **OK**.  
  
 The **Status** column in the grid shows the status of the operation on the model. When you click the **Save model** button, the ![Updating](../master-data-services/media/mds-model-status-updating.png "Updating") image is displayed, which indicates that the model is updating. If there are errors when creating or editing a model, the ![Error](../master-data-services/media/mds-model-status-error.png "Error") image is displayed. Otherwise, the status is OK and the ![OK](../master-data-services/media/mds-model-status-ok.png "OK") image is displayed.  
  
## See Also  
 [Models &#40;Master Data Services&#41;](../master-data-services/models-master-data-services.md)   
 [Create a Model &#40;Master Data Services&#41;](../master-data-services/create-a-model-master-data-services.md)  
  
  

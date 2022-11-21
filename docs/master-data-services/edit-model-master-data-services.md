---
description: "Edit Model (Master Data Services)"
title: Edit Model
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "models [Master Data Services], changing name"
ms.assetid: 399eed32-7c61-4239-9c06-996a65219518
author: CordeliaGrey
ms.author: jiwang6
---
# Edit Model (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], you can change the name and description of a model and indicate how many days you want to retain transaction logs.  
  
 For more information, see [Transactions &#40;Master Data Services&#41;](../master-data-services/transactions-master-data-services.md).  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To change a model  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **Manage** and click **Models**.  
  
3.  On the **Manage Models** page, from the grid, select the row for the model with the name or description you want to change.  
  
4.  Click **Edit**.  
  
5.  In the **Name** box, type the updated name of the model.  
  
6.  In the **Description** field, type the updated description of the model.  
  
7.  In the **Log Retention Days** field, select one of the options for retaining log data. The default value is **System Setting**, which indicates that the value is inherited from system settings in the [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)]. For more information, see [System Settings &#40;Master Data Services&#41;](../master-data-services/system-settings-master-data-services.md).  
  
     To override the system setting and not remove transaction log data, select **NO**. To retain only today's log data and truncate log data for all previous days, select **YES** and set the **Days** field to 0. To retain log data for a specified number of days, select **YES** and set the **Days** field to the number of days.  
  
8.  Click **Save model**.  
  
 The **Status** column in the grid shows the status of the operation on the model. When you click the **Save model** button, the ![Updating](../master-data-services/media/mds-model-status-updating.png "Updating") image is displayed, which indicates that the model is updating. If there are errors when creating or editing a model, the ![Error](../master-data-services/media/mds-model-status-error.png "Error") image is displayed. Otherwise, the status is OK and the ![OK](../master-data-services/media/mds-model-status-ok.png "OK") image is displayed.  
  
## See Also  
 [Create a Model &#40;Master Data Services&#41;](../master-data-services/create-a-model-master-data-services.md)   
 [Delete a Model &#40;Master Data Services&#41;](../master-data-services/delete-a-model-master-data-services.md)   
 [Models &#40;Master Data Services&#41;](../master-data-services/models-master-data-services.md)  
  
  

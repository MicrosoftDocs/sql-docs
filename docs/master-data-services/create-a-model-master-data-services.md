---
title: "Create a Model (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "models [Master Data Services], creating models"
  - "creating models [Master Data Services]"
ms.assetid: 9bb3b3b3-bde8-44aa-ad62-eaae21188141
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Model (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create a model to contain model objects.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To create a model  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **Manage** and click **Models**.  
  
3.  On the **Manage Models** page, click **Add**. A panel is displayed on the right side.  
  
4.  In the **Name** box, type the name of the model.  
  
5.  (Optionally) In the **Description** field, type the model description.  
  
6.  In the **Log Retention Days** field, select one of the options for retaining log data. The default value is **System Setting**, which indicates that the value is inherited from system settings in the [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)]. For more information, see [System Settings &#40;Master Data Services&#41;](../master-data-services/system-settings-master-data-services.md).  
  
     To override the system setting and not remove transaction log data, select **NO**. To retain only today's log data and truncate log data for all previous days, select **YES** and set the **Days** field to 0. To retain log data for a specified number of days, select **YES** and set the **Days** field to the number of days.  
  
7.  Optionally, select **Create entity with same name as model** to create an entity with the same name as the model.  
  
8.  Click **Save model**.  
  
 For each created model, a row with eight columns is added to the grid. The eight columns are:  
  
-   **Status**: The model status. When you click the **Save model** button, the ![Updating](../master-data-services/media/mds-model-status-updating.png "Updating") image is displayed, which indicates that the model is updating. If there are errors when creating or editing a model, the ![Error](../master-data-services/media/mds-model-status-error.png "Error") image is displayed. Otherwise, the status is OK and the ![OK](../master-data-services/media/mds-model-status-ok.png "OK") image is displayed.  
  
-   **Name**: The model name.  
  
-   **Description**: The model description.  
  
-   **Log Retention Days**: The number of days the log is retained for the model.  
  
-   **Created By**: The username of the user who created the model.  
  
-   **Created Date and Time**: The date and time when the model was created.  
  
-   **Updated By**: The username of the user who last updated the model.  
  
-   **Updated Date and Time**: The date and time when the model was last updated.  
  
## Next Steps  
  
-   [Create an Entity &#40;Master Data Services&#41;](../master-data-services/create-an-entity-master-data-services.md)  
  
## See Also  
 [Models &#40;Master Data Services&#41;](../master-data-services/models-master-data-services.md)   
 [Entities &#40;Master Data Services&#41;](../master-data-services/entities-master-data-services.md)   
 [Delete a Model &#40;Master Data Services&#41;](../master-data-services/delete-a-model-master-data-services.md)   
 [Edit Model &#40;Master Data Services&#41;](../master-data-services/edit-model-master-data-services.md)   
 [Transactions &#40;Master Data Services&#41;](../master-data-services/transactions-master-data-services.md)  
  
  

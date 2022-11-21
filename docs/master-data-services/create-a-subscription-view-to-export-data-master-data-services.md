---
title: Create a Subscription View to Export Data
description: Learn how to create a subscription view to export Master Data Services data to subscribing systems, which creates a view of your data.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "subscription views [Master Data Services], creating"
  - "creating subscription views [Master Data Services]"
ms.assetid: a5e28961-af16-414a-9845-d2e06aac5214
author: CordeliaGrey
ms.author: jiwang6
---
# Create a Subscription View to Export Data (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Create a subscription view to export Master Data Services data to subscribing systems. You're creating a view of your data in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Integration Management** functional area. For more information, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md).  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To create and edit a subscription view  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **Integration Management**.  
  
2.  From the menu bar, click **Create Views**.  
  
3.  On the **Subscription Views** page, click **Add** to create a view or click **Edit** to edit a view. A panel displays on the right side.  
  
4.  In the **Create Subscription View** pane, in the **Name** box, type a name for the view.  
  
     In the **Edit Subscription View** pane, in the **Name** box, type the updated name for the view.  
  
5.  From the **Model** list, select a model.  
  
6.  Select **Include soft-deleted members**, to include soft-deleted members in the view.  
  
7.  Select either **Version** or **Version Flag** in **Version Options**, and then select from the corresponding list.  
  
    > [!TIP]  
    >  Create a subscription view based on a version flag. When you lock a version, you can reassign the flag to an open version without updating the subscription view.  
  
8.  Select either **Entity** or **Derived hierarchy** in the **Data Sources** option, and then select from the corresponding list.  
  
9. From the **Format** list, select a subscription view format.  
  
10. If you chose **Explicit levels** or **Derived levels** from the **Format** list, type the number of levels in the hierarchy to include in the view.  
  
11. Click **Save**.  
  
## View Information  
 For each created view, a row with ten columns is added to the grid. The following table describes the columns.  
  
|Column|Description|  
|------------|-----------------|  
|Status|The view status.<br /><br /> When you click **Save**, the ![Icon for updating status](../master-data-services/media/mds-statusicon-updating.png "Icon for updating status") image displays, indicating that the view is updating.<br /><br /> If there are errors when creating or editing a view, the ![Icon for error status](../master-data-services/media/mds-statusicon-error.png "Icon for error status") image displays.<br /><br /> Otherwise, the status is OK and the ![Icon for OK status](../master-data-services/media/mds-statusicon-ok.png "Icon for OK status") image displays.|  
|Name|The subscription view name.|  
|Model|The model name.|  
|Version|The version name.|  
|Version Flag|The version flag name.|  
|Derived Hierarchy|The derived hierarchy name.|  
|Entity|The entity name.|  
|Format|Specifies the type of the data in the view.|  
|Level|Specifies the number of levels in the view, which is only used for Explicit level or Derived level view formats|  
|Include delete members|Indicates whether soft-deleted members are included in the view.|  
  
 When you click a view, the following information displays.  
  
-   **Created By**: The name of the user who created the view.  
  
-   **On**: The date and time when the view was created.  
  
-   **Updated By**: The name of the user who last updated the view.  
  
-   **On**: The date and time when the view was last updated.  
  
## See Also  
 [Overview: Exporting Data &#40;Master Data Services&#41;](../master-data-services/overview-exporting-data-master-data-services.md)   
 [Delete a Subscription View &#40;Master Data Services&#41;](../master-data-services/delete-a-subscription-view-master-data-services.md)   
 [Create a Version Flag &#40;Master Data Services&#41;](../master-data-services/create-a-version-flag-master-data-services.md)  
  
  

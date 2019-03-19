---
title: "Create a Subscription View (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "subscription views [Master Data Services], creating"
  - "creating subscription views [Master Data Services]"
ms.assetid: a5e28961-af16-414a-9845-d2e06aac5214
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Subscription View (Master Data Services)
  Create a subscription view when you want to create a view of your data in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database for use by subscribing systems.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Integration Management** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
### To create a subscription view  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **Integration Management**.  
  
2.  From the menu bar, click **Create Views**.  
  
3.  On the **Subscription Views** page, click **Add subscription view**.  
  
4.  In the **Create Subscription View** pane, in the **Subscription view name** box, type a name for the view.  
  
5.  From the **Model** list, select a model.  
  
6.  Select either the **Version** or **Version Flag** option, and then select from the corresponding list.  
  
    > [!TIP]  
    >  Create a subscription view based on a version flag. When you lock a version, you can reassign the flag to an open version without updating the subscription view.  
  
7.  Select either the **Entity** or **Derived hierarchy** option, and then select from the corresponding list.  
  
8.  From the **Format** list, select a subscription view format.  
  
9. If you chose **Explicit levels** or **Derived levels** from the **Format** list, type the number of levels in the hierarchy to include in the view.  
  
10. Click **Save**.  
  
## See Also  
 [Exporting Data &#40;Master Data Services&#41;](overview-exporting-data-master-data-services.md)   
 [Delete a Subscription View &#40;Master Data Services&#41;](delete-a-subscription-view-master-data-services.md)   
 [Create a Version Flag &#40;Master Data Services&#41;](create-a-version-flag-master-data-services.md)  
  
  

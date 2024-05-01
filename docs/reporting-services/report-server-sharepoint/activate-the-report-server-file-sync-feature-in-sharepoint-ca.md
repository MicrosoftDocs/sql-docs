---
title: "Activate the report server file sync feature in SharePoint"
description: The Report Server File Sync feature of Reporting Services uses SharePoint event handlers to sync the report server catalog with items in document libraries.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2017
ms.service: reporting-services
ms.subservice: report-server-sharepoint
ms.topic: conceptual
ms.custom: updatefrequency5
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---
# Activate the report server file sync feature in SharePoint

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Report Server File Sync feature utilizes SharePoint event handlers to synchronize the report server catalog with items in document libraries. This feature is beneficial when users frequently upload published report items directly to SharePoint document libraries. If the file Sync feature isn't activated, content still synchronizes but not as frequently.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  
 The File Sync feature can be activated in SharePoint Site Administration after you install the [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] Add-in for SharePoint products.  
  
 This feature can be manually activated and deactivated per site but not at the site collection level.  
  
## Prerequisites

 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint must be installed. If the add-in isn't installed, the file sync feature won't be visible in the site feature list.  
  
 To verify installation, view the list of installed applications in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows **Control Panel**. If the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in is installed, follow the instructions in this article to activate the report server file sync feature.  
  
### To activate or deactivate the Reporting Services file sync feature on a site
  
1.  From the main page of your site, select the **Site Actions** menu and select **Site Settings**.
  
2.  In the **Site Actions**, select **Manage Site Features**.  
  
3.  Find **Report Server File Sync** in the list.  
  
4.  Select **Activate**.  

> [!NOTE]
> To deactivate the Report Server file sync feature, you can use the same procedure but select **Deactivate**.

## Related content

 [Activate the report server integration features in SharePoint](../../reporting-services/report-server-sharepoint/site-collection-features-report-server-and-power-view.md)   
 [Install or uninstall the Reporting Services add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)   
 [Install or uninstall the Reporting Services add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)

---
title: "Activate the report server file sync feature in SharePoint | Microsoft Docs"
ms.date: 09/25/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server-sharepoint


ms.topic: conceptual
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016 <=sql-server-2016||=sqlallproducts-allversions"
---
# Activate the report server file sync feature in SharePoint

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Report Server File Sync feature utilizes SharePoint event handlers to synchronize the report server catalog with items in document libraries. This feature is beneficial when users frequently upload published report items directly to SharePoint document libraries. If the file Sync feature is not activated, content will still be synchronized but not as frequently.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  
 The File Sync feature can be activated in SharePoint Site Administration after you install the [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] Add-in for SharePoint products.  
  
 This feature can be manually activated and deactivated per site but not at the site collection level.  
  
## Prerequisites

 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint must be installed. If the add-in is not installed the file sync feature will not be visible in the site feature list.  
  
 To verify installation, view the list of installed applications in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows **Control Panel**. If the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in is installed, follow the instructions in this topic to activate the report server file sync feature.  
  
### To activate or deactivate the Reporting Services file sync feature on a site
  
1.  From the main page of your site, click the **Site Actions** menu and click **Site Settings**..  
  
2.  In the **Site Actions** click **Manage Site Features**.  
  
3.  Find **Report Server File Sync** in the list.  
  
4.  Click **Activate**.  

> [!NOTE]
> To deactivate the Report Server file sync feature, you can use the same procedure but click **Deactivate**.

## See also

 [Troubleshoot Report Parts (Report Builder and SSRS)](https://msdn.microsoft.com/d9fe1932-46e7-421b-a8a9-4c54d9576e94)   
 [Activate the Report Server and Power View Integration Features in SharePoint](../../reporting-services/report-server-sharepoint/site-collection-features-report-server-and-power-view.md)   
 [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)   
 [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)

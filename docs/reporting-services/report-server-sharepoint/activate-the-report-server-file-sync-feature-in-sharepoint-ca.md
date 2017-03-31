---
title: "Activate the Report Server File Sync Feature in SharePoint CA | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 32d1988d-07e7-41c2-b636-e65ecfae4677
caps.latest.revision: 9
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# Activate the Report Server File Sync Feature in SharePoint CA
  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Report Server File Sync feature utilizes SharePoint event handlers to synchronize the report server catalog with items in document libraries. This feature is beneficial when users frequently upload published report items directly to SharePoint document libraries. If the file Sync feature is not activated, content will still be synchronized but not as frequently.  
  
 The File Sync feature can be activated in SharePoint Site Administration after you install the [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] Add-in for SharePoint products.  
  
 This feature can be manually activated and deactivated per site but not at the site collection level.  
  
## Prerequisites  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint must be installed. If the add-in is not installed the file sync feature will not be visible in the site feature list.  
  
 To verify installation, view the list of installed applications in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows **Control Panel**. If the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in is installed, follow the instructions in this topic to activate the report server file sync feature.  
  
### To activate or deactivate the Reporting Services File Sync feature on a Site  
  
1.  From the main page of your site, click the **Site Actions** menu and click **Site Settings**..  
  
2.  In the **Site Actions** click **Manage Site Features**.  
  
3.  Find **Report Server File Sync** in the list.  
  
4.  Click **Activate**.  
  
> [!NOTE]  
>  To deactivate the Report Server file sync feature, you can use the same procedure but click **Deactivate**.  
  
## See Also  
 [Troubleshoot Report Parts (Report Builder and SSRS)](http://msdn.microsoft.com/en-us/d9fe1932-46e7-421b-a8a9-4c54d9576e94)   
 [Activate the Report Server and Power View Integration Features in SharePoint](../../reporting-services/report-server-sharepoint/site-collection-features-report-server-and-power-view.md)   
 [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)   
 [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  
  
  
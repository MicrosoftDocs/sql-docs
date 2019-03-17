---
title: "Activate the Report Server and Power View Integration Features in SharePoint | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: c7f64a54-c555-4d31-bf99-3abe57dc8626
author: markingmyname
ms.author: maghan
manager: kfile
---
# Activate the Report Server and Power View Integration Features in SharePoint
  The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] site collection features are usually activated by default after you install the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] Add-in for SharePoint products. In some situations you will need to manually activate the features.  
  
 If you install the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] add-in for SharePoint 2010 Products after the installation of the SharePoint product, then the Report Server integration feature and the Power View integration feature will only be activated for root site collections. For other site collections, you will need to manually activate the features. For example if you have a site collection of **http://[my server name]/sites/[site collection name]** you will need to manually activate the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] site collection features.  
  
 When there is no root site collection, the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] add-in will log a message similar to the following.  
  
 "SharePoint web app 80 does not have root site collection"  
  
 The message will be found in the add-in installation log, named "RS_SP_#.log" where # is an incrementing number. The log file will be found in the current users Temp folder, for example C:\Users\\[user name]\AppData\Local\Temp. For more information on logging options with the add-in, see [Install or Uninstall the Reporting Services Add-in for SharePoint &#40;SharePoint 2010 and SharePoint 2013&#41;](install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md).  
  
 In this topic:  
  
-   [To Activate the Report Server and Power View Integration Site Collection Features:](#bkmk_features)  
  
-   [To Activate or Deactivate Reporting Services Central Administration Site Collection Feature:](#bkmk_centraladmin)  
  
##  <a name="bkmk_features"></a> To Activate the Report Server and Power View Integration Site Collection Features:  
  
1.  Open your browser to the site where you want the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features active.  
  
2.  Click **Site Actions**.  
  
3.  Click **Site Settings**.  
  
4.  Click **Site Collection Features** in the Site Collection Administration Group.  
  
5.  Find **Report Server Integration Feature** or **Power View Integration Feature** in the list.  
  
6.  Click **Activate**.  
  
 To deactivate the features, you can use the same procedure, but click **Deactivate** rather than **Activate.**  
  
##  <a name="bkmk_centraladmin"></a> To Activate or Deactivate Reporting Services Central Administration Site Collection Feature:  
  
1.  Open your browser to SharePoint Central Administration.  
  
2.  Click **Site Actions**.  
  
3.  Click **Site Settings**.  
  
4.  Click **Site Collection Features** in the Site Collection Administration Group.  
  
5.  Find **Report Server Central Administration Feature** in the list.  
  
6.  Click **Activate**.  
  
 To deactivate the feature, you can use the same procedure, but click **Deactivate** rather than **Activate.**  
  
## Next Steps  
 After the feature is activated, you can continue with server integration.  
  
## See Also  
 [Install or Uninstall the Reporting Services Add-in for SharePoint &#40;SharePoint 2010 and SharePoint 2013&#41;](install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  
  
  

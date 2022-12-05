---
title: "Activate the report server and Power View integration features in SharePoint | Microsoft Docs"
description: SQL Server Reporting Services Add-in for SharePoint features usually automatically activate. Use these instructions if you need to manually activate them.
ms.date: 09/25/2017
ms.service: reporting-services
ms.subservice: report-server-sharepoint


ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---
# Activate the report server and Power View integration features in SharePoint

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

  The Reporting Services site collection features are activated by default after you install the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] Add-in for SharePoint products. In some situations, you need to manually activate the features.  

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016. Power View support is no longer available after SQL Server 2017.

 If you install the Reporting Services add-in for SharePoint 2010 Products after the installation of the SharePoint product, then the Report Server integration feature and the Power View integration feature will only be activated for root site collections. For other site collections, you need to manually activate the features. For example, if you have a site collection of **https://[my server name]/sites/[site collection name]** you need to manually activate the Reporting Services site collection features.  
  
 When there is no root site collection, the Reporting Services add-in will log a message similar to the following.  
  
 "SharePoint web app 80 does not have root site collection"  
  
 The message is found in the add-in installation log, named "RS_SP_#.log" where # is an incrementing number. The log file is found in the current users Temp folder, for example C:\Users\\[user name]\AppData\Local\Temp. For more information on logging options with the add-in, see [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md).  

## Activate the Report Server and Power View integration site collection features
  
1.  Open your browser to the site where you want the Reporting Services features active.  
  
2.  Click **Site Actions**.  
  
3.  Click **Site Settings**.  
  
4.  Click **Site Collection Features** in the Site Collection Administration Group.  
  
5.  Find **Report Server Integration Feature** or **Power View Integration Feature** in the list.  
  
6.  Click **Activate**.  
  
 To deactivate the features, you can use the same procedure, but click **Deactivate** rather than **Activate.**  
  
## Activate or Deactivate Reporting Services central administration site collection feature
  
1.  Open your browser to SharePoint Central Administration.  
  
2.  Click **Site Actions**.  
  
3.  Click **Site Settings**.  
  
4.  Click **Site Collection Features** in the Site Collection Administration Group.  
  
5.  Find **Report Server Central Administration Feature** in the list.  
  
6.  Click **Activate**.  
  
 To deactivate the feature, you can use the same procedure, but click **Deactivate** rather than **Activate.**  
  
## Next steps

After the feature is activated, you can continue with server integration.

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)

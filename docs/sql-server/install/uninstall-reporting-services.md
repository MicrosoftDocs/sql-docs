---
title: "Uninstall Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 5c764a00-d4bc-465d-b32e-e4efce052ce4
author: "markingmyname"
ms.author: "maghan"
manager: "kfile"
---
# Uninstall Reporting Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Uninstalling [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] does not remove the content you have created or configuration you have modified. However, if there is content you need after the uninstall is complete, it is recommended you make copies of content before you begin the uninstallation process.  
  
## Uninstall SharePoint Mode  
 When you uninstall [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode, the following are removed:  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service and service proxy.  
  
-   Files used for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation.  
  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service applications are not removed. If you no longer want the service applications, delete them by using Windows PowerShell or SharePoint Central Administration.  
  
 The report items and related meta data are not removed. This information is contained in the content and configuration databases related to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service applications. The databases are not removed and you can manually migrate the databases to another installation of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode. If you no longer want the information, delete the databases. For more information, see [Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md).  
  
 The following are example names of the three [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] databases that are not removed.  
  
-   **Report server database:** ReportingService_7f616e2d253040e8ab5653b3c09a065e  
  
-   **Report server temp database:** ReportingService_7f616e2d253040e8ab5653b3c09a065eTempDB  
  
-   **Report server alerting database:** ReportingService_7f616e2d253040e8ab5653b3c09a065e_Alerting  
  
### Uninstall the Add-in for SharePoint Products.  
 When you uninstall the add-in from a computer, you can choose to only uninstall the files or to also remove the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] feature from the farm. For information on uninstalling the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products, see [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md).  
  
## Uninstall Native Mode  
 When you uninstall [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode, anything that was **created** or **modified** after the installation is left in place. For example database files, log files, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration files, and content items such as reports and datasource files.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is an instance feature and therefore is not listed in Windows Control Panel, Programs and Features. To uninstall [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode:  
  
1.  In Windows Control Panel, click **Programs and Features**.  
  
2.  In **Programs and Features** select **Microsoft SQL Server 2016**.  
  
3.  In the uninstall wizard, select the instance that includes the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance feature **RS**.  
  
     ![rs_nativemode_uninstall_selectinstance](../../sql-server/install/media/rs-nativemode-uninstall-selectinstance.gif "rs_nativemode_uninstall_selectinstance")  
  
4.  After you select the instance, select the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] feature.  
  
     ![rs_nativemode_uninstall_selectfeatures](../../sql-server/install/media/rs-nativemode-uninstall-selectfeatures.gif "rs_nativemode_uninstall_selectfeatures")  
  
5.  Complete the wizard.  
  
## See Also  
 [Uninstall an Existing Instance of SQL Server &#40;Setup&#41;](../../sql-server/install/uninstall-an-existing-instance-of-sql-server-setup.md)   
 [Install or Uninstall the Power Pivot for SharePoint Add-in &#40;SharePoint 2013&#41;](../../analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013.md)   
 [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  
  
  

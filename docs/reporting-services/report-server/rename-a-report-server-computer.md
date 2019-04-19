---
title: "Rename a Report Server Computer | Microsoft Docs"
ms.date: 03/20/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "renaming report servers"
ms.assetid: 82fc4ba2-291a-4939-a025-271b8d687c54
author: maggiesMSFT
ms.author: maggies
---
# Rename a Report Server Computer
  Renaming a computer causes a corresponding name change for the Web server and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance (if it is on the same computer). In some cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] may not be accessible after a computer name change. Use the steps provided in this topic to reconfigure a report server after a computer name change.  
  
## Renaming a SQL Server Database Engine  
 If you rename the  [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance that runs the report server database, do the following:  
  
1.  Start the Reporting Services Configuration tool and connect to the report server that uses the report server database on the renamed server.  
  
2.  Open the Database Setup page.  
  
3.  In **Server Name**, type or select the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] name, and then click **Connect**.  
  
4.  Click **Apply**.  
  
 If the report server is using a local [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance, you can use *(local)* or *(local)\instancename* to specify the server. If you use *(local)* to refer to the server, you can rename the server and the connections will continue to work. If you are using a remote server, or if [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is configured using the server name, you must update the database connection information whenever the server name is changed.  
  
## Renaming a Report Server Computer  
 If you rename a computer that runs a report server, do the following:  
  
1.  Open RSReportServer.config in a text editor and modify the **UrlRoot** setting to reflect the new server name. The **UrlRoot** setting is used by delivery extensions to compose the URL used to access items stored on the report server. Changing the report server URL address requires that you update the **UrlRoot** setting so that subscriptions continue to deliver reports as expected.  
  
2.  In the same file, if it is set, modify the **ReportServerUrl** setting to reflect the new server name. Note that this setting is not used in every installation. If it is empty, do nothing.  
  
    > [!NOTE]  
    >  If you are using Windows Internet Naming Service (WINS) on your corporate network, the report server and Report Manager may continue to be available under the previous name for a period of time. WINS maps an IP address to each computer it services. After WINS refreshes the IP address for the renamed computer, the old computer name can no longer be used to access a report server or Report Manager.  
  
## See Also  
 [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
 [Reporting Services Report Server &#40;Native Mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)   
 [Start and Stop the Report Server Service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md)   
 [rsconfig Utility &#40;SSRS&#41;](../../reporting-services/tools/rsconfig-utility-ssrs.md)  
  
  

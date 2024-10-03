---
title: "Rename a report server computer"
description: Learn how to reconfigure a report server after a computer name change. SQL Server Reporting Services might not be accessible after a computer name change.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "renaming report servers"
---
# Rename a report server computer
  Renaming a computer causes a corresponding name change for the Web server and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, if the change is on the same computer. In some cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] might not be accessible after a computer name change. Use the steps provided in this article to reconfigure a report server after a computer name change.  
  
## Rename a SQL Server database engine  
 If you rename the  [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance that runs the report server database, complete the following steps:  
  
1.  Start the Reporting Services Configuration tool and connect to the report server that uses the report server database on the renamed server.  
  
1.  Open the **Database Setup** page.  
  
1.  In **Server Name**, enter or select the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] name, and then select **Connect**.  
  
1.  Select **Apply**.  
  
 If the report server is using a local [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance, you can use `(local)` or `(local)\instancename` to specify the server. If you use `(local)` to refer to the server, you can rename the server and the connections continue to work. If you're using a remote server, update the database connection information. This action must be done whenever the server name is changed or if [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is configured using the server name.
  
## Rename a report server computer  
 If you rename a computer that runs a report server, complete the following steps:  
  
1.  Open `RSReportServer.config` in a text editor and modify the **UrlRoot** setting to reflect the new server name. The **UrlRoot** setting is used by delivery extensions to compose the URL used to access items stored on the report server. Changing the report server URL address requires that you update the **UrlRoot** setting so that subscriptions continue to deliver reports as expected.  
  
2.  In the same file, if the setting is set, modify the **ReportServerUrl** setting to reflect the new server name. This setting isn't used in every installation. If the setting is empty, do nothing.  
  
    > [!NOTE]  
    >  If you are using Windows Internet Naming Service (WINS) on your corporate network, the report server and web portal might continue to be available under the previous name for a period of time. WINS maps an IP address to each computer it services. After WINS refreshes the IP address for the renamed computer, the old computer name can no longer be used to access the report server or web portal.  
  
## Related content

- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
- [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)
- [Reporting Services report server &#40;native mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)
- [Start and stop the report server service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md)
- [rsconfig utility &#40;SSRS&#41;](../../reporting-services/tools/rsconfig-utility-ssrs.md)

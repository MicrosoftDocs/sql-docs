---
title: "Add another report server to a farm (SSRS scale-out)"
description: "Add an Additional Report Server to a Farm (SSRS Scale-out)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---

# Add another report server to a farm (SSRS scale-out)

  Adding a second or more SharePoint mode report servers to your SharePoint farm can improve the performance and response time of the report server processing. If performance slows down as you add more users, reports, and other applications to the report server, then adding additions report servers can improve performance. You should also add a second report server to increase the availability of report servers when there are issues with hardware or you're conducting general maintenance on individual servers in your environment. After the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] release, the steps to scale-out a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] environment in SharePoint mode follows standard SharePoint farm deployment and uses the SharePoint load balancing features.  
  
> [!IMPORTANT]  
>  Scale-out of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is not supported on all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md#SSRS).
  
> [!TIP]  
>  Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] you do not use [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to add servers and scale out report servers. SharePoint products manage the scale-out of reporting services as SharePoint servers with the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service are added to the farm.  
  
 For information on how to scale out native mode report servers, see [Configure a Native mode report server scale out deployment](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).  
  
##  <a name="bkmk_loadbalancing"></a> Load balancing  
 SharePoint automatically manages the Load balancing of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service applications unless your environment has a custom or non-Microsoft load balancing solution. The default SharePoint load balancing behavior is that each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Service Application balance across all the application servers where you start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service. To verify if the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service is installed and started, select **Manage services on server** in SharePoint Central Administration.  
  
##  <a name="bkmk_prerequisites"></a> Prerequisites  
  
-   You must be a local administrator to run SQL Server Setup.  
  
-   The computer must be joined to a domain.  
  
-   You need to know the name of the existing database server that is hosting the SharePoint configuration and content databases.  
  
-   The database server must be configured to allow for remote database connections.  If it isn't, you can't join the new server to the farm because the new server can't make a connection to the SharePoint configuration databases.  
  
-   The new server needs to have the same version of SharePoint installed that the current farm servers are running. For example if the farm already has SharePoint 2013 Service Pack 1 (SP1) installed, you need to also install SP1 on the new server before it can join the farm.  
  
##  <a name="bkmk_steps"></a> Steps  
 The steps in this article assume that a SharePoint farm administrator is installing and configuring the server. The diagram shows a typical three tier environment and the numbered items in the diagram are described in the following list:  
  
-   (1) Multiple web front-end (WFE) servers. The WFE servers require the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint 2016.  
  
-   (2) A single application server running [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and web sites, for example Central Administration. The following steps add a second application server to this tier.  
  
-   (3) Two SQL Server database servers.  
  
-   (4) Represents a software or hardware network load balancing solution (NLB)  

:::image type="content" source="../../reporting-services/install-windows/media/rs-sharepointscale.gif" alt-text="Screenshot of a typical three tier environment showing the numbered items.":::
  
 The following steps assume that an administrator is installing and configuring the server. The server is set up as a new application server in the farm and not used as a web front-end (WFE).  
  
|Step|Description and Link|  
|----------|--------------------------|  
|Add a SharePoint server to a farm.|You need to install SharePoint to deploy another Reporting Services application.<br/><br/>For SharePoint 2013, see [Add SharePoint server to a farm in SharePoint Server 2013](/SharePoint/install/add-web-or-application-server-to-the-farm).<br/><br/>For SharePoint 2016, see [Add SharePoint server to a farm in SharePoint Server 2016](/SharePoint/install/add-a-server-to-a-sharepoint-server-2016-farm).|  
|Install and configure Reporting Services SharePoint mode.|Run SQL Server installation. For more information on the installation of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode, see [Install the first report server in SharePoint mode](install-the-first-report-server-in-sharepoint-mode.md)<br /><br /> If the server is only used as an application server and the server isn't used as a WFE, you don't need to select **Reporting Services add-in for SharePoint products**.<br /><br /> 1) On the **Setup Role** page, select **SQL Server Feature Installation**<br /><br /> 2) On the **Feature Selection** page, select **Reporting Services - SharePoint**<br /><br /> 3) On the **Reporting Services Configuration**  page verify the **Install Only** option is selected for **Reporting Services SharePoint Mode**.|  
|Verify that Reporting Services is operational.|1) In SharePoint Central Administration, select **Manage servers in this farm** in the **System Settings** group.<br /><br /> 2) Verify the service **SQL Server Reporting Services Service**.<br /><br />For more information, see [Verify a Reporting Services installation](../../reporting-services/install-windows/verify-a-reporting-services-installation.md)|  
  
##  <a name="bkmk_additional"></a> More configuration  
 You can optimize individual [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] servers in a scaled out deployment to perform background processing only so they don't compete for resources with interactive report execution. Background processing includes schedules, subscriptions, and data alerts.  
  
 To change the behavior of individual report servers, set **\<IsWebServiceEnable>** to false in the **RSreportServer.config** configuration file.  
  
 By default reports servers are configured with \<IsWebServiceEnable> set to TRUE. When all servers are configured for TRUE, interactive and background is load balanced across all nodes in the farm.  
  
 If you configure all report servers with \<IsWebServiceEnable> set to False, you see an error message similar to the following when you try to use [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features:  
  
```output
The Reporting Services Web Service is not enabled. Configure at least one instance of the Reporting Services SharePoint Service to have <IsWebServiceEnable> set to true.
```
 
 For more information, see [Modify a Reporting Services configuration file &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md)  

## Related content

- [Add SharePoint server to a farm in SharePoint Server 2016](/SharePoint/install/add-a-server-to-a-sharepoint-server-2016-farm)
- [Add SharePoint server to a farm in SharePoint Server 2013](/SharePoint/install/add-web-or-application-server-to-the-farm)
- [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)

---
title: "Start or Stop a Power Pivot for SharePoint Server | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: ppvt-sharepoint
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Start or Stop a Power Pivot for SharePoint Server
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service and an [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] instance operate together on the same local application server to support coordinated request and data processing in a SharePoint farm.  
  
 This topic contains the following sections:  
  
 [Service Dependencies](#dependencies)  
  
 [Start or Stop the Services](#startstop)  
  
 [Effects of Stopping a Power Pivot Server](#effects)  
  
##  <a name="dependencies"></a> Service Dependencies  
 The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service has a dependency on the local Analysis Services server instance that is installed with it on the same physical server. If you stop the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service, you must also manually stop the local Analysis Services server instance. If one service is running without the other, you will encounter request allocation errors for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data processing.  
  
 The Analysis Services server should only be run by itself if you are diagnosing or troubleshooting a problem. In all other cases, the server requires the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service that runs locally on the same server.  
  
##  <a name="startstop"></a> Start or Stop the Services  
 Always use Central Administration to start or stop the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service or Analysis Services server instance. Central Administration allows you to start or stop the services together from the same page. In addition, Central Administration uses a timer job called **One or more services have started or stopped** to restart services that it thinks should be running. If you stop the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service or Analysis Services using a non-SharePoint tool, the services will be restarted when the timer job runs.  
  
 Starting and stopping services is an action that applies to a physical service instance. If you have additional [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint servers in the farm, the other servers within the farm will continue to accept requests for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data.  
  
 You cannot start or stop all physical services simultaneously across the farm. You must select each server and then start or stop a particular service.  
  
 You cannot start, pause, or stop a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service for a specific Web application, but you can remove a service from the default connection list to make it unavailable. For more information, see [Connect a Power Pivot Service Application to a SharePoint Web Application in Central Administration](../../analysis-services/power-pivot-sharepoint/connect-power-pivot-service-app-to-sharepoint-web-app-in-ca.md).  
  
1.  In Central Administration, in **System Settings**, click **Manage services on server**.  
  
2.  At the top of the page, in Server, click the down arrow, and then click **Change Server**.  
  
3.  Select the SharePoint server that has the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service or [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] instance that you want to start or stop.  
  
4.  Select the service, and then click the action. Remember to start or stop the services as a pair. If you start or stop the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service, be sure to also start or stop the Analysis Services server instance that runs on the same computer.  
  
##  <a name="effects"></a> Effects of Stopping a Power Pivot Server  
 The following table describes the effects of stopping the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service and Analysis Services service on a SharePoint server.  
  
|Effect on|Description|  
|---------------|-----------------|  
|Existing queries|Queries that are in progress on an Analysis Services server will stop immediately. The user will get a data not found or data source connection not found error.|  
|Existing data refresh jobs that are currently processing|Jobs that are in progress on the current Analysis Services server will stop immediately. Data refresh will fail and an error will be logged to data refresh history.<br /><br /> You can view the status of current jobs before you stop the service by using the Check job status page in SharePoint Central Administration.<br /><br /> While it is possible to know which jobs are currently processing, there is no way to view the queue itself to see whether other jobs are about to start.|  
|Existing data refresh requests in the queue|Scheduled data refresh requests will remain in the processing queue for a complete cycle of the schedule (that is, it stays in the queue until the next start time). If the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service is not restarted by then, the data refresh request will be dropped and an error will be logged.|  
|New requests for queries or data refresh|If you are stopping the only [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint server in the farm, new requests for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data will not be handled and a request for data will result in a data not found error.<br /><br /> If you have additional [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint servers, the request will go to one of the available servers.|  
|Usage data|Usage data will not be collected while the services are stopped.|  
  
## See Also  
 [Configure Power Pivot Service Accounts](../../analysis-services/power-pivot-sharepoint/configure-power-pivot-service-accounts.md)  
  
  

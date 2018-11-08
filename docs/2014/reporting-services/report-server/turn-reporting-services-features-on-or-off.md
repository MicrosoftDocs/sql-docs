---
title: "Turn Reporting Services Features On or Off | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, configuration"
  - "security [Reporting Services], strategies"
ms.assetid: b69db02a-43a7-4fdc-ad9b-438d817a7f83
author: markingmyname
ms.author: maghan
manager: craigg
---
# Turn Reporting Services Features On or Off
  You can turn off report server features that you do not use as part of a lockdown strategy for reducing the attack surface of a production report server. In most cases, you will want to run [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features concurrently to use all of the functionality provided in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. However, depending on your deployment model, you can disable the features that you do not require. For example, you can enable only the background processing if all report processing is configured as scheduled operations. Similarly, you can run just the Report Server Web service if you only want interactive, on-demand reporting.  
  
 The procedures in this topic show you how to turn off native mode [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features. Features can be configured in different ways, such as by editing the `RsReportServer.config` file directly or by using the **Surface Area Configuration for Reporting Services** facet of Policy-Based Management in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Use the links to locate the procedure or procedures that explain how to turn a feature on or off:  
  
-   [Report Server Web service](#RSWebSvc)  
  
-   [Scheduled events and processing](#Sched)  
  
-   [Report Manager](#ReportManager)  
  
-   [Report Builder](#ReportBuilder)  
  
-   [Windows Integrated security for report data sources](#WinIntSec)  
  
##  <a name="RSWebSvc"></a> Report Server Web Service  
  
#### To turn on or off the Report Server Web service by editing configuration  
  
1.  Open the `RsReportServer.config` file in a text editor. For more information, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](modify-a-reporting-services-configuration-file-rsreportserver-config.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
2.  To turn on the Report Server Web service, set `IsWebServiceEnabled` to `true`:  
  
    ```  
    <IsWebServiceEnabled>true</IsWebServiceEnabled>  
    ```  
  
3.  To turn off the Report Server Web service, set `IsWebServiceEnabled` to `false`:  
  
    ```  
    <IsWebServiceEnabled>false</IsWebServiceEnabled>  
    ```  
  
4.  Save your changes and then close the file.  
  
#### To turn on or off the Report Server Web service by using SQL Server Management Studio  
  
1.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and connect to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance that you want to configure.  
  
2.  In Object Explorer, right-click the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] node, point to **Policies**, and click **Facets**.  
  
3.  In the **Facet** list, select **Surface Area Configuration for Reporting Services**.  
  
4.  Under **Facet Properties**:  
  
    -   To turn on the Report Server Web service, set **WebServiceAndHTTPAccessEnabled** to `True`.  
  
    -   To turn off the Report Server Web service, set **WebServiceAndHTTPAccessEnabled** to `False`.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="Sched"></a> Scheduled Events and Delivery  
  
#### To turn on or off scheduled events and delivery by editing configuration  
  
1.  Open the RsReportServer.config file in a text editor. For more information, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](modify-a-reporting-services-configuration-file-rsreportserver-config.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
2.  To turn on scheduled report processing and delivery, set `IsSchedulingService`, `IsNotificationService`, and `IsEventService` to `true`:  
  
    ```  
    <IsSchedulingService>true</IsSchedulingService>  
    <IsNotificationService>true</IsNotificationService>  
    <IsEventService>true</IsEventService>  
    ```  
  
3.  To turn off scheduled report processing and delivery, set `IsSchedulingService`, `IsNotificationService`, and `IsEventService` to `false`:  
  
    ```  
    <IsSchedulingService>false</IsSchedulingService>  
    <IsNotificationService>false</IsNotificationService>  
    <IsEventService>false</IsEventService>  
    ```  
  
4.  Save your changes and then close the file.  
  
> [!NOTE]  
>  You cannot turn off background processing completely because it provides database maintenance functionality that is required for server operations.  
  
#### To turn on or off scheduled events and delivery by using SQL Server Management Studio  
  
1.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and connect to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance that you want to configure.  
  
2.  In Object Explorer, right-click the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] node, point to **Policies**, and click **Facets**.  
  
3.  In the **Facet** list, select **Surface Area Configuration for Reporting Services**.  
  
4.  Under **Facet Properties**:  
  
    -   To turn on scheduled events and delivery, set **ScheduleEventsAndReportDeliveryEnabled** to `True`.  
  
    -   To turn off scheduled events and delivery, set **ScheduleEventsAndReportDeliveryEnabled** to `False`.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
> [!NOTE]  
>  You cannot turn off background processing completely because it provides database maintenance functionality that is required for server operations.  
  
##  <a name="ReportManager"></a> Report Manager  
  
#### To turn on or off Report Manager by editing configuration  
  
1.  Open the RsReportServer.config file in a text editor. For instructions, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](modify-a-reporting-services-configuration-file-rsreportserver-config.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
2.  To turn on Report Manager, set `IsReportManagerEnabled` to `true`:  
  
    ```  
    <IsReportManagerEnabled>true</IsReportManagerEnabled>  
    ```  
  
3.  To turn off Report Manager, set `IsReportManagerEnabled` to `false`:  
  
    ```  
    <IsReportManagerEnabled>false</IsReportManagerEnabled>  
    ```  
  
4.  Save your changes and then close the file.  
  
#### To turn on or off Report Manager by using SQL Server Management Studio  
  
1.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and connect to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance that you want to configure.  
  
2.  In **Object Explorer**, right-click the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] node, point to **Policies**, and click **Facets**.  
  
3.  In the **Facet** list, select **Surface Area Configuration for Reporting Services**.  
  
4.  Under **Facet Properties**:  
  
    -   To turn on Report Manager, set **ReportManagerEnabled** to `True`.  
  
    -   To turn off Report Manager, set **ReportManagerEnabled** to `False`.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="ReportBuilder"></a> Report Builder  
  
#### To turn on or off Report Builder by using SQL Server Management Studio  
  
1.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and connect to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance that you want to configure.  
  
2.  In Object Explorer, right-click the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] node, and click **Properties**.  
  
3.  In the **Server Properties** dialog box, under **Select a page**, click **Security**.  
  
    -   To turn on Report Builder, select the **Enable ad hoc report executions** option.  
  
    -   To turn off Report Builder, unselect the **Enable ad hoc report executions** option.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="WinIntSec"></a> Windows Integrated Security  
  
#### To turn on or off Windows Integrated security by using SQL Server Management Studio  
  
1.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and connect to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance that you want to configure.  
  
2.  In Object Explorer, right-click the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] node, and click **Properties**.  
  
3.  In the **Server Properties** dialog box, under **Select a page**, click **Security**.  
  
    -   To turn on Windows Integrated security, select the **Enable Windows Integrated security for report data sources** option.  
  
    -   To turn off Windows integrated security, unselect the **Enable Windows Integrated security for report data sources** option.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)  
  
  

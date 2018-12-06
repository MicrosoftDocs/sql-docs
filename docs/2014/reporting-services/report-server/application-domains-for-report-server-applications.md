---
title: "Application Domains for Report Server Applications | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "application domains [Reporting Services]"
  - "recycling application domains"
ms.assetid: a455e2e6-8764-493d-a1bc-abe80829f543
author: markingmyname
ms.author: maghan
manager: craigg
---
# Application Domains for Report Server Applications
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], the report server is implemented as a single service that contains the Report Server Web service, Report Manager, and a background processing application. Each application runs in its own application domain within the single report server process. For the most part, application domains are created, configured, and managed internally. However, knowing how recycle operations occur for report server application domains can be helpful if you are investigating performance or memory issues or troubleshooting service disruptions.  
  
> [!NOTE]  
>  If you configure Report Builder access on a report server that uses Basic authentication, Report Builder will run in its own application domain. This application domain is different from other application domains that run in the server process. It is managed by the Service Controller and is not subject memory management features that re-adjust memory allocation in response to memory pressure on the to report server.  
  
 The following list describes the events that cause application domain recycle operations for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] applications:  
  
-   Scheduled recycle operations that occur at predefined intervals.  
  
-   Configuration changes on the report server.  
  
-   [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] configuration changes.  
  
-   Memory allocation failures.  
  
 The following table summarizes application domain recycling behavior in response to these events:  
  
|Event|Event description|Applies to|Configurable|Recycle operation description|  
|-----------|-----------------------|----------------|------------------|-----------------------------------|  
|Scheduled recycle operations that occur at predefined intervals|By default, application domains are recycled every 12 hours.<br /><br /> Scheduled recycle operations are a common practice for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications that promote overall process health.|Report server Web service<br /><br /> Report Manager<br /><br /> Background processing application|Yes. `RecycleTime` configuration setting in the RSReportServer.config file determines the recycle interval.<br /><br /> `MaxAppDomainUnloadTime` sets the wait time during which background processing is allowed to complete.|[!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] manages the recycle operation for the Web service and Report Manager.<br /><br /> For the background processing application, the report server creates a new application domain for new jobs that are initiated from schedules. Jobs already in progress are allowed to complete in the current application domain until the wait time expires.|  
|Configuration changes on the report server|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] will recycle application domains in response to changes in the RSReportServer.config file.|Report server Web service<br /><br /> Report Manager<br /><br /> Background processing application|No.|You cannot stop recycle operations from occurring. However, recycle operations that occur in response to configuration changes are handled the same way as the scheduled recycle operations. New application domains are created for new requests while current requests and jobs complete in the current application domain.|  
|[!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] configuration changes|[!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] will recycle application domains if there are changes to the files that it monitors (for example, machine.config and Web.config files, and [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] program files).|Report server Web service<br /><br /> Report Manager|No.|[!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] manages the operation.<br /><br /> Recycle operations that are initiated by [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] do not affect the background processing application domain.|  
|Memory pressure and memory allocation failures|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CLR will immediately recycle application domains in the event of a memory allocation failure or when the server is under high memory pressure conditions.|Report server Web service<br /><br /> Report Manager<br /><br /> Background processing application|No.|Under high memory pressure, the report server will not accept new requests in the current application domain. During the period in which the server denies new requests, HTTP 503 errors occur. New application domains will not be created until the old application domain is unloaded. This means that if you make a configuration file change while the server is under high memory pressure, requests and jobs that are in progress might not start or complete.<br /><br /> In the event of memory allocation failure, all application domains are immediately restarted. Jobs and requests that were in progress are dropped. You must restart those jobs and requests manually.|  
  
## Planned and Unplanned Recycle Operations  
 Recycle operations are either planned or unplanned depending on the conditions that bring about the operation:  
  
-   Planned recycle operations occur at regular intervals that are defined in the RSReportServer.config file. The default is every 12 hours. This is a common practice for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications that promote overall process health. For planned recycle operations, the report server creates additional application domains for new requests. Requests already in progress are allowed to complete in the current application domain until the wait time expires. Configuration settings that govern planned recycle operations are set for the server as a whole. You cannot configure a different recycle schedule or memory threshold for each application.  
  
-   Unplanned recycle operations occur at arbitrary times in response to configuration changes, memory pressure, and memory allocation failures:  
  
    -   For configuration changes, the report server will try to use a soft recycle that redirects new requests to a new instance of the application domain. If the soft recycle fails, the server initiates a hard application domain recycle that cancels all in-progress requests, shuts down the current application domains, and restarts the application domains.  
  
    -   Memory allocation failures indicate that system resources are insufficient for the amount of report processing performed by the server. A hard recycle operation for all application domains occurs in response to a memory allocation failure. All request queues are cleared. Canceled requests are not restarted. Users who were interactively viewing a report must refresh or reopen the report. Scheduled processing will occur at the next scheduled time. If the delay is unacceptable, you can refresh a report snapshot manually or modify a subscription schedule or report snapshot schedule so that it runs immediately.  
  
 The application domains for the Report Server Web service, Report Manager, and the background processing application might be recycled together or individually, depending on the circumstances that cause the recycling to occur:  
  
-   Recycle operations initiated by [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] affect only the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications: Report Server Web service and Report Manager. [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] will recycle application domains based if there are changes to the files that it monitors. Recycle operations that are initiated by [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] are typically independent of recycle operations for the background processing application.  
  
-   Recycle operations initiated by the report server typically affect Report Server Web service, Report Manager, and the background processing application. Recycle operations occur in response to changes to the configuration settings and service restarts.  
  
## RSReportServer Configuration Settings for Application Domains  
 Configuration settings are specified in the in the [RSReportServer.config file](rsreportserver-config-configuration-file.md). The following example shows the default configuration settings for planned application domain recycling behavior.  
  
 `<RecycleTime>720</RecycleTime>`  
  
 `<MaxAppDomainUnloadTime>30</MaxAppDomainUnloadTime>`  
  
 The following table describes these elements.  
  
|Element|Applies to|Definition|  
|-------------|----------------|----------------|  
|`RecycleTime`|All three [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] application domains|Specifies how often the application domains are recycled. The default recycle schedule conforms to the 12-hour pattern typically followed for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] application domain recycling. At the scheduled time, all new requests are forwarded to a new instance of the application domain. Requests that are currently in progress in the original instance are allowed to complete. Once all processes are complete, the original instance is deleted and the new instance becomes the sole active application domain instance.<br /><br /> The default value is 720 minutes.|  
|`MaxAppDomainUnloadTime`|Background processing application domain only|By default, a report server allocates a wait time of 30 minutes, during which an application domain is allowed to shut down during a recycle operation. If the jobs that are currently in process cannot be completed during the allotted time (or if a job is taking longer than the wait time allows), the application domain instance is restarted immediately. All incomplete jobs are terminated.<br /><br /> For more information about how to view status or cancel jobs that running on the report server, see [Cancel Report Server Jobs &#40;Management Studio&#41;](../tools/cancel-report-server-jobs-management-studio.md).|  
  
> [!NOTE]  
>  Although the Report Server Web service and Report Manager are [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications, neither application responds to scheduled application domain recycling that might be specified in machine.config for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications hosted in IIS.  
  
## See Also  
 [RSReportServer Configuration File](rsreportserver-config-configuration-file.md)   
 [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](modify-a-reporting-services-configuration-file-rsreportserver-config.md)   
 [Configure Available Memory for Report Server Applications](../report-server/configure-available-memory-for-report-server-applications.md)  
  
  

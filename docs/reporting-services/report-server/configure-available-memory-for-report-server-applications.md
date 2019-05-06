---
title: "Configure Available Memory for Report Server Applications | Microsoft Docs"
ms.date: 03/20/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "memory [Reporting Services]"
  - "memory thresholds [Reporting Services]"
ms.assetid: ac7ab037-300c-499d-89d4-756f8d8e99f6
author: maggiesMSFT
ms.author: maggies
---
# Configure Available Memory for Report Server Applications
  Although [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] can use all available memory, you can override default behavior by configuring an upper limit on the total amount of memory resources that are allocated to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] server applications. You can also set thresholds that cause the report server to change how it prioritizes and processes requests depending on whether it is under low, medium, or heavy memory pressure. At low levels of memory pressure, the report server responds by giving a slightly higher priority to interactive or on-demand report processing. At high levels of memory pressure, the report server uses multiple techniques to remain operational using the limited resources available to it.  
  
 This topic describes the configuration settings that you can specify and how the server responds when memory pressure becomes a factor in processing requests.  
  
## Memory Management Policies  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] responds to system resource constraints by adjusting the amount of memory that is allocated to specific applications and types of processing requests. Applications that run in the Report Server service and that are subject to memory management include:  
  
-   Report Manager, a Web front-end application for the report server.  
  
-   Report Server Web service, used for interactive report processing and on-demand requests.  
  
-   A background processing application, used for scheduled report processing, subscription delivery, and database maintenance.  
  
 Memory management policies apply to the Report Server service as a whole, and not to individual applications that run within the process.  
  
 If there is no memory pressure on the system, each server application requests some memory at startup, in advance of receiving requests, to deliver optimum performance when requests are eventually received. As memory pressure builds, the report server adjusts its process model as described in the following table.  
  
|Memory pressure|Server response|  
|---------------------|---------------------|  
|Low|Current requests continue to process. New requests are almost always accepted. Requests that are directed to the background processing application are given a lower priority than requests directed to the Report Server Web service.|  
|Medium|Current requests continue to process. New requests might be accepted. Requests that are directed to the background processing application are given a lower priority than requests directed to the Report Server Web service. Memory allocations for all three server applications are reduced, with relatively larger reductions to background processing to make more memory available for Web service requests.|  
|High|Memory allocation is further reduced. Server applications that request more memory are denied. Current requests are slowed down and take longer to complete. New requests are not accepted. The report server swaps in-memory data files to disk.<br /><br /> If memory constraints become severe and there is no memory available to handle new requests, the report server will return an HTTP 503 server unavailable error while current requests are completing. In some cases, the application domains might be recycled to immediately reduce memory pressure.|  
  
 Although you cannot customize the report server responses to different memory pressure scenarios, you can specify configuration settings that define the boundaries that separate high, medium, and low memory pressure responses.  
  
## When to Customize Memory Management Settings  
 The default settings specify unequal ranges for low, medium, and high memory pressure. By default, the low memory pressure zone is larger than the zones for medium and high memory pressure. This configuration is optimum for processing loads that are evenly distributed or that grow or decline incrementally. In this scenario, the transition between zones is gradual and the report server has time to adjust its response.  
  
 Modifying the default settings is useful if the load pattern includes spikes. When there are sudden spikes in the processing load, the report server might go from no memory pressure to memory allocation failures very quickly. This might occur if you have multiple concurrent instances of a memory-intensive report that start at the same time. To handle this type of processing load, you want the report server to move into a medium or high memory pressure response as soon as possible so that it can slow down processing. This allows more requests to complete. To do this, you should lower the value for **MemorySafetyMargin** to make the low memory pressure zone smaller relative to the other zones. Doing so will cause responses for medium and high memory pressure to occur earlier.  
  
## Configuration Settings for Memory Management  
 Configuration settings that control memory allocation for the report server include **WorkingSetMaximum**, **WorkingSetMinimum**, **MemorySafetyMargin**, and **MemoryThreshold**.  
  
-   **WorkingSetMaximum** and **WorkingSetMinimum** define the range of available memory. You can configure these settings to set a range of available memory for the report server applications. This can be useful if you are hosting multiple applications on the same computer and you determine that the report server is using a disproportionate amount of system resources relative to other applications on the same computer.  
  
-   **MemorySafetyMargin** and **MemoryThreshold** set the boundaries for low, medium, and high levels of memory pressure. For each state, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] takes corrective action to ensure report processing and other requests are handled appropriately relative to the amount of memory that is available on the computer. You can specify configuration settings that determine the delineation between low, high, and medium pressure levels.  
  
     Although you can change the configuration settings, doing so will not improve report processing performance. Changing the configuration settings is useful only if requests are getting dropped before they complete. The best way to improve server performance is to deploy the report server or individual report server applications on dedicated computers.  
  
 The following illustration shows how the settings are used together to distinguish between low, medium, and high levels of memory pressure:  
  
 ![Configuration settings for memory state](../../reporting-services/report-server/media/rs-memoryconfigurationzones.gif "Configuration settings for memory state")  
  
 The following table describes **WorkingSetMaximum**, **WorkingSetMinimum**, **MemorySafetyMargin**, and **MemoryThreshold** settings. Configuration settings are specified in the [RSReportServer.config file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md).  
  
|Element|Description|  
|-------------|-----------------|  
|**WorkingSetMaximum**|Specifies a memory threshold after which no new memory allocations requests are granted to report server applications.<br /><br /> By default, the report server sets **WorkingSetMaximum** to the amount of available memory on the computer. This value is detected when the service starts.<br /><br /> This setting does not appear in the RSReportServer.config file unless you add it manually. If you want the report server to use less memory, you can modify the RSReportServer.config file and add the element and value. Valid values range from 0 to maximum integer. This value is expressed in kilobytes.<br /><br /> When the value for **WorkingSetMaximum** is reached, the report server does not accept new requests. Requests that are currently in progress are allowed to complete. New requests are accepted only when memory use falls below the value specified through **WorkingSetMaximum**.<br /><br /> If existing requests continue to consume additional memory after the **WorkingSetMaximum** value has been reached, all report server application domains will be recycled. For more information, see [Application Domains for Report Server Applications](../../reporting-services/report-server/application-domains-for-report-server-applications.md).|  
|**WorkingSetMinimum**|Specifies a lower limit for resource consumption; the report server will not release memory if overall memory use is below this limit.<br /><br /> By default, the value is calculated at service startup. The calculation is that the initial memory allocation request is for 60 percent of **WorkingSetMaximum**.<br /><br /> This setting does not appear in the RSReportServer.config file unless you add it manually. If you want to customize this value, you must add the **WorkingSetMinimum** element to the RSReportServer.config file. Valid values range from 0 to maximum integer. This value is expressed in kilobytes..|  
|**MemoryThreshold**|Specifies a percentage of **WorkingSetMaximum** that defines the boundary between high and medium pressure scenarios. If report server memory use reaches this value, the report server slows down request processing and changes the amount of memory allocated to different server applications. The default value is 90. This value should be greater than the value set for **MemorySafetyMargin**.|  
|**MemorySafetyMargin**|Specifies a percentage of **WorkingSetMaximum** that defines the boundary between medium and low pressure scenarios. This value is the percentage of available memory that is reserved for the system and cannot be used for report server operations. The default value is 80.|  
  
> [!NOTE]  
>  **MemoryLimit** and **MaximumMemoryLimit** settings are obsolete in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions. If you upgraded an existing installation or using an RSReportServer.config file that includes those settings, the report server no longer reads those values.  
  
#### Example of Memory Configuration Settings  
 The following example shows the configuration settings for a report server computer that uses custom memory configuration values. If you want to add **WorkingSetMaximum** or **WorkingSetMinimum**, you must type the elements and values in the RSReportServer.config file. Both values are integers that express kilobytes of RAM you are allocating to the server applications. The following example specifies that total memory allocation for the report server applications cannot exceed 4 gigabytes. If the default value for **WorkingSetMinimum** (60% of **WorkingSetMaximum**) is acceptable, you can omit it and specify just **WorkingSetMaximum** in the RSReportServer.config file. This example includes **WorkingSetMinimum** to show how it would appear if you wanted to add it:  
  
```  
      <MemorySafetyMargin>80</MemorySafetyMargin>  
      <MemoryThreshold>90</MemoryThreshold>  
      <WorkingSetMaximum>4000000</WorkingSetMaximum>  
      <WorkingSetMinimum>2400000</WorkingSetMinimum>  
```  
  
#### About ASP.NET Memory Configuration Settings  
 Although the Report Server Web service and Report Manager are [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications, neither application responds to memory configuration settings that you specify in the **processModel** section of machine.config for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications that run in IIS 5.0 compatibility mode. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] reads memory configuration settings from the RSReportServer.config file only.  
  
## See Also  
 [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
 [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
 [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md)   
 [Application Domains for Report Server Applications](../../reporting-services/report-server/application-domains-for-report-server-applications.md)  
  
  

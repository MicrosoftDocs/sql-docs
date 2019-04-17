---
title: "Performance Counters for the ReportServer:Service  and ReportServerSharePoint:Service Performance Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Report Server service, performance counters"
ms.assetid: 2bcacab2-3a4f-4aae-b123-19d756b9b9ed
author: markingmyname
ms.author: maghan
manager: kfile
---
# Performance Counters for the ReportServer:Service  and ReportServerSharePoint:Service Performance Objects
  This topic describes performance counters for the following [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] performance objects:  
  
-   `ReportServer:Service`  
  
-   `ReportServerSharePoint:Service`  
  
> [!NOTE]  
>  The performance objects are used to monitor events on the local report server. If you are running a report server in a scale-out deployment, the counts apply to the current server and not the scale-out deployment as a whole.  
  
 The performance objects are available in the Windows Performance Monitor (**Perfmon.exe**). For more information, see the Windows documentation. [Runtime Profiling](https://msdn.microsoft.com/library/w4bz2147.aspx) (https://msdn.microsoft.com/library/w4bz2147.aspx).  
  
 In this topic:  
  
-   [ReportServer:Service Performance Counters (Native Mode Report Server)](#bkmk_ReportServer)  
  
-   [ReportServerSharePoint:Service (SharePoint Mode Report Server)](#bkmk_ReportServerSharePoint)  
  
-   [Use PowerShell Cmdlets to return lists](#bkmk_powershell)  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] SharePoint mode | Native mode.  
  
##  <a name="bkmk_ReportServer"></a> ReportServer:Service Performance Counters (Native Mode Report Server)  
 The `ReportServer:Service` performance object includes a collection of counters to track HTTP-related events and memory-related events for a report server instance. This performance object appears one time for each [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] instance on the computer, and you can add or remove counters from the performance object for each instance. Counters for the default instance appear in the format `ReportServer:Service`. Counters for named instances appear in the format `ReportServer$<`*instance_name*`>:Service`.  
  
 The `ReportServer:Service` performance object was new in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], and it provides a subset of counters that were included with Internet Information Services (IIS) and [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] in previous versions of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. These new counters are specific to [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], and they track HTTP-related events for the report server, such as requests, connections, and logon attempts. Additionally, this performance object includes counters to track memory management events.  
  
 The following table lists the counters that are included in the `ReportServer:Service` performance object.  
  
 ![PowerShell related content](../media/rs-powershellicon.jpg "PowerShell related content") The following Windows PowerShell script will return the list of performance counters for the CounterSetName  
  
```  
(get-counter -listset "ReportServer:Service").paths  
```  
  
|Counter|Description|  
|-------------|-----------------|  
|`Active connections`|The number of connections currently active on the server.|  
|`Bytes Received Total`|The number of bytes received by the server. This counter counts the raw bytes received in total by both Report Manager and the report server.|  
|`Bytes Received/sec`|The number of bytes received per second by the server. This counter is updated only when a transfer is completed. This means that the counter remains at 0 and then the value increases after a transfer is complete.|  
|`Bytes Sent Total`|The number of bytes sent from the server. This counter counts the raw bytes sent in total by both Report Manager and the report server.|  
|`Bytes Sent/sec`|The number of bytes sent per second from the server. This counter is updated only when a transfer is completed. This means that the counter remains at 0 and then the value increases after a transfer is complete.|  
|`Errors Total`|The total number of errors that occur during the processing of HTTP requests. These errors include HTTP status codes in the 400s and 500s.|  
|`Errors/sec`|The total number of errors that occur per second during the processing of HTTP requests. These errors include HTTP status codes in the 400s and 500s.|  
|`Logon Attempts Total`|The number of logon attempts made from RSWindows authentication types. RSWindows authentication types include RSWindowsNegotiate, RSWindowsNTLM, RSWindowsKerberos, and RSWindowsB asic. The value zero (0) represents Custom authentication.|  
|`Logon Attempts/sec`|The rate of logon attempts.|  
|`Logon Successes Total`|The number of successful logons for RSWindows authentication types. RSWindows authentication types include RSWindowsNegotiate, RSWindowsNTLM, RSWindowsKerberos, and RSWindowsB asic. The value zero (0) represents Custom authentication.|  
|`Logon Successes/sec`|The rate of successful logons.|  
|`Memory Pressure State`|One of the following numbers, from 1-5, which indicates the current memory state of the server:<br /><br /> 1: No pressure<br /><br /> 2: Low pressure<br /><br /> 3: Medium pressure<br /><br /> 4: High pressure<br /><br /> 5: Exceeded pressure|  
|`Memory Shrink Amount`|The number of bytes that the server requested to shrink the memory in use.|  
|`Memory Shrink Notifications/sec`|The number of notifications that the server issued in the last second to shrink the memory in use. This value indicates how often the server experiences memory pressure.|  
|`Requests Disconnected`|The number of requests that are disconnected because of a communication failure.|  
|`Requests Executing`|The number of requests that are currently processing.|  
|`Requests Not Authorized`|The number of requests that fail with an HTTP 401 status code.|  
|`Requests Rejected`|The total number of requests that were not processed because of insufficient server resources. This counter represents the number of requests that return a HTTP 503 status code, which indicates that the server is too busy.|  
|`Requests Total`|The total number of requests that are received by the report server service since startup. This counter counts requests that are sent to Report Manager and requests that are sent from Report Manager to the report server.|  
|`Requests/sec`|The number of requests that are processed per second. This value represents the current throughput of the application.|  
|`Tasks Queued`|The number of tasks that are waiting for a thread to become available for processing. Each request made to the report server corresponds to one or more tasks. This counter represents only the number of tasks that are ready for processing; it does not include the number of tasks that are currently running.|  
  
##  <a name="bkmk_ReportServerSharePoint"></a> ReportServerSharePoint:Service (SharePoint Mode Report Server)  
 The `ReportServerSharePoint:Service` performance object was added in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  
  
 ![PowerShell related content](../media/rs-powershellicon.jpg "PowerShell related content") The following Windows PowerShell script will return the list of performance counters for the CounterSetName  
  
```  
(get-counter -listset "ReportServerSharePoint:Service").paths  
```  
  
|Counter|  
|-------------|  
|`Memory Pressure State`|  
|`Memory Shrink Amount`|  
|`Memory Shrink Notifications/Sec`|  
  
##  <a name="bkmk_powershell"></a> Use PowerShell Cmdlets to return lists  
 ![PowerShell related content](../media/rs-powershellicon.jpg "PowerShell related content") The following Windows PowerShell script will return the list of performance counters for the CounterSetName "ReportServerSharePoint:Service":  
  
```  
(get-counter -listset "ReportServerSharePoint:Service").paths  
```  
  
## See Also  
 [Monitoring Report Server Performance](monitoring-report-server-performance.md)   
 [Performance Counters for the MSRS 2014 Web Service and MSRS 2014 Windows Service Performance Objects &#40;Native Mode&#41;](../report-server/performance-counters-msrs-2011-web-service-performance-objects.md)   
 [Performance Counters for the MSRS 2014 Web Service SharePoint Mode and MSRS 2014 Windows Service SharePoint Mode Performance Objects &#40;SharePoint Mode&#41;]../performance-counters-msrs-2011-sharepoint-mode-performance-objects.md)  
  
  

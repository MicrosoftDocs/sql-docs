---
title: "Report Server HTTP Log | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "HTTP [Reporting Services]"
ms.assetid: 6cc433b7-165c-4b16-9034-79256dd6735f
author: markingmyname
ms.author: maghan
manager: craigg
---
# Report Server HTTP Log
  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Report Server HTTP log files keep a record of every HTTP request and response handled by the report server. Because request overflow and timeout errors do not reach the report server, they are not recorded in the log file.  
  
 HTTP logging is not enabled by default. To enable the HTTP logging, modify the **ReportingServicesService.exe.config** configuration file to use this feature in your installation.  
  
## Viewing Log Information  
 The log is an ASCII text file. You can use any text editor to view the file. The Report Server HTTP log file is equivalent to the W3C extended log file in IIS and uses similar fields so that you can use existing IIS log file viewers to read the report server HTTP log file. The following table provides additional information about the HTTP log file:  
  
|||  
|-|-|  
|**File name**|By default, the log file name(s) are<br /><br /> `ReportServerService_HTTP_<timestamp>.log.`<br /><br /> You can customize the prefix of the file name by modifying the HttpTraceFileName attribute in the ReportingServicesService.exe.config file. The timestamp is based on Coordinated Universal Time (UTC).|  
|**File location**|The files are written to the following location:<br /><br /> `\Microsoft SQL Server\<SQL Server Instance>\Reporting Services\LogFiles`|  
|**File format**|The file is in EN-US format. It is an ASCII text file.|  
|**File creation and retention**|The HTTP log is created after you enable it in the configuration file, restart the service, and the report server handles an HTTP request. If you configure the settings but do not see the log file, open a report or start a report server application (such as Report Manager) to generate an HTTP request to create the file.<br /><br /> A new instance of the log file will be created after each service restart and subsequent HTTP request to the report server.<br /><br /> By default, trace logs are limited to 32 megabytes and deleted after 14 days.|  
  
## Configuration Settings for Report Server HTTP Log  
 To configure the Report Server HTTP log, use Notepad to modify the **ReportingServicesService.exe.config** file. The configuration file is located in the \Program Files\Microsoft SQL Server\MSSQL.n\Reporting Services\ReportServer\Bin folder.  
  
 To enable the HTTP server, add `http:4` to the RStrace section of the ReportingServicesService.exe.config file. All other HTTP log file entries are optional. The following example includes all settings so that you can paste the whole section over the RStrace section, and then delete the settings you do not need.  
  
```  
   <RStrace>  
         <add name="FileName" value="ReportServerService_" />  
         <add name="FileSizeLimitMb" value="32" />  
         <add name="KeepFilesForDays" value="14" />  
         <add name="Prefix" value="tid, time" />  
         <add name="TraceListeners" value="debugwindow, file" />  
         <add name="TraceFileMode" value="unique" />  
         <add name="HttpTraceFileName" value="ReportServerService_HTTP_" />  
         <add name="HttpTraceSwitches" value="date,time, clientip,username,serverip,serverport,host,method,uristem,uriquery,protocolstatus,bytesreceived,timetaken,protocolversion,useragent,cookiereceived,cookiesent,referrer" />  
         <add name="Components" value="all:3,http:4" />  
   </RStrace>  
```  
  
## Log File Fields  
 The following table describes the fields that are available in the log. The field list is configurable; you can specify which fields to include through the `HTTPTraceSwitches` configuration setting. The **Default** column specifies whether the field will be included in the log file automatically if you do not specify `HTTPTraceSwitches`.  
  
|Field|Description|Default|  
|-----------|-----------------|-------------|  
|HttpTraceFileName|This value is optional. The default value is ReportServerServiceHTTP_. You can specify a different value if want to use a different file naming convention (for example, to include the server name if you are saving log files to a central location).|Yes|  
|HttpTraceSwitches|This value is optional. If you specify it, you can configure the fields used in the log file in comma-delimited format.|No|  
|Date|The date when the activity occurred.|No|  
|Time|The time when the activity occurred.|No|  
|ClientIp|The IP address of the client accessing the report server.|Yes|  
|UserName|The name of the user who accessed the report server.|No|  
|ServerPort|The port number used for the connection.|No|  
|Host|The content of the host header.|No|  
|Method|The action or SOAP method called from the client.|Yes|  
|UriStem|The resource accessed.|Yes|  
|UriQuery|The query used to access the resource.|No|  
|ProtocolStatus|The HTTP status code.|Yes|  
|BytesReceived|The number of bytes received by the server.|No|  
|TimeTaken|The time (in milliseconds) from the instant HTTP.SYS returns request data until the server finishes the last send, excluding network transmission time.|No|  
|ProtocolVersion|The protocol version used by the client.|No|  
|UserAgent|The browser type used by the client.|No|  
|CookieReceived|The content of the cookie received by the server.|No|  
|CookieSent|The content of the cookie sent by the server.|No|  
|Referrer|The previous site visited by the client.|No|  
  
## See Also  
 [Report Server Service Trace Log](report-server-service-trace-log.md)   
 [Reporting Services Log Files and Sources](../report-server/reporting-services-log-files-and-sources.md)   
 [Errors and Events Reference &#40;Reporting Services&#41;](../troubleshooting/errors-and-events-reference-reporting-services.md)  
  
  

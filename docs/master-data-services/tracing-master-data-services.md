---
title: Tracing
description: The Web.config file contains a tracing section, new in SQL Server 2016 Master Data Services. Learn about default tracing behavior.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ramakoni
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 45823fc8-723a-49f2-9a11-94d241245cfd
author: CordeliaGrey
ms.author: jiwang6
---
# Tracing (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

Once you have installed Master Data Services (MDS), you may find the trace logging feature useful for error diagnostics, support requests, and monitoring of application usage and performance. This article covers the steps to turn on and configure trace logging. Specifically, this article covers tracing to a text log file. This article also discusses the procedure to create a log file that can help troubleshoot failures that may occur when creating an MDS database.

## Background

Master Data Services consists of an ASP.NET web application (Master Data Manager) and a WCF service; both are hosted in IIS and a SQL Server database. Master Data Manager requests and external calls to the web services API end point are handled by a common service layer. The service layer can log each operation request/response, important events, and errors.

As an administrator with access to files on the IIS web server, you can enable logging by modifying the MDS *Web.config* file.

By default, the *Web.config* file is in one of the following web application folders:

- **SQL Server 2008 R2**: *program_files\Microsoft SQL Server\Master Data Services\WebApplication*

- **SQL Server 2012 and later versions**: *program_files\Microsoft SQL Server\\\<nnn>\Master Data Services\WebApplication*

The numbers *nnn* in the path correspond to the version of SQL Server being installed. The following table identifies versions for the paths:

|Version|nnn|
|-|-|
|SQL Server 2019 |150|
|SQL Server 2017 |140|
|SQL Server 2016 |130|
|SQL Server 2014 |120|
|SQL Server 2012 |110|

> [!CAUTION]
> Updating *Web.config* will cause the MDS application domain in IIS to recycle. Existing user sessions will lose cached information, and users may experience session errors or slow page loads. Perform changes at off-peak times if possible.

## Tracing logging for MDS web-application

### SQL Server 2016 and later versions

The *Web.config* file contains a tracing section, as shown below. This section is introduced in [!INCLUDE [sssql15-md](../includes/sssql16-md.md)] [!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)].
  
```XML
<sources>  
     <!-- Adjust the switch value to control the types of messages that should be logged.   
           https://msdn.microsoft.com/library/system.diagnostics.sourcelevels  
           Use a switchValue of Verbose to generate a full log. Please be aware that   
           the trace file can get quite large very quickly. -->  
     <source name="MDS" switchType="System.Diagnostics.SourceSwitch" switchValue="Warning, ActivityTracing">  
          <listeners>  
          <!-- Set a directory path where the service account you chose while setting up Master Data Services has read and write privileges.  
               Default path is Logs in WebApplication folder, for example C:\Program Files\Microsoft SQL Server\130\Master Data Services\WebApplication  
               New log file will be created every day or every 10 mb.  
               When directory size hits the 200 mb limitation, the oldest file will be deleted. -->  
          <add name="FileTraceListener"  
               type="Microsoft.MasterDataServices.Core.Logging.FileTraceListener, Microsoft.MasterDataServices.Core"   
               initializeData="DirectoryPath = Logs; FileSizeInMb = 10; MaxDirectorySizeInMb = 200"/>  
          <remove name="Default"/>  
          </listeners>  
     </source>  
</sources>
```  
  
The following is the default tracing behavior:
  
- Tracing is enabled for `Warning` and `ActivityTracing` messages.  
  
     For more information, see [SourceLevels Enumeration](/dotnet/api/system.diagnostics.sourcelevels).  
  
- The logs are saved in the *Logs* folder under the *WebApplication* folder. The default location is *C:\Program Files\Microsoft SQL Server\nnn\Master Data Services\WebApplication\Logs*.  
  
- The file is created for each day or every 10 MB.  
  
- When the size of the directory reaches 200 MB, the oldest log is deleted.  
  
- The log format is CSV. The following table describes the log format:  
  
    |Element|Description|  
    |-------------|-----------------|  
    |Time|When the trace entry occurs|  
    |CorrelationID|One correlation ID is assigned for each request. All the traces triggered by this request will share the same correlation ID.<br/> When an error occurs in the UI, the correlation ID appears in the error message|  
    |Operation|Request operation name. If the request is a web UI request, the operation name is the url. If the request is an API request, the operation name is the service name|  
    |Level|Level of this trace entry|  
    |Message|Message body of the trace|  

### SQL Server 2014 and earlier versions

#### Diagnostics section from the originally installed Web.config file

The following file snippet shows the diagnostics section from the originally installed *Web.config* file.

> [!NOTE]
> The `switchValue` is set to `Off`. Additionally, the example lines are commented out. These lines are examples for adding trace listeners of various types.

```XML
<system.diagnostics>  
     <sources>  
     <!-- Adjust the switch value to control the types of messages that should be logged. -->  
          <source name="MDS" switchType="System.Diagnostics.SourceSwitch" switchValue="Off">  
          <listeners>  
          <!-- Enable and configure listeners as desired to obtain trace messages. -->  
          <!-- <add name="LogFileListener" type="System.Diagnostics.TextWriterTraceListener" initializeData="MdsTrace.log" traceOutputOptions="DateTime" /> -->  
          <!-- <add name="EtwListener" type="System.Diagnostics.Eventing.EventProviderTraceListener, System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089"  
          initializeData="{F2A341B8-CA5F-49ad-B00C-A82D3FCF948B}"/> -->  
          <!-- <remove name="Default"/> -->  
          </listeners>  
         </source>  
     </sources>  
     <trace autoflush="true"/>  
</system.diagnostics>
```

#### Turning on log file tracing

To enable logging, change `switchValue` to `All` or another valid value as described below in [Table 1](#table-1---switchvalue-settings-for-logging). To enable the output to a log file, uncomment the `LogFileListener` line as shown in the following file snippet:

```xml
<system.diagnostics>  
     <sources>  
          <!-- Adjust the switch value to control the types of messages that should be logged. -->  
          <source name="MDS" switchType="System.Diagnostics.SourceSwitch" switchValue="All">  
          <listeners>  
          <!-- Enable and configure listeners as desired to obtain trace messages. -->  
          <add name="LogFileListener" type="System.Diagnostics.TextWriterTraceListener" initializeData="MdsTrace.log" traceOutputOptions="DateTime"/>  
          <!-- <add name="EtwListener" type="System.Diagnostics.Eventing.EventProviderTraceListener, System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" initializeData="{F2A341B8-CA5F-49ad-B00C-A82D3FCF948B}"/> -->  
          <!-- <remove name="Default"/> -->  
          </listeners>
          </source>  
     </sources>  
     <trace autoflush="true"/>  
</system.diagnostics> 
```

##### Table 1 - SwitchValue settings for logging

|Setting|What is logged|
|-|-|
|Off|nothing|
|Error|errors only|
|Warning|errors and warnings|
|Information|errors, warnings, informational messages |
|Verbose|"Information" and other debugging trace information including API requests and responses in XML format |
|ActivityTracing |start and stop events only |
|All|"Verbose" and "ActivityTracing" |

##### File name and path

The `initializeData` value is the name of the log file. This may be modified to another name or to include a desired path. If the path isn't specified, the file will default to the web application directory path (where *Web.config* resides).

> [!IMPORTANT]
> The service account for the MDS application pool must have write access to the log file location.

#### Logging level of detail

[Table 2](#table-2---mds-logging-event-types) below describes the categories of trace events that may be logged or sorted with the most important/critical events at the top. [Table 1](#table-1---switchvalue-settings-for-logging) above lists the valid settings for the logging `switchValue`. This setting may be adjusted to produce the right amount of logging details to suit the situation.

##### Table 2 - MDS logging event types

|Event Type |Description|
|-|-|
|Critical |a fatal error or application crash |
|Error|a recoverable error |
|Warning|a noncritical problem |
|Information|an informational message |
|Verbose |a debugging trace message |
|Start|starting of a logical operation |
|Stop|stopping of a logical operation |

#### Log setting recommendations

- For normal operation, use the `Off` setting to avoid logging altogether or use the `Error` or `Warning` settings that will keep the log small while alerting administrators to problems.
- Use the `All` setting for support/troubleshooting situations.
- Use `ActivityTracing` for performance measurement or usage monitoring.
- Use `Information` only if prepared to periodically check and clean logs. The logs could get lengthy with this setting. This setting is helpful for tracking usage and usage patterns.
- Don't use `Verbose` or `All` under normal operating conditions, as the volume of data logged will negatively affect performance.

#### Steps for creating a log file for product support

In situations where an unexpected error occurs and further diagnostics are required, it's helpful to create a log file that traces the events leading up to the problem along with the service requests and responses.

To produce a concise and helpful log file, follow these steps:

> [!CAUTION]
> Updating *Web.config* will cause the MDS application domain in IIS to recycle.

1. If possible, stop the MDS application pool in IIS Manager.

   > [!WARNING]
   > Do this only if the application can be taken off line.

1. If possible, move or delete the current log file (if one already exists).

1. Open the *Web.config* file by using a text editor.

    > [!NOTE]
    > By default, the *Web.config* file is in one of the following web application folders:
    >
    > - **SQL Server 2008 R2**: *program_files\Microsoft SQL Server\Master Data Services\WebApplication*
    > - **SQL Server 2012**: *program_files\Microsoft SQL Server\110\Master Data Services\WebApplication*

1. Find the `system.diagnostics` section.

1. Change the `switchValue` to `All` or `ActivityTracing` and uncomment the log file line as shown in the [Turning on log file tracing](#turning-on-log-file-tracing) section of this article.

    1. Use `switchValue="All"` for error diagnostics.
    1. Use `switchValue="ActivityTracing"` for performance diagnostics.
    1. The *MdsTrace.log* file name can be prefixed with a path if desired.

1. If the application pool was previously stopped, start the MDS application pool in IIS. Or else, wait for the log file to appear after some time and web application activity. The web application will periodically reload configuration settings from the file (should be within minutes).

1. Reproduce the problem and perform the same actions or requests that led to the error.

1. If you're able to stop the application pool:

    1. Stop the application pool.
    1. Retrieve the log file (you may need to wait for processes to finish; there could be a delay after stopping the app pool).

    Or else, open the log file by using an editor that doesn't lock the file (like *notepad.exe*) and copy the relevant tracing messages.

1. Open the *Web.config* file by using a text editor and change `switchValue` back to `Off` or the prior value.

1. Start the application pool if stopped.

    Error Handling: All service operations return an array or collection of errors within the `OperationResult` object of a response message. When an error occurs, the error array is also serialized to XML and written to the web application log file for certain `switchValue` settings, as described above.

An example of an API response error that has been written to the log file:

```XML
MDS Error: 0 :
<ArrayOfError xmlns="http://schemas.microsoft.com/sqlserver/masterdataservices/2009/09" xmlns:i="http://www.w3.org/2001/XMLSchema-instance"> 
     <Error> 
     <Code>110003</Code> 
          <Context> 
          <FullyQualifiedName>Model1</FullyQualifiedName> 
          <Identifier> 
               <Id>00000000-0000-0000-0000-000000000000</Id> 
               <Name>Model1</Name> <InternalId>0</InternalId> 
          </Identifier> 
          <Type>Model</Type> 
          </Context> 
     <Description>The name already exists. Type a different name.</Description> 
     </Error> 
</ArrayOfError> 
DateTime=2009-12-10T20:48:05.6949548Z error object contents 
```

As shown in the example above, each error includes the following data properties:

|Property|Description|
|-|-|
|Code|The unique error number identifying the type of error|
|Description|Localized error message text |
|Context.FullyQualifiedName |The fully qualified name of the object involved in the error. Some names are only unique within their context. An entity would be qualified with a Model name prefix such as ModelName : EntityName|
|Context.Type |The type of object involved in the error |
|Context.Identifier |The identifier of the object involved in the error |
|Context.Identifier.Id |The unique GUID of the object, if specified or available |
|Context.Identifier.Name |The name of the object, if specified or available |
|Context.Identifier.InternalId|Deprecated – don't use |

## Tracing MDS database creation issues

You can use the following procedure to create a log file that can help troubleshoot failures that may occur when creating an MDS database:

1. Open the *MDSConfigTool.exe.config* file in the path *C:\Program Files\Microsoft SQL Server\Master Data Services\Configuration* by using *notepad.exe*.

1. Uncomment the following line in the file by removing the <!-- prefix characters and --> suffix characters on that line:

    `<add name="LogFileListener" type="System.Diagnostics.TextWriterTraceListener" initializeData="MdsConfigManagerTrace.log" traceOutputOptions="DateTime"/>`

1. Make sure `switchValue` is set to `All`.

    `<source name="MDS" switchType="System.Diagnostics.SourceSwitch" switchValue="All">`

1. Try again to create the database. Then, open the file *MdsConfigManagerTrace.log* that is saved to *C:\Program Files\Microsoft SQL Server\Master Data Services\Configuration* and review the same for other information regarding the failure.

## External resources

[Troubleshooting logging improvement](https://techcommunity.microsoft.com/t5/sql-server-integration-services/troubleshooting-logging-improvement/ba-p/388214)

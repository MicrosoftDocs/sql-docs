---
title: "Configuration Setting Reference (PowerPivot for SharePoint) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 3b57dd3f-7820-4ba8-b233-01dc68908273
author: minewiskan
ms.author: owend
manager: craigg
---
# Configuration Setting Reference (PowerPivot for SharePoint)
  This topic provides reference documentation for configuration settings used by PowerPivot service applications in a SharePoint farm. If you are using PowerShell script to configure a server, or if you want to look up information for a specific setting, the information in this topic provides detailed descriptions.  
  
 Configuration settings are set for each PowerPivot service application. Within a farm, you can create multiple service applications as a way to configure independent logical instances of the same physical service instance. Configuration settings are stored in the PowerPivot application database created for each service application that you configure.  
  
 If you change configuration settings, the changes are picked up immediately and used for subsequent requests and connections. Operations that are in progress are governed by the settings that were in effect when the operation began.  
  
 Click the following links to read about specific configuration areas:  
  
 [Data Load Timeout](#LoadingData)  
  
 [Connection Pools](#ConnectionPool)  
  
 [Load Balancing](#AllocationScheme)  
  
 [Data Refresh](#DataRefresh)  
  
 [Usage Data Collection](#UsageData)  
  
 For instructions on how to create a PowerPivot service application, see [Create and Configure a PowerPivot Service Application in Central Administration](create-and-configure-power-pivot-service-application-in-ca.md).  
  
##  <a name="LoadingData"></a> Data Load Timeout  
 PowerPivot data is retrieved and loaded by Analysis Services server instances in the farm. Depending on how and when the data was last accessed, it will either be loaded from a content library or from a local file cache. Data is loaded into memory whenever a query or processing request is received. To maximize overall server availability, you can set a timeout value that instructs the server to stop a load data request if it cannot be completed within the allotted time.  
  
|Name|Default|Valid Values|Description|  
|----------|-------------|------------------|-----------------|  
|Data Load Timeout|1800 (in seconds)|1 to 3600|Specifies the amount of time a PowerPivot service application will wait for a response from a specific Analysis Services server instance.<br /><br /> By default, the service application will wait 30 minutes for a data payload from the Engine service instance to which it forwarded a specific request.<br /><br /> If the PowerPivot data source cannot be loaded within this period of time, the thread will be stopped and a new one will be started.|  
  
##  <a name="ConnectionPool"></a> Connection Pools  
 The PowerPivot service application creates and manages connection pools to enable connection reuse. There are two types of connection pools: one for data connections to read-only data, and a second for server connections.  
  
 Data connection pools contain cached connections for PowerPivot data source. Each connection pool is based on the context that was set when the database was loaded. This context includes the identity of the physical service instance, the database ID, and the identity of the SharePoint user who is requesting data. A separate connection pool is created for each combination. For example, requests from different users of the same database running on the same server will consume connections from different pools.  
  
 The purpose of a connection pool is to use cached connections for read-only requests for the same Analysis Services database by the same SharePoint user. The PowerPivot service instance is the server that has the data loaded in memory. The database ID is an internal identifier for the in-memory data structures of the data model (a model is instantiated as an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cube database). Version information is implicitly incorporated in the identifier.  
  
 Server connection pools contain cached connections from a PowerPivot service application instance to an Analysis Services server instance, where the service application is connecting with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Sysadmin permissions on the Analysis Services server. These connections are used to issue a load database request and monitor system health.  
  
 Each type of connection pool has upper limits that you can set to ensure best use of system memory for connection management.  
  
|Name|Default|Valid Values|Description|  
|----------|-------------|------------------|-----------------|  
|Connection Pool Timeout|1800 (in seconds)|1 to 3600.|This setting applies to data connection pools.<br /><br /> It specifies how much time an idle connection can remain in a connection pool before it is removed.<br /><br /> By default, the service application will remove a connection if it is inactive for more than five minutes.|  
|Maximum User Connection Pool Size|1000|-1, 0, or 1 to 10000.<br /><br /> -1 specifies an unlimited number of idle connections.<br /><br /> 0 means no idle connections are kept. New connections to a PowerPivot data source must be created each time.|This setting applies to the number of idle connections in all data connection pools created for a specific PowerPivot service application instance.<br /><br /> Individual connection pools are created for unique combinations of a SharePoint user, PowerPivot data, and service instance. If you have many users accessing a variety of PowerPivot data sources, server performance might benefit from an increase in connection pool size.<br /><br /> If there are more than 100 idle connections to a PowerPivot service instance, newly idle connections are disconnected rather than returned to the pool.|  
|Maximum Administrative Connection Pool Size|200|-1, 0, or 1 to 10000.<br /><br /> -1 specifies an unlimited number of idle connections.|The maximum number of idle server connections in all administrative connection pools created for PowerPivot service application connections to an Analysis Services server instance. Server connections are used for requests to load databases and to save changes back to the SharePoint database.|  
  
##  <a name="AllocationScheme"></a> Load Balancing  
 One of the functions that the PowerPivot service performs is to determine where Analysis Services data will be loaded among the available PowerPivot service instances. The `AllocationMethod` setting specifies the criteria against which a service instance is selected.  
  
|Name|Default|Valid Values|Description|  
|----------|-------------|------------------|-----------------|  
|Allocation Method|RoundRobin|Round Robin<br /><br /> Health Based|A scheme for allocating load requests among two or more Analysis Services server instances.<br /><br /> By default, the PowerPivot service will alternate requests based on server health. Health based allocates requests to the server that has the most system resources available based on available memory and CPU utilization.<br /><br /> Round robin rotates requests amongst the available servers in sequential order, regardless of current load or server health.|  
  
##  <a name="DataRefresh"></a> Data Refresh  
 Specify the range of hours that defines a normal or typical business day for your organization. These configuration settings determine when after-hours data processing occurs for data refresh operations. After-hours processing can begin at the end time of the business day. After-hours processing is a schedule option for document owners who want to refresh a PowerPivot data source with transactional data that was generated during normal business hours.  
  
|Name|Default|Valid values|Description|  
|----------|-------------|------------------|-----------------|  
|Start time|04:00 a.m.|1 to 12 hours, where the value is a valid integer within that range.<br /><br /> Type is Time.|Sets the lower limit of a business hour range.|  
|End time|08:00 p.m.|1 to 12 hours, where the value is a valid integer within that range.<br /><br /> Type is Time.|Sets the upper limit of a business hour range.|  
|PowerPivot Unattended Data Refresh Account|None|A target application ID|This account is used to run data refresh jobs on behalf of a schedule owner.<br /><br /> The unattended data refresh account must be defined in advance before it can be referenced in the service application configuration page. For more information, see [Configure the PowerPivot Unattended Data Refresh Account &#40;PowerPivot for SharePoint&#41;](../configure-unattended-data-refresh-account-powerpivot-sharepoint.md).|  
|Allow users to enter custom Windows credentials|Enabled|Boolean|Determines whether the scheduled data refresh configuration page shows an option that allows a schedule owner to specify Windows user account and password to run a data refresh job.<br /><br /> Secure Store Service must be enabled in order for this option to work. For more information, see [Configure Stored Credentials for PowerPivot Data Refresh &#40;PowerPivot for SharePoint&#41;](../configure-stored-credentials-data-refresh-powerpivot-sharepoint.md).|  
|Maximum Processing History Length|365|1 to 5000 days|Determines how long data refresh history is retained in the PowerPivot service application database. For more information, see [PowerPivot Usage Data Collection](power-pivot-usage-data-collection.md).|  
  
##  <a name="UsageData"></a> Usage Data Collection  
 Usage reports that appear in the PowerPivot Management Dashboard can provide important information about how PowerPivot-enabled workbooks are used. The following configuration settings control aspects of usage data collection for PowerPivot server events that are subsequently presented in usage or activity reports.  
  
|Name|Default|Valid values|Description|  
|----------|-------------|------------------|-----------------|  
|Query Reporting Interval|300 (in seconds)|1 to n seconds, where n is any valid integer.|To ensure that usage data collection does not consume too much of the data transfer capacity of the farm, query statistics are collected on each connection and reported as a single event. The Query Reporting Interval determines how often an event is reported. By default, query statistics are reported every 5 minutes.<br /><br /> Because connections are immediately closed as soon as a request is sent, the system generates a very large number of connections for even a single user accessing a single PowerPivot data source. For this reason, connection pools are created for each user and PowerPivot data source combination so that once a connection is created it can be reused by the same user for the same data. Periodically, at intervals specified through this configuration setting, the PowerPivot service application reports the usage data for each connection in the connection pool.<br /><br /> Increasing the time-to-report value will cause fewer events to be logged. However, if you set it too high, you risk losing event data if the server restarts or a connection is closed.<br /><br /> Lowering the value will cause more events to be logged with greater frequency, adding more PowerPivot-related usage data to the data collection system in the SharePoint usage database.<br /><br /> Generally, do not change this configuration setting unless you are trying to resolve a specific problem (for example, if the usage database is growing too quickly as a result of PowerPivot usage data).|  
|Usage Data History|365 (in days)|0, or 1 to n days, where n is any valid integer.<br /><br /> 0 means that history is always retained and never deleted.|By default, usage data is kept for one year in the PowerPivot service application database. Records that are older than one year are dropped from the database.<br /><br /> A check for expired historical data occurs daily, when the Microsoft SharePoint Foundation Usage Data Processing job runs. The timer job will read this setting and trigger a data deletion command for expired history in the PowerPivot service application database.|  
|Trivial Response Upper Limit|500 (in milliseconds)|1 to n milliseconds, where n is any valid integer.|By default, the threshold for trivial requests is half a second.<br /><br /> Trivial requests include server pings, requests for metadata, and starting sessions.|  
|Quick Response Upper Limit|1000 (in milliseconds)|1 to n milliseconds, where n is any valid integer.|By default, the threshold for quick requests is one second.<br /><br /> Quick requests are those that have an extremely small dataset, or requests for metadata that span large member sets.|  
|Expected Response Upper Limit|3000 (in milliseconds)|1 to n milliseconds, where n is any valid integer.|By default, the threshold for expected requests is three seconds.<br /><br /> This threshold sets the upper limit of an expected query time.|  
|Long Response Upper Limit|10000 (in milliseconds)|1 to n milliseconds, where n is any valid integer.|By default, the threshold for long requests is ten seconds.<br /><br /> These are requests that run longer than expected, but still fall within an acceptable range.|  
  
## See Also  
 [Create and Configure a PowerPivot Service Application in Central Administration](create-and-configure-power-pivot-service-application-in-ca.md)   
 [PowerPivot Data Refresh with SharePoint 2010](../powerpivot-data-refresh-with-sharepoint-2010.md)   
 [Configure Usage Data Collection for &#40;PowerPivot for SharePoint](configure-usage-data-collection-for-power-pivot-for-sharepoint.md)   
 [Configure PowerPivot Service Accounts](configure-power-pivot-service-accounts.md)   
 [PowerPivot Management Dashboard and Usage Data](power-pivot-management-dashboard-and-usage-data.md)  
  
  

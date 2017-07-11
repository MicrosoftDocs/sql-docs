---
title: "Set-PowerPivotServiceApplication cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 16d10e2d-d7e1-40f1-bc9d-a4e10c61af95
caps.latest.revision: 10
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Set-PowerPivotServiceApplication cmdlet
  
 [!INCLUDE[ssas-appliesto-sqlas-all](../../includes/ssas-appliesto-sqlas-all.md)] 

  Sets the properties of a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application.  

>[!NOTE] 
>This article may contain outdated information and examples.
  
 **Applies To:** SharePoint 2010 and SharePoint 2013.  
  
## Syntax  
  
```  
Set-PowerPivotServiceApplication [-Identity] <SPGeminiServiceApplicationPipeBind> [-AdministrationConnectionPoolSize <int>] [-AllowCustomWindowsCredentials] [-BusinessHoursEndTime <string>] [-BusinessHoursStartTime <string>] [-CachedDatabaseholdLimit <int>] [-Confirm <switch>] [-ConnectionPoolSize <int>] [-ConnectionPoolTimeout <int>] [-DataLoadTimeout <int>] [-DataRefreshFailureThreshold <int>] [-DataRefreshInactiveWorkbooksThreshold <int>] [-DataRefreshMaxHistory <int>] [-HealthBasedAllocation <switch>] [-LoadsToConnectionsRatioCollectionInterval <int>] [-LoadsToConnectionsRatioLimit <int>] [-MemoryDatabaseHoldLimit <int>] [-QueryReportingInterval <int>] [-RoundRobinAllocation <switch>] [-UnattendedAccount <string>] [-UsageDataRetentionPeriod <int>] [-UsageExpectedResponseUpperLimit <int>] [-UsageLongResponseUpperLimit <int>] [-UsageQuickResponseUpperLimit <int>] [-UsageTrivialResponseUpperLimit <int>] [-UsageUpdateDayLimit <int>] [<CommonParameters>]  
```  
  
## Description  
 The Set-PowerPivotServiceApplication cmdlet updates the properties of a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application in the farm. The Identity parameter is required. You must provide either the GUID of the service application for which you are updating properties.  
  
 To verify your changes, run the following cmdlet: Get-PowerPivotServiceApplication -Identity \<GUID> | format-list.  
  
## Parameters  
  
### -Identity \<SPGeminiServiceApplicationPipeBind>  
 Specifies the service application to update. The type must be a valid GUID or an instance of a valid [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application object. You can use Get-PowerPivotServiceApplication to return an instance of the object.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|true|  
|Accept wildcard characters?|false|  
  
### -AdministrationConnectionPoolSize \<int>  
 Specifies the number of open connections in a connection pool created for a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service connection to Analysis Services. Each [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service instance opens a separate administrative connection to the Analysis Services instance on the same computer. [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service creates a separate pool to reuse administrative connections for the purpose of checking for idle connections and monitoring server health. The default value is 200 connections. Valid values are -1 (unlimited), 0 (disables administrative connection pooling), or 1 to 10000. If you select 0, every connection will be created anew.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|200|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -AllowCustomWindowsCredentials [\<SwitchParameter>]  
 Specifies whether schedule owners can enter arbitrary Windows credentials to run a data refresh schedule. If you select this checkbox, [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application will create and manage a target application each set of stored credentials. Default is set to true. To turn off this feature, set AllowCustomWindowsCredentials:$false.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -BusinessHoursEndTime \<string>  
 Specifies the ending point in a range of hours that define a business day. Data refresh schedules can run after the close of a business day to pick up transactional data that was generated during normal business hours. Default is 8:00 p.m.  Valid values are specified in quotes, in a.m. or p.m. clock time (for example, "08:00PM". Hours must be between 1 and 12. Minutes must be between 1 and 59.  
  
 To specify the full range of hours for a business day, you must set both BusinessHoursStartTime and BusinessHoursEndTime. The two parameters define the interval of hours that make up a business day.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|8 PM|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -BusinessHoursStartTime \<string>  
 Specifies the starting point in a range of hours that define a business day. Data refresh schedules can run after the close of a business day to pick up transactional data that was generated during normal business hours. Default is 4:00 a.m.  Valid values are specified in quotes, in a.m. or p.m. clock time (for example, "04:00AM". Hours must be between 1 and 12. Minutes must be between 1 and 59.  
  
 To specify the full range of hours for a business day, you must set both BusinessHoursStartTime and BusinessHoursEndTime. The two parameters define the interval of hours that make up a business day.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|4 AM|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -CachedDatabaseholdLimit \<int>  
 Specifies how many hours an inactive database remains on the file system after it has been unloaded from memory. The default is 120 hours. The cleanup job uses this setting to determine which files to delete. All [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] databases that are inactive for 168 hours (48 hours in memory and 120 hours in the cache) are deleted from disk by the cleanup job.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|120|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Confirm \<switch>  
 Prompts you for confirmation before executing the command. This value is enabled by default. To bypass the confirmation response in a command, specify Confirm:$false on the command.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -ConnectionPoolSize \<int>  
 Specifies the maximum number of idle connections the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service will create in individual connection pools for each SharePoint user, [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] dataset, and version combinations. The default value is 1000 idle connections. Valid values are -1 (unlimited), 0 (disables user connection pooling), or 1 to 10000. These connection pools enable the service to more efficiently support ongoing connections to the same read-only data by the same user. If you disable connection pooling, every connection will be created anew. Note that changing the limit on connection pool size, including setting it to 0, will not result in dropped connections. Connection pools exist to reduce wait times when connecting to data. The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service will never deny a connection based on connection pool settings.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|1000|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -ConnectionPoolTimeout \<int>  
 Specifies how many minutes an idle data connection will remain open. The default value is 1800 seconds (or 30 minutes). During this period, the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application will reuse an idle data connection for read-only requests from the same SharePoint user for the same [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data. If no further requests are received for that data during the period specified, the connection is removed from the pool. Valid values are 1 to 3600 seconds.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|1800|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -DataLoadTimeout \<int>  
 Specifies how long the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application waits for a response from the SQL Server Analysis Services ([!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) instance to which it forwarded a load data request. Because very large datasets take time to move over the wire, you must allow sufficient time for the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service instance to retrieve the Excel workbook and move the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data to an Analysis Services instance for query processing. The default value is 1800 seconds (or 30 minutes). Valid values range from 1 to 3600 seconds.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|1800|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -DataRefreshFailureThreshold \<int>  
 Specifies the number of consecutive failures after which a schedule is disabled. The default is 10. You can also enter 0 to never disable a schedule due to refresh failures..  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|10|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -DataRefreshInactiveWorkbooksThreshold \<int>  
 Specify the number of data refresh cycles after which a schedule is disabled, or enter 0 to never disable a schedule due to inactivity. The default is 10 cycles.  
  
 Workbook inactivity is measured as the absence of connection events over multiple data refresh cycles. A data refresh cycle is counted each time a data refresh operation is triggered (either by the schedule or a Run Now operation), regardless of whether the operation succeeds or fails. If a number of cycles pass (10 by default) with no connection requests for the workbook, the data refresh schedule is disabled due to inactivity.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|10|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -DataRefreshMaxHistory \<int>  
 Specifies how long to retain a historical record of data refresh processing. This information appears in data refresh history pages that are kept for each workbook that uses data refresh. It also appears in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard. Default value is 365 days.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|365|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -HealthBasedAllocation \<switch>  
 Specifies the health-based allocation algorithm that forwards connection requests to the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint server that has the most CPU and memory resources available. This is the default allocation algorithm. HealthBasedAllocation and RoundRobinBasedAllocation are mutually exclusive. You must specify one or the other. If you set them both to false, HealthBasedAllocation will be used because it is the default. If you set them both to true, you will get a validation error. Syntax for these parameters include entering just the parameter name, or parameter:$true or parameter:$false.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -LoadsToConnectionsRatioCollectionInterval \<int>  
 Specify the interval (in hours) to count load and connection events for calculating the load-to-connection ratio. By default, the system will calculate a new ratio every 4 hours. Valid values are 1 to 24.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|4|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -LoadsToConnectionsRatioLimit \<int>  
 Specifies the ratio of load events over connection events, used as an indicator of server health. The default is 20 percent.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|20|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -MemoryDatabaseHoldLimit \<int>  
 Specifies how many hours an inactive database stays in memory to service new requests for that data. An active database is always kept in memory as long as you are querying it, but after it is no longer active, the system will keep the database in memory for an additional time period in case there are more requests for that data. The default is 48 hours.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|48|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -QueryReportingInterval \<int>  
 Specifies the number of seconds to gather query response statistics before reporting is as a usage event. The default is 300 seconds.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|300|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -RoundRobinAllocation \<switch>  
 Specifies the round robin allocation algorithm that forwards connection requests to the next [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint server, alternating requests equally among available servers, regardless of server load. HealthBasedAllocation and RoundRobinBasedAllocation are mutually exclusive. You must specify one or the other. If you set them both to false, HealthBasedAllocation will be used because it is the default. If you set them both to true, you will get a validation error. Syntax for these parameters include entering just the parameter name, or parameter:$true or parameter:$false.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -UnattendedAccount \<string>  
 Specifies the target application name of a Secure Store Service application that stores a predefined account for running [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data refresh jobs.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -UsageDataRetentionPeriod \<int>  
 Specifies the number of days to retain a history of usage data and server health statistics. The default is 365 days. Setting this value to 0 keeps all history indefinitely.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|365|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -UsageExpectedResponseUpperLimit \<int>  
 Sets an upper boundary that defines an expected request-response exchange. The default is 3000 milliseconds. Any request that completes between 1000 to 3000 milliseconds is considered an expected response for reporting purposes.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|3000|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -UsageLongResponseUpperLimit \<int>  
 Sets an upper boundary that defines a long running request-response exchange.  The upper limit is 10000 milliseconds. Any requests that exceed this upper limit fall into the Exceeded category, which has no upper threshold.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|10000|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -UsageQuickResponseUpperLimit \<int>  
 Sets an upper boundary that defines a quick request-response exchange. The default is 1000 milliseconds. Any request that completes between 500 to 1000 milliseconds is considered a quick response for reporting purposes.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|1000|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -UsageTrivialResponseUpperLimit \<int>  
 Specifies a category of response times that are too little to be considered relevant for data collection purposes. Most responses that fall into this category are server-to-server communications. By default, this value is 500 milliseconds. Any request that completes between 0 to 500 milliseconds is a trivial request, and ignored for reporting purposes.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|500|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -UsageUpdateDayLimit \<int>  
 Specify the threshold (in days) for triggering a warning about a failure to refresh the data file used by reports in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard. By default, the system updates usage data on a daily basis. The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard.xlsx file, which is the data source for administrative reports, is refreshed on the same schedule. If the .xlsx file is not updated over a number of days, a health rule is triggered indicating the file is out of date. The default is 5 days. Valid values are 1 through 30.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|5|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### \<CommonParameters>  
 This cmdlet supports the common parameters: Verbose, Debug, ErrorAction, ErrorVariable, WarningAction, WarningVariable,OutBuffer and OutVariable. For more information, see [About_CommonParameters](http://go.microsoft.com/fwlink/?linkID=227825).  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs|None.|  
|Outputs|None.|  
  
## Example 1  
  
```  
C:\PS>Set-PowerPivotServiceApplication -identity 1234567-890a-bcde-fghijklm -AllowCustomWindowsCredentials:$false -UnattendedAccount "MyTargetApp"  
```  
  
 This example turns off a data refresh feature that automatically creates and manages Secure Store Service target applications for storing arbitrary Windows credentials. It also sets the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] unattended data refresh account to a predefined target application.  
  
 Use Get-powerpivotserviceapplication to get a valid identity.  
  
## Example 2  
  
```  
C:\PS>Set-PowerPivotServiceApplication -identity 1234567-890a-bcde-fghijklm -HealthBasedAllocation  
```  
  
 This example specifies the health-based allocation algorithm that forwards connection requests to the server that has the most resources available.  
  
 Use Get-powerpivotserviceapplication to get a valid identity.  
  
## Example 3  
  
```  
C:\PS>Set-PowerPivotServiceApplication -identity 1234567-890a-bcde-fghijklmn -BusinessHoursStartTime "07:15AM" -BusinessHoursEndTime "08:00PM"  
```  
  
 This example shows how to set the starting and ending hours of a business day, used as a schedule option for scheduling [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data refresh. Schedules can specify an after business hours option for running data refresh at the end of a business day.  
  
 Use Get-powerpivotserviceapplication to get a valid identity.  
  
  
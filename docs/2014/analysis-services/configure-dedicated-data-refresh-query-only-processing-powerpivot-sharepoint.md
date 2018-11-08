---
title: "Configure Dedicated Data Refresh or Query-Only Processing (PowerPivot for SharePoint) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 5e027605-1086-4941-bb01-f315df8f829b
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure Dedicated Data Refresh or Query-Only Processing (PowerPivot for SharePoint)
  In SharePoint integrated mode, an Analysis Services server instance can be configured to support a specific type of processing request, such as data refresh or query-only processing. By default, both types of load requests are enabled. You can turn off either type to create a dedicated query engine or data refresh server.  
  
 **[!INCLUDE[applies](../includes/applies-md.md)]**  SharePoint 2010  
  
> [!NOTE]  
>  In this release, there are no configuration settings for limiting memory or CPU usage for either data refresh jobs or on-demand queries. An [!INCLUDE[ssGeminiSrv](../includes/ssgeminisrv-md.md)] instance will use all of the resources that are available to run the queries and data refresh jobs it is managing.  
  
 This topic contains the following sections:  
  
 [Configure a processing mode](#config)  
  
 [Change the number of data refresh jobs that can run in parallel](#change)  
  
##  <a name="config"></a> Configure a Processing Mode  
  
1.  In Central Administration, in System Settings, click **Manage services on server**.  
  
2.  At the top of the page, in Server, click the down arrow, and then click **Change Server**.  
  
3.  Select the SharePoint server that has the Analysis Services server instance that you want to configure.  
  
4.  Click **SQL Server Analysis Services**.  
  
5.  In Service Instance Usage, do one of the following:  
  
    1.  Clear the checkbox for **Enable loading of read-only databases** to turn off on-demand query processing that occurs whenever a user opens a workbook that contains PowerPivot data.  
  
    2.  Clear the checkbox for **Enable loading of databases for refresh** to turn off scheduled data refresh.  
  
    > [!NOTE]  
    >  Turning off data refresh does not remove data refresh options from SharePoint sites. Users who own PowerPivot workbooks can still create schedules for data refresh, but data refresh will not occur on that server.  
  
6.  Optionally, for data refresh operations, you can change the number of concurrent refresh jobs. Increasing the number of concurrent jobs is recommended if the server is configured for data refresh only, or if there are additional processors on the server. You might decrease the number of concurrent jobs if you want to free up system resources for more on-demand queries.  
  
7.  Save your changes. The server will not validate your entries until a processing event occurs. If you enter an invalid number for concurrent jobs, the error will be detected and logged when the next request is processed.  
  
##  <a name="change"></a> Change the number of data refresh jobs that can run in parallel  
 A data refresh job is a scheduled task that is added to a processing queue that is maintained and monitored by a PowerPivot service application. A job consists of schedule information for one or more data sources in a PowerPivot workbook. A separate job is created for each schedule that is defined. If a workbook owner defines one schedule for all data sources, only one job will be created for the entire data refresh operation. If a workbook owner creates individual schedules for external data sources, multiple jobs will be created and run to complete a full data refresh for that workbook.  
  
 You can increase the number of data refresh jobs that can run at the same time if your system has the capacity to support the extra load.  
  
|Setting|Valid values|Description|  
|-------------|------------------|-----------------|  
|Default value|Calculated based on RAM.|The default value is based on the amount of available memory divided by 4 gigabytes. The default is calculated by a formula so that the settings can be adjusted depending on the capabilities of the system.<br /><br /> Note: The 4 gigabyte divisor was selected based on RAM usage for a large sampling of actual PowerPivot data sources. It is not based on PowerPivot physical or logical architecture.|  
|Maximum value|Calculated based on number of CPUs.|The maximum number of concurrent jobs that you can specify is based on the number of processors on the computer. For example, on a 4-socket quad-core computer, the maximum number of jobs that you can run concurrently is 16.|  
  
#### Increasing the default value to a higher value  
 The following chart shows different combinations of RAM and CPU, and the resulting default and maximum values that are calculated based on system characteristics. Recall that the calculated default value for the number of data refresh jobs that can run concurrently is based on system memory, while the calculated maximum value is based on CPUs. The last column indicates whether you can increase the maximum number of concurrent data refresh jobs.  
  
|Actual RAM (in gigabytes)|Calculated Default value|Actual Number CPUs|Calculated Maximum value|Increase concurrent jobs?|  
|---------------------------------|------------------------------|------------------------|------------------------------|-------------------------------|  
|4|1|1|1|No. Default and maximum are the same.|  
|4|1|4|4|Yes. You can increase the number of concurrent jobs to 2, 3, or 4.|  
|8|2|4|4|Yes. You can increase the number of concurrent jobs to 3 or 4.|  
|16|4|4|4|No. Default and maximum are the same.|  
|32|Using the formula that calculates the default value, the default would be 8. Because the default is higher than the maximum allowed, the calculated default is not used in this case.|4|4|No. Although the large RAM would indicate a default of 8 concurrent jobs, a computer that has 4 processors will only support a maximum of 4 concurrent jobs.|  
|32|8|8|8|No.|  
|32|8|16|16|Yes.|  
|64|16|16|16|No.|  
  
 Because there is no way to know in advance whether multiple jobs can run successfully at the same time, you should only increase the number of concurrent jobs after you have analyzed memory consumption over time and determined that server memory is generally underutilized.  
  
 Each data refresh job will have different load characteristics depending on the number and size of the data sources that are being refreshed. Workbooks that have a single data source with a smaller number of rows have a far lighter processing load than a workbook that has numerous data sources and very large row sets.  
  
## See Also  
 [PowerPivot Data Refresh with SharePoint 2010](powerpivot-data-refresh-with-sharepoint-2010.md)  
  
  

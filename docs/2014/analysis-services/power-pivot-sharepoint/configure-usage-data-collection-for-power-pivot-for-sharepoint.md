---
title: "Configure Usage Data Collection for (PowerPivot for SharePoint | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 955ca6d6-9d5b-47a4-a87c-59bd23f1bf74
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure Usage Data Collection for (PowerPivot for SharePoint
  Usage data collection is a farm-level SharePoint feature. PowerPivot for SharePoint uses and extends this system to provide reports in the PowerPivot Management Dashboard that show how PowerPivot data and services are used. Depending on how you install SharePoint, usage data collection might be turned off for the farm. A farm administrator must enable usage logging to create the usage data that appears in the PowerPivot Management Dashboard.  
  
 For information on usage data in the PowerPivot Management Dashboard, see [PowerPivot Management Dashboard and Usage Data](power-pivot-management-dashboard-and-usage-data.md).  
  
 **In this topic:**  
  
 [Enable usage data collection and choose events that trigger data collection](#events)  
  
 [Set log file location](#configdb)  
  
 [Configure the timer jobs used in usage data collection](#jobs)  
  
 [Limit how long usage data history is stored](#confighist)  
  
 [Define fast, medium and slow query response categories for reporting purposes](#qrh)  
  
 [Specify how often query statistics are reported to the usage data collection system](#ttr)  
  
 [Open the PowerPivot Service Application page to access configuration settings](#openconfig)  
  
 [The default configuration for PowerPivot usage data collection](#defaultconfig)  
  
> [!IMPORTANT]  
>  Usage data provides insight into how users are accessing data and resources, but it does not guarantee reliable, persistent data about server operations and user access. For example, if there is a server restart, event usage data will be lost and will not be recoverable. Similarly, if the temporary log files reach maximum size, no new data will be added until the files are cleared. If you require audit capability, consider using the workflow and content type features that SharePoint provides to build out an auditing subsystem for your farm. For more information, look for product and community documentation on the web.  
  
##  <a name="events"></a> Enable usage data collection and choose events that trigger data collection  
 Configure usage data collection in SharePoint Central Administration.  
  
1.  In Central Administration, click **Monitoring**.  
  
2.  In the **Reporting section**, click **Configure usage and health data collection.**  
  
3.  Select the **Enable usage data collection**.  
  
4.  In the **Events to log** section, select or clear the checkboxes to either enable or disable the following Analysis Services events:  
  
    |Event|Description|  
    |-----------|-----------------|  
    |**PowerPivot Connections**|PowerPivot Connection event is used to monitor PowerPivot server connections that are made on behalf of a user.|  
    |**PowerPivot Load Data Usage**|PowerPivot Load Data Usage is used to monitor requests that load PowerPivot data into server memory. A load event is generated for PowerPivot data files loaded from a content database or from cache.|  
    |**PowerPivot Unload Data Usage**|PowerPivot Unload Data Usage is used to monitor requests for unloading a PowerPivot data source after a period of inactivity. Caching a PowerPivot data source to disk will be reported as an unload event.|  
    |**PowerPivot Query Usage**|PowerPivot Query Usage is used to monitor query processing times for data that is loaded in an [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] instance.|  
  
    > [!NOTE]  
    >  Server health and data refresh operations also generate usage data, but there is no event associated with these processes.  
  
5.  You can also update the log file location. For more information, see the next section.  
  
6.  Click **OK** to save your changes.  
  
7.  Optionally, you can specify whether all messages or just errors are logged. For more information on how to throttle event messages, see [Configure and View SharePoint Log Files  and Diagnostic Logging &#40;PowerPivot for SharePoint&#41;](configure-and-view-sharepoint-and-diagnostic-logging.md).  
  
##  <a name="configdb"></a> Set log file location  
 PowerPivot usage data is initially stored in usage log files on the local server, and then moved at regular intervals to the PowerPivot service application databases. The log file location is set in Central Administration. The default location is:  
  
 `C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\15\logs`  
  
 To view or change these properties, use the **Usage and health data collection** page.  
  
1.  On the Home page in Central Administration, click **Monitoring**.  
  
2.  In the Monitoring section, click **Configure usage and health data collection.**  
  
3.  In Usage Data Collection Settings, view or modify the file location, name, or maximum file size. If you specify a file size that is too low, the file size will reach the maximum limit and no new entries will be added to it until its contents are moved to the central usage data collection database.  
  
##  <a name="jobs"></a> Configure the timer jobs used in usage data collection  
 PowerPivot server health and usage data is moved to different locations in the usage data collection system through two timer jobs.:  
  
-   The "Microsoft SharePoint Foundation Usage Data Import" timer job moves PowerPivot usage to the PowerPivot service application database.  
  
-   The "PowerPivot Management Dashboard Processing timer job" the data to PowerPivot workbook that is the source of data for built-in administrative reports.  
  
 If you need to refresh the administrative reports that appear in the PowerPivot Management Dashboard more frequently, follow these steps.  
  
1.  In Central Administration, click **Monitoring**.  
  
2.  Click **Review job definitions.** In the **Timer jobs** section.  
  
3.  Click **Microsoft SharePoint Foundation Usage Data Import**.  
  
4.  Click **Run Now**. If the **Run Now** button is disabled, click **Enable** and then click **Run Now**.  
  
5.  In the Job Definitions list, click **PowerPivot Data Management Dashboard Processing Timer Job**.  
  
6.  Click **Run Now**.  
  
7.  Check the reports to view the refresh data. For more information, see [PowerPivot Management Dashboard and Usage Data](power-pivot-management-dashboard-and-usage-data.md).  
  
##  <a name="confighist"></a> Limit how long usage data history is stored  
 Usage data history is stored for events (connections, load, unload, and on-demand query processing) and data refresh (scheduled data processing). Although usage data is collected through the SharePoint usage data collection system, the reporting data is moved to a PowerPivot application database and a reporting database for longer term storage. The usage data history setting controls how long usage data is retained in the PowerPivot application databases. The same limit is applied equally to all types of stored usage data in the same PowerPivot service application database.  
  
1.  [Open the PowerPivot Service Application Page](#openconfig).  
  
2.  In the **Usage Data Collection** section, in **Usage Data History**, enter the number of days for which you want to keep a record of data refresh activity for each workbook.  
  
    -   The default is 365 days.  
  
    -   0 specifies unlimited storage where usage data is kept indefinitely.  
  
    -   Alternatively, you can also specify a range between 1 and 5000.  
  
     Decreasing the retention period to a smaller number of days will delete any data that exceeds the new limit. For example, changing the value from 365 to 30 will result in usage data deletion for all historical information that occurred more than 30 days ago. Only data from the last 30 days is retained.  
  
     Data is actually deleted when the next event occurs. The limit on usage data history is checked only when the system processes an event.  
  
3.  Click **OK**.  
  
 For more information about how usage data is collected and stored, see [PowerPivot Usage Data Collection](power-pivot-usage-data-collection.md).  
  
##  <a name="qrh"></a> Define fast, medium and slow query response categories for reporting purposes  
 Query processing performance is measured against predefined categories that define a request-response cycle by how long it takes to complete. Predefined categories include: Trivial, Quick, Expected, Long-running, and Exceeded. Every request to a PowerPivot server will fall into one of the categories based on time to completion.  
  
 Query response information is used in activity reports. Within the reports, each category is used differently to better reveal the performance trends of the PowerPivot system. For example, trivial requests are excluded completely because doing so removes noise in the data and shows more meaningful trends using the remaining categories. In contrast, Long-running or Exceeded request statistics are prominent in the report so that administrators or workbook owners can take corrective action immediately.  
  
 Although you cannot add or delete categories, you can define the upper and lower limits that determine where one category stops and the next one begins. If your organization uses Service Level Agreements (SLA) to define acceptable levels of server availability and performance, you can tune these categories to reflect the SLA you create.  
  
1.  [Open the PowerPivot Service Application Page](#openconfig).  
  
2.  In the **Usage Data Collection** section, in **Trivial Response upper limit** , enter a value (in milliseconds) that sets the upper boundary for completing a trivial response. Requests that fall into this category typically include server pings, session initiation, and metadata query. The default is 500 milliseconds (or half a second).  
  
3.  In Quick Requests Upper Limit, enter a value (in milliseconds) that sets the upper boundary for completing a quick response. Requests that fall into this category include queries of very small datasets or metadata servers of large datasets. The default is 1000 milliseconds (or 1 second).  
  
4.  In **Expected Response Upper Limit**, enter a value (in milliseconds) that sets the upper boundary for completing a response in an expected or average time frame. Requests that fall into this category include loading data into a viewer. The default is 3000 milliseconds (or 3 seconds).  
  
5.  In **Long Response Upper Limit**, enter a value (in milliseconds) that sets the upper boundary for completing long running response. Requests that fall into this category run longer than expected, but within range that is still acceptable. The default is 10000 milliseconds (or 10 seconds).  
  
     Any requests that exceed this limit are categorized as *Exceeded*. There is no configurable threshold for *Exceeded*. It is inferred from the upper limit you specify on Long Requests Upper Limit. Requests that fall into the Exceeded category run longer than is allowed by an SLA you have defined.  
  
6.  Click **OK**.  
  
##  <a name="ttr"></a> Specify how often query statistics are reported to the usage data collection system  
 The time-to-report interval specifies how often query statistics are reported to the usage data collection system. Query statistics accumulate in a process and are reported as a single event at regular intervals. You can adjust the interval to write to the log file more or less often.  
  
1.  [Open the PowerPivot Service Application Page](#openconfig).  
  
2.  In the **Usage Data Collection** section, in **Query Reporting Interval**, enter the number of seconds after which the server will report the query statistics for all categories (trivial, quick, expected, long running, and exceed) as a single event to the usage data collection system.  
  
    -   The range is 1 to any positive integer.  
  
    -   The default is 300 seconds (or 5 minutes). This value is recommended for dynamic farm environments that run a variety of applications and services.  
  
     If you raise this value to a much larger number, you might lose statistical data before it can be reported. For example, a service restart will cause query statistics to be lost. Conversely, if your built-in activity reports show insufficient data, consider decreasing the interval to get time-to-report events more frequently.  
  
3.  Click **OK**.  
  
##  <a name="openconfig"></a> Open the PowerPivot Service Application page to access configuration settings  
 You must be a farm or service administrator to modify service application settings. If you defined multiple PowerPivot service applications in the farm, you must modify each one individually.  
  
1.  In SharePoint Central Administration, in **Application Management**, click **Manage service applications**.  
  
2.  Find the PowerPivot Service application. You can identify a service application by its type. A PowerPivot service application type is **PowerPivot Service Application**.  
  
3.  Click the PowerPivot service application name. The PowerPivot Management Dashboard opens.  
  
4.  In **Actions**, click **Configure service application settings**. The PowerPivot Service Application Settings page will open.  
  
##  <a name="defaultconfig"></a> The default configuration for PowerPivot usage data collection  
 Usage data collection for PowerPivot service operations can be enabled with default settings to make it immediately available in applications that support the Analysis Services integration feature. The default settings include events that trigger usage data collection, limits on how long usage data is stored, and thresholds for categorizing query response times.  
  
 The following table shows the default values for usage data collection configuration.  
  
|Setting|Default Value|Type|Valid range|  
|-------------|-------------------|----------|-----------------|  
|**Analysis Services usage events** (Connection, Load, Unload, Requests)|\<enabled>|Boolean|These values are either enabled or disabled.|  
|**Query Reporting interval**|300 (in seconds)|Integer|1 to any positive integer. The default is 5 minutes.|  
|**Usage data history**|365 (in days)|Integer|0 specifies unlimited, but you can also set an upper limit to expire historical data and have it deleted automatically. Valid values for a limited retention period are 1 to 5000 (in days).|  
|Trivial Response Upper Limit|500 (in milliseconds)|Integer|Sets an upper boundary that defines a trivial request-response exchange. Any request that completes between 0 to 500 milliseconds is a trivial request, and ignored for reporting purposes.|  
|Quick Response Upper Limit|1000 (in milliseconds)|Integer|Sets an upper boundary that defines a quick request-response exchange.|  
|Expected Response Upper Limit|3000 (in milliseconds)|Integer|Sets an upper boundary that defines an expected request-response exchange.|  
|Long Running Response Upper Limit|10000 (in milliseconds)|Integer|Sets an upper boundary that defines a long running request-response exchange. Any requests that exceed this upper limit fall into the Exceeded category, which has no upper threshold.|  
  
## See Also  
 [Configuration Setting Reference &#40;PowerPivot for SharePoint&#41;](configuration-setting-reference-power-pivot-for-sharepoint.md)   
 [PowerPivot Usage Data Collection](power-pivot-usage-data-collection.md)  
  
  

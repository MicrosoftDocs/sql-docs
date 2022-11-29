---
title: "System Data Collection Set Reports | Microsoft Docs"
description: The data collector provides a report for each of the System Data collection sets for monitoring system capacity and system performance in SQL Server.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "data collector [SQL Server], server activity"
  - "server activity [SQL Server]"
  - "reports [SQL Server], data collection"
  - "reports [SQL Server], disk usage"
  - "collection sets [SQL Server], reports"
  - "data collector [SQL Server], reports"
  - "reports [SQL Server], query statistics"
  - "query statistics reports [SQL Server]"
  - "disk usage reports [SQL Server]"
ms.assetid: 0b126b8d-4fe7-443d-8a9a-c266350181e5
author: MashaMSFT
ms.author: mathoma
---
# System Data Collection Set Reports
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The data collector provides an historical report for each of the System Data collection sets. Each of the following reports uses data that is stored in the management data warehouse:  
  
-   [Disk Usage Summary](#Disk)  
  
-   [Query Statistics History](#Query)  
  
-   [Server Activity History](#Server)  
  
 You can use these reports to obtain information for monitoring system capacity and troubleshooting system performance.  
  
##  <a name="Disk"></a> Disk Usage Summary Report  
 The Disk Usage Summary report contains data about disk space usage for all the databases in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The data provided in the reports is obtained by using the Disk Usage collection set, which uses the Generic T-SQL Query collector type.  
  
 You can access the Disk Usage Summary report from Object Explorer. To view the report, expand the **Management** folder, right-click **Data Collection**, point to **Reports**, point to **Management Data Warehouse**, and then click **Disk Usage Summary**. For more information, see [View a Collection Set Report &#40;SQL Server Management Studio&#41;](../../relational-databases/data-collection/view-a-collection-set-report-sql-server-management-studio.md).  
  
### Disk Usage Collection Set Report  
 The Disk Usage Collection Set report provides an overview of the disk space used for all databases in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and growth trends for the data and log files for each of these databases.  
  
-   The summary table displays the start size (in megabytes) and the current size of all the databases installed on the server that the data collector is monitoring.  
  
-   Trend and average growth information is shown graphically and numerically for both data and log files.  
  
#### Disk Usage Collection Set - Database: <database_name> Subreport  
 The Disk Usage Collection Set - Database: *<database_name>* subreport is displayed when you click a trend line for a specific database or log file in the summary table of the Disk Usage Collection Set report. This report provides a graphical representation of growth trends in disk space usage over the period of the report. Disk usage is organized and reported by used space, data space, unallocated space, and index space for data files, and used space and unused space for log files.  
  
 The table below the graph lists data collection times and corresponding usage data.  
  
#### Disk Usage for Database: <database_name> Subreport  
 The **Disk usage for database:**_<database_name>_ subreport is displayed when you click a database name in the summary table of the Disk Usage Collection Set report. This report provides a numerical and graphical breakdown of space usage by the data and transaction log files of the database. Space usage for data files is categorized as a percentage allocated to index pages, unallocated space, data pages, and unused space. These categories are defined as follows:  
  
|Category|Definition|  
|--------------|----------------|  
|Index|The amount of disk space used to hold index pages.|  
|Unallocated|The amount of disk space available to the database but not yet allocated to any object.|  
|Data|The amount of disk space used by data pages.|  
|Unused|The amount of disk space allocated to one or more objects, but not yet used.|  
  
 Space usage for the transaction log file is categorized as used space and unused space.  
  
 Autogrow and autoshrink events are reported for both the data and log files if these events have occurred in the database. The report displays the start time and duration of each event and the resulting change in file size.  
  
 The disk space used by each data file in the database is reported. The space reported as Space Reserved is the amount of used space plus the space allocated to the file but not yet used. The space reported by Space Used is the actual space currently used by the file, excluding allocated space.  
  
##  <a name="Query"></a> Query Statistics History Report  
 The Query Statistics History report contains query execution statistics. Data used in this report is obtained by using the Query Statistics collection set, which uses the Query Activity Collector Type.  
  
 You can access the Query Statistics History report from Object Explorer. To view the report, expand the **Management** folder, right-click **Data Collection**, point to **Reports**, point to **Management Data Warehouse**, and then click **Query Statistics History**. For more information, see [View a Collection Set Report &#40;SQL Server Management Studio&#41;](../../relational-databases/data-collection/view-a-collection-set-report-sql-server-management-studio.md).  
  
### Selecting Data to Include in the Report  
 The report includes query execution statistics for the entire data collection period. There are two ways you can navigate through the data collection timeline to select a segment of the data to view.  
  
 **Timeline Control and Navigation Buttons**  
  
 Use the timeline control and navigation buttons to move through the timeline or to select a date range. The arrow buttons provide left and right scrolling so you can move backward or forward through the timeline. By default, the arrows move through the timeline in 4-hour increments. Using the magnifier buttons, you can expand or contract this time increment to one of the following values: 15 minutes, 1 hour, 4 hours, 12 hours, or 24 hours. The currently selected time range is indicated by the highlighted portion of the timeline and is displayed in the text below the timeline. These values, as well as the data in the report, are updated whenever you click the timeline or use the navigation buttons.  
  
 **Calendar Button**  
  
 Use the calendar button to specify the start date, start time, and duration of the data that you want to report on.  
  
#### Query Statistics History Report  
 The Top Queries by Total CPU graph shows the relative expense of each query for the selected time range based on total CPU usage. To display a different view of relative query expense, click one of the hyperlinks provided below the graph: **Duration**, **Total I/O**, **Physical Reads**, or **Logical Writes**.  
  
 The table below the graph provides additional query data. It lists the text for each query that is graphed along with detailed statistical information. Note that the graph bars are active links, as are each of the queries shown in the table. Clicking an active link opens the Query Details subreport for the query.  
  
#### Query Details Subreport  
 The Query Details subreport provides the entire query text. There is an **Edit Query Text** hyperlink adjacent to the query. You can click this link to open the query in Query Editor. The table below the query provides query execution statistics, such as the average duration per query execution.  
  
 A graph of query plans and the average duration per execution is displayed. To display a different view of relative query plan cost, click any of the hyperlinks that are displayed below the graph: **Duration**, **Physical Reads**, or **Logical Writes**. The graph line is active and you can click any point to open the Query Plan Details subreport.  
  
 The table below the graph shows the top 10 query plans, based on CPU use per execution. Each number in the **Plan #** column is an active link that you can click to open the Query Plan Details subreport.  
  
#### Query Plan Details Subreport  
 This report displays the information for a query plan. In addition to enabling you to edit the query and view execution statistics, the report provides detailed information about the query plan. The **View graphical query execution plan** hyperlink opens a graphical representation of the execution plan for the current query.  
  
## Server Activity History Report  
 The Server Activity History report contain resource consumption and server activity data for the server and for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The data provided in this report is collected by the Server Activity collection set, which uses the Generic T-SQL Query collector type and the Performance Counters collector type.  
  
 You can access the Server Activity History report from Object Explorer. To view the report, expand the **Management** folder, right-click **Data Collection**, point to **Reports**, point to **Management Data Warehouse**, and then click **Server Activity History**. For more information, see [View a Collection Set Report &#40;SQL Server Management Studio&#41;](../../relational-databases/data-collection/view-a-collection-set-report-sql-server-management-studio.md).  
  
### Selecting Data to Include in the Report  
 The report includes server activity for the entire data collection period. There are two ways you can navigate through the data collection timeline to select a segment of the data to view.  
  
 **Timeline Control and Navigation Buttons**  
  
 Use the timeline control and navigation buttons to move through the timeline or to select a date range. The arrow buttons provide left and right scrolling so you can move backward or forward through the timeline. By default, the arrows move through the timeline in 4-hour increments. Using the magnifier buttons, you can expand or contract this time increment to one of the following values: 15 minutes, 1 hour, 4 hours, 12 hours, or 24 hours. The currently selected time range is indicated by the highlighted portion of the timeline and is displayed in the text below the timeline. These values as well as the data in the report are updated whenever you click the timeline or use the navigation buttons.  
  
 **Calendar Button**  
  
 Use the calendar button to specify the start date, start time, and duration of the data that you want to report on.  
  
###  <a name="Server"></a> Server Activity History Report  
 The Server Activity History report shows the initial view of the server activity for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the host operating system.  
  
 The following table describes the graphs that plot [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and system activity in the report and the detailed subreports that can be accessed through the graphs.  
  
|Graph|Report description|  
|-----------|------------------------|  
|%CPU|These subreports are accessed by clicking any point on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or System graph lines in the %CPU graph.<br /><br /> **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** : The Query Statistics History report provides a graph of the most expensive queries in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A table below the graph lists the queries and includes statistical data for each query. You can click on a query to obtain additional details.<br /><br /> **System**: The System CPU Usage report provides a graph of %CPU time per processor and statistical data for each process in tabular format.|  
|Memory Usage|These subreports are accessed by clicking any point on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or System graph lines in the Memory Usage graph.<br /><br /> **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** : The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Memory Usage report provides graphs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process memory usage, memory counters, internal memory consumption by type, and a table that provides data about the average memory usage by component type.<br /><br /> **System**: The System Memory Usage report provides graphs for memory usage, cache and page hit ratios, and a table that provides information about the working set and private bytes for each process.|  
|Disk I/O Usage|These subreports are accessed by clicking any point on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or System graph lines in the Disk I/O Usage graph.<br /><br /> **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** : The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Disk I/O Usage report provides graphs for disk response time and disk transfer rate. An additional table provides virtual file statistics by disk, database, and file.<br /><br /> **System**: The System Disk Usage report provides graphs for disk response time, average disk queue length, disk transfer rate, and a table that provides information about I/O writes and reads for each process.|  
|Network Usage|No additional reports are available.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Waits|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Waits graph displays waits encountered by threads that executed by wait category. You can access a detailed report by clicking any segment in the graph. In addition to providing graphical [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] wait statistics over a narrower time frame, this report provides information about wait categories in tabular format. For each category, such as CPU and its subcategories, the table shows the number of waits, the wait time, and percentage of total wait time.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Activity|Different aspects of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] activity can be accessed from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Activity graph. The reports that you can obtain by clicking a point on the SQL Compilations/sec graph line are as follows:<br /><br /> <br /><br /> Connections and sessions<br /><br /> Requests<br /><br /> Plan cache hit ratio<br /><br /> tempdb characteristics|  
  
## See Also  
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [View a Collection Set Report &#40;SQL Server Management Studio&#41;](../../relational-databases/data-collection/view-a-collection-set-report-sql-server-management-studio.md)  
  
  

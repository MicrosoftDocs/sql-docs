---
title: "Managed Instance Details (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
ms.assetid: 6e51b7bb-a733-4852-8c33-7f4dbdf931c2
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Managed Instance Details (SQL Server Utility)

  Information in the Managed Instances view of Utility Explorer provides utilization data for individual instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], CPU utilization history, storage utilization details at the file level, and the ability to view and update policy thresholds. Policy thresholds can be controlled at the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance level, for a computer, for database files and log files, and at the level of storage volumes. You can also view property details for individual managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
## UIElement List 
 List view  
 The list view in the top pane displays data about individual instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] listed in rows by ComputerName\InstanceName.  
  
 Health state icons provide summary status for each instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by utilization category:  
  
-   Green check - ![](../../2014/database-engine/media/well-utilized.gif "Well_utilized") - Number of managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] which are not violating resource utilization policies. Resources are well-utilized.  
  
-   Green down arrow - ![](../../2014/database-engine/media/utility-down-arrow.gif "Utility_down_arrow") - Resources are underutilized.  
  
-   Red up arrow - ![](../../2014/database-engine/media/utility-up-arrow.gif "Utility_up_arrow") - Resources are overutilized.  
  
 The sequence of columns in the list view can be changed by dragging them to the left or the right. Columns in the list view can be added or deleted by right-clicking on the column headings and selecting or unselecting columns. The right-click menu also provides sort options. Sorting can also be activated by clicking at the top of a column name.  
  
 To access filter options for the Utility list view, right-click on the **Managed Instances** node in the Utility Explorer navigation pane, and select **Filter**. After filter settings have been implemented, the **Managed Instances** node in Utility Explorer will be labeled **Managed Instances (filtered)**. For more information, see [Filter Settings &#40;Object Explorer and Utility Explorer&#41;](../ssms/object/filter-settings-object-explorer-and-utility-explorer.md).  
  
 By default, the following columns display health state information about each managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   Instance CPU - Displays the health state of processor utilization allocated to this instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The health state for this parameter is determined according to CPU utilization policy set for the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and the configuration setting for volatile resource evaluation policy. For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
     To view the processor utilization history for this instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], or to view or change the policy limits, click on the **CPU Utilization** tab.  
  
-   Computer CPU - Displays the health state of computer processor utilization. The health state for this parameter is determined according to CPU utilization policy set for the computer and the configuration setting for volatile resource evaluation policy. For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
     To view the processor utilization history for this instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], or to view or change the policy limits, click on the **CPU Utilization** tab.  
  
-   File Space - Displays a summary of health states of file space utilization for all databases that belong to the selected instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. If the health state of any database is overutilized, then the file space health state will be reported in the list view as overutilized. If the health state of any database is underutilized, and no database is overutilized, then the file space health state will be reported in the list view as underutilized. Otherwise, the file space health state will be reported in the list view as well-utilized. To view or change the policy limits, click on the **Storage Utilization** tab.  
  
-   Volume Space - Displays a summary of the health states of volume space utilization for all volumes that contain databases belonging to the selected instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. If the health state of any volume is overutilized, then the volume space health state will be reported in the list view as overutilized. If the health state of any volume is underutilized, and no volume is overutilized, then the volume space health state will be reported in the list view as underutilized. Otherwise, the volume space health state will be reported in the list view as well-utilized. To view or change the policy limits, click on the **Storage Utilization** tab.  
  
-   Policy Type - Indicates whether "Global" default policies or "Override" custom policies are in effect for the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 Other columns that can be displayed using the right-click menu in the column heading area of the list view:  
  
-   Processor Name:  
  
-   Processor Speed (MHz):  
  
-   Processor Count:  
  
-   Physical Memory (MB):  
  
-   Operating System Version:  
  
-   SQL Server Version:  
  
-   SQL Server Edition:  
  
-   Clustered: (True or False)  
  
-   Backup Directory:  
  
-   Collation:  
  
-   Case Sensitive: (True or False)  
  
-   Language:  
  
-   Last Reported Time: This column shows the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=164071) topic in SQL Server Books Online. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=141713) topic in SQL Server Books Online.  
  
 CPU Utilization tab  
 The CPU utilization tab shows side-by-side graphs of historical data for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance and computer CPU utilization.  
  
 The graph displays the processor utilization history for the interval specified in the radio buttons on the right side of the display area. To change the display interval and refresh the data set, select a radio button from the list.  
  
 Interval options are as follows:  
  
-   1 Day, displayed in 15-minute intervals.  
  
-   1 Week, displayed in 1-day intervals.  
  
-   1 Month, displayed in 1-week intervals.  
  
-   1 Year, displayed in 1-month intervals.  
  
 Storage Utilization tab  
 The Storage Utilization tab has a tree view that displays storage utilization details. Note that time data show the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=164071) topic in SQL Server Books Online. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=141713) topic in SQL Server Books Online.  
  
 Display can be grouped by database or by volume. To use the database tree view, select the **Database** radio button in the **Group files by:** selection. To view storage utilization status for individual database files, click on the plus sign next to a database name in the tree view. The database files listed include all system and user databases that belong to the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] you selected in the list view.  
  
 The tree structure corresponds to the storage hierarchy. The root node in the tree view is the database. The next level of the tree view contains a filegroup node or nodes that belong to the database, and a log file node for the logs that belong to the database. The next level contains the data files that belong to the filegroup.  
  
 Data displayed in the graph of storage utilization history changes depending on the node selected in the tree view:  
  
-   Select the file group node to display a graph of file space used by all data files that belong to the selected file group.  
  
-   Select the log file node to display a graph of file space used by all log files that belong to the selected database.  
  
-   Select the database node to display a graph that adds file space used by all data files and all log files that belong to the selected database.  
  
 Health state icons provide at-a-glance status for database files, filegroups, and log files:  
  
 For databases:  
  
-   Green check - Health states of all filegroups and log files a neither overutilized or underutilized.  
  
-   Green down arrow - Health states of at least one filegroup or log file is underutilized, and no health states are overutilized.  
  
-   Red up arrow - The health state of at least one filegroup or log file is overutilized.  
  
 For filegroups and log files:  
  
-   Green check - File space utilization for all files in the filegroup are neither overutilized or underutilized.  
  
-   Green down arrow - File space utilization for at least one file in the filegroup is underutilized, and no files in the filegroup are overutilized.  
  
-   Red up arrow - File space utilization for all data files in the filegroup are overutilized.  
  
 To view files by volume, select the **Volume** radio button in the **Group files by:** selection. The graph of storage utilization history displays file space used by all data files and all log files on the storage volume. Expand the tree to view details for individual database data files and log files.  
  
 Status icons are used to provide status for each volume:  
  
-   Green check - Resources are well-utilized.  
  
-   Green down arrow - Resources are underutilized.  
  
-   Red up arrow - Resources are overutilized.  
  
 The gauge next to each file name on the Storage Utilization tab represents the amount of space used by the file relative to the effective capacity of the file. The percentage value displayed next to the gauge is the ratio of the amount of space used by the file relative to the effective capacity of the file. Tool tips provide data used to calculate percentages for each volume and each file compared to effective capacity.  
  
 If the file is not configured to auto-grow, then the effective capacity is the amount of space allocated to the file. If the file is configured to auto-grow, then the effective capacity is the sum of the amount of space currently allocated to the file and the amount of free space currently available on the volume.  
  
 Storage utilization policies can be configured for data files and for log files. To view or change the storage utilization policy thresholds for files, click on the **File Policy** link at the bottom of the Storage Utilization pane. To view or change the storage utilization policy thresholds for a storage volume, click on the **Volume Policy** link at the bottom of the Storage Utilization pane.  
  
 To override the default policy thresholds, click on the radio button to **Override the default policy**, specify values for the upper and lower limits, then click **OK**.  
  
 For more information about changing the tolerance for policy violations, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
 Property Details tab  
 Property details listed for instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] include the following categories:  
  
-   Processor Name:  
  
-   Processor Speed (MHz):  
  
-   Processor Count:  
  
-   Physical Memory (MB):  
  
-   Operating System Version:  
  
-   SQL Server Version:  
  
-   SQL Server Edition:  
  
-   Clustered: (True or False)  
  
-   Backup Directory:  
  
-   Collation:  
  
-   Case Sensitive: (True or False)  
  
-   Language:  
  
## See Also  
 [Deployed Data-tier Application Details &#40;SQL Server Utility&#41;](../../2014/database-engine/deployed-data-tier-application-details-sql-server-utility.md)   
 [Utility Dashboard &#40;SQL Server Utility&#41;](../../2014/database-engine/utility-dashboard-sql-server-utility.md)   
 [Monitor Instances of SQL Server in the SQL Server Utility](../relational-databases/manage/monitor-instances-of-sql-server-in-the-sql-server-utility.md)   
 [SQL Server Utility Features and Tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md)   
 [Troubleshoot the SQL Server Utility](../../2014/database-engine/troubleshoot-the-sql-server-utility.md)  
  
  

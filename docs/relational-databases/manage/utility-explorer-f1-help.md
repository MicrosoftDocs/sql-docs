---
title: Utility Explorer F1 Help
description: Get acquainted with functionality that is offered in various areas of the SQL Server Utility. Learn about views, the dashboard, and the Utility Administration tabs.
ms.custom: ""
ms.date: "08/19/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: "reference"
f1_keywords: 
  - "sql13.swb.ue.navigation.f1"
  - "sql13.SWB.UE.dac.details.F1"
  - "sql13.SQB.UE.dac.details.F1"
  - "utility details"
helpviewer_keywords: 
  - "Utility"
  - "management"
  - "data-tier application"
ms.assetid: 8697e4a4-4f59-4cda-af71-7de86005bd4a
author: MikeRayMSFT
ms.author: mikeray
---

# Utility Explorer F1 Help

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The following sections document [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility functionality and associated operations.  
  
  ## Utility Dashboard (SQL Server Utility)
 To see data in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility dashboard, select the top node in the Utility Explorer tree - labeled "Utility<UCP_Name>\\(ComputerName\UCP)." The dashboard includes summary and detail data from all managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and all data-tier applications in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. To refresh data in the dashboard, right-click the top node in the Utility Explorer tree, and select **Refresh**.  
  
 For more information about how to create a utility control point, see [Create a SQL Server Utility Control Point &#40;SQL Server Utility&#41;](../../relational-databases/manage/create-a-sql-server-utility-control-point-sql-server-utility.md). For more information about how to add an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, see [Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](../../relational-databases/manage/enroll-an-instance-of-sql-server-sql-server-utility.md).  
 
  
### UI element list  
 Managed Instance Health  
 Health status for managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is displayed on the left side of the Utility Explorer content pane.  
  
 Managed Instance Health parameters are as follows:  
  
-   CPU utilization for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Database file utilization.  
  
-   Storage volume space utilization.  
  
-   CPU utilization for the computer.  
  
-   Status for each parameter is divided into three categories:  
  
-   Well-utilized - Number of managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] which are not violating resource utilization policies.  
  
-   Underutilized - Number of managed resources which are violating resource underutilization policies.  
  
-   Overutilized - Number of managed resources which are violating resource overutilization policies.  
  
-   No Data Available - Data is not available for managed instances of SQL Server either because the instance of SQL Server has just been enrolled and the first data collection operation has not completed, or because there is a problem with the managed instance of SQL Server collecting and uploading data to the UCP.  
  
 Detailed status for each health parameter is listed in sliding indicators. The fraction to the right of the sliding indicators shows how many managed instances are in each status category.  
  
 To create a filtered view of a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or a data-tier application, click on the link for a utilization category next to its sliding indicator in the Utility dashboard. For example, if you click on **Overutilized Instance CPU** in the **Utility Explorer Content** pane, SSMS creates a filtered list view of managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that have overutilized CPU based on current policy settings.  
  
 Notice that when you click on a link for a utilization category, the corresponding node in the Utility Explorer navigation pane is appended with **(filtered)** - that is, **Managed Instances** is labeled **Managed Instances (filtered)**. To view filter settings, right-click on the node in the navigation pane and select **Filter**, then click on **Filter Settings**. To clear filter settings, right-click on the node in the navigation pane and select **Filter**, then click on **Remove Filter**.  
  
 For more information about viewing health status for individual instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or to view or change policy configuration settings, see [Managed Instance Details &#40;SQL Server Utility&#41;]().  
  
 Utility Summary  
 Displays the number of managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the number of data-tier applications managed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
 Data-tier Application Health  
 Health status for data-tier applications is displayed on the right side of the Utility Explorer content pane.  
  
 Data-tier Application Health parameters are as follows:  
  
-   CPU utilization for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Database file utilization.  
  
-   Storage volume space utilization.  
  
-   CPU utilization for the computer.  
  
 Status for each parameter is divided into three categories:  
  
-   Well-utilized - Number of data-tier applications which are not violating resource utilization policies.  
  
-   Overutilized - Number of data-tier applications which are violating resource overutilization policies.  
  
-   Underutilized - Number of data-tier applications which are violating resource underutilization policies.  
  
-   No Data Available - Data is not available for data-tier applications because the managed instance of SQL Server that contains the data-tier application is not reporting data.  
  
 Detailed status for each health parameter is listed in sliding indicators. The fraction to the right of the sliding indicators shows how many data-tier applications are in each status category. For more information about viewing health status for individual data-tier applications, or to view or change policy configuration settings, see [Deployed Data-tier Application Details &#40;SQL Server Utility&#41;](/previous-versions/sql/sql-server-2016/ee240857(v=sql.130)).  
  
 Utility Storage Utilization History  
 Utilization history is shown in a time graph at the bottom of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility dashboard. Note that time data show the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](../../t-sql/data-types/datetime-transact-sql.md) topic. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](../../t-sql/data-types/datetimeoffset-transact-sql.md) topic.  
  
 Use the radio buttons to the left of the display area to change the reporting period for the graph.  
  
 Options for the reporting interval are:  
  
-   1 Day, displayed in 15-minute intervals.  
  
-   1 Week, displayed in 1-day intervals.  
  
-   1 Month, displayed in 1-week intervals.  
  
-   1 Year, displayed in 1-month intervals.  
  
 After you make a change to the reporting interval, the data refreshes automatically.  
  
 Utility Storage Utilization  
 In the bottom right of the dashboard, the storage utilization pie chart displays the ratio of used space to free space on volumes residing on computers that contain managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Data for this display are refreshed every 15 minutes.  
 
 ## Deployed Data-tier Application Details (SQL Server Utility)
  Information in the Deployed Data-tier Applications view of Utility Explorer provides utilization data for individual data-tier applications, CPU utilization history, storage utilization details at the file level, and the ability to view and update policy thresholds. Policy thresholds can be controlled at the data-tier application level for CPU utilization and for database data files and log files. You can also view property details for individual data-tier applications.  
  
### UI element list  
 List view  
 The list view in the top pane displays data about individual data-tier applications. Health state icons provide summary status for each data-tier application by utilization category:  
  
-   Green check - :::image type="icon" source="media/well-utilized.png" border="false"::: - Number of data-tier application which are not violating resource utilization policies. Resources are well-utilized.  
  
-   Green down arrow - ![down arrow](../../relational-databases/manage/media/utility-down-arrow.gif "Utility_down_arrow") - Resources are underutilized.  
  
-   Red up arrow - ![up arrow](../../relational-databases/manage/media/utility-up-arrow.gif "Utility_up_arrow") - Resources are overutilized.  
  
 The sequence of columns in the list view can be changed by dragging them to the left or the right. Columns in the list view can be added or deleted by right-clicking on the column headings and selecting or unselecting columns. The right-click menu also provides sort options. Sorting can also be activated by clicking at the top of a column name.  
  
 To access filter options for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility list view, right-click on the **Deployed Data-tier applications** node in the Utility Explorer navigation pane, and select **Filter**. After filter settings have been implemented, the **Deployed Data-tier Applications** node in Utility Explorer will be labeled **Deployed Data-tier Applications (filtered)**. For more information, see [Filter Settings &#40;Object Explorer and Utility Explorer&#41;](../../ssms/object/filter-settings-object-explorer-and-utility-explorer.md).  
  
 By default, the following columns display health state information about each data-tier application.  
  
-   Name - the data-tier application name.  
  
-   Application CPU - Displays the health state of processor utilization for this data-tier application. The health state for this parameter is determined according to CPU utilization policy set for the data-tier application and the configuration setting for volatile resource evaluation policy. For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
     To view the processor utilization history for this data-tier application, or to view or change the policy limits, click on the **CPU Utilization** tab.  
  
-   Computer CPU - Displays the health state of computer processor utilization. The health state for this parameter is determined according to CPU utilization policy set for the computer and the configuration setting for volatile resource evaluation policy. For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
     To view the processor utilization history for this data-tier application, or to view or change the policy limits, click on the **CPU Utilization** tab.  
  
-   File Space - Displays a summary of health states of file space utilization for each data-tier application.  
  
    -   Green check - The health states for all filegroups and the log file group are neither overutilized or underutilized.  
  
    -   Green down arrow - The health state for at least one filegroup or log file group is underutilized, and no filegroup or log file group is overutilized.  
  
    -   Red up arrow - The health state for at least one filegroup or the log file group is overutilized. Note that if a database is in "emergency" state, the health state will display overutilized log file space.  
  
     To view or change the file space policy limits, click on the **Storage Utilization** tab.  
  
-   Volume Space - Displays a summary of the health states of volume space utilization for all volumes that contain databases belonging to the selected data-tier application. If the health state of any volume is overutilized, then the volume space health state will be reported in the list view as overutilized. If the health state of any volume is underutilized, and no volume is overutilized, then the volume space health state will be reported in the list view as underutilized. Otherwise, the volume space health state will be reported in the list view as well-utilized. To view or change the policy limits, click on the **Storage Utilization** tab.  
  
-   Policy Type - Indicates whether "Global" default policies or "Override" custom policies are in effect for the data-tier application.  
  
-   Instance Name - Displays the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the data-tier application.  
  
 Other columns that can be displayed using the right-click menu in the column heading area of the list view:  
  
-   Database Name  
  
-   Deployed Date  
  
-   Trustworthy: (True or False)  
  
-   Collation  
  
-   Compatibility Level: (for example, Version100)  
  
-   Encryption Enabled: (True or False)  
  
-   Recovery Model: (Simple, Full, or Bulk-Logged)  
  
-   Last Reported Time: This column shows the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](../../t-sql/data-types/datetime-transact-sql.md) topic. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](../../t-sql/data-types/datetimeoffset-transact-sql.md) topic.  
  
 CPU Utilization tab  
 The CPU utilization tab shows side-by-side graphs of historical data for data-tier application and computer CPU utilization.  
  
 The graph displays the processor utilization history for the interval specified in the radio buttons on the right side of the display area. To change the display interval and refresh the data set, select a radio button from the list.  
  
 Interval options are as follows:  
  
-   1 Day, displayed in 15-minute intervals.  
  
-   1 Week, displayed in 1-day intervals.  
  
-   1 Month, displayed in 1-week intervals.  
  
-   1 Year, displayed in 1-month intervals.  
  
 Storage Utilization tab  
 The Storage Utilization tab has a tree view that displays storage utilization details for database files and log files that belong to the data-tier application selected in the list view. Note that time data show the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](../../t-sql/data-types/datetime-transact-sql.md) topic. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](../../t-sql/data-types/datetimeoffset-transact-sql.md) topic.  
  
 Display can be grouped by filegroup or by volume. To use the filegroup tree view, select the **Filegroup** radio button in the **Group files by:** selection.  
  
 Data displayed in the graph of storage utilization history changes depending on the node selected in the tree view:  
  
-   Select the file group node to display a graph of file space used by all data files that belong to the selected file group.  
  
-   Select the log file node to display a graph of file space used by all log files that belong to the selected database.  
  
-   Select the database node to display a graph that adds file space used by all data files and all log files that belong to the selected database.  
  
 To view storage utilization status for individual files, click on the plus sign next to a file name in the tree view.  
  
 Health state icons provide at-a-glance status for each filegroup compared to policy thresholds:  
  
-   Green check - File space utilization for at least one data file in the filegroup is neither overutilized or underutilized.  
  
-   Green down arrow - File space utilization for at least one data file in the filegroup is underutilized, and no files in the filegroup are overutilized.  
  
-   Red up arrow - File space utilization for all data files in the filegroup are overutilized. Note that if a database is in "emergency" state, the health state will display overutilized log file space.  
  
 To view files by volume, select the **Volume** radio button in the **Group files by:** selection. The graph of storage utilization history displays file space used by all data files and all log files on the storage volume. Expand the tree to view details for individual database data files and log files.  
  
 Status icons are used to provide status for each volume:  
  
-   Green check - Resources are well-utilized.  
  
-   Green down arrow - Resources are underutilized.  
  
-   Red up arrow - Resources are overutilized.  
  
 The gauge next to each file name on the Storage Utilization tab represents the amount of space used by the file relative to the effective capacity of the file. The percentage value displayed next to the gauge is the ratio of the amount of space used by the file relative to the effective capacity of the file. Tool tips provide data used to calculate percentages for each volume and each file compared to effective capacity.  
  
 If the file is not configured to auto-grow, then the effective capacity is the amount of space allocated to the file. If the file is configured to auto-grow, then the effective capacity is the sum of the amount of space currently allocated to the file and the amount of free space currently available on the volume.  
  
 Storage utilization policies can be configured for data files and for log files. To view or change the storage utilization policy thresholds for files, click on the **File Policy** link at the bottom of the Storage Utilization pane. To view or change the storage utilization policy thresholds for a storage volume, click on the **Volume Policy** link at the bottom of the Storage Utilization pane.  
  
 To override the default policy thresholds, click on the radio button to **Override the default policy**, specify values for the upper and lower limits, then click **OK**.  
  
 For more information about changing the tolerance for policy violations, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
 Property Details tab  
 Property details listed for data-tier applications include the following categories:  
  
-   Database Name  
  
-   Deployed Date  
  
-   Trustworthy: (True or False)  
  
-   Collation  
  
-   Compatibility Level: (for example, Version100)  
  
-   Encryption Enabled: (True or False)  
  
-   Recovery Model: (Simple, Full, or Bulk-Logged)  
  
-   Last Reported Time: This column shows the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](../../t-sql/data-types/datetime-transact-sql.md) topic. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](../../t-sql/data-types/datetimeoffset-transact-sql.md) topic.

## Managed Instance Details (SQL Server Utility)
 Information in the Managed Instances view of Utility Explorer provides utilization data for individual instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], CPU utilization history, storage utilization details at the file level, and the ability to view and update policy thresholds. Policy thresholds can be controlled at the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance level, for a computer, for database files and log files, and at the level of storage volumes. You can also view property details for individual managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### UI element list  
 List view  
 The list view in the top pane displays data about individual instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listed in rows by ComputerName\InstanceName.  
  
 Health state icons provide summary status for each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by utilization category:  
  
-   Green check - ![green check](../../relational-databases/manage/media/well-utilized.gif "Well_utilized") - Number of managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] which are not violating resource utilization policies. Resources are well-utilized.  
  
-   Green down arrow - ![down arrow](../../relational-databases/manage/media/utility-down-arrow.gif "Utility_down_arrow") - Resources are underutilized.  
  
-   Red up arrow - ![up arrow](../../relational-databases/manage/media/utility-up-arrow.gif "Utility_up_arrow") - Resources are overutilized.  
  
 The sequence of columns in the list view can be changed by dragging them to the left or the right. Columns in the list view can be added or deleted by right-clicking on the column headings and selecting or unselecting columns. The right-click menu also provides sort options. Sorting can also be activated by clicking at the top of a column name.  
  
 To access filter options for the Utility list view, right-click on the **Managed Instances** node in the Utility Explorer navigation pane, and select **Filter**. After filter settings have been implemented, the **Managed Instances** node in Utility Explorer will be labeled **Managed Instances (filtered)**. For more information, see [Filter Settings &#40;Object Explorer and Utility Explorer&#41;](../../ssms/object/filter-settings-object-explorer-and-utility-explorer.md).  
  
 By default, the following columns display health state information about each managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Instance CPU - Displays the health state of processor utilization allocated to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The health state for this parameter is determined according to CPU utilization policy set for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the configuration setting for volatile resource evaluation policy. For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
     To view the processor utilization history for this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or to view or change the policy limits, click on the **CPU Utilization** tab.  
  
-   Computer CPU - Displays the health state of computer processor utilization. The health state for this parameter is determined according to CPU utilization policy set for the computer and the configuration setting for volatile resource evaluation policy. For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
     To view the processor utilization history for this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or to view or change the policy limits, click on the **CPU Utilization** tab.  
  
-   File Space - Displays a summary of health states of file space utilization for all databases that belong to the selected instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the health state of any database is overutilized, then the file space health state will be reported in the list view as overutilized. If the health state of any database is underutilized, and no database is overutilized, then the file space health state will be reported in the list view as underutilized. Otherwise, the file space health state will be reported in the list view as well-utilized. To view or change the policy limits, click on the **Storage Utilization** tab.  
  
-   Volume Space - Displays a summary of the health states of volume space utilization for all volumes that contain databases belonging to the selected instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the health state of any volume is overutilized, then the volume space health state will be reported in the list view as overutilized. If the health state of any volume is underutilized, and no volume is overutilized, then the volume space health state will be reported in the list view as underutilized. Otherwise, the volume space health state will be reported in the list view as well-utilized. To view or change the policy limits, click on the **Storage Utilization** tab.  
  
-   Policy Type - Indicates whether "Global" default policies or "Override" custom policies are in effect for the managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
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
  
-   Last Reported Time: This column shows the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](../../t-sql/data-types/datetime-transact-sql.md) topic. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](../../t-sql/data-types/datetimeoffset-transact-sql.md) topic.  
  
 CPU Utilization tab  
 The CPU utilization tab shows side-by-side graphs of historical data for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and computer CPU utilization.  
  
 The graph displays the processor utilization history for the interval specified in the radio buttons on the right side of the display area. To change the display interval and refresh the data set, select a radio button from the list.  
  
 Interval options are as follows:  
  
-   1 Day, displayed in 15-minute intervals.  
  
-   1 Week, displayed in 1-day intervals.  
  
-   1 Month, displayed in 1-week intervals.  
  
-   1 Year, displayed in 1-month intervals.  
  
 Storage Utilization tab  
 The Storage Utilization tab has a tree view that displays storage utilization details. Note that time data show the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](../../t-sql/data-types/datetime-transact-sql.md) topic. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](../../t-sql/data-types/datetimeoffset-transact-sql.md) topic.  
  
 Display can be grouped by database or by volume. To use the database tree view, select the **Database** radio button in the **Group files by:** selection. To view storage utilization status for individual database files, click on the plus sign next to a database name in the tree view. The database files listed include all system and user databases that belong to the managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you selected in the list view.  
  
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
  
 For more information about changing the tolerance for policy violations, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
 Property Details tab  
 Property details listed for instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] include the following categories:  
  
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

## Utility Administration (SQL Server Utility)
Use the Utility Administration tabs to manage policy, security, and data warehouse settings for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility concepts, see [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md).  
  
### UI element list
 **Policy tab** - Use the policy tab to view or specify global monitoring policies.  
  
 Set global data-tier application monitoring policies. To expand the list of values for this option, click on the arrow next to the policy name, or click on the policy title.  
 When is an application running out of processor capacity? To change this policy, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for processor utilization is 70%.  
  
-   The default minimum value for processor utilization is 0%.  
  
 When is an application running out of file space? To change the policy for data file or log file space utilization, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for file space utilization is 70%.  
  
-   The default minimum value for file space utilization is 0%.  
  
 Set global SQL Server managed instance application monitoring policies. To expand the list of values for this option, click on the arrow next to the policy name, or click on the policy title.  
 When is a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running out of processor capacity? To change this policy, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for instance processor utilization is 70%.  
  
-   The default minimum value for instance processor utilization is 0%.  
  
 When is a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer running out of processor capacity? To change this policy, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for computer processor utilization is 70%.  
  
-   The default minimum value for computer processor utilization is 0%.  
  
 When is a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running out of file space? To change the policy for data file or log file space utilization , use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for file space utilization is 70%.  
  
-   The default minimum value for file space utilization is 0%.  
  
 When is a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer running out of storage volume space? To change this policy, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for computer volume space utilization is 70%.  
  
-   The default minimum value for computer volume space utilization is 0%.  
  
 Reducing policy violation noise from highly volatile resources. To expand the controls for this feature, click on the down-arrow on the right side of the display.  
 For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md)  
  
 
**Security tab** - Displays login names with permissions to administer or read from the UCP.  
  
 Select the logins from the UCP that will be added to the Utility Reader role.  
 The Utility Reader privilege allows the user account to:  
  
-   Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
-   See all viewpoints in the Utility Explorer in SSMS.  
  
-   See settings on the Utility Administration node in Utility Explorer in SSMS.  
  
 Utility administrators can enroll instances of SQL Server into and remove instances of SQL Server from a SQL Server Utility, as well as modify policies on managed instances and modify administration settings on the UCP.  
  
 To be a Utility administrator, you must have sysadmin privileges on the instance of SQL Server. To add or change user accounts for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] UCP, use Object Explorer in SSMS to add the user to the server logins of the UCP instance of SQL Server. For more information, see [sp_addlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlogin-transact-sql.md).  
  
 
**Data Warehouse** tab - Displays configuration details for the utility management data warehouse.  
  
 Data Retention  
 Specify the data retention period for utilization information collected for managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The default time period is 1 year. The minimum value is 1 month. The longest supported value is 2 years.  
  
 Utility Data Warehouse Configuration Information  
 The following configuration settings are not configurable in this release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   UMDW name: Sysutility_mdw_\<GUID>_DATA.  
  
-   Collection set upload frequency: Every 15 minutes.  
  
 The UMDW directory is configurable: \<System drive>:\Program Files\Microsoft SQL Server\MSSQL10_50.<UCP_Name>\MSSQL\Data\\, where \<System drive> is normally the C:\ drive. The log file, UMDW_\<GUID>_LOG, is located in the same directory.  
  
> [!NOTE]  
> The UMDW (sysutility_mdw) file location can be changed using detach/attach or ALTER DATABASE. We recommend the use of ALTER DATABASE. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
 Go back to out-of-the-box defaults  
 To reset settings on this tab to default values, click the **Restore Defaults** button, then click **Apply**.  
 
  
## Reference  
 [Create a SQL Server Utility Control Point &#40;SQL Server Utility&#41;](../../relational-databases/manage/create-a-sql-server-utility-control-point-sql-server-utility.md)  
  
 [Connect to a SQL Server Utility](../../relational-databases/manage/connect-to-a-sql-server-utility.md)  
  
 [Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](../../relational-databases/manage/enroll-an-instance-of-sql-server-sql-server-utility.md)  
  
 [Configure Health Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/configure-health-policies-sql-server-utility.md)  
  
 [Monitor Instances of SQL Server in the SQL Server Utility](../../relational-databases/manage/monitor-instances-of-sql-server-in-the-sql-server-utility.md)  
  
## See Also  
 [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md)   
 [Troubleshoot the SQL Server Utility](/previous-versions/sql/sql-server-2016/ee210592(v=sql.130))  
  

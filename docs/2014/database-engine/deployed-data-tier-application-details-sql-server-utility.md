---
title: "Deployed Data-tier Application Details (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "SQL12.SWB.UE.dac.details.F1"
helpviewer_keywords: 
  - "utility, management"
  - "Utility"
  - "SQL Server Utility [SQL Server]"
  - "data-tier application [SQL Server], utility details"
  - "Multi-server management [SQL Server]"
ms.assetid: 79c41dd9-abcb-434e-9326-00a341d5c867
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Deployed Data-tier Application Details (SQL Server Utility)
  Information in the Deployed Data-tier Applications view of Utility Explorer provides utilization data for individual data-tier applications, CPU utilization history, storage utilization details at the file level, and the ability to view and update policy thresholds. Policy thresholds can be controlled at the data-tier application level for CPU utilization and for database data files and log files. You can also view property details for individual data-tier applications.  
  
## UIElement List  
 List view  
 The list view in the top pane displays data about individual data-tier applications. Health state icons provide summary status for each data-tier application by utilization category:  
  
-   Green check - ![](../../2014/database-engine/media/well-utilized.gif "Well_utilized") - Number of data-tier application which are not violating resource utilization policies. Resources are well-utilized.  
  
-   Green down arrow - ![](../../2014/database-engine/media/utility-down-arrow.gif "Utility_down_arrow") - Resources are underutilized.  
  
-   Red up arrow - ![](../../2014/database-engine/media/utility-up-arrow.gif "Utility_up_arrow") - Resources are overutilized.  
  
 The sequence of columns in the list view can be changed by dragging them to the left or the right. Columns in the list view can be added or deleted by right-clicking on the column headings and selecting or unselecting columns. The right-click menu also provides sort options. Sorting can also be activated by clicking at the top of a column name.  
  
 To access filter options for the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility list view, right-click on the **Deployed Data-tier applications** node in the Utility Explorer navigation pane, and select **Filter**. After filter settings have been implemented, the **Deployed Data-tier Applications** node in Utility Explorer will be labeled **Deployed Data-tier Applications (filtered)**. For more information, see [Filter Settings &#40;Object Explorer and Utility Explorer&#41;](../ssms/object/filter-settings-object-explorer-and-utility-explorer.md).  
  
 By default, the following columns display health state information about each data-tier application.  
  
-   Name - the data-tier application name.  
  
-   Application CPU - Displays the health state of processor utilization for this data-tier application. The health state for this parameter is determined according to CPU utilization policy set for the data-tier application and the configuration setting for volatile resource evaluation policy. For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
     To view the processor utilization history for this data-tier application, or to view or change the policy limits, click on the **CPU Utilization** tab.  
  
-   Computer CPU - Displays the health state of computer processor utilization. The health state for this parameter is determined according to CPU utilization policy set for the computer and the configuration setting for volatile resource evaluation policy. For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
     To view the processor utilization history for this data-tier application, or to view or change the policy limits, click on the **CPU Utilization** tab.  
  
-   File Space - Displays a summary of health states of file space utilization for each data-tier application.  
  
    -   Green check - The health states for all filegroups and the log file group are neither overutilized or underutilized.  
  
    -   Green down arrow - The health state for at least one filegroup or log file group is underutilized, and no filegroup or log file group is overutilized.  
  
    -   Red up arrow - The health state for at least one filegroup or the log file group is overutilized. Note that if a database is in "emergency" state, the health state will display overutilized log file space.  
  
     To view or change the file space policy limits, click on the **Storage Utilization** tab.  
  
-   Volume Space - Displays a summary of the health states of volume space utilization for all volumes that contain databases belonging to the selected data-tier application. If the health state of any volume is overutilized, then the volume space health state will be reported in the list view as overutilized. If the health state of any volume is underutilized, and no volume is overutilized, then the volume space health state will be reported in the list view as underutilized. Otherwise, the volume space health state will be reported in the list view as well-utilized. To view or change the policy limits, click on the **Storage Utilization** tab.  
  
-   Policy Type - Indicates whether "Global" default policies or "Override" custom policies are in effect for the data-tier application.  
  
-   Instance Name - Displays the name of the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that hosts the data-tier application.  
  
 Other columns that can be displayed using the right-click menu in the column heading area of the list view:  
  
-   Database Name  
  
-   Deployed Date  
  
-   Trustworthy: (True or False)  
  
-   Collation  
  
-   Compatibility Level: (for example, Version100)  
  
-   Encryption Enabled: (True or False)  
  
-   Recovery Model: (Simple, Full, or Bulk-Logged)  
  
-   Last Reported Time: This column shows the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=164071) topic in SQL Server Books Online. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=141713) topic in SQL Server Books Online.  
  
 CPU Utilization tab  
 The CPU utilization tab shows side-by-side graphs of historical data for data-tier application and computer CPU utilization.  
  
 The graph displays the processor utilization history for the interval specified in the radio buttons on the right side of the display area. To change the display interval and refresh the data set, select a radio button from the list.  
  
 Interval options are as follows:  
  
-   1 Day, displayed in 15-minute intervals.  
  
-   1 Week, displayed in 1-day intervals.  
  
-   1 Month, displayed in 1-week intervals.  
  
-   1 Year, displayed in 1-month intervals.  
  
 Storage Utilization tab  
 The Storage Utilization tab has a tree view that displays storage utilization details for database files and log files that belong to the data-tier application selected in the list view. Note that time data show the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=164071) topic in SQL Server Books Online. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=141713) topic in SQL Server Books Online.  
  
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
  
 For more information about changing the tolerance for policy violations, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
 Property Details tab  
 Property details listed for data-tier applications include the following categories:  
  
-   Database Name  
  
-   Deployed Date  
  
-   Trustworthy: (True or False)  
  
-   Collation  
  
-   Compatibility Level: (for example, Version100)  
  
-   Encryption Enabled: (True or False)  
  
-   Recovery Model: (Simple, Full, or Bulk-Logged)  
  
-   Last Reported Time: This column shows the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=164071) topic in SQL Server Books Online. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=141713) topic in SQL Server Books Online.  
  
## See Also  
 [Managed instance details &#40;SQL Server Utility&#41;](../../2014/database-engine/managed-instance-details-sql-server-utility.md)   
 [Utility Dashboard &#40;SQL Server Utility&#41;](../../2014/database-engine/utility-dashboard-sql-server-utility.md)   
 [Monitor Instances of SQL Server in the SQL Server Utility](../relational-databases/manage/monitor-instances-of-sql-server-in-the-sql-server-utility.md)   
 [SQL Server Utility Features and Tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md)  
  
  

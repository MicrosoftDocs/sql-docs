---
title: "Utility Dashboard (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
ms.assetid: 999eb741-4a60-43f6-ab37-2df7dce845c1
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Utility Dashboard (SQL Server Utility)
  To see data in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility dashboard, select the top node in the Utility Explorer tree - labeled "Utility<UCP_Name>\\(ComputerName\UCP)." The dashboard includes summary and detail data from all managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and all data-tier applications in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility. To refresh data in the dashboard, right-click the top node in the Utility Explorer tree, and select **Refresh**.  
  
 For more information about how to create a utility control point, see [Create a SQL Server Utility Control Point &#40;SQL Server Utility&#41;](../relational-databases/manage/create-a-sql-server-utility-control-point-sql-server-utility.md). For more information about how to add an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility, see [Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](../relational-databases/manage/enroll-an-instance-of-sql-server-sql-server-utility.md).  
  
## UIElement List  
 Managed Instance Health  
 Health status for managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is displayed on the left side of the Utility Explorer content pane.  
  
 Managed Instance Health parameters are as follows:  
  
-   CPU utilization for the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   Database file utilization.  
  
-   Storage volume space utilization.  
  
-   CPU utilization for the computer.  
  
-   Status for each parameter is divided into three categories:  
  
-   Well-utilized - Number of managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] which are not violating resource utilization policies.  
  
-   Underutilized - Number of managed resources which are violating resource underutilization policies.  
  
-   Overutilized - Number of managed resources which are violating resource overutilization policies.  
  
-   No Data Available - Data is not available for managed instances of SQL Server either because the instance of SQL Server has just been enrolled and the first data collection operation has not completed, or because there is a problem with the managed instance of SQL Server collecting and uploading data to the UCP.  
  
 Detailed status for each health parameter is listed in sliding indicators. The fraction to the right of the sliding indicators shows how many managed instances are in each status category.  
  
 To create a filtered view of a managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or a data-tier application, click on the link for a utilization category next to its sliding indicator in the Utility dashboard. For example, if you click on **Overutilized Instance CPU** in the **Utility Explorer Content** pane, SSMS creates a filtered list view of managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that have overutilized CPU based on current policy settings.  
  
 Notice that when you click on a link for a utilization category, the corresponding node in the Utility Explorer navigation pane is appended with **(filtered)** - that is, **Managed Instances** is labeled **Managed Instances (filtered)**. To view filter settings, right-click on the node in the navigation pane and select **Filter**, then click on **Filter Settings**. To clear filter settings, right-click on the node in the navigation pane and select **Filter**, then click on **Remove Filter**.  
  
 For more information about viewing health status for individual instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], or to view or change policy configuration settings, see [Managed Instance Details &#40;SQL Server Utility&#41;](../../2014/database-engine/managed-instance-details-sql-server-utility.md).  
  
 Utility Summary  
 Displays the number of managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and the number of data-tier applications managed by the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility.  
  
 Data-tier Application Health  
 Health status for data-tier applications is displayed on the right side of the Utility Explorer content pane.  
  
 Data-tier Application Health parameters are as follows:  
  
-   CPU utilization for the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   Database file utilization.  
  
-   Storage volume space utilization.  
  
-   CPU utilization for the computer.  
  
 Status for each parameter is divided into three categories:  
  
-   Well-utilized - Number of data-tier applications which are not violating resource utilization policies.  
  
-   Overutilized - Number of data-tier applications which are violating resource overutilization policies.  
  
-   Underutilized - Number of data-tier applications which are violating resource underutilization policies.  
  
-   No Data Available - Data is not available for data-tier applications because the managed instance of SQL Server that contains the data-tier application is not reporting data.  
  
 Detailed status for each health parameter is listed in sliding indicators. The fraction to the right of the sliding indicators shows how many data-tier applications are in each status category. For more information about viewing health status for individual data-tier applications, or to view or change policy configuration settings, see [Deployed Data-tier Application Details &#40;SQL Server Utility&#41;](../../2014/database-engine/deployed-data-tier-application-details-sql-server-utility.md).  
  
 Utility Storage Utilization History  
 Utilization history is shown in a time graph at the bottom of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility dashboard. Note that time data show the UCP local date and time using the datetime data type. For more information, see the [datetime (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=164071) topic in SQL Server Books Online. When using the Utility object model, note that SSMS uses the datetimeoffset data type. For more information, see the [datetimeoffset (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkId=141713) topic in SQL Server Books Online.  
  
 Use the radio buttons to the left of the display area to change the reporting period for the graph.  
  
 Options for the reporting interval are:  
  
-   1 Day, displayed in 15-minute intervals.  
  
-   1 Week, displayed in 1-day intervals.  
  
-   1 Month, displayed in 1-week intervals.  
  
-   1 Year, displayed in 1-month intervals.  
  
 After you make a change to the reporting interval, the data refreshes automatically.  
  
 Utility Storage Utilization  
 In the bottom right of the dashboard, the storage utilization pie chart displays the ratio of used space to free space on volumes residing on computers that contain managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Data for this display are refreshed every 15 minutes.  
  
## See Also  
 [Use Utility Explorer to Manage the SQL Server Utility](../relational-databases/manage/use-utility-explorer-to-manage-the-sql-server-utility.md)   
 [Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](../relational-databases/manage/enroll-an-instance-of-sql-server-sql-server-utility.md)   
 [Modify a Resource Health Policy Definition &#40;SQL Server Utility&#41;](../relational-databases/manage/modify-a-resource-health-policy-definition-sql-server-utility.md)  
  
  

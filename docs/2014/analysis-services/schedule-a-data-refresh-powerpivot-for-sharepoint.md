---
title: "Schedule a Data Refresh (PowerPivot for SharePoint) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "unattended data refresh [Analysis Services with SharePoint]"
  - "scheduled data refresh [Analysis Services with SharePoint]"
  - "data refresh [Analysis Services with SharePoint]"
ms.assetid: 8571208f-6aae-4058-83c6-9f916f5e2f9b
author: minewiskan
ms.author: owend
manager: craigg
---
# Schedule a Data Refresh (PowerPivot for SharePoint)
  You can schedule data refresh to get automatic updates to PowerPivot data inside an Excel workbook that you published to a SharePoint site.  
  
 **[!INCLUDE[applies](../includes/applies-md.md)]**  SharePoint 2010  
  
 **In this topic:**  
  
 [Prerequisites](#prereq)  
  
 [Data Refresh Overview](#intro)  
  
 [Enable and schedule data refresh](#drenablesched)  
  
 [Verify Data Refresh](#drverify)  
  
> [!NOTE]  
>  PowerPivot data refresh is performed by Analysis Services server instances in the SharePoint farm. It is not related to the data refresh feature that is provided in Excel Services. The PowePivot Schedule data refresh feature will not refresh non PowerPivot data.  
  
##  <a name="prereq"></a> Prerequisites  
 You must have Contribute level of permission or greater on the workbook to create a data refresh schedule.  
  
 External data sources that are accessed during data refresh must be available and the credentials you specify in the schedule must have permission to access those data sources. Scheduled data refresh requires a data source location that is accessible over a network connection (for example, from a network file share rather than a local folder on your workstation).  
  
 The data source cannot be an Office document or Access database. Office does not support the use of the Office data connectivity components in a server environment. If your workbook contains data from these sources, be sure to remove those sources from the data source list in your data refresh schedule.  
  
 The workbook must be a [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] version. If you use workbooks that were created in the previous release of PowerPivot for Excel, schedule data refresh will not work unless you upgrade the database to the most recent version.  
  
 The workbook must be checked in at the time the refresh operation is finished. A lock on the workbook is placed on the file at the end of data refresh, when the file is saved, rather than when refresh starts.  
  
> [!NOTE]  
>  The server does not lock the workbook while the data refresh is in progress. However, it does lock the file at the end of data refresh for the purpose of checking in the updated file. If at this time, the file is checked out to another user, the refreshed data will be thrown out. Similarly, if the file is checked in, but it is significantly different from the copy retrieved by the server at the start of data refresh, the refreshed data will be discarded.  
  
##  <a name="intro"></a> Data Refresh Overview  
 PowerPivot data in an Excel workbook can originate from multiple external data sources, including external databases or data files that you access from remote servers or network file shares. For PowerPivot workbooks that contain imported data from connected or external data sources, you can configure data refresh to schedule an automatic import of updated data from those original sources.  
  
 An external data source is accessed through an embedded connection string, URL, or UNC path that you specified when you imported the original data into the workbook using the PowerPivot client application. Original connection information that is stored in the PowerPivot workbook is reused for subsequent data refresh operations. Although you can overwrite credentials used to connect to data sources, you cannot overwrite connection strings for data refresh purposes; only existing connection information is used.  
  
 There is only one data refresh schedule page for each workbook, and all schedule information is specified on that page. Typically, the person who authored the workbook defines the schedule.  
  
 As the schedule owner, you perform the following tasks:  
  
-   Define the default schedule. This is the schedule that is used if there are no scheduled defined at the data source level.  
  
-   Specify the credentials used to run data refresh  
  
-   Choose which data sources to include in the refresh operation.  
  
-   Optionally, specify in-line, individual schedules and credentials for each data source that is queried during data refresh. Each data source can be refreshed independently. If you create custom schedules for every data source, the default schedule is ignored.  
  
 Creating granular schedules for individual data sources enables you to match the refresh schedule to fluctuations in the external data sources. For example, if an external data source contains transactional data that is generated throughout the day, you can create an individual data refresh schedule for that data source to get its updated information nightly.  
  
##  <a name="drenablesched"></a> Enable and schedule data refresh  
 Use the following instructions to schedule data refresh for PowerPivot data in an Excel workbook that is published to a SharePoint library.  
  
1.  In the library that contains the workbook, select the workbook and then click the down arrow to display a list of commands.  
  
2.  Click **Manage PowerPivot Data Refresh**. If a data refresh schedule is already defined, you will see the View Data Refresh history page instead. You can click **Configure data refresh** to open the schedule definition page.  
  
3.  In the schedule definition page, select the **Enable** checkbox.  
  
4.  In Schedule Details, specify the type of schedule and specify its details. This step creates the default schedule.  
  
    > [!IMPORTANT]  
    >  Be sure to select the **Also refresh as soon as possible** checkbox. Doing so lets you verify that data refresh is operational for this workbook. Note that after you save schedule, it can take up to a minute for data refresh to start.  
  
5.  In Earliest Start Time, choose one of the following:  
  
    1.  **After business hours** specifies an off-hours processing period when database servers are more likely to have current data that was generated throughout the business day.  
  
    2.  **Specific earliest start time** is the hour and minutes of the earliest time of day the data refresh request is added to a process queue. You can specify minutes in 15-minute intervals. The setting applies to the current day as well as future dates. For example, if you specify a value of 6:30 A.M. and the current time is 4:30 P.M., the refresh request will be added to the queue in the current day because 4:30 P.M. is after 6:30 A.M.  
  
     Earliest start time defines when a request is added to the process queue. Actual processing occurs when the server has adequate resources to begin data processing. Actual processing time will be recorded in data refresh history after processing is complete.  
  
6.  Select the checkbox **Also refresh as soon as possible** to run data refresh as soon as the server can process it. A successful outcome of a data refresh job verifies that the data sources are available and that permissions and server configuration are set correctly.  
  
7.  In E-mail notifications, enter the e-mail address of the person who should be notified in the event of a processing error.  
  
8.  In Credentials, specify an account used to run the data refresh job. The account must have Contribute permissions on the workbook so that it can open the workbook to refresh its data. It must be a Windows domain user account. In many cases, this account must also have read permissions on the external data sources used during data refresh. Specifically, if you originally imported the data using the Use Windows Authentication option, then the connection string is built to use the Windows credentials of the current user. If the current user is the data refresh account, that account must have read permissions on the external data source in order for data refresh to succeed. Choose one of the following options:  
  
    1.  Choose **Use the data refresh account configured by the administrator** to process the data refresh operation using the PowerPivot unattended data refresh account.  
  
    2.  Choose **Connect using the following Windows user credentials** if you want to enter a user name and password. Enter the account information in domain\user format.  
  
    3.  Choose **Connect using the credentials saved in Secure Store Service** if you know the ID of a target application that contains previously stored credentials you want to use.  
  
     For more information about these options, see [Configure Stored Credentials for PowerPivot Data Refresh &#40;PowerPivot for SharePoint&#41;](configure-stored-credentials-data-refresh-powerpivot-sharepoint.md) and [Configure the PowerPivot Unattended Data Refresh Account &#40;PowerPivot for SharePoint&#41;](configure-unattended-data-refresh-account-powerpivot-sharepoint.md).  
  
9. In Data Sources, select the **All data sources** checkbox if you want data refresh to re-query all of the original data sources.  
  
     If you select this option, any external data source that provides PowerPivot data is automatically included in the refresh, even if the list of data sources changes over time as you add or remove data sources that provide data to the workbook.  
  
     Clear the **All data sources** checkbox if you want to manually select which data sources to include. If you later modify the workbook by adding a new data source, be sure to update the data source list in the schedule. Otherwise, newer data sources will not be included in the refresh operation.  
  
     The list of data sources that you can choose from is retrieved from the PowerPivot workbook when you open the Manage PowerPivot Data Refresh page for the workbook.  
  
     Be sure to select only those data sources that meet the following criteria:  
  
    -   The data source must be available at the time that data refresh occurs and at the stated location. If the original data source is on a local disk drive of the person who authored the workbook, you must either exclude that data source from the data refresh operation, or find a way to publish that data source to a location that is accessible through a network connection. If you move a data source to a network location, be sure to open the workbook in [!INCLUDE[ssGeminiClient](../includes/ssgeminiclient-md.md)] and update the data source connection information. This is necessary to re-establish the connection information that is stored in the PowerPivot workbook.  
  
    -   The data source must be accessed using the credential information that is embedded in the PowerPivot workbook or that is specified in the schedule. Embedded credential information is stored in the PowerPivot workbook when you import data using PowerPivot for Excel. Embedded credential information is often SSPI=IntegratedSecurity or SSPI=TrustedConnection, which means use the credentials of the current user to connect to the data source. If you want to override the credential information in your data refresh schedule, you can specify predefined, stored credentials. For more information, see [Configure Stored Credentials for PowerPivot Data Refresh &#40;PowerPivot for SharePoint&#41;](configure-stored-credentials-data-refresh-powerpivot-sharepoint.md).  
  
    -   Data refresh must succeed for all of the data sources that you specify. Otherwise, the refreshed data is discarded, leaving you with the last saved version of the workbook. Exclude any data sources that you are not sure about.  
  
    -   Data refresh must not invalidate other data in your workbook. When you refresh a subset of your data, it is important that you understand whether the workbook is still valid once newer data is aggregated with static data that is not from the same time period. As a workbook author, it is up to you to know your data dependencies and ensure that data refresh is appropriate for the workbook itself.  
  
10. Optionally, you can define individual schedules for specific data sources. This is useful if you have original data sources that are themselves updated on a schedule. For example, if a PowerPivot data source uses data from a data mart that is refreshed every Monday at 02:00 hours, you can define an inline schedule for the data mart to retrieve its refreshed data every Monday at 04:00 hours.  
  
11. Click **OK** to save your schedule.  
  
##  <a name="drverify"></a> Verify Data Refresh  
 The best way to verify data refresh is to execute data refresh right away and then review the history page to see whether it completed successfully. Selecting the **Also refresh as soon as possible** checkbox on your schedule will provide the verification that data refresh is operational.  
  
 You can view the current and past record of data refresh operations in the Data Refresh History page for the workbook. This page only appears if data refresh has been scheduled for a workbook. If there is no data refresh schedule, the schedule definition page appears instead.  
  
 You must have Contribute permissions or greater to view data refresh history.  
  
1.  On a SharePoint site, open the library that contains a PowerPivot workbook.  
  
     There is no visual indicator that identifies which workbooks in a SharePoint library contain PowerPivot data. You must know in advance which workbooks contain refreshable PowerPivot data.  
  
2.  Select the document, and then click the down arrow that appears to the right.  
  
3.  Select **Manage PowerPivot Data Refresh**.  
  
 The history page appears, showing a complete record for all refresh activity for PowerPivot data in the current Excel workbook, including the status of the most recent data refresh operation.  
  
 In some cases, you might see actual processing times that differ from the time you specified. This will occur if there is a heavy processing load on the server. Under heavy load, the [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] service instance will wait until enough system resources are free before it begins a data refresh.  
  
 The workbook must be checked in when the refresh operation is finished. The workbook will be saved with the refreshed data at that time. If the file is checked out, data refresh is skipped until the next scheduled time.  
  
 If you see a status message that is unexpected (for example, a refresh operation failed or was cancelled), you can investigate the problem by checking permissions and server availability.  
  
 Review the PowerPivot data refresh troubleshooting page on the TechNet WIKI for help on resolving data refresh problems. For more information see [Troubleshooting PowerPivot Data Refresh](https://go.microsoft.com/fwlink/?LinkId=251594).  
  
> [!NOTE]  
>  SharePoint administrators can help you troubleshoot data refresh problems by viewing the consolidated data refresh reports in the PowerPivot Management Dashboard in Central Administration. For more information, see [PowerPivot Management Dashboard and Usage Data](power-pivot-sharepoint/power-pivot-management-dashboard-and-usage-data.md).  
  
## See Also  
 [PowerPivot Data Refresh with SharePoint 2010](powerpivot-data-refresh-with-sharepoint-2010.md)   
 [View Data Refresh History &#40;PowerPivot for SharePoint&#41;](power-pivot-sharepoint/view-data-refresh-history-power-pivot-for-sharepoint.md)   
 [Configure Stored Credentials for PowerPivot Data Refresh &#40;PowerPivot for SharePoint&#41;](configure-stored-credentials-data-refresh-powerpivot-sharepoint.md)  
  
  

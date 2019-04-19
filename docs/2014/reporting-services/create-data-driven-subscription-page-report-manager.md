---
title: "Create Data-driven Subscription Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 814b4653-572a-48c7-847f-b310ba0f3046
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Create Data-driven Subscription Page (Report Manager)
  Use the Create Data-driven Subscription pages to build or modify a subscription that queries a subscriber database for subscription information each time the subscription runs. Data-driven subscriptions use query results to determine the recipients of the subscription, delivery settings, and report parameter values. At run time, the report server runs a query to get values used for subscription settings. You can use the Create Data-driven Subscription pages to define the query and assign query values to subscription settings. The values and options that you specify for a data-driven subscription are divided among several pages, similar to a wizard. There are seven pages in all.  
  
 To create a data-driven subscription, you must know how to write a query or command that gets the data for the subscription. You must also have a data store that contains the subscriber data (for example, subscriber names and e-mail addresses) to use for the subscription.  
  
 This page is available to users with advanced permissions. If you are using default security, data-driven subscriptions cannot be used for reports located in a My Reports folder.  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the Data-driven Subscription page  
  
1.  Open Report Manager, and locate the report for which you want to create a data-driven subscription.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the **General** properties page for the report.  
  
4.  Select the **Subscriptions** tab, and then click **New Data-driven Subscription**.  
  
    > [!NOTE]  
    >  The report data source must use stored credentials in order for this button to be enabled.  
  
## Start a Subscription (Page 1)  
 **Description**  
 Provide a description for the subscription. The description appears in subscription lists in **My Subscriptions** and in the **Subscriptions** tab of the report.  
  
 **Specify how recipients are notified**  
 Select the delivery extension to use to distribute the report. Only one delivery extension can be used for each subscription. The following options are available:  
  
-   Select **Report Server File Share** to deliver reports to a file share. The report will be delivered as a static file, disconnected from the report server. For more information, see [File Share Delivery in Reporting Services](subscriptions/file-share-delivery-in-reporting-services.md).  
  
-   Select **Report Server E-Mail** to deliver reports to an e-mail inbox. For more information, see [E-Mail Delivery in Reporting Services](subscriptions/e-mail-delivery-in-reporting-services.md).  
  
-   Select **Null Delivery Provider** to deliver reports to the report server database. This option creates report snapshots. Choose this option when you want to preload the report server with user-specific or parameterized report snapshots on a specific schedule. For more information, see [Caching Reports &#40;SSRS&#41;](report-server/caching-reports-ssrs.md).  
  
 **Specify a data source that contains recipient information**  
 Specify how the data source connection is defined. You can choose a shared data source if you have one that contains the connection information you need. You can also specify connection information directly in this subscription.  
  
 The data source provides subscriber data. This data might consist of employee names, employee IDs, e-mail addresses, and preferences for export formats (such as HTML or PDF). If you are using the report server e-mail delivery extension, the data source should contain e-mail addresses.  
  
## Specify a Connection (Page 2)  
 If you specified a shared data source, use this page to select the shared data source item. You can use the tree control to navigate to and select the item. If you are defining a connection for this subscription, use this page to specify the following options.  
  
 **Connection Type**  
 Select which data processing extension to use with the data source.  
  
 **Connection String**  
 Type a connection string to use to connect to the data source.  
  
 **Connect Using**  
 Type the credentials to use when connecting to the data source. The credentials are stored as encrypted values in the report server database.  
  
 If the data source uses Windows Authentication, select **Use as Windows credentials** when specifying the connection.  
  
 If you are using a data source that does not authenticate user connections (for example, if the data source is an XML file), select Credentials are not required. This option requires that you previously configured the unattended execution account. For more information, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).  
  
## Specify a Query (Page 3)  
 Use this page to enter the query that retrieves subscriber data. For best result, run the query in SQL Server Management Studio first, before using it in the data-driven subscription. You can then examine the results to verify that it contains the information you require. Important points to recognize about the query results are:  
  
-   Columns in the result set determine the values that you can specify for delivery options and report parameters. For example, if you are creating a data-driven subscription for e-mail delivery, you should have a column of e-mail addresses.  
  
-   Rows in the result set determine the number of report deliveries that are generated. If you have 10,000 rows, the report server will generate 10,000 notifications and deliveries.  
  
 **Query**  
 Specify a SQL query or a command that retrieves a result set that contains one row for each recipient of the subscription. On subsequent pages, the result set is used to populate data-driven extension settings.  
  
 **Timeout**  
 Specify a query time-out value. This value must be large enough to complete query processing.  
  
 **Validate**  
 Click **Validate** to verify the query. The query must produce valid results before you can continue. If you do not click **Validate**, the query is validated when you click **Next**.  
  
## Set Delivery Options (Page 4)  
 On the fourth page, you specify delivery extension options. The options that appear on the page are derived from the delivery extension. How you specify those options can vary considerably based on how the delivery extension presents them. If the extension has no settings, no options appear on this page.  
  
|Select this|To do this|  
|-----------------|----------------|  
|**Specify a static value**|Use a constant value for the delivery setting. Some delivery extensions provide static values that you can choose from. For example, report server e-mail delivery provides values for **IncludeReport**, **RenderFormat**, **Priority**, and **Include Link**.|  
|**Get the value from the database**|Use a value from the result set. The columns of the result set can be used to provide subscriber data and report parameter values.|  
|**No value**|Omit the setting from the subscription.|  
  
#### Set Delivery Options for File Share Delivery  
 The file share delivery extension is often used because it requires no prior configuration. If you are using the file share delivery extension, the following table describes the options you can set:  
  
 **File name**  
 Specifies a file name for the report. The file share delivery extension delivers a report as a static application file to a shared folder. In most cases, you should use a value from the database to create the file name. Depending on how you set the write mode, using a static value will cause each new delivery to overwrite the previous delivery.  
  
 **Path**  
 Specify a shared folder that is accessible over a network connection. To verify that folder is accessible, click **Run** on the Start menu and enter the folder path in this format: \\\\<computername\>\\<sharedfoldername\>.  
  
 **Render format**  
 Specify the output format of the file. The report server can write the file in application formats of the rendering extensions that are installed on the report server.  
  
 **Write mode**  
 Specify whether the report server should replace a file with a newer version, increment it, or drop the delivery if a file of the same name is found.  
  
 **File extension**  
 Specify True to append a file extension that matches the render format you selected.  
  
 **User name**  
 Enter the domain user account that has permission to add files to the shared folder in this format: \<domain>\\<username\>.  
  
 **Password**  
 Enter the password for the account.  
  
## Set Parameters (Page 5)  
 If a report includes parameters, you must specify which parameter values to use with the report. Parameter values can be obtained from the subscriber data source (for example, if you have a regional sales report that is parameterized based on a regional code, you can obtain region information for each employee if that information is stored in the employee database).  
  
|Select this|To do this|  
|-----------------|----------------|  
|**Specify a static value**|Use a constant value for the parameter if you want to use the same parameter for all subscribers. If the parameter is multi-valued, you can choose a value from the list.|  
|**Use Default**|Some reports contain a default value for all or some of the parameters. If the report parameter has a default value, click this checkbox to use it.|  
|**Get the value from the database**|Use a value from the result set. The columns of the result set can be selected as a source of a data value to use with each subscription instance.|  
  
## Specify a Trigger (Page 6)  
 Select an event that initiates subscription processing.  
  
|Select this|To do this|  
|-----------------|----------------|  
|**When the report data is updated on the report server**|If the report is configured to run as a report execution snapshot, you can specify that the subscription runs when the snapshot is refreshed.|  
|**On a schedule created for this subscription**|Run the subscription at a specific date and time.|  
|**On a shared schedule**|Run the subscription using schedule information provided through a shared schedule.|  
  
## Schedule a Subscription (Page 7)  
 If you schedule the subscription, you must specify the frequency with which the report is delivered. The first set of options specifies a category of frequency (hourly, daily, weekly, and so on). The second set of option that appears is based on your initial selection.  
  
 **Hourly**  
 Define a schedule that runs at hourly intervals.  
  
 **Daily**  
 Define a schedule that runs on the days you select at a specific hour and minute. You can specify days in the following ways: Every *\<day>*, Every weekday, and Every *\<number>* day. Choosing one approach voids the others, even if the other days appear to be selected.  
  
 **Weekly**  
 Define a schedule that runs at weekly intervals at a specific hour and minute. The interval can be in complete weeks (for example, every two weeks) or days within a week.  
  
 **Monthly**  
 Define a schedule that runs on a monthly basis. Within a month, you can choose a day based on a pattern (for example, the last Sunday of every month) or specific calendar dates (such as 1 and 15 to indicate the first and fifteenth day of every month). Using commas and hyphens, you can specify multiple days and ranges, for example, 1, 5, 7-12, 21.  
  
 **Once**  
 Define a schedule that runs only once. Use the **Start and end dates** section to specify the day on which to run the schedule. This schedule expires as soon as it is processed.  
  
 **Start and end dates**  
 Specify a start date that determines when the schedule takes effect and an end date that determines when the schedule expires. Schedules expire without notification. After the end date, a schedule no longer runs.  
  
## Saving the Subscription  
 The **Finish** button is enabled when there is enough information for the subscription. Click **Finish** to complete the subscription.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Data-Driven Subscriptions](subscriptions/data-driven-subscriptions.md)   
 [Create a Data-Driven Subscription &#40;SSRS Tutorial&#41;](create-a-data-driven-subscription-ssrs-tutorial.md)   
 [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md)   
 [Subscriptions and Delivery &#40;Reporting Services&#41;](subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  

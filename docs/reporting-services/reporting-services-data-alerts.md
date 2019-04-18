---
title: "Reporting Services Data Alerts | Microsoft Docs"
ms.date: 07/02/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
ms.assetid: 8c234077-b670-45c0-803f-51c5a5e0866e
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016||=sqlallproducts-allversions"
---
# Reporting Services Data Alerts

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016](../includes/ssrs-appliesto-2016.md)] [!INCLUDE [ssrs-appliesto-not-2017](../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../includes/ssrs-previous-versions.md)]

SQL Server Reporting Services data alerts are a data driven alerting solution that helps you be informed about report data that is interesting or important to you, and at a relevant time. By using data alerts you no longer have to seek out information, it comes to you.

Data alert messages are sent by email. Depending on the importance of the information, you can choose to send messages more or less frequently and only when results change. You can specify multiple email recipients and this way keep others informed to enhance efficiency and collaboration.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

##  <a name="AlertingWF"></a> Data Alerts Architecture and Workflow

The following summarizes the key areas of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data alerts:

-   **Define and save data alert definitions**-you run a report, create rules that identify interesting data values, define a recurrence pattern for sending the data alert message, and specify the recipients of the alert message.  
  
-   **Run data alert definitions**-Alerting service processes alert definitions at a scheduled time, retrieves report data, creates data alert instances based on rules in the alert definition.  
  
-   **Deliver data alert messages to recipients**-Alerting service creates an alert instance and sends an alert message to recipients by email.  
  
 In addition, as a data alert owner you can view information about your data alerts and delete and edit your data alert definitions. An alert has only one owner, the person who created it.  
  
 Alerting administrators, users with SharePoint Manage Alerts permission, can manage data alerts at the site level. They can view lists of alerts by each site user and delete alerts.  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data alerts are different from SharePoint alerts. You can define SharePoint alerts on any document type, including reports. SharePoint alerts are sent when the document changes. For example, you add a column to a table in a report. In contrast, data alerts are sent when the data shown in a report satisfied rules in the alert definitions. The rules typically reference the data that displays in a report.  
  
 By creating data alerts on reports, you can monitor changes in report data and send data alert messages by email when report data follow rules that define data of interest to you and others, and at intervals that meet your business needs. You can also run data alerts on demand. If you have SharePoint Create Alert permission, you can create alerts on any report that you have permissions to view. You can create multiple alerts on a report and multiple users can create the same or different alerts on a report. To collaborate with others, you can specify them as the recipients of alert messages in data alert definitions that you create.  
  
 The following diagram shows the workflow of creating and saving a data alert definition, creating a SQL Agent job to begin processing an instance of the data alert, and sending data alert messages that contain the report data that triggered the alert to one or more recipients by email.  
  
 ![Workflow in Reporting Services alerting](../reporting-services/media/rs-alertingworkflow.gif "Workflow in Reporting Services alerting")  
  
### Reports Supported by Data Alerts  
 You can create data alerts on all types of professional reports that are written in the report definition language (RDL) and created in Report Designer or Report Builder. Reports that include data regions such as tables and charts, reports with subreports, and complex reports with multiple parallel column groups and nested data regions. The only requirements are the report includes at least one data region of any type and the report data source is configured to use stored credentials or no credentials. If the report has no data regions, you cannot create an alert on it.  
  
 You cannot create data alerts on reports created with [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)].  
  
 When you install [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] in native mode or SharePoint mode or use the standalone version of Report Builder, you can save reports to a report server, your computer, or a SharePoint library. To create data alerts on reports, the reports must be saved or uploaded to a SharePoint library. This means that you cannot create alerts on reports saved to a report server in native mode or your computer. Also, you cannot create alerts embedded in custom applications.  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports a variety of credential types in reports. You can create data alerts on reports with data source configured to use stored credentials, or no credentials. You cannot create alerts on reports configured to use integrated security credentials or prompt for credentials. The report is run as part of processing the alert definition and the processing fails without credentials. For more information, see the following:  
  
-   [Specify Credential and Connection Information for Report Data Sources](../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)  
  
-   [Roles and Permissions &#40;Reporting Services&#41;](../reporting-services/security/roles-and-permissions-reporting-services.md)  
  
-   [Authentication with the Report Server](../reporting-services/security/authentication-with-the-report-server.md)  
  
### Run Reports  
 The first step in creating a data alert definition is to locate the report you want in the SharePoint library, and then run the report. If a report contains no data when you run it, you cannot create an alert on the report at that time.  
  
 If the report is parameterized, you specify the parameter values to use when you run the report. The parameter values will be saved in the data alert definitions that you create on a report. The values are used when the report is rerun as a step in processing the data alert definition. If you want to change the parameter values you need to rerun the report with those parameter values and create an alert definition on that version of the report.  
  
### Create Data Alert Definitions  
 The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data alerts feature includes the Data Alert Designer, which you use to create data alert definitions.  
  
 To create a data alert definition, you run the report and then open Data Alert Designer from the SharePoint Report Viewer **Actions** menu. The report data feeds for the report are generated and the first 100 rows in the data feed display in a data preview table in Data Alert Designer. All the data feeds from a report are cached as long you are working on the alert definition in Data Alert Designer. The caching enables you to switch quickly between data feeds. When you reopen an alert definition in Data Alert Designer, the data feeds are refreshed.  
  
 Data alert definitions consist of rules and clauses that report data must satisfy to trigger a data alert message, a schedule that defines the frequency to send the alert message and optionally the dates to start and stop sending the alert message, information such the Subject line and a description to include in the alert message, and the recipients of the message. After you create an alert definition, you save it to the SQL Server alerting database.  
  
### Save Data Alert Definitions and Alerting Metadata  
 When you install [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] in SharePoint mode, the SQL Server alerting database is automatically created.  
  
 Data alert definitions and alerting metadata are saved in the alerting database. By default, this database is named ReportingServices\<GUID>_Alerting.  
  
 When you save the data alert definition, alerting creates a SQL Server Agent job for the alert definition. The job includes a job schedule. The schedule is based on the recurrence pattern you define on the alert definition. Running the job initiates the processing of the data alert definition.  
  
### Process Data Alert Definitions  
 When the schedule of the SQL Server Agent job starts the processing of the alert definition, the report is run to refresh the report data feeds. The alerting service reads the data feeds and applies the rules that the data alert definitions specify to the data values. If one or more data values satisfy the rules, a data alert instance is created and a data alert message with the alert results is sent to all recipients by email. The results are rows of report data that satisfied all rules at the time the alert instance was created. To prevent multiple alert messages with the same results, you can specify that messages are sent only when the results change. In this case, an alert instance is created and saved to the alerting database, but no alert message is generated. If an error occurs, the alert instance is also saved to the alerting database and an alert message with the details about the error is sent to recipients. The Diagnostics and Logging section later in this topic has more information about logging and troubleshooting.  
  
### Send Data Alert Messages  
 Data alert message are sent by email.  
  
 The **From** line contains a value provided by the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] email delivery configuration. The **To** line lists the recipients that you specified when you created the alert in Data Alert Designer.  
  
 Besides the email Subject line, which you specify in Data Alert Designer, the data alert message includes the following information:  
  
-   The name of the person who created the data alert definition.  
  
-   If you provided a description in the alert definition, it displays at the top of the email text.  
  
-   The alert results, consisting of the rows in the report data feed that satisfy the rules specified in the alert definition.  
  
-   A link to the report that the alert definition is built upon.  
  
-   The rules in the alert definition.  
  
-   The parameters and values that you used to run the report.  
  
-   The contextual values from report items that are outside of the report data regions.  
  
 If a data alert instance or data alert message cannot be created an error message is sent to all recipients. Instead of the alert results, the message includes an error description.  
  
 For more information, see [Data Alert Messages](../reporting-services/data-alert-messages.md).  
  
##  <a name="InstallAlerting"></a> Install Data Alerts  
 The data alerts feature is available only when [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] is installed in SharePoint mode. When you install [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] in SharePoint mode, setup automatically creates the alerting database that stores data alert definitions and alerting metadata, and two SharePoint pages for managing alerts and adds Data Alert Designer to the SharePoint site. There are no special steps to perform or options to set for alerting during installation.  
  
 If you want to learn more about installing [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] in SharePoint mode, including the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] shared service that is new in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] service application that you must create and configure before you can use [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features, see [Install Reporting Services SharePoint Mode for SharePoint 2010](https://msdn.microsoft.com/47efa72e-1735-4387-8485-f8994fb08c8c) in MSDN library.  
  
 As the diagram earlier in the topic shows, data alerts use SQL Server Agent jobs. To create the jobs, SQL Server Agent must be running. You might have configured SQL Server Agent to start automatically when you installed [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. If not, you can start SQL Server Agent manually. For more information, see [Configure SQL Server Agent](../ssms/agent/configure-sql-server-agent.md) and [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).  
  
 You can use the **Provision Subscriptions and Alerts** page in SharePoint Central Administration to find out whether SQL Server Agent is running and create and download customized [!INCLUDE[tsql](../includes/tsql-md.md)] scripts that you then run to grant permissions to SQL Server Agent. If can also generate the [!INCLUDE[tsql](../includes/tsql-md.md)] scripts by using PowerShell. For more information, see [Provision Subscriptions and Alerts for SSRS Service Applications](../reporting-services/install-windows/provision-subscriptions-and-alerts-for-ssrs-service-applications.md).  
  
##  <a name="ConfigAlert"></a> Configure Data Alerts  
 Starting in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] the settings for [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features, including data alerts, are distributed between the report server configuration file (rsreportserver.config) and a SharePoint configuration database whenever you install [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] in SharePoint mode. When you create the service application as a step in installing and configuring [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], the SharePoint configuration database is automatically created. For more information, see [RsReportServer.config Configuration File](../reporting-services/report-server/rsreportserver-config-configuration-file.md) and [Reporting Services Configuration Files](../reporting-services/report-server/reporting-services-configuration-files.md).  
  
 The settings for [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data alerts include the intervals for cleaning up alerting data and metadata and the number of retries when sending data alert messages by email. You can update the configuration file and the configuration database to use different values for data alert settings.  
  
 You update the report server configuration file directly. You update the SharePoint configuration database by using Windows PowerShell cmdlets.  
  
 The following table lists the configuration elements for data alerts, their default values, descriptions, and locations.  
  
|Setting|Default Value|Description|Location|  
|-------------|-------------------|-----------------|--------------|  
|AlertingCleanupCycleMinutes|20|Number of minutes between starts of the cleanup cycle.|Report Server Configuration File|  
|AlertingExecutionLogCleanupMinutes|10080|Number of minutes to keep execution log entries.|Report Server Configuration File|  
|AlertingDataCleanupMinutes|360|Number of minutes to keep temporary data.|Report Server Configuration File|  
|AlertingMaxDataRetentionDays|180|The number of days until alert execution metadata, alert instances, and execution results is deleted.|Report Server Configuration File|  
|MaxRetries|3|Number of times to retry processing of data alerts.|Service Configuration Database|  
|SecondsBeforeRetry|900|Number of seconds to wait before each retry.|Service Configuration Database|  
  
 By default, the MaxRetries and SecondsBeforeRetry settings apply to all events that data alerts fire. If you want more granular control of retries and retry delays, you can add elements for any and all event handlers that specify different MaxRetries and SecondsBeforeRetry values.  
  
### Event Handlers and Retry  
 The event handlers are:  
  
|Event Handler|Description|  
|-------------------|-----------------|  
|FireAlert|You click **Run**  in Data Alert Manager to initiate immediate processing of an alert definition.|  
|FireSchedule|SQL Server Agent launches the job schedule for an alert definition.|  
|CreateSchedule|You create a data alert definition and a SQL Server Agent job schedule is created based on the frequency interval specified in the alert definition.|  
|UpdateSchedule|You update the frequency interval of the data alert definition and the SQL Server Agent job schedule is updated.|  
|DeleteSchedule|You delete the data alert definition and   its SQL Server Agent job is deleted.|  
|GenerateAlert|The alerting runtime processes the report data feed, applies the rules specified in the data alert definition, determines whether to create an instance of the data alert, and if needed creates an instance of the data alert.|  
|DeliverAlert|The runtime creates the data alert message and sends it to all recipients by email.|  
  
 The following table summarizes the event handlers and when retry will fire:  
  
|Error Category|<|\<|Event type||>|>|>|  
|--------------------|--------|--------|----------------|-|--------|--------|--------|  
||**FireAlert**|**FireSchedule**|**CreateSchedule**|**UpdateSchedule**|**DeleteSchedule**|**GenerateAlert**|**DeliverAlert**|  
|Out of memory|X|X|X|X|X|X|X|  
|Thread abort|X|X|X|X|X|X|X|  
|SQL Agent is not running|X||X|X|X|||  
|Transient. Mostly due to connections problems, timeouts, and locks.|X|X|X|X|X|X|X|  
|IOException|||||||X|  
|WebException|||||||X|  
|SocketException|||||||X|  
|SMTPException **(\*)**|||||||X|  
  
 **(\*)** SMTP errors that will trigger a retry:  
  
-   SmtpStatusCode.ServiceNotAvailable  
  
-   SmtpStatusCode.MailboxBusy  
  
-   SmtpStatusCode.MailboxUnavailable  
  
###  <a name="bkmk_disablealerts"></a> Disable Data Alerts  
 If you want to disable the data alert feature, you update the Service section of the configuration file. The following code shows Service section of the configuration file.  
  
 `<Service>`  
  
 `<IsSchedulingService>True</IsSchedulingService>`  
  
 `<IsNotificationService>True</IsNotificationService>`  
  
 `<IsEventService>True</IsEventService>`  
  
 `<IsAlertingService>True</IsAlertingService>`  
  
 `...`  
  
 `</Service>`  
  
 To disable alerting, change True to False in `<IsAlertingService>True</IsAlertingService>`.  
  
##  <a name="Permissions"></a> Permissions for Data Alerts  
 Before you can create data alerts on reports, you must have permission to run the report and create alerts on the SharePoint site. To learn more about report permissions, see the following.  
  
-   [Generating Data Feeds from Reports &#40;Report Builder and SSRS&#41;](../reporting-services/report-builder/generating-data-feeds-from-reports-report-builder-and-ssrs.md)  
  
-   [Set Permissions for Report Server Items on a SharePoint Site &#40;Reporting Services in SharePoint Integrated Mode&#41;](../reporting-services/security/set-permissions-for-report-server-items-on-a-sharepoint-site.md)  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data alerts support two permission levels: information worker and alerting administrator. The following table lists the related SharePoint permissions and user tasks.  
  
|User Type|SharePoint Permission|Task Description|  
|---------------|---------------------------|----------------------|  
|Information worker|View Items<br /><br /> Create Alerts|View items such as reports and create data alerts on the reports. Edit and delete alerts.|  
|Alerting administrator|Manage Alerts|View a list of all data alerts saved on the SharePoint site and delete alerts.|  
  
##  <a name="DiagnosticsLogging"></a> Diagnostics and Logging  
 Data alerts provides a number of ways to help information workers and administrators keep track of alerts and understand why alerts failed and help administrators make use of logs to learn which alert messages were sent to whom, number of alert instances sent, and so forth.  
  
### Data Alert Manager  
 Data Alert Manager lists alert definitions and error information that help information workers and alerting administrators understand why the failure occurred. Some common reasons for failure include:  
  
-   The report data feed changed and columns that are used in the data alert definition rules are no longer included in the data feed.  
  
-   Permission to view the report was revoked.  
  
-   The data type in the underlying data source changed and the alert definition is no longer valid.  
  
### Logs  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] provides a number of logs that can help you learn more the reports that are run when processing data alert definitions, the data alert instances that are created and so forth. Three logs are particularly useful: the alerting execution log, the report server execution log, and the report server trace log.  
  
 For information about other [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] logs, see [Reporting Services Log Files and Sources](../reporting-services/report-server/reporting-services-log-files-and-sources.md).  
  
#### Alerting Execution Log  
 The alerting runtime service writes entries in the ExecutionLogView table in the alerting database. You can query the table or run the following stored procedures to get richer diagnostic information about the data alerts saved to the alerting database.  
  
-   ReadAlertData  
  
-   ReadAlertHistory  
  
-   ReadAlertInstances  
  
-   ReadEventHistory  
  
-   ReadFeedPollHistory  
  
-   ReadFeedPools  
  
-   ReadPollData  
  
-   ReadSentAlerts  
  
 You can use SQL Agent to run the stored procedure on a schedule. For more information, see [SQL Server Agent](../ssms/agent/sql-server-agent.md).  
  
#### Report Server Execution Log  
 Reports are run to generate the data feeds that data alert definitions are built upon. The report server execution log in the report server database captures information each time the report is run. You can query the ExecutionLog2 view in the database for detailed information. For more information, see [Report Server ExecutionLog and the ExecutionLog3 View](../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  
  
#### Report Server Trace Log  
 The report server trace log contains highly detailed information for report server service operations, including operations performed by the report server Web service and background processing. Trace log information might be useful if you are debugging an application that includes a report server, or investigating a specific problem that was written to the event log or execution log. For more information, see [Report Server Service Trace Log](../reporting-services/report-server/report-server-service-trace-log.md).  
  
##  <a name="PerformanceCounters"></a> Performance Counters  
 Data alerts provide their own performance counters. All but one performance counter is related to an event that is part of the alerting runtime service. The performance counter related to the event queue tells the length of the queue of all active events.  
  
|Event or Event Queue|Performance Counter|  
|--------------------------|-------------------------|  
|ALERTINGQUEUESIZE|Alerting: event queue length|  
|FireAlert|Alerting: events processed - FireAlert|  
|FireSchedule|Alerting: events processed - FireSchedule|  
|CreateSchedule|Alerting: events processed - CreateSchedule|  
|UpdateSchedule|Alerting: events processed - UpdateSchedule|  
|DeleteSchedule|Alerting: events processed - DeleteSchedule|  
|GenerateAlert|Alerting: events processed - GenerateAlert|  
|DeliverAlert|Alerting: events processed - DeliverAlert|  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] provides performance counters for other [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features. For more information, see [Performance Counters for the ReportServer:Service  and ReportServerSharePoint:Service Performance Objects](../reporting-services/report-server/performance-counters-reportserver-service-performance-objects.md), [Performance Counters for the MSRS 2011 Web Service and MSRS 2011 Windows Service Performance Objects &#40;Native Mode&#41;](../reporting-services/report-server/performance-counters-msrs-2011-web-service-performance-objects.md), and [Performance Counters for the MSRS 2011 Web Service SharePoint Mode and MSRS 2011 Windows Service SharePoint Mode Performance Objects &#40;SharePoint Mode&#41;](../reporting-services/report-server/performance-counters-msrs-2011-sharepoint-mode-performance-objects.md).  
  
##  <a name="SupportForSSL"></a> Support for SSL  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] can use the HTTP SSL (Secure Sockets Layer) service to establish encrypted connections to a report server or SharePoint site.  
  
 The alerting runtime service and data alerts user interface support SSL and works similarly whether you use SSL or HTTP; however, there are some subtle differences. When the data alert definition is created using and SSL connection, the URL that links back to the SharePoint library from the data alert message also uses SSL. You can identify the SSL connection because it uses HTTPS instead of HTTP in its URL. Likewise, if the data alert definition was created using an HTTP connection, the link back to the SharePoint site uses HTTP. Whether the alert definition was created using SSL or HTTP, the experience for users and alerting administrators are identical when using Data Alert Designer or Data Alert Manager. If the protocol (HTTP or SSL) should change between the time that the alert definition was created and then updated and resaved, the original protocol is kept and used in link URLs.  
  
 If you create a data alert on a SharePoint site that is configured to use SSL and then remove the SSL requirement the alert continues to work on the site. If the site is deleted, the default zone site is used instead.  
  
##  <a name="UserInterface"></a> Data Alert User Interface  
 Data alerts provide SharePoint pages for managing alerts and a designer for creating and editing data alert definitions.  
  
-   **Data Alert Designer** in which you create or edit data alert definitions. For more information, see [Data Alert Designer](../reporting-services/data-alert-designer.md), [Create a Data Alert in Data Alert Designer](../reporting-services/create-a-data-alert-in-data-alert-designer.md) and [Edit a Data Alert in Alert Designer](../reporting-services/edit-a-data-alert-in-alert-designer.md).  
  
-   **Data Alert Manager** in which you view lists of data alerts, delete alerts, and open alerts for editing. Data Alert Manager comes in two versions: one for users to manage the alerts they created, and one for administrators to manage alerts that belong to site users.  
  
     For more information about managing data alerts that you created, see [Data Alert Manager for SharePoint Users](../reporting-services/data-alert-manager-for-sharepoint-users.md) and [Manage My Data Alerts in Data Alert Manager](../reporting-services/manage-my-data-alerts-in-data-alert-manager.md).  
  
     For more information about managing all data alerts on a site, see [Data Alert Manager for Alerting Administrators](../reporting-services/data-alert-manager-for-alerting-administrators.md) and [Manage All Data Alerts on a SharePoint Site in Data Alert Manager](../reporting-services/manage-all-data-alerts-on-a-sharepoint-site-in-data-alert-manager.md).  
  
-   **Provision Subscriptions and Data Alerts** in which you find out whether Reporting Services can use SQL Server Agent for data alerts and download scripts that allow access to SQL Server Agent. For more information, see [Provision Subscriptions and Alerts for SSRS Service Applications](../reporting-services/install-windows/provision-subscriptions-and-alerts-for-ssrs-service-applications.md).  
  
##  <a name="Globalization"></a> Globalization of Data Alerts  
 Certain script such as Arabic and Hebrew are written right to left. Data alerts support right-to-left scripts as well as left-to-right scripts. Data alerts detect culture and alter the appearance and behavior of the user interface and the layout of data alert messages accordingly. The culture is derived from the regional setting of the operating system on the user's computer. The culture is saved each time you update and then resave the data alert definition.  
  
 Whether data satisfies the rules in the alert definition can be affected by the culture in the alert definition. String comparisons are most commonly affected by culture specific rules.  
  
 Determining whether report data satisfies the rules in the alert definition can be affected by the culture in the alert definition. This most commonly occurs in of strings. For example, in an alert definition with the German culture, a rule that compares the English letter "o" and the German letter "ö" would not be satisfied. In the same alert definition using the English culture the rule would be satisfied.  
  
 Data formatting is also based the culture of the alert definition. For example, if the culture uses a period as the decimal symbol, then the value displays as 45.67; whereas a culture that uses a comma as the decimal symbol, displays 45,67.  
  
 Depending on which data alert user interface you use, the support for right-to-left varies. Data Alert Designer supports right-to-left script in text boxes, but the layout of the designer is not right to left. Its layout is left to right like other tools. If an alert definition, created with right-to-left text orientation, and then edited in a left-to-right environment, the right-to-left text orientation is preserved when you save the alert definition. Data Alert Manager behaves the same as a SharePoint page. Its layout is right-to left, just like other SharePoint pages. Data alert messages that are based on right-to-left data alert definitions, display message text right to left and the message layout is left to right.  
  
##  <a name="HowTo"></a> Related Tasks  
  
-   [Save a Report to a SharePoint Library &#40;Report Builder&#41;](../reporting-services/report-builder/save-a-report-to-a-sharepoint-library-report-builder.md)  
  
-   [Create a Data Alert in Data Alert Designer](../reporting-services/create-a-data-alert-in-data-alert-designer.md)  
  
-   [Edit a Data Alert in Alert Designer](../reporting-services/edit-a-data-alert-in-alert-designer.md)  
  
-   [Manage My Data Alerts in Data Alert Manager](../reporting-services/manage-my-data-alerts-in-data-alert-manager.md)  
  
-   [Manage All Data Alerts on a SharePoint Site in Data Alert Manager](../reporting-services/manage-all-data-alerts-on-a-sharepoint-site-in-data-alert-manager.md)  
  
-   [Grant Permissions to Users and Alerting Administrators](../reporting-services/grant-permissions-to-users-and-alerting-administrators.md)  
  
## See Also

[Data Alert Designer](../reporting-services/data-alert-designer.md)   
[Data Alert Manager for Alerting Administrators](../reporting-services/data-alert-manager-for-alerting-administrators.md)   
[Data Alert Manager for SharePoint Users](../reporting-services/data-alert-manager-for-sharepoint-users.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)

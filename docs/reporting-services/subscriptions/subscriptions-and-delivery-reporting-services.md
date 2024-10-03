---
title: "Subscriptions and delivery (Reporting Services)"
description: In this article, learn about Reporting Services subscriptions, which delivers a report at a specific time or in response to an event.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "subscriptions [Reporting Services], report distribution"
  - "reports [Reporting Services], distributing"
  - "distributing reports [Reporting Services]"
  - "published reports [Reporting Services], distributing"
  - "sending reports"
  - "sharing reports"
  - "delivering reports [Reporting Services]"
  - "distributing reports [Reporting Services], subscriptions"
  - "subscriptions [Reporting Services], about subscriptions"
  - "subscriptions [Reporting Services]"
---
# Subscriptions and delivery (Reporting Services)
  A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscription is a configuration that delivers a report at a specific time or in response to an event. It also is in the file format that you specify. For example, every Wednesday, save the MonthlySales.rdl report as a Microsoft Word document to a file share. Subscriptions can be used to schedule and automate the delivery of a report and with a specific set of report parameter values.  
  
 You can create multiple subscriptions for a single report to vary the subscription options. For example, you can specify different parameter values to produce three versions of a report. These reports are ones such as a Western region sales report, Eastern region sales, and all sales.  
  
:::image type="content" source="../../reporting-services/subscriptions/media/ssrs-subscription-example-flow.png" alt-text="Diagram of an example SSRS subscription flow.":::
   
 Subscriptions aren't available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).
  
 **In this topic:**  
  
-   [Subscription and delivery scenarios](#bkmk_subscription_scenarios)  
  
-   [Standard and data-driven subscriptions](#bkmk_standard_and_datadriven)  
  
-   [Subscription requirements](#bkmk_subscription_requirements)  
  
-   [Delivery extensions](#bkmk_delivery_extensions)  
  
-   [Parts of a subscription](#bkmk_parts_of_subscription)  
  
-   [How subscriptions are processed](#bkmk_subscription_processing)  
  
-   [Programmatic control of subscriptions](#bkmk_code)  
  
 **Topics in this section:**  
  
-   [E-Mail delivery in Reporting Services](../../reporting-services/subscriptions/e-mail-delivery-in-reporting-services.md) Describes report server e-mail delivery operation and configuration.  
  
-   [Create and manage subscriptions for native mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md) Detailed steps for creating subscriptions with a Native mode report server.  
  
-   [Create and manage subscriptions for SharePoint mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-sharepoint-mode-report-servers.md) Detailed steps for creating subscriptions with a SharePoint mode report server.  
  
-   [File share delivery in Reporting Services](../../reporting-services/subscriptions/file-share-delivery-in-reporting-services.md) Describes report server file share delivery operation and configuration.  
  
-   [Disable or pause report and subscription processing](../../reporting-services/subscriptions/disable-or-pause-report-and-subscription-processing.md).  
  
-   [SharePoint library delivery in Reporting Services](../../reporting-services/subscriptions/sharepoint-library-delivery-in-reporting-services.md) Describes subscription delivery to a SharePoint library.  
  
-   [Data-driven subscriptions](../../reporting-services/subscriptions/data-driven-subscriptions.md) Provides information about how to use data-driven subscriptions to customize report output at run time.  
  
-   [Monitor Reporting Services subscriptions](../../reporting-services/subscriptions/monitor-reporting-services-subscriptions.md)  
  
-   [Use PowerShell to change and list Reporting Services subscription owners and run a subscription](../../reporting-services/subscriptions/manage-subscription-owners-and-run-subscription-powershell.md)  
  
##  <a name="bkmk_subscription_scenarios"></a> Subscription and delivery scenarios  
 For each subscription, the delivery extension you choose determines the delivery options you can configure. A delivery extension is a module that supports some manner of distribution. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes several delivery extensions and delivery extension might be available through third-party vendors.  
  
 If you're a developer, you can create custom delivery extensions to support other scenarios. For more information, see [Implementing a delivery extension](../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md).  
  
 The following table describes the common [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscription scenarios.  
  
|Scenario|Description|  
|--------------|-----------------|  
|E-mail Reports|E-mail reports to individual users and groups. Create a subscription and specify a group alias or e-mail alias to receive a report that you want to distribute. You can have [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] determine the subscription data at run time. If you want to send the same report to a group that has a changing list of members, you can use a query to derive the subscription list at run time.|  
|View Reports off-line|Users can select one of the following formats for subscription output:<br /><br /> -   XML file with report data<br />-   CSV (comma delimited)<br />-   PDF<br />-   MHTML (web archive)<br />-   Microsoft Excel<br />-   TIFF file<br />-   Microsoft Word<br /><br /> Reports that you want to archive can be sent directly to a shared folder that you back up on a nightly schedule. Large reports that take too long to load in a browser can be sent to a shared folder in a format that can be viewed in a desktop application.|  
|Preload cache|If you have multiple instances of a parameterized report or a large number of report users who view reports, you can preload reports in the cache. Preloading reduces processing time required to display the report.|  
|Data-driven reports|Use data-driven subscriptions to customize report output, delivery options, and report parameter settings at run time. The subscription uses a query to get input values from a data source at run time. You can use data-driven subscriptions to perform a mail-merge operation that sends a report to a list of subscribers that is determined at the time the subscription is processed.|  
  
##  <a name="bkmk_standard_and_datadriven"></a> Standard and data-driven subscriptions  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports two kinds of subscriptions: **standard** and **data-driven**. Individual users create and manage standard subscriptions. A standard subscription consists of static values that can't be varied during subscription processing. For each standard subscription, there's exactly one set of report presentation options, delivery options, and report parameters.  
  
 Data-driven subscriptions get subscription information at run time by querying an external data source that provides values used to specify a recipient, report parameters, or application format. You might use data-driven subscriptions if you have a large recipient list or if you want to vary report output for each recipient. To use data-driven subscriptions, you must have expertise in building queries and an understanding of how parameters are used. Report server administrators typically create and manage these subscriptions. For more information, see the following articles:  
  
-   [Data-driven subscriptions](../../reporting-services/subscriptions/data-driven-subscriptions.md)  
  
-   [Create a data-driven subscription &#40;SSRS tutorial&#41;](../../reporting-services/create-a-data-driven-subscription-ssrs-tutorial.md)  
  
##  <a name="bkmk_subscription_requirements"></a> Subscription requirements  
 Before you can create a subscription to a report, the following prerequisites must be met:  
  
|Requirement|Description|  
|-----------------|-----------------|  
|Permissions|You must have access to the report. Before you can subscribe to a report, you must have permission to view it.<br /><br /> For Native mode report servers, the following role assignments affect subscriptions:<br /><br /> -   The "Manage individual subscriptions" task allows users to create, modify, and delete subscriptions for a specific report. In the predefined roles, this task is part of Browser and Report Builder roles. Role assignments that include this task allow a user to manage only those subscriptions that they create.<br />-   The "Manage all subscriptions" task allows users to access and modify all subscriptions. This task is required to create data-driven subscriptions. In predefined roles, only the Content Manager role includes this task.|  
|Stored credentials|To create a subscription, the report must use stored credentials or no credentials to retrieve data at run time. You can't subscribe to a report that is configured to use the impersonated or delegated credentials of the current user to connect to an external data source. The stored credentials can be a Windows account or a database user account. For more information, see [Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)<br /><br /> You must have permission to view the report and create individual subscriptions. **Scheduled Events and Report Delivery** must be enabled on the report server. For more information, see [Create and manage subscriptions for native mode report servers](./create-and-manage-subscriptions-for-native-mode-report-servers.md).|  
|User dependent values in a report|For standard subscriptions only, you can create subscriptions to reports that incorporate user account information in a filter or as text that appears on the report. In the report, the user account name is specified through a **User!UserID** expression that resolves to the current user. When you create a subscription, the user who creates the subscription is the considered the current user.|  
|No model item security|You can't subscribe to a Report Builder report that uses a model as a data source if the model contains model item security settings. Only reports that use model item security are included in this restriction.|  
|Parameter values|If the report uses parameters, a parameter value must be specified with the report itself, or in the subscription you define. If you define default values in the report, you can set the parameter value to use the default.|  
  
##  <a name="bkmk_delivery_extensions"></a> Delivery extensions  
 Subscriptions are processed on the report server and are distributed through delivery extensions that are deployed on the server. By default, you can create subscriptions that send reports to a shared folder or to an e-mail address. If the report server is configured for SharePoint integrated mode, you can also send a report to a SharePoint library.  
  
 When a user creates a subscription, they can choose one of the available delivery extensions to determine how the report is delivered. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes the following delivery extensions.  
  
|Delivery Extension|Description|  
|------------------------|-----------------|  
|Windows File Share|Delivers a report as a static application file to a shared folder that is accessible on the network.|  
|E-mail|Delivers a notification or a report as an e-mail attachment or URL link.|  
|SharePoint library|Delivers a report as a static application file to a SharePoint library that is accessible from a SharePoint site. The site must be integrated with a report server that runs in SharePoint integrated mode.|  
|Null|The null delivery provider is a highly specialized delivery extension that is used to preload a cache with ready-to-view parameterized reports This method isn't available to users in individual subscriptions. Null delivery is used by administrators in data-driven subscriptions to improve report server performance by preloading the cache.|  
  
> [!NOTE]  
>  Report delivery is an extensible part of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] architecture. Third-party vendors can create custom delivery extensions to route reports to different locations or devices. For more information about custom delivery extensions, see [Implementing a delivery extension](../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md).  
  
##  <a name="bkmk_parts_of_subscription"></a> Parts of a subscription  
 A subscription definition consists of the following parts:  
  
-   A pointer to a report that can run unattended (that is, a report that uses stored credentials or no credentials).  
  
-   A delivery method (for example, e-mail) and settings for the mode of delivery (such as an e-mail address).  
  
-   A rendering extension to present the report in a specific format.  
  
-   Conditions for processing the subscription, which is expressed as an event.  
  
     Usually, the conditions for running a report are time-based. For example, you might want to run a particular report every Tuesday at 3:00 P.M. UTC. However, if the report runs as a snapshot, you can specify that the subscription runs whenever the snapshot is refreshed.  
  
-   Parameters used when running the report.  
  
     Parameters are optional and are specified only for reports that accept parameter values. Because a subscription is typically user-owned, the parameter values that are specified vary from subscription to subscription. For example, sales managers for different divisions use parameters that return data for their division. All parameters must have a value explicitly defined, or have a valid default value.  
  
 Subscription information is stored with individual reports in a report server database. You can't manage subscriptions separately from the report to which they're associated. Subscriptions can't be extended to include descriptions, other custom text, or other elements. Subscriptions can contain only the items listed earlier.  
  
##  <a name="bkmk_subscription_processing"></a> How subscriptions are processed  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a scheduling and delivery processor, which provides functionality for scheduling reports and delivering them to users. The report server responds to events that it monitors on an ongoing basis. When an event occurs that matches the conditions defined for a subscription, the report server reads the subscription to determine how to process and deliver the report. The report server requests the delivery extension that is specified in the subscription. After the delivery extension is running, the report server extracts delivery information from the subscription and passes it to the delivery extension for processing.  
  
 The delivery extension renders the report in the format defined in the subscription and then delivers the report or notification to the specified destination. If a report can't be delivered, an entry is logged to the report server log file. If you want to support retry operations, you can configure the report server to reattempt the delivery if the first attempt fails.  
  
### Process a standard subscription  
 Standard subscriptions produce one instance of a report. The report is delivered to a single shared folder or to the e-mail addresses specified in the subscription. The report layout and data don't vary. If the report uses parameters, a standard subscription is processed with a single value for each parameter in the report.  
  
### Process a data-driven subscription  
 Data-driven subscriptions can produce many report instances that are delivered to multiple destinations. The report layout doesn't vary, but the data in a report can vary if parameter values are passed in from a subscriber result set. Delivery options that affect how the report is rendered and whether the report is attached or linked to the e-mail. These options can also vary from subscriber to subscriber when the values are passed in from the row set.  
  
 Data-driven subscriptions can produce a large number of deliveries. The report server creates a delivery for each row in the row set that is returned from the subscription query.  
  
### Report delivery characteristics  
 Reports that are delivered through standard subscriptions are typically rendered as static reports. These reports are either based on the most recent report execution snapshot, or generated as a static report for completing a delivery. If you choose the **Include Link** option in a subscription to a report that runs on demand, the report server runs the report when you select the hyperlink.  
  
> [!NOTE]  
>  Reports that are delivered through a URL remain connected to the report server and can be updated or deleted between viewings. The delivery options you choose for your subscription determine whether the report is delivered as a URL, embedded within the body of an e-mail message, or sent as an attachment.  
  
 Reports that are delivered through a data-driven subscription might be regenerated while the subscription is being processed. The report server doesn't lock in a specific instance of a report or its dataset to complete a data-driven subscription. If the subscription uses different parameter values for different subscribers, the report server regenerates the report to produce the required result. The underlying data might be updated after the first report copy is created and delivered. When you update the report, users who get reports later in the process might see data that is based on different result set. You can use a report that runs as a snapshot to ensure that the same report instance is delivered to all subscribers. However, if a scheduled update to the snapshot occurs while the subscription is processing, users might still get different data in their reports.  
  
### Trigger subscription processing  
 The report server uses two kinds of events to trigger subscription processing: a time-driven event that is specified in a schedule or a snapshot update event.  
  
 A time-driven trigger uses a report-specific schedule or a shared schedule to specify when a subscription runs. For on-demand and cached reports, schedules are the only trigger option.  
  
 A snapshot update event uses the scheduled update of a report snapshot to trigger a subscription. You can define a subscription that is triggered whenever the report is updated with new data. The subscription is triggered based on report execution properties that are set on the report.  
  
##  <a name="bkmk_code"></a> Programmatic control of subscriptions  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] object model allows you to programmatically audit and control subscriptions and subscriptions processing.  See the following articles for examples and getting started:  
  
-   [Use PowerShell to change and list Reporting Services subscription owners and run a subscription](../../reporting-services/subscriptions/manage-subscription-owners-and-run-subscription-powershell.md)  
  
-   For examples of how to use PowerShell to enable and disable subscriptions, see [Disable or pause report and subscription processing](../../reporting-services/subscriptions/disable-or-pause-report-and-subscription-processing.md).  
  
-   <xref:ReportService2010.ReportingService2010.ChangeSubscriptionOwner%2A>  
  
-   For an example PowerShell script to list all [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions that are configured to use the **File share account** see [Subscription settings and a file share account &#40;Configuration Manager&#41;](../../reporting-services/install-windows/subscription-settings-and-a-file-share-account-configuration-manager.md).  
  
## Related content

- [Create a data-driven subscription &#40;SSRS tutorial&#41;](../../reporting-services/create-a-data-driven-subscription-ssrs-tutorial.md)
- [Schedules](../../reporting-services/subscriptions/schedules.md)
- [Reporting Services report server &#40;native mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)
- [Monitor Reporting Services subscriptions](../../reporting-services/subscriptions/monitor-reporting-services-subscriptions.md)

---
title: "Monitor Reporting Services subscriptions"
description: Learn to use the UI, PowerShell, or log files to track Reporting Services subscriptions. Monitoring options depend on the report server mode you're running.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/12/2019
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "subscriptions [Reporting Services], inactive"
  - "subscriptions [Reporting Services], status"
  - "monitoring [Reporting Services], subscriptions"
  - "status information [Reporting Services]"
  - "inactive subscriptions [Reporting Services]"
---
# Monitor Reporting Services subscriptions
  You can monitor [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions from the user interface, Windows PowerShell, or log files. The options available to you for monitoring depend on what mode of report server you're running.  
  
**[!INCLUDE[applies](../../includes/applies-md.md)]**

:::row:::
    :::column:::
        [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode  
    :::column-end:::
    :::column:::
        [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode  
    :::column-end:::
:::row-end:::

 **In this article:**  
  
-   [Native mode user interface](#bkmk_native_mode)  
  
-   [SharePoint mode](#bkmk_sharepoint_mode)  
  
-   [Use PowerShell to monitor subscriptions](#bkmk_use_powershell)  
  
-   [Manage inactive subscriptions](#bkmk_manage_inactive)  
  
##  <a name="bkmk_native_mode"></a> Native mode user interface  
 Individual [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] users can monitor the status of a subscription by using the **My Subscriptions** page or the **Subscriptions** tab in the web portal. Subscription pages include columns that indicate when the subscription was last run and the status of the subscription. Status messages are updated when the subscription is scheduled to process. If the trigger never occurs (for example, a report execution snapshot is never refreshed or a schedule never runs), the status message isn't updated.  
  
 The following table describes the possible values for the **Status** column.  
  
|Status|Description|  
|------------|-----------------|  
|New subscription|Appears when you first create the subscription.|  
|Inactive|Appears when a subscription can't be processed. For more information, see "Managing Inactive Subscriptions" later in this article.|  
|Done: \<*number*> processed of \<*number*> total; \<*number*> errors.|Shows the status of a data-driven subscription execution; this message is from the Scheduling and Delivery Processor.|  
|\<*number*> processed|The number of notifications that the Scheduling and Delivery Processor successfully delivered or is no longer attempting to deliver. When a data-driven delivery completes, the number of processed notifications should equal the total number of generated notifications.|  
|\<*number*> total|The total number of notifications generated for the last delivery for the subscription.|  
|\<*number*> error|The number of notifications that the Scheduling and Delivery Processor couldn't deliver or is no longer attempting to deliver.|  
|Failure sending mail: the transport failed to connect to the server.|Indicates that the report server didn't connect to the mail server; this message is from the e-mail delivery extension.|  
|File \<*filename*> was written to \<path>.|Indicates that the delivery to the file share location was successful; this message is from the file share delivery extension.|  
|An unknown error occurred when writing file.|Indicates that the delivery to the file share location didn't succeed; this message is from the file share delivery extension.|  
|Failure connecting to the destination folder, \<path>. Verify the destination folder or file share exists.|Indicates that the folder you specified couldn't be found; this message is from the file share delivery extension.|  
|The file \<filename> couldn't be written to \<path>. Attempting to retry.|Indicates that the file couldn't be updated with a newer version; this message is from the file share delivery extension.|  
|Failure writing file \<filename>: \<message>|Indicates that the delivery to the file share location didn't succeed; this message is from the file share delivery extension.|  
|\<custom status messages>|Status messages about delivery success and failure, provided by delivery extensions. If you use a third-party or custom delivery extension, other status messages might be provided.|  
  
 Report server administrators can also monitor standard subscriptions that are currently processing. Data-driven subscriptions can't be monitored. For more information, see [Manage a running process](../../reporting-services/subscriptions/manage-a-running-process.md).  
  
 If a subscription can't be delivered (for example, if the mail server is unavailable), the delivery extension retries the delivery. A configuration setting specifies the number of attempts to make. The default value is no retries. In some cases, the report might be processed without data (for example, if the data source is offline), in which case text to that effect is provided in the body of the message.  
  
### Native mode log files  
 If an error occurs during delivery, an entry is made in the report server trace log.  
  
 Report server administrators can review the **ReportServerService_*.log** files to determine subscription delivery status. For e-mail delivery, report server log files include a record of processing and deliveries to specific e-mail accounts. The following path is the default location of the log files:  
  
 ```
 C:\Program Files\Microsoft SQL Server Reporting Services\SSRS\LogFiles
 ```  
  
 The following example is a log filename:  
  
 ```
 ReportServerService__05_21_2019_00_05_07.log
 ```
  
 The following example is a trace log file error message related to subscriptions:  
  
-   library!WindowsService_7!b60!05/20/2019-22:34:36 i INFO: Initializing EnableExecutionLogging to 'True'  as specified in Server system properties.emailextension!WindowsService_7!b60!05/20/2019-22:34:41 ERROR: **Error sending email**. Exception: System.Net.Mail.SmtpException: The SMTP server requires a secure connection or the client wasn't authenticated. The server response was: 5.7.1 Client wasn't authenticated   at System.Net.Mail.MailCommand.CheckResponse(SmtpStatusCode statusCode, String response)  
  
 The log file doesn't include information about whether the report was opened, or whether the delivery succeeded. Successful delivery means that there were no errors generated by the Scheduling and Delivery Processor, and that the report server connected to the mail server. If the e-mail resulted in an undeliverable message error in the user mailbox, that information isn't included in the log file. For more information about log files, see [Reporting Services Log Files and Sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md).  
  
##  <a name="bkmk_sharepoint_mode"></a> SharePoint mode  
 To monitor a subscription in SharePoint mode: the subscription status can be monitored from the **Manage Subscriptions** page.  
  
1.  Browse to the document library that contains the report. 
  
2.  Open the context menu of the report (**...**).  
  
3.  Select the expanded menu option (**...**).  
  
4.  Select **Manage Subscriptions**. 
  
### SharePoint ULS log files  
 Subscription related information is written to the SharePoint ULS log. For more information on configuring [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] events for the ULS log, see [Turn on Reporting Services events for the SharePoint trace log &#40;ULS&#41;](../../reporting-services/report-server/turn-on-reporting-services-events-for-the-sharepoint-trace-log-uls.md).  The following example shows an ULS log entry related to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions.  
  
|Date|Process|Area|Category|Level|Correlation|Message|  
|-|-|-|-|-|-|-|  
|5/21/2019 14:34:06:15|App Pool: a0ba039332294f40bc4a81544afde01d|SQL Server Reporting Services|Report Server Email Extension|Unexpected|(empty)|**Error sending email.** Exception: System.Net.Mail.SmtpException: Mailbox unavailable. The server response was: 5.7.1 Client doesn't have permissions to send as this sender  at System.Net.Mail.DataStopCommand.CheckResponse(SmtpStatusCode statusCode, String serverResponse)  at System.Net.Mail.DataStopCommand.Send(SmtpConnection conn)  at System.Net.Mail.SmtpClient.Send(MailMessage message)  at Microsoft.ReportingServices.EmailDeliveryProvider.EmailProvider.Deliver(Notification notification)|  
  
##  <a name="bkmk_use_powershell"></a> Use PowerShell to monitor subscriptions  
 For example PowerShell scripts you can use to check the status of native mode or SharePoint mode subscriptions, see [Manage Subscription Owners and Run Subscription - PowerShell](../../reporting-services/subscriptions/manage-subscription-owners-and-run-subscription-powershell.md).  
  
##  <a name="bkmk_manage_inactive"></a> Manage inactive subscriptions  
 If a subscription becomes inactive, you should either delete it or reactivate it by resolving the underlying conditions that prevent it from being processed. Subscriptions can become inactive if conditions occur that prevent processing. These conditions include:  
  
-   Removing or uninstalling the delivery extension specified in the subscription.  
  
-   Credential settings change from stored to integrated or prompted values.  
  
-   A parameter name or data type change in the report definition and then republishing a report. If a subscription includes a parameter that is no longer valid, the subscription becomes inactive.  
  
-   The execution mode of a report changes (for example, modifying an on-demand report so that it runs as a report execution snapshot). For more information, see [Set report processing properties](../../reporting-services/report-server/set-report-processing-properties.md).  
  
 A message in the subscription itself indicates an inactive subscription. The message includes information about the cause and what steps you should take to reactivate the subscription.  
  
 When conditions cause the subscription to become inactive, the subscription reflects this fact when the report server runs the subscription. For example, a subscription is scheduled to deliver a report every Friday at 2:00 A.M. and the delivery extension it uses was uninstalled on Monday at 9:00 A.M.. In this case, the subscription doesn't reflect its inactive state until Friday at 2:00 A.M.  
  
## Related content  
 [Create and manage subscriptions for native mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md)   
 [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)  
  
  

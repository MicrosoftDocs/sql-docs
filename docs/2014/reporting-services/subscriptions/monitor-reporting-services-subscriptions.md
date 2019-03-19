---
title: "Monitor Reporting Services Subscriptions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [Reporting Services], inactive"
  - "subscriptions [Reporting Services], status"
  - "monitoring [Reporting Services], subscriptions"
  - "status information [Reporting Services]"
  - "inactive subscriptions [Reporting Services]"
ms.assetid: 054c4a87-60bf-4556-9a8c-8b2d77a534e6
author: markingmyname
ms.author: maghan
manager: kfile
---
# Monitor Reporting Services Subscriptions
  You can monitor [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions from the user interface, Windows PowerShell, or log files. The options available to you for monitoring depend on what mode of report server you are running.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode &#124; [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode|  
  
 **In this topic:**  
  
-   [Native mode user interface](#bkmk_native_mode)  
  
-   [SharePoint Mode](#bkmk_sharepoint_mode)  
  
-   [Use PowerShell to monitor subscriptions](#bkmk_use_powershell)  
  
-   [Managing Inactive Subscriptions](#bkmk_manage_inactive)  
  
##  <a name="bkmk_native_mode"></a> Native mode user interface  
 Individual [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] users can monitor the status of a subscription using the **My Subscriptions** page or the **Subscriptions** tab in Report Manager. Subscription pages include columns that indicate when the subscription was last run and the status of the subscription. Status messages are updated when the subscription is scheduled to process. If the trigger never occurs (for example, a report execution snapshot is never refreshed or a schedule never runs), the status message will not be updated.  
  
 The following table describes the possible values for the **Status** column.  
  
|Status|Description|  
|------------|-----------------|  
|New subscription|Appears when you first create the subscription.|  
|Inactive|Appears when a subscription is cannot be processed. For more information, see "Managing Inactive Subscriptions" later in this topic.|  
|Done: \<*number*> processed of \<*number*> total; \<*number*> errors.|Shows the status of a data-driven subscription execution; this message is from the Scheduling and Delivery Processor.|  
|\<*number*> processed|The number of notifications that the Scheduling and Delivery Processor successfully delivered or is no longer attempting to deliver. When a data-driven delivery completes, the number of processed notifications should equal the total number of generated notifications.|  
|\<*number*> total|The total number of notifications generated for the last delivery for the subscription.|  
|\<*number*> error|The number of notifications that the Scheduling and Delivery Processor could not deliver or is no longer attempting to deliver.|  
|Failure sending mail: the transport failed to connect to the server.|Indicates that the report server did not connect to the mail server; this message is from the e-mail delivery extension.|  
|File \<*filename*> was written to \<path>.|Indicates that the delivery to the file share location was successful; this message is from the file share delivery extension.|  
|An unknown error occurred when writing file.|Indicates that the delivery to the file share location did not succeed; this message is from the file share delivery extension.|  
|Failure connecting to the destination folder, \<path>. Verify the destination folder or file share exists.|Indicates that the folder you specified could not be found; this message is from the file share delivery extension.|  
|The file \<filename> could not be written to \<path>. Attempting to retry.|Indicates that the file could not be updated with a newer version; this message is from the file share delivery extension.|  
|Failure writing file \<filename>: \<message>|Indicates that the delivery to the file share location did not succeed; this message is from the file share delivery extension.|  
|\<custom status messages>|Status messages about delivery success and failure, provided by delivery extensions. If you are using a third-party or custom delivery extension, additional status messages may be provided.|  
  
 Report server administrators can also monitor standard subscriptions that are currently processing. Data-driven subscriptions cannot be monitored. For more information, see [Manage a Running Process](manage-a-running-process.md).  
  
 If a subscription cannot be delivered (for example, if the mail server is unavailable), the delivery extension retries the delivery. A configuration setting specifies the number of attempts to make. The default value is no retries. In some cases, the report might have been processed without data (for example, if the data source is offline), in which case text to that effect is provided in the body of the message.  
  
### Native Mode Log Files  
 If an error occurs during delivery, an entry is made in the report server trace log.  
  
 Report server administrators can review the **reportserverservice_\*.log** files to determine subscription delivery status. For e-mail delivery, report server log files include a record of processing and deliveries to specific e-mail accounts. The following is the default location of the log files:  
  
 `C:\Program Files\Microsoft SQL Server\MSRS11.MSSQLSERVER\Reporting Services\LogFiles`  
  
 The following is an example log filename:  
  
 `ReportServerService__05_21_2014_00_05_07.log`  
  
 The following is a trace log file example error message related to subscriptions:  
  
-   library!WindowsService_7!b60!05/20/2014-22:34:36:: i INFO: Initializing EnableExecutionLogging to 'True'  as specified in Server system properties.emailextension!WindowsService_7!b60!05/20/2014-22:34:41:: e ERROR: **Error sending email**. Exception: System.Net.Mail.SmtpException: The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.1 Client was not authenticated   at System.Net.Mail.MailCommand.CheckResponse(SmtpStatusCode statusCode, String response)  
  
 The log file does not include information about whether the report was opened, or whether the delivery actually succeeded. Successful delivery means that there were no errors generated by the Scheduling and Delivery Processor, and that the report server connected to the mail server. If the e-mail resulted in an undeliverable message error in the user mailbox, that information will not be included in the log file. For more information about log files, see [Reporting Services Log Files and Sources](../report-server/reporting-services-log-files-and-sources.md).  
  
##  <a name="bkmk_sharepoint_mode"></a> SharePoint Mode  
 To monitor a subscription in SharePoint mode: the subscription status can be monitored from the **Manage Subscriptions** page.  
  
1.  browse to the document library that contains the report  
  
2.  Open the context menu of the report (**...**).  
  
3.  Select the expanded menu option (**...**).  
  
4.  Select **Manage Subscriptions**  
  
### SharePoint ULS Log files  
 Subscription related information is written to the SharePoint ULS log. For more information on configuring [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] events for the ULS log, see [Turn on Reporting Services events for the SharePoint trace log &#40;ULS&#41;](../report-server/turn-on-reporting-services-events-for-the-sharepoint-trace-log-uls.md).  The following is an example ULS log entry related to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions.  
  
||||||||  
|-|-|-|-|-|-|-|  
|Date|Process|Area|Category|Level|Correlation|Message|  
|5/21/2014 14:34:06:15|App Pool: a0ba039332294f40bc4a81544afde01d|SQL Server Reporting Services|Report Server Email Extension|Unexpected|(empty)|**Error sending email.** Exception: System.Net.Mail.SmtpException: Mailbox unavailable. The server response was: 5.7.1 Client does not have permissions to send as this sender  at System.Net.Mail.DataStopCommand.CheckResponse(SmtpStatusCode statusCode, String serverResponse)  at System.Net.Mail.DataStopCommand.Send(SmtpConnection conn)  at System.Net.Mail.SmtpClient.Send(MailMessage message)  at Microsoft.ReportingServices.EmailDeliveryProvider.EmailProvider.Deliver(Notification notification)|  
  
##  <a name="bkmk_use_powershell"></a> Use PowerShell to monitor subscriptions  
 For example PowerShell scripts you can use to check the status of Native mode or SharePoint mode subscriptions, see [Use PowerShell to Change and List Reporting Services Subscription Owners and Run a Subscription](manage-subscription-owners-and-run-subscription-powershell.md).  
  
##  <a name="bkmk_manage_inactive"></a> Managing Inactive Subscriptions  
 If a subscription becomes inactive, you should either delete it or reactivate it by resolving the underlying conditions that prevent it from being processed. Subscriptions can become inactive if conditions occur that prevent processing. These conditions include:  
  
-   Removing or uninstalling the delivery extension specified in the subscription.  
  
-   Changing credential settings from stored to integrated or prompted values.  
  
-   Changing a parameter name or data type in the report definition and then republishing a report. If a subscription includes a parameter that is no longer valid, the subscription becomes inactive.  
  
-   Changing the execution mode of a report (for example, modifying an on-demand report so that it runs as a report execution snapshot). For more information, see [Set Report Processing Properties](../report-server/set-report-processing-properties.md).  
  
 An inactive subscription is indicated by a message in the subscription itself. The message includes information about the cause and what steps you should take to reactivate the subscription.  
  
 When conditions cause the subscription to become inactive, the subscription reflects this fact when the report server runs the subscription. If a subscription is scheduled to deliver a report every Friday at 2:00 A.M., and the delivery extension it uses was uninstalled on Monday at 9:00 A.M., the subscription will not reflect its inactive state until Friday at 2:00 A.M.  
  
## See Also  
 [Create and Manage Subscriptions for Native Mode Report Servers](../create-manage-subscriptions-native-mode-report-servers.md)   
 [Subscriptions and Delivery &#40;Reporting Services&#41;](subscriptions-and-delivery-reporting-services.md)  
  
  

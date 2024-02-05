---
title: "Troubleshoot Reporting Services subscriptions and delivery"
description: In this article, diagnose and fix problems found when you work with report subscriptions, schedules, and delivery in SQL Server Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/31/2016
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Troubleshoot Reporting Services subscriptions and delivery
  
    
Use this article to troubleshoot problems that you encounter when working with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report subscriptions, schedules, and delivery.  
## Log information
 
The Subscription page in [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] includes a status of a subscription but if there's a problem with the subscription, the detailed information is in the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] logs. 
:::image type="content" source="../../reporting-services/media/ssrs-tutorial-datadriven-subscription-status-reportmanager.png" alt-text="Screenshot that shows the status.":::


**Trace logs:**
The trace logs are text files written to: `\Program Files\Microsoft SQL Server\MSRS13.MSSQLSERVER\Reporting Services\LogFiles`

The following example is a sample log entry:

```
   subscription WindowsService_10   4c7c    05/24/2016-01:05:06  e ERROR     Failure writing file \\ServerName\SalesReports\so71949.xls : Microsoft.ReportingServices.FileShareDeliveryProvider.FileShareProvider+NetworkErrorException: An impersonation error occurred using the security context of the current user. ---> System.ArgumentException: Value does not fall within the expected range.  05/24/2016
```
For more information on [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] trace logs, see: 
+ [Report server trace log](../../reporting-services/report-server/report-server-service-trace-log.md).
+ [Reporting Services log files](../../reporting-services/report-server/reporting-services-log-files-and-sources.md).

**Execution Log views:**

The execution logs are views in the ReportServer SQL database
For more information on [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)], see [Reporting Services ExecutionLog and ExecutionLog3 views](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  

----------
## Unable to send reports by using e-mail with Windows Server 2003 and POP3  
If you're running an e-mail application with Post Office Protocol version 3 (POP3) on Microsoft Windows Server 2003, you might not be able to send reports by using the local POP3 server. If you configure the report server to send e-mail with the local POP3 Server and create a subscription that sends a report, you might receive the following error message: `Failure sending mail: <error message>`, where \<error message> is replaced with other error message information returned from Collaboration Data Objects (CDO).  
  
### Resolve this problem 
* Set the value of the `SendUsing` element in the **Rsreportserver.config** file to `1`.  
* Clear the value of the `SMTPServer` property so that it's empty. You also need to provide a value for the `SMTPServerPickupDirectory` property.   
    
## Failure sending mail: The server rejected the sender address. The server response was: 454 5.7.3 Client doesn't have permission to submit mail to this server  
This error occurs when security policy settings on the SMTP server allow only authenticated users to submit mail for subsequent delivery. If the SMTP server doesn't accept e-mail submissions from anonymous users, see the system administrator about getting permission to use the server.  

This error can also occur when you specify an Exchange server name as the SMTPServer. To use an Exchange server for e-mail delivery, you must specify the name of the SMTP gateway that is configured for your Exchange server. See your Exchange administrator for this information.  
  
## Subscriptions aren't processing  
Subscriptions can fail under these conditions.   
* The schedule used to trigger the report expired. For subscriptions that trigger off of a report snapshot update, the schedule used to refresh the snapshot might be expired.  
  
* The report server, SQL Server Agent, or the e-mail server application isn't running.  
* The report is undeliverable (for example, it's too large). To determine whether the delivery is failing because the report is too large, save the report to a file and then e-mail it. Be sure to choose the same rendering format you specified in the subscription. If you get a delivery error, use the File Share delivery extension instead of Report Server E-mail.  
* The computer used for file share delivery isn't running or the file share is configured for read-only access.  
* The delivery extension specified in the subscription was uninstalled or disabled.  
* The credential settings changed from stored to integrated or prompted values.  
* The parameter name or data type was changed in the report definition, and the report was republished. If a subscription includes a parameter that is no longer valid, the subscription becomes inactive.  

[!INCLUDE[feedback-qa-stackoverflow-md](../../includes/feedback-qa-stackoverflow-md.md)]


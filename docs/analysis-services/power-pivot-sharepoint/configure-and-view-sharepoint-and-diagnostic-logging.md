---
title: "Configure and View SharePoint and Diagnostic Logging | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: ppvt-sharepoint
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Configure and View SharePoint and Diagnostic Logging
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] server operations, events, and messages are recorded in SharePoint log files. Use the information in this topic to configure logging levels and view log file information. You can control which [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] server events are logged to the file. You can also control the severity of messages that are logged. For more information, see [Configure Usage Data Collection for &#40;Power Pivot for SharePoint](../../analysis-services/power-pivot-sharepoint/configure-usage-data-collection-for-power-pivot-for-sharepoint.md).  
  
 In this topic:  
  
-   [Log File Location](#bkmk_filelocation)  
  
-   [Modify diagnostic logging levels for individual event categories](#bkmk_modifyloglevels)  
  
-   [How to View SharePoint Log Files](#bkmk_how2viewlogfiles)  
  
##  <a name="bkmk_filelocation"></a> Log File Location  
 By default, SharePoint log files are saved to the following location:  
  
 `C:\Program files\Common Files\Microsoft Shared\Web Server Extensions\14\LOGS`  
  
 The LOGS folder contains log files (`.log`), data files (`.txt`), and usage files (`.usage`). The file naming convention for a SharePoint trace log is the server name followed by a date and time stamp. SharePoint trace logs are created at regular intervals and whenever there is an IISRESET. It is common to have many trace logs within a 24 hour period.  
  
##  <a name="bkmk_modifyloglevels"></a> Modify diagnostic logging levels for individual event categories  
 By default, ULS logging of [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] events is set at *Medium*. This setting is new for SQL Server 2012. If you upgraded a server from the prior release, the logging level might still be set at *Verbose*, the default level in SQL Server 2008 R2. If you are accustomed to reviewing the ULS logs for [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] server usage information, you will notice that as a result of this change, there is less information about [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] server operations.  
  
 Except for exceptions, which are of type *High*, all [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] messages fall into the Verbose category. If you want log entries for routine server operations such as connections, requests, or query reporting, you must change the logging level to Verbose.  
  
 To modify diagnostic logging levels for individual event categories:  
  
1.  In SharePoint Central Administration, click **Monitoring**.  
  
2.  Click **Configure diagnostic logging**.  
  
3.  Scroll to **[!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Service**.  
  
4.  Expand the category and select individual categories.  
  
     **Application Page Request** specifies events triggered by the service application when locating a [!INCLUDE[ssGeminiSrv_md](../../includes/ssgeminisrv-md.md)] for loading a [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] data source and communicating with other servers in the farm.  
  
     **Request Processing** specifies events triggered by query requests against a [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] database that is loaded on a server in the farm.  
  
     **Usage** specifies an event related to [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] usage data collection.  
  
5.  In Least critical event to report to the event log, select **None** to disable event logging for the category, or **Error** to limit logging for just errors.  
  
6.  Select **Verbose** to log all events to the event log.  
  
7.  In Least critical event to report to the trace log, select **None** to disable tracing for the category, or **Error** to limit tracing for just errors.  
  
8.  Select **Verbose** to log all events to the trace log.  
  
9. Click **OK**.  
  
##  <a name="bkmk_how2viewlogfiles"></a> How to View SharePoint Log Files  
 Log files are text files. You can open them in any text editor. You can also use third-party log viewer applications.  
  
#### Use a Text Editor  
 If you are using a text editor to troubleshoot a [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] server error, the following tips can help you locate relevant information in the file:  
  
-   For errors related to publishing, viewing, or refreshing a [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] workbook, search the log file for the name of the workbook.  
  
-   For errors that provide a correlation ID, copy the ID and use it as a search term in the log file.  
  
-   Search for error status "High" or "Exception". Search for "[!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Service".  
  
-   If you know when the error occurred, use the date and time information to narrow the scope of entries you must scroll through.  
  
#### Use a Log Viewer Application  
 Although you can use a text editor to view the trace logs individually, a log viewer application that enables you to view several log files together can be far more useful. Fortunately, there are several third-party log viewer applications available for download on the Codeplex site that can help you view multiple trace logs in a single workspace.  
  
 The following instructions include links to popular SharePoint ULS Log Viewers that you can download from Codeplex.  
  
1.  Go to [SharePoint LogViewer](http://sharepointlogviewer.codeplex.com) or [SharePoint ULS Log Viewer](http://go.microsoft.com/fwlink/?LinkId=150052) on the Codeplex site.  
  
2.  Click the **Downloads** tab.  
  
3.  Click the executable file. It will either be **SharePointLogViewer.exe** or **ULS Viewer 2.0 Binary**.  
  
4.  Read and accept the licensing terms.  
  
5.  Click **Save** to download the software to your local system.  
  
     If you are downloading SharePoint ULS Log Viewer, save ULSViewer.zip to the Downloads folder.  
  
6.  In the Downloads folder, right-click ULSViewer.zip and select **Extract All**.  
  
7.  Specify a destination folder, and then click **Extract**.  
  
8.  In the folder that contains the extracted program files, double-click **ULSViewer** and run the application.  
  
9. Click the folder icon at the top left corner of the application window.  
  
10. Browse to \Program files\Common Files\Microsoft Shared\Web Server Extensions\14\Logs.  
  
11. Select one or more trace logs. By default, trace log file names consist of the computer name plus date and time information. The file type is Text Document.  
  
12. Click **Open** and wait for the files to load.  
  
13. Use the built-in filters to select records based on severity, process, category, or a user-defined text file. You can also click column headings to change the sort order.  
  
#### Entries for Power Pivot Services  
 The following table describes entries for [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] server operations most likely to be found in a SharePoint log file.  
  
|Process|Area|Category|Level|Message|Details|  
|-------------|----------|--------------|-----------|-------------|-------------|  
|w3wp.exe|[!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Service|Usage|Verbose|There are no current request statistics, nothing to log.|At predefined intervals, the service reports query response statistics as a usage event to the usage data collection system. This message indicates there were no query statistics to report.|  
|w3wp.exe|[!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Service|Web front end|Verbose|Starting to locate an application server for data source=\<*path*>|When it receives a connection request, the [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] service identifies an available [!INCLUDE[ssGeminiSrv_md](../../includes/ssgeminisrv-md.md)] to handle the request. If there is only one server in the farm, the local server accepts the request in all cases.|  
|w3wp.exe|[!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Service|Web front end|Verbose|Locating the application server succeeded.|The request was allocated to a [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] service application.|  
|w3wp.exe|[!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Service|Web front end|Verbose|Redirecting request for the \<*PowerPivotdata source*> to the [!INCLUDE[ssGeminiSrv_md](../../includes/ssgeminisrv-md.md)].|The request was forwarded to the [!INCLUDE[ssGeminiSrv_md](../../includes/ssgeminisrv-md.md)].|  
|w3wp.exe|[!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Service|Request Processing|Verbose|Redirecting request for UserName\<*SharePoint user*> to the database|An impersonated connection to the [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] data source was created on behalf of the SharePoint user.|  
  
## See Also  
 [Power Pivot Usage Data Collection](../../analysis-services/power-pivot-sharepoint/power-pivot-usage-data-collection.md)   
 [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)   
 [Configure Usage Data Collection for &#40;Power Pivot for SharePoint](../../analysis-services/power-pivot-sharepoint/configure-usage-data-collection-for-power-pivot-for-sharepoint.md)  
  
  

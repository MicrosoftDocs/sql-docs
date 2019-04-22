---
title: "Manage a Reporting Services SharePoint Service Application | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: bfda2e04-2d82-4534-bb50-90925f7386ae
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Manage a Reporting Services SharePoint Service Application
  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Service Applications are managed from SharePoint Central Administration. The Management and Properties pages allow you to update the configuration of the service application as well as common administration tasks.  
  
 This topic covers the following information:  
  
-   [To Open Service Application Management Pages](#bkmk_openpages)  
  
-   [System Settings Page](#bkmk_systemsettings)  
  
-   [Manage Jobs](#bkmk_managejobs)  
  
-   [Key Management](#bkmk_keymgt)  
  
-   [Execution Account](#bkmk_executionaccount)  
  
-   [E-mail Settings](#bkmk_email)  
  
-   [Provision Subscriptions and Alerts](#bkmk_provisionsubscriptions)  
  
## To Open Service Application Properties Page  
 To open the properties page for a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] service application, complete the following:  
  
1.  In Central Administration, in the Application Management group, click **Manage service applications**.  
  
2.  Click near the name of your service application or on the **type** column, which will select the entire row, and then click **Properties** in the SharePoint ribbon.  
  
 For more information on service application properties, see [Step 3: Create a Reporting Services Service Application](../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2013.md#bkmk_create_serrviceapplication).  
  
##  <a name="bkmk_openpages"></a> To Open Service Application Management Pages  
 To open the management pages for a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] service application, complete the following:  
  
1.  In Central Administration, in the Application Management group, click **Manage service applications**.  
  
2.  Click the name of your service application and the **Manage Reporting Services Application** page will open.  
  
3.  Alternatively, you could click near the name or on the **type** column for your service application, which will select the entire row, and then click **Manage** in the SharePoint ribbon.  
  
##  <a name="bkmk_systemsettings"></a> System Settings Page  
 The systems settings page allows you to configure the behavior and user experience of your service application including various timeouts.  
  
-   [Report Settings](#bkmk_report_settings_section)  
  
-   [Session Settings](#bkmk_session_settings_section)  
  
-   [System Settings for Logging](#bkmk_logging_settings_section)  
  
-   [Security Settings](#bkmk_security_settings_section)  
  
-   [Client Settings](#bkmk_client_settings_section)  
  
###  <a name="bkmk_report_settings_section"></a> Report Settings  
  
|Setting|Comments|  
|-------------|--------------|  
|External Images Timeout|Default is 600 seconds.|  
|Snapshot Compression|Default is SQL|  
|System Report Timeout|Default is 1800 seconds.<br /><br /> Specify whether report processing times out on the report server after a certain number of seconds. This value applies to report processing on a report server. It does not affect data processing on the database server that provides the data for your report. The report processing timer clock begins when the report is selected and ends when the report opens. The value that you specify must be sufficient to complete both data processing and report processing.|  
|System Snapshot Limit|Default is -1, which is no limit.<br /><br /> Set a site-wide default value for the number of copies of report history to retain. The default value provides an initial setting that establishes the number of snapshots that can be stored for each report. You can specify different limits in property pages for specific reports.|  
|Stored Parameters Lifetime|Default is 180|  
|Stored Parameters Threshold|Default is 1500 days.|  
  
###  <a name="bkmk_session_settings_section"></a> Session Settings  
  
|Setting|Comments|  
|-------------|--------------|  
|Session Timeout|Default is 600 seconds.|  
|Use Session Cookies|Default is TRUE.|  
|EDLX Report Timeout|Default is 1800 seconds.|  
  
###  <a name="bkmk_logging_settings_section"></a> System Settings for Logging  
  
|Setting|Comments|  
|-------------|--------------|  
|Enable Execution Logging|Default is TRUE.<br /><br /> specify whether the report server generates trace logs and the number of days the log is kept. . The logs are stored on the report server computer in the \Microsoft SQL Server\MSSQL.n\ReportServer\Log folder. A new log file is started each time the service is restarted. For more information about log files, see [Report Server Service Trace Log](report-server/report-server-service-trace-log.md)|  
|Execution Log Days Kept|Default is 60 days.|  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] all supports SharePoint ULS logging.  For more information, see [Turn on Reporting Services events for the SharePoint trace log &#40;ULS&#41;](report-server/turn-on-reporting-services-events-for-the-sharepoint-trace-log-uls.md)  
  
###  <a name="bkmk_security_settings_section"></a> Security Settings  
  
|Setting|Comments|  
|-------------|--------------|  
|Enable Integrated Security|Default is TRUE.<br /><br /> Specifies whether a connection to a report data source can be made using the Windows security token of the user who requested the report.|  
|Enable Load Report Definition|Default is TRUE..|  
|Enable Remote Errors|Default is FALSE|  
|Enable Test Connection Detailed Errors|Default is TRUE.|  
  
###  <a name="bkmk_client_settings_section"></a> Client Settings  
  
|Setting|Comments|  
|-------------|--------------|  
|Enable Report Builder Download|Default is TRUE.<br /><br /> Specifies whether clients are able to see the button for downloading the report builder application.|  
|Report Builder Launch URL|Specify a custom URL when the report server does not use the default Report Builder URL. This setting is optional. If you do not specify a value, the default URL will be used, which launches Report Builder. To launch Report Builder 3.0 as a Click-Once application, enter the following value: http://\<computername>/ReportServer/ReportBuilder/ReportBuilder_3_0_0_0.application.|  
|Enable Client Printing|The Default is TRUE.<br /><br /> Specifies whether users can download the client side control, which provides printing options.|  
|Edit Session Timeout|Default is 7200 seconds.|  
|Edit Session Cache Limit|Default is 5.|  
  
##  <a name="bkmk_managejobs"></a> Manage Jobs  
 You can view and delete the running jobs, for example jobs that are created by report subscriptions and data-driven subscriptions. The page is not used to manage subscriptions, but rather jobs that are triggered by a subscription. For example a subscription that is scheduled to run once an hour will generate a job once an hour that appears on the **Manage Jobs** page.  
  
 ![manage running jobs](media/ssrs-manage-jobs.gif "manage running jobs")  
  
##  <a name="bkmk_keymgt"></a> Key Management  
 The following table summarizes the Kay Management pages  
  
> [!IMPORTANT]  
>  Periodically changing the Reporting Services encryption key is a security best practice. A recommended time to change the key is immediately following a major version upgrade of Reporting Services. Changing the key after an upgrade minimizes additional service interruption caused by changing the Reporting Services encryption key outside of the upgrade cycle.  
  
|Page|Description|  
|----------|-----------------|  
|Backup Encryptions Key|1) Type a password in to the **Password:** and **Confirm Password:** boxes and click **Export**. You will see a warning if the password you typed is does not meet the complexity requirements of the domain policy.<br /><br /> 2) You will be prompted for a file location of where to save the key file. You should consider storing the key file on a separate computer from the one that is running [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. The default file name is the same name as the service application.|  
|Restore Encryption Key|1) Type or browse to the key file in the **File Location** box<br /><br /> 2) In the **Password** box, type the password that was used to back up the encryption file.<br /><br /> 3) Click **OK**|  
|Change Encryption Key|This operation will create a new key and re-encrypt your encrypted content. If you have a lot of content, this operation make take several hour.<br /><br /> When the change encryption key operation is complete, it is recommended you make a backup of your new key.|  
|Deleted Encrypted Content|Deleted content cannot be recovered.<br /><br /> **\*\* Important \*\*** The action of deleting and recreating the symmetric key cannot be reversed or undone. Deleting or recreating the key can have important ramifications on your current installation. If you delete the key, any existing data encrypted by the symmetric key will also deleted. Deleted data includes connection strings to external report data sources, stored connection strings, and some subscription information.|  
  
##  <a name="bkmk_executionaccount"></a> Execution Account  
 Use this page to configure an account to use for unattended processing. This account is used under special circumstances when other sources of credentials are not available:  
  
-   When the report server connects to a data source that does not require credentials. Examples of data sources that might not require credentials include XML documents and some client-side database applications.  
  
-   When the report server connects to another server to retrieve external image files or other resources that are referenced in a report.  
  
 Setting this account is optional, but not setting it limits your use of external images and connections to some data sources. When retrieving external image files, the report server checks to see if an anonymous connection can be made. If the connection is password protected, the report server uses the unattended report processing account to connect to the remote server. When retrieving data for a report, the report server either impersonates the current user, prompts the user to provide credentials, uses stored credentials, or uses the unattended processing account if the data source connection specifies **None** as the credential type. The report server does not allow its service account credentials to be delegated or impersonated when connecting to other computers, so it must use the unattended processing account if no other credentials are available.  
  
 The account you specify should be different from the one used to run the service account. If you are running the report server in a scale-out deployment, you must configure this account the same way on each report server.  
  
 You can use any Windows user account. For best results, choose an account that has read permissions and network logon permissions to support connections to other computers. It must have read permissions on any external image or data file that you want to use in a report. Do not specify a local account unless all report data sources and external images are stored on the report server computer. Use the account only for unattended report processing.  
  
 ![PowerShell related content](media/rs-powershellicon.jpg "PowerShell related content")  
  
 The following is an example PowerShell command to return the list of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] service applications with the UEAccount property:  
  
```  
Get-SPRSServiceApplication | select typename, name, service, ueaccountname  
```  
  
 For more information, see [PowerShell cmdlets for Reporting Services SharePoint Mode](../../2014/reporting-services/powershell-cmdlets-for-reporting-services-sharepoint-mode.md).  
  
### Options  
 **Specify an execution account**  
 Select to specify an account.  
  
 **Account**  
 Enter a Windows domain user account. Use this format: *\<domain>\\<user account\>*.  
  
 **Password**  
 Type the password.  
  
 **Confirm password**  
 Retype the password.  
  
##  <a name="bkmk_email"></a> E-mail Settings  
 Use this page to specify Simple Mail Transport Protocol (SMTP) settings that enable report server e-mail delivery from the report server. You can use the Report Server E-Mail delivery extension to distribute reports or report processing notifications through e-mail subscriptions. The Report Server E-Mail delivery extension requires an SMTP server and an e-mail address to use in the From: field.  
  
### Options  
 **Use SMTP Server**  
 Specifies that report server e-mail is routed through an SMTP server.  
  
 **Outbound SMTP Server**  
 Specify the SMTP server or gateway to use. You can use a local server or an SMTP server on your network.  
  
 **From Address**  
 Specifies the e-mail address to use in the From: field of a generated e-mail. You must specify a user account that has permission to send mail from the SMTP server.  
  
##  <a name="bkmk_provisionsubscriptions"></a> Provision Subscriptions and Alerts  
 Use this page to verify if SQL Server Agent is running and to provision access for reporting services to use SQL Server Agent. SQL Server Agent is required for [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] subscriptions, schedules, and data alerts. [Provision Subscriptions and Alerts for SSRS Service Applications](install-windows/provision-subscriptions-and-alerts-for-ssrs-service-applications.md)  
  
## Proxy association  
 When you created the Reporting Services service application you selected the web application to associate and provision permissions for access by the Reporting Services service application. If you chose to not associate or you want to change the association, you can use the following steps.  
  
1.  In SharePoint Central Administration, in the Application Management, click **Configure Service Application Associations**.  
  
2.  On the Service application Associations page, change the view to **Service Applications**.  
  
3.  Find and click the name of your new [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Service application. You could also click the application proxy group name **default** to add the proxy to default group rather than completing the following steps.  
  
4.  Select **Custom** in the selection box **Edit the following group of connections**.  
  
5.  Check the box for your proxy and click **Ok**.  
  
  

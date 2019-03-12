---
title: "Data Sources Properties Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: f37edda0-19e6-489e-b544-8751fa6b6cfb
author: markingmyname
ms.author: maghan
manager: kfile
---
# Data Sources Properties Page (Report Manager)
  Use the Data Sources properties page to define how the current report connects to an external data source. You can override the data source connection information that was originally published with the report. If multiple data sources are used with a report, each data source has its own section in the properties page. Data sources are listed in the order in which they are defined in the report.  
  
 When specifying a data source to use with the report, you can use a shared data source that is created and managed separately from the reports that use it. If you do not want to use a shared data source item, you can define a data source connection to use with the report.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
### To open the Data Sources properties page  
  
1.  Open Report Manager, and locate the report for which you want to select a data source.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the **General** properties page for the report.  
  
4.  Select the **Data Sources** tab.  
  
## Options  
 **A shared data source**  
 Specify a shared data source to use with the report. For more information about creating a new data source, see [Create, Delete, or Modify a Shared Data Source &#40;Report Manager&#41;](../../2014/reporting-services/create-delete-or-modify-a-shared-data-source-report-manager.md).  
  
 **Browse**  
 Click **Browse** to open the Data Source Selection page, which is used to select a shared data source. For more information, see [Data Source Selection Page &#40;Report Manager&#41;](../../2014/reporting-services/data-source-selection-page-report-manager.md).  
  
 **A custom data source**  
 Specify how the report connects to the data source.  
  
 The following options are used to specify a custom data source connection.  
  
 **Data source type**  
 Specify the data processing extension that is used to process data from the data source. For the list of built-in data extensions, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](create-deploy-and-manage-mobile-and-paginated-reports.md). Additional data processing extensions may be available from third-party vendors.  
  
 **Connection string**  
 Specify the connection string that the report server uses to connect to the data source. The connection type determines the syntax you should use. For example, a connection string for the XML data processing extension is a URL to an XML document. In most cases, a typical connection string specifies the database server and a data file. The following example illustrates a connection string used to connect to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database that is named MyData:  
  
 `data source=<a SQL Server instance>;initial catalog=MyData`  
  
 A connection string can be configured as an expression so that you can specify the data source at run time. Data source expressions are defined in the report in Report Designer. Data source expressions cannot be defined, viewed, or modified in Report Manager. However, you can replace a data source expression by clicking **Override Default** to type in a static connection string. If you want to switch back to the expression, click **Revert to Default**. The report server stores the original connection string in case you need to restore it. To use data source expressions, you must use the data source connection information that was originally published in the report. Shared data sources do not support the use of expressions in the connection string.  
  
 **Connect using**  
 Specifies options that determine how credentials are obtained.  
  
> [!IMPORTANT]  
>  If credentials are provided in the connection string, the options and values provided in this section are ignored. Note that if you specify credentials on the connection string, the values are displayed in clear text to all users who view this page.  
  
 **Credentials supplied by the user running the report**  
 Each user must type in a user name and password to access the data source. You can define the prompt text that requests user credentials. The default text string is "Enter a user name and password to access the data source."  
  
 Select **Use as Windows credentials when connecting to the data source** if the credentials that the user provides are Windows Authentication credentials. Do not select this check box if you are using database authentication (for example, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication).  
  
 **Credentials stored securely in the report server**  
 Store an encrypted user name and password in the report server database. Select this option to run a report unattended (for example, reports that are initiated by schedules or events instead of user action). If you are using default security, the user name must be a Windows domain account. Specify the account in this format: \<domain>\\<username\>. The account you specify must have log on locally permissions on the computer that hosts the data source used by the report.  
  
 Select **Use as Windows credentials when connecting to the data source** if the credentials are Windows Authentication credentials. Do not select this check box if you are using database authentication (for example, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication).  
  
 Select **Impersonate the authenticated user after a connection has been made to the data source** to allow delegation of credentials, but only if a data source supports impersonation. For [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases, this option sets the SETUSER function.  
  
> [!TIP]  
>  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]only supports Windows account credentials. Therefore select both options "Use as Windows credentials when connecting to the data source" and "Impersonate the authenticated user after a connection has been made to the data source" for an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] data source.  
  
 **Windows integrated security**  
 Use the Windows credentials of the current user to access the data source. Select this option when the credentials that are used to access a data source are the same as those used to log on to the network domain. This option works best when Kerberos authentication is enabled for your domain, or when the data source is on the same computer as the report server. If Kerberos is not enabled, Windows credentials can be passed to one other computer. If additional computer connections are required, you will get an error instead of the data you expect.  
  
 A report server administrator can disable the use of Windows integrated security for accessing report data sources. If this value is grayed out, the feature is not available.  
  
 Do not use this option if you plan to schedule or subscribe to this report. Scheduled or unattended report processing requires credentials that can be obtained without user input or the security context of a current user. Only stored credentials provide this capability. For this reason, the report server prevents you from scheduling report or subscription processing if the report is configured for the Windows integrated security credential type. If you choose this option for a report that is already subscribed to or that has scheduled operations, the subscriptions and scheduled operations will stop.  
  
 **Credentials are not required**  
 Specify that credentials are not required to access the data source. Note that if a data source requires a user logon, choosing this option will have no effect. You should only choose this option if the data source connection does not require user credentials.  
  
 To use this option, you must have previously configured the unattended execution account for your report server deployment. The unattended execution account is used to connect to external data sources when other sources of credentials are not available. If you specify this option and the account is not configured, the connection to the report data source will fail and report processing will not occur.  For more information about this account, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).  
  
 **Apply**  
 Click to save your changes.  
  
## See Also  
 [Manage Report Data Sources](report-data/manage-report-data-sources.md)   
 [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  

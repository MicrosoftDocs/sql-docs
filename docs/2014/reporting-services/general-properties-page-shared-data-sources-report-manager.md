---
title: "General Properties Page, Shared Data Sources (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 1b344449-6f7c-47d2-a737-972d88c0faf8
author: markingmyname
ms.author: maghan
manager: kfile
---
# General Properties Page, Shared Data Sources (Report Manager)
  Use the General properties page to view or modify properties of a shared data source item. Any changes that you make to the properties take effect for all reports that reference the item when you click **Apply**.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the General properties page for a shared data source  
  
1.  Open Report Manager, and locate the shared data source for which you want to view properties.  
  
2.  Hover over the data source, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the shared data source.  
  
## Options  
 **Name**  
 Specifies a name for the shared data source, which is used to identify the item within the report server namespace.  
  
 **Description**  
 Provides information about the shared data source. This description appears on the Contents page.  
  
 **Hide in list view**  
 Select this option to hide the shared data source from users who are using list view mode in Report Manager. List view mode is the default view format when browsing the report server folder hierarchy. In list view, item names and descriptions flow across the page. The alternate format is details view. Details view omits descriptions, but includes other information about the item. Although you can hide an item in list view, you cannot hide an item in details view. If you want to restrict access to an item, you must create a role assignment.  
  
 **Enable this data source**  
 Select to enable or disable the shared data source. You can disable the shared data source to prevent processing for all reports, report models, and data-driven subscriptions that reference the item.  
  
 **Data source type**  
 Specifies the data processing extension that is used to process data from the data source. The report server includes data processing extensions for [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], Oracle, XML, SAP, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], ODBC, and OLE DB. Additional data processing extensions may be available from third-party vendors.  
  
 Note that if you are using [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] Edition with Advanced Services, you can only choose [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data sources.  
  
 **Connection string**  
 Specify the connection string that the report server uses to connect to the data source. The connection type determines the syntax you should use. For example, a connection string for the XML data processing extension is a URL to an XML document. In most cases, a typical connection string specifies the database server and a data file. The following example illustrates a connection string used to connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssSampleDBnormal](../includes/sssampledbnormal-md.md)] database:  
  
```  
data source=<a SQL Server instance>;initial catalog=AdventureWorks2012  
```  
  
 **Connect Using**  
 Specifies options that determine how credentials are obtained.  
  
> [!IMPORTANT]  
>  If credentials are provided in the connection string, the options and values provided in this section are ignored. Note that if you specify credentials on the connection string, the values are displayed in clear text to all users who view this page.  
  
 **Credentials supplied by the user running the report**  
 Each user must type in a user name and password to access the data source. You can define the prompt text that requests user credentials. The default text string is "Enter a user name and password to access the data source."  
  
 Select **Use as Windows credentials when connecting to the data source** if the credentials that the user provides are Windows Authentication credentials. Do not select this check box if you are using database authentication (for example, SQL Server Authentication).  
  
 **Credentials stored securely in the report server**  
 Store an encrypted user name and password in the report server database. Select this option to run a report unattended (for example, reports that are initiated by schedules or events instead of user action). If you are using default security, the user name must be a Windows domain account. Specify the account in this format: \<domain>\\<username\>. The account you specify must have log on locally permissions on the computer that hosts the data source used by the report.  
  
 Select **Use as Windows credentials when connecting to the data source** if the credentials are Windows Authentication credentials. Do not select this check box if you are using database authentication (for example, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication).  
  
 If you are using database authentication, select **Impersonate the authenticated user after a connection has been made to the data source (Connect using)** to allow delegation of database credentials, but only if a database server supports impersonation. For [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases, this option sets the SETUSER function.  
  
 **Windows Integrated Security**  
 Use the Windows credentials of the current user to access the data source. Select this option when the credentials that are used to access a data source are the same as those used to log on to the network domain. This option works best when Kerberos is enabled for your domain, or when the data source is on the same computer as the report server. If Kerberos is not enabled, Windows credentials can be passed to one other computer. If additional computer connections are required, you will get an error instead of the data you expect.  
  
 A report server administrator can disable the use of Windows integrated security for accessing report data sources. If this value is grayed out, the feature is not available.  
  
 Do not use this option if you plan to schedule or subscribe to this report. Scheduled or unattended report processing requires credentials that can be obtained without user input or the security context of a current user. Only stored credentials provide this capability. For this reason, the report server prevents you from scheduling report or subscription processing if the report is configured for the Windows integrated security credential type. If you choose this option for a report that is already subscribed to or that has scheduled operations, the subscriptions and scheduled operations will stop.  
  
 **Credentials are not required**  
 Specify that credentials are not required to access the data source. Note that if a data source requires a user logon, choosing this option will have no effect. You should only choose this option if the data source connection does not require user credentials.  
  
 To use this option, you must have previously configured the unattended execution account for your report server deployment. The unattended execution account is used to connect to external data sources when other sources of credentials are not available. If you specify this option and the account is not configured, the connection to the report data source will fail and report processing will not occur. For more information about this account, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).  
  
 **Apply**  
 Click to save your changes.  
  
 **Delete**  
 Click to delete the shared data source. Deleting a shared data source deactivates any reports, models, and data-driven subscriptions that use it. To reactivate reports, models, and subscriptions, you must open each one and update its data source properties to use a different shared data source. For reports and subscriptions, you can specify data source connection information as Data Source property values.  
  
 **Move**  
 Click to move the shared data source to a different location in the report server folder namespace.  
  
 **Generate Model**  
 Click to create a new model based on the shared data source.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [New Data Source Page &#40;Report Manager&#41;](../../2014/reporting-services/new-data-source-page-report-manager.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)   
 [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md)  
  
  

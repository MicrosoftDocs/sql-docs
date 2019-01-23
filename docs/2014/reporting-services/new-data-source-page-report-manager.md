---
title: "New Data Source Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 35563d4c-a3d5-4f95-bf46-605da9dfcbb8
author: markingmyname
ms.author: maghan
manager: craigg
---
# New Data Source Page (Report Manager)
  Use the New Data Source page to create a shared data source item. A shared data source defines a connection to an external data source. With a shared data source, you can create and maintain the settings for the data source connection separately from the reports, models, and data-driven subscriptions that use the data source.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the New Data Source page  
  
1.  Open Report Manager, and navigate to the folder in which you want to create a data source.  
  
2.  In the toolbar, click **New Data Source**. You must have Content Manager permissions to create a shared data source.  
  
## Options  
 **Name**  
 Type a name for the shared data source, which is used to identify the item within the report server folder hierarchy.  
  
 **Description**  
 Provide information about the shared data source. This description appears on the Contents page.  
  
 **Hide in list view**  
 Select this option to hide the shared data source from users who are using list view mode in Report Manager. List view mode is the default view format when browsing the report server folder hierarchy. In list view, item names and descriptions flow across the page. The alternate format is details view. Details view omits descriptions, but includes other information about the item. Although you can hide an item in list view, you cannot hide an item in details view. If you want to restrict access to an item, you must create a role assignment  
  
 **Enable this data source**  
 Select to enable or disable the shared data source. You can disable the shared data source to prevent processing for all reports and models that reference the item.  
  
 **Data source type**  
 Specify the data processing extension that is used to process data from the data source. Report server includes data processing extensions for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], Oracle, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], SAP, XML, ODBC, and OLE DB. Additional data processing extensions may be available from third-party vendors.  
  
 For more information about remote and non-SQL data source support, see [Features Supported by the Editions of SQL Server 2012](https://go.microsoft.com/fwlink/?linkid=232473) ( HYPERLINK "<https://go.microsoft.com/fwlink/?linkid=232473>" <https://go.microsoft.com/fwlink/?linkid=232473>) and [Data Sources Supported by Reporting Services &#40;SSRS&#41;](create-deploy-and-manage-mobile-and-paginated-reports.md).  
  
 **Connection string**  
 Specify the connection string that the report server uses to connect to the data source. The connection type determines the syntax you should use. For example, a connection string for the XML data processing extension is a URL to an XML document. In most cases, a typical connection string specifies the database server and a data file.  
  
 The following example illustrates a connection string used to connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssSampleDBnormal](../includes/sssampledbnormal-md.md)] database:  
  
```  
data source=<a SQL Server instance>;initial catalog=AdventureWorks2012  
```  
  
 For more examples and information about different ways to specify a connection string, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md).  
  
 **Connect using**  
 Specify options that determine how credentials are obtained.  
  
> [!IMPORTANT]  
>  If credentials are provided in the connection string, the options and values provided in this section are ignored. Note that if you specify credentials on the connection string, the values are displayed in clear text to all users who view this page.  
  
 **Credentials supplied by the user running the report (Connect using)**  
 Each user is prompted to type in a user name and password to access the data source. You can define the prompt text that requests user credentials. The default text string is "Enter a user name and password to access the data source."  
  
 Select **Use as Windows credentials when connecting to the data source** if the credentials that the user provides are Windows Authentication credentials. Do not select this check box if you are using database authentication (for example, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication).  
  
 **Credentials stored securely in the report server (Connect using)**  
 Store an encrypted user name and password in the report server database. Choose this option to run a report unattended (for example, reports that are initiated by schedules or events instead of user action). If you are using default security, the user name must be a Windows domain account. Specify the account in this format: \<domain>\\<username\>. The account you specify must have log on locally permissions on the computer that hosts the data source used by the report.  
  
 Select **Use as Windows credentials when connecting to the data source** if the credentials are Windows Authentication credentials. Do not select this check box if you are using database authentication (for example, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication).  
  
 If you are using database authentication, select **Impersonate the authenticated user after a connection has been made to the data source** to allow delegation of database credentials, but only if a database server supports impersonation. For [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases, this option sets the SETUSER function.  
  
 **Windows integrated security (Connect using)**  
 Use the Windows credentials of the current user to access the data source. Choose this option when the credentials that are used to access a data source are the same as those used to log on to the network domain. This option works best when Kerberos authentication is enabled for your domain, or when the data source is on the same computer as the report server. If Kerberos authentication is not enabled, Windows credentials can be passed to one other computer. If additional computer connections are required, you will get an error instead of the data you expect.  
  
 A report server administrator can disable the use of Windows integrated security for accessing report data sources. If this value is grayed out, the feature is not available.  
  
 Do not use this option if you plan to schedule or subscribe to this report. Scheduled or unattended report processing requires credentials that can be obtained without user input or the security context of a current user. Only stored credentials provide this capability. For this reason, the report server prevents you from scheduling report or subscription processing if the report is configured for the Windows integrated security credential type. If you choose this option for a report that is already subscribed to or that has scheduled operations, the subscriptions and scheduled operations will stop.  
  
 **Credentials are not required (Connect using)**  
 Specify that credentials are not required to access the data source. Note that if a data source requires a user logon, choosing this option will have no effect. You should only choose this option if the data source connection does not require user credentials.  
  
 To use this option, you must have previously configured the unattended execution account for your report server deployment. The unattended execution account is used to connect to external data sources when other sources of credentials are not available. If you specify this option and the account is not configured, the connection to the report data source will fail and report processing will not occur. For more information about this account, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).  
  
 **OK**  
 Click to save your changes.  
  
## See Also  
 [Create, Delete, or Modify a Shared Data Source &#40;Report Manager&#41;](../../2014/reporting-services/create-delete-or-modify-a-shared-data-source-report-manager.md)   
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Contents Page &#40;Report Manager&#41;](../../2014/reporting-services/contents-page-report-manager.md)   
 [Create, Modify, and Delete Shared Data Sources &#40;SSRS&#41;](report-data/create-modify-and-delete-shared-data-sources-ssrs.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)   
 [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md)  
  
  

---
title: "Set credentials and connections for report data sources"
description: A report server uses credentials to connect to external data sources that provide content to reports or recipient information to a data-driven subscription.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/01/2024
ms.service: reporting-services
ms.subservice: report-data
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "no credentials option [Reporting Services]"
  - "impersonation [Reporting Services]"
  - "user impersonation [Reporting Services]"
  - "Windows authentication [Reporting Services]"
  - "data sources [Reporting Services], security"
  - "connection strings [Reporting Services]"
  - "unattended report processing [Reporting Services]"
  - "reports [Reporting Services], security"
  - "remote data sources [Reporting Services]"
  - "credentials [Reporting Services]"
  - "authentication [Reporting Services]"
  - "external data sources [Reporting Services]"
  - "prompted credentials [Reporting Services]"
  - "multiple connections"
  - "stored credentials [Reporting Services]"
  - "security [Reporting Services], data sources"
  - "Windows integrated security [Reporting Services]"
#customer intent: As a SQL server user, I want to use a report server to set credentials and connections so that I can safely use remote data sources.
---
# Specify credential and connection information for report data sources

A report server uses credentials to connect to external data sources that provide content to reports or recipient information to a data-driven subscription. You can specify credentials that use Windows Authentication, database authentication, no authentication, or custom authentication. When the report server sends a connection request over the network, it either impersonates a user account or the unattended execution account. For more information about the security context under which a connection request is made, see [Data source configuration and network connections](#DataSourceConfigurationConnections) in this article.

> [!NOTE]
> A report server also uses credentials to authenticate users who access them. Information about authenticating users to a report server is provided in another topic.

The connection to an external data source is defined when you create the report. It can be managed separately after the report is published. You can specify a static connection string or an expression that allows users to select a data source from a dynamic list. For more information about how to specify a data source type and connection string, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).

## How Report Builder uses credentials

In Report Builder, the report server often uses credentials when you connect. The report server also uses credentials for data-related tasks, such as creating an embedded data source, running a dataset query, or previewing a report. Credentials aren't stored in the report. The report server and the local client manage credentials separately. The following list describes the types of credentials that you might need to provide, where the report server stores them, and how the report server uses them:

- Report server credentials that you enter in the Reporting Services sign-in dialog box.

   When you first save to, publish to, or browse to the report server or SharePoint site, you might need to enter your credentials. The credentials that you enter are used until the Report Builder session ends. If you choose to save the credentials, the report server or SharePoint site stores them securely with your user settings on your computer. In subsequent Report Builder sessions, saved credentials are used to connect to the same report server or SharePoint site. The report server administrator or SharePoint administrator specifies which type of credentials to use.

- Data source credentials that you enter in the **Data Source Properties** dialog box for an embedded data source.

   The report server uses these credentials to make a data connection to the external data source. For some types of data sources, the report server can securely store these credentials. These credentials enable other users to run the report without providing credentials for the underlying data connection.

- Data source credentials that you enter in the **Enter Data Source Credentials** dialog box when you run a dataset query, refresh dataset fields, or preview the report.

   The report server use these credentials to make a data connection from Report Builder to the external data source, or to preview a report that is configured to prompt for credentials. Credentials that you enter in this dialog box aren't stored on the report server and aren't available for use by other users. Report Builder caches the credentials during the report editing session so that you don't need to enter them every time you run the query or preview the report.

   For shared data sources, use the **Save my password** option to save the credentials locally with your user settings on your computer. Report Builder uses the saved credentials every time a connection is made to the corresponding external data source.

For more information, see [Preview reports in Report Builder](../../reporting-services/report-builder/previewing-reports-in-report-builder.md).

### Use remote data sources

If the report retrieves data from a remote database server, you must verify:

- The credentials provided to the database server are valid. If you use Windows user credentials, make sure that the user has permission to the server and database.

- Ports used by the database server are open. You must open ports 1433 and 1434 on the external computer if:
   - You're accessing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases on external computers.
   - The report server database is on an external [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

> [!NOTE]
>Be sure to restart the server after you open ports. For more information, see [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).

- Remote connections are enabled. If you're accessing SQL Server relational databases on external computers, you can use SQL Server Configuration Manager tool to verify that remote connections over Transmission Control Protocol (TCP) are enabled.

## Specify credentials for connecting to remote data sources

The data sources that provide content to reports are often hosted on remote servers. To retrieve data for a report, the report server must connect to the server by using a set of credentials that you provide in advance or that are obtained at run time. When configuring a data source, you can specify credentials in the following ways:

- Prompt the user for credentials.
- Store credentials.
- Use Windows integrated security.
- Use no credentials.

The network environment determines the kinds of connections you can support. For example, if the Kerberos version 5 protocol is enabled, you might be able to use the delegation and impersonation features available in Windows Authentication to support connections across multiple servers. If your network doesn't support these security features, you need to work around connection constraints. If delegation and impersonation aren't enabled, Windows credentials can be passed across one computer connection before they expire. A user connection from a client computer to a report server computer counts as the first connection. If the user opens a report that retrieves data from a remote server, that sign-in counts as a second connection. That connection fails if you specified the connection to use integrated security when delegation isn't enabled.

### Multiple connections

If multiple connections are required to complete a round trip from the client computer to an external report data source, choose from the following strategies to make the connections succeed.

- Enable impersonation and delegation features in your domain so that the report server can delegate credentials to other computers without limit.
- Use stored credentials or prompted credentials to query external data sources for report data. The credentials can be either a Windows domain account or a database sign-in.

### Prompted credentials

When you configure a report data source connection to use prompted credentials, each user who accesses the report must enter a user name and password to retrieve the data. This approach is recommended for reports that contain confidential data. Prompted credentials can be used only on reports that run on demand. Prompted credentials can be a Windows account or a database sign-in. To use Windows Authentication, you must select **Use as Windows credentials when connecting to the data source**. Otherwise, the report server passes credentials to the database server for user authentication. If the database server can't authenticate the credentials that you provide, the connection fails.

### Windows integrated security

When you use the **Windows Integrated Security** option, the report server passes the security token of the user accessing the report to the server hosting the external data source. In this case, the user isn't prompted to type a user name or password. This approach is recommended if impersonation and delegation features are enabled. If these features aren't enabled, you should only use this approach if all the servers that you want to access are located on the same computer.

### Stored credentials

You can store the credentials used to access an external data source. Credentials are stored in reversible encryption in the report server database. You can specify one set of stored credentials for each data source used in a report. The credentials you provide retrieve the same data for every user who runs the report.

Stored credentials are recommended as part of a strategy for accessing remote database servers. Stored credentials are required if you want to support subscriptions, or schedule report history generation or report snapshot refreshes. When a report runs as a background process, the report server is the agent that executes the report. Because there's no user context in place, the report server must get credential information from the report server database in order to connect to a data source.

The user name and password that you specify can be Windows credentials or a database sign-in. If you specify Windows credentials, the report server passes the credentials to Windows for subsequent authentication. Otherwise, the credentials are passed to the database server for authentication.

### How to grant "Allow log on locally" permissions to domain user accounts

If you use stored credentials to connect to an external data source, the Windows domain user account must have permission to sign on locally. This permission allows the report server to impersonate the user on the report server and send the request to the external data source as that impersonated user.

To grant this permission:

1. On the report server computer, in **Administrative Tools**, open **Local Security Policy**.

1. Under **Security Settings**, expand **Local Policies**, and then select **User Rights Assignment**.

1. In the details pane, right-click **Allow log on locally** and then right-click **Properties**.

1. Select **Add User or Group**.

1. Select **Locations**, specify a domain or other location that you want to search, and then select **OK**.

1. Enter the Windows account for which you want to allow interactive sign-in, and then select **OK**.

1. In the **Allow log on locally Properties** dialog box, select **OK**.

1. Verify that the account you selected doesn't also have deny permissions:

   1. Right-click **Deny log on locally** and then right-click **Properties**.

   1. If the account is listed, select it and then select **Remove**.

You can also use credentials to impersonate the identity of another user. For SQL Server databases, the impersonation options use the [SETUSER](../../t-sql/statements/setuser-transact-sql.md) function.

> [!IMPORTANT]
> Don't use impersonation for reports that support subscriptions or that use schedules to generate report history or refresh a report execution snapshot.

### No credentials

You can configure a data source connection to use no credentials. Microsoft recommends that you always use credentials to access a data sources; avoid the use of no credentials. However, you can choose to run a report with no credentials in the following cases:

- The remote data source doesn't require credentials.
- The credentials are passed in the connection string, which is recommended only for secure connections.
- The report is a subreport that uses the credentials of the parent report.

Under these conditions, the report server connects to a remote data source by using the unattended execution account that you must define in advance. Because the report server doesn't connect to a remote server by using its service credentials, you must specify an account that the report server can use to make the connection. For more information about creating this account, see [Configure the unattended execution account (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).

## Other methods

- User name and password sign-in: When you select **Use this user name and password**, a user name and password must be supplied to access the data source. For a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, the credentials might be for a database sign-in. The credentials are passed to the data source for authentication.
- Setting credentials programmatically: You can set credentials in your code to control access to reports and to the report server. For more information, see [Data sources and connection methods](../../reporting-services/report-server-web-service/methods/data-sources-and-connection-methods.md).

## <a name="DataSourceConfigurationConnections"></a> Data source configuration and network connections

 The following table shows how connections are made for specific combinations of credential types and data processing extensions. If you use a custom data processing extension, see [Specify Connections for Custom Data Processing Extensions](../../reporting-services/report-data/specify-connections-for-custom-data-processing-extensions.md).

|**Type**|**Context for network connection**|**Data Source Types**<br /><br /> **(SQL Server, Oracle, ODBC, OLE DB, Analysis Services, XML, SAP NetWeaver BI, Hyperion Essbase)**|
|--------------|----------------------------------------|------------------------------------------------------------------------------------------------------------------------------------|
|Integrated security|Impersonate the current user.|For all data source types, connect by using the current user account.|
|Windows credentials|Impersonate the specified user.|For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, ODBC, and OLE DB: connect by using the impersonated user account.|
|Database credentials|Impersonate the unattended execution account or the service account.<br /><br /> (Reporting Services removes administrator permissions when sending the connection request by using the service identity).|For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, ODBC, and OLE DB:<br /><br /> Append the user name and password on the connection string.<br /><br /> For [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]:<br /><br /> The connection succeeds if you use the TCP/IP protocol, otherwise it fails.<br /><br /> For XML:<br /><br /> Fail the connection on the report server if database credentials are used.|
|None|Impersonate the unattended execution account.|For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, ODBC, and OLE DB:<br /><br /> Use the credentials defined in the connection string. The connection fails on the report server if the unattended execution account is undefined.<br /><br /> For [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]:<br /><br /> Always fail the connection if no credentials are specified, even if the unattended execution account is defined.<br /><br /> For XML:<br /><br /> Connect as Anonymous User if the unattended execution account is defined; otherwise, fail the connection.|

## Related content

- [Data Sources Supported by Reporting Services (SSRS)](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md)
- [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)
- [Manage Report Data Sources](../../reporting-services/report-data/manage-report-data-sources.md)

---
title: Troubleshoot server and database connection problems
description: Learn how to diagnose and fix problems you experience when you're connecting to a report server and about 'Unexpected error' messages.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/04/2024
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: troubleshooting-general
ms.custom: updatefrequency5

#customer intent: As a Reporting Services user, I want to learn about different connection problems that I might experience so that I can diagnose and fix them.
---
# Troubleshoot server and database connection problems with Reporting Services

Use this article to troubleshoot problems that you experience when you connect to a report server. This article also provides information about "Unexpected error" messages. For more information about data source configuration, see [Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md). For more information about configuring report server connection information, see [Configure a report server database connection (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).

## Can't create a connection to data source 'datasourcename' (rsErrorOpeningConnection)

This generic error occurs when the report server can't open a connection to an external data source that provides data to a report. This error appears with a second error message that indicates the underlying cause. The following errors can appear with **rsErrorOpeningConnection**.

### Sign in failed for user 'UserName'

The user doesn't have permission to access the data source. If you use a SQL Server database, verify that the user has a valid database user sign-in. For more information about how to create a database user or a SQL Server sign-in, see [Create a database user](../../relational-databases/security/authentication-access/create-a-database-user.md) and [Create a login](../../relational-databases/security/authentication-access/create-a-login.md).

### Sign in failed for user 'NT AUTHORITY\ANONYMOUS LOGON'

This error occurs when credentials are passed across multiple computer connections. If you use Windows Authentication and the Kerberos version 5 protocol isn't enabled, an error occurs. This error happens when credentials are passed across more than one computer connection. To work around this error, you might want to use stored credentials or prompted credentials. For more information about how to work around this issue, see [Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md).

### An error occurred while establishing a connection to the server

When you're connecting to SQL Server, you might see this failure because the default settings under SQL Server don't allow remote connections: `provider: Named Pipes Provider, error: 40 - Couldn't open a connection to SQL Server`. The instance of the Database Engine hosting the report server database returns this error. In most cases, the SQL Server service stops, leading to this error. If you use SQL Server Express with Advanced Services or a named instance, this error occurs if the report server URL or connection string for the report server database isn't correct. To work through these issues, do the following tasks:

- Verify that the SQL Server (**MSSQLSERVER**) service is started.

    1. On the computer that hosts the instance of the Database Engine, select **Start** > **Administrative Tools** > **Services**, and scroll to SQL Server (**MSSQLSERVER**).
    1. If it's not started, right-click the service and select **Properties**.
    1. In **Startup Type** choose **Automatic**
    1. Select **Apply** and then choose **Start**.
    1. elect **OK**.

- Verify that the report server URL and report server database connection string is correct. If Reporting Services or the Database Engine was installed as a named instance, the default connection string that's created during Setup includes the instance name. For example, if you installed a default instance of SQL Server Express with Advanced Services on a server named DEVSRV01, the web portal URL is DEVSRV01\Reports$SQLEXPRESS. Furthermore, the database server name in the connection string resembles DEVSRV01\SQLEXPRESS. For more information about URLs and data source connection strings for SQL Server Express, see [Reporting Services in SQL Server Express with Advanced Services](/previous-versions/sql/sql-server-2008-r2/ms365166(v=sql.105)). To verify the connection string for the report server database, start the Reporting Services Configuration tool and view the Database Setup page.

### A connection can't be made. Ensure that the server is running

The ADOMD.NET provider returns this error. There are several reasons why this error can occur. If you specified the server as "localhost," try specifying the server name instead. This error can also occur if memory can't be allocated to the new connection. For more information, see [FIX: Error message when you connect to an instance of SQL Server 2005 Analysis Services: "A connection cannot be made. Ensure that the server is running."](https://www.betaarchive.com/wiki/index.php?title=Microsoft_KB_Archive/912017).

If the error also includes "No such host is known," it indicates that the Analysis Services server isn't available or is refusing the connection. If the Analysis Services server is installed as a named instance on a remote computer, you might have to run the SQL Server Browser service to get the port number used by that instance.

### Report Services SOAP proxy source

If the additional information section includes "SQL Server don't exist or access denied," you might be encountering the following conditions with this error:

- The connection string for the data source includes "localhost."
- TCP/IP is disabled for the SQL Server service.

To resolve this error, you can either modify the connection string to use the server name, or you can enable TCP/IP for the service. To enable TCP/IP, follow these steps:

1. Start SQL Server Configuration Manager.
1. Select **SQL Server Network Configuration**.
1. Right-click **Protocols for MSSQLSERVER**.
1. Select **Open**.
1. Right-click **TCP/IP**, and select **Enable**.
1. Select **SQL Server Services**.
1. Right-click **SQL Server (MSSQLSERVER)**, and select **Restart**.

## WMI error when connecting to a report server in Management Studio

By default, Management Studio uses the Reporting Services Windows Management Instrumentation (WMI) provider to establish a connection to the report server. If the WMI provider isn't installed correctly, you get the following error when attempting to connect to the report server:

Can't connect to \<your server name>. The Reporting Services WMI provider isn't installed or is misconfigured (Microsoft.SqlServer.Management.UI.RSClient).

To resolve this error, you should reinstall the software. For all other cases, as a temporary work-around, you can connect to the report server through the SOAP endpoint:

- In the **Connect to Server** dialog box in Management Studio, in **Server Name**, enter the report server URL. By default, the URL is `https://<your server name>/reportserver`. Or, if you use SQL Server 2008 Express with Advanced Services, the URL is `https://<your server name>/reportserver$sqlexpress`.

To resolve the error so that you can connect by using the WMI provider, run Setup to repair Reporting Services. Or reinstall Reporting Services.

## Connection error, where sign in failed due to unknown user name or bad password

An **rsReportServerDatabaseLogonFailed** error can occur if you use a domain account for the connection from the report server to the report server database connection, and the password for the domain account was changed.

The full error text is: `The report server can't open a connection to the report server database. The logon failed (**rsReportServerDatabaseLogonFailed**). Logon failure: unknown user name or bad password.`

If you reset the password, you must update the connection. For more information, see [Configure a report server database connection (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).

## The report server can't open a connection to the report server database. (rsReportServerDatabaseUnavailable)

The full error text is: `The report server can't open a connection to the report server database. A connection to the database is required for all requests and processing. (rsReportServerDatabaseUnavailable)`

This error occurs when the report server can't connect to the SQL Server relational database that provides internal storage to the server. The connection to the report server database is managed through the Reporting Services Configuration tool. You can run the tool, go to the Database Setup page, and correct the connection information. Use the tool to update connection information. The tool ensures that dependent settings are updated and that services are restarted. For more information, see [Configure a report server database connection (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md) and [Configure the report server service account (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).

This error can also occur if the Database Engine instance that hosts the report server database isn't configured for remote connections. Remote connection is enabled by default in some editions of SQL Server. To verify whether the remote connection is enabled on the SQL Server Database Engine instance you use, run the SQL Server Configuration Manager tool. You must enable both TCP/IP and named pipes. A report server uses both protocols. For instructions on how to enable remote connections, see [Configure remote connections to the report server database](../report-server/configure-a-report-server-for-remote-administration.md#configure-remote-connections-to-the-report-server-database).

If the error includes the following text, the password expired on the account used to run the Database Engine instance: "An error occurred while establishing a connection to the server. When you're connecting to SQL Server, this failure might occur because under the default settings, SQL Server doesn't permit remote connections: `provider: SQL Server Network Interfaces, error: 26 - Error Locating Server/Instance Specified.` To resolve this error, reset the password.

## "RPC Server isn't listening"

The Report Server service uses Remote Procedure Call (RPC) server for some operations. If you get the "RPC Server isn't listening" error, verify that the Report Server service is running.

## Unexpected error (General network error)

This error indicates a data source connection error. You should check the connection string, and verify that you have permission to access the data source. If you use Windows Authentication to access a data source, you must have permission to access the computer that hosts the data source.

## Unable to grant database access in SharePoint central administration

When you configured Reporting Services to integrate with a SharePoint product or technology on Windows Vista or Windows Server 2008, you might receive the following error message when you try to grant access on the **Grant Database Access** page in SharePoint Central Administration: `A connection to the computer can't be established.`

This error happens because User Account Control (UAC) in Windows Vista and Windows Server 2008 requires explicit acceptance from an administrator to elevate. You need the acceptance to use the administrator token when performing tasks that require administrator permissions. In this case, however, the Windows SharePoint Services Administration service can't be elevated to grant the Reporting Services service account or accounts access to the SharePoint configuration and content databases.

In SQL Server 2008 Reporting Services, only the Report Server service account requires database access. In SQL Server 2005 Reporting Services SP2, both the Report Server Windows service account and the Report Server Web service account require database access.

There are two workarounds for this issue.

- You can temporarily turn off UAC and use SharePoint Central Administration to grant access.

    > [!IMPORTANT]
    > Use caution if you turn off UAC to work around this issue, and turn on UAC immediately after you grant database access in SharePoint Central Administration. If you don't want to turn off UAC, use the second workaround provided in this section. For information about UAC, see the Windows product documentation.

- You can manually grant database access to the Reporting Services service account or accounts. You can use the following procedure to grant access by adding the Reporting Services service account or accounts to the correct Windows group and database roles. This procedure applies to the Report Server service account in SQL Server 2008 Reporting Services. If you're running SQL Server 2005 Reporting Services, perform the procedure for the Report Server Windows service account and the Report Server Web service account.

### Manually grant database access

1. Add the Report Server service account to the WSS_WPG Windows group on the Reporting Services computer.
1. Connect to the database instance that hosts the SharePoint configuration and content databases, and create an SQL database sign-in for the Report Server service account.
1. Add the SQL database sign-in to the following database roles:

    -db_owner role in the Windows SharePoint Services (WSS) Content database
    -WSS_Content_Application_Pools role in the SharePoint_Config database

## Unable to connect to the /reports and /reportserver directories. 

This issue occurs when the report server databases are created on a virtual SQL Server that runs in a Microsoft Cluster Services (MSCS) cluster.

When you create the report server databases, **ReportServer** and **ReportServerTempDB**, on a virtual SQL Server that runs in an MSCS cluster, the remote name in the format `<domain>\<computer_name>$` might not be registered to SQL Server as a sign-in. If you configure the report server service account as an account that requires this remote name for connections, users can't connect to the `/reports` and `/reportserver` directories in Reporting Services. For example, the built-in Windows account NetworkService requires this remote name. To avoid this issue, use an explicit domain account or a SQL Server sign-in to connect to the report server databases.

## Related content

- [Errors and events reference (Reporting Services)](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)
- [Troubleshoot data retrieval issues with Reporting Services reports](../../reporting-services/troubleshooting/troubleshoot-data-retrieval-issues-with-reporting-services-reports.md)
- [Troubleshoot Reporting Services subscriptions and delivery](../../reporting-services/troubleshooting/troubleshoot-reporting-services-subscriptions-and-delivery.md)

[!INCLUDE[feedback-qa-stackoverflow-md](../../includes/feedback-qa-stackoverflow-md.md)]

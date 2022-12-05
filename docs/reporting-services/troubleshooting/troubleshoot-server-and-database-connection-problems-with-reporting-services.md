---
title: "Troubleshoot Server & Database Connection Problems"
description: "In this article, diagnose and fix problems you experience when you're connecting to a report server. Also learn about 'Unexpected error' messages."
ms.date: 12/16/2019
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.assetid: 8bbb88df-72fd-4c27-91b7-b255afedd345
author: maggiesMSFT
ms.author: maggies
---
# Troubleshoot Server & Database Connection Problems with Reporting Services
Use this topic to troubleshoot problems that you experience when you're connecting to a report server. This topic also provides information about "Unexpected error" messages. For more information about data source configuration and configuring report server connection information, see [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md) and [Configure a Report Server Database Connection (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).  
  
## Cannot create a connection to data source 'datasourcename'. (rsErrorOpeningConnection)  
This is a generic error that occurs when the report server can't open a connection to an external data source that provides data to a report. This error appears with a second error message that indicates the underlying cause. The following additional errors can appear with **rsErrorOpeningConnection**.  
  
### Login failed for user 'UserName'  
The user doesn't have permission to access the data source. If you're using a SQL Server database, verify that the user has a valid database user login. For more information about how to create a database user or a SQL Server login, see [Create a Database User](../../relational-databases/security/authentication-access/create-a-database-user.md) and [Create a SQL Server Login](../../relational-databases/security/authentication-access/create-a-login.md).  
  
### Login failed for user 'NT AUTHORITY\ANONYMOUS LOGON'  
This error occurs when credentials are passed across multiple computer connections. If you're using Windows Authentication, and the Kerberos version 5 protocol is not enabled, this error occurs when credentials are passed across more than one computer connection. To work around this error, consider using stored credentials or prompted credentials. For more information about how to work around this issue, see [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md).  
  
### An error has occurred while establishing a connection to the server.  
When you're connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server don't allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server). This error is returned by the instance of the Database Engine that hosts the report server database. In most cases, this error occurs because the SQL Server service is stopped. Or, if you're using SQL Server Express with Advanced Services or a named instance, this error will occur if the report server URL or connection string for the report server database is not correct. To work through these issues, do the following:  
  
* Verify that the SQL Server (**MSSQLSERVER**) service is started. On the computer that hosts the instance of the Database Engine, click Start, click Administrative Tools, click Services, and scroll to SQL Server (**MSSQLSERVER**). If it is not started, right-click the service, select Properties, in Startup Type select Automatic, click Apply, click Start, and then click OK.   
* Verify that the report server URL and report server database connection string is correct. If Reporting Services or the Database Engine was installed as a named instance, the default connection string that is created during Setup will include the instance name. For example, if you installed a default instance of SQL Server Express with Advanced Services on a server named DEVSRV01, the web portal URL is DEVSRV01\Reports$SQLEXPRESS. Furthermore, the database server name in the connection string will resemble DEVSRV01\SQLEXPRESS. For more information about URLs and data source connection strings for SQL Server Express, see [Reporting Services in SQL Server Express with Advanced Services](/previous-versions/sql/sql-server-2008-r2/ms365166(v=sql.105)). To verify the connection string for the report server database, start the Reporting Services Configuration tool and view the Database Setup page.  
  
### A connection can't be made. Ensure that the server is running.  
This error is returned by ADOMD.NET provider. There are several reasons why this error can occur. If you specified the server as "localhost", try specifying the server name instead. This error can also occur if memory can't be allocated to the new connection. For more information, see [Knowledge Base Article 912017 - Error message when you connect to an instance of SQL Server 2005 Analysis Services:](https://www.betaarchive.com/wiki/index.php?title=Microsoft_KB_Archive/912017).  
  
If the error also includes "No such host is known", it indicates that the Analysis Services server is not available or is refusing the connection. If the Analysis Services server is installed as a named instance on a remote computer, you might have to run the SQL Server Browser service to get the port number used by that instance.  
  
### (Report Services SOAP Proxy Source)  
If you get this error during report model generation, and the additional information section includes "SQL Server don't exist or access denied", you might be encountering the following conditions:  
* The connection string for the data source includes "localhost".  
* TCP/IP is disabled for the SQL Server service.  
  
To resolve this error, you can either modify the connection string to use the server name or you can enable TCP/IP for the service. Follow these steps to enable TCP/IP:  
  
1. Start SQL Server Configuration Manager.  
2. Expand **SQL Server Network Configuration**.  
3. Select **Protocols for MSSQLSERVER**.  
4. Right-click **TCP/IP**, and select **Enable**.  
5. Select **SQL Server Services**.  
6. Right-click **SQL Server (MSSQLSERVER)**, and select **Restart**.  
  
## WMI error when connecting to a report server in Management Studio  
By default, Management Studio uses the Reporting Services Windows Management Instrumentation (WMI) provider to establish a connection to the report server. If the WMI provider is not installed correctly, you will get the following error when attempting to connect to the report server:  
  
Cannot connect to \<your server name>. The Reporting Services WMI provider is not installed or is misconfigured (Microsoft.SqlServer.Management.UI.RSClient).  
  
To resolve this error, you should reinstall the software. For all other cases, as a temporary work-around, you can connect to the report server through the SOAP endpoint:  
  
* In the **Connect to Server** dialog box in Management Studio, in **Server Name**, type the report server URL. By default, it is `https://<your server name>/reportserver`. Or if you're using SQL Server 2008 Express with Advanced Services, it is `https://<your server name>/reportserver$sqlexpress`.  
  
To resolve the error so that you can connect using the WMI provider, you should run Setup to repair Reporting Services, or reinstall Reporting Services.  
  
## Connection error, where login failed due to unknown user name or bad password  
An **rsReportServerDatabaseLogonFailed** error can occur if you're using a domain account for the connection from the report server to the report server database connection, and the password for the domain account has been changed.   
  
The full error text is: "The report server can't open a connection to the report server database. The logon failed (**rsReportServerDatabaseLogonFailed**). Logon failure: unknown user name or bad password."  
  
If you reset the password, you must update the connection. For more information, see [Configure a Report Server Database Connection (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).  
  
## The report server can't open a connection to the report server database. (rsReportServerDatabaseUnavailable).  
Full Message: The report server can't open a connection to the report server database. A connection to the database is required for all requests and processing. (rsReportServerDatabaseUnavailable)  
This error occurs when the report server can't connect to the SQL Server relational database that provides internal storage to the server. The connection to the report server database is managed through the Reporting Services Configuration tool. You can run the tool, go to the Database Setup page, and correct the connection information. Using the tool to update connection information is a best practice; the tool ensures that dependent settings are updated and that services are restarted. For more information, see [Configure a Report Server Database Connection](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md) and [Configure the Report Server Service Account](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).  
  
This error can also occur if the Database Engine instance that hosts the report server database is not configured for remote connections. Remote connection is enabled by default in some editions of SQL Server. To verify whether it is enabled on the SQL Server Database Engine instance you're using, run the SQL Server Configuration Manager tool. You must enable both TCP/IP and named pipes. A report server uses both protocols. For instructions on how to enable remote connections, see the section "How to Configure Remote Connections to the Report Server Database" in [Configure a Report Server for Remote Administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md).  
  
If the error includes the following additional text, the password expired on the account used to run the Database Engine instance: "An error has occurred while establishing a connection to the server. When you're connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server don't permit remote connections. (**provider: SQL Server Network Interfaces, error: 26 - Error Locating Server/Instance Specified)**." To resolve this error, reset the password.   
  
## "RPC Server is not listening"  
The Report Server service uses Remote Procedure Call (RPC) server for some operations. If you get the "RPC Server is not listening" error, verify that the Report Server service is running.  
  
## Unexpected error (General network error)  
This error indicates a data source connection error. You should check the connection string, and verify that you have permission to access the data source. If you're using Windows Authentication to access a data source, you must have permission to access the computer that hosts the data source.  
  
## Unable to grant database access in SharePoint Central Administration  
When you have configured Reporting Services to integrate with a SharePoint product or technology on Windows Vista or Windows Server 2008, you might receive the following error message when you try to grant access on the **Grant Database Access** page in SharePoint Central Administration: "A connection to the computer can't be established."  
  
This happens because User Account Control (UAC) in Windows Vista and Windows Server 2008 requires explicit acceptance from an administrator to elevate and use the administrator token when performing tasks that require administrator permissions. In this case, however, the Windows SharePoint Services Administration service can't be elevated to grant the Reporting Services service account or accounts access to the SharePoint configuration and content databases.  
  
In SQL Server 2008 Reporting Services, only the Report Server service account requires database access; in SQL Server 2005 Reporting Services SP2, both the Report Server Windows service account and the Report Server Web service account require database access. For more information about the Report Server service account in SQL Server 2008, see Service Account (Reporting Services Configuration).  
  
There are two workarounds for this issue.   
1.  In one workaround, you can temporarily turn off UAC and use SharePoint Central Administration to grant access.  
> [!IMPORTANT]  
> Use caution if you turn off UAC to work around this issue, and turn on UAC immediately after you grant database access in SharePoint Central Administration. If you don't want to turn off UAC, use the second workaround provided in this section. For information about UAC, see the Windows product documentation.  
2. In the second workaround, you can manually grant database access to the Reporting Services service account or accounts. You can use the following procedure to grant access by adding the Reporting Services service account or accounts to the correct Windows group and database roles. This procedure applies to the Report Server service account in SQL Server 2008 Reporting Services; if you're running SQL Server 2005 Reporting Services, perform the procedure for the Report Server Windows service account and the Report Server Web service account.   
  
### To manually grant database access  
  
1. Add the Report Server service account to the WSS_WPG Windows group on the Reporting Services computer.  
2. Connect to the database instance that hosts the SharePoint configuration and content databases, and create a SQL database login for the Report Server service account.  
3. Add the SQL database login to the following database roles:  
  
* db_owner role in the WSS Content database  
* WSS_Content_Application_Pools role in the SharePoint_Config database  
  
## Unable to connect to the /reports and /reportserver directories when the report server databases are created on a virtual SQL Server that runs in a Microsoft Cluster Services (MSCS) cluster  
When you create the report server databases, **ReportServer** and **ReportServerTempDB**, on a virtual SQL Server that runs in an MSCS cluster, the remote name in the format `<domain>\<computer_name>$` might not be registered to SQL Server as a login. If you have configured the Report Server service account as an account that requires this remote name for connections, users can't connect to the /reports and /reportserver directories in Reporting Services. For example, the built-in Windows account NetworkService requires this remote name. To avoid this issue, use an explicit domain account or a SQL Server login to connect to the report server databases.  
    
  ## See Also  
[Browser Support for Reporting Services](../../reporting-services/browser-support-for-reporting-services-and-power-view.md)  
[Errors and events (Reporting Services)](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)  
[Troubleshoot Data Retrieval issues with Reporting Services Reports](../../reporting-services/troubleshooting/troubleshoot-data-retrieval-issues-with-reporting-services-reports.md)  
[Troubleshoot Reporting Services Subscriptions and Delivery](../../reporting-services/troubleshooting/troubleshoot-reporting-services-subscriptions-and-delivery.md)  
  
  
  

[!INCLUDE[feedback_stackoverflow_msdn_connect](../../includes/feedback-stackoverflow-msdn-connect-md.md)]

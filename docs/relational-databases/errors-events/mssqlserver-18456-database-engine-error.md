---
title: MSSQLSERVER_18456
description: A connection attempt is rejected due to a failure with a bad password or username in SQL Server. See an explanation of the error and possible resolutions.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: jopilov, randolphwest
ms.date: 01/16/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "18456 (Database Engine error)"
---

# MSSQLSERVER_18456

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 18456 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | LOGON_FAILED |
| Message Text | Login failed for user '%.*ls'.%.\*ls |

## Explanation

You get this error message when a connection attempt is rejected because of an authentication failure. User logins can fail for many reasons, such as invalid credentials, password expiration, and enabling the wrong authentication mode. In many cases, error codes include descriptions.

## User action

The following examples are some of the common login failures. Select the exact error that you're experiencing to troubleshoot the issue:

- [Login failed for user '\<username>' or login failed for user '\<domain>\\\<username>'](#login-failed-for-user-username-or-login-failed-for-user-domainusername)

- [Login failed for user 'NT AUTHORITY\ANONYMOUS' LOGON](#login-failed-for-user-nt-authorityanonymous-logon)

- [Login failed for user 'empty'](#login-failed-for-user-empty)

- [Login failed for user '(null)'](#login-failed-for-user-null)

## Login failed for user '\<username>' or login failed for user '\<domain>\\\<username>'

If the domain name isn't specified, the problem is a failing SQL Server login attempt. If the domain name is specified, the problem is a failing Windows user account login. For potential causes and suggested resolutions, see:

| Potential cause | Suggested resolution |
| - | - |
| You're trying to use **SQL Server Authentication**, but the SQL server instance is configured for Windows Authentication mode. | Verify that SQL Server is configured to use **SQL Server and Windows Authentication mode**. You can review and change the authentication mode for your SQL Server instance on the **Security** page under **Properties** for the corresponding instance in SQL Server Management Studio (SSMS). For more information, see [Change server authentication mode](../../database-engine/configure-windows/change-server-authentication-mode.md). Alternatively, you can change your application to use **Windows Authentication mode** to connect to SQL Server.<br />**Note**: You can see a message like the following one in the SQL Server Error log for this scenario:<br />`Login failed for user '<UserName>'. Reason: An attempt to login using SQL authentication failed. Server is configured for Windows authentication only.` |
| Login doesn't exist on the SQL Server instance you're trying to connect to. | Verify that the SQL Server login exists and that you've spelled it properly. If the login doesn't exist, create it. If it's present but misspelled, correct that in the application connection string. The SQL Server Errorlog will have one of the following messages:<br />- `Login failed for user 'username'. Reason: Could not find a login matching the name provided.`<br />- `Login failed for user 'Domain\username'. Reason: Could not find a login matching the name provided.`</li>This can be a common issue if you deploy an application that uses a DEV or QA server into production and you fail to update the connection string. To resolve this issue, validate that you are connecting to the appropriate server. If not, correct the connection string. If it is, grant the login access to your SQL Server. Or if it's a windows login grant access directly or add it to a local or domain group that is allowed to connect to the database server. For more information, see [Create a Login](../security/authentication-access/create-a-login.md). |
| You're using **SQL Server Authentication**, but the password you specified for SQL Server login is incorrect. | Check the SQL error log for messages like "*Reason: Password did not match that for the login provided*" to confirm the cause. To fix the issue, use the correct password in your application or use a different account if you can't remember the password. Alternatively, work with your SQL Server administrator to reset the password for the account.<br />If the application is SQL Server Integration Services (SSIS), there may be multiple levels of a Configuration file for the job, which may override the Connection Manager settings for the package.<br />If the application was written by your company and the connection string is programmatically generated, engage the development team to resolve the issue. As a temporary workaround, hard-code the connection string and test. Use a [UDL file](/troubleshoot/sql/connect/test-oledb-connectivity-use-udl-file) or a script to prove a connection is possible with a hard-coded connection string. |
| Server name is incorrect. | Ensure you're connecting to the correct server. |
| You're trying to connect using Windows authentication but are logged into an incorrect domain. | Verify that you're properly logged into the correct domain. The error message usually displays the domain name. |
| You aren't running your application (for example, SSMS) as an administrator. | If you're trying to connect using your administrator credentials, start your application by using the **Run as Administrator** option. When connected, add your Windows user as an individual login. |
| Login is deleted after a migration to a contained database user. | If the Database Engine supports contained databases, confirm that the login wasn't deleted after migration to a contained database user. For more information, see [Contained Database Authentication: Introduction](https://techcommunity.microsoft.com/t5/sql-server-blog/contained-database-authentication-introduction/ba-p/383696). |
| Login's default database is offline or otherwise not available. | Check with your SQL Server administrator and resolve issues related to database availability. If the login has permissions to other databases on the server and you don't need to access the currently configured default database in your application, use one of the following options:<br />- Request the administrator to change the default database for the login using [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) statement or SSMS.<br />- Explicitly specify a different database in your application [connection string](../../connect/homepage-sql-connection-programming.md). Or if you're using SSMS switch to the [Connection Properties](../../ssms/f1-help/connect-to-server-connection-properties-page-database-engine.md) tab to specify a database that is currently available.</li>Applications like SSMS may show an error message like the following one:<br />`Cannot open user default database. Login failed.`<br />`Login failed for user <user name>. (Microsoft SQL Server, Error: 4064)`<br />SQL Server Errorlog will have an error message like the following one:<br />`Login failed for user '<user name>'. Reason: Failed to open the database '<dbname>' specified in the login properties [CLIENT: <ip address>]`<br />For more information, see [MSSQLSERVER_4064](./mssqlserver-4064-database-engine-error.md). |
| The database explicitly specified in the connection string or in SSMS is incorrectly spelled, offline, or otherwise not available. | - Fix the database name in the connection string. Pay attention to case sensitivity if using a case sensitive collation on the server.<br />- If the database name is correct, check with your SQL Server administrator and resolve issues related to database availability. Check if the database is offline, not recovered, and so on.<br />- If the login has been mapped to users with permissions to other databases on the server and you don't need to access the currently configured database in your application, then specify a different database in your [connection string](../../connect/homepage-sql-connection-programming.md). Or if you're connecting with SSMS, use the [Connection Properties](../../ssms/f1-help/connect-to-server-connection-properties-page-database-engine.md) tab to specify a database that is currently available.<br />SQL Server Errorlog will have an error message like the following one:<br />`Login failed for user <UserName>. Reason: Failed to open the explicitly specified database 'dbname'. [CLIENT: <ip address>]`<br />**Note**: If the login's default database is available, SQL Server allows the connection to succeed. For more information, see [MSSQLSERVER_4064](./mssqlserver-4064-database-engine-error.md). |
| The user doesn't have permissions to the requested database. | - Try connecting as another user that has sysadmin rights to see if connectivity can be established.<br />- Grant the login access to the database by creating the corresponding user (for example, `CREATE USER [<UserName>] FOR LOGIN [UserName]`) |

Also, check the extensive list of error codes at [Troubleshooting Error 18456](https://sqlblog.org/2020/07/28/troubleshooting-error-18456).

For more troubleshooting help, see [Troubleshooting SQL Client / Server Connectivity Issues](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/0000-Troubleshooting-Workflows).

## Login failed for user NT AUTHORITY\ANONYMOUS LOGON

There are at least four scenarios for this issue. In the following table, examine each applicable potential cause, and use the appropriate resolution:
See the note below the table for an explanation of the term *double hop*.

| Potential causes | Suggested resolutions |
| --- | --- |
| You're trying to pass NT LAN Manager (NTLM) credentials from one service to another service on the same computer (for example: from IIS to SQL server), but a failure occurs in the process. | Add the [DisableLoopbackCheck or BackConnectionHostNames](/troubleshoot/windows-server/networking/accessing-server-locally-with-fqdn-cname-alias-denied) registry entries. |
| There are [double-hop](https://techcommunity.microsoft.com/t5/ask-the-directory-services-team/understanding-kerberos-double-hop/ba-p/395463) (constraint delegation) scenarios across multiple computers. The error could occur if the Kerberos connection fails because of Service Principal Names (SPN) issues. | Run [SQLCheck](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/SQLCHECK) on each SQL Server and the web server. Use the troubleshooting guides: [0600 Credential Delegation Issue](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/0600-Credential-Delegation-Issue) and [0650 SQL Server Linked Server Delegation Issues](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/0650-SQL-Server-Linked-Server-Delegation-Issues). |
| If no double-hop (constraint delegation) is involved, then likely duplicate SPNs exist, and the client is running as a LocalSystem or another machine account that gets NTLM credentials instead of Kerberos credentials. | Use [SQLCheck](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/SQLCHECK) or [Setspn.exe](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/cc731241(v=ws.11)) to diagnose and fix any SPN-related issues. Also review [Overview of the Kerberos Configuration Manager for SQL Server](/troubleshoot/sql/connect/using-kerberosmngr-sqlserver). |
| Windows Local Security policy may have been configured to prevent the use of the machine account for remote authentication requests. | Navigate to **Local Security Policy** > **Local Policies** > **Security Options** > **Network security: Allow Local System to use computer identity for NTLM**, select the **Enabled** option if the setting is disabled, and then select **OK**.<br />**Note**: As detailed on the **Explain** tab, this policy is enabled in Windows 7 and later versions by default. |
| Intermittent occurrence of this issue when using constrained delegation can indicate presence of an expired ticket that can't be renewed by middle tier. This is an expected behavior with either linked server scenario or any application that is holding a logon session for more than 10 hours. | Change delegation settings on your middle-tier server from **Trust this computer for delegation to specified services only – Use Kerberos Only** to **Trust this computer for delegation to specified services only - Use any protocol.** For more information review [Intermittent ANONYMOUS LOGON of SQL Server linked server double hop](https://techcommunity.microsoft.com/t5/sql-server-support-blog/intermittent-anonymous-logon-of-sql-server-linked-server-double/ba-p/3694876). |

> [!NOTE]  
> A double-hop typically involves delegation of user credentials across multiple remote computers. For example, assume you have a SQL Server instance named *SQL1* where you created a linked server for a remote SQL Server named *SQL2*. In linked server security configuration, you selected the option **[Be made using the login's current security context](../linked-servers/create-linked-servers-sql-server-database-engine.md#specify-the-default-security-context-for-logins-not-present-in-the-mapping-list)**. When using this configuration, if you execute a linked server query on *SQL1* from a remote client computer named *Client1*, the windows credentials will first have to hop from *Client1* to *SQL1* and then from *SQL1* to *SQL2* (hence, it's called a double-hop). For more information, see [Understanding Kerberos Double Hop](https://techcommunity.microsoft.com/t5/ask-the-directory-services-team/understanding-kerberos-double-hop/ba-p/395463) and [Kerberos Constrained Delegation Overview](/windows-server/security/kerberos/kerberos-constrained-delegation-overview)

## Login failed for user (empty)

This error occurs when a user tries unsuccessfully to log in. This error might occur if your computer isn't connected to the network. For example, you may receive an error message that resembles the following one:

```Output
Source: NETLOGON
Date: 8/12/2012 8:22:16 PM
Event ID: 5719
Task Category: None
Level: Error
Keywords: Classic
User: N/A
Computer: <computer name>
Description: This computer was not able to set up a secure session with a domain controller in domain due to the following: The remote procedure call was cancelled.
This may lead to authentication problems. Make sure that this computer is connected to the network. If the problem persists, please contact your domain administrator.
```

An empty string means that SQL Server tried to hand off the credentials to the Local Security Authority Subsystem Service (LSASS) but couldn't because of some problem. Either LSASS wasn't available, or the domain controller couldn't be contacted.

Check the event logs on the client and the server for any network-related or Active Directory-related messages that were logged around the time of the failure. If you find any, work with your domain administrator to fix the issues.

## Login failed for user '(null)'

An indication of "null" could mean that LSASS can't decrypt the security token by using the SQL Server service account credentials. The main reason for this condition is that the SPN is associated with the wrong account.

To fix the issue, follow these steps:

1. Use the [SQLCheck](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/SQLCHECK) or [Setspn.exe](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/cc731241(v=ws.11)) to diagnose and fix SPN-related issues.

1. Use [SQLCheck](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/SQLCHECK) to check whether the SQL Service account is trusted for delegation. If the output indicates that the account isn't trusted for delegation, work with your Active Directory administrator to enable delegation for the account.

1. Diagnose and fix Domain Name System (DNS) name resolution issues. For example:

   - Ping IP address by using PowerShell scripts:

     - `ping -a <your_target_machine>` (use `-4` for IPv4 and `-6` IPv6 specifically)
     - `ping -a <your_remote_IPAddress>`

   - Use [NSLookup](/windows-server/administration/windows-commands/nslookup) to enter your local and remote computer name and IP address multiple times.

1. Look for any discrepancies and mismatches in the returned results. The accuracy of the DNS configuration on the network is important for a successful SQL Server connection. An incorrect DNS entry could cause numerous connectivity issues later.

1. Make sure that firewalls or other network devices don't block a client from connecting to the domain controller. SPNs are stored in Active Directory. If the clients can't communicate with the directory, the connection can't succeed.

## Additional error information

To increase security, the error message that is returned to the client deliberately hides the nature of the authentication error. However, in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, a corresponding error contains an error state that maps to an authentication failure condition. Compare the error state to the following list to determine the reason for the login failure.

| State | Description |
| --- | --- |
| 1 | Error information isn't available. This state usually means you don't have permission to receive the error details. Contact your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrator for more information. |
| 2 | User ID isn't valid. |
| 5 | User ID isn't valid. |
| 6 | An attempt was made to use a Windows login name with SQL Server Authentication. |
| 7 | Login is disabled, and the password is incorrect. |
| 8 | The password is incorrect. |
| 9 | Password isn't valid. |
| 11 | Login is valid, but server access failed. One possible cause of this error is when the Windows user has access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a member of the local administrators' group, but Windows isn't providing administrator credentials. To connect, start the connecting program using the **Run as administrator** option, and then add the Windows user to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a specific login. |
| 12 | Login is valid login, but server access failed. |
| 18 | Password must be changed. |
| 38, 46 | Couldn't find database requested by user. |
| 58 | When SQL Server is set to use Windows Authentication only, and a client attempts to log in using SQL authentication. Another cause is when SIDs don't match. |
| 102 - 111 | Azure AD failure. |
| 122 - 124 | Failure due to empty user name or password. |
| 126 | Database requested by user doesn't exist. |
| 132 - 133 | Azure AD failure. |

Other error states exist and signify an unexpected internal processing error.

### More rare possible cause

The error reason **An attempt to login using SQL authentication failed. Server is configured for Windows authentication only.** can be returned in the following situations.

- When the server is configured for mixed mode authentication, and an ODBC connection uses the TCP protocol, and the connection doesn't explicitly specify that the connection should use a trusted connection.

- When SQL server is configured for mixed mode authentication, and an ODBC connection uses named pipes, and the credentials the client used to open the named pipe are used to automatically impersonate the user, and the connection string doesn't explicitly specify the use of a trusted authentication.

To resolve this issue, include `TRUSTED_CONNECTION = TRUE` in the connection string.

## Examples

In this example, the authentication error state is 8. This indicates that the password is incorrect.

| Date | Source | Message |
| --- | --- | --- |
| 2007-12-05 20:12:56.34 | Logon | Error: 18456, Severity: 14, State: 8. |
| 2007-12-05 20:12:56.34 | Logon | Login failed for user '<user_name>'. [CLIENT: \<ip address\>] |

> [!NOTE]  
> When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed using Windows Authentication mode and is later changed to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Authentication mode, the **sa** login is initially disabled. This causes the state 7 error: "Login failed for user 'sa'." To enable the **sa** login, see [Change Server Authentication Mode](~/database-engine/configure-windows/change-server-authentication-mode.md).

## See also

- [0420 Reasons for Consistent Auth Issues](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/0420-Reasons-for-Consistent-Auth-Issues)

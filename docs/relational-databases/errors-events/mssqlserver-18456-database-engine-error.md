---
title: "MSSQLSERVER_18456 | Microsoft Docs"
ms.custom: ""
ms.date: "06/09/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "18456 (Database Engine error)"
ms.assetid: c417631d-be1f-42e0-8844-9f92c77e11f7
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_18456
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|18456|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LOGON_FAILED|  
|Message Text|Login failed for user '%.*ls'.%.\*ls|  
  
## Explanation  
When a connection attempt is rejected because of an authentication failure that involves a bad password or user name, a message similar to the following is returned to the client:  "Login failed for user '<user_name>'. (Microsoft SQL Server, Error: 18456)".  
  
Additional information returned to the client includes the following:  
  
"Login failed for user '<user_name>'. (.Net SqlClient Data Provider)"  
  
-----------------------------\-  
  
"Server Name: <computer_name>"  
  
"Error Number: 18456"  
  
"Severity: 14"  
  
"State: 1"  
  
"Line Number: 65536"  
  
The following message might also be returned:  
  
"Msg 18456, Level 14, State 1, Server <computer_name>, Line 1"  
  
"Login failed for user '<user_name>'."  
  
## Additional Error Information  
To increase security, the error message that is returned to the client deliberately hides the nature of the authentication error. However, in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, a corresponding error contains an error state that maps to an authentication failure condition. Compare the error state to the following list to determine the reason for the login failure.  
  
|State|Description|  
|---------|---------------|  
|1|Error information is not available. This state usually means you do not have permission to receive the error details. Contact your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrator for more information.|  
|2|User ID is not valid.|  
|5|User ID is not valid.|  
|6|An attempt was made to use a Windows login name with SQL Server Authentication.|  
|7|Login is disabled, and the password is incorrect.|  
|8|The password is incorrect.|  
|9|Password is not valid.|  
|11|Login is valid, but server access failed. One possible cause of this error is when the Windows user has access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a member of the local administrators group, but Windows is not providing administrator credentials. To connect, start the connecting program using the **Run as administrator** option, and then add the Windows user to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a specific login.|  
|12|Login is valid login, but server access failed.|  
|18|Password must be changed.|  
|38, 46|Could not find database requested by user.|
|102 - 111|AAD failure.|
|122 - 124|Failure due to empty user name or password.|
|126|Database requested by user does not exist.|
|132 - 133|AAD failure.|
  
Other error states exist and signify an unexpected internal processing error.  
  
**An additional unusual possible cause**  
  
The error reason **An attempt to login using SQL authentication failed. Server is configured for Windows authentication only.** can be returned in the following situations.  
  
-   When the server is configured for mixed mode authentication, and an ODBC connection uses the TCP protocol, and the connection does not explicitly specify that the connection should use a trusted connection.  
  
-   When the server is configured for mixed mode authentication, and an ODBC connection uses named pipes, and the credentials the client used to open the named pipe are used to automatically impersonate the user, and the connection does not explicitly specify that the connection should use a trusted connection.  
  
To resolve this issue, include **TRUSTED_CONNECTION = TRUE** in the connection string.  
  
## Examples  
In this example, the authentication error state is 8. This indicates that the password is incorrect.  
  
|Date|Source|Message|  
|--------|----------|-----------|  
|2007-12-05 20:12:56.34|Logon|Error: 18456, Severity: 14, State: 8.|  
|2007-12-05 20:12:56.34|Logon|Login failed for user '<user_name>'. [CLIENT: <ip address>]|  
  
> [!NOTE]  
> When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed using Windows Authentication mode and is later changed to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Authentication mode, the **sa** login is initially disabled. This causes the state 7 error: "Login failed for user 'sa'." To enable the **sa** login, see [Change Server Authentication Mode](~/database-engine/configure-windows/change-server-authentication-mode.md).  
  
## User Action  
If you are trying to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, verify that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured in Mixed Authentication Mode.  
  
If you are trying to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, verify that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login exists and that you have spelled it properly.  
  
If you are trying to connect using Windows Authentication, verify that you are properly logged into the correct domain.  
  
If your error indicates state 1, contact your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrator.  
  
If you are trying to connect using your administrator credentials, start you application by using the **Run as Administrator** option. When connected, add your Windows user as an individual login.  
  
If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] supports contained databases, confirm that the login was not deleted after migration to a contained database user.  
  
When connecting locally to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], connections from services running under **NT AUTHORITY\NETWORK SERVICE** must authenticate using the computers fully qualified domain name. For more information, see [How To: Use the Network Service Account to Access Resources in ASP.NET](https://msdn.microsoft.com/library/ff647402.aspx)  
  

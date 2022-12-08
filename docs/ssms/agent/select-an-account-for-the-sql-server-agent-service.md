---
title: Select an Account for the SQL Server Agent Service
description: Select an Account for the SQL Server Agent Service
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 11/11/2021
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---

# Select an account for the SQL Server Agent service

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

The service startup account defines the Microsoft Windows account in which SQL Server Agent runs and its network permissions. SQL Server Agent runs as a specified user account. You select an account for the SQL Server Agent service by using SQL Server Configuration Manager, where you can choose from the following options:  
  
- **Built-in account**. You can choose from a list of the following built-in Windows service accounts:  
  
  - **Local System** account. The name of this account is NT AUTHORITY\System. it's a powerful account that has unrestricted access to all local system resources. it's a member of the Windows **Administrators** group on the local computer.
  
      > [!IMPORTANT]  
      > The **Local System account** option is provided for backward compatibility only. The Local System account has permissions that SQL Server Agent does not require. Avoid running SQL Server Agent as the Local System account. For improved security, use a Windows domain account with the permissions listed in the following section, "Windows Domain Account Permissions."  
  
- **This account**. Lets you specify the Windows domain account in which the SQL Server Agent service runs. We recommend choosing a Windows user account that isn't a member of the Windows **Administrators** group. However, there are limitations for using multiserver administration when the SQL Server Agent service account isn't a member of the local **Administrators** group. For more information, see 'Supported Service Account Types' that follows in this topic.  
  
## Windows domain account permissions

For improved security, select **This account**, which specifies a Windows domain account. The Windows domain account that you specify must have the following permissions:  
  
- In all Windows versions, permission to log on as a service (SeServiceLogonRight)  
  
   > [!NOTE]  
   > The SQL Server Agent service account must be part of the Pre-Windows 2000 Compatible Access group on the domain controller, or jobs that are owned by domain users who are not members of the Windows Administrators group fails.  
  
- In Windows servers, the account that the SQL Server Agent Service runs as requires the following permissions can support SQL Server Agent proxies.  
  
  - Permission to bypass traverse checking (SeChangeNotifyPrivilege)  
  
  - Permission to replace a process-level token (SeAssignPrimaryTokenPrivilege)  
  
  - Permission to adjust memory quotas for a process (SeIncreaseQuotaPrivilege)  
  
  - Permission to access this computer from the network (SeNetworkLogonRight)  
  
> [!NOTE]  
> If the account does not have the permissions required to support proxies, only members of the **sysadmin** fixed server role can create jobs.  
  
> [!NOTE]  
> To receive WMI alert notification, the service account for SQL Server Agent must have been granted permission to the namespace that contains the WMI events, and ALTER ANY EVENT NOTIFICATION.  
  
## SQL Server role membership

The account that the SQL Server Agent service runs as must be a member of the following SQL Server roles:  
  
- To use multiserver job processing, the account must be a member of the **msdb** database role **TargetServersRole** on the master server.  
  
## Supported service account types

The following table lists the Windows account types that can be used for the SQL Server Agent service.  
  
|Service account type|Nonclustered Server|Clustered server|Domain controller (nonclustered)|  
|------------------------|-------------------------|--------------------|--------------------------------------|  
|Microsoft Windows domain account (member of Windows Administrators group)|Supported|Supported|Supported|  
|Windows domain account (non-administrative)|Supported<br /><br />See Limitation 1 below.|Supported<br /><br />See Limitation 1 below.|Supported<br /><br />See Limitation 1 below.|  
|Network Service account (NT AUTHORITY\NetworkService)|Supported<br /><br />See Limitation 1, 3, and 4  below.|Not supported|Not supported|  
|Local user account (non-administrative)|Supported<br /><br />See Limitation 1 below.|Not supported|Not applicable|  
|Local System account (NT AUTHORITY\System)|Supported<br /><br />See Limitation 2 below.|Not supported|Supported<br /><br />See Limitation 2 below.|  
|Local Service account (NT AUTHORITY\LocalService)|Not supported|Not supported|Not supported|  
  
### Limitation 1: Using non-administrative accounts for multiserver administration

Enlisting target servers to a master server may fail with the following error message: "The enlist operation failed."  
  
To resolve this error, restart both the SQL Server and the SQL Server Agent services. For more information, see [Start, Stop, Pause, Resume, Restart the Database Engine,  SQL Server Agent, or  SQL Server  Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).  
  
### Limitation 2: Using the Local System account for multiserver administration

Multiserver administration is supported when the SQL Server Agent service is run under the Local System account only when both the master server and the target server reside on the same computer. If you use this configuration, the following message is returned when you enlist target servers to the master server:  
  
"Ensure the agent start-up account for *<target_server_computer_name>* has rights to log on as targetServer."  
  
You can ignore this informational message. The enlistment operation should complete successfully. For more information, see [Create a Multiserver Environment](../../ssms/agent/create-a-multiserver-environment.md).  
  
### Limitation 3: Using the Network Service account when it's a SQL Server user

  SQL Server Agent may fail to start if you run the SQL Server Agent service under the Network Service account, and the Network Service account has been explicitly granted access to log into a SQL Server  instance as a SQL Server user.  
  
To resolve this, reboot the computer where SQL Server is running. This only needs to be done once.
  
### Limitation 4: Using the Network Service account when SQL Server Reporting Services is running on the same computer

  SQL Server Agent may fail to start if you run the SQL Server Agent service under the Network Service account and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is also running on the same computer.  
  
To resolve this, reboot the computer where SQL Server is running, and then restart both the SQL Server and the SQL Server Agent services. This only needs to be done once.  
  
## Common tasks

**To specify the startup account for the SQL Server Agent service**
  
- [Set the Service Startup Account for SQL Server Agent &#40; SQL Server Configuration Manager&#41;](../../ssms/agent/set-service-startup-account-sql-server-agent-sql-server-configuration-manager.md)  
  
**To specify the mail profile for SQL Server Agent**
  
- [How to: Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md)  
  
> [!NOTE]  
> Use SQL Server Configuration Manager to specify that SQL Server Agent must start up when the operating system starts.  
  
## See also

- [Setting Up Windows Service Accounts](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)  
- [Managing Services Using SQL Computer Manager](../../database-engine/configure-windows/scm-services-connect-to-another-computer.md)  
- [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md)  

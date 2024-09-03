---
title: "Implement SQL Server Agent Security"
description: "Implement SQL Server Agent Security"
author: markingmyname
ms.author: maghan
ms.date: 08/30/2024
ms.service: sql
ms.subservice: ssms
ms.topic: how-to
helpviewer_keywords:
  - "SQL Server Agent, security"
  - "security [SQL Server Agent], about security"
  - "security [SQL Server Agent]"
  - "security [SQL Server], SQL Server Agent"
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Implement SQL Server Agent Security
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent lets the database administrator run each job step in a security context that has only the permissions required to perform that job step, which is determined by a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy. To set the permissions for a particular job step, you create a proxy that has the required permissions and then assign that proxy to the job step. A proxy can be specified for more than one job step. For job steps that require the same permissions, you use the same proxy.  
  
The following section explains what database role you must grant to users so they can create or execute jobs by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
## Granting Access to SQL Server Agent  
To use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, users must be a member of one or more of the following fixed database roles:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
These roles are stored in the **msdb** database. By default, no user is a member of these database roles. Membership in these roles must be granted explicitly. Users who are members of the **sysadmin** fixed server role have full access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, and don't need to be a member of these fixed database roles to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. If a user isn't a member of one of these database roles or of the **sysadmin** role, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node isn't available to them when they connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio.  
  
Members of these database roles can view and execute jobs that they own, and create job steps that run as an existing proxy account. For more information about the specific permissions that are associated with each of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
Members of the **sysadmin** fixed server role have permission to create, modify, and delete proxy accounts. Members of the **sysadmin** role have permission to create job steps that don't specify a proxy, but instead run as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account, which is the account that is used to start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
## Guidelines  
Follow these guidelines to improve the security of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent implementation:  
  
-   Create dedicated user accounts specifically for proxies, and only use these proxy user accounts for running job steps.  
  
-   Only grant the necessary permissions to proxy user accounts. Grant only those permissions required to run the job steps that are assigned to a given proxy account.  
  
-   Don't run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service under a Microsoft Windows account that is a member of the Windows **Administrators** group.  
  
-   Proxies are only as secure as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential store.  
  
-   If user write operations can write to the NT Event log, they can raise alerts via [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
-   Don't specify the NT Admin account as a service account or proxy account.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent have access to each other's assets. The two services share a single process space and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is a sysadmin on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
-   When a [TSX (target server)](./create-a-multiserver-environment.md) enlists with an MSX (master server), the MSX sysadmins gets total control over the TSX instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   ACE is an extension and can't invoke itself. Chainer ScenarioEngine.exe (also known as Microsoft.SqlServer.Chainer.Setup.exe) can invoke ACE. Other host processes can also invoke ACE. 
  
-   ACE depends on the following configuration DLLs owned by SSDP, because those APIs of DLLs are called by ACE:  
  
    -   **SCO** - Microsoft.SqlServer.Configuration.Sco.dll, including new SCO validations for virtual accounts  
  
    -   **Cluster** - Microsoft.SqlServer.Configuration.Cluster.dll  
  
    -   **SFC** - Microsoft.SqlServer.Configuration.SqlConfigBase.dll  
  
    -   **Extension** - Microsoft.SqlServer.Configuration.ConfigExtension.dll  

## Linked servers

In some scenarios, such as with [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview), to run a SQL Agent job that executes a Transact-SQL (T-SQL) query on a remote server through a linked server, you need to map a local login to a login on the remote server. 

Use [sp_addlinkedsrvlogin](../../relational-databases/system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md) to create a mapping between a login on the local server to a login on the remote server that has the necessary permission to execute the T-SQL query. When the SQL Agent job connects to the remote server through the linked server, it executes the T-SQL query in the context of the remote login. 

The following table describes how to map the login based on the SQL Agent job owner in Azure SQL Managed Instance: 

|SQL Agent job owner |How to map the login  |
|---------|---------|
|User that is not **sysadmin**    | Map the [local user](../../relational-databases/system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md#c-map-specific-local-login-to-a-remote-server-login) that owns the SQL Agent job to the remote login.        |
|**sysadmin**     | Map [all local users](../../relational-databases/system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md#d-map-all-local-logins-to-a-remote-server-login) to the remote login by setting the `@locallogin` parameter to `NULL`.     |


> [!NOTE]
> Creating logins on the remote server for SQL Agent jobs is required when the local server is Azure SQL Managed Instance. Failing to map users correctly can result in errors such as the following examples: 
> - `Windows logins are not supported in this version of SQL Server`
> - `Linked servers cannot be used under impersonation without a mapping for the impersonated login`


## Related content
 
- [Using Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md)  
- [sp_addrolemember (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)  
- [sp_droprolemember (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-droprolemember-transact-sql.md)  
- [Security and Protection](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)  

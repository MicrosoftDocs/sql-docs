---
description: "Configure a User to Create and Manage SQL Server Agent Jobs"
title: Configure a User to Create and Manage SQL Server Agent Jobs
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, user configuration"
  - "jobs [SQL Server Agent], user configuration"
  - "SQLAgentUserRole database role"
  - "proxy accounts [SQL Server Agent]"
ms.assetid: 67897e3e-b7d0-43dd-a2e2-2840ec4dd1ef
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 01/19/2017
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---

# Configure a User to Create and Manage SQL Server Agent Jobs

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to configure a user to create or execute [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.  

- **Before you begin:**  [Security](#Security)  
 
- **To configure a user to create and manage SQL Server Agent jobs, using:**  [SQL Server Management Studio](#SSMS)  

## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
To configure a user to create or execute [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, you must first add an existing SQL Server login or msdb role to one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the msdb database: SQLAgentUserRole, SQLAgentReaderRole, or SQLAgentOperatorRole.  
  
By default, members of these database roles can create their own job steps that run as themselves. If these non-administrative users want to run jobs that execute other job step types (for example, [!INCLUDE[ssIS](../../includes/ssis-md.md)] packages), they will need to have access to a proxy account. All members of the sysadmin fixed server role have permission to create, modify, and delete proxy accounts. For more information about the permissions that are associated with these [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
#### <a name="Permissions"></a>Permissions  
For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
**To add a SQL login or msdb role to a SQL Server Agent fixed database role**  
  
1.  In **Object Explorer**, expand a server.  
  
2.  Expand **Security**, and then expand **Logins**.  
  
3.  Right-click the login you wish to add to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database role, and select **Properties**.  
  
4.  On the **User Mapping** page of the **Login Properties** dialog box, select the row containing **msdb**.  
  
5.  Under **Database role membership for: msdb**, check the appropriate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database role.  
  
**To configure a proxy account to create and manage SQL Server Agent job steps**  
  
1.  In **Object Explorer**, expand a server.  
  
2.  Expand **SQL Server Agent**.  
  
3.  Right-click **Proxies** and select **New Proxy**.  
  
4.  On the **General** page of the **New Proxy Account** dialog, specify the proxy name, credential name, and description for the new proxy. Note that you must create a credential first before creating a SQL Server Agent proxy. For more information about creating a credential, see [How to: Create a Credential](../../relational-databases/security/authentication-access/create-a-credential.md) and [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md).  
  
5.  Check the appropriate subsystems for this proxy.
    1. [Operating system (CmdExec)](create-a-cmdexec-job-step.md)
    1. [SQL Server Analysis Services Query](create-an-analysis-services-job-step.md#to-create-an-analysis-services-query-job-step)
    1. [SQL Server Analysis Services Command](create-an-analysis-services-job-step.md#to-create-an-analysis-services-command-job-step-1)
    1. [SQL Server Integration Services Package](../../integration-services/packages/run-integration-services-ssis-packages.md)
    1. [PowerShell](../../powershell/run-windows-powershell-steps-in-sql-server-agent.md)
  
6.  On the **Principals** page, add or remove logins or roles to grant or remove access to the proxy account.  

## See Also
- [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md)
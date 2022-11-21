---
description: Configure SQL Server Agent
title: Configure SQL Server Agent
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, configuring"
  - "accounts [SQL Server], SQL Server Agent"
  - "SQL Server Agent, permissions"
  - "security [SQL Server], SQL Server Agent"
ms.assetid: 2e361a62-9e92-4fcd-80d7-d6960f127900
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 01/19/2017
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---

# Configure SQL Server Agent

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. Enabling and disabling SQL Server Agent is currently not supported in SQL Managed Instance. SQL Agent is always running. See [SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to specify some configuration options for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent during installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The full set of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent configuration options is only available within SQL Server Management Studio, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO), or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent stored procedures.
  
## <a name="BeforeYouBegin"></a>Before you begin
  
### <a name="Restrictions"></a>Limitations and restrictions
  
-   Select **SQL Server Agent** in Object Explorer of SQL Server Management Studio to administer jobs, operators, alerts, and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service. However, Object Explorer only displays the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node if you have permission to use it.
  
-   Auto-restart should not be enabled for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on failover cluster instances.
  
### <a name="Security"></a>Security
  
#### <a name="Permissions"></a>Permissions
To perform its functions, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be configured to use the credentials of an account that is a member of the **sysadmin** fixed server role in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The account must have the following Windows permissions:  
  
-   Log on as a service (SeServiceLogonRight)
  
-   Replace a process-level token (SeAssignPrimaryTokenPrivilege)
  
-   Bypass traverse checking (SeChangeNotifyPrivilege)
  
-   Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)
  
For more information about the Windows permissions required for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account, see [Select an Account for the SQL Server Agent Service](../../ssms/agent/select-an-account-for-the-sql-server-agent-service.md) and [Setting Up Windows Service Accounts](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).
  
## To configure SQL Server Agent

1. Select the **Start** button, and then, on the **Start**  menu, select **Control Panel**.  
2. In Control Panel, select **System and Security**, select **Administrative Tools**, and then select **Local Security Policy**.
3. In Local Security Policy, select the chevron to expand the **Local Policies** folder, and then Select the **User Rights Assignment** folder.
4. Right-click a permission that you want to configure for use with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and select **Properties**.
5. In the permission's properties dialog box, verify that the account under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs is listed. If not, select **Add User or Group**, enter the account under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs in the **Select Users, Computers, Service Accounts, or Groups** dialog box, and then Select **OK**.
6. Repeat for each permission that you want to add to run with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. When finished, select **OK**.

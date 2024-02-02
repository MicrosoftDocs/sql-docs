---
title: Set the Service Startup Account
description: "Set the Service Startup Account for SQL Server Agent (SQL Server Configuration Manager)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 10/03/2023
ms.service: sql
ms.subservice: ssms
ms.topic: how-to
helpviewer_keywords:
  - "SQL Server Agent, service accounts"
  - "startup accounts [SQL Server]"
  - "service startup accounts [SQL Server Agent]"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2016"
---

# Set the Service Startup Account for SQL Server Agent (SQL Server Configuration Manager)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service startup account defines the Windows account that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs as, as well as its network permissions. This article describes how to set the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio.

## Limitations

By default, the SQL Server Agent service account is mapped to the default SQL Server Agent service SID (`NT SERVICE\SQLSERVERAGENT`), which is a member of the **sysadmin** fixed server role. The account must also be a member of the `msdb` database role **TargetServersRole** on the master server if multiserver job processing is used. The Master Server Wizard automatically adds the service account to this role as part of the enlistment process.

Object Explorer only displays the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent node if you have permission to use it.

## Permissions

To perform its functions, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be configured to use the credentials of an account that is a member of the **sysadmin** fixed server role in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The account must have the following Windows permissions:

- Log on as a service (`SeServiceLogonRight`)
- Replace a process-level token (`SeAssignPrimaryTokenPrivilege`)
- Bypass traverse checking (`SeChangeNotifyPrivilege`)
- Adjust memory quotas for a process (`SeIncreaseQuotaPrivilege`)

For more information about the Windows permissions required for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account, see [Select an Account for the SQL Server Agent Service](../../ssms/agent/select-an-account-for-the-sql-server-agent-service.md) and [Setting Up Windows Service Accounts](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

#### Set the Service Startup Account for SQL Server Agent

1. In **Registered Servers**, select the plus sign to expand **Database Engine**.

1. Select the plus sign to expand the **Local Server Groups** folder.

1. Right-click the server instance where you want to set up the Service Startup Account, and select **SQL Server Configuration Manager...**.

1. In the **User Account Control** dialog box, select **Yes**.

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the console pane, select **SQL Server Services**.

1. In the details pane, right-click **SQL Server Agent**_(server\_name)_, where *server_name* is the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent instance for which you want to change the service startup account, and select **Properties**.

1. In the **SQL Server Agent**_(server\_name)_ **Properties** dialog box, in the **Log On** tab, select one of the following options under **Log on as**:

   - **Built-in account**: select this option if your jobs require resources from the local server only. For information about how to choose a Windows built-in account type, see [Selecting an Account for SQL Server Agent Service.](./select-an-account-for-the-sql-server-agent-service.md)

     > [!IMPORTANT]  
     > The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service does not support the **Local Service** account in SQL Server Management Studio.

   - **This account**: select this option if your jobs require resources across the network, including application resources; if you want to forward events to other Windows application logs; or if you want to notify operators through e-mail or pagers.

     If you select this option:

     1. In the **Account Name** box, enter the account that will be used to run SQL Server Agent. Alternately, select **Browse** to open the **Select User or Group** dialog box and select the account to use.

     1. In the **Password** box, enter the password for the account. Re-enter the password in the **Confirm password** box.

1. Select **OK**.

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, select the **Close** button.

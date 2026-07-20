---
title: "Configure SQL Server Agent Mail to Use Database Mail"
description: "Configure SQL Server Agent mail to use Database Mail"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/16/2025
ms.service: sql
ms.topic: how-to
helpviewer_keywords:
  - "Database Mail [SQL Server], SQL Server Agent Mail"
  - "SQL Server Agent Mail"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Configure SQL Server Agent mail to use Database Mail

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  This article describes how to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to use [Database Mail](database-mail.md) to send notification and alerts in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. 

- For information on how to enable and configure Database Mail, see [Configure database mail](configure-database-mail.md). 
- For an example using [!INCLUDE[tsql](../../includes/tsql-md.md)], see [Create a Database Mail Profile](create-a-database-mail-profile.md).
- To send e-mail using SQL Agent jobs in [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)], SQL Server Agent can use only one Database Mail profile, and it must be called `AzureManagedInstance_dbmail_profile`. For more information and a sample script, see [Azure SQL Managed Instance SQL Agent job notifications](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-notifications).

## Prerequisites

1. [Configure database mail](configure-database-mail.md).  

1. [Create a Database Mail Account](create-a-database-mail-account.md) for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account to use.  

1. [Create a Database Mail Profile](create-a-database-mail-profile.md) for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account to use and add the user to the **DatabaseMailUserRole** database role in the `msdb` database. Verify that [users properly configured to send mail](database-mail-general-troubleshooting.md#are-users-properly-configured-to-send-mail).

1. Set the profile as the default profile for the `msdb` database.  

### Permissions

 The user creating the profiles accounts and executing stored procedures should be a member of the sysadmin fixed server role.  

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio to configure SQL Server Agent to use Database Mail

> [!TIP]
> The following steps aren't necessary in [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)], which is already configured to look for a profile called `AzureManagedInstance_dbmail_profile`. For more information and a sample script, see [Azure SQL Managed Instance SQL Agent job notifications](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-notifications).

The following steps use [SQL Server Management Studio (SSMS)](https://aka.ms/ssms)

1. Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  

1. In **Object Explorer**, expand a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  

1. Right-click **SQL Server Agent**, and then select **Properties**.  

1. Select **Alert System**.  

1. Select **Enable Mail Profile**.  

1. In the **Mail system** list, select **Database Mail**.  

1. In the **Mail profile list**, select a mail profile for Database Mail. 

1. Restart SQL Server Agent.  

<a id="Follow_Up"></a>

## Follow-up tasks

 The following tasks are necessary to complete the configuration of Agent to send alerts and notifications.

-   [Alerts](/ssms/agent/alerts)  

     SQL Agent Alerts can be configured to notify an operator of a particular database event or operating system condition.  

-   [Operators](/ssms/agent/operators)  

     SQL Agent Operators are aliases for people or groups that can receive notifications, for example by email.

## Related content

- [Configure Database Mail](configure-database-mail.md)
- [Configure SQL Server Agent](/ssms/agent/configure-sql-server-agent)
- [General database mail troubleshooting steps](database-mail-general-troubleshooting.md)
- [Automate management tasks using SQL Agent jobs in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/job-automation-managed-instance)

---
description: "Configure SQL Server Agent mail to use Database Mail"
title: "Configure SQL Server Agent mail to use Database Mail"
ms.date: "04/19/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Mail [SQL Server], SQL Server Agent Mail"
  - "SQL Server Agent Mail"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-dt-2019
---
# Configure SQL Server Agent mail to use Database Mail
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This article describes how to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to use [Database Mail](database-mail.md) to send notification and alerts in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. 

- For information on how to enable and configure Database Mail, see [Configure Database Mail](../../relational-databases/database-mail/configure-database-mail.md). 
- For an example using [!INCLUDE[tsql](../../includes/tsql-md.md)], see [Create a Database Mail Profile](../../relational-databases/database-mail/create-a-database-mail-profile.md).
- To send e-mail using SQL Agent jobs in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], SQL Server Agent can use only one Database Mail profile, and it must be called `AzureManagedInstance_dbmail_profile`. For more information and a sample script, see [Azure SQL Managed Instance SQL Agent job notifications](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-notifications).
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   [Enable Database Mail](../../relational-databases/database-mail/configure-database-mail.md).  
  
-   [Create a Database Mail account](../../relational-databases/database-mail/create-a-database-mail-account.md) for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account to use.  
  
-   [Create a Database Mail profile](../../relational-databases/database-mail/create-a-database-mail-profile.md) for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account to use and add the user to the **DatabaseMailUserRole** database role in the `msdb` database.
  
-   Set the profile as the default profile for the `msdb` database.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 The user creating the profiles accounts and executing stored procedures should be a member of the sysadmin fixed server role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  

The following steps are not necessary in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], which is already configured to look for a profile called `AzureManagedInstance_dbmail_profile`. For more information and a sample script, see [Azure SQL Managed Instance SQL Agent job notifications](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-notifications).

 **To configure SQL Server Agent to use Database Mail in SQL Server**  
 
-   In Object Explorer, expand a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
-   Right-click **SQL Server Agent**, and then select **Properties**.  
  
-   Select **Alert System**.  
  
-   Select **Enable Mail Profile**.  
  
-   In the **Mail system** list, select **Database Mail**.  
  
-   In the **Mail profile list**, select a mail profile for Database Mail. 
  
-   Restart SQL Server Agent.  
  
##  <a name="Follow_Up"></a> Follow-up Tasks  
 The following tasks are necessary to complete the configuration of Agent to send alerts and notifications.  
  
-   [Alerts](../../ssms/agent/alerts.md)  
  
     Alerts can be configured to notify an operator of a particular database event or operating system condition.  
  
-   [Operators](../../ssms/agent/operators.md)  
  
     Operators are aliases for people or groups that can receive electronic notification  
  
## Next steps

- [Configure Database Mail](configure-database-mail.md)
- [Automate management tasks using SQL Agent jobs in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/job-automation-managed-instance)
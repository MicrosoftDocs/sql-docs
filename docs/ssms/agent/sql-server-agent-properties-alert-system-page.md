---
title: "SQL Server Agent Properties (Alert System Page) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.agent.alert.f1"
ms.assetid: 3e6d3bfd-20ee-4593-86cc-f65b1c08c69d
author: "markingmyname"
ms.author: "maghan"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# SQL Server Agent Properties (Alert System Page)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and modify the settings for messages sent by [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts.  
  
## Options  
**Mail session**  
The options in this section configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Mail.  
  
**Enable mail profile**  
Enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Mail. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Mail is not enabled.  
  
**Mail System**  
Sets the mail system for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to use. Database Mail  
  
> [!NOTE]  
> After changing the e-mail system, you must restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service for the change to take effect.  
  
**Mail Profile**  
Sets the profile for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to use. You may also select **\<new Database Mail profile...>** to create a new profile.  
  
**Pager e-mails**  
The options in this section allow you to configure e-mail messages sent to pager addresses to work with your paging system.  
  
**Address formatting for pager e-mails**  
This section allows you to specify the format of the addresses and the subject line included in pager e-mails.  
  
**To line**  
Specifies the options for the **To** line of the message  
  
**Prefix**  
Type any fixed text that your system requires at the beginning of the **To** line of messages sent to a pager.  
  
**Pager**  
Includes the e-mail address for the message between the prefix and the suffix.  
  
**Suffix**  
Type any fixed text that your paging system requires at the end of the **To** line of messages sent to a pager.  
  
**Cc line**  
Specifies options for the **Cc** line of the message.  
  
**Prefix**  
Type any fixed text that your system requires at the beginning of the **Cc** line of messages sent to a pager.  
  
**Pager**  
Includes the e-mail address for the message between the prefix and the suffix.  
  
**Suffix**  
Type any fixed text that your paging system requires at the end of the **Cc** line of messages sent to a pager.  
  
**Subject**  
Specifies the options for the subject of the message.  
  
**Prefix**  
Type any fixed text that your paging system requires at the beginning of the **Subject** line of messages sent to a pager.  
  
**Suffix**  
Type any fixed text that your paging system requires at the end of the **Subject** line of messages sent to a pager.  
  
**Include body of e-mail in notification message**  
Includes the body of the e-mail message in the message sent to the pager.  
  
**Fail-safe operator**  
This section allows you to specify the options for the fail-safe operator.  
  
**Enable fail-safe operator**  
Specifies a fail safe operator.  
  
**Operator**  
Sets the name of the operator to receive fail-safe notifications.  
  
**Notify using**  
Sets the method to use for notifying the fail-safe operator.  
  
**Token Replacement**  
This section allows you to enable job step tokens that can be used in jobs run by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts. For more information about job step tokens, see [Use Tokens in Job Steps](../../ssms/agent/use-tokens-in-job-steps.md).  
  
> [!IMPORTANT]  
> Any Windows user with write permissions on the Windows Event Log may be able to access job steps that are activated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts. To avoid this security risk, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent tokens that can be used in jobs activated by alerts are disabled by default. These tokens are: **$(A-DBN)**, **$(A-SVR)**, **$(A-ERR)**, **$(A-SEV)**, and **$(A-MSG)**.  
>   
> If you need to use these tokens, ensure that only members of trusted Windows security groups, such as the Administrators group, have write permissions on the Event Log of the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resides before enabling them.  
  
**Replace tokens for all job responses to alerts**  
Select this check box to enable token replacement for jobs that are activated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] alerts.  
  
## See Also  
[Operators](../../ssms/agent/operators.md)  
[Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md)  
[Database Mail](../../relational-databases/database-mail/database-mail.md)  
  

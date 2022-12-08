---
description: "Assign Alerts to an Operator"
title: "Assign Alerts to an Operator"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, operators"
  - "assigning alerts to operator"
  - "SQL Server Agent, alerts"
  - "alerts [SQL Server], assigning to operator"
  - "jobs [SQL Server Agent], operators"
  - "notifications [SQL Server], job status"
ms.assetid: aa818155-6fa2-4565-a09f-5c7e31c89754
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Assign Alerts to an Operator

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to assign [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts to operators so they can receive notifications about jobs in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   SQL Server Management Studio provides an easy, graphical way to manage the entire alerting system. Using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] is the recommended way to configure your alert infrastructure.  
  
-   To send a notification in response to an alert, you must first configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to send mail. For more information, see [Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md).  
  
-   If a failure occurs when sending an e-mail message or pager notification, the failure is reported in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service error log.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Only members of the **sysadmin** fixed server role can assign alerts to operators.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To assign alerts to an operator  
  
1.  In **Object Explorer**, click the plus sign to expand the server that contains the operator to which you want to assign an alert.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Operators** folder.  
  
4.  Right-click the operator to which you want to assign an alert and select **Properties**, and select the **Notifications** page.  
  
5.  In the _operator\_name_**Properties** dialog box, under **Select a page**, select **Notifications**.  
  
6.  Under **View notifications sent to this user by**, select **Alerts** to view a list of alerts sent to this operator or select **Jobs** to view a list of jobs that send notifications to this operator. Select one or more of the following checkboxes to define the notification method for each notification as necessary: **E-mail**, **Pager**, or **Net send**.  
  
7.  When finished, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To assign alerts to an operator  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- adds an e-mail notification for the specified alert (Test Alert)  
    -- This example assumes that Test Alert already exists
    -- and that François Ajenstat is a valid operator name.  
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_add_notification  
     @alert_name = N'Test Alert',  
     @operator_name = N'François Ajenstat',  
     @notification_method = 1 ;  
    GO  
    ```  
  
For more information, see [sp_add_notification (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-notification-transact-sql.md).  

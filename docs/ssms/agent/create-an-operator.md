---
description: "Create an Operator"
title: "Create an Operator"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, operators"
  - "jobs [SQL Server Agent], notification options"
  - "operators (users) [Database Engine], creating with Management Studio"
  - "SQL Server Agent jobs, notification options"
  - "jobs [SQL Server Agent], operators"
  - "notifications [SQL Server], job status"
ms.assetid: 1359d790-5905-4927-a208-e7155e7768a2
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Create an Operator
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to configure a user to receive notifications about [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   The Pager and **net send** options will be removed from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these features in new development work, and plan to modify applications that currently use these features.  
  
-   Note that SQL Server Agent must be configured to use Database Mail to send e-mail and pager notifications to operators. For more information, see [Assign Alerts to an Operator](assign-alerts-to-an-operator.md).  
  
-   SQL Server Management Studio provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Only members of the **sysadmin** fixed server role can create operators.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To create an operator  
  
1.  In **Object Explorer**, click the plus sign to expand the server where you want to create a SQL Server Agent operator.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Right-click the **Operators** folder and select **New Operator**.  
  
    The following options are available on the **General** page of the **New Operator** dialog box:  
  
    **Name**  
    Change the name of the operator.  
  
    **Enabled**  
    Enable the operator. When not enabled, no notifications are sent to the operator.  
  
    **E-mail name**  
    Specifies the e-mail address for the operator.  
  
    **Net send address**  
    Specify the address to use for **net send**.  
  
    **Pager e-mail name**  
    Specifies the e-mail address to use for the operator's pager.  
  
    **Pager on duty schedule**  
    Sets the times at which the pager is active.  
  
    **Monday - Sunday**  
    Select the days that the pager is active.  
  
    **Workday begin**  
    Select the time of day after which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent sends messages to the pager.  
  
    **Workday end**  
    Select the time of day after which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent no longer sends messages to the pager.  
  
    The following options are available on the **Notifications** page of the **New Operator** dialog box:  
  
    **Alerts**  
    View the alerts in the instance.  
  
    **Jobs**  
    View the jobs in the instance.  
  
    **Alert list**  
    Lists the alerts in the instance.  
  
    **Job list**  
    Lists the jobs in the instance.  
  
    **E-mail**  
    Notify this operator using e-mail.  
  
    **Pager**  
    Notify this operator by sending e-mail to the pager address.  
  
    **Net send**  
    Notify this operator using **net send**.  
  
4.  When finished creating the new operator, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To create an operator  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- sets up the operator information for user 'danwi.'
    -- The operator is enabled.   
    -- SQL Server Agent sends notifications by pager 
    -- from Monday through Friday from 8 A.M. to 5 P.M.  
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_add_operator  
        @name = N'Dan Wilson',  
        @enabled = 1,  
        @email_address = N'danwi',  
        @pager_address = N'5551290AW@pager.Adventure-Works.com',  
        @weekday_pager_start_time = 080000,  
        @weekday_pager_end_time = 170000,  
        @pager_days = 62 ;  
    GO  
    ```  
  
For more information, see [sp_add_operator (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-operator-transact-sql.md).  

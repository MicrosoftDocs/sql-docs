---
title: "Define the Response to an Alert (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, alerts"
  - "alerts [SQL Server], responding to"
  - "responding to alerts"
ms.assetid: c86ca6eb-c59f-46e9-bc32-d474e7c3b170
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Define the Response to an Alert (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to define how [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] responds to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   **To define the response to an alert, using:**  
  
    [SQL Server Management Studio](#SSMSProcedure)  
  
    [Transact-SQL](#TsqlProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   The Pager and **net send** options will be removed from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent in a future version of [!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these features in new development work, and plan to modify applications that currently use these features.  
  
-   Note that SQL Server Agent must be configured to use Database Mail to send e-mail and pager notifications to operators. For more information, see [Assign Alerts to an Operator](assign-alerts-to-an-operator.md).  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Only members of the **sysadmin** fixed server role can define the response to an alert.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To define the response to an alert  
  
1.  In **Object Explorer**, click the plus sign to expand the server that contains the alert on which you want to define a response.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Alerts** folder.  
  
4.  Right-click the alert on which you want to define a response and select **Properties**.  
  
5.  In the _alert\_name_**alert properties** dialog box, under **Select a page**, select **Response**.  
  
6.  Select the **Execute job** check box and, from the list below the **Execute job** check box, select a job to execute when the alert occurs. You can create a new job by clicking **New Job**. You can view more information about the job by clicking **View Job**. For more information about the available options in the **New Job** and **Job Properties**_job\_name_ dialog boxes, see [Create a Job](../../ssms/agent/create-a-job.md) and [View a Job](../../ssms/agent/view-a-job.md).  
  
7.  Select the **Notify Operators** check box if you want to notify operators when the alert is activated. In the **Operator list**, select one or more of the following methods for notifying the operator or operators: **E-mail**, **Pager**, or **Net Send**. You can create a new operator by clicking **New Operator**. You can view more information about an operator by clicking **View Operator**. For more information about the available options in the **New Operator** and **View Operator Properties** dialog boxes, see [Create an Operator](../../ssms/agent/create-an-operator.md) and [View Information About an Operator](../../ssms/agent/view-information-about-an-operator.md).  
  
8.  When finished, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To define the response to an alert  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- adds an e-mail notification for Test Alert.  
    -- assumes that Test Alert already exists and that
    -- François Ajenstat is a valid operator name   
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_add_notification  
     @alert_name = N'Test Alert',  
     @operator_name = N'François Ajenstat',  
     @notification_method = 1 ;  
    GO  
    ```  
  
For more information, see [sp_add_notification (Transact-SQL)](https://msdn.microsoft.com/0525e0a2-ed0b-4e69-8a4c-a9e3e3622fbd).  
  

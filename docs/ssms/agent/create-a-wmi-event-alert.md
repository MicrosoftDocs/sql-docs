---
title: "Create a WMI Event Alert"
description: "Create a WMI Event Alert"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/20/2022
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "WMI event alerts [SQL Server Management Studio]"
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Create a WMI event alert

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This article describes how to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alert that is raised when a specific [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] event occurs that is monitored by the WMI Provider for Server Events in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].

For information about the using the WMI Provider to monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events, see [WMI Provider for Server Events Classes and Properties](../../relational-databases/wmi-provider-server-events/wmi-provider-for-server-events-concepts.md). For information about the permissions necessary to receive WMI event alert notifications, see [Select an Account for the SQL Server Agent Service](../../ssms/agent/select-an-account-for-the-sql-server-agent-service.md). For more information about WQL, see [Using WQL with the WMI Provider for Server Events](../../relational-databases/wmi-provider-server-events/using-wql-with-the-wmi-provider-for-server-events.md).  

## <a id="Restrictions"></a> Limitations and restrictions

- SQL Server Management Studio provides an easy, graphical way to manage the entire alerting system and is the recommended way to configure an alert infrastructure.

- Events generated with `xp_logevent` occur in the `master` database. Therefore, `xp_logevent` does not trigger an alert unless the `@database_name` for the alert is `'master'` or NULL.

- Only WMI namespaces on the computer that runs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent are supported.

## <a id="Permissions"></a> Permissions

By default, only members of the **sysadmin** fixed server role can execute `sp_add_alert`.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In **Object Explorer,** select the plus sign to expand the server where you want to create a WMI event alert.

1. Select the plus sign to expand **SQL Server Agent**.

1. Right-click **Alerts** and select **New Alert**.

1. In the **New Alert** dialog box, in the **Name** box, enter a name for this alert.

1. Check the **Enable** check box to enable the alert to run. By default, **Enable** is checked.

1. In the **Type** list, select **WMI event alert**.

1. Under **WMI event alert definition**, in the **Namespace** box, specify the WMI namespace for the WMI Query Language (WQL) statement that identifies which WMI event will trigger this alert.

1. In the **Query** box, specify the WQL statement that identifies the event that this alert responds to.

1. Select **OK**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   USE msdb;
   GO

   EXEC dbo.sp_add_alert @name = N'Test Alert 2',
       @message_id = 54001,
       @notification_message = N'Error 54001 has occurred on the Sales.SalesOrderDetail table on the AdventureWorks2012 database.',
       @wmi_namespace = '\.\root\Microsoft\SqlServer\ServerEvents',
       @wmi_query = N'SELECT * FROM ALTER_TABLE
   WHERE DatabaseName = ''AdventureWorks2012'' AND SchemaName = ''Sales''
   AND ObjectType=''Table'' AND ObjectName = ''SalesOrderDetail''';
   GO
   ```

## Next steps

- [sp_add_alert (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-alert-transact-sql.md)

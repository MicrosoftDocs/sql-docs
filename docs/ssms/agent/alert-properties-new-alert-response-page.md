---
title: "Alert Properties - New Alert (Response Page)"
description: "Alert Properties - New Alert (Response Page)"
author: markingmyname
ms.author: maghan
ms.date: 12/11/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.ag.alert.response.f1"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---

# Alert Properties - New Alert (Response Page)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to specify a job that you want to run and to obtain a list of operators to be notified in response to a [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent alert.

## Options

**Execute job**  
Enables the **Job list**, **New job** and **View job** options.

**New job**  
Open the **New Job** dialog box. This button isn't available when **Execute job** isn't selected.

**View job**  
View or modify the selected job. This option isn't available when **Execute job** isn't selected.

**Notify operators**  
Enables the controls that allow you to add, remove, or change operators.

**Operator list**  
Lists the operators to notify when an alert occurs. To specify a notification method, select the **E-mail**, **Pager**, or **Net send** check box that is displayed after the operator name.This option not available when **Notify operators** isn't selected.

**E-mail**  
Use e-mail to notify the operator.

**Pager**  
Use the pager e-mail address to notify the operator.

**Net send**  
Use **net send** to notify the operator.

**New operator**  
Displays the **New Operator** dialog box, which you can use to create a new operator.

**View operator**  
Displays the **Properties** dialog box for the currently selected operator. You can view and modified operator properties on the **Operator Properties** dialog box.

## Related content

- [Alerts](../../ssms/agent/alerts.md)
- [Create an Alert Using Severity Level](../../ssms/agent/create-an-alert-using-severity-level.md)
- [Edit an Alert](../../ssms/agent/edit-an-alert.md)
- [Delete an Alert](../../ssms/agent/delete-an-alert.md)

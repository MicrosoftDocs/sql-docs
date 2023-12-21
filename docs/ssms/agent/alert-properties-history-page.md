---
title: "Alert Properties (History Page)"
description: "Alert Properties (History Page)"
author: markingmyname
ms.author: maghan
ms.date: 12/11/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.ag.alert.history.f1"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---

# Alert Properties (History Page)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and modify the history of [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts.

## Options

**Date of last alert**  
Displays the date that the specified event last occurred, or **(Never occurred)** if the event hasn't occurred since the alert was created.

**Date of last response**  
Displays the date that the alert last responded to the event, or **(Never responded)** if the event hasn't occurred since the alert was created.

**Number of occurrences**  
Total number of occurrences of the event since the alert was created, or the last time that the count was reset.

**Reset count**  
Reset the information on this page.

## Related content

- [Alerts](../../ssms/agent/alerts.md)

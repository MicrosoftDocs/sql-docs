---
title: "Target Servers (Target Server Status Tab)"
description: "Target Servers (Target Server Status Tab)"
author: markingmyname
ms.author: maghan
ms.date: 12/05/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.ag.target.status.f1"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---

# Target Servers (Target Server Status Tab)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view the status of the target servers for this master server.

## Options

**Target Server**  
View the name of the target server.

**Local time**  
View the date and time of the target server in the local time zone for that server.

**Last polled**  
View the local date and time that the target server last polled the master.

**Unread instructions**  
View the number of instructions that the target server has not yet received.

**Status**  
View the status of the target server.

**Force Poll**  
Select this button to force the selected target servers to poll the master server.

**Force Defection**  
Select this button to force the selected target servers to defect from the master server.

**Post Instructions**  
Post instructions to the selected target servers.

**Enable Auto Refresh**  
Select this option to automatically refresh the information shown.

**Refresh every**  
Specify how often the information on this page is refreshed.

## Related content

- [Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md)

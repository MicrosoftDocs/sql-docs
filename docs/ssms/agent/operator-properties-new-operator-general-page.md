---
title: New Operator Properties (General Page)
description: "Operator Properties - New Operator (General Page)"
author: markingmyname
ms.author: maghan
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.ag.operator.general.f1"
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---

# Operator Properties - New Operator (General Page)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and modify the general properties of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent operators.  
  
## Options  
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
  
## See Also  
[Operators](../../ssms/agent/operators.md)  

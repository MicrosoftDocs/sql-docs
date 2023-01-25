---
description: "Alert Properties - New Alert (Options Page)"
title: "Alert Properties - New Alert (Options Page)"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.alert.options.f1"
ms.assetid: 6e4f41aa-832d-46ba-b6b5-cf1d3b15d33f
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Alert Properties - New Alert (Options Page)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and modify options for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts.  

## Options  
**E-mail**  
Include error text from the event, if any, in e-mail notifications.  
  
**Pager**  
Include error text from the event, if any, in pager notifications.  
  
**Net send**  
Include error text from the event, if any, in net send notifications.  
  
**Additional notification message to send**  
Type any additional text to include in notification messages.  
  
**Delay between responses**  
Specify a delay for repeated occurrences of the event. Some events may occur frequently during a short period of time. In this case, you may want to know that the event has occurred, but you may not want a response for every event. Use this option to specify a time-out. With a delay, after the alert responds to an event, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent waits for the delay specified before responding again, regardless of whether the event occurs during the delay.  
  
**Minutes**  
Specify a delay in minutes. To respond every time the event occurs, specify 0 minutes and 0 seconds.  
  
**Seconds**  
Specify a delay in seconds. To respond every time the event occurs, specify 0 minutes and 0 seconds.  
  
## See Also  
[Alerts](../../ssms/agent/alerts.md)  

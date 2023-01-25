---
description: "Alert Properties - New Alert (General Page)"
title: "Alert Properties - New Alert (General Page)"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.alert.general.f1"
ms.assetid: f5c11610-62e3-44df-9800-a5dc35be4a09
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Alert Properties - New Alert (General Page)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]


> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and modify the general properties of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts.  

## Options  
**Name**  
Change the name of the alert.  
  
**Enable**  
Enable the alert. When the alert is not enabled, the actions specified in the alert do not occur.  
  
**Type**  
Select the type of alert:  
  
-   **SQL Server event alert** responds to messages in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows event log.  
  
-   **SQL Server performance condition alert** responds to a specific condition in a performance counter.  
  
-   **WMI event alert** responds to a Windows Management Instrumentation (WMI) event.  
  
## SQL Server Event Alert Options  
**Database name**  
Specify a database for the event, or **all databases** to respond to a message regardless of the database where the event occurs.  
  
**Error number**  
Specify that this event responds to an error, and specify the error number.  
  
**Severity**  
Specify that this event responds to any message with a specific severity level, and specify the severity level.  
  
**Raise alert when message contains**  
Filter events by a specific string. When this option is selected, the alert only responds to events that contain a specific string.  
  
**Message text**  
Specify the string to use to filter events.  
  
## SQL Server Performance Condition Alerts  
**Object**  
Specify the performance object to monitor.  
  
**Counter**  
Specify the counter within the performance object to monitor.  
  
**Instance**  
Specify the instance of the counter to monitor.  
  
**Alert if counter**  
Specify the behavior of the counter that the alert responds to. For example, you may want the alert to respond to a condition where the value of the **Free space in tempdb (KB)** counter falls below a certain value, or you may want the alert to respond to a condition where the **SQL Compilations/sec** rises above a certain value.  
  
**Value**  
Specify a value for the counter.  
  
## WMI Event Alert Options  
**Namespace**  
Specify the namespace to use for the WMI Query Language (WQL) statement. Only namespaces on the computer that runs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent are supported.  
  
**Query**  
Specify the WQL statement that identifies the event that the alert responds to.  
  
## See Also  
[Alerts](../../ssms/agent/alerts.md)  
[Using WQL with the WMI Provider for Server Events](../../relational-databases/wmi-provider-server-events/using-wql-with-the-wmi-provider-for-server-events.md)  
[Create an Alert Using an Error Number](../../ssms/agent/create-an-alert-using-an-error-number.md)  
[Create an Alert Using Severity Level](../../ssms/agent/create-an-alert-using-severity-level.md)  

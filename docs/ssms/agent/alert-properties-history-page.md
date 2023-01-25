---
title: "Alert Properties (History Page)"
description: "Alert Properties (History Page)"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.alert.history.f1"
ms.assetid: f5359f5c-93a3-4a4a-8286-e9fe6f0196c7
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 01/19/2017
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---

# Alert Properties (History Page)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]


> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.


Use this page to view and modify the history of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts.  

## Options  
**Date of last alert**  
Displays the date that the specified event last occurred, or **(Never occurred)** if the event has not occurred since the alert was created.  
  
**Date of last response**  
Displays the date that the alert last responded to the event, or **(Never responded)** if the event has not occurred since the alert was created.  
  
**Number of occurrences**  
Total number of occurrences of the event since the alert was created, or the last time that the count was reset.  
  
**Reset count**  
Reset the information on this page.  
  
## See Also  
[Alerts](../../ssms/agent/alerts.md)  

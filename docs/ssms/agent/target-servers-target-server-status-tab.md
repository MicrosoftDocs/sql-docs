---
description: "Target Servers (Target Server Status Tab)"
title: "Target Servers (Target Server Status Tab)"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.target.status.f1"
ms.assetid: 010a4cab-d878-4889-8ac8-7d91db6345d6
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
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
Click this button to force the selected target servers to poll the master server.  
  
**Force Defection**  
Click this button to force the selected target servers to defect from the master server.  
  
**Post Instructions**  
Post instructions to the selected target servers.  
  
**Enable Auto Refresh**  
Select this option to automatically refresh the information shown.  
  
**Refresh every**  
Specify how often the information on this page is refreshed.  
  
## See Also  
[Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md)  

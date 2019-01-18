---
title: "Post Download Instructions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.target.post.f1"
ms.assetid: 11db1efb-8f5b-4284-b17c-04b4bfcef9ed
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Post Download Instructions
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to specify download instructions for a target server.  
  
## Options  
**Instruction type**  
Specify the type of download instruction to post.  
  
**Description**  
View a description of the download instruction.  
  
**Polling interval**  
Set the polling interval for the target server. Applies only to the **Set Polling Interval** instruction.  
  
**All target servers**  
Select this option to post the download instruction to all target servers.  
  
**These target servers**  
Select this option to post the download instruction to the selected target servers.  
  
**Select**  
Specify that the target server should receive the download instruction.  
  
**Target Server**  
View the name of the target server.  
  
**Local time**  
View the date and time of the target server in the local time zone for that server.  
  
**Polling interval**  
View the current polling interval for the target server.  
  
## See Also  
[Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md)  
  

---
title: "New Job Schedule - Job Schedule Properties | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.job.scheduleproperties.f1"
  - "sql13.swb.maint.editrecurringjobsched.f1"
ms.assetid: 5c0b1bc9-dd87-49cc-b0dd-75d0d922b177
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# New Job Schedule - Job Schedule Properties
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and change the properties of the schedule.  
  
## Options  
**Name**  
Type a new name for the schedule.  
  
**Jobs in schedule**  
View the jobs that use the schedule.  
  
**Schedule type**  
Select the type of schedule.  
  
**Enabled**  
Check to enable or disable the schedule.  
  
## Recurring Schedule Types Options  
**Occurs**  
Select the interval at which the schedule recurs.  
  
**Recurs every**  
Select the number of days or weeks between recurrences of the schedule. This option is not available for monthly recurring schedules.  
  
**Monday**  
Set the job to occur on a Monday. Only available for weekly recurring schedules.  
  
**Tuesday**  
Set the job to occur on a Tuesday. Only available for weekly recurring schedules.  
  
**Wednesday**  
Set the job to occur on a Wednesday. Only available for weekly recurring schedules.  
  
**Thursday**  
Set the job to occur on a Thursday. Only available for weekly recurring schedules.  
  
**Friday**  
Set the job to occur on a Friday. Only available for weekly recurring schedules.  
  
**Saturday**  
Set the job to occur on a Saturday. Only available for weekly recurring schedules.  
  
**Sunday**  
Set the job to occur on a Sunday. Only available for weekly recurring schedules.  
  
**Day**  
Select the day of the month the schedule occurs. Only available for monthly recurring schedules.  
  
**of every**  
Select the number of months between occurrences of the schedule. Only available for monthly recurring schedules.  
  
**The**  
Specify a schedule for a specific day of the week on a specific week within the month. Only available for monthly recurring schedules.  
  
**Occurs once at**  
Set the time for a job to occur daily.  
  
**Occurs every**  
Set the number of hours, minutes, or seconds between occurrences.  
  
**Start date**  
Set the date when this schedule will become effective.  
  
**End date**  
Set the date when the schedule will no longer be effective.  
  
**No end date**  
Specify that the schedule will remain effective indefinitely.  
  
## One Time Schedule Types Options  
**Date**  
Select the date for the job to run.  
  
**Time**  
Select the time for the job to run.  
  
## See Also  
[Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md)  
[Schedule a Job](../../ssms/agent/schedule-a-job.md)  
  

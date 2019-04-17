---
title: "dbo.sysschedules (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.sysschedules_TSQL"
  - "sysschedules"
  - "sysschedules_TSQL"
  - "dbo.sysschedules"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysschedules system table"
ms.assetid: 4cac9237-7a69-4035-bb3e-928b76aad698
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.sysschedules (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job schedules. This table is stored in the **msdb** database.  
  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**schedule_id**|**int**|ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job schedule.|  
|**schedule_uid**|**uniqueidentifier**|Unique identifier of the job schedule. This value is used to identify a schedule for distributed jobs.|  
|**originating_server_id**|**int**|ID of the master server from which the job schedule came.|  
|**name**|**sysname (nvarchar(128))**|User-defined name for the job schedule. This name must be unique within a job.|  
|**owner_sid**|**varbinary(85)**|Microsoft Windows *security_identifier* of the user or group that owns the job schedule.|  
|**enabled**|**int**|Status of the job schedule:<br /><br /> **0** = Not enabled.<br /><br /> **1** = Enabled.<br /><br /> If the schedule is not enabled, no jobs will run on the schedule.|  
|**freq_type**|**int**|How frequently a job runs for this schedule.<br /><br /> **1** = One time only<br /><br /> **4** = Daily<br /><br /> **8** = Weekly<br /><br /> **16** = Monthly<br /><br /> **32** = Monthly, relative to **freq_interval**<br /><br /> **64** = Runs when the SQL Server Agent service starts<br /><br /> **128** = Runs when the computer is idle|  
|**freq_interval**|**int**|Days that the job is executed. Depends on the value of **freq_type**. The default value is **0**, which indicates that **freq_interval** is unused. See the table below for the possible values and their effects.|  
|**freq_subday_type**|**int**|Units for the **freq_subday_interval**. The following are the possible values and their descriptions.<br /><br /> <br /><br /> **1** : At the specified time<br /><br /> **2** : Seconds<br /><br /> **4** : Minutes<br /><br /> **8** : Hours|  
|**freq_subday_interval**|**int**|Number of **freq_subday_type** periods to occur between each execution of the job.|  
|**freq_relative_interval**|**int**|When **freq_interval** occurs in each month, if **freq_interval** is **32** (monthly relative). Can be one of the following values:<br /><br /> **0** = **freq_relative_interval** is unused<br /><br /> **1** = First<br /><br /> **2** = Second<br /><br /> **4** = Third<br /><br /> **8** = Fourth<br /><br /> **16** = Last|  
|**freq_recurrence_**<br /><br /> **factor**|**int**|Number of weeks or months between the scheduled execution of a job. **freq_recurrence_factor** is used only if **freq_type** is **8**, **16**, or **32**. If this column contains **0**, **freq_recurrence_factor** is unused.|  
|**active_start_date**|**int**|Date on which execution of a job can begin. The date is formatted as YYYYMMDD. NULL indicates today's date.|  
|**active_end_date**|**int**|Date on which execution of a job can stop. The date is formatted YYYYMMDD.|  
|**active_start_time**|**int**|Time on any day between **active_start_date** and **active_end_date** that job begins executing. Time is formatted HHMMSS, using a 24-hour clock.|  
|**active_end_time**|**int**|Time on any day between **active_start_date** and **active_end_date** that job stops executing. Time is formatted HHMMSS, using a 24-hour clock.|  
|**date_created**|**datetime**|Date and time that the schedule was created.|  
|**date_modified**|**datetime**|Date and time that the schedule was last modified.|  
|**version_number**|**int**|Current version number of the schedule. For example, if a schedule has been modified 10 times, the **version_number** is 10.|  
  
|Value of freq_type|Effect on freq_interval|  
|-------------------------|------------------------------|  
|**1** (once)|**freq_interval** is unused (**0**)|  
|**4** (daily)|Every **freq_interval** days|  
|**8** (weekly)|**freq_interval** is one or more of the following:<br /><br /> **1** = Sunday<br /><br /> **2** = Monday<br /><br /> **4** = Tuesday<br /><br /> **8** = Wednesday<br /><br /> **16** = Thursday<br /><br /> **32** = Friday<br /><br /> **64** = Saturday|  
|**16** (monthly)|On the **freq_interval** day of the month|  
|**32** (monthly, relative)|**freq_interval** is one of the following:<br /><br /> **1** = Sunday<br /><br /> **2** = Monday<br /><br /> **3** = Tuesday<br /><br /> **4** = Wednesday<br /><br /> **5** = Thursday<br /><br /> **6** = Friday<br /><br /> **7** = Saturday<br /><br /> **8** = Day<br /><br /> **9** = Weekday<br /><br /> **10** = Weekend day|  
|**64** (starts when SQL Server Agent service starts)|**freq_interval** is unused (**0**)|  
|**128** (runs when computer is idle)|**freq_interval** is unused (**0**)|  
  
## See also  
 [dbo.sysjobschedules &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysjobschedules-transact-sql.md)  
  
  

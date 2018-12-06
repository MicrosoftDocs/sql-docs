---
title: "Manage Jobs Across an Enterprise | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "multiserver job management [SQL Server]"
  - "jobs [SQL Server Agent], modifying"
  - "modifying jobs"
  - "SQL Server Agent jobs, modifying"
  - "target servers [SQL Server], job changes"
ms.assetid: 4fe7f6c6-f89b-4430-979c-4994a5dcf7a6
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Manage Jobs Across an Enterprise
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

If you make changes to multiserver job definitions outside [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you must post the changes to the download list so that target servers can download the updated job again. To ensure that target servers have current job definitions, post an INSERT instruction after you update the multiserver job, as follows:  
  
```  
EXECUTE sp_post_msx_operation 'INSERT', 'JOB', '<job id>'  
```  
  
To notify target servers that a multiserver job has been modified, you must invoke the preceding command after using any of the following procedures:  
  
-   [sp_add_jobstep (Transact-SQL)](https://msdn.microsoft.com/97900032-523d-49d6-9865-2734fba1c755)  
  
-   [sp_update_jobstep (Transact-SQL)](https://msdn.microsoft.com/e158802c-c347-4a5d-bf75-c03e5ae56e6b)  
  
-   [sp_delete_jobstep (Transact-SQL)](https://msdn.microsoft.com/421ede8e-ad57-474a-9fb9-92f70a3e77e3)  
  
-   [Managing Events](../../relational-databases/system-stored-procedures/sp-attach-schedule-transact-sql.md)  
  
-   [sp_detach_schedule (Transact-SQL)](https://msdn.microsoft.com/9a1fc335-1bef-4638-a33a-771c54a5dd19)  
  
    > [!NOTE]  
    > It is not necessary to call **sp_post_msx_operation** after you call **sp_update_job** or **sp_delete_job**, because these stored procedures post the required changes to the download list automatically.  
  
The following are common tasks for managing jobs across an enterprise:  
  
**To check the status of a target server**  
  
-   [Transact-SQL](https://msdn.microsoft.com/f841d3bd-901a-4980-ad0b-1c6eeba3f717)  
  
-   [SQL Server Management Objects (SMO)](https://msdn.microsoft.com/4cde2b85-2a31-4cac-8d16-7a4196066193)  
  
**To change the target servers for a job**  
  
-   [SQL Server Management Studio](../../ssms/agent/modify-the-target-servers-for-a-job.md)  
  
-   [Transact-SQL](https://msdn.microsoft.com/485252cc-0081-490a-9bd1-cbbd68eea286)  
  
-   [SQL Server Management Objects (SMO)](https://msdn.microsoft.com/4cde2b85-2a31-4cac-8d16-7a4196066193)  
  
**To change the location of a target server**  
  
-   [SQL Server Management Studio](../../ssms/agent/specify-a-target-server-s-location-sql-server-management-studio.md)  
  
-   [Transact-SQL](https://msdn.microsoft.com/ceb3b2bc-0cc4-48d8-9bdc-6a809556e35f)  
  
-   [SQL Server Management Objects (SMO)](https://msdn.microsoft.com/4cde2b85-2a31-4cac-8d16-7a4196066193)  
  
**To synchronize target server clocks**  
  
-   [SQL Server Management Studio](../../ssms/agent/synchronize-target-server-clocks-sql-server-management-studio.md)  
  
-   [Transact-SQL](https://msdn.microsoft.com/40e44df7-d3e3-44ee-b149-08aba629a21f)  
  
**To force a target server to poll the master server**  
  
-   [SQL Server Management Studio](../../ssms/agent/force-a-target-server-to-poll-the-master-server.md)  
  
-   [Transact-SQL](https://msdn.microsoft.com/085deef8-2709-4da9-bb97-9ab32effdacf)  
  
## See Also  
[Manage Events](../../ssms/agent/manage-events.md)  
  

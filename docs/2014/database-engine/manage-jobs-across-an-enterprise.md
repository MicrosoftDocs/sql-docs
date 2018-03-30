---
title: "Manage Jobs Across an Enterprise | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-cross-instance"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "multiserver job management [SQL Server]"
  - "jobs [SQL Server Agent], modifying"
  - "modifying jobs"
  - "SQL Server Agent jobs, modifying"
  - "target servers [SQL Server], job changes"
ms.assetid: 4fe7f6c6-f89b-4430-979c-4994a5dcf7a6
caps.latest.revision: 22
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Manage Jobs Across an Enterprise
  If you make changes to multiserver job definitions outside [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you must post the changes to the download list so that target servers can download the updated job again. To ensure that target servers have current job definitions, post an INSERT instruction after you update the multiserver job, as follows:  
  
```  
EXECUTE sp_post_msx_operation 'INSERT', 'JOB', '<job id>'  
```  
  
 To notify target servers that a multiserver job has been modified, you must invoke the preceding command after using any of the following procedures:  
  
-   [sp_add_jobstep (Transact-SQL)](../Topic/sp_add_jobstep%20\(Transact-SQL\).md)  
  
-   [sp_update_jobstep (Transact-SQL)](../Topic/sp_update_jobstep%20\(Transact-SQL\).md)  
  
-   [sp_delete_jobstep (Transact-SQL)](../Topic/sp_delete_jobstep%20\(Transact-SQL\).md)  
  
-   [sp_attach_schedule &#40;Transact-SQL&#41;](../Topic/sp_attach_schedule%20\(Transact-SQL\).md)  
  
-   [sp_detach_schedule &#40;Transact-SQL&#41;](../Topic/sp_detach_schedule%20\(Transact-SQL\).md)  
  
    > [!NOTE]  
    >  It is not necessary to call **sp_post_msx_operation** after you call **sp_update_job** or **sp_delete_job**, because these stored procedures post the required changes to the download list automatically.  
  
 The following are common tasks for managing jobs across an enterprise:  
  
 **To check the status of a target server**  
  
-   [Transact-SQL](../Topic/sp_help_targetserver%20\(Transact-SQL\).md)  
  
-   [SQL Server Management Objects (SMO)](../../2014/database-engine/dev-guide/sql-server-management-objects-smo-programming-guide.md)  
  
 **To change the target servers for a job**  
  
-   [SQL Server Management Studio](../../2014/database-engine/modify-the-target-servers-for-a-job.md)  
  
-   [Transact-SQL](../Topic/sp_add_jobserver%20\(Transact-SQL\).md)  
  
-   [SQL Server Management Objects (SMO)](../../2014/database-engine/dev-guide/sql-server-management-objects-smo-programming-guide.md)  
  
 **To change the location of a target server**  
  
-   [SQL Server Management Studio](../../2014/database-engine/specify-a-target-server-s-location-sql-server-management-studio.md)  
  
-   [Transact-SQL](../Topic/sp_msx_enlist%20\(Transact-SQL\).md)  
  
-   [SQL Server Management Objects (SMO)](../../2014/database-engine/dev-guide/sql-server-management-objects-smo-programming-guide.md)  
  
 **To synchronize target server clocks**  
  
-   [SQL Server Management Studio](../../2014/database-engine/synchronize-target-server-clocks-sql-server-management-studio.md)  
  
-   [Transact-SQL](../Topic/sp_resync_targetserver%20\(Transact-SQL\).md)  
  
 **To force a target server to poll the master server**  
  
-   [SQL Server Management Studio](../../2014/database-engine/force-a-target-server-to-poll-the-master-server.md)  
  
-   [Transact-SQL](../Topic/sp_post_msx_operation%20\(Transact-SQL\).md)  
  
## See Also  
 [Manage Events](../../2014/database-engine/manage-events.md)  
  
  
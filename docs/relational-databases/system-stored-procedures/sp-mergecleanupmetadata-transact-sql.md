---
description: "sp_mergecleanupmetadata (Transact-SQL)"
title: "sp_mergecleanupmetadata (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_mergecleanupmetadata_TSQL"
  - "sp_mergecleanupmetadata"
helpviewer_keywords: 
  - "sp_mergecleanupmetadata"
ms.assetid: 892f8628-4cbe-4cc3-b959-ed45ffc24064
author: markingmyname
ms.author: maghan
---
# sp_mergecleanupmetadata (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Should be used only in replication topologies that include servers running versions of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prior to [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] Service Pack 1.**sp_mergecleanupmetadata** allows administrators to clean up metadata in the **MSmerge_genhistory**, **MSmerge_contents** and **MSmerge_tombstone** system tables. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_mergecleanupmetadata [ [ @publication = ] 'publication' ]  
    [ , [ @reinitialize_subscriber = ] 'reinitialize_subscriber' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of **%**, which cleans up metadata for all publications. The publication must already exist if explicitly specified.  
  
`[ @reinitialize_subscriber = ] 'subscriber'`
 Specifies whether to reinitialize the Subscriber. *subscriber* is **nvarchar(5)**, can be **TRUE** or **FALSE**, with a default of **TRUE**. If **TRUE**, subscriptions are marked for reinitialization. If **FALSE**, the subscriptions are not marked for reinitialization.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_mergecleanupmetadata** should be used only in replication topologies that include servers running versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prior to [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] Service Pack 1. Topologies that include only [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] Service Pack 1 or later should use automatic retention based metadata cleanup. When running this stored procedure, be aware of the necessary and potentially large growth of the log file on the computer on which the stored procedure is running.  
  
> [!CAUTION]
>  After **sp_mergecleanupmetadata** is executed, by default, all subscriptions at the Subscribers of publications that have metadata stored in **MSmerge_genhistory**, **MSmerge_contents** and **MSmerge_tombstone** are marked for reinitialization, any pending changes at the Subscriber are lost, and the current snapshot is marked obsolete.  
> 
> [!NOTE]
>  If there are multiple publications on a database, and any one of those publications uses an infinite publication retention period (**\@retention**=**0**), running **sp_mergecleanupmetadata** does not clean up the merge replication change tracking metadata for the database. For this reason, use infinite publication retention with caution.  
  
 When executing this stored procedure, you can choose whether to reinitialize Subscribers by setting the **\@reinitialize_subscriber** parameter to **TRUE** (the default) or **FALSE**. If **sp_mergecleanupmetadata** is executed with the **\@reinitialize_subscriber** parameter set to **TRUE**, a snapshot is reapplied at the Subscriber even if the subscription was created without an initial snapshot (for example, if the snapshot data and schema were manually applied or already existed at the Subscriber). Setting the parameter to **FALSE** should be used with caution, because if the publication is not reinitialized, you must ensure that data at the Publisher and Subscriber is synchronized.  
  
 Regardless of the value of **\@reinitialize_subscriber**, **sp_mergecleanupmetadata** fails if there are ongoing merge processes that are attempting to upload changes to a Publisher or a republishing Subscriber at the time the stored procedure is invoked.  
  
 **Executing sp_mergecleanupmetadata with \@reinitialize_subscriber = TRUE:**  
  
1.  It is recommended, but not required, that you stop all updates to the publication and subscription databases. If updates continue, any updates made at a Subscriber since the last merge are lost when the publication is reinitialized, but data convergence is maintained.  
  
2.  Execute a merge by running the Merge Agent. We recommend that you use the **-Validate** agent command line option at each Subscriber when you run the Merge Agent. If you are running continuous mode merges, see *Special Considerations for Continuous Mode Merges* later in this section.  
  
3.  After all merges have completed, execute **sp_mergecleanupmetadata**.  
  
4.  Execute **sp_reinitmergepullsubscription** on all subscribers using named or anonymous pull subscription to ensure data convergence.  
  
5.  If you are running continuous mode merges, see *Special Considerations for Continuous Mode Merges* later in this section.  
  
6.  Regenerate snapshot files for all merge publications involved at all levels. If you try to merge without regenerating the snapshot first, you receive a prompt to regenerate the snapshot.  
  
7.  Back up the publication database. Failure to do so can cause a merge failure after a restore of the publication database.  
  
 **Executing sp_mergecleanupmetadata with \@reinitialize_subscriber = FALSE:**  
  
1.  Stop **all** updates to the publication and subscription databases.  
  
2.  Execute a merge by running the Merge Agent. We recommend that you use the **-Validate** agent command line option at each Subscriber when you run the Merge Agent. If you are running continuous mode merges, see *Special Considerations for Continuous Mode Merges* later in this section.  
  
3.  After all merges have completed, execute **sp_mergecleanupmetadata**.  
  
4.  If you are running continuous mode merges, see *Special Considerations for Continuous Mode Merges* later in this section.  
  
5.  Regenerate snapshot files for all merge publications involved at all levels. If you try to merge without regenerating the snapshot first, you receive a prompt to regenerate the snapshot.  
  
6.  Back up the publication database. Failure to do so can cause a merge failure after a restore of the publication database.  

 **Special Considerations for Continuous Mode Merges**  
  
 If you are running continuous-mode merges, you must either:  
  
-   Stop the Merge Agent, and then perform another merge without the **-Continuous** parameter specified.  
  
-   Deactivate the publication with **sp_changemergepublication** to ensure that any continuous-mode merges that are polling for the publication status fail.  
  
    ```  
    EXEC central..sp_changemergepublication @publication = 'dynpart_pubn', @property = 'status', @value = 'inactive'  
    ```  
  
 When you have completed step 3 of running **sp_mergecleanupmetadata**, resume continuous mode merges based on how you stopped them. Either:  
  
-   Add the **-Continuous** parameter back for the Merge Agent.  
  
-   Reactivate the publication with **sp_changemergepublication.**  
  
    ```  
    EXEC central..sp_changemergepublication @publication = 'dynpart_pubn', @property = 'status', @value = 'active'  
    ```  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_mergecleanupmetadata**.  
  
 To use this stored procedure, the Publisher must be running [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]. The Subscribers must be running either [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] or [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0, Service Pack 2.  
  
## See Also  
 [MSmerge_genhistory &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msmerge-genhistory-transact-sql.md)   
 [MSmerge_contents &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msmerge-contents-transact-sql.md)   
 [MSmerge_tombstone &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msmerge-tombstone-transact-sql.md)  
  
  

---
title: "Configure Snapshot Properties (Replication Transact-SQL Programming) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "snapshots [SQL Server replication], properties"
ms.assetid: 978d150f-8971-458a-ab2b-3beba5937b46
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Configure Snapshot Properties (Replication Transact-SQL Programming)
  Snapshot properties can be defined and modified programmatically using replication stored procedures, where the stored procedures used depend on the type of publication.  
  
### To configure snapshot properties when creating a snapshot or transactional publication  
  
1.  At the Publisher, execute [sp_addpublication](/sql/relational-databases/system-stored-procedures/sp-addpublication-transact-sql). Specify a publication name for **@publication**, a value of either **snapshot** or **continuous** for **@repl_freq**, and one or more of the following snapshot-related parameters:  
  
    -   **@alt_snapshot_folder** - specify a path if the snapshot for this publication is accessed from that location instead of or in addition to the snapshot default folder.  
  
    -   **@compress_snapshot** - specify a value of **true** if the snapshot files in the alternate snapshot folder are compressed in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] CAB file format.  
  
    -   **@pre_snapshot_script** - specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization before the initial snapshot is applied.  
  
    -   **@post_snapshot_script** - specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization after the initial snapshot is applied.  
  
    -   **@snapshot_in_defaultfolder** - specify a value of **false** if the snapshot is available only in a non-default location.  
  
     For more information about creating publications, see [Create a Publication](create-a-publication.md).  
  
### To configure snapshot properties when creating a merge publication  
  
1.  At the Publisher, execute [sp_addmergepublication](/sql/relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql). Specify a publication name for **@publication**, a value of either **snapshot** or **continuous** for **@repl_freq**, and one or more of the following snapshot-related parameters:  
  
    -   **@alt_snapshot_folder** - specify a path if the snapshot for this publication is accessed from that location instead of or in addition to the snapshot default folder.  
  
    -   **@compress_snapshot** - specify a value of **true** if the snapshot files in the alternate snapshot folder are compressed in the CAB file format.  
  
    -   **@pre_snapshot_script** - specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization before the initial snapshot is applied.  
  
    -   **@post_snapshot_script** - specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization after the initial snapshot is applied.  
  
    -   **@snapshot_in_defaultfolder** - specify a value of **false** if the snapshot is available only in a non-default location.  
  
2.  For more information about creating publications, see [Create a Publication](create-a-publication.md).  
  
### To modify snapshot properties of an existing snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_changepublication](/sql/relational-databases/system-stored-procedures/sp-changepublication-transact-sql). Specify a value of **1** for **@force_invalidate_snapshot** and one of the following values for **@property**:  
  
    -   **alt_snapshot_folder** -also specify a new path to the alternate snapshot folder for **@value**.  
  
    -   **compress_snapshot** - also specify a value of either **true** or **false** for **@value** to indicate whether the snapshot files in the alternate snapshot folder are compressed in the CAB file format.  
  
    -   **pre_snapshot_script** - also for **@value** specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization before the initial snapshot is applied.  
  
    -   **post_snapshot_script** - also for **@value** specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization after the initial snapshot is applied.  
  
    -   **snapshot_in_defaultfolder** - also specify a value of either **true** or **false** to indicate whether the snapshot is available only in a non-default location.  
  
2.  (Optional) At the Publisher on the publication database, execute [sp_changepublication_snapshot](/sql/relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql). Specify **@publication** and one or more of the scheduling or security credential parameters being changed.  
  
    > [!IMPORTANT]  
    >  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
3.  Run the [Replication Snapshot Agent](../agents/replication-snapshot-agent.md) from the command prompt or start the Snapshot Agent job to generate a new snapshot. For more information, see [Create and Apply the Initial Snapshot](../create-and-apply-the-initial-snapshot.md).  
  
### To modify snapshot properties of an existing merge publication  
  
1.  At the Publisher on the publication database, execute [sp_changemergepublication](/sql/relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql). Specify a value of **1** for **@force_invalidate_snapshot** and one of the following values for **@property**:  
  
    -   **alt_snapshot_folder** -also specify a new path to the alternate snapshot folder for **@value**.  
  
    -   **compress_snapshot** - also specify a value of either **true** or **false** for **@value** to indicate whether the snapshot files in the alternate snapshot folder are compressed in the CAB file format.  
  
    -   **pre_snapshot_script** - also for **@value** specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization before the initial snapshot is applied.  
  
    -   **post_snapshot_script** - also for **@value** specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization after the initial snapshot is applied.  
  
    -   **snapshot_in_defaultfolder** - also specify a value of either **true** or **false** to indicate whether the snapshot is available only in a non-default location.  
  
2.  Run the [Replication Snapshot Agent](../agents/replication-snapshot-agent.md) from the command prompt or start the Snapshot Agent job to generate a new snapshot. For more information, see [Create and Apply the Initial Snapshot](../create-and-apply-the-initial-snapshot.md).  
  
## Example  
 This example creates a publication that uses an alternate snapshot folder and a compressed snapshot.  
  
 [!code-sql[HowTo#sp_mergealtsnapshot](../../../snippets/tsql/SQL15/replication/howto/tsql/createmergepubaltsnapshot.sql#sp_mergealtsnapshot)]  
  
## See Also  
 [Alternate Snapshot Folder Locations](../alternate-snapshot-folder-locations.md)   
 [Compressed Snapshots](../compressed-snapshots.md)   
 [Execute Scripts Before and After the Snapshot Is Applied](../snapshot-options.md#execute-scripts-before-and-after-snapshot-is-applied)   
 [Replication System Stored Procedures Concepts](../concepts/replication-system-stored-procedures-concepts.md)   
 [Transfer Snapshots Through FTP](../transfer-snapshots-through-ftp.md)   
 [Change Publication and Article Properties](change-publication-and-article-properties.md)  
  
  

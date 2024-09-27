---
title: "Configure snapshot properties (Replication SP)"
description: Use replication stored procedures to configure snapshot properties for Snapshot or Transactional publications.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "snapshots [SQL Server replication], properties"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Configure Snapshot Properties (Replication Transact-SQL Programming)
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Snapshot properties can be defined and modified programmatically using replication stored procedures, where the stored procedures used depend on the type of publication.  
  
### To configure snapshot properties when creating a snapshot or transactional publication  
  
1.  At the Publisher, execute [sp_addpublication](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md). Specify a publication name for `@publication`, a value of either **snapshot** or **continuous** for `@repl_freq`, and one or more of the following snapshot-related parameters:  
  
    -   `@alt_snapshot_folder` - specify a path if the snapshot for this publication is accessed from that location instead of or in addition to the snapshot default folder.    
    -   `@compress_snapshot` - specify a value of **true** if the snapshot files in the alternate snapshot folder are compressed in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] CAB file format.    
    -   `@pre_snapshot_script` - specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization before the initial snapshot is applied.    
    -   `@post_snapshot_script` - specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization after the initial snapshot is applied.    
    -   `@snapshot_in_defaultfolder` - specify a value of **false** if the snapshot is available only in a non-default location.  
  
     For more information about creating publications, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
### To configure snapshot properties when creating a merge publication  
  
1.  At the Publisher, execute [sp_addmergepublication](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md). Specify a publication name for `@publication`, a value of either **snapshot** or **continuous** for `@repl_freq`, and one or more of the following snapshot-related parameters:  
  
    -   **alt_snapshot_folder** - specify a path if the snapshot for this publication is accessed from that location instead of or in addition to the snapshot default folder.    
    -   `@compress_snapshot` - specify a value of **true** if the snapshot files in the alternate snapshot folder are compressed in the CAB file format.   
    -   `@pre_snapshot_script` - specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization before the initial snapshot is applied.    
    -   `@post_snapshot_script` - specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization after the initial snapshot is applied.    
    -   `@snapshot_in_defaultfolder` - specify a value of **false** if the snapshot is available only in a non-default location.  
  
2.  For more information about creating publications, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
### To modify snapshot properties of an existing snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md). Specify a value of **1** for `@force_invalidate_snapshot` and one of the following values for `@property`:  
  
    -   **alt_snapshot_folder** -also specify a new path to the alternate snapshot folder for `@value`.    
    -   **compress_snapshot** - also specify a value of either **true** or **false** for `@value` to indicate whether the snapshot files in the alternate snapshot folder are compressed in the CAB file format.    
    -   **pre_snapshot_script** - also for `@value` specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization before the initial snapshot is applied.    
    -   **post_snapshot_script** - also for `@value` specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization after the initial snapshot is applied.    
    -   **snapshot_in_defaultfolder** - also specify a value of either **true** or **false** to indicate whether the snapshot is available only in a non-default location.  
  
2.  (Optional) At the Publisher on the publication database, execute [sp_changepublication_snapshot](../../../relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql.md). Specify `@publication` and one or more of the scheduling or security credential parameters being changed.  
  
    > [!IMPORTANT]  
    >  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
3.  Run the [Replication Snapshot Agent](../../../relational-databases/replication/agents/replication-snapshot-agent.md) from the command prompt or start the Snapshot Agent job to generate a new snapshot. For more information, see [Create and Apply the Initial Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
### To modify snapshot properties of an existing merge publication  
  
1.  At the Publisher on the publication database, execute [sp_changemergepublication](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md). Specify a value of **1** for `@force_invalidate_snapshot` and one of the following values for `@property**`:  
  
    -   **alt_snapshot_folder** -also specify a new path to the alternate snapshot folder for `@value`.    
    -   **compress_snapshot** - also specify a value of either **true** or **false** for `@value` to indicate whether the snapshot files in the alternate snapshot folder are compressed in the CAB file format.    
    -   **pre_snapshot_script** - also for `@value` specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization before the initial snapshot is applied.    
    -   **post_snapshot_script** - also for `@value` specify the file name and full path of a **.sql** file that will be executed at the Subscriber during initialization after the initial snapshot is applied.    
    -   **snapshot_in_defaultfolder** - also specify a value of either **true** or **false** to indicate whether the snapshot is available only in a non-default location.  
  
2.  Run the [Replication Snapshot Agent](../../../relational-databases/replication/agents/replication-snapshot-agent.md) from the command prompt or start the Snapshot Agent job to generate a new snapshot. For more information, see [Create and Apply the Initial Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
## Example  
 This example creates a publication that uses an alternate snapshot folder and a compressed snapshot.  
  
 :::code language="sql" source="../codesnippet/tsql/configure-snapshot-prope_1.sql":::
  
## Related content

- [Modify snapshot options](../../../relational-databases/replication/snapshot-options.md)
- [Execute Scripts Before and After the Snapshot Is Applied](../../../relational-databases/replication/snapshot-options.md#execute-scripts-before-and-after-snapshot-is-applied)
- [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)
- [Transfer Snapshots Through FTP](../../../relational-databases/replication//publish/deliver-a-snapshot-through-ftp.md)
- [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md)

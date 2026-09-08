---
title: New Peer Initialization (Peer-to-Peer)
description: Use the New Peer Initialization page in SSMS to specify whether peers were initialized manually or from a backup. Explore both options and their sync_type values.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.p2pwizard.init.f1"
---
# New peer initialization (Peer-to-Peer Replication)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **New Peer Initialization** page to specify how peer databases were initialized. (You must initialize peers before you complete this wizard.) Initialize peers manually or by using the **initialize with backup** functionality that transactional replication provides. (Peer-to-peer transactional replication doesn't support initializing peers by using a snapshot.) If you need to initialize different peers by using different methods, add the peers separately by running the wizard multiple times.  
  
## Options  
 **Specify how the new peer databases were initialized**  
 Each peer must have the schema and data for all published objects. Select one of the following options:  
  
-   Select the first option if you manually created the schema for published objects or restored a backup, and the first publication database has no data changes since the backup was taken. If you created the schema manually, ensure that each peer has all required data. This option corresponds to a value of **replication support only** for the subscription property **sync_type**.  
  
-   Select the second option if you restored a backup, and the first publication database has data changes since the backup was taken. Replication must now deliver changes from the first publication database that weren't included in the backup. This option corresponds to a value of **initialize with backup** for the subscription property **sync_type**.  
  
     When you enable a publication for Peer-to-Peer Replication, set the **allow_initialize_from_backup** publication property. Replication immediately starts to track changes in the first publication database. If you select the **initialize with backup** option, replication can deliver these changes to a restored database at one or more peers. Select the **Browse** button to locate the backup used, and replication reads the log sequence number (LSN) from the backup. Each peer receives all changes in the first publication database that have a higher LSN.  
  
     This option might not be available if you're creating or adding to a topology that includes [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. The following table shows whether the option is available when you're adding a node to an existing topology.  
  
    |New node|First node|Additional nodes|Option|  
    |--------------|----------------|----------------------|------------|  
    |[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|Disabled|  
    |[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|[!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]|None|Disabled|  
    |[!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]|[!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|Disabled|  
    |[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|None|Enabled|  
    |[!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]|[!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]|None|Enabled|  
    |[!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]|[!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]|[!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]|Enabled|  
    |[!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|None|Enabled|  
  
## Related content

- [Administer a Peer-to-Peer Topology (Replication Transact-SQL Programming)](../../relational-databases/replication/administration/administer-a-peer-to-peer-topology-replication-transact-sql-programming.md)
- [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)

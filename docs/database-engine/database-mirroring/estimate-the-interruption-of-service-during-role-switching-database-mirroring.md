---
title: "Estimate service interruption during mirror failover"
description: Estimate the interruption of service when failover a database mirror from the primary to the secondary role.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "parallel redo [SQL Server]"
  - "role switching [SQL Server]"
  - "database mirroring [SQL Server], queues"
  - "failover [SQL Server], database mirroring"
  - "redo [database mirroring]"
  - "database mirroring [SQL Server], failover"
---
# Estimate the Interruption of Service During Role Switching (Database Mirroring)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  During a role switch, the amount of time that database mirroring will be out of service depends on the type of role switching and the cause of the role switch.  
  
-   For automatic failover, two factors contribute to the time service is interrupted: the time required for the mirror server to recognize that the principal server instance has failed, that is error detection, plus the time required to fail over the database, that is failover time.  
  
-   For a forced-service operation, though a failure has occurred, detecting and responding to the failure depends on human responsiveness. However, estimating the potential interruption of service is limited to estimating the time for the mirror server to switch roles after the forced service command is issued.  
  
    > [!NOTE]  
    >  To reduce the time required to detect specific conditions such as some types of errors, you can define alerts for those conditions.  
  
-   For a manual failover, only the time required to fail over the database after the failover command is issued.  
  
## Error detection  
 The time for the system to notice an error depends on the type of error; for example, a network error is noticed almost instantly, while noticing a server that is not responding takes 10 seconds (with the default timeout).  
  
 For information on errors that can cause a failure during a database mirroring session and timeout detection in high-safety mode with automatic failover, see [Possible Failures During Database Mirroring](../../database-engine/database-mirroring/possible-failures-during-database-mirroring.md)).  
  
## Failover time  
 Failover time consists mainly of the time that the former mirror server requires to roll forward any log remaining in its redo queue, plus a short additional time (for more information about how the mirror server processes log records, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)). For information on estimating failover time, see Estimating Your Failover Redo Rate, later in this topic.  
  
> [!IMPORTANT]  
>  If failover occurs during a transaction in which an index or table is created and then changed, failover might take longer than usual.  For example, failing over during the following series of operations might increase failover time:  BEGIN TRANSACTION, CREATE INDEX on a table, and SELECT INTO the table. The possibility of increased failover time during such a transaction remains until it is completed with either a COMMIT TRANSACTION or ROLLBACK TRANSACTION statement.  
  
### The Redo Queue  
 Rolling forward the database involves applying whatever log records are currently in the redo queue on the mirror server. The *redo queue* consists of the log records that have been written to disk on the mirror server but not yet rolled forward on the mirror database.  
  
 Failover time for the database depends on how fast the mirror server can roll forward the log in the redo queue, which, in turn, is determined primarily by the system hardware and the current work load. Potentially, a principal database can become so busy that the principal server ships log to the mirror server much faster than it can roll the log forward. In this situation, failover might take significant time while the mirror server rolls forward the log in the redo queue. To learn the current size of the redo queue, use the **Redo Queue** counter in the database mirroring performance object. For more information, see [SQL Server, Database Mirroring Object](../../relational-databases/performance-monitor/sql-server-database-mirroring-object.md).  
  
### Estimating the Failover Redo Rate  
 You can measure the amount of time required to roll forward log records-the *redo rate*-by using a test copy of the production database.  
  
 The method for estimating roll forward time during failover depends on the number of threads the mirror server uses during the redo phase. The number of threads depends on the following:  
  
-   In [!INCLUDE[ssStandard](../../includes/ssstandard-md.md)], the mirror server always uses a single thread to roll forward the database.  
  
-   In [!INCLUDE[ssEnterprise](../../includes/ssenterprise-md.md)], mirror servers on computers with fewer than five CPUs also use only a single thread. With five or more CPUs, a mirror server distributes its roll forward operations among multiple threads during a failover (this is known as *parallel redo*). Parallel redo is optimized to use one thread for every four CPUs.  
  
#### Estimating the Single-Threaded Redo Rate  
 For single-threaded redo, roll forward of the mirror database during failover takes approximately the same amount of time as a restore of a log backup takes to roll forward the same amount of log. To estimate failover time, create a test database in the environment under which you intend to run mirroring. Then take a log backup from the production database. To measure the redo rate for that log backup, time how long it takes you to restore the log backup WITH NORECOVERY onto the test database.  
  
 Once you know the redo rate of your mirror server, you can estimate the time to fail over the database at a given point in time by dividing the amount of current log to be redone on the mirror (as measured by the **Redo Queue** performance counter) by the redo rate. Under normal conditions, if the mirror server can keep up with the load from the principal, the **Redo Queue** is small or close to zero, and a failover only takes a few seconds.  
  
#### Estimating the Parallel Redo Rate  
 In [!INCLUDE[ssEnterprise](../../includes/ssenterprise-md.md)], parallel redo is optimized to use one thread for every four CPUs. To estimate roll forward time for parallel redo, it is more accurate to access a running test system than a test database. While monitoring the redo queue on the mirror server, increase the load on the principal server. In normal operation, the redo queue is close to zero. Increase the load on the principal server until the Redo Queue starts to grow continuously; the system is then at its maximum redo rate, and the **Redo Bytes/sec** performance counter at this point represents the maximum redo rate. For more information, see [SQL Server, Database Mirroring Object](../../relational-databases/performance-monitor/sql-server-database-mirroring-object.md).  
  
## Estimating Interruption of Service During Automatic Failover  
 The following figure illustrates how error detection and failover time contribute to the overall time required for an automatic failover to complete on **Partner_B**. Failover requires time to roll forward the database (the redo phase) plus a small amount of time to bring the database online. The undo phase, which involves rolling back any uncommitted transactions, occurs after the new principal database goes online and continues after fail over. The database is available during the undo phase.  
  
 ![Error detection and failover time](../../database-engine/database-mirroring/media/dbm-failovauto-time.gif "Error detection and failover time")  
  
## See Also  
 [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md)   
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)   
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)  
  
  

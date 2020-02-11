---
title: "Change the Target Recovery Time of a Database (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "10/13/2015"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
ms.assetid: e466419a-d8a4-48f7-8d97-13a903ad6b15
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Change the Target Recovery Time of a Database (SQL Server)
  This topic describes how to set the change the target recovery time of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. By default, the target recovery time is 0, and the database uses *automatic checkpoints* (which are controlled by the **recovery interval** server option). Setting the target recovery time to greater than 0 causes the database to use the *indirect-checkpoints* and establishes an upper-bound on recovery time for this database.  
  
> [!NOTE]  
>  The upper-bound that is specified for a given database by its target recovery time setting could be exceeded if a long-running transaction causes excessive UNDO times.  
  
-   **Before you begin:**  [Limitations and Restrictions](#Restrictions), [Security](#Security)  
  
-   **To change the target recovery time, using:**  [SQL Server Management Studio](#SSMSProcedure) or [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a>  
  
> [!CAUTION]  
>  An online transactional workload on a database that is configured for indirect checkpoints could experience performance degradation. Indirect checkpoints make sure that the number of dirty pages are below a certain threshold so that the database recovery completes within the target recovery time. The recovery interval configuration option uses the number of transactions to determine the recovery time as opposed to indirect checkpoints which makes use of number of dirty pages. When indirect checkpoints are enabled on a database receiving a large number of DML operations, the background writer can start aggressively flushing dirty buffers to disk to ensure that the time required to perform recovery is within the target recovery time set of the database. This can cause additional I/O activity on certain systems which can contribute to a performance bottleneck if the disk subsystem is operating above or nearing the I/O threshold.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To change the target recovery time**  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and expand that instance.  
  
2.  Right-click the database you want to change, and click the **Properties** command.  
  
3.  In the **Database Properties** dialog box, click the **Options** page.  
  
4.  In the **Recovery** panel, in the **Target Recovery Time (Seconds)** field, specify the number of seconds that you want as the upper-bound on the recovery time for this database.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To change the target recovery time**  
  
1.  Connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where the database resides.  
  
2.  Use the following [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql-set-options)statement, as follows:  
  
     TARGET_RECOVERY_TIME **=**_target_recovery_time_ { SECONDS | MINUTES }  
  
     *target_recovery_time*  
     When greater than 0 (the default), specifies the upper-bound on the recovery time for the specified database in the event of a crash.  
  
     SECONDS  
     Indicates that *target_recovery_time* is expressed as the number of seconds.  
  
     MINUTES  
     Indicates that *target_recovery_time* is expressed as the number of minutes.  
  
     The following example sets the target recovery time of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `60` seconds.  
  
    ```  
    ALTER DATABASE AdventureWorks2012 SET TARGET_RECOVERY_TIME = 60 SECONDS;  
    ```  
  
## See Also  
 [Database Checkpoints &#40;SQL Server&#41;](database-checkpoints-sql-server.md)   
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options)  
  
  

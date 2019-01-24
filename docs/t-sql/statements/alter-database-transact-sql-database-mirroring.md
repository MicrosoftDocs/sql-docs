---
title: "ALTER DATABASE Database Mirroring (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/17/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "witness [SQL Server], establishing"
  - "manual failover [SQL Server]"
  - "ALTER DATABASE statement, database mirroring"
  - "database mirroring [SQL Server], Transact-SQL"
ms.assetid: 27a032ef-1cf6-4959-8e67-03d28c4b3465
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER DATABASE (Transact-SQL) Database Mirroring 
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

    
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] instead.  
  
 Controls database mirroring for a database. Values specified with the database mirroring options apply to both copies of the database and to the database mirroring session as a whole. Only one \<database_mirroring_option> is permitted per ALTER DATABASE statement.  
  
> [!NOTE]  
>  We recommend that you configure database mirroring during off-peak hours because configuration can affect performance.  
  
 For ALTER DATABASE options, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md). For ALTER DATABASE SET options, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER DATABASE database_name   
SET { <partner_option> | <witness_option> }  
  <partner_option> ::=  
    PARTNER { = 'partner_server'   
            | FAILOVER   
            | FORCE_SERVICE_ALLOW_DATA_LOSS  
            | OFF  
            | RESUME   
            | SAFETY { FULL | OFF }  
            | SUSPEND   
            | TIMEOUT integer  
            }  
  <witness_option> ::=  
    WITNESS { = 'witness_server'   
            | OFF   
            }  
  
```  
  
## Arguments  
  
> [!IMPORTANT]  
>  A SET PARTNER or SET WITNESS command can complete successfully when entered, but fail later.  
  
> [!NOTE]  
>  ALTER DATABASE database mirroring options are not available for a contained database.  
  
 *database_name*  
 Is the name of the database to be modified.  
  
 PARTNER \<partner_option>  
 Controls the database properties that define the failover partners of a database mirroring session and their behavior. Some SET PARTNER options can be set on either partner; others are restricted to the principal server or to the mirror server. For more information, see the individual PARTNER options that follow. A SET PARTNER clause affects both copies of the database, regardless of the partner on which it is specified.  
  
 To execute a SET PARTNER statement, the STATE of the endpoints of both partners must be set to STARTED. Note, also, that the ROLE of the database mirroring endpoint of each partner server instance must be set to either PARTNER or ALL. For information about how to specify an endpoint, see [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md). To learn the role and state of the database mirroring endpoint of a server instance, on that instance, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
```  
SELECT role_desc, state_desc FROM sys.database_mirroring_endpoints  
```  
  
 **\<partner_option> ::=**  
  
> [!NOTE]  
>  Only one \<partner_option> is permitted per SET PARTNER clause.  
  
 **'** _partner_server_ **'**  
 Specifies the server network address of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to act as a failover partner in a new database mirroring session. Each session requires two partners: one starts as the principal server, and the other starts as the mirror server. We recommend that these partners reside on different computers.  
  
 This option is specified one time per session on each partner. Initiating a database mirroring session requires two ALTER DATABASE *database* SET PARTNER **='**_partner_server_**'** statements. Their order is significant. First, connect to the mirror server, and specify the principal server instance as *partner_server* (SET PARTNER **='**_principal_server_**'**). Second, connect to the principal server, and specify the mirror server instance as *partner_server* (SET PARTNER **='**_mirror_server_**'**); this starts a database mirroring session between these two partners. For more information, see [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md).  
  
 The value of *partner_server* is a server network address. This has the following syntax:  
  
 TCP**://**_\<system-address>_**:**_\<port>_  
  
 where  
  
-   *\<system-address>* is a string, such as a system name, a fully qualified domain name, or an IP address, that unambiguously identifies the destination computer system.  
  
-   *\<port>* is a port number that is associated with the mirroring endpoint of the partner server instance.  
  
 For more information, see [Specify a Server Network Address &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md).  
  
 The following example illustrates the SET PARTNER **='**_partner_server_**'** clause:  
  
```  
'TCP://MYSERVER.mydomain.Adventure-Works.com:7777'  
```  
  
> [!IMPORTANT]  
>  If a session is set up by using the ALTER DATABASE statement instead of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the session is set to full transaction safety by default (SAFETY is set to FULL) and runs in high-safety mode without automatic failover. To allow automatic failover, configure a witness; to run in high-performance mode, turn off transaction safety (SAFETY OFF).  
  
 FAILOVER  
 Manually fails over the principal server to the mirror server. You can specify FAILOVER only on the principal server. This option is valid only when the SAFETY setting is FULL (the default).  
  
 The FAILOVER option requires **master** as the database context.  
  
 FORCE_SERVICE_ALLOW_DATA_LOSS  
 Forces database service to the mirror database after the principal server fails with the database in an unsynchronized state or in a synchronized state when automatic failover does not occur.  
  
 We strongly recommend that you force service only if the principal server is no longer running. Otherwise, some clients might continue to access the original principal database instead of the new principal database.  
  
 FORCE_SERVICE_ALLOW_DATA_LOSS is available only on the mirror server and only under all the following conditions:  
  
-   The principal server is down.  
  
-   WITNESS is set to OFF or the witness is connected to the mirror server.  
  
 Force service only if you are willing to risk losing some data in order to restore service to the database immediately.  
  
 Forcing service suspends the session, temporarily preserving all the data in the original principal database. Once the original principal is in service and able to communicate with the new principal server, the database administrator can resume service. When the session resumes, any unsent log records and the corresponding updates are lost.  
  
 OFF  
 Removes a database mirroring session and removes mirroring from the database. You can specify OFF on either partner. For information, see about the impact of removing mirroring, see [Removing Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/removing-database-mirroring-sql-server.md).  
  
 RESUME  
 Resumes a suspended database mirroring session. You can specify RESUME only on the principal server.  
  
 SAFETY { FULL | OFF }  
 Sets the level of transaction safety. You can specify SAFETY only on the principal server.  
  
 The default is FULL. With full safety, the database mirroring session runs synchronously (in *high-safety mode*). If SAFETY is set to OFF, the database mirroring session runs asynchronously (in *high-performance mode*).  
  
 The behavior of high-safety mode depends partly on the witness, as follows:  
  
-   When safety is set to FULL and a witness is set for the session, the session runs in high-safety mode with automatic failover. When the principal server is lost, the session automatically fails over if the database is synchronized and the mirror server instance and witness are still connected to each other (that is, they have quorum). For more information, see [Quorum: How a Witness Affects Database Availability &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/quorum-how-a-witness-affects-database-availability-database-mirroring.md).  
  
     If a witness is set for the session but is currently disconnected, the loss of the mirror server causes the principal server to go down.  
  
-   When safety is set to FULL and the witness is set to OFF, the session runs in high-safety mode without automatic failover. If the mirror server instance goes down, the principal server instance is unaffected. If the principal server instance goes down, you can force service (with possible data loss) to the mirror server instance.  
  
 If SAFETY is set to OFF, the session runs in high-performance mode, and automatic failover and manual failover are not supported. However, problems on the mirror do not affect the principal, and if the principal server instance goes down, you can, if necessary, force service (with possible data loss) to the mirror server instance-if WITNESS is set to OFF or the witness is currently connected to the mirror. For more information on forcing service, see "FORCE_SERVICE_ALLOW_DATA_LOSS" earlier in this section.  
  
> [!IMPORTANT]  
>  High-performance mode is not intended to use a witness. However, whenever you set SAFETY to OFF, we strongly recommend that you ensure that WITNESS is set to OFF.  
  
 SUSPEND  
 Pauses a database mirroring session.  
  
 You can specify SUSPEND on either partner.  
  
 TIMEOUT *integer*  
 Specifies the time-out period in seconds. The time-out period is the maximum time that a server instance waits to receive a PING message from another instance in the mirroring session before considering that other instance to be disconnected.  
  
 You can specify the TIMEOUT option only on the principal server. If you do not specify this option, by default, the time period is 10 seconds. If you specify 5 or greater, the time-out period is set to the specified number of seconds. If you specify a time-out value of 0 to 4 seconds, the time-out period is automatically set to 5 seconds.  
  
> [!IMPORTANT]  
>  We recommend that you keep the time-out period at 10 seconds or greater. Setting the value to less than 10 seconds creates the possibility of a heavily loaded system missing PINGs and declaring a false failure.  
  
 For more information, see [Possible Failures During Database Mirroring](../../database-engine/database-mirroring/possible-failures-during-database-mirroring.md).  
  
 WITNESS \<witness_option>  
 Controls the database properties that define a database mirroring witness. A SET WITNESS clause affects both copies of the database, but you can specify SET WITNESS only on the principal server. If a witness is set for a session, quorum is required to serve the database, regardless of the SAFETY setting; for more information, see [Quorum: How a Witness Affects Database Availability &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/quorum-how-a-witness-affects-database-availability-database-mirroring.md).  
  
 We recommend that the witness and failover partners reside on separate computers. For information about the witness, see [Database Mirroring Witness](../../database-engine/database-mirroring/database-mirroring-witness.md).  
  
 To execute a SET WITNESS statement, the STATE of the endpoints of both the principal and witness server instances must be set to STARTED. Note, also, that the ROLE of the database mirroring endpoint of a witness server instance must be set to either WITNESS or ALL. For information about specifying an endpoint, see [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md).  
  
 To learn the role and state of the database mirroring endpoint of a server instance, on that instance, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
```  
SELECT role_desc, state_desc FROM sys.database_mirroring_endpoints  
```  
  
> [!NOTE]  
>  Database properties cannot be set on the witness.  
  
 **\<witness_option> ::=**  
  
> [!NOTE]  
>  Only one \<witness_option> is permitted per SET WITNESS clause.  
  
 **'** _witness_server_ **'**  
 Specifies an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to act as the witness server for a database mirroring session. You can specify SET WITNESS statements only on the principal server.  
  
 In a SET WITNESS **='**_witness_server_**'** statement, the syntax of *witness_server* is the same as the syntax of *partner_server*.  
  
 OFF  
 Removes the witness from a database mirroring session. Setting the witness to OFF disables automatic failover. If the database is set to FULL SAFETY and the witness is set to OFF, a failure on the mirror server causes the principal server to make the database unavailable.  
  
## Remarks  
  
## Examples  
  
### A. Creating a database mirroring session with a witness  
 Setting up database mirroring with a witness requires configuring security and preparing the mirror database, and also using ALTER DATABASE to set the partners. For an example of the complete setup process, see [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md).  
  
### B. Manually failing over a database mirroring session  
 Manual failover can be initiated from either database mirroring partner. Before failing over, you should verify that the server you believe to be the current principal server actually is the principal server. For example, for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, on that server instance that you think is the current principal server, execute the following query:  
  
```  
SELECT db.name, m.mirroring_role_desc   
FROM sys.database_mirroring m   
JOIN sys.databases db  
ON db.database_id = m.database_id  
WHERE db.name = N'AdventureWorks2012';   
GO  
```  
  
 If the server instance is in fact the principal, the value of `mirroring_role_desc` is `Principal`. If this server instance were the mirror server, the `SELECT` statement would return `Mirror`.  
  
 The following example assumes that the server is the current principal.  
  
1.  Manually fail over to the database mirroring partner:  
  
    ```  
    ALTER DATABASE AdventureWorks2012 SET PARTNER FAILOVER;  
    GO  
    ```  
  
2.  To verify the results of the failover on the new mirror, execute the following query:  
  
    ```  
    SELECT db.name, m.mirroring_role_desc   
    FROM sys.database_mirroring m   
    JOIN sys.databases db  
    ON db.database_id = m.database_id  
    WHERE db.name = N'AdventureWorks2012';   
    GO  
    ```  
  
     The current value of `mirroring_role_desc` is now `Mirror`.  
  
## See Also  
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md?&tabs=sqlserver)   
 [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md)   
 [sys.database_mirroring_witnesses &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)  
  
  

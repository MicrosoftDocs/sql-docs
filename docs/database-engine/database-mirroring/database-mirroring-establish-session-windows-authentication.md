---
title: "Configure database mirroring"
description: "Steps to configure a database mirroring relationship between a principal and mirror using Windows authentication."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Windows authentication [SQL Server]"
  - "database mirroring [SQL Server], security"
---
# Configure database mirroring
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] instead.  
  
 After the mirror database is prepared (see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md)), you can establish a database mirroring session. The principal, mirror, and witness server instances must be separate server instances, which should be on separate host systems.  
  
> [!IMPORTANT]
>  We recommend that you configure database mirroring during off-peak hours because configuring mirroring can impact performance.  
> 
> [!NOTE]
>  A given server instance can participate in multiple concurrent database mirroring sessions with the same or different partners. A server instance can be a partner in some sessions and a witness in other sessions. The mirror server instance must be running the same edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as the principal server instance. Database mirroring is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md). Also, we strongly recommend that they run on comparable systems that can handle identical workloads.  
  
### To establish a database mirroring session  
  
1.  Create the mirror database. For more information, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md).  
  
2.  Set up security on each server instance.  
  
     Each server instance in a database mirroring session requires a database mirroring endpoint. If the endpoint does not exist, you must create it.  
  
    > [!NOTE]  
    >  The form of authentication used for database mirroring by a server instance is a property of its database mirroring endpoint. Two types of transport security are available for database mirroring: Windows Authentication or certificate-based authentication. For more information, see [Transport Security for Database Mirroring and Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md).  
  
     On each partner server, ensure that an endpoint exists for database mirroring. Regardless of the number of mirroring sessions to be supported, the server instance can have only one database mirroring endpoint. If you intend to use this server instance exclusively for partners in database mirroring sessions, you can assign the role of partner to the endpoint (ROLE**=**PARTNER). If you intend also to use this server for the witness in other database mirroring sessions, assign the role of the endpoint as ALL.  
  
     To execute a SET PARTNER statement, the STATE of the endpoints of both partners must be set to STARTED.  
  
     To learn whether a server instance has a database mirroring endpoint and to learn its role and state, on that instance, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
    ```  
    SELECT role_desc, state_desc FROM sys.database_mirroring_endpoints  
    ```  
  
    > [!IMPORTANT]  
    >  Do not reconfigure an in-use database mirroring endpoint. If a database mirroring endpoint exists and is already in use, we recommend that you use that endpoint for every session on the server instance. Dropping an in-use endpoint can cause the endpoint to restart, disrupting the connections of the existing sessions, which can appear to be an error to the other server instances. This is particularly important in high-safety mode with automatic failover, in which reconfiguring the endpoint on a partner could cause a failover to occur. Also, if a witness has been set for a session, dropping the database mirroring endpoint can cause the principal server of that session to lose quorum; if that occurs, the database is taken offline and its users are disconnected. For more information, see [Quorum: How a Witness Affects Database Availability &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/quorum-how-a-witness-affects-database-availability-database-mirroring.md).  
  
     If either partner lacks an endpoint, see [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md).  
  
3.  If server instances are running under different domain user accounts, each requires a login in the **master** database of the others. If the login does not exist, you must create it. For more information, see [Allow Network Access to a Database Mirroring Endpoint Using Windows Authentication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-allow-network-access-windows-authentication.md).  
  
4.  To set the principal server as partner on the mirror database, connect to the mirror server, and issue the following statement:  
  
     ALTER DATABASE *\<database_name\>* SET PARTNER **=**_\<server\_network\_address\>_  
  
     where _\<database\_name\>_ is the name of the database to be mirrored (this name is the same on both partners), and _\<server\_network\_address\>_ is the server network address of the principal server.  
  
     The syntax for a server network address is as follows:  
  
     TCP<b>\://</b>_\<system-address\>_<b>\:</b>_\<port\>_  
  
     where _\<system-address>_ is a string that unambiguously identifies the destination computer system, and _\<port>_ is the port number used by the mirroring endpoint of the partner server instance. For more information, see [Specify a Server Network Address &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md).  
  
     For example, on the mirror server instance, the following ALTER DATABASE statement sets the partner as the original principal server instance. The database name is **AdventureWorks**, the system address is DBSERVER1-the name of the partner's system-and the port used by the partner's database mirroring endpoint is 7022:  
  
    ```  
    ALTER DATABASE AdventureWorks   
       SET PARTNER = 'TCP://DBSERVER1:7022'  
    ```  
  
     This statement prepares the mirror server to form a session when it is contacted by the principal server.  
  
5.  To set the mirror server as partner on the principal database, connect to the principal server, and issue the following statement:  
  
     ALTER DATABASE _\<database\_name\>_ SET PARTNER **=**_\<server\_network\_address\>_  
  
     For more information, see step 4.  
  
     For example, on the principal server instance, the following ALTER DATABASE statement sets the partner as the original mirror server instance. The database name is **AdventureWorks**, the system address is DBSERVER2-the name of the partner's system-and the port used by the partner's database mirroring endpoint is 7025:  
  
    ```  
    ALTER DATABASE AdventureWorks SET PARTNER = 'TCP://DBSERVER2:7022'  
    ```  
  
     Entering this statement on the principal server begins the database mirroring session.  
  
6.  By default a session is set to full transaction safety (SAFETY is set to FULL), which starts the session in synchronous, high-safety mode without automatic failover. You can reconfigure the session to run in high-safety mode with automatic failover or in asynchronous, high-performance mode, as follows:  
  
    -   **High-safety mode with automatic failover**  
  
         If you want a high-safety mode session to support automatic failover, add a witness server instance. For more information, see [Add a Database Mirroring Witness Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/add-a-database-mirroring-witness-using-windows-authentication-transact-sql.md).  
  
    -   **High-performance mode**  
  
         Alternatively, if you do not want automatic failover and you prefer to emphasize performance over availability, turn off transaction safety. For more information, see [Change Transaction Safety in a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/change-transaction-safety-in-a-database-mirroring-session-transact-sql.md).  
  
        > [!NOTE]  
        >  In high-performance mode, WITNESS should be set to OFF. For more information, see [Quorum: How a Witness Affects Database Availability &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/quorum-how-a-witness-affects-database-availability-database-mirroring.md).  
  
## Example  
  
> [!NOTE]  
>  The following example establishes a database mirroring session between partners for an existing mirror database. For information on creating a mirror database, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md)=.  
  
 The example shows the basic steps for creating a database mirroring session without a witness. The two partners are the default server instances on two computer systems (PARTNERHOST1 and PARTNERHOST5). The two partner instances run the same Windows domain user account (MYDOMAIN\dbousername).  
  
1.  On the principal server instance (default instance on PARTNERHOST1), create an endpoint that supports all roles using port 7022:  
  
    ```  
    --create an endpoint for this instance  
    CREATE ENDPOINT Endpoint_Mirroring  
        STATE=STARTED   
        AS TCP (LISTENER_PORT=7022)   
        FOR DATABASE_MIRRORING (ROLE=ALL)  
    GO  
    --Partners under same domain user; login already exists in master.  
    ```  
  
    > [!NOTE]  
    >  For an example of how to setup a login, see [Allow Network Access to a Database Mirroring Endpoint Using Windows Authentication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-allow-network-access-windows-authentication.md).  
  
2.  On the mirror server instance (default instance on PARTNERHOST5), create an endpoint that supports all roles using port 7022:  
  
    ```  
    --create an endpoint for this instance  
    CREATE ENDPOINT Endpoint_Mirroring  
        STATE=STARTED   
        AS TCP (LISTENER_PORT=7022)   
        FOR DATABASE_MIRRORING (ROLE=ALL)  
    GO  
    --Partners under same domain user; login already exists in master.  
    ```  
  
3.  On the principal server instance (on PARTNERHOST1), back up the database:  
  
    ```  
    BACKUP DATABASE AdventureWorks   
        TO DISK = 'C:\AdvWorks_dbmirror.bak'   
        WITH FORMAT  
    GO  
    ```  
  
4.  On the mirror server instance (on `PARTNERHOST5`), restore the database:  
  
    ```  
    RESTORE DATABASE AdventureWorks   
        FROM DISK = 'Z:\AdvWorks_dbmirror.bak'   
        WITH NORECOVERY  
    GO  
    ```  
  
5.  After you create the full database backup, you must create a log backup on the principal database. For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement backs up the log to the same file used by the preceding database backup:  
  
    ```  
    BACKUP LOG AdventureWorks   
        TO DISK = 'C:\AdventureWorks.bak'   
    GO  
    ```  
  
6.  Before you can start mirroring, you must apply the required log backup (and any subsequent log backups).  
  
     For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement restores the first log from C:\AdventureWorks.bak:  
  
    ```  
    RESTORE LOG AdventureWorks   
        FROM DISK = 'C:\ AdventureWorks.bak'   
        WITH FILE=1, NORECOVERY  
    GO  
    ```  
  
7.  On the mirror server instance, set the server instance on PARTNERHOST1 as the partner (making it the initial principal server):  
  
    ```  
    USE master;  
    GO  
    ALTER DATABASE AdventureWorks   
        SET PARTNER =   
        'TCP://PARTNERHOST1:7022'  
    GO  
    ```  
  
    > [!IMPORTANT]  
    >  default, a database mirroring session runs in synchronous mode, which depends on having full transaction safety (SAFETY is set to FULL). To cause a session to run in asynchronous, high-performance mode, set SAFETY to OFF. For more information, see [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md).  
  
8.  On the principal server instance, set the server instance on `PARTNERHOST5` as the partner (making it the initial mirror server):  
  
    ```  
    USE master;  
    GO  
    ALTER DATABASE AdventureWorks   
        SET PARTNER = 'TCP://PARTNERHOST5:7022'  
    GO  
    ```  
  
9. Optionally, if you intend to use high-safety mode with automatic failover, set up the witness server instance. For more information, see [Add a Database Mirroring Witness Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/add-a-database-mirroring-witness-using-windows-authentication-transact-sql.md).  
  
> [!NOTE]  
>  For a complete example showing security setup, preparing the mirror database, setting up the partners, and adding a witness, see [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md).  
  
## See Also  
 [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [Allow Network Access to a Database Mirroring Endpoint Using Windows Authentication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-allow-network-access-windows-authentication.md)   
 [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md)   
 [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)   
 [Database Mirroring and Log Shipping &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-log-shipping-sql-server.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Database Mirroring and Replication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md)   
 [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md)   
 [Specify a Server Network Address &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md)   
 [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md)  

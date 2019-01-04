---
title: "Add a Database Mirroring Witness Using Windows Authentication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "witness [SQL Server], establishing"
  - "Windows authentication [SQL Server]"
  - "database mirroring [SQL Server], witness"
ms.assetid: bf5e87df-91a4-49f9-ae88-2a6dcf644510
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Add a Database Mirroring Witness Using Windows Authentication (Transact-SQL)
  To set up a witness for a database, the database owner assigns a Database Engine instance to the role of witness server. The witness server instance can run on the same computer as the principal or mirror server instance, but this substantially reduces the robustness of automatic failover.  
  
 We strongly recommend that the witness reside on a separate computer. A given server can participate in multiple concurrent database mirroring sessions with the same or different partners. A given server can be a partner in some sessions and a witness in other sessions.  
  
 The witness is intended exclusively for high-safety mode with automatic failover. Before you set a witness, we strongly recommend that you ensure that the SAFETY property is currently set to FULL.  
  
> [!IMPORTANT]  
>  We recommend that you configure database mirroring during off-peak hours because configuration can impact performance.  
  
### To establish a witness  
  
1.  On the witness server instance, ensure that an endpoint exists for database mirroring. Regardless of the number of mirroring session to be supported, the server instance must have only one database mirroring endpoint. If you intend to use this server instance exclusively as a witness in database mirroring sessions, assign the role of witness to the endpoint (ROLE**=**WITNESS). If you intend to use this server instance as a partner in one or more other database mirroring sessions, assign the role of the endpoint as ALL.  
  
     To execute a SET WITNESS statement, the database mirroring session must already be started (between the partners), and the STATE of the endpoint of the witness must be set to STARTED.  
  
     To learn whether the witness server instance has its database mirroring endpoint and to learn its role and state, on that instance, use the following Transact-SQL statement:  
  
    ```  
    SELECT role_desc, state_desc FROM sys.database_mirroring_endpoints  
    ```  
  
    > [!IMPORTANT]  
    >  If a database mirroring endpoint exists and is already in use, we recommend that you use that endpoint for every session on the server instance. Dropping an in-use endpoint disrupts the connections of the existing sessions. If a witness has been set for a session, dropping the database mirroring endpoint can cause the principal server of that session to lose quorum; if that occurs, the database is taken offline and its users are disconnected. For more information, see [Quorum: How a Witness Affects Database Availability &#40;Database Mirroring&#41;](quorum-how-a-witness-affects-database-availability-database-mirroring.md).  
  
     If the witness lacks an endpoint, see [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md).  
  
2.  If the partner instances are running under different domain user accounts, create a login for the different accounts in the master database of each instance. For more information, see [Allow Network Access to a Database Mirroring Endpoint Using Windows Authentication &#40;SQL Server&#41;](../database-mirroring-allow-network-access-windows-authentication.md).  
  
3.  Connect to the principal server and issue the following statement:  
  
     ALTER DATABASE *<database_name>* SET WITNESS **=**_<server_network_address>_  
  
     where *<database_name>* is the name of the database to be mirrored (this name is the same on both partners), and *<server_network_address>* is the server network address of the witness server instance.  
  
     The syntax for a server network address is as follows:  
  
     TCP**://**\<_system-address>_**:**\<*port>*  
  
     where \<*system-address>* is a string that unambiguously identifies the destination computer system, and \<*port>* is the port number used by the mirroring endpoint of the partner server instance. For more information, see [Specify a Server Network Address &#40;Database Mirroring&#41;](specify-a-server-network-address-database-mirroring.md).  
  
     For example, on the principal server instance, the following ALTER DATABASE statement sets the witness. The database name is **AdventureWorks**, the system address is DBSERVER3-the name of the witness system, and the port used by the database mirroring endpoint of the witness is `7022`:  
  
    ```  
    ALTER DATABASE AdventureWorks   
      SET WITNESS = 'TCP://DBSERVER3:7022'  
    ```  
  
## Example  
 The following example establishes a data mirroring witness. On the witness server instance (default instance on `WITNESSHOST4`):  
  
1.  Create an endpoint for this server instance for the WITNESS role only using port `7022`.  
  
    ```  
    CREATE ENDPOINT Endpoint_Mirroring  
        STATE=STARTED   
        AS TCP (LISTENER_PORT=7022)   
        FOR DATABASE_MIRRORING (ROLE=WITNESS)  
    GO  
    ```  
  
2.  Create a login for domain user account of partner instances, if different; for example, assume that the witness is running as `SOMEDOMAIN\witnessuser`, but the partners are running as `MYDOMAIN\dbousername`. Create a login for the partners, as follows:  
  
    ```  
    --Create a login for the partner server instances,  
    --which are both running as MYDOMAIN\dbousername:  
    USE master ;  
    GO  
    CREATE LOGIN [MYDOMAIN\dbousername] FROM WINDOWS ;  
    GO  
    --Grant connect permissions on endpoint to login account   
    --of partners  
    GRANT CONNECT ON ENDPOINT::Endpoint_Mirroring TO [MYDOMAIN\dbousername];  
    GO  
    ```  
  
3.  On each of the partner server instances, create a login for the witness server instance:  
  
    ```  
    --Create a login for the witness server instance,  
    --which is running as SOMEDOMAIN\witnessuser:  
    USE master ;  
    GO  
    CREATE LOGIN [SOMEDOMAIN\witnessuser] FROM WINDOWS ;  
    GO  
    --Grant connect permissions on endpoint to login account   
    --of partners  
    GRANT CONNECT ON ENDPOINT::Endpoint_Mirroring TO [SOMEDOMAIN\witnessuser];  
    GO  
    ```  
  
4.  On the principal server, set the witness (which is on `WITNESSHOST4`):  
  
    ```  
    ALTER DATABASE AdventureWorks   
        SET WITNESS =   
        'TCP://WITNESSHOST4:7022'  
    GO  
    ```  
  
> [!NOTE]  
>  The server network address indicates the target server instance by the port number, which maps to the mirroring endpoint of the instance.  
  
 For a complete example showing security setup, preparing the mirror database, setting up the partners, and adding a witness, see [Setting Up Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md).  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)   
 [Allow Network Access to a Database Mirroring Endpoint Using Windows Authentication &#40;SQL Server&#41;](../database-mirroring-allow-network-access-windows-authentication.md)   
 [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)   
 [Establish a Database Mirroring Session Using Windows Authentication &#40;Transact-SQL&#41;](database-mirroring-establish-session-windows-authentication.md)   
 [Remove the Witness from a Database Mirroring Session &#40;SQL Server&#41;](remove-the-witness-from-a-database-mirroring-session-sql-server.md)   
 [Database Mirroring Witness](database-mirroring-witness.md)  
  
  

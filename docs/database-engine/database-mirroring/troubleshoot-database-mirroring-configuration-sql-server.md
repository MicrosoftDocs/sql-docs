---
title: "Common issues when configuring database mirroring"
description: "Provides information to help troubleshoot problems setting up a database mirroring session."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "database mirroring [SQL Server], deployment"
  - "endpoints [SQL Server], database mirroring"
  - "database mirroring [SQL Server], troubleshooting"
  - "troubleshooting [SQL Server], database mirroring"
---
# Troubleshoot Database Mirroring Configuration (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic provides information to help you troubleshoot problems in setting up a database mirroring session.  
  
> [!NOTE]  
>  Ensure that you are meeting all the [prerequisites for database mirroring](../../database-engine/database-mirroring/prerequisites-restrictions-and-recommendations-for-database-mirroring.md).  
  
|Issue|Summary|  
|-----------|-------------|  
|Error Message 1418|This [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] message indicates that the server network address cannot be reached or does not exist, and it suggests that you verify the network address name and reissue the command. |  
|[Accounts](#Accounts)|Discusses requirements for correctly configuring the accounts under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.|  
|[Endpoints](#Endpoints)|Discusses requirements for correctly configuring the database mirroring endpoint of each server instance.|  
|[SystemAddress](#SystemAddress)|Summarizes the alternatives for specifying the system name of a server instance in a database mirroring configuration.|  
|[Network access](#NetworkAccess)|Documents the requirement that each the server instance be able to access the ports of the other server instance or instances over TCP.|  
|[Mirror database preparation](#MirrorDbPrep)|Summarizes the requirements for preparing the mirror database to enable mirroring to start.|  
|[Failed create-file operation](#FailedCreateFileOp)|Describes how to respond to a failed create-file operation.|  
|[Starting mirroring by Using Transact-SQL](#StartDbm)|Describes the required order for ALTER DATABASE *database_name* SET PARTNER **='**_partner_server_**'** statements.|  
|[Cross-Database Transactions](#CrossDbTxns)|An automatic failover could lead to automatic and possibly incorrect resolution of in-doubt transactions. For this reason database mirroring does not support cross-database transactions.|  
  
##  <a name="Accounts"></a> Accounts  
 The accounts under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running must be correctly configured.  
  
1.  Do the accounts have the correct permissions?  
  
    1.  If the accounts are running in the same domain accounts, the chances of misconfiguration are reduced.  
  
    2.  If the accounts are running in different domains or are not domain accounts, the login of one account must be created in **master** on the other computer, and that login must be granted CONNECT permissions on the endpoint. For more information, see [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md). This includes the Network Service account.  
  
2.  If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running as a service that is using the local system account, you must use certificates for authentication. For more information, see [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md).  
  
##  <a name="Endpoints"></a> Endpoints  
 Endpoints must be correctly configured.  
  
1.  Make sure that each server instance (the principal server, mirror server, and witness, if any) has a database mirroring endpoint. For more information, see [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md) and, depending on the form of authentication, either [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md) or [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md).  
  
2.  Check that the port numbers are correct.  
  
     To identify the port currently associated with database mirroring endpoint of a server instance, use the [sys.database_mirroring_endpoints](../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md) and [sys.tcp_endpoints](../../relational-databases/system-catalog-views/sys-tcp-endpoints-transact-sql.md) catalog views.  
  
3.  For database mirroring setup issues that are difficult to explain, we recommend that you inspect each server instance to determine whether it is listening on the correct ports.   
  
4.  Make sure that the endpoints are started (STATE=STARTED). On each server instance, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
    ```  
    SELECT state_desc FROM sys.database_mirroring_endpoints  
    ```  
  
     For more information about the **state_desc** column, see [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md).  
  
     To start an endpoint, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
    ```  
    ALTER ENDPOINT Endpoint_Mirroring   
    STATE = STARTED   
    AS TCP (LISTENER_PORT = <port_number>)  
    FOR database_mirroring (ROLE = ALL);  
    GO  
    ```  
  
     For more information, see [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md).  
  
5.  Check that the ROLE is correct. On each server instance use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
    ```  
    SELECT role FROM sys.database_mirroring_endpoints;  
    GO  
    ```  
  
     For more information, see [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md).  
  
6.  The login for the service account from the other server instance requires CONNECT permission. Make sure that the login from the other server has CONNECT permission. To determine who has CONNECT permission for an endpoint, on each server instance use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
    ```  
    SELECT 'Metadata Check';  
    SELECT EP.name, SP.STATE,   
       CONVERT(nvarchar(38), suser_name(SP.grantor_principal_id))   
          AS GRANTOR,   
       SP.TYPE AS PERMISSION,  
       CONVERT(nvarchar(46),suser_name(SP.grantee_principal_id))   
          AS GRANTEE   
       FROM sys.server_permissions SP , sys.endpoints EP  
       WHERE SP.major_id = EP.endpoint_id  
       ORDER BY Permission,grantor, grantee;   
    GO  
  
    ```  
  
##  <a name="SystemAddress"></a> System Address  
 For the system name of a server instance in a database mirroring configuration, you can use any name that unambiguously identifies the system. The server address can be a system name (if the systems are in the same domain), a fully qualified domain name, or an IP address (preferably, a static IP address). Using the fully qualified domain name is guaranteed to work. For more information, see [Specify a Server Network Address &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md).  
  
##  <a name="NetworkAccess"></a> Network Access  
 Each server instance must be able to access the ports of the other server instance or instances over TCP. This is especially important if the server instances are in different domains that do not trust each other (untrusted domains). This restricts much of the communication between the server instances.  
  
##  <a name="MirrorDbPrep"></a> Mirror Database Preparation  
 Whether starting mirroring for the first time or starting it again after mirroring was removed, verify that the mirror database is prepared for mirroring.  
  
 When you create the mirror database on the mirror server, make sure that you restore the backup of the principal database specifying the same database name WITH NORECOVERY. Also, all log backups created after that backup was taken must also be applied, again WITH NORECOVERY.  
  
 Also, we recommend that, if it is possible, the file path (including the drive letter) of the mirror database be identical to the path of the principal database. If the file paths must differ, for example, if the principal database is on drive 'F:' but the mirror system lacks an F: drive, you must include the MOVE option in the RESTORE statement.  
  
> [!IMPORTANT]  
>  If you move the database files when you are creating the mirror database, you might be unable to add files to the database later without mirroring being suspended.  
  
 If database mirroring has been stopped, all subsequent log backups taken on the principal database must be applied to the mirror database before mirroring can be restarted.  
  
 For more information, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md).  
  
##  <a name="FailedCreateFileOp"></a> Failed Create-File Operation  
 Adding a file without impacting a mirroring session requires that the path of the file exist on both servers. Therefore, if you move the database files when creating the mirror database, a later add-file operation might fail on the mirror database and cause mirroring to be suspended.  
  
 To fix the problem:  
  
1.  The database owner must remove the mirroring session and restore a full backup of the filegroup that contains the added file.  
  
2.  The owner must then back up the log containing the add-file operation on the principal server and manually restore the log backup on the mirror database using the WITH NORECOVERY and WITH MOVE options. Doing this creates the specified file path on the mirror server and restores the new file to that location.  
  
3.  To prepare the database for a new mirroring session, the owner must also restore WITH NO RECOVERY any other outstanding log backups from the principal server.  
  
 For more information, see [Removing Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/removing-database-mirroring-sql-server.md), [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md), [Establish a Database Mirroring Session Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-establish-session-windows-authentication.md), [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md), or [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md).  
  
##  <a name="StartDbm"></a> Starting mirroring by Using Transact-SQL  
 The order in which the ALTER DATABASE *database_name* SET PARTNER **='**_partner_server_**'** statements are issued is very important.  
  
1.  The first statement must be run on the mirror server. When this statement is issued, the mirror server does not try to contact any other server instance. Instead, the mirror server instructs its database to wait until the mirror server has been contacted by the principal server.  
  
2.  The second ALTER DATABASE statement must be run on the principal server. This statement causes the principal server to try to connect to the mirror server. After that connection is created, the mirror then tries to connect to the principal server on another connection.  
  
 For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
> [!NOTE]  
>  For information about using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to start mirroring, see [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md).  
  
##  <a name="CrossDbTxns"></a> Cross-Database Transactions  
 When a database is being mirrored in high-safety mode with automatic failover, an automatic failover could lead to automatic and possibly incorrect resolution of in-doubt transactions. If an automatic failover occurs on either database while a cross-database transaction is being committed, logical inconsistencies can occur between the databases.  
  
 The types of cross-database transactions that can be affected by an automatic failover include the following:  
  
-   A transaction that is updating multiple databases in the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Transactions that use a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC).  
  
 For more information, see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md).  
  
## See Also  
 [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md)   
 [Transport Security for Database Mirroring and Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md)  
  
  



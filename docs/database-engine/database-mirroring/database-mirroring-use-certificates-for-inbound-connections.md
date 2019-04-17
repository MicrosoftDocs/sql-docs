---
title: "Database Mirroring - Use Certificates for Inbound Connections | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "certificates [SQL Server], database mirroring"
  - "inbound connections"
  - "database mirroring [SQL Server], security"
ms.assetid: 5d48bb98-61f0-4b99-8f1a-b53f831d63d0
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Mirroring - Use Certificates for Inbound Connections
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes the steps for configuring server instances to use certificates to authenticate inbound connections for database mirroring. Before you can set up inbound connections, you must configure outbound connections on each server instance. For more information, see [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md).  
  
 The process of configuring inbound connections, involves the following general steps:  
  
1.  Create a login for other system.  
  
2.  Create a user for that login.  
  
3.  Obtain the certificate for the mirroring endpoint of the other server instance.  
  
4.  Associate the certificate with the user created in step 2.  
  
5.  Grant CONNECT permission on the login for that mirroring endpoint.  
  
 If there is a witness, you must also set up inbound connections for it. This requires setting up logins, users, and certificates for the witness on both of the partners, and vice versa.  
  
 The following procedure describes these steps in detail. For each step, the procedure provides an example for configuring a server instance on a system named HOST_A. The accompanying Example section demonstrates the same steps for another server instance on a system named HOST_B.  
  
### To configure server instances for inbound mirroring connections (on HOST_A)  
  
1.  Create a login for the other system.  
  
     The following example creates a login for the system, HOST_B, in the **master** database of the server instance on HOST_A; in this example, the login is named `HOST_B_login`. Substitute a password of your own for the sample password.  
  
    ```  
    USE master;  
    CREATE LOGIN HOST_B_login   
       WITH PASSWORD = '1Sample_Strong_Password!@#';  
    GO  
    ```  
  
     For more information, see [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md).  
  
     To view the logins on this server instance, you can use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
    ```  
    SELECT * FROM sys.server_principals  
    ```  
  
     For more information, see [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md).  
  
2.  Create a user for that login.  
  
     The following example creates a user, `HOST_B_user`, for the login created in the preceding step.  
  
    ```  
    USE master;  
    CREATE USER HOST_B_user FOR LOGIN HOST_B_login;  
    GO  
    ```  
  
     For more information, see [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md).  
  
     To view the users on this server instance, you can use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
    ```  
    SELECT * FROM sys.sysusers;  
    ```  
  
     For more information, see [sys.sysusers &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysusers-transact-sql.md).  
  
3.  Obtain the certificate for the mirroring endpoint of the other server instance.  
  
     If you have not already done so when configuring outbound connections, obtain a copy of the certificate for the mirroring endpoint of the remote server instance. To do this, back up the certificate on that server instance as described in [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md). When copying a certificate to another system, use a secure copy method. Be extremely careful to keep all of your certificates secure.  
  
     For more information, see [BACKUP CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/backup-certificate-transact-sql.md).  
  
4.  Associate the certificate with the user created in step 2.  
  
     The following example, associates the certificate of HOST_B with its user on HOST_A.  
  
    ```  
    USE master;  
    CREATE CERTIFICATE HOST_B_cert  
       AUTHORIZATION HOST_B_user  
       FROM FILE = 'C:\HOST_B_cert.cer'  
    GO  
    ```  
  
     For more information, see [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md).  
  
     To view the certificates on this server instance, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
    ```  
    SELECT * FROM sys.certificates  
    ```  
  
     For more information, see [sys.certificates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md).  
  
5.  Grant CONNECT permission on the login for the remote mirroring endpoint.  
  
     For example, to grant permission on HOST_A to the remote server instance on HOST_B to connect to its local login-that is, to connect to `HOST_B_login`-use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements:  
  
    ```  
    USE master;  
    GRANT CONNECT ON ENDPOINT::Endpoint_Mirroring TO [HOST_B_login];  
    GO  
    ```  
  
     For more information, see [GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-endpoint-permissions-transact-sql.md).  
  
 This completes setting up certificate authentication for HOST_B to log in to HOST_A.  
  
 You now need to perform the equivalent inbound steps for HOST_A on HOST_B. These steps are illustrated in the inbound portion of the example in the Example section, below.  
  
## Example  
 The following example demonstrates configuring HOST_B for inbound connections.  
  
> [!NOTE]  
>  This example uses a certificate file containing the HOST_A certificate that is created by a code snippet in [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md).  
  
```  
USE master;  
--On HOST_B, create a login for HOST_A.  
CREATE LOGIN HOST_A_login WITH PASSWORD = 'AStrongPassword!@#';  
GO  
--Create a user, HOST_A_user, for that login.  
CREATE USER HOST_A_user FOR LOGIN HOST_A_login  
GO  
--Obtain HOST_A certificate. (See the note   
--   preceding this example.)  
--Asscociate this certificate with the user, HOST_A_user.  
CREATE CERTIFICATE HOST_A_cert  
   AUTHORIZATION HOST_A_user  
   FROM FILE = 'C:\HOST_A_cert.cer';  
GO  
--Grant CONNECT permission for the server instance on HOST_A.  
GRANT CONNECT ON ENDPOINT::Endpoint_Mirroring TO HOST_A_login  
GO  
```  
  
 If you intend to run in high-safety mode with automatic failover, you must repeat the same set up steps to configure the witness for outbound and inbound connections.  
  
 For information on creating a mirror database, including a Transact-SQL example, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md).  
  
 For a Transact-SQL example of establishing a high-performance mode session, see [Example: Setting Up Database Mirroring Using Certificates &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/example-setting-up-database-mirroring-using-certificates-transact-sql.md).  
  
## .NET Framework Security  
 When copying a certificate to another system, use a secure copy method. Be extremely careful to keep all of your certificates secure.  
  
## See Also  
 [Transport Security for Database Mirroring and Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md)   
 [GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-endpoint-permissions-transact-sql.md)   
 [Set Up an Encrypted Mirror Database](../../database-engine/database-mirroring/set-up-an-encrypted-mirror-database.md)   
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Troubleshoot Database Mirroring Configuration &#40;SQL Server&#41;](../../database-engine/database-mirroring/troubleshoot-database-mirroring-configuration-sql-server.md)  
  
  

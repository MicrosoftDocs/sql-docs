---
title: "Transport Security - Database Mirroring - Always On Availability | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "sessions [SQL Server], database mirroring"
  - "cryptography [SQL Server], database mirroring"
  - "certificates [SQL Server], database mirroring"
  - "encryption [SQL Server], database mirroring"
  - "Windows authentication [SQL Server]"
  - "authentication [SQL Server], database mirroring"
  - "transport security"
  - "database mirroring [SQL Server], security"
ms.assetid: 49239d02-964e-47c0-9b7f-2b539151ee1b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Transport Security - Database Mirroring - Always On Availability
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Transport security involves authentication and, optionally, encryption of messages exchanged between the databases. For database mirroring and [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], authentication and encryption are configured on the database mirroring endpoint. For an introduction to database mirroring endpoints, see [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md).  
  
 **In this Topic:**  
  
-   [Authentication](#Authentication)  
  
-   [Data Encryption](#DataEncryption)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="Authentication"></a> Authentication  
 Authentication is the process of verifying that a user is who the user claims to be. Connections between database mirroring endpoints require authentication. Connection requests from a partner or witness, if any, must be authenticated.  
  
 The type of authentication used by a server instance for database mirroring or [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] is a property of the database mirroring endpoint. Two types of transport security are available for database mirroring endpoints: Windows Authentication (the Security Support Provider Interface (SSPI)) and certificate-based authentication.  
  
### Windows Authentication  
 Under Windows Authentication, each server instance logs in to the other side using the Windows credentials of the Windows user account under which the process is running. Windows Authentication might require some manual configuration of login accounts, as follows:  
  
-   If the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] run as services under the same domain account, no extra configuration is required.  
  
-   If the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] run as services under different domain accounts (in the same or trusted domains), the login of each account must be created in **master** on each of the other server instances, and that login must be granted CONNECT permissions on the endpoint.  
  
-   If the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] run as the Network Service account, the login of the each host computer account (_DomainName_**\\**_ComputerName$_) must be created in **master** on each of the other servers, and that login must be granted CONNECT permissions on the endpoint. This is because a server instance running under the Network Service account authenticates using the domain account of the host computer.  
  
> [!NOTE]  
>  For an example of setting up a database mirroring session using Windows Authentication, see [Example: Setting Up Database Mirroring Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/example-setting-up-database-mirroring-using-windows-authentication-transact-sql.md).  
  
### Certificates  
 In some situations, such as when server instances are not in trusted domains or when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running as a local service, Windows Authentication is unavailable. In such cases, instead of user credentials, certificates are required to authenticate connection requests. The mirroring endpoint of each server instance must be configured with its own locally created certificate.  
  
 The encryption method is established when the certificate is created. For more information, see [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md). Carefully manage the certificates that you use.  
  
 A server instance uses the private key of its own certificate to establish its identity when setting up a connection. The server instance that receives the connection request uses the public key of the sender's certificate to authenticate the sender's identity. For example, consider two server instances, Server_A and Server_B. Server_A uses its private key to encrypt the connection header before sending a connection request to Server_B. Server_B uses the public key of Server_A's certificate to decrypt the connection header. If the decrypted header is correct, Server_B knows that the header was encrypted by Server_A, and the connection is authenticated. If the decrypted header is incorrect, Server_B knows that the connection request is inauthentic and refuses the connection.  
  
##  <a name="DataEncryption"></a> Data Encryption  
 By default, a database mirroring endpoint requires encryption of data sent over mirroring connections. In this case, the endpoint can connect only to endpoints that also use encryption. Unless you can guarantee that your network is secure, we recommend that you require encryption for your database mirroring connections. However, you can disable encryption or make it supported, but not required. If encryption is disabled, data is never encrypted and the endpoint cannot connect to an endpoint that requires encryption. If encryption is supported, data is encrypted only if the opposite endpoint either supports or requires encryption.  
  
> [!NOTE]  
>  Mirroring endpoints created by [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] are created with encryption either required or disabled. To change the encryption setting to SUPPORTED, use the ALTER ENDPOINT [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. For more information, see [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md).  
  
 Optionally, you can control the encryption algorithms that can be used by an endpoint, by specifying one of the following values for the ALGORITHM option in a CREATE ENDPOINT statement or ALTER ENDPOINT statement:  
  
|ALGORITHM value|Description|  
|---------------------|-----------------|  
|RC4|Specifies that the endpoint must use the RC4 algorithm. This is the default.<br /><br /> <strong>\*\* Warning \*\*</strong> The RC4 algorithm is deprecated. [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] We recommend that you use AES.|  
|AES|Specifies that the endpoint must use the AES algorithm.|  
|AES RC4|Specifies that the two endpoints will negotiate for an encryption algorithm with this endpoint giving preference to the AES algorithm.|  
|RC4 AES|Specifies that the two endpoints will negotiate for an encryption algorithm with this endpoint giving preference to the RC4 algorithm.|  
  
 If connecting endpoints specify both algorithms but in different orders, the endpoint accepting the connection wins.  
  
> [!NOTE]  
>  The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]and higher versions,  material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  
>   
>  Though considerably faster than AES, RC4 is a relatively weak algorithm, while AES is a relatively strong algorithm. Therefore, we recommend that you use the AES algorithm.  
  
 For information about the [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax for specifying encryption, see [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To configure transport security for a database mirroring endpoint**  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md)  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
-   [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md)  
  
## See Also  
 [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)   
 [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md)   
 [DROP ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/drop-endpoint-transact-sql.md)   
 [Security Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)   
 [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md)   
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)   
 [sys.dm_db_mirroring_connections &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-mirroring-sys-dm-db-mirroring-connections.md)   
 [Troubleshoot Database Mirroring Configuration &#40;SQL Server&#41;](../../database-engine/database-mirroring/troubleshoot-database-mirroring-configuration-sql-server.md)   
 [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
  
  

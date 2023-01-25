---
title: "Use certificates for a database mirroring endpoint"
description: "Use Certificates for a Database Mirroring Endpoint (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "database mirroring [SQL Server], deployment"
  - "certificates [SQL Server], database mirroring"
  - "authentication [SQL Server], database mirroring"
  - "database mirroring [SQL Server], security"
---
# Use Certificates for a Database Mirroring Endpoint (Transact-SQL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  To enable certificate authentication for database mirroring on a given server instance, the system administrator must configure each server instance to use certificates on both outbound and inbound connections. Outbound connections must be configured first.  
  
> [!NOTE]  
>  All mirroring connections on a server instance use a single database mirroring endpoint, and you must specify the authentication method of the server instance when you create the endpoint. Therefore, you can use only one form of authentication per server instance for database mirroring.  
  
## Configuring Outbound Connections  
 Follow these steps on each server instance that you are configuring for database mirroring:  
  
1.  In the **master** database, create a database master key.  
  
2.  In the **master** database, create an encrypted certificate on the server instance.  
  
3.  Create an endpoint for the server instance using its certificate.  
  
4.  Back up the certificate to a file and securely copy it to the other system or systems.  
  
 You must complete these steps for each partner and the witness, if there is one.  
  
 For more information, see [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md).  
  
## Configuring Inbound Connections  
 Next, follow these steps for each partner that you are configuring for database mirroring. In the **master** database:  
  
1.  Create a login for the other system.  
  
2.  Create a user for that login.  
  
3.  Obtain the certificate for the mirroring endpoint of the other server instance.  
  
4.  Associate the certificate with the user created in step 2.  
  
5.  Grant CONNECT permission on the login for that mirroring endpoint.  
  
 If there is a witness, you must also set up inbound connections for it. This requires setting up logins, users, and certificates for the witness on both of the partners, and vice versa.  
  
 For more information, see [Allow a Database Mirroring Endpoint to Use Certificates for Inbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-inbound-connections.md).  
  
## Security  
 Unless you can guarantee that your network is secure, we recommend that you use encryption for database mirroring connections. For more information, see [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md).  
  
 When copying a certificate to another system, use a secure copy method. Be extremely careful to keep all of your certificates secure.  
  
## See Also  
 [Create a Database Master Key](../../relational-databases/security/encryption/create-a-database-master-key.md)   
 [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md)   
 [Transport Security for Database Mirroring and Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md)   
 [Security Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)   
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)  
  
  

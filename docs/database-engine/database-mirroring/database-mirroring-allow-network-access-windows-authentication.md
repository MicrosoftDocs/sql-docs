---
title: "Database Mirroring - Allow Network Access - Windows Authentication"
description: Learn how to use Windows Authentication for connecting the database mirroring endpoints of two instances of SQL Server, which can require manual configuration.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
helpviewer_keywords:
  - "Windows authentication [SQL Server]"
  - "database mirroring [SQL Server], security"
---
# Database Mirroring - Allow Network Access - Windows Authentication
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Using Windows Authentication for connecting the database mirroring endpoints of two instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires manual configuration of login accounts under the following conditions:  
  
-   If the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] run as services under different domain accounts (in the same or trusted domains), the login of each account must be created in **master** on each of the remote server instances and that login must be granted CONNECT permissions on the endpoint.  
  
-   If the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] run as the Network Service account, the login of the each host computer account (*DomainName***\\***ComputerName$*) must be created in **master** on each of the remote server instances and that login must be granted CONNECT permissions on the endpoint. This is because a server instance running under the Network Service account authenticates using the domain account of the host computer.  
  
> [!NOTE]  
>  Ensure that an endpoint exists for each of the server instances. For more information, see [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md).  
  
### To configure logins for Windows Authentication  
  
1.  For the user account of each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], create a login on the other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use a [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md) statement with the `FROM WINDOWS` clause.  
  
     For more information, see [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md).  
  
2.  Also, to ensure that the login user has access to the endpoint, use the [GRANT](../../t-sql/statements/grant-transact-sql.md) statement to grant connect permissions on the endpoint to the login. Note that granting connect permissions to the endpoint is unnecessary if the user is an Administrator.  
  
     For more information, see [Grant a Permission to a Principal](../../relational-databases/security/authentication-access/grant-a-permission-to-a-principal.md).  
  
## Example  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] example creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for a user account named `Otheruser` that belongs to a domain called `Adomain`. The example then grants this user connect permissions to a pre-existing database mirroring endpoint named `Mirroring_Endpoint`.  
  
```  
USE master;  
GO  
CREATE LOGIN [Adomain\Otheruser] FROM WINDOWS;  
GO  
GRANT CONNECT on ENDPOINT::Mirroring_Endpoint TO [Adomain\Otheruser];  
GO  
```  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Transport Security for Database Mirroring and Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md)   
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)  
  
  

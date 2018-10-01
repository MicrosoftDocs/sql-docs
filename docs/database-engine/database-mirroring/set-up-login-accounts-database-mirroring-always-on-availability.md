---
title: "Set Up Login Accounts - Database Mirroring Always On Availability | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "database mirroring [SQL Server], deployment"
  - "logins [SQL Server], database mirroring"
ms.assetid: e9f5287b-1325-4cda-88a6-19eaaa52a652
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Set Up Login Accounts - Database Mirroring Always On Availability
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  For two server instances to connect to each other's [database mirroring endpoint](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md) point, the login account of each instance requires access to the other instance. Also, each login account requires connect permission to the Database Mirroring endpoint of the other instance.  
  
 The impact of this requirement depends on whether the server instances run as the same domain user account:  
  
-   If the server instances run as the same domain user account, the correct user logins exist automatically in both **master** databases. This simplifies the security configuration for Database Mirroring and Always On Availability Groups.  
  
-   If the server instances run as different user accounts, user logins on the server instance that hosts the principal server or primary replica must be manually reproduced on the server instance that hosts the mirror server or on every server instance that hosts a secondary replica. For more information, see [Create a Login for a Different Account](#CreateLogin) and [Grant Connect Permission](#GrantConnect), later in this topic.  
  
    > [!IMPORTANT]  
    >  To create a more secure environment, consider using separate domain accounts for each server instance.  
  
##  <a name="CreateLogin"></a> Create a Login for a Different Account  
 If two server instances run as different accounts, the system administrator must use the CREATE LOGIN [!INCLUDE[tsql](../../includes/tsql-md.md)] statement to create a login for the startup service account of the remote instance for each server instance. For more information, see [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md).  
  
> [!IMPORTANT]  
>  If you run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] under a non-domain account, you must use certificates. For more information, see [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md).  
  
 For example, for the server instance sqlA, which runs under loginA, to connect to the server instance sqlB, which runs under loginB, loginA must exist on sqlB, and loginB must exist on sqlA. In addition, for a database mirroring session that includes a witness server instance (sqlC) and in which the three server instances run under different domain accounts, the following logins must be created:  
  
|On instance...|Create logins for and grant connection permission to ...|  
|--------------------|--------------------------------------------------------------|  
|sqlA|sqlB and sqlC|  
|sqlB|sqlA and sqlC|  
|sqlC|sqlA and sqlB|  
  
> [!NOTE]  
>  It is possible to connect with the network service account by using the machine account instead of a domain user. If the machine account is used, it must be added as a user on the other server instance.  
  
##  <a name="GrantConnect"></a> Grant Connect Permission  
 Once a login has been created on a server instance, the login must be granted permission to connect to the database mirroring endpoint of the server instance. The system administrator grants the connect permission using a GRANT [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. For more information, see [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md)  
  
-   [Allow Network Access to a Database Mirroring Endpoint Using Windows Authentication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-allow-network-access-windows-authentication.md).  
  
-   [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)  
  
## See Also  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Troubleshoot Database Mirroring Configuration &#40;SQL Server&#41;](../../database-engine/database-mirroring/troubleshoot-database-mirroring-configuration-sql-server.md)   
 [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
  
  

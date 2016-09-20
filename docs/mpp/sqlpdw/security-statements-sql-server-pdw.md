---
title: "Security Statements (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5fa250a4-1a91-4b45-bf15-4b327cb1a41d
caps.latest.revision: 5
author: BarbKess
---
# Security Statements (SQL Server PDW)
SQL Server PDW includes statements for manipulation of logins, users, roles, security related objects.  
  
This topic is organized by object type, as shown:  
  
[User, Login, and Role Management](#LoginServerRole)  
  
[Master Keys, Database Keys, and Certificates](#Keys)  
  
[Special Statements](#Special)  
  
## <a name="LoginServerRole"></a>User, Login, and Role Management  
  
|Logins|Server Roles|Users|Database Roles|  
|----------|----------------|---------|------------------|  
|[CREATE LOGIN](../../mpp/sqlpdw/create-login-sql-server-pdw.md)|[CREATE SERVER ROLE](../../mpp/sqlpdw/create-server-role-sql-server-pdw.md)|[CREATE USER](../../mpp/sqlpdw/create-user-sql-server-pdw.md)|[CREATE ROLE](../../mpp/sqlpdw/create-role-sql-server-pdw.md)|  
|[ALTER LOGIN](../../mpp/sqlpdw/alter-login-sql-server-pdw.md)|[ALTER SERVER ROLE](../../mpp/sqlpdw/alter-server-role-sql-server-pdw.md)|[ALTER USER](../../mpp/sqlpdw/alter-user-sql-server-pdw.md)|[ALTER ROLE](../../mpp/sqlpdw/alter-role-sql-server-pdw.md)|  
|[DROP LOGIN](../../mpp/sqlpdw/drop-login-sql-server-pdw.md)|[DROP SERVER ROLE](../../mpp/sqlpdw/drop-server-role-sql-server-pdw.md)|[DROP USER](../../mpp/sqlpdw/drop-user-sql-server-pdw.md)|[DROP ROLE](../../mpp/sqlpdw/drop-role-sql-server-pdw.md)|  
  
## <a name="Keys"></a>Master Keys, Database Keys, and Certificates  
  
|Master Keys|Database Encryption Keys|Certificates|  
|---------------|----------------------------|----------------|  
|[CREATE MASTER KEY](../../mpp/sqlpdw/create-master-key-sql-server-pdw.md)|[CREATE DATABASE ENCRYPTION KEY](../../mpp/sqlpdw/create-database-encryption-key-sql-server-pdw.md)|[CREATE CERTIFICATE](../../mpp/sqlpdw/create-certificate-sql-server-pdw.md)|  
|[ALTER MASTER KEY](../../mpp/sqlpdw/alter-master-key-sql-server-pdw.md)|[ALTER DATABASE ENCRYPTION KEY](../../mpp/sqlpdw/alter-database-encryption-key-sql-server-pdw.md)|[BACKUP CERTIFICATE](../../mpp/sqlpdw/backup-certificate-sql-server-pdw.md)|  
|[OPEN MASTER KEY](../../mpp/sqlpdw/open-master-key-sql-server-pdw.md)|[DROP DATABASE ENCRYPTION KEY](../../mpp/sqlpdw/drop-database-encryption-key-sql-server-pdw.md)|[ALTER CERTIFICATE](../../mpp/sqlpdw/alter-certificate-sql-server-pdw.md)|  
|[CLOSE MASTER KEY](../../mpp/sqlpdw/close-master-key-sql-server-pdw.md)||[DROP CERTIFICATE](../../mpp/sqlpdw/drop-certificate-sql-server-pdw.md)|  
|[DROP MASTER KEY](../../mpp/sqlpdw/drop-master-key-sql-server-pdw.md)|||  
  
## <a name="Special"></a>Special Statements  
  
|Command|Description|  
|-----------|---------------|  
|[ALTER AUTHORIZATION](../../mpp/sqlpdw/alter-authorization-sql-server-pdw.md)|Changes ownership of an object.|  
|[Permissions: GRANT, DENY, REVOKE](../../mpp/sqlpdw/permissions-grant-deny-revoke-sql-server-pdw.md)|Describes how to manage object permissions.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Definition Statements &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-definition-statements-sql-server-pdw.md)  
[Data Manipulation Statements &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-manipulation-statements-sql-server-pdw.md)  
[Queries &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/queries-sql-server-pdw.md)  
  

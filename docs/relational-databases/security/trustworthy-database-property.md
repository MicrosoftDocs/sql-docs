---
title: "TRUSTWORTHY Database Property | Microsoft Docs"
description: Learn about the TRUSTWORTHY database property, which indicates whether the instance of SQL Server trusts the database and its contents. The default is OFF.
ms.custom: ""
ms.date: 08/24/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "TRUSTWORTHY database property"
ms.assetid: 64b2a53d-4416-4a19-acc0-664a61b45348
author: VanMSFT
ms.author: vanto
---

# TRUSTWORTHY database property

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The TRUSTWORTHY database property is used to indicate whether the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] trusts the database and the contents within it. By default, this setting is OFF, but can be set to ON by using the `ALTER DATABASE` statement. For example, `ALTER DATABASE AdventureWorks2012 SET TRUSTWORTHY ON;`.  
  
> [!NOTE]  
> To set this option, you must be a member of the **sysadmin** fixed server role.  

We recommend that you leave the TRUSTWORTHY database property set to OFF to mitigate certain threats that can exist as a result of attaching a database that contains one of the following objects:
  
- Malicious assemblies with an EXTERNAL_ACCESS or UNSAFE permission setting. For more information, see [CLR Integration Security](../../relational-databases/clr-integration/security/clr-integration-security.md).  
  
- Malicious modules that are defined to execute as high privileged users. For more information, see [EXECUTE AS Clause (Transact-SQL)](../../t-sql/statements/execute-as-clause-transact-sql.md).  
  
Both situations require a specific degree of privilege and are protected by appropriate mechanisms when they are used in the context of a database that is already attached to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, if the database is taken offline, if you have access to the database file you can potentially attach it to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] of your choice and add malicious content to the database. When databases are detached and attached in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], certain permissions are set on the data and log files that restrict access to the database files.  
  
Because a database that is attached to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can't be immediately trusted, the database isn't allowed to access resources beyond the scope of the database until the database is explicitly marked trustworthy. Therefore, if you back up or detach a database that has the TRUSTWORTHY option ON and you attach or restore the database to the same or another SQL Server instance, the TRUSTWORTHY property will be set to OFF when attach or restore is completed. Also, modules that are designed to access resources outside the database, and assemblies with either the EXTERNAL_ACCESS and UNSAFE permission setting, have additional requirements to run successfully.  

> [!NOTE]
> By default, the TRUSTWORTHY setting is set to ON for the `msdb` database. If you change this setting from its default value, it might result in unexpected behavior by SQL Server components that use the `msdb` database.

If the TRUSTWORTHY setting is set to ON, and if the owner of the database is a member of a group that has administrative credentials, such as the sysadmin group, the database owner can then be able to create and run unsafe assemblies that can compromise the instance of the SQL Server.

## More information

In an Internet Service Provider (ISP) environment (for example, in a web-hosting service), each customer is permitted to manage their own database and is restricted from accessing system databases and other user databases. For example, the databases of two competing companies could be hosted by the same ISP and exist in the same instance of SQL Server. Dangerous code could be added to a user database when the database is attached to its original instance, and the code would be enabled on the ISP instance when the database is deployed. This situation makes controlling cross-database access crucial.

If the same general entity owns and manages each database, it is still not a good practice to establish a trust relationship with a database unless an application-specific feature, such as a cross-database Service Broker communication, is required. A trust relationship between databases can be established by enabling cross-database ownership chaining or by marking a database as trusted by the instance using the TRUSTWORTHY property. The `is_trustworthy_on` column of the `sys.databases` catalog view indicates if a database has its TRUSTWORTHY property set.

The best practices for database ownership and trust include the following:

- Have distinct owners for databases. Not all databases should be owned by the system administrator.
- Limit the number of owners for each database.
- Confer trust selectively.
- Leave the [cross db ownership chaining](../../database-engine/configure-windows/cross-db-ownership-chaining-server-configuration-option.md) setting set to OFF unless multiple databases are deployed at a single unit.
- Migrate usage to selective trust instead of using the TRUSTWORTHY property.

The following code sample can be used to obtain a list of databases that have the TRUSTWORTHY property set to ON and whose database owner belongs to the **sysadmin** server role.

```sql
SELECT SUSER_SNAME(owner_sid) AS DBOWNER, d.name AS DATABASENAME 
FROM sys.server_principals r 
INNER JOIN sys.server_role_members m ON r.principal_id = m.role_principal_id 
INNER JOIN sys.server_principals p ON p.principal_id = m.member_principal_id 
INNER JOIN sys.databases d ON suser_sname(d.owner_sid) = p.name 
WHERE is_trustworthy_on = 1 AND d.name NOT IN ('msdb') AND r.type = 'R' AND r.name = N'sysadmin' 
```

You can run the following query to determine the TRUSTWORTHY property of the `msdb` database:

```sql
SELECT name, trustworthy_setting = 
CASE is_trustworthy_on
WHEN 1 THEN 'Trustworthy setting is ON for msdb' 
ELSE 'Trustworthy setting is OFF for msdb' 
END 
FROM sys.databases WHERE database_id = 4
```

If this query shows that the TRUSTWORTHY property is set to OFF, you can run the following query to set the TRUSTWORTHY property to ON.

```sql
ALTER DATABASE msdb SET TRUSTWORTHY ON;
GO 
```
  
## Next steps

 - [Security Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)  
 - [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
 - [Extending Database Impersonation by Using EXECUTE AS](/previous-versions/sql/sql-server-2008-r2/ms188304(v=sql.105))
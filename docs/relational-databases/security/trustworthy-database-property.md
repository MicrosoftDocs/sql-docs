---
title: TRUSTWORTHY Database Property
description: Learn about the TRUSTWORTHY database property, which indicates whether the instance of SQL Server trusts the database and its contents. The default is OFF.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/12/2026
ms.service: sql
ms.subservice: security
ms.topic: concept-article
helpviewer_keywords:
  - "TRUSTWORTHY database property"
---
# TRUSTWORTHY database property

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The `TRUSTWORTHY` database property indicates whether the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] trusts the database and the contents within it. By default, this setting is `OFF`, but you can set it to `ON` by using the `ALTER DATABASE` statement.

For example:

```sql
ALTER DATABASE AdventureWorks2025 SET TRUSTWORTHY ON;
```

> [!NOTE]  
> To set this option, you must have `CONTROL SERVER` permission, or be a member of the **sysadmin** fixed server role.

To mitigate certain threats, leave the `TRUSTWORTHY` database property set to `OFF`. These threats can exist as a result of attaching a database that contains one of the following objects:

- Malicious assemblies with the `EXTERNAL_ACCESS` or `UNSAFE` permission setting. For more information, see [CLR integration security](../clr-integration/security/clr-integration-security.md).

- Malicious modules defined to run as highly privileged users. For more information, see [EXECUTE AS clause](../../t-sql/statements/execute-as-clause-transact-sql.md).

Both situations require a specific degree of privilege and are protected by appropriate mechanisms when they're used in the context of a database that's already attached to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. However, if the database is taken offline, if you have access to the database file you can potentially attach it to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] of your choice and add malicious content to the database. When databases are detached and attached in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], certain permissions are set on the data and log files that restrict access to the database files.

Because a database that's attached to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't be immediately trusted, the database isn't allowed to access resources beyond the scope of the database until the database is explicitly marked trustworthy. Therefore, if you back up or detach a database that has the `TRUSTWORTHY` option `ON` and you attach or restore the database to the same or another SQL Server instance, the `TRUSTWORTHY` property is set to `OFF` when attach or restore is completed. Also, modules that are designed to access resources outside the database, and assemblies with either the `EXTERNAL_ACCESS` or `UNSAFE` permission setting, have extra requirements to run successfully.

> [!NOTE]  
> By default, the `TRUSTWORTHY` setting is set to `ON` for the `msdb` database. If you change this setting from its default value, it might result in unexpected behavior by SQL Server components that use the `msdb` database.

If you set the `TRUSTWORTHY` setting to `ON`, and if the owner of the database is a member of a group that has administrative credentials, such as the **sysadmin** group, the database owner can then create and run unsafe assemblies that can compromise the SQL Server instance.

<a id="more-information"></a>

## Remarks

In an Internet service provider (ISP) environment, such as a web-hosting service, each customer manages their own database and can't access system databases or other user databases. For example, an ISP can host the databases of two competing companies in the same instance of SQL Server. If you attach a user database to its original instance, you might add dangerous code to the user database. When you deploy the database to the ISP instance, the code becomes active. This scenario makes controlling cross-database access crucial.

If the same general entity owns and manages each database, it's still not a good practice to establish a trust relationship with a database unless an application-specific feature, such as cross-database Service Broker communication, requires it. You establish a trust relationship between databases by enabling cross-database ownership chaining, or by marking a database as trusted by the instance using the `TRUSTWORTHY` property. The `is_trustworthy_on` column of the `sys.databases` catalog view indicates whether a database has its `TRUSTWORTHY` property set.

The best practices for database ownership and trust include the following:

- Have distinct owners for databases. Not all databases should be owned by the system administrator.

- Limit the number of owners for each database.

- Confer trust selectively.

- Keep the [cross db ownership chaining](../../database-engine/configure-windows/cross-db-ownership-chaining-server-configuration-option.md) setting set to `OFF` unless you deploy multiple databases as a single unit.

- Migrate usage to selective trust instead of using the `TRUSTWORTHY` property.

The following Transact-SQL example returns a list of databases that have the `TRUSTWORTHY` property set to `ON` and whose database owner belongs to the **sysadmin** fixed server role.

```sql
SELECT SUSER_SNAME(owner_sid) AS DBOWNER,
       d.name AS DATABASENAME
FROM sys.server_principals AS r
     INNER JOIN sys.server_role_members AS m
         ON r.principal_id = m.role_principal_id
     INNER JOIN sys.server_principals AS p
         ON p.principal_id = m.member_principal_id
     INNER JOIN sys.databases AS d
         ON suser_sname(d.owner_sid) = p.name
WHERE is_trustworthy_on = 1
      AND d.name NOT IN ('msdb')
      AND r.type = 'R'
      AND r.name = N'sysadmin';
GO
```

The following example determines the `TRUSTWORTHY` property of the `msdb` database.

```sql
SELECT name,
       CASE is_trustworthy_on
           WHEN 1 THEN 'Trustworthy setting is ON for msdb'
           ELSE 'Trustworthy setting is OFF for msdb'
       END AS trustworthy_setting
FROM sys.databases
WHERE database_id = 4;
GO
```

If this query shows that the `TRUSTWORTHY` property is set to `OFF` for `msdb`, you can run the following query to restore it to its default value of `ON`.

```sql
ALTER DATABASE msdb SET TRUSTWORTHY ON;
GO
```

## Security warning

When the `TRUSTWORTHY` property is `ON` *and* the database owner is a member of the **sysadmin** fixed server role, a member of the **db_owner** fixed database role can elevate their permissions to **sysadmin**. Both conditions are required. Use caution with the `TRUSTWORTHY` property.

The following Transact-SQL code returns a list of database users in a database that are granted the **db_owner** fixed database role.

```sql
SELECT roles.principal_id AS RolePrincipalID,
       roles.name AS RolePrincipalName,
       database_role_members.member_principal_id AS MemberPrincipalID,
       members.name AS MemberPrincipalName
FROM sys.database_role_members AS database_role_members
     INNER JOIN sys.database_principals AS roles
         ON database_role_members.role_principal_id = roles.principal_id
     INNER JOIN sys.database_principals AS members
         ON database_role_members.member_principal_id = members.principal_id
WHERE roles.name = 'db_owner'
      AND members.name <> 'dbo';
GO
```

## Related content

- [Security for SQL Server Database Engine and Azure SQL Database](security-center-for-sql-server-database-engine-and-azure-sql-database.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Extending Database Impersonation by Using EXECUTE AS](/previous-versions/sql/sql-server-2008-r2/ms188304(v=sql.105))

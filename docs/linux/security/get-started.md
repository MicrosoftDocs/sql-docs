---
title: Walkthrough for the Security Features of SQL Server on Linux
description: Walk through the security features of SQL Server on Linux to get an idea of areas to investigate further.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/11/2026
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - intro-get-started
  - linux-related-content
---
# Walkthrough for the security features of SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

If you're a Linux user who is new to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the following tasks walk you through some of the security tasks. These tasks aren't unique or specific to Linux, but they give you an idea of areas to investigate further. Each example links to the in-depth documentation for that area.

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## Create a login and a database user

Grant others access to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] by creating a login in the `master` database with the [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md) statement. For example:

```sql
CREATE LOGIN Larry
    WITH PASSWORD = '<password>';
```

> [!CAUTION]  
> [!INCLUDE [password-complexity](../includes/password-complexity.md)]

Logins can connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and have access (with limited permissions) to the `master` database. To connect to a user-database, a login needs a corresponding identity at the database level, called a database user. Users are specific to each database, so you must create them separately in each database to grant access.

The following example switches to the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database, and then uses the [CREATE USER](../../t-sql/statements/create-user-transact-sql.md) statement to create a user named `Larry` that maps to the login named `Larry`. Though the login and the user are related (mapped to each other), they're different objects. The login is a server-level principal. The user is a database-level principal.

```sql
USE AdventureWorks2025;
GO

CREATE USER Larry;
GO
```

- A [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] administrator account can connect to any database and can create more logins and users in any database.
- When you create a database, you become the database owner and can connect to that database. Database owners can create more users.

Later you can authorize other logins to create more logins by granting them the `ALTER ANY LOGIN` permission. Inside a database, you can authorize other users to create more users by granting them the `ALTER ANY USER` permission. For example:

```sql
GRANT ALTER ANY LOGIN TO Larry;
GO

USE AdventureWorks2025;
GO

GRANT ALTER ANY USER TO Jerry;
GO
```

Now the login `Larry` can create more logins, and the user `Jerry` can create more users.

## Grant access with least privileges

Administrators and database owners are usually the first users to connect to a user database. These accounts have all permissions on the database. Don't use these accounts for tasks that need fewer permissions.

When you're just getting started, you can assign some general categories of permissions with the built-in *fixed database roles*. For example, the **db_datareader** fixed database role can read all tables in the database, but can't make changes. Grant membership in a fixed database role with the [ALTER ROLE](../../t-sql/statements/alter-role-transact-sql.md) statement. The following example adds the user `Jerry` to the **db_datareader** fixed database role.

```sql
USE AdventureWorks2025;
GO

ALTER ROLE db_datareader ADD MEMBER Jerry;
```

For a list of the fixed database roles, see [Database-level roles](../../relational-databases/security/authentication-access/database-level-roles.md).

Later, when you're ready to configure more precise access to your data (highly recommended), create your own user-defined database roles with the [CREATE ROLE](../../t-sql/statements/create-role-transact-sql.md) statement. Then assign specific granular permissions to your custom roles.

For example, the following statements create a database role named `Sales`, grant the `Sales` group the ability to read, update, and delete rows from the `Orders` table, and then add the user `Jerry` to the `Sales` role.

```sql
CREATE ROLE Sales;

GRANT SELECT ON OBJECT::Orders TO Sales;
GRANT UPDATE ON OBJECT::Orders TO Sales;
GRANT DELETE ON OBJECT::Orders TO Sales;

ALTER ROLE Sales ADD MEMBER Jerry;
```

For more information about the permission system, see [Get started with Database Engine permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md).

## Configure row-level security

[Row-level security](../../relational-databases/security/row-level-security.md) enables you to restrict access to rows in a database based on the user who runs a query. This feature is useful for scenarios like ensuring that customers can only access their own data or that workers can only access data for their department.

The following steps walk through setting up two users with different row-level access to the `Sales.SalesOrderHeader` table.

Create two user accounts to test row-level security:

```sql
USE AdventureWorks2025;
GO

CREATE USER Manager WITHOUT LOGIN;
CREATE USER SalesPerson280 WITHOUT LOGIN;
```

Grant read access on the `Sales.SalesOrderHeader` table to both users:

```sql
GRANT SELECT ON Sales.SalesOrderHeader TO Manager;
GRANT SELECT ON Sales.SalesOrderHeader TO SalesPerson280;
```

Create a new schema and inline table-valued function. The function returns `1` when a row in the `SalesPersonID` column matches the ID of a `SalesPerson` login, or when the user who runs the query is the `Manager` user.

```sql
CREATE SCHEMA Security;
GO

CREATE FUNCTION Security.fn_securitypredicate
(@SalesPersonID INT)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN
    SELECT 1 AS fn_securitypredicate_result
    WHERE ('SalesPerson' + CAST (@SalesPersonId AS VARCHAR (16)) = USER_NAME())
          OR (USER_NAME() = 'Manager')
```

Create a security policy adding the function as both a filter and a block predicate on the table:

```sql
CREATE SECURITY POLICY SalesFilter
    ADD FILTER PREDICATE Security.fn_securitypredicate(SalesPersonID) ON Sales.SalesOrderHeader,
    ADD BLOCK PREDICATE Security.fn_securitypredicate(SalesPersonID) ON Sales.SalesOrderHeader
    WITH (STATE = ON);
```

Run the following statements to query the `SalesOrderHeader` table as each user. Verify that `SalesPerson280` only sees the 95 rows from their own sales and that the `Manager` can see all the rows in the table.

```sql
EXECUTE AS USER = 'SalesPerson280';

SELECT *
FROM Sales.SalesOrderHeader;

REVERT;

EXECUTE AS USER = 'Manager';

SELECT *
FROM Sales.SalesOrderHeader;

REVERT;
```

Alter the security policy to disable it. Now both users can access all rows.

```sql
ALTER SECURITY POLICY SalesFilter
    WITH (STATE = OFF);
```

## Enable dynamic data masking

[Dynamic data masking](../../relational-databases/security/dynamic-data-masking.md) enables you to limit the exposure of sensitive data to users of an application by fully or partially masking certain columns.

Use an `ALTER TABLE` statement to add a masking function to the `EmailAddress` column in the `Person.EmailAddress` table:

```sql
USE AdventureWorks2025;
GO

ALTER TABLE Person.EmailAddress
    ALTER COLUMN EmailAddress
        ADD MASKED WITH (FUNCTION = 'email()');
```

Create a new user `TestUser` with `SELECT` permission on the table, and then execute a query as `TestUser` to view the masked data:

```sql
CREATE USER TestUser WITHOUT LOGIN;

GRANT SELECT
    ON Person.EmailAddress TO TestUser;

EXECUTE AS USER = 'TestUser';

SELECT EmailAddressID,
       EmailAddress
FROM Person.EmailAddress;

REVERT;
```

Verify that the masking function changes the email address in the first record from:

| EmailAddressID | EmailAddress |
| --- | --- |
| 1 | `ken0@adventure-works.com` |

into

| EmailAddressID | EmailAddress |
| --- | --- |
| 1 | `kXXX@XXXX.com` |

## Enable transparent data encryption

An attacker can steal database files from your hard drive. This can occur if an attacker gets elevated access to the system, if an employee takes the files, or if someone steals the computer that holds the files.

Transparent data encryption (TDE) encrypts the data files as they are stored on the hard drive. The `master` database of the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] has the encryption key, so that the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] can manipulate the data. The database files can't be read without access to the key. High-level administrators can manage, back up, and recreate the key, so only selected people can move the database. When you enable TDE, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] also automatically encrypts the `tempdb` database.

Because the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] can read the data, TDE doesn't protect against unauthorized access by computer administrators who can directly read memory or access [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] through an administrator account.

### Configure TDE

- Create a master key
- Create or obtain a certificate protected by the master key
- Create a database encryption key and protect it with the certificate
- Set the database to use encryption

Configuring TDE requires `CONTROL` permission on the `master` database and `CONTROL` permission on the user database. Typically an administrator configures TDE.

The following example illustrates encrypting and decrypting the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database with a certificate named `MyServerCert` installed on the server.

```sql
USE master;
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<master-key-password>';
GO

CREATE CERTIFICATE MyServerCert
    WITH SUBJECT = 'My Database Encryption Key Certificate';
GO

USE AdventureWorks2025;
GO

CREATE DATABASE ENCRYPTION KEY WITH ALGORITHM = AES_256
    ENCRYPTION BY SERVER CERTIFICATE MyServerCert;
GO

ALTER DATABASE AdventureWorks2025
    SET ENCRYPTION ON;
```

To remove TDE, run the following command:

```sql
ALTER DATABASE AdventureWorks2025
    SET ENCRYPTION OFF;
```

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] schedules the encryption and decryption operations on background threads. You can view the status of these operations with the catalog views and dynamic management views in the list that appears later in this article.

> [!WARNING]  
> The database encryption key also encrypts backup files of databases that have TDE enabled. As a result, when you restore these backups, the certificate protecting the database encryption key must be available. In addition to backing up the database, you must back up the server certificates to prevent data loss. Data loss results if the certificate is no longer available. For more information, see [SQL Server Certificates and Asymmetric Keys](../../relational-databases/security/sql-server-certificates-and-asymmetric-keys.md).

For more information about TDE, see [Transparent data encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md).

## Configure backup encryption

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] can encrypt data while creating a backup. By specifying the encryption algorithm and the encryptor (a certificate or asymmetric key) when creating a backup, you can create an encrypted backup file.

> [!WARNING]  
> Always back up the certificate or asymmetric key, and preferably to a different location than the backup file it encrypts. Without the certificate or asymmetric key, you can't restore the backup, rendering the backup file unusable.

The following example creates a certificate, and then creates a backup protected by the certificate.

```sql
USE master;
GO

CREATE CERTIFICATE BackupEncryptCert
    WITH SUBJECT = 'Database backups';
GO

BACKUP DATABASE [AdventureWorks2025]
TO DISK = N'/var/opt/mssql/backups/AdventureWorks2025.bak'
WITH COMPRESSION,
    ENCRYPTION (ALGORITHM = AES_256, SERVER CERTIFICATE = BackupEncryptCert),
    STATS = 10;
GO
```

For more information, see [Backup encryption](../../relational-databases/backup-restore/backup-encryption.md).

## Related content

- [Security for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)

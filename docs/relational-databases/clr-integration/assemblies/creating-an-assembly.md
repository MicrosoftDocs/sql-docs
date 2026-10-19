---
title: Create an Assembly
description: Use CREATE ASSEMBLY to register an assembly in SQL Server and specify its security settings. Register an assembly to use its functionality.
author: uc-msft
ms.author: umajay
ms.date: 07/23/2025
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "creating assemblies"
  - "UNSAFE assemblies"
  - "CREATE ASSEMBLY statement"
  - "SAFE assemblies"
  - "EXTERNAL_ACCESS assemblies"
  - "assemblies [CLR integration], creating"
---
# Create an assembly

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Managed database objects, such as stored procedures or triggers, are compiled and then deployed in units called an assembly. Managed DLL assemblies must be registered in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] before the functionality the assembly provides can be used. To register an assembly in a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] database, use the `CREATE ASSEMBLY` statement. This article discusses how to register an assembly in a database using the `CREATE ASSEMBLY` statement, and how to specify the security settings for the assembly.

## Code access security no longer supported

[!INCLUDE [code-access-security](../../../database-engine/includes/code-access-security.md)]

## The CREATE ASSEMBLY statement

The `CREATE ASSEMBLY` statement is used to create an assembly in a database. Here's an example:

```sql
CREATE ASSEMBLY SQLCLRTest
    FROM 'C:\MyDBApp\SQLCLRTest.dll';
```

The `FROM` clause specifies the pathname of the assembly to create. This path can either be a Universal Naming Convention (UNC) path or a physical file path that is local to the machine.

[!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] doesn't allow registering different versions of an assembly with the same name, culture, and public key.

It's possible to create assemblies that reference other assemblies. When an assembly is created in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] also creates the assemblies referenced by the root-level assembly, if the referenced assemblies aren't already created into the database.

Database users or user roles are given permissions to create, and therefore own, assemblies in a database. In order to create assemblies, the database user or role should have the `CREATE ASSEMBLY` permission.

An assembly can only succeed in referencing other assemblies if:

- The assembly that is called or referenced, is owned by the same user or role.
- The assembly that is called or referenced, was created in the same database.

## Specify security when creating assemblies

When creating an assembly into a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] database, you can specify one of three different levels of security in which your code can run: `SAFE`, `EXTERNAL_ACCESS`, or `UNSAFE`. When the `CREATE ASSEMBLY` statement is run, certain checks are performed on the code assembly, which might cause the assembly to fail to register on the server.

`SAFE` is the default permission set and works for most scenarios. To specify a given security level, you modify the syntax of the `CREATE ASSEMBLY` statement as follows:

```sql
CREATE ASSEMBLY SQLCLRTest
    FROM 'C:\MyDBApp\SQLCLRTest.dll'
    WITH PERMISSION_SET = SAFE;
```

It's also possible to create an assembly with the `SAFE` permission set by omitting the third line of previous.

```sql
CREATE ASSEMBLY SQLCLRTest
    FROM 'C:\MyDBApp\SQLCLRTest.dll';
```

When code in an assembly runs under the `SAFE` permission set, it can only do computation and data access within the server through the in-process managed provider.

## Create EXTERNAL_ACCESS and UNSAFE assemblies

`EXTERNAL_ACCESS` addresses scenarios in which the code needs to access resources outside the server, such as files, network, registry, and environment variables. Whenever the server accesses an external resource, it impersonates the security context of the user calling the managed code.

`UNSAFE` code permission is for those situations in which an assembly isn't verifiably safe or requires additional access to restricted resources, such as the Win32 API.

To create an `EXTERNAL_ACCESS` or `UNSAFE` assembly in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], one of the following two conditions must be met:

1. The assembly is strong name signed or Authenticode signed with a certificate. This strong name (or certificate) is created inside [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] as an asymmetric key (or certificate), and has a corresponding login with `EXTERNAL ACCESS ASSEMBLY` permission (for external access assemblies) or `UNSAFE ASSEMBLY` permission (for unsafe assemblies).

1. The database owner (DBO) has `EXTERNAL ACCESS ASSEMBLY` (for `EXTERNAL ACCESS` assemblies) or `UNSAFE ASSEMBLY` (for `UNSAFE` assemblies) permission, and the database has the [TRUSTWORTHY database property](../../security/trustworthy-database-property.md) set to `ON`.

The two conditions listed previously are also checked at assembly load time (which includes execution). At least one of the conditions must be met in order to load the assembly.

We recommend that the [TRUSTWORTHY database property](../../security/trustworthy-database-property.md) on a database isn't set to `ON` only to run common language runtime (CLR) code in the server process. Instead, we recommend that an asymmetric key is created from the assembly file in the `master` database. A login mapped to this asymmetric key must then be created, and the login must be granted `EXTERNAL ACCESS ASSEMBLY` or `UNSAFE ASSEMBLY` permission.

The following [!INCLUDE [tsql](../../../includes/tsql-md.md)] statements perform the steps that are required to create an asymmetric key, map a login to this key, and then grant `EXTERNAL_ACCESS` permission to the login. You must run the following [!INCLUDE [tsql](../../../includes/tsql-md.md)] statements before running the `CREATE ASSEMBLY` statement.

```sql
USE master;
GO

CREATE ASYMMETRIC KEY SQLCLRTestKey
     FROM EXECUTABLE FILE = 'C:\MyDBApp\SQLCLRTest.dll';

CREATE LOGIN SQLCLRTestLogin
    FROM ASYMMETRIC KEY SQLCLRTestKey;

GRANT EXTERNAL ACCESS ASSEMBLY TO SQLCLRTestLogin;
GO
```

> [!NOTE]  
> You must create a new login to associate with the asymmetric key. This login is only used to grant permissions. It doesn't have to be associated with a user, or used within the application.

To create an `EXTERNAL ACCESS` assembly, the creator needs to specify `EXTERNAL ACCESS` permission when creating the assembly:

```sql
CREATE ASSEMBLY SQLCLRTest
    FROM 'C:\MyDBApp\SQLCLRTest.dll'
    WITH PERMISSION_SET = EXTERNAL_ACCESS;
```

The following [!INCLUDE [tsql](../../../includes/tsql-md.md)] statements perform the steps that are required to create an asymmetric key, map a login to this key, and then grant `UNSAFE` permission to the login. You must run the following [!INCLUDE [tsql](../../../includes/tsql-md.md)] statements before running the `CREATE ASSEMBLY` statement.

```sql
USE master;
GO

CREATE ASYMMETRIC KEY SQLCLRTestKey
     FROM EXECUTABLE FILE = 'C:\MyDBApp\SQLCLRTest.dll';

CREATE LOGIN SQLCLRTestLogin
    FROM ASYMMETRIC KEY SQLCLRTestKey;

GRANT UNSAFE ASSEMBLY TO SQLCLRTestLogin;
GO
```

To specify that an assembly loads with `UNSAFE` permission, you specify the `UNSAFE` permission set when loading the assembly into the server:

```sql
CREATE ASSEMBLY SQLCLRTest
    FROM 'C:\MyDBApp\SQLCLRTest.dll'
    WITH PERMISSION_SET = UNSAFE;
```

For more information about the permissions for each of the settings, see [CLR integration security](../security/clr-integration-security.md).

## Related content

- [Manage CLR integration assemblies](managing-clr-integration-assemblies.md)
- [Alter an assembly](altering-an-assembly.md)
- [Drop an assembly](dropping-an-assembly.md)
- [CLR integration code access security](../security/clr-integration-code-access-security.md)
- [TRUSTWORTHY database property](../../security/trustworthy-database-property.md)

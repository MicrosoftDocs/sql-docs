---
title: Alter an Assembly
description: Use ALTER ASSEMBLY to update assemblies registered in SQL Server. You can also change the permission set and add source code or other files for an assembly.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "assemblies [CLR integration], modifying"
  - "permissions [CLR integration]"
  - "altering assemblies"
  - "ALTER ASSEMBLY statement"
---
# Alter an assembly

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Assemblies that are registered in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] can be updated from a more recent version using the `ALTER ASSEMBLY` statement. To update an assembly, use the `ALTER ASSEMBLY` statement with the following syntax:

```sql
ALTER ASSEMBLY SQLCLRTest
    FROM 'C:\MyDBApp\SQLCLRTest.dll';
```

`ALTER ASSEMBLY` doesn't disrupt currently running processes that are using the assembly; the processes continue executing with the unaltered assembly. `ALTER ASSEMBLY` can't be used to change the signatures of common language runtime (CLR) functions, aggregate functions, stored procedures, and triggers. You can add new public methods to the assembly, private methods can be modified in any way, and public methods can be modified as long as signatures or attributes aren't changed. Fields that are contained within a native-serialized user-defined type, including data members or base classes, can't be changed by using `ALTER ASSEMBLY`. All other changes are unsupported. For more information, see [ALTER ASSEMBLY](../../../t-sql/statements/alter-assembly-transact-sql.md).

## Change the permission set of an assembly

The permission set of an assembly can also be changed using the `ALTER ASSEMBLY` statement. The following statement changes the permission set of the `SQLCLRTest` assembly to `EXTERNAL_ACCESS`.

```sql
ALTER ASSEMBLY SQLCLRTest
    WITH PERMISSION_SET = EXTERNAL_ACCESS;
```

If the permission set of an assembly is being changed from `SAFE` to `EXTERNAL_ACCESS` or `UNSAFE`, an asymmetric key and corresponding login with `EXTERNAL ACCESS ASSEMBLY` permission or `UNSAFE ASSEMBLY` permission for the assembly must first be created. For more information, see [Create an assembly](creating-an-assembly.md).

## Add the source code of an assembly

The `ADD FILE` clause in the `ALTER ASSEMBLY` syntax isn't present in `CREATE ASSEMBLY`. You can use it to add source code or any other files associated with an assembly. The files are copied from their original locations and stored in system tables in the database. This way, you always have source code or other files on hand should you ever need to recreate or document the current version of the user-defined type (UDT).

The following statement adds the `Point.cs` class source code for the `Point` UDT. It copies the text contained in the `Point.cs` file and stores it in the database under the name `PointSource`.

```sql
ALTER ASSEMBLY Point
ADD FILE FROM 'C:\Projects\Point\Point.cs' AS PointSource;
```

## Related content

- [Manage CLR integration assemblies](managing-clr-integration-assemblies.md)
- [Create an assembly](creating-an-assembly.md)
- [Drop an assembly](dropping-an-assembly.md)
- [ALTER ASSEMBLY (Transact-SQL)](../../../t-sql/statements/alter-assembly-transact-sql.md)

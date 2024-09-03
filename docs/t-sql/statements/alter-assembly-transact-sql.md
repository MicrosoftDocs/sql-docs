---
title: "ALTER ASSEMBLY (Transact-SQL)"
description: ALTER ASSEMBLY alters an assembly by modifying the SQL Server catalog properties of an assembly.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_ASSEMBLY_TSQL"
  - "ALTER ASSEMBLY"
helpviewer_keywords:
  - "assemblies [CLR integration], modifying"
  - "refreshing assemblies"
  - "assemblies [CLR integration], versioning"
  - "assemblies [CLR integration], adding files"
  - "modifying assemblies"
  - "adding files"
  - "ALTER ASSEMBLY statement"
dev_langs:
  - "TSQL"
---
# ALTER ASSEMBLY (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Alters an assembly by modifying the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] catalog properties of an assembly. `ALTER ASSEMBLY` refreshes it to the latest copy of the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] modules that hold its implementation and adds or removes files associated with it. Assemblies are created by using [CREATE ASSEMBLY](create-assembly-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ALTER ASSEMBLY assembly_name
    [ FROM <client_assembly_specifier> | <assembly_bits> ]
    [ WITH <assembly_option> [ , ...n ] ]
    [ DROP FILE { file_name [ , ...n ] | ALL } ]
    [ ADD FILE FROM
    {
        client_file_specifier [ AS file_name ]
      | file_bits AS file_name
    } [ , ...n ]
    ] [ ; ]
<client_assembly_specifier> ::=
    '\\computer_name\share-name\ [ path\ ] manifest_file_name '
  | '[ local_path\ ] manifest_file_name'

<assembly_bits> ::=
    { varbinary_literal | varbinary_expression }

<assembly_option> ::=
    PERMISSION_SET = { SAFE | EXTERNAL_ACCESS | UNSAFE }
  | VISIBILITY = { ON | OFF }
  | UNCHECKED DATA
```

## Arguments

#### *assembly_name*

The name of the assembly you want to modify. *assembly_name* must already exist in the database.

#### FROM \<client_assembly_specifier> | \<assembly_bits>

Updates an assembly to the latest copy of the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] modules that hold its implementation. This option can only be used if there are no associated files with the specified assembly.

`<client_assembly_specifier>` specifies the network or local location where the assembly being refreshed is located. The network location includes the computer name, the share name, and a path within that share. *manifest_file_name* specifies the name of the file that contains the manifest of the assembly.

> [!IMPORTANT]  
> Azure SQL Database doesn't support referencing a file.

`<assembly_bits>` is the binary value for the assembly.

Separate `ALTER ASSEMBLY` statements must be issued for any dependent assemblies that also require updating.

#### PERMISSION_SET = { SAFE | EXTERNAL_ACCESS | UNSAFE }

Specifies the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] code access permission set property of the assembly. For more information about this property, see [CREATE ASSEMBLY](create-assembly-transact-sql.md).

The `PERMISSION_SET` option is affected by the [clr strict security](../../database-engine/configure-windows/clr-strict-security.md) option. When `clr strict security` is enabled, all assemblies are treated as `UNSAFE`.

The `EXTERNAL_ACCESS` and `UNSAFE` options aren't available in a contained database.

#### VISIBILITY = { ON | OFF }

Indicates whether the assembly is visible for creating common language runtime (CLR) functions, stored procedures, triggers, user-defined types, and user-defined aggregate functions against it. If set to `OFF`, the assembly is intended to be called only by other assemblies. If there are existing CLR database objects already created against the assembly, the visibility of the assembly can't be changed. Any assemblies referenced by *assembly_name* are uploaded as not visible by default.

#### UNCHECKED DATA

By default, `ALTER ASSEMBLY` fails if it must verify the consistency of individual table rows. This option allows postponing the checks until a later time by using `DBCC CHECKTABLE`. If specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] executes the `ALTER ASSEMBLY` statement even if there are tables in the database that contain the following conditions:

- Persisted computed columns that either directly or indirectly reference methods in the assembly, through [!INCLUDE [tsql](../../includes/tsql-md.md)] functions or methods.

- `CHECK` constraints that directly or indirectly reference methods in the assembly.

- Columns of a CLR user-defined type that depend on the assembly, and the type implements a `UserDefined` (non-`Native`) serialization format.

- Columns of a CLR user-defined type that reference views created by using `WITH SCHEMABINDING`.

If any `CHECK` constraints are present, they're disabled and marked untrusted. Any tables containing columns depending on the assembly are marked as containing unchecked data until those tables are explicitly checked.

Only members of the **db_owner** and **db_ddlowner** fixed database roles can specify this option.

Requires the `ALTER ANY SCHEMA` permission to specify this option.

For more information, see [Implementing assemblies](../../relational-databases/clr-integration/assemblies-implementing.md).

#### DROP FILE { *file_name* [ ,*...n* ] | ALL }

Removes the file name associated with the assembly, or all files associated with the assembly, from the database. If used with `ADD FILE` that follows, `DROP FILE` executes first. This lets you replace a file with the same file name.

> [!NOTE]  
> This option isn't available in a contained database or Azure SQL Database.

#### ADD FILE FROM { *client_file_specifier* [ AS *file_name* ] | *file_bits* AS *file_name* }

Uploads a file to be associated with the assembly, such as source code, debug files or other related information, into the server and made visible in the `sys.assembly_files` catalog view. *client_file_specifier* specifies the location from which to upload the file. *file_bits* can be used instead to specify the list of binary values that make up the file. *file_name* specifies the name under which the file should be stored in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. *file_name* must be specified if *file_bits* is specified, and is optional if *client_file_specifier* is specified. If *file_name* isn't specified, the file_name part of *client_file_specifier* is used as *file_name*.

> [!NOTE]  
> This option isn't available in a contained database or Azure SQL Database.

## Code access security no longer supported

[!INCLUDE [code-access-security](../../database-engine/includes/code-access-security.md)]

## Remarks

`ALTER ASSEMBLY` doesn't disrupt currently running sessions that are running code in the assembly being modified. Current sessions complete execution by using the unaltered bits of the assembly.

If the `FROM` clause is specified, `ALTER ASSEMBLY` updates the assembly with respect to the latest copies of the modules provided. Because there might be CLR functions, stored procedures, triggers, data types, and user-defined aggregate functions in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are already defined against the assembly, the `ALTER ASSEMBLY` statement rebinds them to the latest implementation of the assembly. To accomplish this rebinding, the methods that map to CLR functions, stored procedures, and triggers must still exist in the modified assembly with the same signatures. The classes that implement CLR user-defined types and user-defined aggregate functions must still satisfy the requirements for being a user-defined type or aggregate.

> [!CAUTION]  
> If `WITH UNCHECKED DATA` isn't specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tries to prevent `ALTER ASSEMBLY` from executing if the new assembly version affects existing data in tables, indexes, or other persistent sites. However, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't guarantee that computed columns, indexes, indexed views or expressions will be consistent with the underlying routines and types when the CLR assembly is updated. Use caution when you execute `ALTER ASSEMBLY` to make sure that there's not a mismatch between the result of an expression and a value based on that expression stored in the assembly.

`ALTER ASSEMBLY` changes the assembly version. The culture and public key token of the assembly remain the same.

The `ALTER ASSEMBLY` statement can't be used to change the following items:

- The signatures of CLR functions, aggregate functions, stored procedures, and triggers in an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that reference the assembly. ALTER ASSEMBLY fails when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't rebind [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] database objects in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with the new version of the assembly.

- The signatures of methods in the assembly that are called from other assemblies.

- The list of assemblies that depend on the assembly, as referenced in the `DependentList` property of the assembly.

- The indexability of a method, unless there are no indexes or persisted computed columns depending on that method, either directly or indirectly.

- The `FillRow` method name attribute for CLR table-valued functions.

- The `Accumulate` and `Terminate` method signature for user-defined aggregates.

- System assemblies.

- Assembly ownership. Use [ALTER AUTHORIZATION](alter-authorization-transact-sql.md) instead.

Additionally, for assemblies that implement user-defined types, `ALTER ASSEMBLY` can be used for making only the following changes:

- Modifying public methods of the user-defined type class, as long as signatures or attributes aren't changed.

- Adding new public methods.

- Modifying private methods in any way.

Fields contained within a native-serialized user-defined type, including data members or base classes, can't be changed by using `ALTER ASSEMBLY`. All other changes are unsupported.

If `ADD FILE FROM` isn't specified, `ALTER ASSEMBLY` drops any files associated with the assembly.

If `ALTER ASSEMBLY` is executed without the `UNCHECKED` data clause, checks are performed to verify that the new assembly version doesn't affect existing data in tables. Depending on the amount of data that needs to be checked, this step might affect performance.

## Permissions

Requires `ALTER` permission on the assembly. Additional requirements are as follows:

- To alter an assembly whose existing permission set is `EXTERNAL_ACCESS`, requires `EXTERNAL ACCESS ASSEMBLY` permission on the server.

- To alter an assembly whose existing permission set is `UNSAFE` requires `UNSAFE ASSEMBLY` permission on the server.

- To change the permission set of an assembly to `EXTERNAL_ACCESS`, requires `EXTERNAL ACCESS ASSEMBLY` permission on the server.

- To change the permission set of an assembly to `UNSAFE`, requires `UNSAFE ASSEMBLY` permission on the server.

- Specifying `WITH UNCHECKED DATA` requires `ALTER ANY SCHEMA` permission.

### Permissions with CLR strict security

The following permissions required to alter a CLR assembly when `clr strict security` is enabled:

- The user must have the `ALTER ASSEMBLY` permission
- And one of the following conditions must also be true:

  - The assembly is signed with a certificate or asymmetric key that has a corresponding login with the `UNSAFE ASSEMBLY` permission on the server. Signing the assembly is recommended.

  - The database has the `TRUSTWORTHY` property set to `ON`, and the database is owned by a login that has the `UNSAFE ASSEMBLY` permission on the server. This option isn't recommended.

For more information about assembly permission sets, see [Designing assemblies](../../relational-databases/clr-integration/assemblies-designing.md).

## Examples

### A. Refresh an assembly

The following example updates assembly `ComplexNumber` to the latest copy of the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] modules that hold its implementation.

> [!NOTE]  
> Assembly `ComplexNumber` can be created by running the `UserDefinedDataType` sample scripts. For more information, see [User Defined Type](/previous-versions/sql/sql-server-2016/ms131078(v=sql.130)).

```sql
 ALTER ASSEMBLY ComplexNumber
 FROM 'C:\Program Files\Microsoft SQL Server\130\Tools\Samples\1033\Engine\Programmability\CLR\UserDefinedDataType\CS\ComplexNumber\obj\Debug\ComplexNumber.dll'
  ```

> [!IMPORTANT]  
> Azure SQL Database doesn't support referencing a file.

### B. Add a file to associate with an assembly

The following example uploads the source code file `Class1.cs` to be associated with assembly `MyClass`. This example assumes assembly `MyClass` is already created in the database.

```sql
ALTER ASSEMBLY MyClass
ADD FILE FROM 'C:\MyClassProject\Class1.cs';
```

> [!IMPORTANT]  
> Azure SQL Database doesn't support referencing a file.

### C. Change the permissions of an assembly

The following example changes the permission set of assembly `ComplexNumber` from SAFE to `EXTERNAL ACCESS`.

```sql
ALTER ASSEMBLY ComplexNumber WITH PERMISSION_SET = EXTERNAL_ACCESS;
```

## Related content

- [CREATE ASSEMBLY (Transact-SQL)](create-assembly-transact-sql.md)
- [DROP ASSEMBLY (Transact-SQL)](drop-assembly-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)

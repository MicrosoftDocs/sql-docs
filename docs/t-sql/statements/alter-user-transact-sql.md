---
title: "ALTER USER (Transact-SQL)"
description: ALTER USER renames a database user or changes its default schema.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf
ms.date: 01/08/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ALTER_USER_TSQL"
  - "ALTER USER"
helpviewer_keywords:
  - "modifying database users"
  - "ALTER USER statement"
  - "renaming databases"
  - "schemas [SQL Server], default"
  - "database user names [SQL Server]"
  - "names [SQL Server], database users"
  - "default schemas"
  - "modifying default schemas"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# ALTER USER (Transact-SQL)

Renames a database user or changes its default schema.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

[!INCLUDE [entra-id](../../includes/entra-id.md)]

[!INCLUDE [select-product](../includes/select-product.md)]

::: moniker range=">=sql-server-2017 || >=sql-server-linux-2017"

:::row:::
    :::column:::
        **_\* SQL Server \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Database](alter-user-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Microsoft Fabric Data Warehouse](alter-user-transact-sql.md?view=fabric&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL database in Microsoft Fabric](alter-user-transact-sql.md?view=fabric-sqldb&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-user-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-user-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-user-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server

## Syntax

```syntaxsql
-- Syntax for SQL Server

ALTER USER user_name
 WITH <set_item> [ ,...n ]
[;]

<set_item> ::=
NAME = new_user_name
| DEFAULT_SCHEMA = { schema_name | NULL }
| LOGIN = login_name
| PASSWORD = 'password' [ OLD_PASSWORD = 'oldpassword' ]
| DEFAULT_LANGUAGE = { NONE | <lcid> | <language name> | <language alias> }
| ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]
```

## Arguments

#### *user_name*

 Specifies the name by which the user is identified inside this database.

#### LOGIN = _login_name_

 Remaps a user to another login by changing the user's Security Identifier (SID) to match the login's SID.

#### NAME = _new_user_name_

 Specifies the new name for this user. *new_user_name* must not already exist in the current database.

#### DEFAULT_SCHEMA = { *schema_name* | NULL }

 Specifies the first schema that will be searched by the server when it resolves the names of objects for this user. Setting the default schema to NULL removes a default schema from a Windows group. The NULL option cannot be used with a Windows user.

#### PASSWORD = '*password*'

 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions, [!INCLUDE[sssds](../../includes/ssazure-sqldb.md)].

 Specifies the password for the user that is being changed. Passwords are case-sensitive.

> [!NOTE]
> This option is available only for contained users. For more information, see [Contained Databases](../../relational-databases/databases/contained-databases.md) and [sp_migrate_user_to_contained (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md).

#### OLD_PASSWORD =_'oldpassword'_

 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions, [!INCLUDE[sssds](../../includes/ssazure-sqldb.md)].

 The current user password that will be replaced by '*password*'. Passwords are case-sensitive. *OLD_PASSWORD* is required to change a password, unless you have **ALTER ANY USER** permission. Requiring *OLD_PASSWORD* prevents users with **IMPERSONATION** permission from changing the password.

> [!NOTE]
> This option is available only for contained users.

#### DEFAULT_LANGUAGE =_{ NONE \| \<lcid> \| \<language name> \| \<language alias> }_

 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.

 Specifies a default language to be assigned to the user. If this option is set to NONE, the default language is set to the current default language of the database. If the default language of the database is later changed, the default language of the user remains unchanged. *DEFAULT_LANGUAGE* can be the local ID (lcid), the name of the language, or the language alias.

> [!NOTE]
> This option can only be specified in a contained database and only for contained users.

#### ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]

 **Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later versions, [!INCLUDE[sssds](../../includes/ssazure-sqldb.md)].

 Suppresses cryptographic metadata checks on the server in bulk copy operations. This enables the user to bulk copy encrypted data between tables or databases, without decrypting the data. The default is OFF.

> [!WARNING]
> Improper use of this option can lead to data corruption. For more information, see [Bulk load encrypted data to columns using Always Encrypted](../../relational-databases/security/encryption/migrate-sensitive-data-protected-by-always-encrypted.md).

## Remarks

 The default schema will be the first schema that will be searched by the server when it resolves the names of objects for this database user. Unless otherwise specified, the default schema will be the owner of objects created by this database user.

 If the user has a default schema, that default schema will be used. If the user doesn't have a default schema, but the user is a member of a group that has a default schema, the default schema of the group will be used. If the user doesn't have a default schema, and is a member of more than one group, the default schema for the user will be that of the Windows group with the lowest principal_id and an explicitly set default schema. If no default schema can be determined for a user, the `dbo` schema is used.

 DEFAULT_SCHEMA can be set to a schema that doesn't currently occur in the database. Therefore, you can assign a DEFAULT_SCHEMA to a user before that schema is created.

 DEFAULT_SCHEMA can't be specified for a user who is mapped to a certificate, or an asymmetric key.

> [!IMPORTANT]
> The value of DEFAULT_SCHEMA is ignored if the user is a member of the **sysadmin** fixed server role. All members of the **sysadmin** fixed server role have a default schema of `dbo`.

 You can change the name of a user who is mapped to a Windows login or group only when the SID of the new user name matches the SID that is recorded in the database. This check helps prevent spoofing of Windows logins in the database.

 The `WITH LOGIN` clause enables the remapping of a user to a different login. Users without a login, users mapped to a certificate, or users mapped to an asymmetric key can't be remapped with this clause. Only SQL users and Windows users (or groups) can be remapped. The `WITH LOGIN` clause can't be used to change the type of user, such as changing a Windows account to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.
 
 A mismatched SID can occur when you have restored a database from another server and have a database user mapped to a SQL Server login. You can use the `WITH LOGIN` clause to correct this situation by replacing the user SID in the database with the login SID from the server.

 The name of the user will be automatically renamed to the login name if the following conditions are true.

- The user is a Windows user.

- The name is a Windows name (contains a backslash).

- No new name was specified.

- The current name differs from the login name.

 Otherwise, the user won't be renamed unless the caller additionally invokes the `NAME` clause.

The name of a user mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, a certificate, or an asymmetric key can't contain the backslash character (`\`).

> [!NOTE]  
> [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]

[!INCLUDE [encryption-algorithm-history-md](../../includes/encryption-algorithm-history.md)]

## Security

> [!NOTE]
> A user who has **ALTER ANY USER** permission can change the default schema of any user. A user who has an altered schema might unknowingly select data from the wrong table or execute code from the wrong schema.

### Permissions

 To change the name of a user requires the **ALTER ANY USER** permission.

 To change the target login of a user requires the **CONTROL** permission on the database.

 To change the user name of a user having **CONTROL** permission on the database requires the **CONTROL** permission on the database.

 To change the default schema or language requires **ALTER** permission on the user. Users can change their own default schema or language.

## Examples

All examples are executed in a user database.

<a id="a-changing-the-name-of-a-database-user"></a>

### A. Change the name of a database user

 The following example changes the name of the database user `Mary5` to `Mary51`.

```sql
ALTER USER Mary5 WITH NAME = Mary51;
GO
```

<a id="b-changing-the-default-schema-of-a-user"></a>

### B. Change the default schema of a user

 The following example changes the default schema of the user `Mary51` to `Purchasing`.

```sql
ALTER USER Mary51 WITH DEFAULT_SCHEMA = Purchasing;
GO
```

<a id="c-changing-several-options-at-once"></a>

### C. Change several options at once

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.

 The following example changes several options for a contained database user in one statement. Replace `<new strong password here>` and `<old strong password here>` with your own values.

```sql
ALTER USER Philip
WITH NAME = Philipe
, DEFAULT_SCHEMA = Development
, PASSWORD = '<new strong password here>' OLD_PASSWORD = '<old strong password here>'
, DEFAULT_LANGUAGE= French ;
GO
```

### D. Correct a mismatched SID

 The following example corrects the user SID in the database to match the SID on the server for a SQL Server authenticated login.
 
```sql
ALTER USER Mai
WITH LOGIN = Mai;
GO
```

## Related content

- [CREATE USER (Transact-SQL)](create-user-transact-sql.md)
- [DROP USER (Transact-SQL)](drop-user-transact-sql.md)
- [Contained Databases](../../relational-databases/databases/contained-databases.md)
- [sp_migrate_user_to_contained (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md)

::: moniker-end
::: moniker range="=azuresqldb-current||=fabric||=fabric-sqldb"

:::row:::
    :::column:::
        [SQL Server](alter-user-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure SQL Database and Microsoft Fabric \*_**
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-user-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-user-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-user-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure SQL Database, SQL database in Microsoft Fabric, and Fabric Data Warehouse

## Syntax

**Syntax for Azure SQL Database**

```syntaxsql
-- Syntax for Azure SQL Database

ALTER USER user_name
 WITH <set_item> [ ,...n ]

<set_item> ::=
NAME = new_user_name
| DEFAULT_SCHEMA = schema_name
| LOGIN = login_name
| ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]
[;]

-- Azure SQL Database Update Syntax
ALTER USER user_name
 WITH <set_item> [ ,...n ]
[;]

<set_item> ::=
NAME = new_user_name
| DEFAULT_SCHEMA = { schema_name | NULL }
| LOGIN = login_name
| PASSWORD = 'password' [ OLD_PASSWORD = 'oldpassword' ]
| ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]

-- SQL Database syntax when connected to a federation member
ALTER USER user_name
 WITH <set_item> [ ,... n ]
[;]

<set_item> ::=
 NAME = new_user_name
```

**Syntax for [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]**

```syntaxsql
-- Syntax for SQL database in Fabric
ALTER USER   
    {  
    Microsoft_Entra_principal FROM EXTERNAL PROVIDER [WITH OBJECT_ID = 'objectid'] 
    }  
 [ ; ]  
  
-- Users that cannot authenticate   
ALTER USER user_name   
    {  
         { FOR | FROM } CERTIFICATE cert_name   
       | { FOR | FROM } ASYMMETRIC KEY asym_key_name   
    }  
 [ ; ]  
  
<options_list> ::=  
    DEFAULT_LANGUAGE = { NONE | lcid | language name | language alias }   

-- SQL Database syntax when connected to a federation member  
ALTER USER user_name
[;]
```

**Syntax for [!INCLUDE [fabric](../../includes/fabric.md)] [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)]**

```syntaxsql
-- Syntax for Fabric Data Warehouse

ALTER USER user_name
 WITH <set_item> [ ,...n ]

<set_item> ::=
 | DEFAULT_SCHEMA = schema_name
[;]
```

## Arguments

#### *user_name*

 Specifies the name by which the user is identified inside this database.

#### LOGIN = _login_name_

 Remaps a user to another login by changing the user's Security Identifier (SID) to match the login's SID.

 If the ALTER USER statement is the only statement in a SQL batch, Azure SQL Database supports the `WITH LOGIN` clause. If the ALTER USER statement isn't the only statement in a SQL batch or is executed in dynamic SQL, the `WITH LOGIN` clause isn't supported.

#### NAME = _new_user_name_

 Specifies the new name for this user. *new_user_name* must not already exist in the current database.

#### DEFAULT_SCHEMA = { *schema_name* | NULL }

 Specifies the first schema that will be searched by the server when it resolves the names of objects for this user. 

Setting the default schema to `NULL` removes a default schema from a user created for a Microsoft Entra ID group. The NULL option can't be used with any other kind of user.

#### PASSWORD = '*password*'

 **Applies to**: [!INCLUDE[sssds](../../includes/ssazure-sqldb.md)].

 Specifies the password for the user that is being changed. Passwords are case-sensitive.

> [!NOTE]
> This option is available only for contained users. For more information, see [Contained Databases](../../relational-databases/databases/contained-databases.md) and [sp_migrate_user_to_contained (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md).

#### OLD_PASSWORD =_'oldpassword'_

 **Applies to**: [!INCLUDE[sssds](../../includes/ssazure-sqldb.md)].

 The current user password that will be replaced by '*password*'. Passwords are case-sensitive. *OLD_PASSWORD* is required to change a password, unless you have **ALTER ANY USER** permission. Requiring *OLD_PASSWORD* prevents users with **IMPERSONATION** permission from changing the password.

> [!NOTE]
> This option is available only for contained users.

#### ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]

 **Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later versions, [!INCLUDE[sssds](../../includes/ssazure-sqldb.md)].

 Suppresses cryptographic metadata checks on the server in bulk copy operations. This enables the user to bulk copy encrypted data between tables or databases, without decrypting the data. The default is OFF.

> [!WARNING]
> Improper use of this option can lead to data corruption. For more information, see [Bulk load encrypted data to columns using Always Encrypted](../../relational-databases/security/encryption/migrate-sensitive-data-protected-by-always-encrypted.md).  

## Remarks

 The default schema will be the first schema that will be searched by the server when it resolves the names of objects for this database user. Unless otherwise specified, the default schema will be the owner of objects created by this database user.

 If the user has a default schema, that default schema is used. If the user doesn't have a default schema, but the user is a member of a group that has a default schema, the default schema of the group will be used. If the user doesn't have a default schema, and is a member of more than one group, the default schema for the user will be that of the group with the lowest `principal_id` and an explicitly set default schema. If no default schema can be determined for a user, the `dbo` schema is used.

 DEFAULT_SCHEMA can be set to a schema that doesn't currently occur in the database. Therefore, you can assign a DEFAULT_SCHEMA to a user before that schema is created.

 DEFAULT_SCHEMA can't be specified for a user who is mapped to a certificate, or an asymmetric key.

> [!IMPORTANT]
> The value of DEFAULT_SCHEMA is ignored if the user is a member of the **sysadmin** fixed server role. All members of the **sysadmin** fixed server role have a default schema of `dbo`.

 The `WITH LOGIN` clause enables the remapping of a user to a different login. Users without a login, users mapped to a certificate, or users mapped to an asymmetric key can't be remapped with this clause. Only SQL users can be remapped. The `WITH LOGIN` clause can't be used to change the type of user.

 The name of the user will be automatically renamed to the login name if the following conditions are true.

- No new name was specified.

- The current name differs from the login name.

 Otherwise, the user won't be renamed unless the caller additionally invokes the `NAME` clause.

The name of a user mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, a certificate, or an asymmetric key can't contain the backslash character (`\`).

> [!NOTE]  
> [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]

### Fabric SQL database

For more information, see:

- [Authentication in Microsoft Fabric SQL database](/fabric/database/sql/authentication)
- [Authorization in Microsoft Fabric SQL database](/fabric/database/sql/authorization)

### Fabric Warehouse

In [!INCLUDE [fabric](../../includes/fabric.md)] [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)], ALTER USER is limited to setting the default schema only. Any other arguments commonly associated with ALTER USER in SQL or other products are not supported and will return an error.

In [!INCLUDE [fabric](../../includes/fabric.md)] [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)], the collation name is case sensitive.

The default schema will be the first schema that will be searched by the server when it resolves the names of objects for this database user. Unless otherwise specified, the default schema will be the owner of objects created by this database user.

If a user has a default schema, that schema will be used. If no default schema is specified, the dbo schema will be applied. The database engine will first attempt to resolve objects in the user's default schema. If no matching object is found, it will then check the dbo schema.

DEFAULT_SCHEMA can be set to a schema that doesn't currently exist in the database. Therefore, you can assign a DEFAULT_SCHEMA to a user before that schema is created.

For more information, see:

- [SQL granular permissions in Fabric Warehouse](/fabric/data-warehouse/sql-granular-permissions)
- [Security for data warehousing in Microsoft Fabric](/fabric/data-warehouse/security)

## Security

> [!NOTE]
> A user who has **ALTER ANY USER** permission can change the default schema of any user. A user who has an altered schema might unknowingly select data from the wrong table or execute code from the wrong schema.

### Permissions

 To change the name of a user requires the **ALTER ANY USER** permission.

 To change the target login of a user requires the **CONTROL** permission on the database.

 To change the user name of a user having **CONTROL** permission on the database requires the **CONTROL** permission on the database.

 To change the default schema or language requires **ALTER** permission on the user. Users can change their own default schema or language.

#### Fabric Warehouse role permissions

Users with a minimum of CONTRIBUTOR role membership at workspace are able to change the DEFAULT SCHEMA of any user on Fabric Data Warehouse. 

Users with VIEWER role membership at workspace, are unable to change their own defaults schema unless a user with elevated access grants this permission.  

Users included in the VIEWER role at workspace level, requires the ALTER ANY USER permission to change the default schema of other users.

## Examples

All examples are executed in a user database.

<a id="a-changing-the-name-of-a-database-user"></a>

### A. Change the name of a database user

 The following example changes the name of the database user `Mary5` to `Mary51`.

```sql
ALTER USER Mary5 WITH NAME = Mary51;
GO
```

<a id="b-changing-the-default-schema-of-a-user"></a>

### B. Change the default schema of a user

 The following example changes the default schema of the user `Mary51` to `Purchasing`.

```sql
ALTER USER Mary51 WITH DEFAULT_SCHEMA = Purchasing;
GO
```

<a id="c-changing-several-options-at-once"></a>

### C. Change several options at once

 The following example changes several options for a contained database user in one statement. Replace `<new strong password here>` and `<old strong password here>` with your own values.

```sql
ALTER USER Philip
WITH NAME = Philipe
, DEFAULT_SCHEMA = Development
, PASSWORD = '<new strong password here>' OLD_PASSWORD = '<old strong password here>';
GO
```

### D. Change the default schema of a user

The following example changes the default schema of the user `Mary51` to `Purchasing`.

```sql
ALTER USER Mary51 WITH DEFAULT_SCHEMA = Purchasing;
GO
```

## Related content

- [CREATE USER (Transact-SQL)](create-user-transact-sql.md)
- [DROP USER (Transact-SQL)](drop-user-transact-sql.md)
- [Contained Databases](../../relational-databases/databases/contained-databases.md)
- [sp_migrate_user_to_contained (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md)
- [Tables in Microsoft Fabric Data Warehouse](/fabric/data-warehouse/tables)
- [Authentication in Microsoft Fabric SQL database](/fabric/database/sql/authentication)

::: moniker-end
::: moniker range="=azuresqldb-mi-current"

:::row:::
    :::column:::
        [SQL Server](alter-user-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-user-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Microsoft Fabric Data Warehouse](alter-user-transact-sql.md?view=fabric&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL database in Microsoft Fabric](alter-user-transact-sql.md?view=fabric-sqldb&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* SQL Managed Instance \*_**
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-user-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-user-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure SQL Managed Instance

## Syntax

> [!IMPORTANT]
> Only the following options are supported for Azure SQL Managed Instance when applying to users with Microsoft Entra logins:
> `DEFAULT_SCHEMA = { schema_name | NULL }` and
> `DEFAULT_LANGUAGE = { NONE | lcid | language name | language alias }`
> </br> </br> There is a new syntax extension that was added to help remap users in a database that was migrated to Azure SQL Managed Instance. The ALTER USER syntax helps map database users in a federated and synchronized domain with Microsoft Entra ID, to Microsoft Entra logins.

```syntaxsql
-- Syntax for SQL Managed Instance
ALTER USER user_name
 { WITH <set_item> [ ,...n ] | FROM EXTERNAL PROVIDER }
[;]

<set_item> ::=
NAME = new_user_name
| DEFAULT_SCHEMA = { schema_name | NULL }
| LOGIN = login_name
| PASSWORD = 'password' [ OLD_PASSWORD = 'oldpassword' ]
| DEFAULT_LANGUAGE = { NONE | <lcid> | <language name> | <language alias> }
| ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]

-- Users or groups that are migrated as federated and synchronized with Azure AD have the following syntax:

/** Applies to Windows users that were migrated and have the following user names:
- Windows user <domain\user>
- Windows group <domain\MyWindowsGroup>
- Windows alias <MyWindowsAlias>
**/

ALTER USER user_name
 { WITH <set_item> [ ,...n ] | FROM EXTERNAL PROVIDER }
[;]

<set_item> ::=
 NAME = new_user_name
| DEFAULT_SCHEMA = { schema_name | NULL }
| LOGIN = login_name
| DEFAULT_LANGUAGE = { NONE | <lcid> | <language name> | <language alias> }
```

## Arguments

#### *user_name*
 Specifies the name by which the user is identified inside this database.

#### LOGIN = _login_name_
 Remaps a user to another login by changing the user's Security Identifier (SID) to match the login's SID.

 If the ALTER USER statement is the only statement in a SQL batch, Azure SQL Database supports the `WITH LOGIN` clause. If the ALTER USER statement isn't the only statement in a SQL batch or is executed in dynamic SQL, the `WITH LOGIN` clause isn't supported.

#### NAME = _new_user_name_
 Specifies the new name for this user. *new_user_name* must not already exist in the current database.

#### DEFAULT_SCHEMA = { *schema_name* | NULL }
 Specifies the first schema that will be searched by the server when it resolves the names of objects for this user. Setting the default schema to NULL removes a default schema from a Windows group. The NULL option can't be used with a Windows user.

#### PASSWORD = '*password*'

 Specifies the password for the user that is being changed. Passwords are case-sensitive.

> [!NOTE]
> This option is available only for contained users. For more information, see [Contained Databases](../../relational-databases/databases/contained-databases.md) and [sp_migrate_user_to_contained (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md).

#### OLD_PASSWORD = _'oldpassword'_

 The current user password that will be replaced by '*password*'. Passwords are case-sensitive. *OLD_PASSWORD* is required to change a password, unless you have **ALTER ANY USER** permission. Requiring *OLD_PASSWORD* prevents users with **IMPERSONATION** permission from changing the password.

> [!NOTE]
> This option is available only for contained users.

#### DEFAULT_LANGUAGE =_{ NONE | \<lcid> | \<language name> | \<language alias> }_

 Specifies a default language to be assigned to the user. If this option is set to NONE, the default language is set to the current default language of the database. If the default language of the database is later changed, the default language of the user will remain unchanged. *DEFAULT_LANGUAGE* can be the local ID (lcid), the name of the language, or the language alias.

> [!NOTE]
> This option can only be specified in a contained database and only for contained users.

#### ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]

 Suppresses cryptographic metadata checks on the server in bulk copy operations. This enables the user to bulk copy encrypted data between tables or databases, without decrypting the data. The default is OFF.

> [!WARNING]
> Improper use of this option can lead to data corruption. For more information, see [Bulk load encrypted data to columns using Always Encrypted](../../relational-databases/security/encryption/migrate-sensitive-data-protected-by-always-encrypted.md).

## Remarks

 The default schema will be the first schema that will be searched by the server when it resolves the names of objects for this database user. Unless otherwise specified, the default schema will be the owner of objects created by this database user.

 If the user has a default schema, that default schema is used. If the user doesn't have a default schema, but the user is a member of a group that has a default schema, the default schema of the group will be used. If the user doesn't have a default schema, and is a member of more than one group, the default schema for the user will be that of the Windows group with the lowest principal_id and an explicitly set default schema. If no default schema can be determined for a user, the `dbo` schema is used.

 DEFAULT_SCHEMA can be set to a schema that doesn't currently occur in the database. Therefore, you can assign a DEFAULT_SCHEMA to a user before that schema is created.

 DEFAULT_SCHEMA can't be specified for a user who is mapped to a certificate, or an asymmetric key.

> [!IMPORTANT]
> The value of DEFAULT_SCHEMA is ignored if the user is a member of the **sysadmin** fixed server role. All members of the **sysadmin** fixed server role have a default schema of `dbo`.

 You can change the name of a user who is mapped to a Windows login or group only when the SID of the new user name matches the SID that is recorded in the database. This check helps prevent spoofing of Windows logins in the database.

 The `WITH LOGIN` clause enables the remapping of a user to a different login. Users without a login, users mapped to a certificate, or users mapped to an asymmetric key can't be remapped with this clause. Only SQL users and Windows users (or groups) can be remapped. The `WITH LOGIN` clause can't be used to change the type of user, such as changing a Windows account to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The only exception is when changing a Windows user to a Microsoft Entra user.

> [!NOTE]
> The following rules do not apply to Windows users on Azure SQL Managed Instance as we do not support creating Windows logins on Azure SQL Managed Instance. The WITH LOGIN option can only be used if Microsoft Entra logins are present.

 The name of the user will be automatically renamed to the login name if the following conditions are true.

- The user is a Windows user.

- The name is a Windows name (contains a backslash).

- No new name was specified.

- The current name differs from the login name.

 Otherwise, the user won't be renamed unless the caller additionally invokes the `NAME` clause.

The name of a user mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, a certificate, or an asymmetric key can't contain the backslash character (`\`).

> [!NOTE]  
> [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]

### Remarks for Windows users in SQL on-premises migrated to Azure SQL Managed Instance

These remarks apply to authenticating as Windows users that have been federated and synchronized with Microsoft Entra ID.


- Validation of Windows users or groups that are mapped to Microsoft Entra ID is done by default through Graph API in all versions of the ALTER USER syntax used for migration purpose.
- On-premises users that were aliased (use a different name from the original Windows account) will keep the aliased name.
- For Microsoft Entra authentication, the LOGIN parameter applies only to Azure SQL Managed Instance and can't be used with SQL Database.
- To view logins for Microsoft Entra principals, use the following command: `SELECT * FROM sys.server_principals;`.
- Check the login's indicated type is `E` or `X`.
- PASSWORD option can't be used for Microsoft Entra users.
- In all migration cases, the roles and permissions of Windows users or groups will automatically be transferred to the new Microsoft Entra users or groups.
- `FROM EXTERNAL PROVIDER` is for altering Windows users and groups from SQL on-premises to Microsoft Entra users and groups. The Windows domain must be federated with Microsoft Entra ID and all Windows domain members must exist in Microsoft Entra ID when using this extension. The `FROM EXTERNAL PROVIDER` syntax applies to Azure SQL Managed Instance and should be used in case Windows users do not have logins on the original SQL instance and need to be mapped to standalone Microsoft Entra database users.
- In this case, the allowable `user_name` can be:
- A Windows user (`domain\user`).
- A Windows group (`MyWindowsGroup`).
- A Windows alias (`MyWindowsAlias`).
- The outcome of the ALTER command replaces the old `user_name` with the corresponding name that is found in Microsoft Entra ID based on the original SID of the old `user_name`. The altered name is replaced and stored in the metadata of the database:
- (`domain\user`) will be replaced with Microsoft Entra `user@domain.com`.
- (`domain\MyWindowsGroup`) will be replaced with Microsoft Entra group.
- (`MyWindowsAlias`) will remain unchanged but the SID of this user will be checked in Microsoft Entra ID.

> [!NOTE]
> If the SID of the original user converted to `object_ID` cannot be found in the Microsoft Entra ID tenant, the `ALTER USER` command will fail.

- To view altered users, use the following command: `SELECT * FROM sys.database_principals;`
- Check the user's indicated type `E` or `X`.
- When NAME is used to migrate Windows users to Microsoft Entra users, the following restrictions apply:
   - A valid LOGIN must be specified.
   - The NAME will be checked in the Microsoft Entra ID tenant and can only be:
      - The name of the LOGIN.
      - An alias - the name can't exist in Microsoft Entra ID.
      - In all other cases, the syntax fails.
    
## Security

> [!NOTE]
> A user who has **ALTER ANY USER** permission can change the default schema of any user. A user who has an altered schema might unknowingly select data from the wrong table or execute code from the wrong schema.

### Permissions

 To change the name of a user requires the **ALTER ANY USER** permission.

 To change the target login of a user requires the **CONTROL** permission on the database.

 To change the user name of a user having **CONTROL** permission on the database requires the **CONTROL** permission on the database.

 To change the default schema or language requires **ALTER** permission on the user. Users can change their own default schema or language.

## Examples

All examples are executed in a user database.

<a id="a-changing-the-name-of-a-database-user"></a>

### A. Change the name of a database user

 The following example changes the name of the database user `Mary5` to `Mary51`.

```sql
ALTER USER Mary5 WITH NAME = Mary51;
GO
```

<a id="b-changing-the-default-schema-of-a-user"></a>

### B. Change the default schema of a user

 The following example changes the default schema of the user `Mary51` to `Purchasing`.

```sql
ALTER USER Mary51 WITH DEFAULT_SCHEMA = Purchasing;
GO
```

<a id="c-changing-several-options-at-once"></a>

### C. Change several options at once

 The following example changes several options for a contained database user in one statement. Replace `<new strong password here>` and `<old strong password here>` with your own values.

```sql
ALTER USER Philip
WITH NAME = Philipe
, DEFAULT_SCHEMA = Development
, PASSWORD = '<new strong password here>' OLD_PASSWORD = '<old strong password here>'
, DEFAULT_LANGUAGE= French ;
GO
```

<a id="d-map-the-user-in-the-database-to-an-azure-ad-login-after-migration"></a>



### D. Map the user in the database to a Microsoft Entra login after migration

The following example remaps the user, `westus/joe` to a Microsoft Entra user, `joe@westus.com`. This example is for logins that already exist in the managed instance. This needs to be performed after you have completed a database migration to Azure SQL Managed Instance, and want to use the Microsoft Entra login to authenticate.

```sql
ALTER USER [westus/joe] WITH LOGIN = [joe@westus.com]

```

<a id="e-map-an-old-windows-user-in-the-database-without-a-login-in-azure-sql-managed-instance-to-an-azure-ad-user"></a>



### E. Map an old Windows user in the database without a login in Azure SQL Managed Instance to a Microsoft Entra user

The following example remaps the user, `westus/joe` without a login, to a Microsoft Entra user, `joe@westus.com`. The federated user must exist in Microsoft Entra ID.

```sql
ALTER USER [westus/joe] FROM EXTERNAL PROVIDER
```

<a id="f-map-the-user-alias-to-an-existing-azure-ad-login"></a>



### F. Map the user alias to an existing Microsoft Entra login

The following example remaps the user name, `westus\joe` to `joe_alias`. The corresponding Microsoft Entra login in this case is `joe@westus.com`.

```sql
ALTER USER [westus/joe] WITH LOGIN = [joe@westus.com], name= joe_alias
```

<a id="g-map-a-windows-group-that-was-migrated-in-azure-sql-managed-instance-to-an-azure-ad-group"></a>



### G. Map a Windows group that was migrated in Azure SQL Managed Instance to a Microsoft Entra group

The following example remaps the old on-premises group, `westus\mygroup` to a Microsoft Entra group `mygroup` in the managed instance. The group must exist in Microsoft Entra ID.

```sql
ALTER USER [westus\mygroup] WITH LOGIN = mygroup;
```

## Related content

- [CREATE USER (Transact-SQL)](create-user-transact-sql.md)
- [DROP USER (Transact-SQL)](drop-user-transact-sql.md)
- [Contained Databases](../../relational-databases/databases/contained-databases.md)
- [sp_migrate_user_to_contained (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md)
- [Tutorial: Migrating SQL Server on-premises Windows users and groups to SQL Managed Instance using T-SQL DDL syntax](/azure/sql-database/tutorial-managed-instance-azure-active-directory-migration)

::: moniker-end
::: moniker range="=azure-sqldw-latest"

:::row:::
    :::column:::
        [SQL Server](alter-user-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-user-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Microsoft Fabric Data Warehouse](alter-user-transact-sql.md?view=fabric&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL database in Microsoft Fabric](alter-user-transact-sql.md?view=fabric-sqldb&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-user-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure Synapse<br />Analytics \*_**
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-user-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure Synapse Analytics

[!INCLUDE [synapse-fabric-migration](../../includes/synapse-fabric-migration.md)]

## Syntax

```syntaxsql
-- Syntax for Azure Synapse

ALTER USER user_name
 WITH <set_item> [ ,...n ]

<set_item> ::=
 NAME = new_user_name
 | LOGIN = login_name
 | DEFAULT_SCHEMA = schema_name
[;]
```

## Arguments

#### *user_name*

 Specifies the name by which the user is identified inside this database.

#### LOGIN = _login_name_

 Remaps a user to another login by changing the user's Security Identifier (SID) to match the login's SID.

 If the ALTER USER statement is the only statement in a SQL batch, Azure SQL Database supports the `WITH LOGIN` clause. If the ALTER USER statement isn't the only statement in a SQL batch or is executed in dynamic SQL, the `WITH LOGIN` clause isn't supported.

#### NAME = _new_user_name_

 Specifies the new name for this user. *new_user_name* must not already exist in the current database.

#### DEFAULT_SCHEMA = { *schema_name* | NULL }

 Specifies the first schema that will be searched by the server when it resolves the names of objects for this user. Setting the default schema to NULL removes a default schema from a Windows group. The NULL option can't be used with a Windows user.

## Remarks

 The default schema will be the first schema that will be searched by the server when it resolves the names of objects for this database user. Unless otherwise specified, the default schema will be the owner of objects created by this database user.

 If the user has a default schema, that default schema is used. If the user doesn't have a default schema, but the user is a member of a group that has a default schema, the default schema of the group will be used. If the user doesn't have a default schema, and is a member of more than one group, the default schema for the user will be that of the Windows group with the lowest principal_id and an explicitly set default schema. If no default schema can be determined for a user, the `dbo` schema is used.

 DEFAULT_SCHEMA can be set to a schema that doesn't currently occur in the database. Therefore, you can assign a DEFAULT_SCHEMA to a user before that schema is created.

 DEFAULT_SCHEMA can't be specified for a user who is mapped to a certificate, or an asymmetric key.

> [!IMPORTANT]
> The value of DEFAULT_SCHEMA is ignored if the user is a member of the **sysadmin** fixed server role. All members of the **sysadmin** fixed server role have a default schema of `dbo`.

 The `WITH LOGIN` clause enables the remapping of a user to a different login. Users without a login, users mapped to a certificate, or users mapped to an asymmetric key can't be remapped with this clause. Only SQL users and Windows users (or groups) can be remapped. The `WITH LOGIN` clause can't be used to change the type of user, such as changing a Windows account to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.

 The name of the user will be automatically renamed to the login name if the following conditions are true.

- No new name was specified.

- The current name differs from the login name.

 Otherwise, the user won't be renamed unless the caller additionally invokes the `NAME` clause.

The name of a user mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, a certificate, or an asymmetric key can't contain the backslash character (`\`).

> [!NOTE]  
> [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]

## Security

> [!NOTE]
> A user who has **ALTER ANY USER** permission can change the default schema of any user. A user who has an altered schema might unknowingly select data from the wrong table or execute code from the wrong schema.

### Permissions

 To change the name of a user requires the **ALTER ANY USER** permission.

 To change the target login of a user requires the **CONTROL** permission on the database.

 To change the user name of a user having **CONTROL** permission on the database requires the **CONTROL** permission on the database.

 To change the default schema or language requires **ALTER** permission on the user. Users can change their own default schema or language.

## Examples

All examples are executed in a user database.

<a id="a-changing-the-name-of-a-database-user"></a>

### A. Change the name of a database user

 The following example changes the name of the database user `Mary5` to `Mary51`.

```sql
ALTER USER Mary5 WITH NAME = Mary51;
GO
```

<a id="b-changing-the-default-schema-of-a-user"></a>

### B. Change the default schema of a user

The following example changes the default schema of the user `Mary51` to `Purchasing`.

```sql
ALTER USER Mary51 WITH DEFAULT_SCHEMA = Purchasing;
GO
```

## Related content

- [CREATE USER (Transact-SQL)](create-user-transact-sql.md)
- [DROP USER (Transact-SQL)](drop-user-transact-sql.md)
- [Contained Databases](../../relational-databases/databases/contained-databases.md)
- [sp_migrate_user_to_contained (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md)

::: moniker-end
::: moniker range=">=aps-pdw-2016"

:::row:::
    :::column:::
        [SQL Server](alter-user-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-user-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Microsoft Fabric Data Warehouse](alter-user-transact-sql.md?view=fabric&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL database in Microsoft Fabric](alter-user-transact-sql.md?view=fabric-sqldb&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-user-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-user-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Analytics<br />Platform System (PDW) \*_**
    :::column-end:::
:::row-end:::

&nbsp;

## Analytics Platform System

## Syntax

```syntaxsql
-- Syntax for Analytics Platform System

ALTER USER user_name
 WITH <set_item> [ ,...n ]

<set_item> ::=
 NAME = new_user_name
 | LOGIN = login_name
 | DEFAULT_SCHEMA = schema_name
[;]
```

## Arguments

#### *user_name*

 Specifies the name by which the user is identified inside this database.

#### LOGIN = _login_name_

 Remaps a user to another login by changing the user's Security Identifier (SID) to match the login's SID.

 If the ALTER USER statement is the only statement in a SQL batch, Azure SQL Database supports the `WITH LOGIN` clause. If the ALTER USER statement isn't the only statement in a SQL batch or is executed in dynamic SQL, the `WITH LOGIN` clause isn't supported.

#### NAME = _new_user_name_

 Specifies the new name for this user. *new_user_name* must not already exist in the current database.

#### DEFAULT_SCHEMA = { *schema_name* | NULL }

 Specifies the first schema that will be searched by the server when it resolves the names of objects for this user. Setting the default schema to NULL removes a default schema from a Windows group. The NULL option can't be used with a Windows user.

## Remarks

 The default schema will be the first schema that will be searched by the server when it resolves the names of objects for this database user. Unless otherwise specified, the default schema will be the owner of objects created by this database user.

 If the user has a default schema, that default schema is used. If the user doesn't have a default schema, but the user is a member of a group that has a default schema, the default schema of the group will be used. If the user doesn't have a default schema, and is a member of more than one group, the default schema for the user will be that of the Windows group with the lowest principal_id and an explicitly set default schema. If no default schema can be determined for a user, the `dbo` schema is used.

 DEFAULT_SCHEMA can be set to a schema that doesn't currently occur in the database. Therefore, you can assign a DEFAULT_SCHEMA to a user before that schema is created.

 DEFAULT_SCHEMA can't be specified for a user who is mapped to a certificate, or an asymmetric key.

> [!IMPORTANT]
> The value of DEFAULT_SCHEMA is ignored if the user is a member of the **sysadmin** fixed server role. All members of the **sysadmin** fixed server role have a default schema of `dbo`.

 The `WITH LOGIN` clause enables the remapping of a user to a different login. Users without a login, users mapped to a certificate, or users mapped to an asymmetric key can't be remapped with this clause. Only SQL users and Windows users (or groups) can be remapped. The `WITH LOGIN` clause can't be used to change the type of user, such as changing a Windows account to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.

 The name of the user will be automatically renamed to the login name if the following conditions are true.

- No new name was specified.

- The current name differs from the login name.

 Otherwise, the user won't be renamed unless the caller additionally invokes the `NAME` clause.

The name of a user mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, a certificate, or an asymmetric key can't contain the backslash character (`\`).

> [!NOTE]  
> [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]

## Security

> [!NOTE]
> A user who has **ALTER ANY USER** permission can change the default schema of any user. A user who has an altered schema might unknowingly select data from the wrong table or execute code from the wrong schema.

### Permissions

 To change the name of a user requires the **ALTER ANY USER** permission.

 To change the target login of a user requires the **CONTROL** permission on the database.

 To change the user name of a user having **CONTROL** permission on the database requires the **CONTROL** permission on the database.

 To change the default schema or language requires **ALTER** permission on the user. Users can change their own default schema or language.

## Examples

All examples are executed in a user database.

<a id="a-changing-the-name-of-a-database-user"></a>

### A. Change the name of a database user

 The following example changes the name of the database user `Mary5` to `Mary51`.

```sql
ALTER USER Mary5 WITH NAME = Mary51;
GO
```

<a id="b-changing-the-default-schema-of-a-user"></a>

### B. Change the default schema of a user
 The following example changes the default schema of the user `Mary51` to `Purchasing`.

```sql
ALTER USER Mary51 WITH DEFAULT_SCHEMA = Purchasing;
GO
```

## Related content

- [CREATE USER (Transact-SQL)](create-user-transact-sql.md)
- [DROP USER (Transact-SQL)](drop-user-transact-sql.md)
- [Contained Databases](../../relational-databases/databases/contained-databases.md)
- [sp_migrate_user_to_contained (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md)

::: moniker-end

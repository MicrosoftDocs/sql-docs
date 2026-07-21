---
title: "DROP SERVER ROLE (Transact-SQL)"
description: DROP SERVER ROLE removes a user-defined server role.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP SERVER ROLE"
  - "DROP_SERVER_ROLE_TSQL"
helpviewer_keywords:
  - "SERVER ROLE, DROP"
  - "DROP SERVER ROLE statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# DROP SERVER ROLE (Transact-SQL)

[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

Removes a user-defined server role.

User-defined server roles were introduced in [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DROP SERVER ROLE role_name
[ ; ]
```

## Arguments

#### *role_name*

Specifies the user-defined server role to be dropped from the server.

## Remarks

User-defined server roles that own securables can't be dropped from the server. To drop a user-defined server role that owns securables, you must first transfer ownership of those securables or delete them.

User-defined server roles that have members can't be dropped. To drop a user-defined server role that has members, you must first remove members of the role by using [ALTER SERVER ROLE](alter-server-role-transact-sql.md).

Fixed server roles can't be removed.

You can view information about role membership by querying the [sys.server_role_members](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md) catalog view.

## Permissions

Requires `CONTROL` permission on the server role or `ALTER ANY SERVER ROLE` permission.

## Examples

### A. To drop a server role

The following example drops the server role `purchasing`.

```sql
DROP SERVER ROLE purchasing;
GO
```

### B. To view role membership

To view role membership, use the **Server Role (Members)** page in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or execute the following query:

```sql
SELECT SRM.role_principal_id,
       SP.name AS Role_Name,
       SRM.member_principal_id,
       SP2.name AS Member_Name
FROM sys.server_role_members AS SRM
     INNER JOIN sys.server_principals AS SP
         ON SRM.Role_principal_id = SP.principal_id
     INNER JOIN sys.server_principals AS SP2
         ON SRM.member_principal_id = SP2.principal_id
ORDER BY SP.name, SP2.name;
```

### C. To view role membership

To determine whether a server role owns another server role, execute the following query:

```sql
SELECT SP1.name AS RoleOwner,
       SP2.name AS Server_Role
FROM sys.server_principals AS SP1
     INNER JOIN sys.server_principals AS SP2
         ON SP1.principal_id = SP2.owning_principal_id
ORDER BY SP1.name;
```

## Related content

- [ALTER ROLE (Transact-SQL)](alter-role-transact-sql.md)
- [CREATE ROLE (Transact-SQL)](create-role-transact-sql.md)
- [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [DROP ROLE (Transact-SQL)](drop-role-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [sp_addrolemember (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)
- [sys.database_role_members (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)
- [sys.database_principals (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)

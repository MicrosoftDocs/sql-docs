---
title: Security cache
description: Discover how the security cache in SQL Server and Azure SQL operates.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: randolphwest, vanto
ms.date: 04/29/2025
ms.service: sql
ms.topic: concept-article
ms.custom:
  - build-2025
# customer intent: As a database engineer or administrator, I want understand how the security cache works so that I can understand performance impacts.
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Security cache concepts

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article explains how SQL Server uses a security cache to validate permissions a principal has to access securables.

## Purpose

The Database Engine organizes a hierarchical collection of entities, known as securables, which can be secured with permissions. The most prominent securables are servers and databases, but permissions can also be set at a finer level. SQL Server controls the actions of principals on securables by ensuring they have the appropriate permissions.

The following diagram shows that a user, Alice, has a login at the server level, and a three different users mapped to the same login at each different database.

:::image type="content" source="media/security-cache/logins-and-users.png" alt-text="Diagram represents that Alice can have one login at the server level and three different users mapped to the same login in each of the different databases." lightbox="media/security-cache/logins-and-users.png":::

To optimize the authentication process, SQL Server uses a security cache.

## No cache workflow

When the security cache is invalid, SQL Server follows a no cache workflow to validate permissions. This section describes the no cache workflow.

To demonstrate, consider the following query:

```sql
SELECT t1.Column1,
       t2.Column1
FROM Schema1.Table1 AS t1
     INNER JOIN Schema2.Table2 AS t2
         ON t1.Column1 = t2.Column2;
```

When the security cache is invalid, the service completes the steps described in the following workflow before it resolves the query.

:::image type="content" source="media/security-cache/no-cache-workflow.svg" alt-text="Diagram represents the no cache workflow.":::

For SQL Server the tasks without a security cache include:

1. Connect to the instance.
1. Perform login validation.
1. Create the security context token and login token. Details of these tokens are explained in the next section.
1. Connect to the database.
1. Create a database user token inside the database.
1. Check the membership of database roles. For example, db_datareader, db_datawriter, or db_owner.
1. Verify user permissions on all columns, for example, the permissions of the user on `t1.Column1` and `t2.Column1`.
1. Checks user permissions on all tables, such as `table1` and `table2`, and schema permissions on `Schema1` and `Schema2`.
1. Verifies database permissions.

SQL Server repeats the process for every single role that the user belongs to. Once all permissions are obtained, the server performs a check to ensure that the user has all the necessary grants in the chain and not a single deny in the chain. After the permission check is complete, the query execution begins.

For more information, review [Summary of the permission check algorithm](../relational-databases/security/permissions-database-engine.md#summary-of-the-permission-check-algorithm).

To simplify validation, SQL Server uses a security cache.

## Security cache definition

The security cache stores permissions for a user or a login for various securable objects in a database or server. One of the benefits is that it speeds up query execution. Before SQL Server executes a query, it checks if the user has the necessary permissions for different database securables, such as schema-level permissions, table-level permissions, and column permissions.

### Security cache objects

To make the workflow explained in the previous section faster, SQL Server caches many different objects inside security caches. Some of the objects that are cached include:

| Token | Description |
| --- | --- |
| `SecContextToken` | The server-wide security context for a principal is held within this structure. It contains a hashtable of user tokens and serves as the starting point or base for all other caches. Includes references to the login token, user token, audit cache, and TokenPerm cache. Additionally, it acts as the base token for a login at the server level. |
| `LoginToken` | Similar to the security context token. Contains details of server-level principals. The login token includes various elements such as SID, login ID, login type, login name, isDisabled status, and server fixed role membership. Additionally, it encompasses special roles at the server level, such as sysadmin and security admin. |
| `UserToken` | This structure is related to database-level principals. It includes details such as username, database roles, SID, default language, default schema, ID, roles, and name. There's one user token per database for a login. |
| `TokenPerm` | Records all permissions for a securable object for a UserToken or SecContextToken. |
| `TokenAudit` | Key is the class and ID of a securable object. The entry is a series of lists containing audit IDs for each auditable operation on an object. Server audit is based on permission checks, detailing each auditable operation a specific user has on a particular object. |
| `TokenAccessResult` | This cache stores query permission check results for individual queries, with one entry per query plan. It's the most important and commonly used cache, as it's the first thing checked during query execution. To prevent ad hoc queries from flooding the cache, it only stores query permission check results if the query is executed three times. |
| `ObjectPerm` | This records all permissions for an object in the database for all users within the database. The difference between TokenPerm and ObjectPerm is that TokenPerm is for a specific user, while ObjectPerm can be for all users in the database. |

### Security cache stores

The tokens are stored inside different cache stores.

| Store | Description |
| --- | --- |
| `TokenAndPermUserStore` | One big store which contains all of the following objects:<br /><br />- `SecContextToken`<br />- `LoginToken`<br />- `UserToken`<br />- `TokenPerm`<br />- `TokenAudit` |
| `SecCtxtACRUserStore` | Access check result (ACR) store. Every login has their own separate security context user store. |
| `ACRUserStore` | Access check result store<br />`<unique id>` - <br />`<db id>`<br />- `<user id>`<br /><br />Every user has individual ACR user store. For example, two logins with five users in two different databases amounts to two `SecCtxtACRUserStore` and 10 different `ACRUserStore`. |
| `ObjectPerm` | One per database `ObjPerm` tokens. All different securables inside the database. |

## Known issues

This section describes issues with the security cache.

### Security cache invalidations

Various scenarios can trigger security cache invalidations at either the database or server level. When an invalidation occurs, all current cache entries are invalidated. All future queries and permission checks follow the full "No cache" workflow until the caches are repopulated. Invalidation can significantly affect server performance, especially under high load, as all active connections need to rebuild the cached entries. Repeated cache invalidations can make this impact worse. Invalidations in the `master` database are treated as server-wide invalidations, affecting the caches in all databases on the instance. 

SQL Server 2025 introduces a feature that invalidates caches for only a specific login. This means that when security cache entries are invalidated, only those entries belonging to the affected login are affected. For instance, if you grant login L1 a new permission, the tokens for login L2 remain unaffected.

As an initial step, this feature applies to the CREATE, ALTER and DROP login scenarios, and permission changes for individual logins. Group logins continue to experience server-level invalidation.

The table below lists all security Data Definition Language (DDL) actions that invalidate the security cache.

| Action | Subject | Scope |
| --- | --- | --- |
| `CREATE/ALTER/DROP` | `APPLICATION ROLE`<br />`SYMMETRIC KEY`<br />`ASYMMETRIC KEY`<br />`AUTHORIZATION`<br />`CERTIFICATE`<br />`ROLE`<br />`SCHEMA`<br />`USER` | Specified database |
| `DROP` | *Any* object that appears in [sys.all_objects](system-catalog-views/sys-all-objects-transact-sql.md) or any securable listed in the database or schema-scoped [securable list](security/securables.md). | Specified database |
| `GRANT/DENY/REVOKE` | *Any* [permission](security/permissions-database-engine.md) to securable contained by database or schema. | Specified database |
| `CREATE/ALTER/DROP` | `LOGIN`<br />(`SERVICE`) `MASTER KEY` | SQL Server instance |
| `ALTER` | `DATABASE` | Specified database |

### Query performance when the size of TokenAndPermUserStore grows

Performance issues, such as high CPU usage and increased memory consumption, can be caused by excessive entries in the TokenAndPermUserStore cache. By default, SQL Server only cleans up entries in this cache when it detects internal memory pressure. However, on servers with plenty of RAM, internal memory pressure might not occur frequently. As the cache grows, the time required to search for existing entries to reuse increases. This cache is managed by a spinlock, allowing only one thread to perform the search at a time. Consequently, this behavior can lead to decreased query performance and higher CPU usage.

#### Workaround

SQL Server provides two trace flags (TF) that can be used to set a quota for the TokenAndPermUserStore cache. By default, there's no quota, meaning the cache can hold an unlimited number of entries.

- TF 4618: Limits the number of entries in the TokenAndPermUserStore to 1024.
- TF 4618 and TF 4610: Limits the number of entries in the TokenAndPermUserStore to 8192. If the low entry count limit of TF 4618 causes other performance issues, it's recommended to use trace flags 4610 and 4618 together. For more information, see [Set trace flags with DBCC TRACEON](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

For more information, you can refer to the article [Performance issues can be caused by excessive entries in the TokenAndPermUserStore cache - SQL Server](/troubleshoot/sql/database-engine/performance/token-and-perm-user-store-perf-issue)

## Best practices

This section lists best practices to optimize security workflow.

### User management during nonpeak hours

Given the high-level invalidation nature of security caches (database/server level), perform security DDLs during nonbusiness hours when the server load is low. If a security cache invalidation occurs during heavy workloads, there can be a noticeable performance impact on the entire server as the security caches are repopulated.

### Use single transactions for permission modifications

Performing multiple security Data Definition Language (DDL) operations within the same transaction can significantly increase the chance of encountering deadlocks with other active connections To mitigate this risk, it's recommended to avoid executing multiple security DDLs in a single transaction. Instead, execute security-related DDL operations in separate transactions to minimize lock contention.

## Related content

- [Get started with Database Engine permissions](security/authentication-access/getting-started-with-database-engine-permissions.md)

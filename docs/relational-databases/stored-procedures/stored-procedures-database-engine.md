---
title: "Stored procedures (Database Engine)"
description: Learn how a stored procedure in SQL Server is a group of one or more Transact-SQL statements or a reference to a .NET Framework common runtime language method.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 01/04/2024
ms.service: sql
ms.subservice: stored-procedures
ms.topic: conceptual
helpviewer_keywords:
  - "storing programs as stored procedures"
  - "stored procedures [SQL Server], about stored procedures"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Stored procedures (Database Engine)

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

A stored procedure in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is a group of one or more [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, or a reference to a [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] common runtime language (CLR) method. Procedures resemble constructs in other programming languages because they can:

- Accept input parameters and return multiple values in the form of output parameters to the calling program.

- Contain programming statements that perform operations in the database. These include calling other procedures.

- Return a status value to a calling program to indicate success or failure (and the reason for failure).

## Benefits of using stored procedures

The following list describes some benefits of using procedures.

### Reduced server/client network traffic

The commands in a procedure are executed as a single batch of code. This can significantly reduce network traffic between the server and client because only the call to execute the procedure is sent across the network. Without the code encapsulation provided by a procedure, every individual line of code would have to cross the network.

### Stronger security

Multiple users and client programs can perform operations on underlying database objects through a procedure, even if the users and programs don't have direct permissions on those underlying objects. The procedure controls what processes and activities are performed and protects the underlying database objects. This eliminates the requirement to grant permissions at the individual object level and simplifies the security layers.

The [EXECUTE AS](../../t-sql/statements/execute-as-clause-transact-sql.md) clause can be specified in the `CREATE PROCEDURE` statement to enable impersonating another user, or enable users or applications to perform certain database activities without needing direct permissions on the underlying objects and commands. For example, some actions such as `TRUNCATE TABLE` don't have grantable permissions. To execute `TRUNCATE TABLE`, the user must have `ALTER` permissions on the specified table. Granting a user `ALTER` permissions on a table might not be ideal, because the user effectively has permissions well beyond the ability to truncate a table. By incorporating the `TRUNCATE TABLE` statement in a module and specifying that module execute as a user who has permissions to modify the table, you can extend the permissions to truncate the table to the user that you grant `EXECUTE` permissions on the module.

When an application calls a procedure over the network, only the call to execute the procedure is visible. Therefore, malicious users can't see table and database object names, embed [!INCLUDE [tsql](../../includes/tsql-md.md)] statements of their own, or search for critical data.

Using procedure parameters helps guard against SQL injection attacks. Since parameter input is treated as a literal value and not as executable code, it's more difficult for an attacker to insert a command into the [!INCLUDE [tsql](../../includes/tsql-md.md)] statements inside the procedure and compromise security.

Procedures can be encrypted, helping to obfuscate the source code. For more information, see [SQL Server encryption](../security/encryption/sql-server-encryption.md).

### Reuse of code

The code for any repetitious database operation is the perfect candidate for encapsulation in procedures. This eliminates needless rewrites of the same code, decreases code inconsistency, and allows the access and execution of code by any user or application possessing the necessary permissions.

### Easier maintenance

When client applications call procedures and keep database operations in the data tier, only the procedures must be updated for any changes in the underlying database. The application tier remains separate and doesn't have to know how about any changes to database layouts, relationships, or processes.

### Improved performance

By default, a procedure compiles the first time it's executed, and creates an execution plan that is reused for subsequent executions. Since the query processor doesn't have to create a new plan, it typically takes less time to process the procedure.

If there are significant changes to the tables or data referenced by the procedure, the precompiled plan might actually cause the procedure to perform slower. In this case, recompiling the procedure and forcing a new execution plan can improve performance.

## Types of stored procedures

### User-defined

A user-defined procedure can be created in a user-defined database or in all system databases except the `Resource` database. The procedure can be developed in either [!INCLUDE [tsql](../../includes/tsql-md.md)], or as a reference to a [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] common runtime language (CLR) method.

### Temporary

Temporary procedures are a form of user-defined procedures. Temporary procedures are like a permanent procedure, except that they're stored in `tempdb`. There are two types of temporary procedures: *local* and *global*. They differ from each other in their names, their visibility, and their availability. Local temporary procedures have a single number sign (`#`) as the first character of their names; they're visible only to the current user connection, and they're deleted when the connection is closed. Global temporary procedures have two number signs (`##`) as the first two characters of their names; they're visible to any user after they are created, and they're deleted at the end of the last session using the procedure.

### System

System procedures are included with the [!INCLUDE [ssde-md](../../includes/ssde-md.md)]. They are physically stored in the internal, hidden `Resource` database and logically appear in the `sys` schema of every system-defined and user-defined database. In addition, the `msdb` database also contains system stored procedures in the `dbo` schema that are used for scheduling alerts and jobs. Because system procedures start with the prefix `sp_`, we recommend that you don't use this prefix when naming user-defined procedures. For a complete list of system procedures, see [System stored procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md).

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the system procedures that provide an interface from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to external programs for various maintenance activities. These extended procedures use the `xp_` prefix. For a complete list of extended procedures, see [General extended stored procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/general-extended-stored-procedures-transact-sql.md).

### Extended user-defined

Extended procedures enable creating external routines in a programming language such as C. These procedures are DLLs that an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can dynamically load and run.

> [!NOTE]  
> Extended stored procedures will be removed in a future version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Don't use this feature in new development work, and modify applications that currently use this feature as soon as possible. Create CLR procedures instead. This method provides a more robust and secure alternative to writing extended procedures.

## Related tasks

| Task description | Article |
| --- | --- |
| Describes how to create a stored procedure. | [Create a stored procedure](create-a-stored-procedure.md) |
| Describes how to modify a stored procedure. | [Modify a stored procedure](modify-a-stored-procedure.md) |
| Describes how to delete a stored procedure. | [Delete a stored procedure](delete-a-stored-procedure.md) |
| Describes how to execute a stored procedure. | [Execute a stored procedure](execute-a-stored-procedure.md) |
| Describes how to grant permissions on a stored procedure. | [Grant Permissions on a stored procedure](grant-permissions-on-a-stored-procedure.md) |
| Describes how to return data from a stored procedure to an application. | [Return data from a stored procedure](return-data-from-a-stored-procedure.md) |
| Describes how to recompile a stored procedure. | [Recompile a stored procedure](recompile-a-stored-procedure.md) |
| Describes how to rename a stored procedure. | [Rename a stored procedure](rename-a-stored-procedure.md) |
| Describes how to view the definition of a stored procedure. | [View the definition of a stored procedure](view-the-definition-of-a-stored-procedure.md) |
| Describes how to view the dependencies on a stored procedure. | [View the dependencies of a stored procedure](view-the-dependencies-of-a-stored-procedure.md) |
| Describes how parameters are used in a stored procedure. | [Parameters](parameters.md) |

## Related content

- [CLR Stored Procedures](/dotnet/framework/data/adonet/sql/clr-stored-procedures)
- [Deferred name resolution](../../t-sql/statements/create-trigger-transact-sql.md#deferred-name-resolution)

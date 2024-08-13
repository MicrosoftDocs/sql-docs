---
title: "MSSQLSERVER_4064"
description: "MSSQLSERVER_4064"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: sureshka, randolphwest
ms.date: 01/10/2024
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "4064 (Database Engine error)"
---
# MSSQLSERVER_4064

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 4064 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | DB_UFAIL_FATAL |
| Message Text | Cannot open user default database. Login failed. |

## Explanation

The SQL Server login was unable to connect to SQL Server, either due to permission issues with a database user associated with the login in the default database, or a problem with its default database.

Permission issues can be one or more of the following:

- The login doesn't have a corresponding mapped user in its default database.
- You assigned a default database to the login but didn't create a user mapping in the specified database.
- The mapped user for the login has been denied access. (For example, this can happen if the user is inadvertently added to a **db_denydatareader** fixed database role.)

The default database may be unavailable at the time of connection for the following reasons:

- The default database is in suspect mode.
- The default database no longer exists.
- The default database name is not correct. 
- The default database is in single-user mode, and the only available connection is already used by someone or something else.
- The default database has been detached.
- The default database is set to a RESTRICTED_USER state.
- The default database is offline.
- The default database is set to an EMERGENCY state.
- The default database is part of a database mirror.

Additionally, the login account may be a member of multiple groups, and the default database for one of those groups is unavailable at the time of connection.

For more information about database users in SQL Server, see [Create a Database User](../security/authentication-access/create-a-database-user.md).

## User action

You can take one of the following actions:

### Work around the issue

If you don't need to access the currently configured default database and you just need to connect to the SQL Server instance for other operations using SQL Server Management Studio (SSMS), follow these steps:

1. Open SQL Server Management Studio (SSMS).

1. On **Object Explorer**, select **Connect** > **Database Engine**.

1. Fill out the **Connect to Server** dialog.

1. Select **Options**.

1. Under **Connection Properties**, modify the **Connect to database** value using one of the following options:

    - If the login is a member of the sysadmin role, enter `master`, and select **Connect** to establish a connection to SQL Server. Once you're successfully connected to SQL Server, you can change your default database to a different one that's currently available in the **General** page of login's properties in SSMS. For more information, see [Create a Login](../security/authentication-access/create-a-login.md).

    - If the login isn't a member of the sysadmin role, enter a database name on the server that you know you have access to. Alternatively, you can try entering a system database name like `master` and then select **Connect**. This step may not work if your system administrator has explicitly denied permissions to a guest user in the `master` database. In that scenario, you need to work with your system administrator to resolve the issue.

### Fix the issue

A system administrator can check what the current default database of the user is, using the following query:

```sql
SELECT name, default_database_name
FROM sys.server_principals
WHERE type = 'S' AND name = '<sql-login>';
```

Use the following table to determine the appropriate action for fixing the issue for associated causes:

| Cause | Resolution |
| --- | --- |
| No user mapping exists in the login's default database or the user has been denied access. | These database users are also referred to as orphaned users. This problem typically occurs when databases are moved between two server instances, and is one of the common causes of the 4064 error. To detect orphaned users and resolve the problem, see [Troubleshoot orphaned users (SQL Server)](../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md). |
| No database user exists for the login | [Create a database user](../security/authentication-access/create-a-database-user.md) and assign relevant permissions to access the database. |
| Database user account denied permissions to access the database | Navigate to the user properties in the database (Expand database node > **Security** > **Users**) and check if the user is part of **db_denydatareader** role in the **Membership** page. You can also check effective permissions of a user using [sys.fn_my_permissions](../system-functions/sys-fn-my-permissions-transact-sql.md). |
| The default database is in suspect mode. | A database can become suspect for several reasons. Possible causes include denial of access to a database resource by the operating system and the unavailability or corruption of one or more database files. You can check the state of the database using this query: `SELECT DATABASEPROPERTYEX (N'<dbname>', N'STATUS') AS N'Database Status';`. In SSMS, the status of suspect databases shows as (Recovery Pending). You may have to recover the database from its backup to resolve this situation. |
|Incorrect database name in connection string|When you attempt to connect to a database that doesn't exist, you might see the following error message:<br/>`Cannot open database "AdventureWorks" requested by the login. The login failed.`<br/>The database management system (DBMS) might also show the `Login failed for user CONTOSO\user1` error message. For more information, see [MSSQLSERVER_18456](mssqlserver-18456-database-engine-error.md).<br/>The SQL Server error log will have the following message:<br/>"Login failed for user 'CONTOSO\User1'. Reason: Failed to open the explicitly specified database 'AdventureWorks'."<br/>To resolve this error, make sure that the database name is the same in both the error message and the error log entry. Change the connection string if it's incorrect, or grant the user the required permissions.|
| The default database no longer exists. | If you've intentionally removed the database from the server, change the default database for the login to another existing database on the server using SSMS or an [ALTER LOGIN (Transact-SQL)](../../t-sql/statements/alter-login-transact-sql.md) statement. Optionally, you may want to check if there are other logins on the server whose default database is set to this non-existing database by using this query: `SELECT name AS Login_Name FROM sys.server_principals where default_database_name = '<removed-dbname>';`. |
| The default database is in a single-user mode, and the only connection is being used by the administrator or someone else. | If the database is set to single-user mode for maintenance purposes, you should set it back to multi-user mode after the completion of maintenance activity, using this query: `ALTER DATABASE <dbname> SET MULTI_USER;`.<br />For more information, see [Set a database to single-user mode](../databases/set-a-database-to-single-user-mode.md).<br />**Note**: To check if a database is in single-user mode, you can use this query: `SELECT name, user_access_desc FROM sys.databases WHERE name = '<dbname>';`.<br />If you still need to restrict access to the database but want to enable connectivity for the affected logins, change their default database to a different database on the server. |
| The default database has been detached. | Detaching a database removes it from the instance of SQL Server and is no longer accessible. To make it available for logins, attach the database using SSMS or an [sp_attach_db](../system-stored-procedures/sp-attach-db-transact-sql.md) stored procedure. For more information, see [Database Detach and Attach (SQL Server)](../databases/database-detach-and-attach-sql-server.md). |
| The default database has been set to a RESTRICTED_USER state. | When a database is set to a RESTRICTED_USER state, only members of the db_owner fixed database role and dbcreator and sysadmin fixed server roles can connect to the database. If you no longer need to restrict access to the corresponding database, set the database to multi-user mode using this query: `ALTER DATABASE <dbname> SET MULTI_USER;`.<br />**Note**: To check if a database is in a restricted-user state, you can use this query: `SELECT name, user_access_desc FROM sys.databases WHERE name = '<dbname>';`.<br />If you still need to restrict access to the database but want to enable connectivity for the affected logins, change their default database to a different database on the server. |
| The default database is offline. | A database that is in the offline state can't be modified. You can bring your database online using this query: `ALTER database <dnname> SET ONLINE;`.<br />You can check if a database is OFFLINE either using SSMS, or using this query: `SELECT DATABASEPROPERTYEX (N'<dbname>', N'STATUS') AS N'Database Status';`.<br />For more information, see [Database States](../databases/database-states.md) and [ALTER DATABASE SET Options (Transact-SQL) - SQL Server](../../t-sql/statements/alter-database-transact-sql-set-options.md).<br />If you still need to keep the database in an offline state but want to enable connectivity for the affected logins, change their default database to a different database on the server. |
| The default database is set to an EMERGENCY state. | A database may have been put into an EMERGENCY state for troubleshooting by a system administrator. Only users of the sysadmin role can access databases set to an EMERGENCY state. You can bring your database online using this query: `ALTER database <dnname> SET ONLINE;`.<br />You can check if a database is in an EMERGENCY state either using SSMS, or this query: `SELECT DATABASEPROPERTYEX (N'<dbname>', N'STATUS') AS N'Database Status';`.<br />For more information, see [Database States](../databases/database-states.md) and [ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md).<br />If you still need to keep the database in an EMERGENCY state, but want to enable connectivity for the affected logins, change their default database to a different database on the server. |
| The default database is part of the database mirror. | You can't connect to a mirror database on the mirror server, and this behavior is by design. To check if the database is currently in mirror role, use this query `SELECT DB_NAME(database_id) as database_name, mirroring_role_desc FROM sys.database_mirroring WHERE DB_NAME(database_id) = '<dbname>';`. For more information about database mirroring, see [Database Mirroring (SQL Server)](../../database-engine/database-mirroring/database-mirroring-sql-server.md). |
| The login account may be a member of multiple groups, and the default database for one of the groups is unavailable at the time of connection. | To enumerate the groups that have a specified user using PowerShell, see [Get-ADPrincipalGroupMembership (ActiveDirectory)](/powershell/module/activedirectory/get-adprincipalgroupmembership). |

## Change the default database for a given user

To make changes to a user's default database, you need to have ALTER ANY LOGIN permission. If the login being changed is a member of the sysadmin fixed server role or a grantee of CONTROL SERVER permission, it also requires CONTROL SERVER permission when making the following changes. Members of the sysadmin role have these permissions enabled by default. For more information, see [ALTER LOGIN (Transact-SQL)](../../t-sql/statements/alter-login-transact-sql.md).

# [SQL Server Management Studio](#tab/ssms)

### Change the default database using SSMS

1. Connect to your SQL Server instance using SQL Server Management Studio (SSMS).

1. Select **Security** > **Logins** to locate the user and change user's default database to a different one that's currently available in the **General** page of login's properties in SSMS. For more information, see [Create a Login](../security/authentication-access/create-a-login.md).

1. After connecting to the SQL Server instance, you can execute an `ALTER LOGIN` statement, like the following examples:

   `ALTER LOGIN <LoginName> WITH DEFAULT_DATABASE = <AvailableDatabaseName>;`

   `<AvailableDatabaseName>` is a placeholder for the name of the existing database that can be accessed by the SQL Server login in the instance. `<LoginName>` is a placeholder for the SQL Server login with necessary `ALTER LOGIN` permissions.

   For example:

   ```sql
   ALTER LOGIN [SQLLogin] WITH DEFAULT_DATABASE = master;
   ALTER LOGIN [Constoso\Windowslogin] WITH DEFAULT_DATABASE = [AdventureWorks];
   ```

# [sqlcmd utility](#tab/sqlcmd)

### Change the default database using the sqlcmd utility

You can use the following procedure to change the default database using the [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md):

1. On the computer running SQL Server, select **Start**, then select **Run**, enter `cmd`, and then press **Enter**.

1. Connect to SQL Server using the **sqlcmd** utility and one of the following options:

   - If you want to use Windows authentication to connect to the instance using the Windows user account currently logged on, enter the following command in the **Command Prompt** window, and then press **Enter**:

     `sqlcmd -E -S <InstanceName> -d master`

     For example:

     ```cmd
     sqlcmd -S "contoso\sql22" -E -d master
     ```

   - If you want to use SQL Server authentication to connect to the instance, enter the following command in the **Command Prompt** window, and then press **Enter**:

     `sqlcmd -S <InstanceName> -d master -U <SQLAdminAccount> -P <Password>`

     `<InstanceName>` is a placeholder for the name of the SQL Server instance to which you are connecting. `<Password>` is a placeholder for the SQL Server login password.

     For example:

     ```cmd
     sqlcmd -S contososql -U sqladmin -P <Strong password>
     ```

1. To change the default database of a login, at the **sqlcmd** prompt, use the following example, and then press **Enter**:

   `ALTER LOGIN <LoginName> WITH DEFAULT_DATABASE = <AvailableDatabaseName>;`

   `<AvailableDatabaseName>` is a placeholder for the name of the existing database that can be accessed by the SQL Server login in the instance. `<LoginName>` is a placeholder for the SQL Server login with necessary `ALTER LOGIN` permissions.

   For example:

   ```sql
   ALTER LOGIN [SQLLogin] WITH DEFAULT_DATABASE = master;
   ALTER LOGIN [Constoso\Windowslogin] WITH DEFAULT_DATABASE = [AdventureWorks];
   ```

1. At the **sqlcmd** prompt, enter `GO`, and then press **Enter**.

   For more information about the **sqlcmd** utility, see [sqlcmd - Use the utility](../../tools/sqlcmd/sqlcmd-use-utility.md).

# [PowerShell](#tab/powershell)

### Change the default database using PowerShell

You can use the following PowerShell script to update the default database for a specified login.

```powershell
# Define the variables for the SQL Server instance name and login name

$instanceName = "YOUR_SQL_SERVER_INSTANCE_NAME"
$loginName = "YOUR_SQL_SERVER_LOGIN_NAME"
$newDefaultDB = "YOUR_NEW_DEFAULT_DATABASE"

# Connect to the SQL Server instance

$connectionString = "Server=$instanceName;Integrated Security=True"
$connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
$connection.Open()

# Execute a T-SQL command to change the default database for the specified login

$tsql = "ALTER LOGIN $loginName WITH DEFAULT_DATABASE = $newDefaultDB"
$command = New-Object System.Data.SqlClient.SqlCommand($tsql, $connection)
$command.ExecuteNonQuery()

# Print a message indicating the update was successful
 
Write-Host "The default database for the login '$loginName' has been successfully updated to '$newDefaultDB'"
 
# Close the SQL Server connection

$connection.Close()
```

---

## Error 18456 is displayed along with error 4064

When using applications like SSMS that get error 4064 displayed to the user, the following message is logged in the SQL Server error log. This behavior is by design. Fixing the default database for the failed login, using the procedures documented in this article, automatically addresses the 18456 error.

```output
2023-02-06 18:17:02.56 Logon       Error: 18456, Severity: 14, State: 40.
2023-02-06 18:17:02.56 Logon       Login failed for user '<user name>. Reason: Failed to open the database '<db_name>' specified in the login properties. [CLIENT: <hostname >]
```

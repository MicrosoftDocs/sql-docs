---
title: "MSSQLSERVER_15517"
description: "MSSQLSERVER_15517"
author: MashaMSFT
ms.author: mathoma
ms.date: "11/10/2023"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "15517 (Database Engine error)"
---

# MSSQLSERVER_15517
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details  
  
| Attribute | Value |
| :-------- | :---- |
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|
|Event ID|15517|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|SEC_CANNOTEXECUTEASUSER|
|Message Text|Cannot execute as the database principal because the principal "*principal*" does not exist, this type of principal can't be impersonated, or you do not have permission.|  
  
## Explanation

This error generally occurs because SQL Server can't obtain the information about the execution context of the principal that's specified in a user statement or module using the `EXECUTE AS` statement.

Your login information sid (Security identifier) is automatically saved when you create a database on a SQL Server instance as the database owner in the corresponding database row in the `sys.databases` table and for the `dbo` user item in the `sys.database_principals` table within the database.  

The statements or modules that use `EXECUTE AS OWNER` clause will work fine as long as the sid entry stored for dbo user is valid.

> [!NOTE]
> The issue can happen with any principal that's used in the `EXECUTE AS` statement and that doesn't exist on the server where the database is restored.

Here are a few common scenarios that can lead to this situation:

- You restore a database on the same server instance where the backup was originally taken but the SQL Server principal that created the database is no longer valid.

   - SQL Server authentication login might have been removed.
   - Windows authentication login might have left the company and is no longer a valid user in the Active Directory.

- You restore a database on a server that's different from the instance where the backup was originally taken but the SQL Server principal that created the database isn't a valid principal on the new server.

   - If the user is a SQL Server, the principal might exist on the target or destination server but the sid is different.
   - If the user is a Windows login, the Windows login doesn't exist on the target server or it's no longer valid.

The user or application executing the stored procedure, function, or trigger doesn't have the necessary permissions to impersonate the principal specified in the `EXECUTE AS` statement.

## User Action

Use the name of an existing principal or grant the IMPERSONATE permission on that principal to the required users.  
  
To resolve the error caused due to issues with an invalid dbo user error, change the dbo_User to a valid login on your server, by running the following command:  
  
  ```sql
  ALTER AUTHORIZATION ON DATABASE:: DBName TO [NewLogin]  
  ```

## Example scenario

1. Create two temporary principals.

    ```sql
    CREATE LOGIN login1 WITH PASSWORD = 'J345#$)thb';
    CREATE LOGIN login2 WITH PASSWORD = 'Uor80$23b';
    ```

1. Add these two logins to the sysadmin role (for demo purposes only).
1. Log in to your SQL Server instance using login1.
1. Create a demo database and a stored procedure called `testexec` using the following script:

    ```sql
    CREATE DATABASE Demodb_15517
    GO
    USE Demodb_15517
    GO
    CREATE procedure [dbo].[testexec]
    WITH EXECUTE AS owner
    AS SELECT @@VERSION
    GO
    EXEC dbo.testexec
    GO
    ```

1. Run the following two queries and note that the `sid` values are resolving to a valid login.

    - Query 1: Check the value of owner name in sys.databases.

    ```sql
    SELECT NAME AS Database_Name, owner_sid, SUSER_SNAME(owner_sid) AS OwnerName
    FROM sys.databases
    WHERE NAME = N'Demodb_15517';
    /* 
     Database_Name                                                                                                                    owner_sid                                                                                       OwnerName
    ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
    Demodb_15517                               0xDB79ED7B6731CF4E8DC7DF02871E3E36                                                             login1
    (1 row affected)
    */ 
    ```

    - Query 2: Check the value of owner name in the `sys.database_principals` table within the demo database.

    ```sql
    SELECT SUSER_SNAME(sid) AS Owner_Name, sid 
    FROM Demodb_15517.sys.database_principals 
    WHERE NAME = N'dbo'; 
    /* 
    Owner_Name                                                                                     sid
    ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
    login1                                                                                        0xDB79ED7B6731CF4E8DC7DF02871E3E36
    (1 row affected)
    */
    ```

1. Back up your demo database using a query that resembles the following script:

```sql
BACKUP DATABASE [Demodb_15517] TO DISK = N'C:\SQLBackups\Demodb_15517.bak' WITH NOFORMAT, NOINIT, NAME = N'Demodb_15517 Full backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10 
GO 
```

1. Drop the demo database and login1.

```sql
DROP DATABASE demodb_15517
GO
DROP login login1
GO
```

1. Log in to SQL Server as login2.

1. Restore the demo database from backup using a statement that resembles the following script:

```sql
USE [master] 
RESTORE DATABASE [Demodb_15517] FROM   
DISK = N'C:\SQLBackups\Demodb_15517.bak' WITH FILE = 1,   
MOVE N'Demodb_15517' TO N'C:\SQLBackups\Demodb_15517.mdf',   
MOVE N'Demodb_15517_log' TO N'C:\SQLBackups\\Demodb_155172_log.ldf',   
NOUNLOAD, STATS = 5 
GO 
```

1. Now re-run Query 1 and Query 2.

1. In Query 1, check the value of owner name in `sys.databases`. The new owner name now reflects login2.

```sql
SELECT NAME AS Database_Name, owner_sid, SUSER_SNAME(owner_sid) AS OwnerName 
FROM sys.databases 
WHERE NAME = N'Demodb_15517'; 
/* 
Database_Name  owner_sid                                                                                      OwnerName
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Demodb_15517 0xD63086DD7277BC4EB88013D359AF73A6                                                             login2
(1 row affected)
*/ 
```

1. In Query 2, check the value of owner name in the `sys.database_principals` table within the demo database. The `owner_name` now reflects `NULL`.

```sql
SELECT SUSER_SNAME(sid) AS Owner_Name, sid
FROM Demodb_15517.sys.database_principals
WHERE NAME = N'dbo';
/* 
Owner_Name
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
sid 
NULL                                                                                           0xDB79ED7B6731CF4E8DC7DF02871E3E36 
(1 row affected) 
*/ 
```

1. Now, if you run the `testexec` stored procedure, you see the 15517 error message.

```sql
USE Demodb_15517 
GO 
EXEC dbo.testexec 
GO 
/* 
Msg 15517, Level 16, State 1, Procedure dbo.testexec, Line 0 [Batch Start Line 19] 
Cannot execute as the database principal because the principal "dbo" does not exist, this type of principal cannot be impersonated, or you do not have permission. 
*/ 
```

1. To resolve the error, change the dbo to a valid user (login2) using the following statement:

```sql
ALTER AUTHORIZATION ON DATABASE:: Demodb_15517 TO [login2]   
```

1. Rerun Query 2 and verify that dbo users now resolve to the login2 user.

```sql
/* 
Owner_Name                                                                                     sid
-------------------------------------------------------------------------------------------------------------------------------- ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
login2                                                                                         0xD63086DD7277BC4EB88013D359AF73A6 
(1 row affected) 
*/ 
```

1. Now, retry running the test stored procedure and notice that this time it executes successfully.

```sql
USE Demodb_15517 
GO 
EXEC dbo.testexec 
GO 
/* -- You get an output similar to the following 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
Microsoft SQL Server 2019 (RTM-CU16-GDR) (KB5014353) - 15.0.4236.7 (X64)  
May 29 2022 15:55:47  
Copyright (C) 2019 Microsoft Corporation 
Express Edition (64-bit) on Windows 10 Enterprise 10.0 <X64> (Build 22621: ) (Hypervisor) 
(1 row affected) 
*/ 
```

## See Also

[Copy Databases to Other Servers](../databases/copy-databases-to-other-servers.md)

[Transfer logins and passwords between instances](/troubleshoot/sql/database-engine/security/transfer-logins-passwords-between-instances)
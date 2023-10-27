---
title: "MSSQLSERVER_15517"
description: "MSSQLSERVER_15517"
author: MashaMSFT
ms.author: mathoma
ms.date: "10/18/2023"
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

This error typically occurs because Microsoft SQL Server can't get the information about the execution context of the principal that's specified in a user statement or module by using the `EXECUTE AS` statement.

Your login information Security Identifier (SID) is automatically saved when you create a database on a SQL Server instance as the database owner in the corresponding database row in the `sys.databases` table and for the `dbo` user item in the `sys.database_principals` table within the database.  

The statements or modules that use the `EXECUTE AS OWNER` clause will work as expected if the SID entry that's stored for the dbo user is valid.

> [!NOTE]
> The issue can occur for any principal that's used in the `EXECUTE AS` statement and that doesn't exist on the server to which the database is restored.

Here are a few common scenarios that might cause this issue to occur:

- You restore a database on the same server instance where the backup was originally taken, but the SQL Server principal that created the database is no longer valid for some reason. For example:

   - The SQL Server authentication login was removed.
   - The Windows authentication login is no longer for a valid user in Active Directory because the employee left the company.

- You restore a database on a server that's different from the instance where the backup was originally taken, but the SQL Server principal that created the database isn't a valid principal on the new server.

   - If the user is a SQL Server login, the principal might exist on the target or destination server but the `sid` value will be different.
   - If the user is a Windows login, the Windows login doesn't exist on the target server or it's no longer valid.

The user or application that's running the stored procedure, function, or trigger doesn't have the necessary permissions to impersonate the principal that's specified in the `EXECUTE AS` statement.

## User action

Use the name of an existing principal or grant the IMPERSONATE permission on that principal to the required users.  
  
To resolve the issue that occurs because of an invalid dbo user error, change the `dbo_User` value to a valid login on your server by running the following command:  
  
  ```sql
  ALTER AUTHORIZATION ON DATABASE:: DBName TO [NewLogin]  
  ```

## Example scenario

1. Create two temporary principals:

    ```sql
    CREATE LOGIN login1 WITH PASSWORD = 'J345#$)thb';
    CREATE LOGIN login2 WITH PASSWORD = 'Uor80$23b';
    ```

1. Add these logins to the sysadmin role (for demonstration only).
1. Log in to your SQL Server instance by using `login1`.
1. Create a demonstration database and a stored procedure that's named `testexec` by running the following script:

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

1. Run the following queries, and check whether the `sid` values are resolving to a valid login:

   - Query 1: Check the value of the `Owner_Name` value in sys.databases.

     ```sql
     SELECT NAME AS Database_Name, owner_sid, SUSER_SNAME(owner_sid) AS OwnerName
     FROM sys.databases
     WHERE NAME = N'Demodb_15517';
     ```

     ```output
     Database_Name         owner_sid                               OwnerName
     --------------------- -------------------------------------- ----------------------------
     Demodb_15517          0xDB79ED7B6731CF4E8DC7DF02871E3E36      login1
     ```

   - Query 2: Check the `Owner_Name` value in the `sys.database_principals` table within the demonstration database:

     ```sql
     SELECT SUSER_SNAME(sid) AS Owner_Name, sid 
     FROM Demodb_15517.sys.database_principals 
     WHERE NAME = N'dbo'; 
     ```

     ```output
     Owner_Name    SID
     ------------- ------------------------------------------------ 
     login1        0xDB79ED7B6731CF4E8DC7DF02871E3E36
     ```

1. Back up the demonstration database by using a query that resembles the following script:

   ```sql
   BACKUP DATABASE [Demodb_15517] TO DISK = N'C:\SQLBackups\Demodb_15517.bak' WITH NOFORMAT, NOINIT, NAME = N'Demodb_15517 Full backup', SKIP, EWIND, NOUNLOAD, STATS = 10 
   GO
   ```

1. Drop the demonstration database and `login1`:

   ```sql
   DROP DATABASE demodb_15517
   GO
   DROP login login1
   GO
   ```

1. Log in to SQL Server as `login2`.

1. Restore the demonstration database from the backup by using a statement that resembles the following script:

   ```sql
   USE [master] 
   RESTORE DATABASE [Demodb_15517] FROM   
   DISK = N'C:\SQLBackups\Demodb_15517.bak' WITH FILE = 1,   
   MOVE N'Demodb_15517' TO N'C:\SQLBackups\Demodb_15517.mdf',   
   MOVE N'Demodb_15517_log' TO N'C:\SQLBackups\\Demodb_155172_log.ldf',   
   NOUNLOAD, STATS = 5 
   GO 
   ```

1. Rerun Query 1 and Query 2.

1. In Query 1, check the value of the `Owner_Name` value in `sys.databases`. The value now reflects `login2`.

   ```sql
   SELECT NAME AS Database_Name, owner_sid, SUSER_SNAME(owner_sid) AS OwnerName 
   FROM sys.databases 
   WHERE NAME = N'Demodb_15517';
   ```

   ```output
   Database_Name  owner_sid                               OwnerName
   -------------- --------------------------------------- ---------------------
   Demodb_15517   0xD63086DD7277BC4EB88013D359AF73A6      login2
   ```

1. In Query 2, check the value of the `Owner_Name` value in the `sys.database_principals` table within the demonstration database. The value now reflects `NULL`.

   ```sql
   SELECT SUSER_SNAME(sid) AS Owner_Name, sid
   FROM Demodb_15517.sys.database_principals
   WHERE NAME = N'dbo';
   ```

   ```output
   Owner_Name         sid 
	 ------------------ -----------------------------------------------
   NULL               0xDB79ED7B6731CF4E8DC7DF02871E3E36 
   ```

1. Run the `testexec` stored procedure. You should now see the "15517" error message.

   ```sql
   USE Demodb_15517 
   GO 
   EXEC dbo.testexec 
   GO
   ```

   ```output
   Msg 15517, Level 16, State 1, Procedure dbo.testexec, Line 0 [Batch Start Line 19] 
   Cannot execute as the database principal because the principal "dbo" does not exist, this type of principal cannot be impersonated, or you do not have permission. 
   ```

1. To resolve the error, change the dbo to a valid user (`login2`) by using the following command:

   ```sql
   ALTER AUTHORIZATION ON DATABASE:: Demodb_15517 TO [login2]   
   ```

1. Rerun Query 2 and verify that dbo users now resolve to the login2 user.

   ```output
   Owner_Name          SID
   ---------------------------------------------------------------- 
   login2              0xD63086DD7277BC4EB88013D359AF73A6 
   ```

1. Try again to run the test stored procedure. Notice that it now runs successfully.

   ```sql
   USE Demodb_15517 
   GO 
   EXEC dbo.testexec 
   GO
   ```

   ```output
   /* -- You get an output that resembles the following 
   ---------------------------------------------------------------------------------------------------------
   Microsoft SQL Server 2019 (RTM-CU16-GDR) (KB5014353) - 15.0.4236.7 (X64)  
   May 29 2022 15:55:47  
   Copyright (C) 2019 Microsoft Corporation 
   Express Edition (64-bit) on Windows 10 Enterprise 10.0 <X64> (Build 22621: ) (Hypervisor) 
   */ 
   ```

## See Also

[Copy databases to other servers](../databases/copy-databases-to-other-servers.md)

[Transfer logins and passwords between instances](/troubleshoot/sql/database-engine/security/transfer-logins-passwords-between-instances)

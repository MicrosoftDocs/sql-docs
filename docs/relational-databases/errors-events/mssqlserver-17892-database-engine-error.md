---
description: "MSSQLSERVER_17892"
title: MSSQLSERVER_17892
ms.custom: ""
ms.date: 08/20/2020
ms.prod: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "17892 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni,sureshka
---
# MSSQLSERVER_17892

 [!INCLUDE [SQL Server](../../includes/ssnoversion-md.md)]
 [!INCLUDE [SQL Server 2019](../../includes/sssqlv15-md.md)]
 [!INCLUDE [SQL Server 2017](../../includes/sssql17-md.md)]
 [!INCLUDE [SQL Server 2016](../../includes/sssql15-md.md)]
 [!INCLUDE [SQL Server 2014](../../includes/sssql14-md.md)]
 [!INCLUDE [SQL Server 2012](../../includes/sssql11-md.md)]
 [!INCLUDE [SQL Server 2008](../../includes/sskatmai-md.md)]
 [!INCLUDE [Azure SQL DB](../../includes/sssdsfull-md.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|17892|
|Event Source|MSSQLSERVER|
|Component|SQL SQLEngine|
|Symbolic Name|SRV_LOGON_FAILED_BY_TRIGGER|
|Message Text|Logon failed for login \<Login Name> due to trigger execution.|
||

## Explanation

Error 17892 is raised when a logon trigger code cannot execute successfully. [Logon Triggers](/sql/relational-databases/triggers/logon-triggers) fire stored procedures in response to a LOGON event. This event is raised when a user session is established with an instance of SQL Server. An error message like the following is reported to the user:

> Msg 17892, Level 14, State 1, Server \<Server Name>, Line 1  
Logon failed for login \<Login Name> due to trigger execution.

## Possible causes

The problem could occur if there is an error when executing trigger code for that specific user account. Some of the scenarios include:

- The trigger tires to insert data into a table that does not exist.
- The login does not have permissions to the object that is referred to by the logon trigger.

## User action

You can use one of the resolutions below depending on the scenario you are in.

- **Scenario 1**: You currently have access to an open session to SQL server under an admin account

  In this case, you can take the corrective action that is needed to fix your trigger code.

  - Example 1: If an object referred to by the trigger code does not exist, create that object so that the login trigger can execute successfully.

  - Example 2: If an object referred to by the trigger code does exist but users do not have permissions, grant them the necessary privileges to access the object.  
  
  Alternatively, you can just drop or disable the login trigger so that users can continue to log in to SQL Server.  

- **Scenario 2**: You do not have any current session that is open under admin privileges, but Dedicated Administrator Connection (DAC) is enabled on the SQL Server.

    In this case, you can use the DAC connection to take the same steps as discussed in Case 1 since DAC connections are not affected by Login triggers. For more information on DAC connection, see:
    [Diagnostic Connection for Database Administrators](/sql/database-engine/configure-windows/diagnostic-connection-for-database-administrators).

    To check whether DAC is enabled on your SQL Server, you can check SQL Server error log for a message that is similar to the following:

    > 2020-02-09 16:17:44.150 Server Dedicated admin connection support was established for listening locally on port 1434.  

- **Scenario 3**: You neither have DAC enabled on your server nor have an existing admin session to SQL Server.

    In this scenario, the only way to remediate the problem would be to take the following steps:
  
    1. Stop SQL Server and related services.
    2. Start SQL Server from the [command prompt](/previous-versions/sql/sql-server-2008-r2/ms180965(v=sql.105)) using the startup parameters `-c`, `-m` and `-f`. Doing this disables the login trigger and lets you perform the same remedial measures that are discussed under **Case 1** above.
  
        > [!NOTE]
        > The above procedure requires a *SA* or an equivalent administrator account.
  
         For more information about these and other startup options, see: [Database Engine Service Startup Options](/sql/database-engine/configure-windows/database-engine-service-startup-options).

## More information

Another situation where logon triggers fail is when using the `EVENTDATA` function. This function returns XML, and its case sensitive.  So, you create the following logon trigger, intending to block access based on IP address, you can  ran into the issue:

``` sql
 CREATE TRIGGER tr_logon_CheckIP  
 ON ALL SERVER  
 FOR LOGON  
 AS
 BEGIN
  IF IS_SRVROLEMEMBER ( 'sysadmin' ) = 1  
     BEGIN
         DECLARE @IP NVARCHAR ( 15 );  
         SET @IP = ( SELECT EVENTDATA ().value ( '(/EVENT_INSTANCE/ClientHost)[1]' , 'NVARCHAR(15)' ));  
         IF NOT EXISTS( SELECT IP FROM DBAWork.dbo.ValidIP WHERE IP = @IP )  
         ROLLBACK ;  
     END ;  
 END ;  
 GO
```

User didn't maintain case when copying this from the internet on this part of the trigger:

```sql
 SELECT EVENTDATA ().value ( '(/event_instance/clienthost)[1]' , 'NVARCHAR(15)' ));  
```

As a consequence, `EVENTDATA` always returned **NULL**, and all his SA equivalent logins were denied access. In this case the DAC connection was not enabled, so we had no choice but to restart the server with the startup parameters listed above to drop the trigger.

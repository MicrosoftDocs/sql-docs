---
title: Configure the user connections (server configuration option)
description: "Find out about the user connections option. See how it can help you avoid overloading an instance of SQL Server with too many concurrent connections."
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "simultaneous connections [SQL Server]"
  - "user connections option [SQL Server]"
  - "users [SQL Server], simultaneous connections"
  - "maximum number of simultaneous user connections"
  - "connections [SQL Server], simultaneous"
---

# Configure the user connections (server configuration option)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to set the **user connections** server configuration option in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The **user connections** option specifies the maximum number of simultaneous user connections that are allowed on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The actual number of user connections allowed also depends on the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you are using, and also the limits of your application or applications and hardware. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows a maximum of 32,767 user connections. Because **user connections** is a dynamic (self-configuring) option, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] adjust the maximum number of user connections automatically as needed, up to the maximum value allowable. For example, if only 10 users are logged in, each with one connection, 10 user connection objects are allocated. The same would happen in the case of a single user establishing 10 connections. In most cases, you do not have to change the value for this option. The default is 0, which means that the maximum (32,767) user connections are allowed.  
  
To determine the maximum number of user connections that your system allows, you can execute [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) or query the [sys.configuration](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) catalog view.  
  
**In This Topic**  
  
- **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
- **To configure the user connections option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
- **Follow Up:**  [After you configure the user connections option](#FollowUp)
  
## <a name="BeforeYouBegin"></a> Before You Begin  
  
### <a name="Recommendations"></a> Recommendations  
  
- This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] professional.  
  
- Using the **user connections** option helps avoid overloading the server with too many concurrent connections. You can estimate the number of connections based on system and user requirements. For example, on a system with many users, each user wouldn't usually require a unique connection. Connections can be shared among users. Users running OLE DB applications need a connection for each open connection object, users running Open Database Connectivity (ODBC) applications need a connection for each active connection handle in the application, and users running DB-Library applications need one connection for each process started that calls the DB-Library **dbopen** function.  
  
   > [!IMPORTANT]  
   >  If you must use this option, do not set the value too high, because each connection has overhead regardless of whether the connection is being used. If you exceed the maximum number of user connections, you receive an error message and are not able to connect until another connection becomes available.  
  
### <a name="Security"></a> Security  
  
#### <a name="Permissions"></a> Permissions  
Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
## <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the user connections option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Select the **Connections** node.  
  
3.  Under **Connections**, in the **Max number of concurrent connections** box, type or select a value from 0 through 32767 to set the maximum number of users that are allowed to connect simultaneously to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
4.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

## <a name="SSMSProcedure"></a> Using Azure Data Studio  
  
#### To configure the user connections option  
  
1.  In the Connections pane under servers, right-click a server and Select **Properties**.  
  
2.  Select the **Connections** node.  
  
3.  Under **Connections**, in the **Max number of concurrent connections** box, type or select a value from 0 through 32767 to set the maximum number of users that are allowed to connect simultaneously to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
4.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
  
## <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the user connections option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the value of the `user connections` option to `325` connections.  
  
    ```sql  
    USE master;  
    GO  
    EXEC sp_configure 'show advanced options', 1;  
    GO  
    RECONFIGURE ;  
    GO  
    EXEC sp_configure 'user connections', 325 ;  
    GO  
    RECONFIGURE;  
    GO
    EXEC sp_configure 'show advanced options', 0;  
    GO  
    RECONFIGURE ;  
    GO 
    ```

4. Restart the SQL Server instance for the new value to take effect.

  
For more information, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
## <a name="FollowUp"></a> Follow Up: After you configure the user connections option  

The SQL instance must be restarted before the setting can take effect.  
  
## See Also  
- [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
- [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
- [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  

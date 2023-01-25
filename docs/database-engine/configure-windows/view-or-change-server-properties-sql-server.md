---
title: View or Change Server Properties (SQL Server)
description: Learn how to use SQL Server Management Studio, Transact-SQL, or SQL Server Configuration Manager to view or change the properties of an instance of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/14/2017
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
ms.custom: contperf-fy20q4
f1_keywords:
  - "sql13.swb.connectionproperties.f1"
helpviewer_keywords:
  - "viewing server properties"
  - "server properties [SQL Server]"
  - "displaying server properties"
  - "servers [SQL Server], viewing"
  - "Connection Properties dialog box"
---

# View or Change Server Properties (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to view or change the properties of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or SQL Server Configuration Manager.  

Steps depend on the tool:
+ [SQL Server Management Studio](#SSMSProcedure)  
+ [Transact-SQL](#TsqlProcedure)  
+ [SQL Server Configuration Manager](#PowerShellProcedure)  
    
## <a name="Restrictions"></a> Limitations and restrictions  
  
-   When using sp_configure, you must run either RECONFIGURE or RECONFIGURE WITH OVERRIDE after setting a configuration option. The RECONFIGURE WITH OVERRIDE statement is usually reserved for configuration options that should be used with extreme caution. However, RECONFIGURE WITH OVERRIDE works for all configuration options, and you can use it in place of RECONFIGURE.  
  
    > [!NOTE]  
    >  RECONFIGURE executes within a transaction. If any of the reconfigure operations fail, none of the reconfigure operations will take effect.  
  
-   Some property pages present information obtained via Windows Management Instrumentation (WMI). To display those pages, WMI must be installed on the computer running [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
## <a name="Security"></a> Server-Level roles  
  
For more information, see [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md).  
  
Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
## <a name="SSMSProcedure"></a>SQL Server Management Studio  
  
#### To view or change server properties  
  
1.  In Object Explorer, right-click a server, and then click **Properties**.  
  
2.  In the **Server Properties** dialog box, click a page to view or change server information about that page. Some properties are read-only.  
  
##  <a name="TsqlProcedure"></a>Transact-SQL  
  
### To view server properties by using the SERVERPROPERTY built-in function  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example uses the [SERVERPROPERTY](../../t-sql/functions/serverproperty-transact-sql.md) built-in function in a `SELECT` statement to return information about the current server. This scenario is useful when there are multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed on a Windows-based server, and the client must open another connection to the same instance that is used by the current connection.  
  
    ```sql  
    SELECT CONVERT( sysname, SERVERPROPERTY('servername'));  
    GO  
    ```  
  
### To view server properties by using the sys.servers catalog view  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example queries the [sys.servers](../../relational-databases/system-catalog-views/sys-servers-transact-sql.md) catalog view to return the name (`name`) and ID (`server_id`) of the current server, and the name of the OLE DB provider (`provider`) for connecting to a linked server.  
  
    ```sql  
    USE AdventureWorks2012;   
    GO  
    SELECT name, server_id, provider  
    FROM sys.servers ;   
    GO  
  
    ```  
  
### To view server properties by using the sys.configurations catalog view  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example queries the [sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) catalog view to return information about each server configuration option on the current server. The example returns the name (`name`) and description (`description`) of the option, its value (`value`), and whether the option is an advanced option (`is_advanced`).  
  
    ```sql   
    SELECT name, description, value, is_advanced  
    FROM sys.configurations;   
    GO  
  
    ```  
  
### To change a server property by using sp_configure  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to change a server property. The example changes the value of the `fill factor` option to `100`. The server must be restarted before the change can take effect.  
  
```sql  
sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
sp_configure 'fill factor', 100;  
GO  
RECONFIGURE;  
GO  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
## <a name="PowerShellProcedure"></a>SQL Server Configuration Manager  
 Some server properties can be viewed or changed by using SQL Server Configuration Manager. For example, you can view the version and edition of the instance of SQL Server, or change the location where error log files are stored. These properties can also be viewed by querying the [Server-Related Dynamic Management Views and Functions](../../relational-databases/system-dynamic-management-views/server-related-dynamic-management-views-and-functions-transact-sql.md).  
  
### To view or change server properties  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
2.  In **SQL Server Configuration Manager**, select **SQL Server Services**.  
  
3.  In the details pane, right-click **SQL Server (\<**_instancename_**>)**, and then select **Properties**.  
  
4.  In the **SQL Server (\<**_instancename_**>) Properties** dialog box, change the server properties on the **Service** tab or the **Advanced** tab, and then select **OK**.  
  
## <a name="FollowUp"></a>Restart after changes

For some properties, you may need to restart the server before the change can take effect.  
  
## Next steps  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)   
 [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)   
 [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [Server-Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/server-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  

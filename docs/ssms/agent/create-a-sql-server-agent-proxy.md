---
description: "Create a SQL Server Agent Proxy in SQL Server Management Studio"
title: "Create a SQL Server Agent Proxy"
ms.custom: seo-lt-2019
ms.date: "05/03/2022"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "proxies [SQL Server Agent], creating"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: ">= sql-server-2016"
---
# Create a SQL Server Agent proxy
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to create a SQL Server Agent proxy in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account defines a security context in which a job step can run. Each proxy corresponds to a security credential. To set permissions for a particular job step, create a proxy that has the required permissions for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent subsystem, and then assign that proxy to the job step.  

On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) or [SQL Agent job limitations in SQL Managed Instance](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-limitations-in-sql-managed-instance) for details.

## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   You must create a credential before you create a proxy if one is not already available.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxies use credentials to store information about Windows user accounts. The user specified in the credential must have "Access this computer from the network" permission (`SeNetworkLogonRight`) on the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent checks subsystem access for a proxy and gives access to the proxy each time the job step runs. If the proxy no longer has access to the subsystem, the job step fails. Otherwise, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent impersonates the user that is specified in the proxy and runs the job step.  For a list of proxy subsystems, see [sp_grant_proxy_to_subsystem](../../relational-databases/system-stored-procedures/sp-grant-proxy-to-subsystem-transact-sql.md).
  
-   Creation of a proxy does not change the permissions for the user that is specified in the credential for the proxy. For example, you can create a proxy for a user that does not have permission to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. In this case, job steps that use that proxy are unable to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   If the login for the user has access to the proxy, or the user belongs to any role with access to the proxy, the user can use the proxy in a job step.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
  
-   Only members of the **sysadmin** fixed server role have permission to create, modify, or delete proxy accounts. Users who are not members of the **sysadmin** fixed server role must be added to one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database to use proxies: **SQLAgentUserRole**, **SQLAgentReaderRole**, or **SQLAgentOperatorRole**.  
  
-   Requires **ALTER ANY CREDENTIAL** permission if creating a credential in addition to the proxy.  
  
## <a name="SSMSProcedure"></a>Use SQL Server Management Studio (SSMS)
  
#### To create a SQL Server Agent proxy  
  
1.  In **Object Explorer**, select the plus sign to expand the server where you want to create a proxy on SQL Server Agent.  
  
2.  Select the plus sign to expand **SQL Server Agent**.  
  
3.  Right-click the **Proxies** folder and select **New Proxy**.  
  
4.  On the **New Proxy Account** dialog box, on the **General** page, enter the name of the proxy account in the **Proxy name** box.  
  
5.  In the **Credential name** box, enter the name of the security credential that the proxy account will use.  
  
6.  In the **Description** box, enter a description for the proxy account  
  
7.  Under **Active to the following subsystems**, select the appropriate subsystem or subsystems for this proxy.  
  
8.  On the **Principals** page, add or remove logins or roles to grant or remove access to the proxy account.  
  
9. When finished, select **OK**.  
  
## <a name="TsqlProcedure"></a>Use Transact-SQL  
  
#### To create a SQL Server Agent proxy  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  The following script creates a credential called `CatalogApplicationCredential`, creates proxy `Catalog application proxy` and assigns the credential `CatalogApplicationCredential` to it, and grants the proxy access to the ActiveX Scripting subsystem. Copy and paste the following example into the query window and select **Execute**. 
  
    ```  
    -- creates credential CatalogApplicationCredential  
    USE msdb ;  
    GO  
    CREATE CREDENTIAL CatalogApplicationCredential WITH IDENTITY = 'REDMOND/TestUser',   
        SECRET = 'G3$1o)lkJ8HNd!';  
    GO  
    -- creates proxy "Catalog application proxy" and assigns
    -- the credential 'CatalogApplicationCredential' to it.  
    EXEC dbo.sp_add_proxy  
        @proxy_name = 'Catalog application proxy',  
        @enabled = 1,  
        @description = 'Maintenance tasks on catalog application.',  
        @credential_name = 'CatalogApplicationCredential' ;  
    GO  
    -- grants the proxy "Catalog application proxy" access to 
    -- the ActiveX Scripting subsystem.  
    EXEC dbo.sp_grant_proxy_to_subsystem  
        @proxy_name = N'Catalog application proxy',  
        @subsystem_id = 2 ;  
    GO  
    ```  
  
## Next steps
  
-   [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md)  

-   [sp_add_proxy (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-proxy-transact-sql.md)  

-   [sp_grant_proxy_to_subsystem (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-grant-proxy-to-subsystem-transact-sql.md)
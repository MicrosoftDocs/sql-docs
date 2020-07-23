---
title: "OLE DB connection manager | Microsoft Docs"
description: An OLE DB connection manager enables a package to connect to a data source by using an OLE DB provider.
ms.custom: ""
ms.date: "05/24/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.oledbconnection.f1"
helpviewer_keywords: 
  - "OLE DB connection manager"
  - "data sources [Integration Services], connections"
  - "connection managers [Integration Services], OLE DB"
  - "connections [Integration Services], OLE DB"
ms.assetid: 91e3622e-4b1a-439a-80c7-a00b90d66979
author: chugugrace
ms.author: chugu
---
# OLE DB connection manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


An OLE DB connection manager enables a package to connect to a data source by using an OLE DB provider. For example, an OLE DB connection manager that connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].    
    
> [!NOTE]
>  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client 11.0 OLEDB provider doesn't support the new connection string key words (MultiSubnetFailover=True) for multi-subnet failover clustering. For more information, see the [SQL Server Release Notes](https://go.microsoft.com/fwlink/?LinkId=247824).    
> 
> [!NOTE]
>  If the data source is [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Excel 2007 or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Access 2007, the data source requires a different data provider than earlier versions of Excel or Access. For more information, see [Connect to an Excel Workbook](../../integration-services/connection-manager/connect-to-an-excel-workbook.md) and [Connect to an Access Database](../../integration-services/connection-manager/connect-to-an-access-database.md).    
    
Several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] tasks and data flow components use an OLE DB connection manager. For example, the OLE DB source and OLE DB destination use this connection manager to extract and load data. The Execute SQL task can use this connection manager to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database to run queries.    
    
You can also use the OLE DB connection manager to access OLE DB data sources in custom tasks written in unmanaged code that uses a language such as C++.    
    
When you add an OLE DB connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that resolves to an OLE DB connection at runtime, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package.    
    
The `ConnectionManagerType` property of the connection manager is set to `OLEDB`.    
    
Configure the OLE DB connection manager in the following ways:    
    
-   Provide a specific connection string configured to meet the requirements of the selected provider.    
    
-   Depending on the provider, include the name of the data source to connect to.    
    
-   Provide security credentials as appropriate for the selected provider.    
    
-   Indicate whether the connection created from the connection manager is retained at runtime.    
    
## Log calls and troubleshoot connections    
 You can log the calls that the OLE DB connection manager makes to external data providers. You can then troubleshoot the connections that the OLE DB connection manager makes to external data sources. To log the calls that the OLE DB connection manager makes to external data providers, enable package logging, and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).    
    
## Configure the OLE DB connection manager    
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, or programmatically. For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Configure OLE DB Connection Manager](../../integration-services/connection-manager/configure-ole-db-connection-manager.md). For information about configuring a connection manager programmatically, see the documentation for **T:Microsoft.SqlServer.Dts.Runtime.ConnectionManager** class in the Developer Guide.    
    
### Configure OLE DB connection manager

Use the **Configure OLE DB Connection Manager** dialog box to add a connection to a data source. This connection can be new, or a copy of an existing connection.  
  
> [!NOTE]  
>  If the data source is [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Excel 2007, the data source requires a different connection manager than earlier versions of Excel. For more information, see [Connect to an Excel Workbook](../../integration-services/connection-manager/connect-to-an-excel-workbook.md).  
>   
>  If the data source is [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Access 2007, the data source requires a different OLE DB provider than earlier versions of Access. For more information, see [Connect to an Access Database](../../integration-services/connection-manager/connect-to-an-access-database.md).  
  
 To learn more about the OLE DB connection manager, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
#### Options  
 **Data connections**  
 Select an existing OLE DB data connection from the list.  
  
 **Data connection properties**  
 View properties and values for the selected OLE DB data connection.  
  
 **New**  
 Create an OLE DB data connection by using the **Connection Manager** dialog box.  
  
 **Delete**  
 Select a data connection, and then delete it by selecting **Delete**.  
  
#### Managed identities for Azure resources authentication
When running SSIS packages on [Azure-SSIS integration runtime in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/concepts-integration-runtime#azure-ssis-integration-runtime), use the [managed identity](https://docs.microsoft.com/azure/data-factory/connector-azure-sql-database#managed-identity) associated with your data factory for Azure SQL Database (or managed instance) authentication. The designated factory can access and copy data from or to your database by using this identity.

> [!NOTE]
>  When you use Azure Active Directory (Azure AD) authentication (including managed identity authentication) to connect to Azure SQL Database (or managed instance), you might encounter a problem related to package execution failure or unexpected behavior change. For more information, see [Azure AD features and limitations](https://docs.microsoft.com/azure/sql-database/sql-database-aad-authentication#azure-ad-features-and-limitations).

To use managed identity authentication for Azure SQL Database, follow these steps to configure your database:

1. [Provision an Azure Active Directory administrator](https://docs.microsoft.com/azure/sql-database/sql-database-aad-authentication-configure#provision-an-azure-active-directory-administrator-for-your-azure-sql-database-server) for your Azure SQL server on the Azure portal, if you haven't already done so. The Azure AD administrator can be an Azure AD user or Azure AD group. If you grant the group with managed identity an admin role, skip step 2 and 3. The administrator will have full access to the database.

1. [Create contained database users](https://docs.microsoft.com/azure/sql-database/sql-database-aad-authentication-configure#create-contained-database-users-in-your-database-mapped-to-azure-ad-identities) for the data factory managed identity. Connect to the database from or to which you want to copy data by using tools like SSMS, with an Azure AD identity that has at least ALTER ANY USER permission. Run the following T-SQL: 
    
    ```sql
    CREATE USER [your data factory name] FROM EXTERNAL PROVIDER;
    ```

1. Grant the data factory managed identity needed permissions, as you normally do for SQL users and others. Refer to [Database-Level Roles](https://docs.microsoft.com/sql/relational-databases/security/authentication-access/database-level-roles) for appropriate roles. Run the following code. For more options, see [this document](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-addrolemember-transact-sql).

    ```sql
    EXEC sp_addrolemember [role name], [your data factory name];
    ```

To use managed identity authentication for Azure SQL Database managed instance, follow these steps to configure your database:
    
1. [Provision an Azure Active Directory administrator](https://docs.microsoft.com/azure/sql-database/sql-database-aad-authentication-configure#provision-an-azure-active-directory-administrator-for-your-managed-instance) for your managed instance on the Azure portal, if you haven't already done so. The Azure AD administrator can be an Azure AD user or Azure AD group. If you grant the group with managed identity an admin role, skip step 2-4. The administrator will have full access to the database.

1. [Create logins](https://docs.microsoft.com/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-mi-current) for the data factory managed identity. In SQL Server Management Studio (SSMS), connect to your Managed Instance using a SQL Server account that is a **sysadmin**. In **master** database, run the following T-SQL:

    ```sql
    CREATE LOGIN [your data factory name] FROM EXTERNAL PROVIDER;
    ```

1. [Create contained database users](https://docs.microsoft.com/azure/sql-database/sql-database-aad-authentication-configure#create-contained-database-users-in-your-database-mapped-to-azure-ad-identities) for the data factory managed identity. Connect to the database from or to which you want to copy data, run the following T-SQL: 
  
    ```sql
    CREATE USER [your data factory name] FROM EXTERNAL PROVIDER;
    ```

1. Grant the data factory managed identity needed permissions as you normally do for SQL users and others. Run the following code. For more options, see [this document](https://docs.microsoft.com/sql/t-sql/statements/alter-role-transact-sql?view=azuresqldb-mi-current).

    ```sql
    ALTER ROLE [role name e.g., db_owner] ADD MEMBER [your data factory name];
    ```

Then configure OLE DB provider for the OLE DB connection manager. Here are the options to do this:
    
- **Configure at design time.** In SSIS Designer, double-click the OLE DB connection manager to open the **Connection Manager** window. In the **Provider** drop-down list, select [**Microsoft OLE DB Driver for SQL Server**](https://go.microsoft.com/fwlink/?linkid=871294).
    > [!NOTE]
    >  Other providers in the drop-down list might not support managed identity authentication.
    
- **Configure at runtime.** When you run the package via [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/integration-services/ssis-quickstart-run-ssms) or [Azure Data Factory Execute SSIS Package activity](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-ssis-activity), find the connection manager property **ConnectionString** for the OLE DB connection manager. Update the connection property `Provider` to `MSOLEDBSQL` (that is, Microsoft OLE DB Driver for SQL Server).
    ```vb
    Data Source=serverName;Initial Catalog=databaseName;Provider=MSOLEDBSQL;...
    ```

Finally, configure managed identity authentication for the OLE DB connection manager. Here are the options to do this:
    
- **Configure at design time.** In SSIS Designer, right-click the OLE DB connection manager, and select **Properties**. Update the property `ConnectUsingManagedIdentity` to `True`.
    > [!NOTE]
    >  Currently, the connection manager property `ConnectUsingManagedIdentity` doesn't take effect (indicating that managed identity authentication doesn't work) when you run SSIS package in SSIS Designer or [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server.

- **Configure at runtime.** When you run the package via SSMS or an **Execute SQL Package** activity, find the OLE DB connection manager, and update its property `ConnectUsingManagedIdentity` to `True`.
    > [!NOTE]
    >  In Azure-SSIS integration runtime, all other authentication methods (for example, integrated security and password) preconfigured on the OLE DB connection manager are overridden when managed identity authentication is used to establish a database connection.

> [!NOTE]
>  To configure managed identity authentication on existing packages, the preferred way is to rebuild your SSIS project with the [latest SSIS Designer](https://docs.microsoft.com/sql/ssdt/download-sql-server-data-tools-ssdt) at least once. Redeploy that SSIS project to your Azure-SSIS integration runtime, so that the new connection manager property `ConnectUsingManagedIdentity` is automatically added to all OLE DB connection managers in your SSIS project. The alternative way is to directly use a property override with property path **\Package.Connections[{the name of your connection manager}].Properties[ConnectUsingManagedIdentity]** at runtime.

## See also    
 [OLE DB Source](../../integration-services/data-flow/ole-db-source.md)     
 [OLE DB Destination](../../integration-services/data-flow/ole-db-destination.md)     
 [Execute SQL Task](../../integration-services/control-flow/execute-sql-task.md)     
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)    
    
  

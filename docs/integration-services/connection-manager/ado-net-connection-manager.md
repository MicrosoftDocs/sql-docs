---
title: "ADO.NET connection manager | Microsoft Docs"
description: An ADO.NET connection manager enables a package to access data sources by using a .NET provider.
ms.custom: ""
ms.date: "05/24/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.adonetconnection.f1"
helpviewer_keywords: 
  - "connection managers [Integration Services], ADO.NET"
  - "ADO.NET connection manager [Integration Services]"
  - "connections [Integration Services], ADO.NET"
ms.assetid: fc5daa2f-0159-4bda-9402-c87f1035a96f
author: chugugrace
ms.author: chugu
---
# ADO.NET connection manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


An [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager enables a package to access data sources by using a .NET provider. Typically, you use this connection manager to access data sources such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can also access data sources exposed through OLE DB and XML in custom tasks that are written in managed code, by using a language such as C#.  
  
When you add an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to a package, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that is resolved as an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection at runtime. It sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package.  
  
The `ConnectionManagerType` property of the connection manager is set to `ADO.NET`. The value of `ConnectionManagerType` is qualified to include the name of the .NET provider that the connection manager uses.  
  
## ADO.NET connection manager troubleshooting  
You can log the calls that the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager makes to external data providers. You can then troubleshoot the connections that the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager makes to external data sources. To log the calls that the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager makes to external data providers, enable package logging, and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
When being read by an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager, data of certain [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date data types generates the results shown in the following table.  
  
|SQL Server data type|Result|  
|--------------------------|------------|  
|**time**, **datetimeoffset**|The package fails unless the package uses parameterized SQL commands. To use parameterized SQL commands, use the Execute SQL Task in your package. For more information, see [Execute SQL Task](../../integration-services/control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](https://msdn.microsoft.com/library/a3ca65e8-65cf-4272-9a81-765a706b8663).|  
|**datetime2**|The [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager truncates the millisecond value.|  
  
> [!NOTE]  
>  For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types and how they map to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md) and [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## ADO.NET connection manager configuration  
  
You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, or programmatically.  
  
-   Provide a specific connection string configured to meet the requirements of the selected .NET provider.  
  
-   Depending on the provider, include the name of the data source to connect to.  
  
-   Provide security credentials as appropriate for the selected provider.  
  
-   Indicate whether the connection created from the connection manager is retained at runtime.  
  
Many of configuration options of the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager depend on the .NET provider that the connection manager uses.  
  
For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Configure ADO.NET Connection Manager](../../integration-services/connection-manager/configure-ado-net-connection-manager.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
### Configure ADO.NET connection manager
Use the **Configure ADO.NET Connection Manager** dialog box to add a connection to a data source that can be accessed by using a .NET Framework data provider. For example, one such provider is the SqlClient provider. The connection manager can use an existing connection, or you can create a new one.  
  
 To learn more about the ADO.NET connection manager, see [ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md).  
  
#### Options  
**Data connections**  
Select an existing ADO.NET data connection from the list.  
  
**Data connection properties**  
View properties and values for the selected ADO.NET data connection.  
  
**New**  
Create an ADO.NET data connection by using the **Connection Manager** dialog box.  
  
**Delete**  
Select a connection, and then delete it by selecting **Delete**.  
  
#### Managed identities for Azure resources authentication
When running SSIS packages on [Azure-SSIS integration runtime in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/concepts-integration-runtime#azure-ssis-integration-runtime), you can use the [managed identity](https://docs.microsoft.com/azure/data-factory/connector-azure-sql-database#managed-identity) associated with your data factory for Azure SQL Database (or managed instance) authentication. The designated factory can access and copy data from or to your database by using this identity.

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

Finally, configure managed identity authentication for the ADO.NET connection manager. Here are the options to do this:
    
- **Configure at design time.** In SSIS Designer, right-click the ADO.NET connection manager, and select **Properties**. Update the property `ConnectUsingManagedIdentity` to `True`.
    > [!NOTE]
    >  Currently, the connection manager property `ConnectUsingManagedIdentity` doesn't take effect (indicating that managed identity authentication doesn't work) when you run SSIS package in SSIS Designer or [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server.
    
- **Configure at runtime.** When you run the package via [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/integration-services/ssis-quickstart-run-ssms) or [Azure Data Factory Execute SSIS Package activity](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-ssis-activity), find the ADO.NET connection manager. Update its property `ConnectUsingManagedIdentity` to `True`.
    > [!NOTE]
    >  In Azure-SSIS integration runtime, all other authentication methods (for example, integrated authentication and password) preconfigured on the ADO.NET connection manager are overridden when managed identity authentication is used to establish a database connection.

> [!NOTE]
>  To configure managed identity authentication on existing packages, the preferred way is to rebuild your SSIS project with the [latest SSIS Designer](https://docs.microsoft.com/sql/ssdt/download-sql-server-data-tools-ssdt) at least once. Redeploy that SSIS project to your Azure-SSIS integration runtime, so that the new connection manager property `ConnectUsingManagedIdentity` is automatically added to all ADO.NET connection managers in your SSIS project. The alternative way is to directly use a property override with property path **\Package.Connections[{the name of your connection manager}].Properties[ConnectUsingManagedIdentity]** at runtime.

## See also  
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  
  
  

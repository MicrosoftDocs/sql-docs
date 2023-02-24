---
title: "ADO.NET connection manager | Microsoft Docs"
description: An ADO.NET connection manager enables a package to access data sources by using a .NET provider.
ms.custom: ""
ms.date: "07/19/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
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
|**time**, **datetimeoffset**|The package fails unless the package uses parameterized SQL commands. To use parameterized SQL commands, use the Execute SQL Task in your package. For more information, see [Execute SQL Task](../../integration-services/control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](../control-flow/execute-sql-task.md).|  
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
  
For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Configure ADO.NET Connection Manager]().  
  
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
When running SSIS packages on [Azure-SSIS integration runtime (IR) in Azure Data Factory (ADF)](/azure/data-factory/concepts-integration-runtime#azure-ssis-integration-runtime), you can use Azure Active Directory (AAD) authentication with [the specified system/user-assigned managed identity for your ADF](/azure/data-factory/connector-azure-sql-database#managed-identity) to access Azure SQL Database server/Managed Instance. Your Azure-SSIS IR can access and copy data from or to your database by using this managed identity.

> [!NOTE]
>  When you use AAD authentication to access Azure SQL Database server/Managed Instance, you might encounter a problem related to package execution failure or unexpected behavior change. For more information, see [AAD features and limitations](/azure/sql-database/sql-database-aad-authentication#azure-ad-features-and-limitations).

To use AAD authentication with the specified system/user-assigned managed identity for your ADF to access Azure SQL Database server, follow these steps:

1. [Provision an AAD administrator](/azure/sql-database/sql-database-aad-authentication-configure#provision-an-azure-active-directory-administrator-for-your-azure-sql-database-server) for your Azure SQL Database server in Azure portal, if you haven't already done so. The AAD administrator can be an AAD user or group. If you grant the group with specified system/user-assigned managed identity for your ADF an admin role, skip step 2 - 3. The administrator will have full access to your Azure SQL Database server.

1. [Create a contained database user](/azure/sql-database/sql-database-aad-authentication-configure#create-contained-database-users-in-your-database-mapped-to-azure-ad-identities) to represent the specified system/user-assigned managed identity for your ADF. Connect to the database from or to which you want to copy data using SQL Server Management Studio (SSMS) with an AAD user that has at least ALTER ANY USER permission. Run the following T-SQL statement: 

   ```sql
   CREATE USER [your managed identity name] FROM EXTERNAL PROVIDER;
   ```

   If you use the system managed identity for your ADF, then *your managed identity name* should be your ADF name. If you use a user-assigned managed identity for your ADF, then *your managed identity name* should be the specified user-assigned managed identity name.

1. Grant the specified system/user-assigned managed identity for your ADF the required permissions, as you normally do for SQL users. Refer to [Database-level roles](../../relational-databases/security/authentication-access/database-level-roles.md) for appropriate roles. Run the following T-SQL statement. For more options, see [this article](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md).

   ```sql
   EXEC sp_addrolemember [role name], [your managed identity name];
   ```

To use AAD authentication with the specified system/user-assigned managed identity for your ADF to access Azure SQL Managed Instance, follow these steps:
    
1. [Provision an AAD administrator](/azure/sql-database/sql-database-aad-authentication-configure#provision-an-azure-active-directory-administrator-for-your-managed-instance) for your Azure SQL Managed Instance in Azure portal, if you haven't already done so. The AAD administrator can be an AAD user or group. If you grant the group with specified system/user-assigned managed identity for your ADF an admin role, skip step 2 - 4. The administrator will have full access to your Azure SQL Managed Instance.

1. [Create a login](../../t-sql/statements/create-login-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true) assigned to the specified system/user-assigned managed identity for your ADF. On SSMS, connect to your Azure SQL Managed Instance using SQL Server account that is a **sysadmin**. In **master** database, run the following T-SQL statement:

   ```sql
   CREATE LOGIN [your managed identity name] FROM EXTERNAL PROVIDER;
   ```

   If you use the system managed identity for your ADF, then *your managed identity name* should be your ADF name. If you use a user-assigned managed identity for your ADF, then *your managed identity name* should be the specified user-assigned managed identity name.

1. [Create a contained database user](/azure/sql-database/sql-database-aad-authentication-configure#create-contained-database-users-in-your-database-mapped-to-azure-ad-identities) representing the specified system/user-assigned managed identity for your ADF. Connect to the database from or to which you want to copy data using SSMS and run the following T-SQL statement:
  
   ```sql
   CREATE USER [your managed identity name] FROM EXTERNAL PROVIDER;
   ```

1. Grant the specified system/user-assigned managed identity for your ADF the required permissions, as you normally do for SQL users. Run the following T-SQL statement. For more options, see [this article](../../t-sql/statements/alter-role-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true).

   ```sql
   ALTER ROLE [role name e.g., db_owner] ADD MEMBER [your managed identity name];
   ```

Finally, you can configure AAD authentication with the specified system/user-assigned managed identity for your ADF on the ADO.NET connection manager. Here are the options to do this:
    
- **Configure at design time.** In SSIS Designer, right-click on your ADO.NET connection manager, and select **Properties**. Update the property `ConnectUsingManagedIdentity` to `True`.

  > [!NOTE]
  >  Currently, the connection manager property `ConnectUsingManagedIdentity` doesn't take effect (indicating that AAD authentication with the specified system/user-assigned managed identity for your ADF doesn't work) when you run your package in SSIS Designer or on SQL Server.

- **Configure at run time.** When you run your package via [SSMS](../ssis-quickstart-run-ssms.md) or [Execute SSIS Package activity in ADF pipeline](/azure/data-factory/how-to-invoke-ssis-package-ssis-activity), find the ADO.NET connection manager and update its property `ConnectUsingManagedIdentity` to `True`.

  > [!NOTE]
  > On Azure-SSIS IR, all other authentication methods (for example, integrated security and password) preconfigured on your ADO.NET connection manager are overridden when using AAD authentication with the specified system/user-assigned managed identity for your ADF.

To configure AAD authentication with the specified system/user-assigned managed identity for your ADF on your existing packages, the preferred way is to rebuild your SSIS project with the [latest SSIS Designer](../../ssdt/download-sql-server-data-tools-ssdt.md) at least once. Redeploy your SSIS project to run on Azure-SSIS IR, so that the new connection manager property `ConnectUsingManagedIdentity` is automatically added to all ADO.NET connection managers in your project. The alternative way is to directly use property overrides with the property path *\Package.Connections[{the name of your connection manager}].Properties[ConnectUsingManagedIdentity]* assigned to `True` at run time.

## See also
- [ADO.NET Source](../../integration-services/data-flow/ado-net-source.md)
- [ADO.NET Destination](../../integration-services/data-flow/ado-net-destination.md)
- [Integration Services (SSIS) Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)
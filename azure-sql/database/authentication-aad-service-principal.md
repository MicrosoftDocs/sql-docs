---
title: Microsoft Entra service principal with Azure SQL
description: Utilize AD Applications (service principals) support Microsoft Entra user creation in Azure SQL Database and Azure SQL Managed Instance
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 09/27/2023
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Microsoft Entra service principal with Azure SQL

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Microsoft Entra ID ([formerly Azure Active Directory](/azure/active-directory/fundamentals/new-name)) supports user creation in Azure SQL Database (SQL DB) on behalf of Microsoft Entra applications (service principals). This is supported for [Azure SQL Database](sql-database-paas-overview.md) and [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md).

<a name='service-principal-azure-ad-applications-support'></a>

## Service principal (Microsoft Entra applications) support

This article applies to applications that are integrated with Microsoft Entra ID, and are part of Microsoft Entra registration. These applications often need authentication and authorization access to Azure SQL to perform various tasks. This feature allows service principals to create Microsoft Entra users in SQL Database.

When a Microsoft Entra application is registered using the Azure portal or a PowerShell command, two objects are created in the Microsoft Entra tenant:

- An application object
- A service principal object

For more information on Microsoft Entra applications, see [Application and service principal objects in Microsoft Entra ID](/azure/active-directory/develop/app-objects-and-service-principals) and [Create an Azure service principal with Azure PowerShell](/powershell/azure/create-azure-service-principal-azureps).

SQL Database and SQL Managed Instance support the following Microsoft Entra objects:

- Microsoft Entra users (managed, federated, and guest)
- Microsoft Entra groups (managed and federated)
- Microsoft Entra applications 

Microsoft Entra users, groups, and applications can all be created on a database using the T-SQL command `CREATE USER [Azure_AD_Object] FROM EXTERNAL PROVIDER`.

<a name='functionality-of-azure-ad-user-creation-using-service-principals'></a>

## Functionality of Microsoft Entra user creation using service principals

Supporting this functionality is useful in Microsoft Entra application automation processes where Microsoft Entra objects are created and maintained in SQL Database without human interaction. Service principals can be a Microsoft Entra admin for the SQL logical server, as part of a group or an individual user. The application can automate Microsoft Entra object creation in SQL Database when executed as a system administrator, and does not require any additional SQL privileges. This allows for a full automation of a database user creation. This feature also supports Microsoft Entra system-assigned managed identity and user-assigned managed identity  that can be created as users in SQL Database on behalf of service principals. For more information, see [What are managed identities for Azure resources?](/azure/active-directory/managed-identities-azure-resources/overview)

<a name='enable-service-principals-to-create-azure-ad-users'></a>

## Enable service principals to create Microsoft Entra users

To enable Microsoft Entra object creation in SQL Database on behalf of a Microsoft Entra application, the following settings are required:

1. Assign the server identity. The assigned server identity represents the Managed Service Identity (MSI). The server identity can be a system-assigned or user-assigned managed identity. For more information, see [User-assigned managed identity in Microsoft Entra ID for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).
    - For a new Azure SQL logical server, execute the following PowerShell command:
    
    ```powershell
    New-AzSqlServer -ResourceGroupName <resource group> -Location <Location name> -ServerName <Server name> -ServerVersion "12.0" -SqlAdministratorCredentials (Get-Credential) -AssignIdentity
    ```

    For more information, see the [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) command, or [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) command for SQL Managed Instance.

    - For existing Azure SQL Logical servers, execute the following command:
    
    ```powershell
    Set-AzSqlServer -ResourceGroupName <resource group> -ServerName <Server name> -AssignIdentity
    ```

    For more information, see the [Set-AzSqlServer](/powershell/module/az.sql/set-azsqlserver) command, or [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance) command for SQL Managed Instance.

    - To check if the server identity is assigned to the server, execute the Get-AzSqlServer command.

    > [!NOTE]
    > Server identity can be assigned using REST API and CLI commands as well. For more information, see [az sql server create](/cli/azure/sql/server#az-sql-server-create), [az sql server update](/cli/azure/sql/server#az-sql-server-update), and [Servers - REST API](/rest/api/sql/2020-08-01-preview/servers).


2. Grant the Microsoft Entra ID [**Directory Readers**](/azure/active-directory/roles/permissions-reference#directory-readers) permission to the server identity created or assigned to the server.
    - To grant this permission, follow the description used for SQL Managed Instance that is available in the following article: [Provision Microsoft Entra admin (SQL Managed Instance)](authentication-aad-configure.md?tabs=azure-powershell#provision-azure-ad-admin-sql-managed-instance)
    - The Microsoft Entra user who is granting this permission must be part of the Microsoft Entra ID **Global Administrator** or **Privileged Roles Administrator** role.
    - For dedicated SQL pools in an Azure Synapse workspace, use the workspace's managed identity instead of the Azure SQL server identity.

> [!IMPORTANT]
> With [Microsoft Graph](/graph/overview) support for Azure SQL, the Directory Readers role can be replaced with using lower level permissions. For more information, see [User-assigned managed identity in Microsoft Entra ID for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md)
>
> Steps 1 and 2 must be executed in the above order. First, create or assign the server identity, followed by granting the [**Directory Readers**](/azure/active-directory/roles/permissions-reference#directory-readers) permission, or lower level permissions discussed in [User-assigned managed identity in Microsoft Entra ID for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md). Omitting one of these steps, or both will cause an execution error during Microsoft Entra object creation in Azure SQL on behalf of a Microsoft Entra application.
>
> You can assign the **Directory Readers** role to a group in Microsoft Entra ID. The group owners can then add the managed identity as a member of this group, which would bypass the need for a **Global Administrator** or **Privileged Roles Administrator** to grant the **Directory Readers** role. For more information on this feature, see [Directory Readers role in Microsoft Entra ID for Azure SQL](authentication-aad-directory-readers-role.md).

## Troubleshooting and limitations

- When creating Microsoft Entra objects in Azure SQL on behalf of a Microsoft Entra application without enabling server identity and granting **Directory Readers** permission, or lower level permissions discussed in [User-assigned managed identity in Microsoft Entra ID for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md), the operation will fail with the following possible errors. The following example error is for a PowerShell command execution to create a SQL Database user `myapp` in the article [Tutorial: Create Microsoft Entra users using Microsoft Entra applications](authentication-aad-service-principal-tutorial.md).
    - `Exception calling "ExecuteNonQuery" with "0" argument(s): "'myapp' is not a valid login or you do not have permission. Cannot find the user 'myapp', because it does not exist, or you do not have permission."`
    - `Exception calling "ExecuteNonQuery" with "0" argument(s): "Principal 'myapp' could not be resolved. Error message:
    'Server identity is not configured. Please follow the steps in "Assign a Microsoft Entra identity to your server and add
    Directory Reader permission to your identity" (https://aka.ms/sqlaadsetup)'"`
      - For the above error, follow the steps to [Assign an identity to the Azure SQL logical server](authentication-aad-service-principal-tutorial.md#assign-an-identity-to-the-azure-sql-logical-server) and [Assign Directory Readers permission to the SQL logical server identity](authentication-aad-service-principal-tutorial.md#assign-directory-readers-permission-to-the-sql-logical-server-identity).
    - Setting the service principal (Microsoft Entra application) as a Microsoft Entra admin for SQL Database is supported using the Azure portal, [PowerShell](authentication-aad-configure.md?tabs=azure-powershell#powershell-for-sql-database-and-azure-synapse), [REST API](/rest/api/sql/2020-08-01-preview/servers), and [CLI](authentication-aad-configure.md?tabs=azure-cli#powershell-for-sql-database-and-azure-synapse) commands.
- Using a Microsoft Entra application with service principal from another Microsoft Entra tenant will fail when accessing SQL Database or SQL Managed Instance created in a different tenant. A service principal assigned to this application must be from the same tenant as the SQL logical server or SQL Managed Instance.
- [Az.Sql 2.9.0](https://www.powershellgallery.com/packages/Az.Sql/2.9.0) module or higher is needed when using PowerShell to set up an individual Microsoft Entra application as Microsoft Entra admin for Azure SQL. Ensure you are upgraded to the latest module.

## Next steps

> [!div class="nextstepaction"]
> [Tutorial: Create Microsoft Entra users using Microsoft Entra applications](authentication-aad-service-principal-tutorial.md)

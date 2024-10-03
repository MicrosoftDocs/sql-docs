---
title: Microsoft Entra service principals with Azure SQL
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Use Microsoft Entra service principals and managed identities in Azure SQL Database and Azure SQL Managed Instance
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 09/16/2024
ms.service: azure-sql
ms.subservice: security
ms.topic: conceptual
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Microsoft Entra service principals with Azure SQL

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Azure SQL resources support programmatic access for applications using service principals and managed identities in Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)).

<a name='service-principal-azure-ad-applications-support'></a>

## Service principals (Microsoft Entra applications) support

This article applies to applications registered in Microsoft Entra ID. Using application credentials to access Azure SQL supports the security principle of Separation of Duties, enabling organizations to configure precise access for each application connecting to their databases. [Managed identities](/entra/identity/managed-identities-azure-resources/overview), a special form of service principals, are recommended as they're passwordless and eliminate the need for developer-managed credentials.

Microsoft Entra ID further enables advanced authentication scenarios like [OAuth 2.0 On-Behalf-Of Flow (OBO)](/entra/identity-platform/v2-oauth2-on-behalf-of-flow). OBO allows applications to request signed-in user credentials, for scenarios when applications themselves shouldn't be given database access without delegated permissions.

For more information on Microsoft Entra applications, see [Application and service principal objects in Microsoft Entra ID](/entra/identity-platform/app-objects-and-service-principals) and [Create an Azure service principal with Azure PowerShell](/powershell/azure/create-azure-service-principal-azureps).

<a name='functionality-of-azure-ad-user-creation-using-service-principals'></a>

## Microsoft Entra user creation using service principals

Supporting this functionality is useful in Microsoft Entra application automation processes where Microsoft Entra principals are created and maintained in SQL Database or SQL Managed Instance without human interaction. Service principals can be a Microsoft Entra admin for the SQL logical server or managed instance, as part of a group or as a standalone identity. The application can automate Microsoft Entra object creation in SQL Database or SQL Managed Instance, allowing full automation of database user creation.

<a name='enable-service-principals-to-create-azure-ad-users'></a>

## Enable service principals to create Microsoft Entra users

When using applications to access Azure SQL, creating Microsoft Entra users and logins requires permissions that aren't assigned to service principals or managed identities by default: the ability to read users, groups, and applications in a tenant from Microsoft Graph. These permissions are necessary for the SQL engine to validate the identity specified in `CREATE LOGIN` or `CREATE USER`, and pull important information including the identity's Object or Application ID, which is used to create the login or user.

When a Microsoft Entra user executes these commands, Azure SQL's [Microsoft application](/troubleshoot/azure/active-directory/verify-first-party-apps-sign-in#application-ids-of-commonly-used-microsoft-applications) uses delegated permissions to impersonate the signed-in user and queries Microsoft Graph using their permissions. This flow isn't possible with service principals, because an application can't impersonate another application. Instead, the SQL engine tries to use its server identity, which is the primary managed identity assigned to a SQL managed instance, Azure SQL logical server, or Azure Synapse workspace. The server identity must exist and have the Microsoft Graph query permissions or the operations fail.

The following steps explain how to assign a managed identity to the server and assign it the Microsoft Graph permissions to enable service principals to create Microsoft Entra users and logins in the database.

1. Assign the server identity. The server identity can be a system-assigned or user-assigned managed identity. For more information, see [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).

    - The following PowerShell command creates a new logical server provisioned with a system-assigned managed identity:

    ```powershell
    New-AzSqlServer -ResourceGroupName <resource group> -Location <Location name> -ServerName <Server name> -ServerVersion "12.0" -SqlAdministratorCredentials (Get-Credential) -AssignIdentity
    ```

    For more information, see the [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) command, or [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) command for SQL Managed Instance.

    - For an existing logical server, execute the following command to add a system-assigned managed identity to it:

    ```powershell
    Set-AzSqlServer -ResourceGroupName <resource group> -ServerName <Server name> -AssignIdentity
    ```

    For more information, see the [Set-AzSqlServer](/powershell/module/az.sql/set-azsqlserver) command, or [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance) command for SQL Managed Instance.

    - To check if the server identity is assigned to the server, execute the [Get-AzSqlServer](/powershell/module/az.sql/get-azsqlserver) command, or [Get-AzSqlInstance](/powershell/module/az.sql/get-azsqlinstance) command for SQL Managed Instance.

    > [!NOTE]  
    > The server identity can be assigned using REST API and CLI commands as well. For more information, see [az sql server create](/cli/azure/sql/server#az-sql-server-create), [az sql server update](/cli/azure/sql/server#az-sql-server-update), and [Servers - REST API](/rest/api/sql/servers).

1. Grant the server identity permissions to query Microsoft Graph. This can be done multiple ways: by adding the identity to the Microsoft Entra [**Directory Readers**](/entra/identity/role-based-access-control/permissions-reference#directory-readers) role, by assigning the identity the individual Microsoft Graph permissions, or by adding the identity to a role-assignable group that has the **Directory Readers** role:

    - Add server identity to a role-assignable group

        In production environments, it's recommended that a tenant administrator creates a [role-assignable group](/entra/identity/role-based-access-control/groups-concept) and assigns the **Directory Readers** role to it. Group owners can then add server identities to the group, inheriting those permissions. This removes the requirement for a **Global Administrator** or **Privileged Roles Administrator** to grant permissions to each individual server identity, allowing administrators to delegate permission assignment to owners of the group for this scenario. For more information, see [Directory Readers role in Microsoft Entra ID for Azure SQL](authentication-aad-directory-readers-role.md).

    - Assign Microsoft Graph permissions to server identity

        To assign the individual Microsoft Graph permissions to the server identity, you must have the Microsoft Entra **Global Administrator** or **Privileged Roles Administrator** role. This is recommended over assigning the **Directory Readers** role, because there are permissions included in the role that the server identity doesn't need. Assigning only the individual Microsoft Graph read permissions limits the server identity's permissions within your tenant and maintains the principle of least privilege. For instructions, see [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).

    - Add server identity to Directory Readers role

        To add the server identity to the **Directory Readers** role, you must be a member of the Microsoft Entra **Global Administrator** or **Privileged Roles Administrator** role. In production environments this option isn't recommended for two reasons: the Directory Reader role gives more permissions than the server identity requires, and the role assignment process still requires administrator approvals for each server identity (unlike using groups). Follow the SQL Managed Instance instructions available in the article [Set Microsoft Entra admin (SQL Managed Instance)](authentication-aad-configure.md?tabs=azure-powershell#provision-azure-ad-admin-sql-managed-instance).

## Troubleshooting

When troubleshooting, you might encounter the following error:

```output
Msg 33134, Level 16, State 1, Line 1
Principal 'test-user' could not be resolved.
Error message: 'Server identity is not configured. Please follow the steps in "Assign an Azure AD identity to your server and add Directory Reader permission to your identity" (https://aka.ms/sqlaadsetup)'
```

This error indicates that the server identity hasn't been created or hasn't been assigned Microsoft Graph permissions. Follow the steps to [Assign an identity to the logical server](authentication-aad-service-principal-tutorial.md#assign-an-identity-to-the-logical-server) and [Assign Directory Readers permission to the logical server identity](authentication-aad-service-principal-tutorial.md#add-server-identity-to-directory-readers-role).

## Limitations

- Service principals can't authenticate across tenants' boundaries. Trying to access SQL Database or SQL Managed Instance using a Microsoft Entra application created in a different tenant fails.

- [Az.Sql 2.9.0](https://www.powershellgallery.com/packages/Az.Sql/2.9.0) module or higher is required to set a Microsoft Entra application as the Microsoft Entra admin for Azure SQL. Ensure you're upgraded to the latest module.

## Related content

- [Tutorial: Create Microsoft Entra users using Microsoft Entra applications](authentication-aad-service-principal-tutorial.md)
- [Tutorial: Connect an App Service app to SQL Database on behalf of the signed-in user](/azure/app-service/tutorial-connect-app-access-sql-database-as-user-dotnet)

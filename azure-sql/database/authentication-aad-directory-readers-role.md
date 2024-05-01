---
title: Directory Readers role in Microsoft Entra ID for Azure SQL
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn about the directory reader's role in Microsoft Entra for Azure SQL.
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 09/27/2023
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
ms.custom: azure-synapse
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Directory Readers role in Microsoft Entra ID for Azure SQL

[!INCLUDE[appliesto-sqldb-sqlmi-asa](../includes/appliesto-sqldb-sqlmi-asa.md)]

Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) has introduced [using groups to manage role assignments](/azure/active-directory/roles/groups-concept). This allows for Microsoft Entra roles to be assigned to groups.

> [!NOTE]
> With [Microsoft Graph](/graph/overview) support for Azure SQL, the Directory Readers role can be replaced with using lower level permissions. For more information, see [User-assigned managed identity in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).

When enabling a [managed identity](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) for Azure SQL Database, Azure SQL Managed Instance, or Azure Synapse Analytics, the Microsoft Entra ID [**Directory Readers**](/azure/active-directory/roles/permissions-reference#directory-readers) role can be assigned to the identity to allow read access to the [Microsoft Graph API](/graph/overview). The managed identity of SQL Database and Azure Synapse is referred to as the server identity. The managed identity of SQL Managed Instance is referred to as the managed instance identity, and is automatically assigned when the instance is created. For more information on assigning a server identity to SQL Database or Azure Synapse, see [Enable service principals to create Microsoft Entra users](authentication-aad-service-principal.md#enable-service-principals-to-create-azure-ad-users).

The **Directory Readers** role can be used as the server or instance identity to help:

- Create Microsoft Entra logins for SQL Managed Instance
- Impersonate Microsoft Entra users in Azure SQL
- Migrate SQL Server users that use Windows authentication to SQL Managed Instance with Microsoft Entra authentication (using the [ALTER USER (Transact-SQL)](/sql/t-sql/statements/alter-user-transact-sql?view=azuresqldb-mi-current&preserve-view=true#d-map-the-user-in-the-database-to-an-azure-ad-login-after-migration) command)
- Change the Microsoft Entra admin for SQL Managed Instance
- Allow [service principals (Applications)](authentication-aad-service-principal.md) to create Microsoft Entra users in Azure SQL

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Assigning the Directory Readers role

In order to assign the [**Directory Readers**](/azure/active-directory/roles/permissions-reference#directory-readers) role to an identity, a user with [Global Administrator](/azure/active-directory/roles/permissions-reference#global-administrator) or [Privileged Role Administrator](/azure/active-directory/roles/permissions-reference#privileged-role-administrator) permissions is needed. Users who often manage or deploy SQL Database, SQL Managed Instance, or Azure Synapse may not have access to these highly privileged roles. This can often cause complications for users that create unplanned Azure SQL resources, or need help from highly privileged role members that are often inaccessible in large organizations.

For SQL Managed Instance, the **Directory Readers** role must be assigned to the managed instance identity before you can [set up a Microsoft Entra admin for the managed instance](authentication-aad-configure.md#provision-azure-ad-admin-sql-managed-instance). 

Assigning the **Directory Readers** role to the server identity isn't required for SQL Database or Azure Synapse when setting up a Microsoft Entra admin for the logical server. However, to enable Microsoft Entra object creation in SQL Database or Azure Synapse on behalf of a Microsoft Entra application, the **Directory Readers** role is required. If the role isn't assigned to the logical server identity, creating Microsoft Entra users in Azure SQL will fail. For more information, see [Microsoft Entra service principal with Azure SQL](authentication-aad-service-principal.md).

<a name='granting-the-directory-readers-role-to-an-azure-ad-group'></a>

## Granting the Directory Readers role to a Microsoft Entra group

You can now have a [Global Administrator](/azure/active-directory/roles/permissions-reference#global-administrator) or [Privileged Role Administrator](/azure/active-directory/roles/permissions-reference#privileged-role-administrator) create a Microsoft Entra group and assign the [**Directory Readers**](/azure/active-directory/roles/permissions-reference#directory-readers) permission to the group. This will allow access to the Microsoft Graph API for members of this group. In addition, Microsoft Entra users who are owners of this group are allowed to assign new members for this group, including identities of the logical servers.

This solution still requires a high privilege user (Global Administrator or Privileged Role Administrator) to create a group and assign users as a one time activity, but the Microsoft Entra group owners will be able to assign additional members going forward. This eliminates the need to involve a high privilege user in the future to configure all SQL Databases, SQL Managed Instances, or Azure Synapse servers in their Microsoft Entra tenant.

## Next steps

> [!div class="nextstepaction"]
> [Tutorial: Assign Directory Readers role to a Microsoft Entra group and manage role assignments](authentication-aad-directory-readers-role-tutorial.md)

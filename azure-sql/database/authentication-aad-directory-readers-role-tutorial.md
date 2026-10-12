---
title: Assign Directory Readers Role to a Microsoft Entra Group and Manage Role Assignments
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: This article guides you through enabling the Directory Readers role using Microsoft Entra groups to manage Microsoft Entra role assignments with Azure SQL Database and Azure SQL Managed Instance
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 10/07/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: tutorial
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
ms.custom:
  - has-azure-ad-ps-ref
  - azure-ad-ref-level-one-done
  - sfi-image-nochange
---

# Tutorial: Assign Directory Readers role to a Microsoft Entra group and manage role assignments

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article guides you through creating a group in Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)), and assigning that group the [**Directory Readers**](/azure/active-directory/roles/permissions-reference#directory-readers) role. The Directory Readers permissions allow the group owners to add more members to the group, such as a [managed identity](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) of [Azure SQL Database](sql-database-paas-overview.md) and [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md). This bypasses the need for a [Privileged Role Administrator](/azure/active-directory/roles/permissions-reference#privileged-role-administrator) to assign the Directory Readers role directly for each [logical server](logical-servers.md) identity in the tenant.

[!INCLUDE [entra-id](../includes/entra-id.md)]

This tutorial uses the feature introduced in [Use Microsoft Entra groups to manage role assignments](/azure/active-directory/roles/groups-concept).

For more information on the benefits of assigning the Directory Readers role to a Microsoft Entra group for Azure SQL, see [Directory Readers role in Microsoft Entra ID for Azure SQL](authentication-aad-directory-readers-role.md).

> [!NOTE]
> With [Microsoft Graph](/graph/overview) support for Azure SQL, the Directory Readers role can be replaced with using lower level permissions. For more information, see [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).

## Prerequisites

- A Microsoft Entra tenant. For more information, see [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md).
- An Azure SQL Database or Azure SQL Managed Instance.

## Directory Readers role assignment using the Azure portal

### Create a new group and assign owners and role

1. A user with [Privileged Role Administrator](/azure/active-directory/roles/permissions-reference#privileged-role-administrator) permissions is required for this initial setup.
1. Have the privileged user sign into the [Azure portal](https://portal.azure.com).
1. Go to the **Microsoft Entra ID** resource. Under **Manage**, go to **Groups**. Select **New group** to create a new group.
1. Select **Security** as the group type, and fill in the rest of the fields. Make sure that the setting **Microsoft Entra roles can be assigned to the group** is switched to **Yes**. Then assign the Microsoft Entra ID **Directory readers** role to the group.
1. Assign Microsoft Entra users as an owner to the group that you created. A group owner can be a regular AD user without any Microsoft Entra administrative role assigned. The owner should be a user that manages your SQL Database or SQL Managed Instance.
1. Select **Create**

<a id="checking-the-group-that-was-created"></a>

### Check the group that was created

> [!NOTE]
> Make sure that the **Group Type** is **Security**. *Microsoft 365* groups aren't supported for Azure SQL.

To check and manage the group that was created, go back to the **Groups** pane in the Azure portal, and search for your group name. Additional owners and members can be added under the **Owners** and **Members** menu of **Manage** setting after selecting your group. You can also review the **Assigned roles** for the group.

:::image type="content" source="media/authentication-aad-directory-readers-role-tutorial/azure-ad-group-created.png" alt-text="Screenshot of a Group pane with the links that open the Settings menus for Members, Owners, and Assigned roles highlighted." lightbox="media/authentication-aad-directory-readers-role-tutorial/azure-ad-group-created.png":::

<a id="add-an-azure-sql-or-azure-synapse-managed-identity-to-the-group"></a>
<a id="add-azure-sql-managed-identity-to-the-group"></a>

### Add Azure SQL managed identity to the group

> [!NOTE]
> This article uses SQL Managed Instance for the example, but you can apply similar steps for SQL Database to achieve the same results.

For subsequent steps, the Privileged Role Administrator user is no longer needed.

1. Sign in to the Azure portal as the user managing SQL Managed Instance, and is an owner of the group created earlier.

1. Find the name of your **SQL managed instance** resource in the Azure portal.

   :::image type="content" source="media/authentication-aad-directory-readers-role-tutorial/azure-ad-managed-instance.png" alt-text="Screenshot of the SQL managed instances screen with the SQL instance name ssomitest and the Subnet name ManagedInstance highlighted." lightbox="media/authentication-aad-directory-readers-role-tutorial/azure-ad-managed-instance.png":::

   During SQL Managed Instance provisioning, a Microsoft Entra identity is created for your instance, registering it as a Microsoft Entra application. The identity has the same name as the prefix of your SQL Managed Instance name. You can find the identity (also known as the service principal) for your SQL Managed Instance by following these steps:

    - Go to the **Microsoft Entra ID** resource. Under the **Manage** setting, select **Enterprise applications**. The **Object ID** is the identity of the instance.

    :::image type="content" source="media/authentication-aad-directory-readers-role-tutorial/azure-ad-managed-instance-service-principal.png" alt-text="Screenshot of the Enterprise applications page for a Microsoft Entra ID resource with the Object ID of the SQL Managed instance highlighted." lightbox="media/authentication-aad-directory-readers-role-tutorial/azure-ad-managed-instance-service-principal.png":::

1. Go to the **Microsoft Entra ID** resource. Under **Manage**, go to **Groups**. Select the group that you created. Under the **Manage** setting of your group, select **Members**. Select **Add members** and add your SQL Managed Instance service principal as a member of the group by searching for the name found above.

> [!NOTE]
> It can take a few minutes to propagate the service principal permissions through the Azure system, and allow access to Microsoft Graph. You might have to wait a few minutes before you provision a Microsoft Entra admin for SQL Managed Instance.

### Remarks

For SQL Database, create the server identity during [logical server](logical-servers.md) creation or after you create the server. For more information about creating or setting the server identity in SQL Database, see [Enable service principals to create Microsoft Entra users](authentication-aad-service-principal.md#enable-service-principals-to-create-azure-ad-users).

For SQL Managed Instance, the **Directory Readers** role must be assigned to the managed instance identity before you can [set up a Microsoft Entra admin for the managed instance](authentication-aad-configure.md#provision-azure-ad-admin-sql-managed-instance).

You don't need to assign the **Directory Readers** role to the server identity for SQL Database when setting up a Microsoft Entra admin for the logical server. However, to enable Microsoft Entra object creation in SQL Database on behalf of a Microsoft Entra application, the **Directory Readers** role (or lower level permissions discussed in [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md)) is required. If you don't assign the role to the logical server identity, creating Microsoft Entra users in Azure SQL fails. For more information, see [Microsoft Entra service principals with Azure SQL](authentication-aad-service-principal.md).

## Directory Readers role assignment using PowerShell

> [!IMPORTANT]
> A [Privileged Role Administrator](/azure/active-directory/roles/permissions-reference#privileged-role-administrator) will need to run these initial steps. In addition to PowerShell, Microsoft Entra ID offers Microsoft Graph API to [Create a role-assignable group in Microsoft Entra ID](/azure/active-directory/roles/groups-create-eligible#microsoft-graph-api).

1. Download the Microsoft Graph PowerShell module using the following commands. You might need to run PowerShell as an administrator.

    ```powershell
    Install-Module Microsoft.Graph.Authentication
    Import-Module Microsoft.Graph.Authentication
    # To verify that the module is ready to use, run the following command:
    Get-Module Microsoft.Graph.Authentication
    ```

1. Connect to your Microsoft Entra tenant.

    ```powershell
    Connect-MgGraph
    ```

1. Create a security group to assign the **Directory Readers** role.

    - `DirectoryReaderGroup`, `Directory Reader Group`, and `DirRead` can be changed according to your preference.

    ```powershell
    $group = New-MgGroup -DisplayName "DirectoryReaderGroup" -Description "Directory Reader Group" -SecurityEnabled:$true -IsAssignableToRole:$true -MailEnabled:$false -MailNickname "DirRead"
    $group
    ```

1. Assign **Directory Readers** role to the group.

    ```powershell
    # Displays the Directory Readers role information
    $roleDefinition = Get-MgRoleManagementDirectoryRoleDefinition -Filter "DisplayName eq 'Directory Readers'"
    $roleDefinition
    ```

    ```powershell
    # Assigns the Directory Readers role to the group
    $roleAssignment = New-MgRoleManagementDirectoryRoleAssignment -DirectoryScopeId '/' -RoleDefinitionId $roleDefinition.Id -PrincipalId $group.Id
    $roleAssignment
    ```

1. Assign owners to the group.

    - Replace `<username>` with the user you want to own this group. Several owners can be added by repeating these steps.

    ```powershell
    $newGroupOwner = Get-MgUser -UserId "<username>"
    $newGroupOwner
    ```

    ```powershell
    $GrOwner = New-MgGroupOwnerByRef -GroupId $group.Id -OdataId "https://graph.microsoft.com/v1.0/users/$($newGroupOwner.Id)" 
    ```

    Check owners of the group:

    ```powershell
    Get-MgGroupOwner -GroupId $group.Id
    ```

    You can also verify owners of the group in the [Azure portal](https://portal.azure.com). Follow the steps in [Checking the group that was created](#checking-the-group-that-was-created).

<a id="assigning-the-service-principal-as-a-member-of-the-group"></a>

### Assign the service principal as a member of the group

For subsequent steps, the Privileged Role Administrator user is no longer needed.

1. Using an owner of the group that also manages the Azure SQL resource, run the following command to connect to your Microsoft Entra ID.

    ```powershell
    Connect-MgGraph
    ```

1. Assign the service principal as a member of the group that was created.

    - Replace `<ServerName>` with the name of your logical server or managed instance. For more information, see the section, [Add Azure SQL service identity to the group](#add-azure-sql-managed-identity-to-the-group)

    ```powershell
    # Returns the service principal of your Azure SQL resource
    $managedIdentity = Get-MgServicePrincipal -Filter "displayName eq '<ServerName>'"
    $managedIdentity
    ```

    ```powershell
    # Adds the service principal to the group
    New-MgGroupMember -GroupId $group.Id -DirectoryObjectId $managedIdentity.Id
    ```

    The following command will return the service principal Object ID indicating that it has been added to the group:

    ```powershell
    Get-MgGroupMember -GroupId $group.Id -Filter "Id eq '$($managedIdentity.Id)'"
    ```

## Assign Directory Readers role in Azure Synapse Analytics

For Azure Synapse Analytics, see [Tutorial: Assign Directory Readers role to a Microsoft Entra group and manage role assignments](/azure/synapse-analytics/sql/authentication-aad-directory-readers-role-tutorial).

## Related content

- [Directory Readers role in Microsoft Entra ID for Azure SQL](authentication-aad-directory-readers-role.md)
- [Tutorial: Create Microsoft Entra users using Microsoft Entra applications](authentication-aad-service-principal-tutorial.md)
- [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md)

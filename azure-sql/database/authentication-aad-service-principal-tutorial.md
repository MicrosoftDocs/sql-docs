---
title: Create Microsoft Entra users using service principals
description: This tutorial walks you through creating a Microsoft Entra user with a Microsoft Entra applications (service principals) in Azure SQL Database
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 09/27/2023
ms.service: sql-database
ms.subservice: security
ms.custom: has-azure-ad-ps-ref
ms.topic: tutorial
---

# Tutorial: Create Microsoft Entra users using Microsoft Entra applications

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article explains how to configure your Azure SQL Database with a Microsoft Entra application that can create other Microsoft Entra database users. To support this scenario, a Microsoft Entra identity must be generated and assigned to the  [logical server in Azure](logical-servers.md).

[!INCLUDE [entra-id](../includes/entra-id.md)]

For more information on Microsoft Entra authentication for Azure SQL, see the article [Use Microsoft Entra authentication](authentication-aad-overview.md).

In this tutorial, you learn how to:

> [!div class="checklist"]
> - Assign an identity to the Azure SQL logical server
> - Assign Directory Readers permission to the SQL logical server identity
> - Create a service principal (a Microsoft Entra application) in Microsoft Entra ID
> - Create a service principal user in Azure SQL Database
> - Create a different Microsoft Entra user in SQL Database using a Microsoft Entra service principal user

## Prerequisites

- An existing [Azure SQL Database](single-database-create-quickstart.md) deployment. We assume you have a working SQL Database for this tutorial.
- Access to an existing Microsoft Entra tenant.
- [Az.Sql 2.9.0](https://www.powershellgallery.com/packages/Az.Sql/2.9.0) module or higher is needed when using PowerShell to set up an individual Microsoft Entra application as Microsoft Entra admin for Azure SQL. Ensure you are upgraded to the latest module.

## Assign an identity to the Azure SQL logical server

1. Connect to your Microsoft Entra ID. You will need to find your Tenant ID. This can be found by going to the [Azure portal](https://portal.azure.com), and going to your **Microsoft Entra ID** resource. In the **Overview** pane, you should see your **Tenant ID**. Run the following PowerShell command:

    - Replace `<TenantId>` with your **Tenant ID**.

    ```powershell
    Connect-AzAccount -Tenant <TenantId>
    ```

    Record the `TenantId` for future use in this tutorial.

1. Generate and assign a Microsoft Entra identity to the logical server. Execute the following PowerShell command:

    - Replace `<resource group>` and `<server name>` with your resources. If your server name is `myserver.database.windows.net`, replace `<server name>` with `myserver`.

    ```powershell
    Set-AzSqlServer -ResourceGroupName <resource group> -ServerName <server name> -AssignIdentity
    ```

    For more information, see the [Set-AzSqlServer](/powershell/module/az.sql/set-azsqlserver) command.

    > [!IMPORTANT]
    > If a Microsoft Entra identity is set up for the logical server, the [**Directory Readers**](/azure/active-directory/roles/permissions-reference#directory-readers) permission must be granted to the identity. We will walk through this step in following section. **Do not** skip this step as Microsoft Entra authentication will stop working.
    >
    > With [Microsoft Graph](/graph/overview) support for Azure SQL, the Directory Readers role can be replaced with using lower level permissions. For more information, see [User-assigned managed identity in Microsoft Entra ID for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).
    > 
    > If a system-assigned or user-assigned managed identity is used as the server or instance identity, deleting the identity will result in the server or instance inability to access Microsoft Graph. Microsoft Entra authentication and other functions will fail. To restore Microsoft Entra functionality, a new SMI or UMI must be assigned to the server with appropriate permissions.

    - If you used the [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) command with the parameter `AssignIdentity` for a new SQL server creation in the past, you'll need to execute the [Set-AzSqlServer](/powershell/module/az.sql/set-azsqlserver) command afterwards as a separate command to enable this property in the Azure fabric.

1. Check the server identity was successfully assigned. Execute the following PowerShell command:

    - Replace `<resource group>` and `<server name>` with your resources. If your server name is `myserver.database.windows.net`, replace `<server name>` with `myserver`.
    
    ```powershell
    $xyz = Get-AzSqlServer  -ResourceGroupName <resource group> -ServerName <server name>
    $xyz.identity
    ```

    Your output should show you `PrincipalId`, `Type`, and `TenantId`. The identity assigned is the `PrincipalId`.

1. You can also check the identity by going to the [Azure portal](https://portal.azure.com).

    - Under the **Microsoft Entra ID** resource, go to **Enterprise applications**. Type in the name of your SQL logical server. You will see that it has an **Object ID** attached to the resource.
    
    :::image type="content" source="media/authentication-aad-service-principals-tutorial/enterprise-applications-object-id.png" alt-text="Screenshot shows where to find the Object ID for an enterprise application.":::

## Assign Directory Readers permission to the SQL logical server identity

To allow the Microsoft Entra assigned identity to work properly for Azure SQL, the Microsoft Entra ID `Directory Readers` permission must be granted to the server identity.

To grant this required permission, run the following script.

> [!NOTE] 
> This script must be executed by a Microsoft Entra ID `Global Administrator` or a `Privileged Roles Administrator`.
>
> You can assign the `Directory Readers` role to a group in Microsoft Entra ID. The group owners can then add the managed identity as a member of this group, which would bypass the need for a `Global Administrator` or `Privileged Roles Administrator` to grant the `Directory Readers` role. For more information on this feature, see [Directory Readers role in Microsoft Entra ID for Azure SQL](authentication-aad-directory-readers-role.md).

- Replace `<TenantId>` with your `TenantId` gathered earlier.
- Replace `<server name>` with your SQL logical server name. If your server name is `myserver.database.windows.net`, replace `<server name>` with `myserver`.

```powershell
# This script grants Azure "Directory Readers" permission to a Service Principal representing the Azure SQL logical server
# It can be executed only by a "Global Administrator" or "Privileged Roles Administrator" type of user.
# To check if the "Directory Readers" permission was granted, execute this script again

Import-Module AzureAD
Connect-AzureAD -TenantId "<TenantId>"    #Enter your actual TenantId
$AssignIdentityName = "<server name>"     #Enter Azure SQL logical server name
 
# Get Azure AD role "Directory Users" and create if it doesn't exist
$roleName = "Directory Readers"
$role = Get-AzureADDirectoryRole | Where-Object {$_.displayName -eq $roleName}
if ($role -eq $null) {
    # Instantiate an instance of the role template
    $roleTemplate = Get-AzureADDirectoryRoleTemplate | Where-Object {$_.displayName -eq $roleName}
    Enable-AzureADDirectoryRole -RoleTemplateId $roleTemplate.ObjectId
    $role = Get-AzureADDirectoryRole | Where-Object {$_.displayName -eq $roleName}
}
 
# Get service principal for server
$roleMember = Get-AzureADServicePrincipal -SearchString $AssignIdentityName
$roleMember.Count
if ($roleMember -eq $null) {
    Write-Output "Error: No Service Principals with name '$($AssignIdentityName)', make sure that AssignIdentityName parameter was entered correctly."
    exit
}

if (-not ($roleMember.Count -eq 1)) {
    Write-Output "Error: More than one service principal with name pattern '$($AssignIdentityName)'"
    Write-Output "Dumping selected service principals...."
    $roleMember
    exit
}
 
# Check if service principal is already member of readers role
$allDirReaders = Get-AzureADDirectoryRoleMember -ObjectId $role.ObjectId
$selDirReader = $allDirReaders | where{$_.ObjectId -match $roleMember.ObjectId}
 
if ($selDirReader -eq $null) {
    # Add principal to readers role
    Write-Output "Adding service principal '$($AssignIdentityName)' to 'Directory Readers' role'..."
    Add-AzureADDirectoryRoleMember -ObjectId $role.ObjectId -RefObjectId $roleMember.ObjectId
    Write-Output "'$($AssignIdentityName)' service principal added to 'Directory Readers' role'..."
 
    #Write-Output "Dumping service principal '$($AssignIdentityName)':"
    #$allDirReaders = Get-AzureADDirectoryRoleMember -ObjectId $role.ObjectId
    #$allDirReaders | where{$_.ObjectId -match $roleMember.ObjectId}
} else {
    Write-Output "Service principal '$($AssignIdentityName)' is already member of 'Directory Readers' role'."
}
```

> [!NOTE]
> The output from this above script will indicate if the Directory Readers permission was granted to the identity. You can re-run the script if you are unsure if the permission was granted.

For a similar approach on how to set the **Directory Readers** permission for SQL Managed Instance, see [Provision Microsoft Entra admin (SQL Managed Instance)](authentication-aad-configure.md#powershell).

<a name='create-a-service-principal-an-azure-ad-application-in-azure-ad'></a>

## Create a service principal (a Microsoft Entra application) in Microsoft Entra ID

Register your application if you have not already done so. To register an app, you need to either be a Microsoft Entra admin or a user assigned the Microsoft Entra ID *Application Developer* role. For more information about assigning roles, see [Assign administrator and non-administrator roles to users with Microsoft Entra ID](/azure/active-directory/fundamentals/active-directory-users-assign-role-azure-portal).

Completing an app registration generates and displays an **Application ID**.

To register your application:

1. In the Azure portal, select **Microsoft Entra ID** > **App registrations** > **New registration**.

    :::image type="content" source="media/authentication-aad-service-principals-tutorial/new-registration.png" alt-text="Screenshot shows the Register an application page.":::

    After the app registration is created, the **Application ID** value is generated and displayed.

    ![App ID displayed](./media/active-directory-interactive-connect-azure-sql-db/image2.png)

1. You'll also need to create a client secret for signing in. Follow the guide here to [upload a certificate or create a secret for signing in](/azure/active-directory/develop/howto-create-service-principal-portal#authentication-two-options).

1. Record the following from your application registration. It should be available from your **Overview** pane:
    - **Application ID**
    - **Tenant ID** - This should be the same as before

In this tutorial, we'll be using *AppSP* as our main service principal, and *myapp* as the second service principal user that will be created in Azure SQL by *AppSP*. You'll need to create two applications, *AppSP* and *myapp*.

For more information on how to create a Microsoft Entra application, see the article [How to: Use the portal to create a Microsoft Entra application and service principal that can access resources](/azure/active-directory/develop/howto-create-service-principal-portal).

## Create the service principal user in Azure SQL Database

Once a service principal is created in Microsoft Entra ID, create the user in SQL Database. You'll need to connect to your SQL Database with a valid login with permissions to create users in the database.

> [!IMPORTANT]
> Only Microsoft Entra users can create other Microsoft Entra users in Azure SQL Database. Any SQL user with SQL authentication, including a server admin cannot create a Microsoft Entra user. The Microsoft Entra admin is the only user who can initially create Microsoft Entra users in SQL Database. After the Microsoft Entra admin has created other users, any Microsoft Entra user with proper permissions can create other Microsoft Entra users.

1. Create the user *AppSP* in the SQL Database using the following T-SQL command:

    ```sql
    CREATE USER [AppSP] FROM EXTERNAL PROVIDER
    GO
    ```

2. Grant `db_owner` permission to *AppSP*, which allows the user to create other Microsoft Entra users in the database.

    ```sql
    EXEC sp_addrolemember 'db_owner', [AppSP]
    GO
    ```

    For more information, see [sp_addrolemember](/sql/relational-databases/system-stored-procedures/sp-addrolemember-transact-sql)

    Alternatively, `ALTER ANY USER` permission can be granted instead of giving the `db_owner` role. This will allow the service principal to add other Microsoft Entra users.

    ```sql
    GRANT ALTER ANY USER TO [AppSp]
    GO
    ```

    > [!NOTE]
    > The above setting is not required when *AppSP* is set as a Microsoft Entra admin for the server. To set the service principal as an AD admin for the SQL logical server, you can use the Azure portal, PowerShell, or Azure CLI commands. For more information, see [Provision Microsoft Entra admin (SQL Database)](authentication-aad-configure.md?tabs=azure-powershell#powershell-for-sql-database-and-azure-synapse).

<a name='create-an-azure-ad-user-in-sql-database-using-an-azure-ad-service-principal'></a>

## Create a Microsoft Entra user in SQL Database using a Microsoft Entra service principal

> [!IMPORTANT]
> The service principal used to login to SQL Database must have a client secret. If it doesn't have one, follow step 2 of [Create a service principal (a Microsoft Entra application) in Microsoft Entra ID](#create-a-service-principal-an-azure-ad-application-in-azure-ad). This client secret needs to be added as an input parameter in the script below.

1. Use the following script to create a Microsoft Entra service principal user *myapp* using the service principal *AppSP*.

    - Replace `<TenantId>` with your `TenantId` gathered earlier.
    - Replace `<ClientId>` with your `ClientId` gathered earlier.
    - Replace `<ClientSecret>` with your client secret created earlier.
    - Replace `<server name>` with your SQL logical server name. If your server name is `myserver.database.windows.net`, replace `<server name>` with `myserver`.
    - Replace `<database name>` with your SQL Database name.

    ```powershell
    # PowerShell script for creating a new SQL user called myapp using application AppSP with secret
    # AppSP is part of an Azure AD admin for the Azure SQL server below
    
    # Download latest  MSAL  - https://www.powershellgallery.com/packages/MSAL.PS
    Import-Module MSAL.PS
    
    $tenantId = "<TenantId>"   # tenantID (Azure Directory ID) were AppSP resides
    $clientId = "<ClientId>"   # AppID also ClientID for AppSP     
    $clientSecret = "<ClientSecret>"   # Client secret for AppSP 
    $scopes = "https://database.windows.net/.default" # The end-point
    
    $result = Get-MsalToken -RedirectUri $uri -ClientId $clientId -ClientSecret (ConvertTo-SecureString $clientSecret -AsPlainText -Force) -TenantId $tenantId -Scopes $scopes
    
    $Tok = $result.AccessToken
    #Write-host "token"
    $Tok
      
    $SQLServerName = "<server name>"    # Azure SQL logical server name 
    $DatabaseName = "<database name>"     # Azure SQL database name
    
    Write-Host "Create SQL connection string"
    $conn = New-Object System.Data.SqlClient.SQLConnection 
    $conn.ConnectionString = "Data Source=$SQLServerName.database.windows.net;Initial Catalog=$DatabaseName;Connect Timeout=30"
    $conn.AccessToken = $Tok
    
    Write-host "Connect to database and execute SQL script"
    $conn.Open() 
    $ddlstmt = 'CREATE USER [myapp] FROM EXTERNAL PROVIDER;'
    Write-host " "
    Write-host "SQL DDL command"
    $ddlstmt
    $command = New-Object -TypeName System.Data.SqlClient.SqlCommand($ddlstmt, $conn)       
    
    Write-host "results"
    $command.ExecuteNonQuery()
    $conn.Close()
    ``` 

    Alternatively, you can use the code sample in the blog, [Microsoft Entra service Principal authentication to SQL DB - Code Sample](https://techcommunity.microsoft.com/t5/azure-sql-database/azure-ad-service-principal-authentication-to-sql-db-code-sample/ba-p/481467). Modify the script to execute a DDL statement `CREATE USER [myapp] FROM EXTERNAL PROVIDER`. The same script can be used to create a regular Microsoft Entra user or a group in SQL Database.

    
2. Check if the user *myapp* exists in the database by executing the following command:

    ```sql
    SELECT name, type, type_desc, CAST(CAST(sid as varbinary(16)) as uniqueidentifier) as appId from sys.database_principals WHERE name = 'myapp'
    GO
    ```

    You should see a similar output:

    ```output
    name	type	type_desc	appId
    myapp	E	EXTERNAL_USER	6d228f48-xxxx-xxxx-xxxx-xxxxxxxxxxxx
    ```

## Next steps

- [Microsoft Entra service principal with Azure SQL](authentication-aad-service-principal.md)
- [What are managed identities for Azure resources?](/azure/active-directory/managed-identities-azure-resources/overview)
- [How to use managed identities for App Service and Azure Functions](/azure/app-service/overview-managed-identity)
- [Microsoft Entra service principal authentication to SQL DB - Code Sample](https://techcommunity.microsoft.com/t5/azure-sql-database/azure-ad-service-principal-authentication-to-sql-db-code-sample/ba-p/481467)
- [Application and service principal objects in Microsoft Entra ID](/azure/active-directory/develop/app-objects-and-service-principals)
- [Create an Azure service principal with Azure PowerShell](/powershell/azure/create-azure-service-principal-azureps)
- [Directory Readers role in Microsoft Entra ID for Azure SQL](authentication-aad-directory-readers-role.md)

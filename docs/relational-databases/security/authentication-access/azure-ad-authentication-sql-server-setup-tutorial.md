---
title: "Set up Azure Active Directory authentication for SQL Server"
description: Tutorial on how to set up Azure Active Directory Authentication for SQL Server
ms.date: "05/24/2022"
ms.prod: sql
ms.technology: security
ms.reviewer: vanto
ms.topic: tutorial
author: GithubMirek
ms.author: mireks
monikerRange: ">=sql-server-ver16||>= sql-server-linux-ver16"
---

# Tutorial: Set up Azure Active Directory authentication for SQL Server

[!INCLUDE [SQL Server 2022](../../../includes/applies-to-version/sqlserver2022.md)]

This document describes a step-by-step process on how to set up Azure Active Directory (Azure AD) authentication for SQL Server, as well as how to use different Azure AD authentication methods. This feature is available in SQL Server 2022 or later versions, and is only supported for SQL Server on-premises for Windows OS.

In this tutorial, you learn how to:

> [!div class="checklist"]
> - Create and register an Azure AD application
> - Grant permissions to the Azure AD application
> - Create and assign a certificate
> - Configure Azure AD authentication for SQL Server through Azure portal
> - Create logins and users
> - Connect with a supported authentication method

## Prerequisites

- SQL Server 2022 is installed.
- SQL Server is connected to Azure cloud. For more information, see [Connect SQL Server to Azure Arc](/sql/sql-server/azure-arc/connect).
- SQL Server Arc extension version 1.1.1795.50 or higher is installed.
- Access to Azure Active Directory is available for authentication purpose. For more information, see [Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-overview.md).
- [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms) version 18.0 or higher is installed on the client machine. Or download the latest [Azure Data Studio](/sql/azure-data-studio/download-azure-data-studio).

> [!NOTE]
> To perform Azure AD authentication, SQL Server needs to be able to query Azure AD and requires an Azure AD app registration which it can authenticate as. The app registration also needs a handful of permissions for the queries SQL Server will perform.
>
> SQL Server uses a certificate for this authentication, and it is stored in Azure Key Vault (AKV). The Azure Arc agent downloads the certificate to the SQL Server instance host.
>
> Azure AD authentication is only supported for SQL Server running in the mixed mode authentication (allowing SQL Server and Windows authentication mode). If only Windows authentication mode is chosen, Azure AD authentication is not supported.

## Create and register an Azure AD application

1. Go to the [Azure portal](https://portal.azure.com), select **Azure Active Directory** > **App Registrations** > **New Registration**.
   1. Specify a name - In the example below, we use *SQLServerCTP1*.
   1. Select **Supported account types** and use **Accounts in this organization directory only**
   1. Do not set a redirect URI
   1. Select **Register**

See the application registration below:

:::image type="content" source="media/register-app.png" alt-text="Screenshot of registering application in the Azure portal.":::

## Grant application permissions

Select the newly created application, and on the left side menu, select **API Permissions**.

1. Select **Add a permission** > **Microsoft Graph** > **Application permissions**
   1. Check **Directory.Read.All**
   1. Select **Add permissions**

1. Select **Add a permission** > **Microsoft Graph** > **Delegated permissions**
   1. Check **Application.Read.All**
   1. Check **Directory.AccessAsUser.All**
   1. Check **Group.Read.All**
   1. Check **User.Read.All**
   1. Select **Add permissions**

1. Select **Grant admin consent**

:::image type="content" source="media/configured-app-permissions.png" alt-text="Screenshot of application permissions in the Azure portal.":::

> [!NOTE]
> To grant **Admin consent** to the permissions above, your account requires a role of Azure AD Global Administrator or Privileged Role Administrator.

## Create and assign a certificate

1. Go to the [Azure portal](https://portal.azure.com), select **Key vaults**, and select the key vault you wish to use or create a new one. Select **Certificates** > **Generate/Import**

   1. For the **Method of certificate creation**, use **Generate**.
   1. Add a certificate name and subject.

   1. The recommended validity period is at most 12 months. The rest of the values can be left as default.

   1. Select **Create**.

   :::image type="content" source="media/create-certificate.png" alt-text="Screenshot of creating certificate in the Azure portal.":::

   > [!NOTE]
   > Once the certificate is created, it may say it is **disabled**. Refresh the site and it will show the certificate as enabled.

1. Navigate to the new certificate, and select the row for the certificate's latest version. Select **Download in CER format** to save the certificate's public key.

   :::image type="content" source="media/download-certificate.png" alt-text="Screenshot of certificate in the Azure portal where you can view and download the certificate.":::

   > [!NOTE]
   > This does not need to be done on the SQL Server host. Rather, any client that will access the Azure portal for the next step.

1. In the Azure portal, navigate to the app registration created above and select **Certificates & secrets**

   1. Select **upload certificate**.
   1. Select the public key (.cer file) downloaded in the last step.
   1. Select **Add**.

   :::image type="content" source="media/upload-certificate-to-application.png" alt-text="Screenshot of certificate and secrets menu in the Azure portal." lightbox="media/upload-certificate-to-application.png":::

1. In the Azure portal, navigate to the Azure Key Vault instance which the certificate is stored, and select **Access policies**

   1. Select **Add Access Policy**.
   1. For **Key permissions**, use **0 selected**.
   1. For **Secret permissions**, select **Get**.
   1. For **Certificate permissions**, select **Get**.
   1. For **Select principal**, use the account for your Azure Arc instance, which is the hostname of the SQL Server host.
   1. Select **Add** and then select **Save**.

   > [!WARNING]
   > You must **Save** to ensure the permissions are applied. They are not applied after selecting **Add.** To ensure permissions have been stored, refresh the browser window and check the row for your Azure Arc instance is still present.

   :::image type="content" source="media/add-access-policy-on-key-vault.png" alt-text="Screenshot of adding access policy to the key vault in the Azure portal.":::

## Configure Azure AD authentication for SQL Server through Azure portal

1. Go to the [Azure portal](https://portal.azure.com), and select **SQL Server – Azure Arc**, and select the instance for your SQL Server host.
1. Check the status of your **SQL Server - Azure Arc** resource and see if it's connected by going to the **Properties** menu. For more information, see [Validate the SQL Server - Azure Arc resources](/sql/sql-server/azure-arc/connect?#validate-the-sql-server---azure-arc-resources).

1. Select **Azure Active Directory** on the left-hand column.

1. Select **Set Admin**, and choose an account which will be added as an admin login to SQL Server.
1. Select **Customer-managed cert** and **Select a certificate**.
1. Select **Change certificate**, and select your AKV instance and certificate that you created earlier in the new blade.
1. Select **Customer-managed app registration**.
1. Select **Change app registration**, and select the app registration you created earlier.
1. Select **Save**. This will send a request to the Arc server agent which will configure Azure AD authentication for that SQL Server instance.

:::image type="content" source="media/configure-azure-ad-for-sql-server-instance.png" alt-text="Screenshot of setting Azure Active Directory authentication in the Azure portal.":::

> [!NOTE]
>
> - It takes several minutes to download certificates and configure settings. After setting all parameters and selecting **Save** on the Azure portal, the following message may appear: `SQL Server's Azure Arc agent is currently processing a request. Values below may be incorrect. Please wait until the agent is done before continuing`. Wait until the save process is confirmed with `Saved successfully`, before attempting an Azure AD login.
> - The Azure Arc server agent can only update once the previous action has completed. This means that saving a new Azure AD configuration before the last one has finalized can cause a failure. If you see the message **Extended call failed** when you select **Save**, wait 5 minutes and then try again.
> - The admin login specified in the portal will be added as a `sysadmin` to the SQL Server instance, but it will not be listed in `syslogins` or `sys.server_principals`.

## Create logins and users

After the Azure Arc agent on the SQL Server host has completed its operation, the admin account selected in the **Azure Active Directory** blade in the portal will be a `sysadmin` on the SQL Server instance. To sign in, use any SQL Server client like [SSMS](/sql/ssms/download-sql-server-management-studio-ssms) or [Azure Data Studio](/sql/azure-data-studio/download-azure-data-studio).

> [!NOTE]
> All connections to SQL Server that are done with Azure AD authentication require an encrypted connection. If the Database Administrator (DBA) has not set up a trusted SSL/TLS certificate for the server, logins will likely fail with the message **The certificate chain was issued by an authority that is not trusted.** To fix this, either configure the SQL Server instance to use an SSL/TLS certificate which is trusted by the client or select **trust server certificate** in the advanced connection properties. For more information, see [Enable encrypted connections to the Database Engine](/sql/database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine).

### Create login syntax

The same syntax that is used for creating Azure AD logins and users on Azure SQL Database and Azure SQL Managed Instance can now be used on SQL Server. However, on SQL Server, this can be done by any account which has the `ALTER ANY LOGIN` or `ALTER ANY USER` permission. Any account with either of these permissions can create a login or user respectively. They do not need to be an Azure AD login.

To create a login for an Azure AD account, execute the T-SQL command below in `master` database:

```sql
CREATE LOGIN [principal name] FROM EXTERNAL PROVIDER 
```

For users, the principal name should be in the format `user@contoso.com`. For all other account types, the tenant name is not necessary and either the Azure AD group name or application name must be used.

Here's some examples:

```sql
CREATE LOGIN [user@contoso.com] FROM EXTERNAL PROVIDER   -- login creation for Azure AD user;
GO
CREATE LOGIN [my_group_name] FROM EXTERNAL PROVIDER      -- login creation for Azure AD group;
GO
CREATE LOGIN [my_app_name] FROM EXTERNAL PROVIDER        -- login creation for Azure AD application;
GO
```

To list the Azure AD logins in `master` database, execute the T-SQL command:

```sql
SELECT * FROM sys.server_principals  
```

> [!NOTE]
> To grant an Azure AD user membership to the `sysadmin` role (for example `admin@contoso.com`), execute the following commands in `master` database:
>
> ```sql
> CREATE LOGIN [admin@contoso.com] FROM EXTERNAL PROVIDER; 
> GO
> EXEC sp_addsrvrolemember @loginame='admin@contoso.com', @rolename='sysadmin'
> GO
> ```

The `sp_addsrvrolemember` command must be executed as a member of the SQL Server `sysadmin` server role.

### Create user syntax

You can create an Azure AD user either as a user with an Azure AD login, or as an Azure AD contained user.

To create an Azure AD user from an Azure AD login in a SQL Server database where the user should reside in, use the following syntax:

```sql
CREATE USER [principal_name] FROM LOGIN [principal_name]
```

The `principal_name` syntax is the same as for logins.

Here are some examples:

```sql
CREATE USER [user@contoso.com] FROM LOGIN [user@contoso.com];  -- for Azure AD user
GO
CREATE USER [my_group_name] FROM LOGIN [my_group_name];        -- for Azure AD group
GO
CREATE USER [my_app_name] FROM LOGIN [my_app_name];            -- for Azure AD application
GO
```

To create an Azure AD contained user without a login, the following syntax can be executed:

```sql
CREATE USER [principal name] FROM EXTERNAL PROVIDER
```

Use Azure AD group name or Azure AD application name as `<principal name>` when creating an Azure AD user as a group or application.

Here are some examples:

```sql
CREATE USER [user@contoso.com] FROM EXTERNAL PROVIDER;  --for Azure AD contained user
GO
CREATE USER [my_group_name] FROM EXTERNAL PROVIDER;     --for Azure AD contained group
GO
CREATE USER [my_group_name] FROM EXTERNAL PROVIDER;     --for Azure AD contained application
GO
```

To list the users created in the database, execute the following T-SQL command:

```sql
SELECT * FROM sys.database_principals;
```

> [!NOTE]
> The newly created user in a database, by default has only the **Connect** permission. All other SQL Server permissions for this user must be explicitly granted by the grantors.  

### Azure AD guest accounts

The `CREATE LOGIN` and `CREATE USER` syntax also supports guest users. For example, if `testuser@outlook.com` was invited to the `contoso.com` tenant, it could be added as a login to SQL Server with the syntax below. In the example, `outlook.com` is provided even though SQL Server will use the account registered in the `contoso.com` tenant.

The following section has examples of creating guest users.

#### Create a guest user with login that exist

```sql
CREATE USER [testuser@outlook.com] FROM LOGIN [testuser@outlook.com]
```

#### Create a guest user as a contained user  

```sql
CREATE USER [testuser@outlook.com] FROM EXTERNAL PROVIDER 
```

## Connect with a supported authentication method

SQL Server supports four authentication methods for Azure AD authentication:

- Azure Active Directory Password
- Azure Active Directory Integrated
- Azure Active Directory Universal with Multi-Factor Authentication
- Azure Active Directory access token

Use one of these methods to connect to the SQL Server instance. For more information, see [Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-overview.md).

## Authentication example using SSMS

Below is the snapshot of the SSMS connection page using the authentication method, **Azure Active Directory - Universal with MFA**.

:::image type="content" source="media/sql-server-management-studio-connection.png" alt-text="Screenshot S S M S showing the Connect to Server window.":::

> [!NOTE]
> During the authentication process, a database where the user was created must be explicitly indicated in SSMS. Expand **Options>> Connection Properties > Connect to database: `database_name`**.

For more information on **Azure Active Directory - Universal with MFA** authentication method see [Universal with MFA](/azure/azure-sql/database/authentication-mfa-ssms-configure).

> [!NOTE]
> SQL Server tools that support Azure AD authentication for Azure SQL are also supported for SQL Server 2022.

## Known issues

- Updating certificate doesn't propagate
  - Once Azure AD is configured for SQL Server, updating the certificate in **SQL Server - Azure Arc** resource's **Azure AD** blade may not propagate fully. This results in the save being **successful** but the old value still being displayed. To update the certificate, do the following:

    - Select **Remove Admin**.
    - Select **Save**.
    - Select **Set Admin** and reconfigure Azure AD authentication with the new certificate.
    - Select **Save**.

## See also

- [Connect SQL Server to Azure Arc](/sql/sql-server/azure-arc/connect)
- [Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-overview.md)
- [Linked server for SQL Server with Azure Active Director authentication](azure-ad-authentication-sql-server-linked-server.md)

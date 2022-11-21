---
title: "Set up Azure Active Directory authentication for SQL Server"
description: Tutorial on how to set up Azure Active Directory Authentication for SQL Server
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto, randolphwest
ms.date: 10/25/2022
ms.service: sql
ms.subservice: security
ms.topic: tutorial
ms.custom: event-tier1-build-2022
monikerRange: ">=sql-server-ver16||>= sql-server-linux-ver16"
---

# Tutorial: Set up Azure Active Directory authentication for SQL Server

[!INCLUDE [SQL Server 2022](../../../includes/applies-to-version/sqlserver2022.md)]

This document describes a step-by-step process on how to set up Azure Active Directory (Azure AD) authentication for SQL Server, and how to use different Azure AD authentication methods. This feature is available in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and later versions, and is only supported for SQL Server on-premises, for Windows and Linux hosts and [SQL Server 2022 on Windows Azure VMs](/azure/azure-sql/virtual-machines/windows/security-considerations-best-practices#azure-ad-authentication-preview).

In this tutorial, you learn how to:

> [!div class="checklist"]

> - Create and register an Azure AD application
> - Grant permissions to the Azure AD application
> - Create and assign a certificate
> - Configure Azure AD authentication for SQL Server through Azure portal
> - Create logins and users
> - Connect with a supported authentication method

## Prerequisites

- [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] is installed.
- SQL Server is connected to Azure cloud. For more information, see [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md).
- Access to Azure Active Directory is available for authentication purpose. For more information, see [Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-overview.md).
- [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md) version 18.0 or higher is installed on the client machine. Or download the latest [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

### Authentication prerequisites

> [!NOTE]
> Extended functionality has been implemented in Azure to allow the automatic creation of the Azure Key Vault certificate and Azure AD application during setting up an Azure AD admin for the SQL Server. For more information, see [Tutorial: Using automation to set up the Azure Active Directory admin for SQL Server](azure-ad-authentication-sql-server-automation-setup-tutorial.md).

- To perform Azure AD authentication, SQL Server needs to be able to query Azure AD and requires an Azure AD app registration, which it can authenticate as. The app registration also needs a handful of permissions for the queries SQL Server will perform.

- SQL Server uses a certificate for this authentication, and it is stored in Azure Key Vault (AKV). The Azure Arc agent downloads the certificate to the SQL Server instance host.

> [!WARNING]  
> Connections authenticated by Azure AD are always encrypted. If SQL Server is using a self-signed certificate, you must add `trust server cert = true` in the connection string. SQL Server and Windows authenticated connections don't require encryption, but it is recommended.

## Create and register an Azure AD application

1. Go to the [Azure portal](https://portal.azure.com), select **Azure Active Directory** > **App Registrations** > **New Registration**.
   1. Specify a name - In the example below, we use *SQLServerCTP1*.
   1. Select **Supported account types** and use **Accounts in this organization directory only**
   1. Don't set a redirect URI
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

1. In the Azure portal, navigate to the app registration created above and select **Certificates** list

   1. Select **upload certificate**.
   1. Select the public key (.cer file) downloaded in the last step.
   1. Select **Add**.

   :::image type="content" source="media/upload-certificate-to-application.png" alt-text="Screenshot of certificate and secrets menu in the Azure portal." lightbox="media/upload-certificate-to-application.png":::

1. In the Azure portal, navigate to the Azure Key Vault instance where the certificate is stored, and select **Access policies**

   1. Select **Add Access Policy**.
   1. For **Key permissions**, use **0 selected**.
   1. For **Secret permissions**, select **Get** and **List**.
   1. For **Certificate permissions**, select **Get** and **List**.
   1. For **Select principal**, use the account for your Azure Arc instance, which is the hostname of the SQL Server host.
   1. Select **Add** and then select **Save**.

      You must **Save** to ensure the permissions are applied. They are not applied after selecting **Add**. To ensure permissions have been stored, refresh the browser window, and check the row for your Azure Arc instance is still present.

   :::image type="content" source="media/add-access-policy-on-key-vault.png" alt-text="Screenshot of adding access policy to the key vault in the Azure portal.":::

## Configure Azure AD authentication for SQL Server through Azure portal

> [!NOTE]
> Using the [Azure CLI](azure-ad-authentication-sql-server-automation-setup-tutorial.md?tabs=azure-cli#setting-up-the-azure-ad-admin-for-the-sql-server), [PowerShell](azure-ad-authentication-sql-server-automation-setup-tutorial.md?tabs=azure-powershell#setting-up-the-azure-ad-admin-for-the-sql-server), or [ARM template](azure-ad-authentication-sql-server-automation-setup-tutorial.md?tabs=arm-template#setting-up-the-azure-ad-admin-for-the-sql-server) to set up an Azure AD admin for SQL Server is available.

1. Go to the [Azure portal](https://portal.azure.com), and select **SQL Server – Azure Arc**, and select the instance for your SQL Server host.

1. Check the status of your **SQL Server - Azure Arc** resource and see if it's connected by going to the **Properties** menu. For more information, see [Validate the SQL Server - Azure Arc resources](../../../sql-server/azure-arc/connect.md?#validate-your-arc-enabled-sql-server-resources).

1. Select **Azure Active Directory** on the left-hand column.

1. Select **Set Admin**, and choose an account that will be added as an admin login to SQL Server.

1. Select **Customer-managed cert** and **Select a certificate**.

1. Select **Change certificate**, and select your AKV instance and certificate that you created earlier in the new pane.

1. Select **Customer-managed app registration**.

1. Select **Change app registration**, and select the app registration you created earlier.

1. Select **Save**. This will send a request to the Arc server agent, which will configure Azure AD authentication for that SQL Server instance.

   :::image type="content" source="media/configure-azure-ad-for-sql-server-instance.png" alt-text="Screenshot of setting Azure Active Directory authentication in the Azure portal.":::

   It takes several minutes to download certificates and configure settings. After setting all parameters and selecting **Save** on the Azure portal, the following message may appear: `SQL Server's Azure Arc agent is currently processing a request. Values below may be incorrect. Please wait until the agent is done before continuing`. Wait until the save process is confirmed with `Saved successfully`, before attempting an Azure AD login.

   The Azure Arc server agent can only update once the previous action has completed. This means that saving a new Azure AD configuration before the last one has finalized can cause a failure. If you see the message **Extended call failed** when you select **Save**, wait 5 minutes and then try again.

   The Azure AD admin login is listed in `sys.server_principals`, but is not part of the `sysadmin` role. To grant the Azure AD admin the `sysadmin` role, use the [sp_addsrvrolemember](../../system-stored-procedures/sp-addsrvrolemember-transact-sql.md) stored procedure.

   ```sql
   ALTER SERVER ROLE sysadmin ADD MEMBER [aadadmin@contoso.com]
   GO
   ```

   > [!NOTE]
   > Once the Azure AD admin login is granted the `sysadmin` role, changing the Azure AD admin in the Azure portal does not remove the previous login that remains as a `sysadmin`. To remove the login, it must be dropped manually.
   >
   > The Azure AD admin change for the SQL Server instance takes place without a server restart, once the process is completed with the SQL Server's Azure Arc agent. For the new admin to display in `sys.server_principals`, the SQL Server instance must be restarted, and until then, the old admin is displayed. The current Azure AD admin can be checked in the Azure portal.

## Create logins and users

After the Azure Arc agent on the SQL Server host has completed its operation, the admin account selected in the **Azure Active Directory** pane in the portal will be a `sysadmin` on the SQL Server instance. To sign in, use any SQL Server client like [SSMS](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

> [!NOTE]  
> All connections to SQL Server that are done with Azure AD authentication require an encrypted connection. If the Database Administrator (DBA) has not set up a trusted SSL/TLS certificate for the server, logins will likely fail with the message **The certificate chain was issued by an authority that is not trusted.** To fix this, either configure the SQL Server instance to use an SSL/TLS certificate which is trusted by the client or select **trust server certificate** in the advanced connection properties. For more information, see [Enable encrypted connections to the Database Engine](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).

### Create login syntax

The same syntax that is used for creating Azure AD logins and users on Azure SQL Database and Azure SQL Managed Instance can now be used on SQL Server. However, on SQL Server this can be done by any account that has the `ALTER ANY LOGIN` or `ALTER ANY USER` permission. Any account with either of these permissions can create a login or user respectively. They don't need to be an Azure AD login.

To create a login for an Azure AD account, execute the T-SQL command below in the `master` database:

```sql
CREATE LOGIN [principal name] FROM EXTERNAL PROVIDER;
```

For users, the principal name should be in the format `user@contoso.com`. For all other account types, the tenant name isn't necessary and either the Azure AD group name or application name must be used.

Here's some examples:

```sql
-- login creation for Azure AD user
CREATE LOGIN [user@contoso.com] FROM EXTERNAL PROVIDER;
GO
-- login creation for Azure AD group
CREATE LOGIN [my_group_name] FROM EXTERNAL PROVIDER;
GO
-- login creation for Azure AD application
CREATE LOGIN [my_app_name] FROM EXTERNAL PROVIDER;
GO
```

To list the Azure AD logins in `master` database, execute the T-SQL command:

```sql
SELECT * FROM sys.server_principals;
```

To grant an Azure AD user membership to the `sysadmin` role (for example `admin@contoso.com`), execute the following commands in `master` database:

```sql
CREATE LOGIN [admin@contoso.com] FROM EXTERNAL PROVIDER; 
GO
EXEC sp_addsrvrolemember @loginame='admin@contoso.com', @rolename='sysadmin';
GO
```

The `sp_addsrvrolemember` stored procedure must be executed as a member of the SQL Server `sysadmin` server role.

### Create user syntax

You can create an Azure AD user either as a user with an Azure AD login, or as an Azure AD contained user.

To create an Azure AD user from an Azure AD login in a SQL Server database where the user should reside in, use the following syntax:

```sql
CREATE USER [principal_name] FROM LOGIN [principal_name];
```

The `principal_name` syntax is the same as for logins.

Here are some examples:

```sql
-- for Azure AD user
CREATE USER [user@contoso.com] FROM LOGIN [user@contoso.com];
GO
-- for Azure AD group
CREATE USER [my_group_name] FROM LOGIN [my_group_name];
GO
-- for Azure AD application
CREATE USER [my_app_name] FROM LOGIN [my_app_name];
GO
```

To create an Azure AD contained user without a login, the following syntax can be executed:

```sql
CREATE USER [principal name] FROM EXTERNAL PROVIDER;
```

Use Azure AD group name or Azure AD application name as `<principal name>` when creating an Azure AD user as a group or application.

Here are some examples:

```sql
-- for Azure AD contained user
CREATE USER [user@contoso.com] FROM EXTERNAL PROVIDER;
GO
-- for Azure AD contained group
CREATE USER [my_group_name] FROM EXTERNAL PROVIDER;
GO
--for Azure AD contained application
CREATE USER [my_group_name] FROM EXTERNAL PROVIDER;
GO
```

To list the users created in the database, execute the following T-SQL command:

```sql
SELECT * FROM sys.database_principals;
```

The newly created user in a database has only the **Connect** permission, by default. All other SQL Server permissions for this user must be explicitly granted by the grantors.

### Azure AD guest accounts

The `CREATE LOGIN` and `CREATE USER` syntax also supports guest users. For example, if `testuser@outlook.com` was invited to the `contoso.com` tenant, it could be added as a login to SQL Server with the syntax below. In the example, `outlook.com` is provided even though SQL Server will use the account registered in the `contoso.com` tenant.

The following section has examples of creating guest users.

#### Create a guest user with login that exists

```sql
CREATE USER [testuser@outlook.com] FROM LOGIN [testuser@outlook.com];
```

#### Create a guest user as a contained user

```sql
CREATE USER [testuser@outlook.com] FROM EXTERNAL PROVIDER;
```

## Connect with a supported authentication method

SQL Server supports four authentication methods for Azure AD authentication:

- Azure Active Directory Password
- Azure Active Directory Integrated
- Azure Active Directory Universal with Multi-Factor Authentication
- Azure Active Directory access token

Use one of these methods to connect to the SQL Server instance. For more information, see [Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-overview.md).

## Authentication example using SSMS

Below is the snapshot of the SQL Server Management Studio (SSMS) connection page using the authentication method, **Azure Active Directory - Universal with MFA**.

:::image type="content" source="media/sql-server-management-studio-connection.png" alt-text="Screenshot SSMS showing the Connect to Server window.":::

During the authentication process, a database where the user was created must be explicitly indicated in SSMS. Expand **Options > Connection Properties > Connect to database: `database_name`**.

For more information on **Azure Active Directory - Universal with MFA** authentication method, see [Universal with MFA](/azure/azure-sql/database/authentication-mfa-ssms-configure).

SQL Server tools that support Azure AD authentication for Azure SQL are also supported for [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)].

## Location where Azure AD parameters are stored

> [!WARNING]  
> Azure AD parameters are configured by the Azure Arc agent, and should not be reconfigured manually.

On Linux, Azure Active Directory parameters are stored in `mssql-conf`. For more information about the Azure AD configuration options in Linux, see [Configure SQL Server on Linux with the mssql-conf tool](../../../linux/sql-server-linux-configure-mssql-conf.md).

## Known issues

- Updating certificate doesn't propagate:
  - Once Azure AD is configured for SQL Server, updating the certificate in **SQL Server - Azure Arc** resource's **Azure AD** pane may not propagate fully. This results in the save being **successful** but the old value still being displayed. To update the certificate, do the following:

    - Select **Remove Admin**.
    - Select **Save**.
    - Select **Set Admin** and reconfigure Azure AD authentication with the new certificate.
    - Select **Save**.

## See also

- [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md)
- [Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-overview.md)
- [Linked server for SQL Server with Azure Active Directory authentication](azure-ad-authentication-sql-server-linked-server.md)
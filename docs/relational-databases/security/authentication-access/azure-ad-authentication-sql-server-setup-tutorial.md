---
title: "Set up Microsoft Entra authentication for SQL Server"
description: Tutorial on how to set up Microsoft Entra authentication for SQL Server
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto, randolphwest
ms.date: 08/09/2023
ms.service: sql
ms.subservice: security
ms.topic: tutorial
monikerRange: ">=sql-server-ver16||>= sql-server-linux-ver16"
---

# Tutorial: Set up Microsoft Entra authentication for SQL Server

[!INCLUDE [SQL Server 2022](../../../includes/applies-to-version/sqlserver2022.md)]

This article describes a step-by-step process to set up authentication with Microsoft Entra ID, and demonstrates how to use different Microsoft Entra authentication methods.

[!INCLUDE [entra-id](../../../includes/entra-id.md)]

In this tutorial, you learn how to:

> [!div class="checklist"]
> - Create and register a Microsoft Entra application
> - Grant permissions to the Microsoft Entra application
> - Create and assign a certificate
> - Configure Microsoft Entra authentication for SQL Server through Azure portal
> - Create logins and users
> - Connect with a supported authentication method

## Prerequisites

- A physical or virtual Windows Server on-premises with an instance of [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)].

    For SQL Server on Azure VMs, review [Microsoft Entra authentication for SQL Server 2022 on Azure VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm).

- The server and instance enabled by Azure Arc. For more information, see [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md).
- Access to Microsoft Entra ID is available for authentication purpose. For more information, see [Microsoft Entra authentication for SQL Server](azure-ad-authentication-sql-server-overview.md).
- [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md) version 18.0 or higher is installed on the client machine. Or download the latest [Azure Data Studio](/azure-data-studio/download-azure-data-studio).

### Authentication prerequisites

> [!NOTE]
> Extended functionality has been implemented in Azure to allow the automatic creation of the Azure Key Vault certificate and Microsoft Entra application during setting up a Microsoft Entra admin for the SQL Server. For more information, see [Tutorial: Using automation to set up the Microsoft Entra admin for SQL Server](azure-ad-authentication-sql-server-automation-setup-tutorial.md).

- Microsoft Entra application registration for SQL Server. Registering a SQL Server instance as a Microsoft Entra application allows the instance to query Microsoft Entra ID, and allows the Microsoft Entra application to authenticate on behalf of the SQL Server instance. Application registration also requires a few permissions, which are used by SQL Server for certain queries.

- SQL Server uses a certificate for this authentication, which is stored in Azure Key Vault (AKV). The Azure Arc agent downloads the certificate to the SQL Server instance host.

> [!WARNING]  
> Connections authenticated by Microsoft Entra ID are always encrypted. If SQL Server is using a self-signed certificate, you must add `trust server cert = true` in the connection string. SQL Server and Windows authenticated connections don't require encryption, but it is strongly recommended.

<a name='create-and-register-an-azure-ad-application'></a>

## Create and register a Microsoft Entra application

1. Go to the [Azure portal](https://portal.azure.com), select **Microsoft Entra ID** > **App Registrations** > **New Registration**.
   1. Specify a name - The example in this article uses *SQLServerCTP1*.
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
> To grant **Admin consent** to the permissions above, your Microsoft Entra account requires either the Global Administrator or Privileged Role Administrator role.

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

1. In the Azure portal, navigate to the Azure Key Vault instance where the certificate is stored, and select **Access policies** from the navigation menu.

   1. Select **Create**.
   1. For **Secret permissions**, select **Get** and **List**.
   1. For **Certificate permissions**, select **Get** and **List**.
   1. Select **Next**.
   1. On the **Principal** page, search for the name of your Machine - Azure Arc instance, which is the hostname of the SQL Server host.

       :::image type="content" source="media/machine-azure-arc-resource.png" alt-text="Screenshot of Azure Arc server resource in portal.":::

   1. Skip the **Application (optional)** page by selecting **Next** twice, or selecting **Review + create**.

      Verify that the "Object ID" of the **Principal** matches the **Principal ID** of the managed identity assigned to the instance.

      :::image type="content" source="media/customer-managed-akv-review-create.png" alt-text="Screenshot of Azure portal to review and create access policy."

      To confirm, go to the resource page and select **JSON View** in the top right of the Essentials box on the Overview page. Under **identity** you'll find the **principalId**

      :::image type="content" source="media/machine-azure-arc-json-view.png" alt-text="Screenshot of portal control of JSON view of machine definition.":::

   1. Select **Create**.

   You must select **Create** to ensure that the permissions are applied. To ensure permissions have been stored, refresh the browser window, and check that the row for your Azure Arc instance is still present.

   :::image type="content" source="media/add-access-policy-on-key-vault.png" alt-text="Screenshot of adding access policy to the key vault in the Azure portal.":::

<a name='configure-azure-ad-authentication-for-sql-server-through-azure-portal'></a>

## Configure Microsoft Entra authentication for SQL Server through Azure portal

> [!NOTE]
> Using the [Azure CLI](azure-ad-authentication-sql-server-automation-setup-tutorial.md?tabs=azure-cli#setting-up-the-azure-ad-admin-for-the-sql-server), [PowerShell](azure-ad-authentication-sql-server-automation-setup-tutorial.md?tabs=azure-powershell#setting-up-the-azure-ad-admin-for-the-sql-server), or [ARM template](azure-ad-authentication-sql-server-automation-setup-tutorial.md?tabs=arm-template#setting-up-the-azure-ad-admin-for-the-sql-server) to set up a Microsoft Entra admin for SQL Server is available.

1. Go to the [Azure portal](https://portal.azure.com), and select **SQL Server – Azure Arc**, and select the instance for your SQL Server host.

1. Check the status of your **SQL Server - Azure Arc** resource and see if it's connected by going to the **Properties** menu. For more information, see [Validate the SQL Server - Azure Arc resources](../../../sql-server/azure-arc/connect.md?#validate-your-arc-enabled-sql-server-resources).

1. Select **Microsoft Entra ID and Purview** under **Settings** in the resource menu. 

1. Select **Set Admin** to open the **Microsoft Entra ID** pane, and choose an account to set as an admin login for SQL Server.

1. Select **Customer-managed cert** and **Select a certificate**.

1. Select **Change certificate**, and select your AKV instance and certificate that you created earlier in the new pane.

1. Select **Customer-managed app registration**.

1. Select **Change app registration**, and select the app registration you created earlier.

1. Select **Save**. This sends a request to the Arc server agent, which configures Microsoft Entra authentication for that SQL Server instance.

   :::image type="content" source="media/configure-azure-ad-for-sql-server-instance.png" alt-text="Screenshot of setting Microsoft Entra authentication in the Azure portal.":::

   It takes several minutes to download certificates and configure settings. After setting all parameters and selecting **Save** on the Azure portal, the following message may appear: `SQL Server's Azure Arc agent is currently processing a request. Values below may be incorrect. Please wait until the agent is done before continuing`. Wait until the save process is confirmed with `Saved successfully`, before attempting a Microsoft Entra login.

   The Azure Arc server agent can only update once the previous action has completed. This means that saving a new Microsoft Entra configuration before the last one has finalized can cause a failure. If you see the message **Extended call failed** when you select **Save**, wait 5 minutes and then try again.

   > [!NOTE]
   > Once the Microsoft Entra admin login is granted the `sysadmin` role, changing the Microsoft Entra admin in the Azure portal does not remove the previous login that remains as a `sysadmin`. To remove the login, it must be dropped manually.
   >
   > The Microsoft Entra admin change for the SQL Server instance takes place without a server restart, once the process is completed with the SQL Server's Azure Arc agent. For the new admin to display in `sys.server_principals`, the SQL Server instance must be restarted, and until then, the old admin is displayed. The current Microsoft Entra admin can be checked in the Azure portal.

## Create logins and users

After the Azure Arc agent on the SQL Server host has completed its operation, the admin account selected in the **Microsoft Entra ID** menu in the portal will be a `sysadmin` on the SQL Server instance. Sign into SQL Server with the Microsoft Entra admin account that has `sysadmin` permissions on the server using a client like [SSMS](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](/azure-data-studio/download-azure-data-studio).

> [!NOTE]  
> All connections to SQL Server that are done with Microsoft Entra authentication require an encrypted connection. If the Database Administrator (DBA) has not set up a trusted SSL/TLS certificate for the server, logins will likely fail with the message **The certificate chain was issued by an authority that is not trusted.** To fix this, either configure the SQL Server instance to use an SSL/TLS certificate which is trusted by the client or select **trust server certificate** in the advanced connection properties. For more information, see [Enable encrypted connections to the Database Engine](../../../database-engine/configure-windows/configure-sql-server-encryption.md).

### Create login syntax

The same syntax for creating Microsoft Entra logins and users on Azure SQL Database and Azure SQL Managed Instance can now be used on SQL Server.

> [!NOTE]  
> On SQL Server, any account that has the `ALTER ANY LOGIN` or `ALTER ANY USER` permission can create Microsoft Entra logins or users, respectively. The account doesn't need to be a Microsoft Entra login.

To create a login for a Microsoft Entra account, execute the following T-SQL command in the `master` database:

```sql
CREATE LOGIN [principal_name] FROM EXTERNAL PROVIDER;
```

For users, the principal name must be in the format `user@tenant.com`. In Microsoft Entra ID, this is the user principal name. For all other account types, like Microsoft Entra groups or applications, the principal name is the name of the Microsoft Entra object.

Here's some examples:

```sql
-- login creation for Microsoft Entra user
CREATE LOGIN [user@contoso.com] FROM EXTERNAL PROVIDER;
GO
-- login creation for Microsoft Entra group
CREATE LOGIN [my_group_name] FROM EXTERNAL PROVIDER;
GO
-- login creation for Microsoft Entra application
CREATE LOGIN [my_app_name] FROM EXTERNAL PROVIDER;
GO
```

To list the Microsoft Entra logins in the `master` database, execute the T-SQL command:

```sql
SELECT * FROM sys.server_principals
WHERE type IN ('E', 'X');
```

To grant a Microsoft Entra user membership to the `sysadmin` role (for example `admin@contoso.com`), execute the following commands in `master`:

```sql
CREATE LOGIN [admin@contoso.com] FROM EXTERNAL PROVIDER; 
GO
ALTER SERVER ROLE sysadmin ADD MEMBER [admin@contoso.com];
GO
```

The `sp_addsrvrolemember` stored procedure must be executed as a member of the SQL Server `sysadmin` server role.

### Create user syntax

You can create a database user from Microsoft Entra ID either as a database user associated with a server principal (login), or as a contained database user.

To create a Microsoft Entra user from a Microsoft Entra login in a SQL Server database, use the following syntax:

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

To create a Microsoft Entra contained database user, which is a user not tied to a server login, the following syntax can be executed:

```sql
CREATE USER [principal name] FROM EXTERNAL PROVIDER;
```

Use Microsoft Entra group name or Microsoft Entra application name as `<principal name>` when creating a Microsoft Entra database user from a group or application.

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

A new database user is given the **Connect** permission by default. All other SQL Server permissions must be explicitly granted by authorized grantors.

<a name='azure-ad-guest-accounts'></a>

### Microsoft Entra guest accounts

The `CREATE LOGIN` and `CREATE USER` syntax also supports guest users. For example, if `testuser@outlook.com` is invited to the `contoso.com` tenant, it can be added as a login to SQL Server with the same syntax as creating any other Microsoft Entra user or login. When creating guest users and logins, use the guest account's originating email, not its user principal name in the tenant. In the examples, `outlook.com` is provided even though the account is registered in the `contoso.com` tenant.

#### Create a guest user from an existing login

```sql
CREATE USER [testuser@outlook.com] FROM LOGIN [testuser@outlook.com];
```

#### Create a guest user as a contained user

```sql
CREATE USER [testuser@outlook.com] FROM EXTERNAL PROVIDER;
```

## Connect with a supported authentication method

SQL Server supports several Microsoft Entra authentication methods:

- Default 
- Username and password
- Integrated
- Universal with multifactor authentication
- Service principal
- Managed identity
- Access token

Use one of these methods to connect to the SQL Server instance. For more information, see [Microsoft Entra authentication for SQL Server](azure-ad-authentication-sql-server-overview.md).

## Authentication example using SSMS

[!INCLUDE [entra-id](../../../includes/entra-id-hard-coded.md)]

Below is the snapshot of the SQL Server Management Studio (SSMS) connection page using the authentication method **Azure Active Directory - Universal with MFA**.

:::image type="content" source="media/sql-server-management-studio-connection.png" alt-text="Screenshot SSMS showing the Connect to Server window.":::

During the authentication process, a database where the user was created must be explicitly indicated in SSMS. Expand **Options > Connection Properties > Connect to database: `database_name`**.

For more information, see [Using Microsoft Entra multifactor authentication](/azure/azure-sql/database/authentication-mfa-ssms-overview).

SQL Server tools that support Microsoft Entra authentication for Azure SQL are also supported for [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)].

<a name='location-where-azure-ad-parameters-are-stored'></a>

## Location where Microsoft Entra ID parameters are stored

> [!WARNING]  
> Microsoft Entra ID parameters are configured by the Azure Arc agent and should not be reconfigured manually.

On Linux, Microsoft Entra ID parameters are stored in `mssql-conf`. For more information about the configuration options in Linux, see [Configure SQL Server on Linux with the mssql-conf tool](../../../linux/sql-server-linux-configure-mssql-conf.md).

## Known issues

- Updating certificate doesn't propagate:
  - Once Microsoft Entra authentication is configured for SQL Server, updating the certificate in **SQL Server - Azure Arc** resource's **Microsoft Entra ID and Purview** pane may not propagate fully. This results in the save being **successful** but the old value still being displayed. To update the certificate, do the following:

    - Select **Remove Admin**.
    - Select **Save**.
    - Select **Set Admin** and reconfigure Microsoft Entra authentication with the new certificate.
    - Select **Save**.

## See also

- [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md)
- [Microsoft Entra authentication for SQL Server](azure-ad-authentication-sql-server-overview.md)
- [Linked server for SQL Server with Microsoft Entra authentication](azure-ad-authentication-sql-server-linked-server.md)

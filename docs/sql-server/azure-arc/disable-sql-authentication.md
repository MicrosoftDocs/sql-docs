---
title: Disable SQL Authentication
description: Learn how to disable SQL authentication for SQL Server 2025 enabled by Azure Arc instances, so the instance supports only Microsoft Entra and Windows authentication. Configure it from the Azure portal without restarting SQL Server.
author: PratimDasgupta
ms.author: prdasgu
ms.reviewer: vanto, randolphwest, mathoma
ms.date: 07/10/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
monikerRange: ">=sql-server-ver17"
---

# Disable SQL authentication for SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

This article describes how to disable SQL authentication for [SQL Server enabled by Azure Arc](overview.md) running [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] on Windows. When you disable SQL authentication, the instance only accepts Microsoft Entra authentication and Windows authentication. You can easily reverse this setting, and it takes effect without restarting the SQL Server instance. This setting is the SQL Server equivalent of [Microsoft Entra-only authentication](/azure/azure-sql/database/authentication-azure-ad-only-authentication) for Azure SQL Database and Azure SQL Managed Instance.

## How disabling SQL authentication works

When you disable SQL authentication on SQL Server:

- The engine blocks all **new** SQL authentication login attempts and only accepts Microsoft Entra and Windows authentication.
- Blocked SQL logins receive error 18456 (login failed).
- **Your existing logins and users stay in place.** SQL logins and contained database users that use SQL authentication keep their definitions and permissions. They just can't sign in until you re-enable SQL authentication.
- **Existing active SQL authentication sessions aren't affected.** The setting blocks only new connections.
- The setting applies at the instance (server) level, to all databases on the instance.
- The setting takes effect without a restart, after the Azure extension for SQL Server finishes applying the change. Like the other settings on the **Microsoft Entra ID** page, the extension takes several minutes to apply the configuration. The Azure portal shows a processing message while the extension works and confirms with `Saved successfully` when the change is applied. To learn how the extension applies server properties to the instance, see [Configure SQL Server enabled by Azure Arc](manage-configuration.md).

## Prerequisites

- A SQL Server enabled by Azure Arc instance running [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] on Windows. Earlier versions of SQL Server don't support disabling SQL authentication.
- The **Azure extension for SQL Server** version `1.1.3394.392` or higher. For more information, see the [release notes](release-notes.md).
- A **primary managed identity** configured for the SQL Server instance. SQL Server uses the managed identity to validate Microsoft Entra tokens. For more information, see [Microsoft Entra authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md).

## Permissions

To enable or disable SQL authentication, you need permissions to manage the Azure extension for SQL Server on the SQL Server resource in the Azure portal. High-privilege Azure roles such as [Owner](/azure/role-based-access-control/built-in-roles#owner) and [Contributor](/azure/role-based-access-control/built-in-roles#contributor) on the resource can manage the setting. The same permissions that let you set the Microsoft Entra administrator for the instance let you configure this setting.

## Prepare your environment

Disabling SQL authentication blocks **all** SQL login connections, including the `sa` login.

Before you disable SQL authentication:
- Make sure at least one Microsoft Entra login can perform the administrative tasks you need on the instance.
- Update your applications to use Microsoft Entra or Windows authentication.

Follow the principle of least privilege when you grant permissions to the administrative Microsoft Entra login by only granting the permissions it needs. For example, grant the specific **ALTER ANY LOGIN** and **ALTER ANY USER** permissions, or membership in a [server role](../../relational-databases/security/authentication-access/server-level-roles.md), rather than full **sysadmin** rights, unless broader administration is required.

If you lose data-plane access, re-enable SQL authentication from the Azure portal. Azure role-based access control, not a SQL Server connection, controls this setting.

## Migrate logins before you disable SQL authentication

Disabling SQL authentication is a hard cutoff for every SQL login and every password-based contained user, so migrate login authentication first.

To migrate login authentication, follow this sequence of steps:

1. **Identify which principals actually connect.** Metadata tells you what *exists*; auditing tells you what's *used*. Enable SQL Server Audit with the `SUCCESSFUL_LOGIN_GROUP` action group to see which logins and contained users still authenticate with SQL authentication. To list the SQL logins that exist on the instance, run the following query:

   ```sql
   SELECT name, type_desc, is_disabled, create_date
   FROM sys.server_principals
   WHERE type_desc = 'SQL_LOGIN'
   ORDER BY name;
   ```

   Then run the following query in each user database to find password-based contained users, which are also blocked:

   ```sql
   SELECT name, type_desc
   FROM sys.database_principals
   WHERE type = 'S' AND authentication_type_desc = 'DATABASE'
   ORDER BY name;
   ```

1. **Create Microsoft Entra equivalents and grant matching permissions.** For each SQL login or contained user that's still needed, create a corresponding Microsoft Entra login or user and grant the equivalent permissions. For more information, see [Microsoft Entra authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md).

1. **Update everything that connects with SQL authentication.** Reconfigure applications, SQL Server Agent jobs, linked servers, replication, and maintenance scripts to use Microsoft Entra authentication or Windows authentication, and upgrade to a client driver version that supports Microsoft Entra authentication. These features all support Microsoft Entra authentication, so move them rather than leaving them on SQL authentication.

1. **Validate that no SQL-authentication traffic remains.** Recheck your audit data and confirm every connection moved to Microsoft Entra or Windows authentication *before* you disable SQL authentication.

The following walkthrough configures passwordless authentication for Azure SQL Database but the steps for inventory, permission recreation, and validation apply equally to SQL Server: [Securing Azure SQL Database with Microsoft Entra passwordless authentication: migration guide](https://techcommunity.microsoft.com/blog/azuresqlblog/securing-azure-sql-database-with-microsoft-entra-password-less-authentication-mi/4470734)

## Disable SQL authentication

Disable SQL authentication for your [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] instance on Windows directly from the Azure portal by following these steps:

1. Go to your [SQL Server instance](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/SqlServerInstance) resource in the Azure portal.
1. Under **Settings**, select **Microsoft Entra ID** to open the **Microsoft Entra ID** pane.
   1. Select the box to **Use a primary managed identity**, if it's not already selected.
   1. Select the **Disable SQL Authentication** checkbox to disable SQL authentication.
   1. Select **Save** to save your new setting:

   :::image type="content" source="media/disable-sql-authentication/disable-sql-authentication.png" alt-text="Screenshot of the Microsoft Entra ID pane in the Azure portal with the Disable SQL Authentication checkbox selected and the Save button highlighted." lightbox="media/disable-sql-authentication/disable-sql-authentication.png":::

After you save, the Azure extension for SQL Server applies the setting to the instance. It takes several minutes: the portal shows a processing message while the extension works and confirms with `Saved successfully` when the change is applied. No restart is required. If you see **Extended call failed**, wait a few minutes and select **Save** again.

To re-enable SQL authentication, clear the **Disable SQL Authentication** checkbox and select **Save**.

To confirm the change took effect, see [Check authentication status](#check-authentication-status).

## Check authentication status

Use the [SERVERPROPERTY](../../t-sql/functions/serverproperty-transact-sql.md) function `IsExternalAuthenticationOnly` to check whether SQL authentication is disabled on the instance. A value of `1` indicates SQL authentication is disabled (Microsoft Entra and Windows authentication only); `0` indicates SQL authentication is enabled.

```sql
SELECT SERVERPROPERTY('IsExternalAuthenticationOnly');
```

## Remarks

Consider the following remarks:

- The **Disable SQL Authentication** setting requires a primary managed identity on the instance.
- You can't remove the primary managed identity while SQL authentication is disabled. Re-enable SQL authentication first.
- **Disable SQL Authentication** is cleared by default, so SQL authentication is allowed.

## Limitations

You can disable SQL authentication only for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] instances enabled by Azure Arc on Windows.

## Related content

- [Microsoft Entra authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md)
- [Securing Azure SQL Database with Microsoft Entra passwordless authentication: migration guide](https://techcommunity.microsoft.com/blog/azuresqlblog/securing-azure-sql-database-with-microsoft-entra-password-less-authentication-mi/4470734)
- [Connect your SQL Server to Azure Arc](connect.md)
- [SQL Server enabled by Azure Arc](overview.md)
- [Microsoft Entra-only authentication with Azure SQL](/azure/azure-sql/database/authentication-azure-ad-only-authentication)

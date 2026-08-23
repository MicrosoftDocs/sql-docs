---
title: Enable Least Privilege
description: Describes how to configure a service account for SQL Server enabled by Azure Arc to run with least privilege.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: nikitatakru, randolphwest, anatripathi, twright
ms.date: 08/21/2026
ms.topic: how-to
ai-usage: ai-assisted
# customer intent: As a system engineer, compliance mandates that I configure services to run with least privilege.
---

# Operate SQL Server enabled by Azure Arc with least privilege

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

The information security principle of least privilege asserts that accounts and applications only have access to the data and operations they require. With SQL Server enabled by Azure Arc, you can run the agent extension service with least privilege. This article explains how to run the agent extension service with least privilege.

[Configure Windows service accounts and permissions for Azure Extension for SQL Server](configure-windows-accounts-agent.md) describes the least privilege permissions for the agent extension service.

## Least privilege enabled by default

Least privilege is enabled by default in the following versions of Azure Extension for SQL Server:

- Version 1.1.3453.436
- Version 1.1.3518.465 and later versions

Least privilege isn't enabled by default in versions earlier than 1.1.3453.436 or in versions 1.1.3464.439, 1.1.3494.451, and 1.1.3500.453. For these versions, follow the steps in this article to enable least privilege manually.

> [!NOTE]  
> [!INCLUDE [least-privilege-default](includes/least-privilege-default.md)]

When least privilege is enabled, the Azure Extension for SQL Server automatically creates and manages `NT SERVICE\SqlServerExtension` as a local Windows service account and SQL Server login:

- The extension grants the account only the Windows permissions required to read and store configuration and write logs.
- The extension grants the login the minimum SQL Server permissions required by the enabled features.
- The extension revokes permissions when they're no longer required, such as when you disable a feature.
- The extension removes the account when you uninstall Azure Extension for SQL Server or disable least privilege.

## Prerequisites

This section identifies the system requirements and tools you need to enable or disable least privilege.

### System requirements

Configuring least privilege requires:

- [!INCLUDE [winserver2016-md](../../includes/winserver2016-md.md)] or later versions.
- [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] or later versions.
- The SQL Server service account must be a member of the **sysadmin** fixed server role.
- All databases must be online and updatable.

Least privilege isn't currently supported on Linux.

### Tools

To complete the steps in this article, you need the following tools:

- [Azure CLI](/cli/azure/)
- [`arcdata` Azure CLI extension](/azure/azure-arc/data/install-arcdata-extension) version `1.5.9` or later
- Azure Extension for SQL Server version `1.1.2859.223` or later versions to manually enable least privilege.

## Enable or disable least privilege

1. Sign in with Azure CLI.

   ```azurecli
   az login
   ```

1. Verify the `arcdata` extension version.

   ```azurecli
   az extension list -o table
   ```

   If the results include a supported version of `arcdata`, skip to the next step.

   If necessary, install or update the `arcdata` Azure CLI extension.

   To install the extension:

   ```azurecli
   az extension add --name arcdata
   ```

   To update the extension:

   ```azurecli
   az extension update --name arcdata
   ```

1. Enable least privilege with Azure CLI.

   To enable least privilege, set the `LeastPrivilege` feature flag to `true`. To disable, set the feature flag to `false`. To complete this task, run the following command with your values for the `<resource-group>` and `<machine-name>`.

   ```azurecli
   az sql server-arc extension feature-flag set --name LeastPrivilege --enable true --resource-group <resource-group> --machine-name <machine-name>
   ```

   For example, the following command enables least privilege for a server named `myserver` in a resource group named `myrg`:

   ```azurecli
   az sql server-arc extension feature-flag set --name LeastPrivilege --enable true --resource-group myrg --machine-name myserver
   ```

## Verify least privilege configuration

To verify that your SQL Server enabled by Azure Arc is configured to run with least privilege:

1. In **Services**, locate **Microsoft SQL Server Extension Service**. Verify that the service runs under the `NT SERVICE\SqlServerExtension` service account.

1. Open **Task Scheduler** on the server. Verify that an event-driven task named `SqlServerExtensionPermissionProvider` exists under `Microsoft\SqlServerExtension`.

1. Open SQL Server Management Studio and check the login named `NT SERVICE\SqlServerExtension`. Verify that the account is assigned these base permissions:

   - `CONNECT SQL`
   - `VIEW DATABASE STATE`
   - `VIEW SERVER STATE`

1. Validate the permissions with the following queries:

   To verify server-level permissions, run the following query:

   ```sql
   EXECUTE AS LOGIN = 'NT SERVICE\SqlServerExtension';

   SELECT *
   FROM fn_my_permissions(NULL, 'SERVER');

   REVERT;
   ```

   To verify database-level permissions, replace `<database-name>` with the name of one of your databases, and run the following query:

   ```sql
   EXECUTE AS LOGIN = 'NT SERVICE\SqlServerExtension';

   USE [<database-name>];
   SELECT * FROM fn_my_permissions(NULL, 'database');

   REVERT;
   ```

## Optional: Manage the SQL Server database engine service account

Use the default service account configuration for most environments. Use the following procedure only if you need to remove the SQL Server database engine service account from the **sysadmin** fixed server role between extension operations.

To use least privilege mode, the SQL Server database engine service account must be a member of the **sysadmin** fixed server role on each SQL Server instance. This membership allows the system to automatically grant or revoke permissions. By default, the SQL Server database engine service account is a member of the **sysadmin** fixed server role.

The Azure Extension for SQL Server runs a temporary process called `Deployer.exe` as `NT AUTHORITY\SYSTEM` when:

- You enable or disable features
- You add or remove SQL Server instances
- You install or upgrade the extension

`Deployer.exe` impersonates the SQL Server service account when it connects to SQL Server. Once connected, it adds or removes permissions in the server and database roles depending on which features are enabled or disabled. This process ensures that the Azure Extension for SQL Server uses the least privileges required.

If you want to manage this process with more control, such that the SQL Server service account isn't a member of the **sysadmin** fixed server role all the time, follow these steps:

1. Before you install or upgrade the extension, enable or disable a feature, or add or remove a SQL Server instance, temporarily add the SQL Server database engine service account to the **sysadmin** fixed server role.
1. Allow `Deployer.exe` to run at least once so that the permissions are set.
1. Remove the SQL Server service account from the **sysadmin** fixed server role.

Repeat this procedure whenever you install or upgrade the extension, enable or disable a feature, or add or remove a SQL Server instance. This procedure allows `Deployer.exe` to grant the least privileges required.

Because you must repeat this procedure for each extension upgrade, consider disabling automatic upgrades. You can then use a script that performs these steps and upgrades the extension.

> [!IMPORTANT]
> The Azure Extension for SQL Server `Deployer.exe` requires `NT AUTHORITY\SYSTEM` to be able to connect to SQL Server, with `CONNECT SQL` permission, in both `standard` and `least privilege` modes. This requirement exists because `Deployer.exe` always runs under the `LocalSystem` account, regardless of which service account the extension uses after provisioning.
>
> If `NT AUTHORITY\SYSTEM` can't connect to SQL Server, `Deployer.exe` can't create the `NT SERVICE\SqlServerExtension` login or grant the required permissions. Before you enable least privilege mode, verify that `NT AUTHORITY\SYSTEM` has an active SQL Server login with `CONNECT SQL` permission. To verify, see [Prerequisites](prerequisites.md).
>
> If the SQL Server database engine service account doesn't have the **sysadmin** fixed server role, [just-in-time permissions](configure-windows-accounts-agent.md#just-in-time-sql-permissions) can't be granted.

For details about when these permissions are used, see [Configure Windows service accounts and permissions for Azure Extension for SQL Server](configure-windows-accounts-agent.md).

## Related content

- [Protect SQL Server with Microsoft Defender for Cloud](configure-advanced-data-security.md)
- [Configure best practices assessment for SQL Server enabled by Azure Arc](assess.md)
- [Known issues: SQL Server enabled by Azure Arc](known-issues.md)

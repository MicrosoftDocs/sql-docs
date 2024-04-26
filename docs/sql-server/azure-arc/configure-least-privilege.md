---
title: "Enable least privilege"
description: "Describes how to configure a service account for SQL Server enabled by Azure Arc to run with least privilege."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: nikitatakru
ms.topic: how-to
ms.date: 01/17/2024

# customer intent: As a system engineer, compliance mandates that I configure services to run with least privilege. 

---

# Operate SQL Server enabled by Azure Arc with least privilege 

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

The information security principle of least privilege asserts that accounts and applications only have access to the data and operations they require. With SQL Server enabled by Azure Arc, you can run the agent extension service with least privilege. This article explains how to run the agent extension service with least privilege.

To optionally configure the service to run with least privilege, follow the steps in this article. Currently, the service does not automatically run with least privilege.

[Configure Windows service accounts and permissions for Azure Extension for SQL Server](configure-windows-accounts-agent.md) describes the least privilege permissions for the agent extension service.

> [!NOTE]
> [!INCLUDE [least-privilege-default](includes/least-privilege-default.md)]

After you configure the agent extension service to run with least privilege, it uses the `NT Service\SQLServerExtension` service account.

The `NT Service\SQLServerExtension` account is a local Windows service account:

- Created and managed by the Azure Extension for SQL Server when least privilege option is enabled.
- Granted the minimum required permissions and privileges to run the Azure extension for SQL Server service on the Windows operating system. It only has access to folders and directories used for reading and storing configuration or writing logs.
- Granted permission to connect and query in SQL Server with a new login specifically for that service account that has the minimum permissions required. Minimum permissions depend on the enabled features.
- Updated when permissions are no longer necessary. For example, permissions are revoked when you disable a feature, disable least privilege configuration, or uninstall the Azure extension for SQL Server. Revocation ensures that no permissions remain after they're no longer required.

## Prerequisites

This section identifies the system requirements and tools you need to complete the example in this article.

### System requirements

The configuration with least privilege requires:

- [!INCLUDE [winserver2012-md](../../includes/winserver2012-md.md)] or later
- SQL Server 2012 or later

The configuration with least privilege is not currently supported on Linux.

### Tools

To complete the steps in this article, you need the following tools:

- [Azure CLI](/cli/azure/)
- [`arcdata` Azure CLI extension](/azure/azure-arc/data/install-arcdata-extension) version `1.5.9` or later
- Azure extension for SQL server version `1.1.2504.99` or later

## Enable least privilege

1. Log in with Azure CLI.

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

   To enable least privilege, set the `LeastPrivilege` feature flag to `true`. To complete this task, run the following command with updated values for the `<resource-group>` and `<machine-name>`.

   ```azurecli
   az sql server-arc extension feature-flag set --name LeastPrivilege --enable true --resource-group <resource-group> --machine-name <machine-name>
   ```

   For example, the following command enables least privilege for a server named `myserver` in a resource group named `myrg`:

   ```azurecli
   az sql server-arc extension feature-flag set --name LeastPrivilege --enable true --resource-group myrg --machine-name myserver 
   ```

## Validate configuration

To verify that your SQL Server enabled by Azure Arc is configured to run with least privilege:

1. In the Windows services, locate **Microsoft SQL Server Extension Service** service. Verify that the service is running under the as the service account `NT Service\SqlServerExtension`.  

1. Open task scheduler in the server and check that a scheduled task with name `SqlServerExtensionPermissionProvider` is created under `Microsoft\SqlServerExtension`. This task runs hourly to add or remove permissions as needed based on which features are enabled and disabled.

1. Open SQL Server Management Studio and check the login named `NT Service\SqlServerExtension`. Verify that the account is assigned these permissions:

   - Connect SQL  
   - View Database State  
   - View Server State  

1. Validate the permissions with the following queries:

   To verify server level permissions, run the following query:

   ```sql  
   EXECUTE AS LOGIN = 'NT Service\SqlServerExtension'  
   SELECT * FROM fn_my_permissions (NULL, 'SERVER");
   ```

   To verify database level permissions, replace `<database name>` with the name of one of your databases, and run the following query:

   ```sql
   EXECUTE AS LOGIN = 'NT Service\SqlServerExtension'  
   USE <database name>; 
   SELECT * FROM fn_my_permissions (NULL, 'database");
   ```

## Disable least privilege

To disable least privilege, set the `LeastPrivilege` feature flag to `false`. To complete this task, run the following command with updated values for the `<resource-group>` and `<machine-name>`:

```azurecli
az sql server-arc extension feature-flag set --name LeastPrivilege --enable false --resource-group <resource-group> --machine-name <machine-name>
```

For example, the following command disables least privilege for a server named `myserver` in a resource group named `myrg`:

```azurecli
az sql server-arc extension feature-flag set --name LeastPrivilege --enable false --resource-group myrg --machine-name myserver 
```

## Related content

- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure best practices assessment on a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instance](assess.md)

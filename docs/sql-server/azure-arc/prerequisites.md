---
title: Prerequisites
description: Describes prerequisites required for SQL Server enabled by Azure Arc.
author: pochiraju
ms.author: rajpo
ms.reviewer: randolphwest
ms.date: 04/16/2026
ms.topic: checklist
ms.custom:
  - references_regions
ai-usage: ai-assisted
---

# Prerequisites - SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

An Azure Arc-enabled instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is an instance on-premises or in a cloud provider that is connected to Azure Arc. This article explains those prerequisites.

If your SQL Server virtual machines run in VMware vSphere-based environments (including environments licensed through VMware vSphere Foundation or VMware Cloud Foundation), review [Support on VMware](#support-on-vmware).

## Before you deploy

Before you can Arc-enable an instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you need to:

- Create an Azure account with an active subscription. If needed, [create a free Azure Account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- Verify [Arc connected machine agent prerequisites](/azure/azure-arc/servers/prerequisites). The Arc agent must run in the typical 'full' mode.
- Verify [Arc connected machine agent network requirements](/azure/azure-arc/servers/network-requirements).
- Open firewall to [Azure Arc data processing service](#connect-to-azure-arc-data-processing-service).
- Confirm connectivity access to the following two domains:
   - `aka.ms`
   - `*.web.core.windows.net`
- Register resource providers. Specifically:
  - `Microsoft.AzureArcData`
  - `Microsoft.HybridCompute`

  For instructions, see [Register resource providers](#register-resource-providers).

### Installation account permissions

The user or service principal needs:

- Read permission on the subscription
- Local administrator permission on the operating system to install and configure the agent
  - For Linux, use the root account
  - For Windows, use an account that's a member of the Local Administrators group

Before enabling your SQL Server instances with Arc, the installation script checks:

- The region where the Arc-enabled SQL Server is supported
- `Microsoft.AzureArcData` resource provider is registered

These checks require read permission on the subscription for the user.

To complete the task, the user or service principal needs the following permissions in the Azure resource group:

- [`Azure Connected Machine Onboarding`](/azure/role-based-access-control/built-in-roles#azure-connected-machine-onboarding) role
- `Microsoft.AzureArcData/register/action`
- `Microsoft.HybridCompute/machines/extensions/read`
- `Microsoft.HybridCompute/machines/extensions/write`
- `Microsoft.Resources/deployments/validate/action`

Assign users to built-in roles that have these permissions, such as:

- [Contributor](/azure/role-based-access-control/built-in-roles#contributor)
- [Owner](/azure/role-based-access-control/built-in-roles#owner)

For more information, see [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal).

### Verify state of user databases

When a SQL Server instance is enabled by Azure Arc, the connection sets some database permissions so that you can manage databases from Azure. For details about the permissions set at a database level, see [SQL permissions](configure-windows-accounts-agent.md#sql-permissions).

Only databases that are online and updatable are included.

Verify the state of any databases you plan to manage from Azure.

This query lists all databases, their status, and if they're updatable:

```sql
SELECT name AS DatabaseName,
       CASE WHEN state_desc = 'ONLINE' THEN 'Online'
            WHEN state_desc = 'OFFLINE' THEN 'Offline'
            ELSE 'Unknown'
       END AS Status,
       CASE WHEN is_read_only = 0 THEN 'READ_WRITE'
            ELSE 'READ_ONLY'
       END AS UpdateableStatus
FROM sys.databases;
```

Run that query on any instance that you enable.

### Service account permissions

The SQL Server service account must be a member of the **sysadmin** fixed server role on each SQL Server instance. By default, the SQL Server service account is a member of the **sysadmin** fixed server role.

For more information about this requirement, see [SQL Server service account](configure-least-privilege.md#optional-manage-the-sql-server-database-engine-service-account).

### NT AUTHORITY\SYSTEM login requirements

The Azure extension for SQL Server Deployer runs under the `LocalSystem` (`NT AUTHORITY\SYSTEM`) account to perform permission configuration. As part of this process, the deployer connects to each SQL Server instance using Windows integrated authentication.

By default, `NT AUTHORITY\SYSTEM` has a SQL Server login with `CONNECT SQL` permission. In environments where SQL Server security hardening removes or restricts the `NT AUTHORITY\SYSTEM` login (such as by disabling the login or denying `CONNECT SQL`), the Azure extension for SQL Server fails to provision successfully.

Before running this query in a production environment, review and test it in a non-production or test environment to validate the results. To verify that `NT AUTHORITY\SYSTEM` can connect to SQL Server, run the following query on each instance (review and test in a non-production or test environment before running in production):

```sql
SELECT sp.name AS login_name,
       CASE WHEN sp.is_disabled = 1 THEN 'DISABLED' ELSE 'ENABLED' END AS login_status,
       ISNULL(p.state_desc, 'NONE (implicit)') AS connect_sql_permission
FROM sys.server_principals AS sp
     LEFT OUTER JOIN sys.server_permissions AS p
         ON p.grantee_principal_id = sp.principal_id
        AND p.permission_name = N'CONNECT SQL'
        AND p.class_desc = N'SERVER'
WHERE sp.name = N'NT AUTHORITY\SYSTEM';
```

Successful provisioning requires that:

- The login exists (a row is returned)
- The login status is `ENABLED`
- `CONNECT SQL` permission is granted

If your organization determines that re-adding the `NT AUTHORITY\SYSTEM` account or granting extra permissions is acceptable for your environment, restore connectivity by creating the authentication and granting `CONNECT SQL` permission:

```sql
CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS;
GRANT CONNECT SQL TO [NT AUTHORITY\SYSTEM];
```

After making changes, verify that the extension provisions successfully.

### Set proxy exclusions

> [!NOTE]
> Starting with the April 2024 release (extension version 1.1.2986.256), you don't need to set these exclusions. You can also use `NO_PROXY` to bypass the proxy for specific URLs while routing all other requests through the proxy server. For example, use `NO_PROXY` to route requests to Azure Key Vault through private endpoints.

Set the `NO_PROXY` environment variable to exclude proxy traffic for the following addresses when either condition applies:

- The extension version is earlier than 1.1.2986.256.
- The machine has a system-level `HTTPS_PROXY` environment variable, regardless of the extension version. This condition prevents requests to the local Azure Arc identity endpoint (`https://localhost:40341`) from being routed through the proxy.

Exclude these addresses:

- `localhost`
- `127.0.0.1`

### Connect to Azure Arc data processing service

[!INCLUDE [data-processing-service-permission](includes/data-processing-service-permission.md)]

> [!NOTE]  
> You can't use Azure Private Link connections to the Azure Arc data processing service. See [Unsupported configurations](#unsupported-configurations).

### Network requirements for enabling Microsoft Entra authentication

[!INCLUDE [entra-id-authentication-prerequisites](includes/entra-id-authentication-prerequisites.md)]

## Supported extension versions

Only extensions released within the last 12 months are supported. If you contact support and the extension version is out of support, the support team might ask you to update to the latest version before proceeding with troubleshooting. For version history and release dates, see [Release Notes](release-notes.md).

## Supported SQL Server versions and environments

[!INCLUDE [supported-configurations](includes/supported-configurations.md)]

## Unsupported configurations

[!INCLUDE [unsupported-configurations](includes/unsupported-configurations.md)]

## Register resource providers

To register the resource providers, use one of the following methods:

## [Azure portal](#tab/azure)

1. Select **Subscriptions**.
1. Choose your subscription.
1. Under **Settings**, select **Resource providers**.
1. Search for `Microsoft.AzureArcData` and `Microsoft.HybridCompute` and select **Register**.

## [PowerShell](#tab/powershell)

Run:

```powershell
Register-AzResourceProvider -ProviderNamespace Microsoft.HybridCompute
Register-AzResourceProvider -ProviderNamespace Microsoft.AzureArcData
```

## [Azure CLI](#tab/az)

Run:

```azurecli
az provider register --namespace 'Microsoft.HybridCompute'
az provider register --namespace 'Microsoft.AzureArcData'
```

---

## Azure subscription and service limits

Before configuring your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances and machines with Azure Arc, review the Azure Resource Manager [subscription limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#subscription-limits) and [resource group limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#resource-group-limits) to plan for the number of machines to connect.

## Supported regions

[!INCLUDE [azure-arc-data-regions](includes/azure-arc-data-regions.md)]

## Install Azure extension for SQL Server

The [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Setup Installation Wizard doesn't support installation of the Azure extension for SQL Server.

You can install this component in two ways:

- [SQL Server enabled by Azure Arc deployment options](deployment-options.md)
- [Install Azure extension for SQL Server from the command line](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#install-and-connect-to-azure)

For VMware vSphere-based environments, review [Support on VMware](#support-on-vmware).

## Related content

- [SQL Server enabled by Azure Arc](overview.md)
- [Known issues: SQL Server enabled by Azure Arc](known-issues.md)

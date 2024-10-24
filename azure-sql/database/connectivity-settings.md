---
title: Connectivity settings for Azure SQL Database and Azure Synapse Analytics
titleSuffix: Azure SQL Database and Azure Synapse Analytics
description: This article explains the Transport Layer Security (TLS) version choice and the Proxy versus Redirect settings for Azure SQL Database and Azure Synapse Analytics.
author: rohitnayakmsft
ms.author: rohitna
ms.reviewer: wiassaf, mathoma, vanto
ms.service: azure-sql-database
ms.date: 10/24/2024
ms.subservice: connect
ms.topic: how-to
ms.custom:
  - devx-track-azurepowershell
  - devx-track-azurecli
ms.devlang: azurecli
---
# Connectivity settings for Azure SQL Database and Azure Synapse Analytics
[!INCLUDE [appliesto-sqldb-asa](../includes/appliesto-sqldb-asa-formerly-sqldw.md)]

This article introduces settings that control connectivity to the server for Azure SQL Database and [dedicated SQL pool (formerly SQL DW)](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) in Azure Synapse Analytics.

- For more information on various components that direct network traffic and connection policies, see [connectivity architecture](connectivity-architecture.md).
- This article does not apply to Azure SQL Managed Instance, instead see [Connect your application to Azure SQL Managed Instance](../managed-instance/connect-application-instance.md).
- This article does not apply to dedicated SQL pools in Azure Synapse Analytics workspaces. See [Azure Synapse Analytics IP firewall rules](/azure/synapse-analytics/security/synapse-workspace-ip-firewall) for guidance on how to configure IP firewall rules for Azure Synapse Analytics with workspaces.

## Networking and connectivity

You can change these settings in your [logical server](logical-servers.md). A logical SQL server can host both Azure SQL databases and standalone dedicated SQL pools not in an Azure Synapse Analytics workspace.

> [!NOTE]
> These settings apply to Azure SQL databases and dedicated SQL pools (formerly SQL DW) associated with the logical server. These instructions do not apply to dedicated SQL pools in an Azure Synapse analytics workspace. 

:::image type="content" source="media/connectivity-settings/manage-connectivity-settings.png" alt-text="Screenshot of the Firewalls and virtual networks settings in Azure portal for SQL server.":::

## Change public network access

It's possible to change the public network access for your Azure SQL Database or standalone dedicated SQL pool via the Azure portal, Azure PowerShell, and the Azure CLI.

> [!NOTE]
> These settings take effect immediately after they're applied. Your customers might experience connection loss if they don't meet the requirements for each setting.

### [Portal](#tab/azure-portal)

To enable public network access for the logical server hosting your databases:

1. Go to the Azure portal, and go to the [logical server in Azure](logical-servers.md).
1. Under **Security**, select the **Networking** page.
1. Choose the **Public access** tab, and then set the **Public network access** to **Select networks**.

From this page, you can add a virtual network rule, as well as configure firewall rules for your public endpoint.

Choose the **Private access** tab to configure a [private endpoint](private-endpoint-overview.md).


### [PowerShell](#tab/azure-powershell)

It's possible to change public network access by using Azure PowerShell.

> [!IMPORTANT]
> Azure SQL Database still supports the PowerShell Azure Resource Manager module, but all future development is for the Az.Sql module. For these cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/). The arguments for the commands in the Az module and in the AzureRm modules are substantially identical. The following script requires the [Azure PowerShell module](/powershell/azure/install-az-ps).

The following PowerShell script shows how to `Get` and `Set` the **Public Network Access** property at the server level:

```powershell
# Get the Public Network Access property
(Get-AzSqlServer -ServerName sql-server-name -ResourceGroupName sql-server-group).PublicNetworkAccess

# Update Public Network Access to Disabled
$SecureString = ConvertTo-SecureString "password" -AsPlainText -Force

Set-AzSqlServer -ServerName sql-server-name -ResourceGroupName sql-server-group -SqlAdministratorPassword $SecureString -PublicNetworkAccess "Disabled"
```

### [Azure CLI](#tab/azure-cli)

It's possible to change the public network settings by using the [Azure CLI](/cli/azure/install-azure-cli). 

The following CLI script shows how to change the **Public Network Access** setting in a Bash shell:

```azurecli-interactive

# Get current setting for Public Network Access
az sql server show -n sql-server-name -g sql-server-group --query "publicNetworkAccess"

# Update setting for Public Network Access
az sql server update -n sql-server-name -g sql-server-group --set publicNetworkAccess="Disabled"
```

---

## Deny public network access

The default for the **Public network access** setting is **Disable**. Customers can choose to connect to a database by using either public endpoints (with IP-based server-level firewall rules or with virtual-network firewall rules), or [private endpoints](private-endpoint-overview.md) (by using Azure Private Link), as outlined in the [network access overview](network-access-controls-overview.md).

When **Public network access** is set to **Disable**, only connections from private endpoints are allowed. All connections from public endpoints will be denied with an error message similar to:  

```output
Error 47073
An instance-specific error occurred while establishing a connection to SQL Server. 
The public network interface on this server is not accessible. 
To connect to this server, use the Private Endpoint from inside your virtual network.
```

When **Public network access** is set to **Disable**, any attempts to add, remove, or edit any firewall rules will be denied with an error message similar to:

```output
Error 42101
Unable to create or modify firewall rules when public network interface for the server is disabled. 
To manage server or database level firewall rules, please enable the public network interface.
```

Ensure that **Public network access** is set to **Selected networks** to be able to add, remove, or edit any firewall rules for Azure SQL Database and Azure Synapse Analytics.

## Minimum TLS version

The minimum [Transport Layer Security (TLS)](https://support.microsoft.com/help/3135244/tls-1-2-support-for-microsoft-sql-server) version setting allows customers to choose which version of TLS their SQL database uses. It's possible to change the minimum TLS version by using the Azure portal, Azure PowerShell, and the Azure CLI.

Currently, Azure SQL Database and dedicated SQL pools (formerly SQL DW) support TLS 1.0, 1.1, 1.2, and 1.3. Starting November 2024, it will only support TLS 1.2 and 1.3. For more information, see [TLS 1.0 and 1.1 deprecation](https://azure.microsoft.com/en-us/updates/azure-support-tls-will-end-by-31-october-2024-2/). Setting a minimum TLS version ensures that newer TLS versions are supported. For example, choosing a TLS version 1.2 means only connections with TLS 1.2 and 1.3 are accepted, and connections with TLS 1.1 are rejected. After you test to confirm that your applications support it, we recommend setting the minimal TLS version to 1.2. This version includes fixes for vulnerabilities in previous versions and is the highest version of TLS that's supported in Azure SQL Database.

### Upcoming retirement changes 

Azure has announced that support for older TLS versions (TLS 1.0, and 1.1) ends October 31, 2024. For more information, see [TLS 1.0 and 1.1 deprecation](https://azure.microsoft.com/updates/azure-support-tls-will-end-by-31-october-2024-2/).

Starting November 2024, you will no longer be able to set the minimal TLS version for Azure SQL Database and Azure Synapse Analytics client connections below TLS 1.2. 

### Configure minimum TLS version 

You can configure the minimum TLS version for client connections by using the Azure portal, Azure PowerShell, or the Azure CLI.

> [!CAUTION]
> - The default for the minimal TLS version is to allow all versions. After you enforce a version of TLS, it's not possible to revert to the default.
> - Enforcing a minimum of TLS 1.3 might cause issues for connections from clients that don't support TLS 1.3 since not all [drivers](/sql/connect/driver-feature-matrix) and operating systems support TLS 1.3.

For customers with applications that rely on older versions of TLS, we recommend setting the minimal TLS version according to the requirements of your applications. If application requirements are unknown or workloads rely on older drivers that are no longer maintained, we recommend not setting any minimal TLS version.

For more information, see [TLS considerations for SQL Database connectivity](connect-query-content-reference-guide.md#tls-considerations-for-database-connectivity).

After you set the minimal TLS version, customers who are using a TLS version lower than the minimum TLS version of the server will fail to authenticate, with the following error:

```output
Error 47072
Login failed with invalid TLS version
```

> [!NOTE]
> The minimum TLS version is enforced at the application layer. Tools that attempt to determine TLS support at the protocol layer might return TLS versions in addition to the minimum required version when run directly against the SQL Database endpoint.

### [Portal](#tab/azure-portal)

1. Go to the Azure portal, and go to the [logical server in Azure](logical-servers.md).
1. Under **Security**, select the **Networking** page.
1. Choose the **Connectivity** tab. Select the **Minimum TLS Version** desired for all databases associated with the server, and select **Save**.

:::image type="content" source="media/connectivity-settings/minimal-tls-version.png" alt-text="Screenshot of the Connectivity tab of the Networking settings for your logical server, minimal TLS version drop-down selected." lightbox="media/connectivity-settings/minimal-tls-version.png":::

### [PowerShell](#tab/azure-powershell)

It's possible to change the minimum TLS version by using Azure PowerShell. 

> [!IMPORTANT]
> Azure SQL Database still supports the PowerShell Azure Resource Manager module, but all future development is for the Az.Sql module. For these cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/). The arguments for the commands in the Az module and in the AzureRm modules are substantially identical. The following script requires the [Azure PowerShell module](/powershell/azure/install-az-ps).

The following PowerShell script shows how to `Get` the **Minimal TLS Version** property at the logical server level:

```powershell
$serverParams = @{
    ServerName = "sql-server-name"
    ResourceGroupName = "sql-server-group"
}

(Get-AzSqlServer @serverParams).MinimalTlsVersion
```

To `Set` the **Minimal TLS Version** property at the logical server level, substitute your Sql Administrator password for `strong_password_here_password`, and execute:

```powershell
$serverParams = @{
    ServerName = "sql-server-name"
    ResourceGroupName = "sql-server-group"
    SqlAdministratorPassword = (ConvertTo-SecureString "strong_password_here_password" -AsPlainText -Force)
    MinimalTlsVersion = "1.2"
}
Set-AzSqlServer @serverParams
```

### [Azure CLI](#tab/azure-cli)

It's possible to change the minimum TLS settings by using the Azure CLI. 

> [!IMPORTANT]
> All scripts in this section require the [Azure CLI](/cli/azure/install-azure-cli).


The following CLI script shows how to change the **Minimal TLS Version** setting in a Bash shell:

```azurecli-interactive
# Get current setting for Minimal TLS Version
az sql server show -n sql-server-name -g sql-server-group --query "minimalTlsVersion"

# Update setting for Minimal TLS Version
az sql server update -n sql-server-name -g sql-server-group --set minimalTlsVersion="1.2"
```

---

## Identify client connections 

You can use the Azure portal and SQL audit logs to identify clients that are connecting using TLS 1.0 and 1.0. 

In the Azure portal, go to **Metrics** under **Monitoring** for your database resource, and then filter by *Successful connections*, and *TLS versions* = `1.0` and `1.1`:

:::image type="content" source="media/connectivity-settings/connections-in-portal.png" alt-text="Screenshot of the montoring page for the database resource in the Azure portal with successful tls 1.0 and 1.1 connections filtered. "::: 
 
You can also query [sys.fn_get_audit_file](/sql/relational-databases/system-functions/sys-fn-get-audit-file-transact-sql) directly within your database to view the `client_tls_version_name` in the audit file: 

:::image type="content" source="media/connectivity-settings/tls-entries-in-audit-file.png" alt-text="Screenshot of a query result of the audit file showing tls version connections. ":::


## Change the connection policy

[Connection policy](connectivity-architecture.md#connection-policy) determines how customers connect. We highly recommend the `Redirect` connection policy over the `Proxy` connection policy for the lowest latency and highest throughput.

It's possible to change the connection policy by using the Azure portal, Azure PowerShell, and the Azure CLI.

### [Portal](#tab/azure-portal)

It's possible to change your connection policy for your logical server by using the Azure portal.

1. Go to the Azure portal. Go to the [logical server in Azure](logical-servers.md).
1. Under **Security**, select the **Networking** page.
1. Choose the **Connectivity** tab. Choose the desired connection policy, and select **Save**.

:::image type="content" source="media/connectivity-settings/change-connection-policy.png" alt-text="Screenshot of the Connectivity tab of the Networking page, Connection policy selected." lightbox="media/connectivity-settings/change-connection-policy.png":::

### [PowerShell](#tab/azure-powershell)

It's possible to change the connection policy for your logical server by using Azure PowerShell.

> [!IMPORTANT]
> Azure SQL Database still supports the PowerShell Azure Resource Manager module, but all future development is for the Az.Sql module. For these cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/). The arguments for the commands in the Az module and in the AzureRm modules are substantially identical. The following script requires the [Azure PowerShell module](/powershell/azure/install-az-ps).

The following PowerShell script shows how to change the connection policy by using PowerShell:

```powershell
# Get SQL Server ID
$sqlserverid = (Get-AzSqlServer -ServerName sql-server-name -ResourceGroupName sql-server-group).ResourceId

# Set URI
$id = "$sqlserverid/connectionPolicies/Default"

# Get current connection policy
$resourceParams = @{
    ResourceId = $id
    ApiVersion = "2014-04-01"
    Verbose = $true
}
(Get-AzResource @resourceParams).Properties.ConnectionType

# Update connection policy
$updateParams = @{
    ResourceId = $id
    Properties = @{
        connectionType = "Proxy"
    }
    Force = $true
}
Set-AzResource @updateParams
```

### [Azure CLI](#tab/azure-cli)

It's possible to change the connection policy for your logical server by using the Azure CLI. 

> [!IMPORTANT]
> All scripts in this section require the [Azure CLI](/cli/azure/install-azure-cli).

### Azure CLI in a Bash shell

The following CLI script shows how to change the connection policy in a Bash shell:

```azurecli-interactive
# Get SQL Server ID
sqlserverid=$(az sql server show -n sql-server-name -g sql-server-group --query 'id' -o tsv)

# Set URI
ids="$sqlserverid/connectionPolicies/Default"

# Get current connection policy
az resource show --ids $ids

# Update connection policy
az resource update --ids $ids --set properties.connectionType=Proxy
```

### Azure CLI from a Windows command prompt

The following CLI script shows how to change the connection policy from a Windows command prompt (with the Azure CLI installed):

```azurecli
# Get SQL Server ID and set URI
FOR /F "tokens=*" %g IN ('az sql server show --resource-group myResourceGroup-571418053 --name server-538465606 --query "id" -o tsv') do (SET sqlserverid=%g/connectionPolicies/Default)

# Get current connection policy
az resource show --ids %sqlserverid%

# Update connection policy
az resource update --ids %sqlserverid% --set properties.connectionType=Proxy
```

---

## Related content

- [Azure SQL Database and Azure Synapse Analytics connectivity architecture](connectivity-architecture.md)
- [conn-policy](/cli/azure/sql/server/conn-policy)

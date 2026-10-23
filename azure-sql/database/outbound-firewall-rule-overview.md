---
title: Outbound Firewall Rules
titleSuffix: Azure SQL Database and Azure Synapse Analytics
description: Overview of the outbound firewall rules feature for Azure SQL Database and Azure Synapse Analytics.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, vanto
ms.date: 08/12/2025
ms.service: azure-sql-database
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
monikerRange: "=azuresql || =azuresql-db"
---

# Outbound firewall rules for Azure SQL Database and Azure Synapse Analytics

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa-formerly-sqldw.md)] 

Outbound firewall rules limit network traffic from the [Azure SQL Database logical server](logical-servers.md) to a customer defined list of Azure Storage accounts and Azure SQL Database logical servers. Any attempt to access storage accounts or databases not in this list is denied. Traffic is limited only for the following [Azure SQL Database](sql-database-paas-overview.md) features:

- [Auditing](auditing-overview.md?view=azuresql-db&preserve-view=true)
- [Vulnerability assessment](/azure/defender-for-cloud/sql-azure-vulnerability-assessment-overview)
- [Import/Export service](database-import-export-azure-services-off.md?view=azuresql-db&preserve-view=true)
- [OPENROWSET](/sql/t-sql/functions/openrowset-transact-sql?view=azuresqldb-current&preserve-view=true)
- [Bulk Insert](/sql/t-sql/statements/bulk-insert-transact-sql?view=azuresqldb-current&preserve-view=true)
- [sp_invoke_external_rest_endpoint](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql?view=azuresqldb-current&preserve-view=true)

Outbound traffic from features not on this list isn't affected.

> [!IMPORTANT]
> - This article applies to both Azure SQL Database and [dedicated SQL pool (formerly SQL DW)](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) in Azure Synapse Analytics. These settings apply to all SQL Database and dedicated SQL pool (formerly SQL DW) databases associated with the server. For simplicity, the term 'database' refers to both databases in Azure SQL Database and Azure Synapse Analytics. Likewise, any references to 'server' is referring to the [logical SQL server](logical-servers.md) that hosts Azure SQL Database and dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics. This article does *not* apply to Azure SQL Managed Instance or dedicated SQL pools in Azure Synapse Analytics workspaces.
> - Outbound firewall rules are defined at the [logical server](logical-servers.md). Geo-replication and failover groups require the same set of rules to be defined on the primary and all secondaries.

## Set outbound firewall rules in the Azure portal

1. Under **Security**, select **Networking**.
1. Select the **Connectivity** tab. Under **Outbound networking**, select the link to **Configure outbound networking restrictions**.

   :::image type="content" source="media/outbound-firewall-rule-overview/configure-outbound-networking-restrictions.png" alt-text="Screenshot of Outbound Networking section, and the configure outbound networking restrictions link.":::  

   This will open up the following pane:

   :::image type="content" source="media/outbound-firewall-rule-overview/restrict-outbound-networking.png" alt-text="Screenshot of Outbound Networking pane with nothing selected.":::

1. Select the check box titled **Restrict outbound networking**. 
1. Select the **Add domain** button and provide the fully-qualified domain name for the Storage accounts (or databases in Azure SQL Database).

1. After you're done, you should see a screen similar to the one below. Select **OK** to apply these settings.

   :::image type="content" source="media/outbound-firewall-rule-overview/fully-qualified-domain-names.png" alt-text="Screenshot of Outbound Networking pane after fully-qualified domain names are added.":::

## Set outbound firewall rules using PowerShell

> [!IMPORTANT]
> The PowerShell Azure Resource Manager (AzureRM) module was deprecated on February 29, 2024. All future development should use the Az.Sql module. Users are advised to migrate from AzureRM to the Az PowerShell module to ensure continued support and updates. The AzureRM module is no longer maintained or supported. The arguments for the commands in the Az PowerShell module and in the AzureRM modules are substantially identical. For more about their compatibility, see [Introducing the new Az PowerShell module](/powershell/azure/new-azureps-module-az).

The following script requires the [Azure PowerShell module](/powershell/azure/install-az-ps). 

The following PowerShell script shows how to change the outbound networking setting (using the `RestrictOutboundNetworkAccess` property):

```powershell
# Get current settings for Outbound Networking
(Get-AzSqlServer -ServerName <SqlServerName> -ResourceGroupName <ResourceGroupName>).RestrictOutboundNetworkAccess

# Update setting for Outbound Networking
$SecureString = ConvertTo-SecureString "<ServerAdminPassword>" -AsPlainText -Force

Set-AzSqlServer -ServerName <SqlServerName> -ResourceGroupName <ResourceGroupName> -SqlAdministratorPassword $SecureString  -RestrictOutboundNetworkAccess "Enabled"
```

Use these PowerShell cmdlets to configure outbound firewall rules:

```powershell
# List all Outbound Firewall Rules
Get-AzSqlServerOutboundFirewallRule -ServerName <SqlServerName> -ResourceGroupName <ResourceGroupName>

# Add an Outbound Firewall Rule
New-AzSqlServerOutboundFirewallRule -ServerName <SqlServerName> -ResourceGroupName <ResourceGroupName> -AllowedFQDN testOBFR1

# List a specific Outbound Firewall Rule
Get-AzSqlServerOutboundFirewallRule -ServerName <SqlServerName> -ResourceGroupName <ResourceGroupName> -AllowedFQDN <StorageAccountFQDN>

#Delete an Outbound Firewall Rule
Remove-AzSqlServerOutboundFirewallRule -ServerName <SqlServerName> -ResourceGroupName <ResourceGroupName> -AllowedFQDN <StorageAccountFQDN>
```

## Set outbound firewall rules using the Azure CLI

> [!IMPORTANT]
> All scripts in this section require the [Azure CLI](/cli/azure/install-azure-cli).

### Azure CLI in a bash shell

The following CLI script shows how to change the outbound networking setting (using the **restrictOutboundNetworkAccess** property) in a bash shell:

```azurecli-interactive
# Get current setting for Outbound Networking 
az sql server show -n sql-server-name -g sql-server-group --query "restrictOutboundNetworkAccess"

# Update setting for Outbound Networking
az sql server update -n sql-server-name -g sql-server-group --set restrictOutboundNetworkAccess="Enabled"
```

Use these CLI commands to configure outbound firewall rules:

```azurecli-interactive
# List a server's outbound firewall rules.
az sql server outbound-firewall-rule list -g sql-server-group -s sql-server-name

# Create a new outbound firewall rule
az sql server outbound-firewall-rule create -g sql-server-group -s sql-server-name --outbound-rule-fqdn allowedFQDN

# Show the details for an outbound firewall rule.
az sql server outbound-firewall-rule show -g sql-server-group -s sql-server-name --outbound-rule-fqdn allowedFQDN

# Delete the outbound firewall rule.
az sql server outbound-firewall-rule delete -g sql-server-group -s sql-server-name --outbound-rule-fqdn allowedFQDN
```

## Related content

- [Overview of Azure SQL Database and SQL Managed Instance security capabilities](security-overview.md?view=azuresql-db&preserve-view=true)
- [Connectivity architecture](connectivity-architecture.md?view=azuresql-db&preserve-view=true)
- [Azure SQL Database network access controls](network-access-controls-overview.md?view=azuresql-db&preserve-view=true)
- [Azure Private Link for Azure SQL Database](private-endpoint-overview.md?view=azuresql-db&preserve-view=true)

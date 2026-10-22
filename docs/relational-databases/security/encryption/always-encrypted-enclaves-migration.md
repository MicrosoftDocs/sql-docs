---
title: "Always Encrypted with Intel SGX enclaves migration guide"
description: Learn how to migrate from Always Encrypted with Intel SGX enclaves before support ends.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto
ms.date: 09/01/2026
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ai-usage: ai-assisted
---

# Always Encrypted with Intel SGX enclaves migration guide

[!INCLUDE [asdb](../../../includes/applies-to-version/_asdb.md)]

> [!IMPORTANT]
> Always Encrypted with Intel Software Guard Extensions (Intel SGX) enclaves reaches the end of support on October 31, 2027. Migrate affected databases before this date. After October 31, 2027, Azure automatically moves any database that remains on the DC-series compute tier to a supported standard-series (non-DC) compute tier and enables virtualization-based security (VBS) enclaves.

This article describes the alternatives to Always Encrypted with Intel SGX enclaves and the changes required for each alternative. Review the [security considerations](always-encrypted-enclaves.md#security-considerations) before you choose an alternative. Intel SGX and VBS enclaves provide different protections against attacks originating from the guest operating system and the host.

Before you begin, confirm that you can view and modify the target Azure SQL logical servers, databases, and elastic pools. For PowerShell, install the Az PowerShell modules and sign in to Azure. For Azure CLI, install the Azure CLI and sign in to Azure. Inventory the applications that connect to affected databases so that you can update their drivers, connection strings, and attestation settings during migration.

## Identify databases that use DC-series

Identify all standalone databases and elastic pools that use DC-series before you plan the migration. Every database in a DC-series elastic pool is affected.

### [Azure portal](#tab/azure-portal)

1. In the [Azure portal](https://portal.azure.com), go to your Azure SQL logical server.
1. On the **Overview** page, locate **Available resources**. This table lists the databases on the logical server.
1. In the **Pricing tier** column, select the filter, and then filter the list for **DC-series**.
1. Record each database in the filtered list. These databases use DC-series and Intel SGX enclaves.
1. Repeat these steps for each logical server that hosts Azure SQL databases in your environment.

### [PowerShell](#tab/azure-powershell)

Set `$resourceGroupName` and `$serverName`, and then run the following script. The script returns standalone DC-series databases and databases in DC-series elastic pools on the specified [logical server](/azure/azure-sql/database/logical-servers).

```azurepowershell-interactive
$resourceGroupName = "<resource-group-name>"
$serverName = "<server-name>"

$databases = Get-AzSqlDatabase `
	-ResourceGroupName $resourceGroupName `
	-ServerName $serverName

$standaloneDatabases = $databases | Where-Object {
	-not $_.ElasticPoolName -and
	$_.CurrentServiceObjectiveName -match '(^|_)DC(_|$)'
}

$dcPoolNames = Get-AzSqlElasticPool `
	-ResourceGroupName $resourceGroupName `
	-ServerName $serverName | ForEach-Object {
		$poolResource = Get-AzResource -ResourceId $_.ResourceId -ExpandProperties
		if ($poolResource.Sku.Family -eq 'DC') {
			$_.ElasticPoolName
		}
	}

$pooledDatabases = $databases | Where-Object {
	$_.ElasticPoolName -in $dcPoolNames
}

@($standaloneDatabases) + @($pooledDatabases) |
	Select-Object DatabaseName, ElasticPoolName, Edition, CurrentServiceObjectiveName
```

Run the script for each logical server that hosts Azure SQL databases in your environment. If the script returns no rows, the specified server has no databases that use DC-series.

### [Azure CLI](#tab/azure-cli)

Set the resource group and logical server name.

```azurecli-interactive
resourceGroupName="<resource-group-name>"
serverName="<server-name>"
```

Sign in to Azure:

```azurecli-interactive
az login
```

List standalone databases that use DC-series:

```azurecli-interactive
az sql db list \
    --resource-group $resourceGroupName \
    --server $serverName \
    --query "[?elasticPoolName == null && currentSku.family == 'DC'].{Database:name, ServiceObjective:currentServiceObjectiveName}" \
    --output table
```

Get the DC-series elastic pools on the server:

```azurecli-interactive
dcPoolNames=$(az sql elastic-pool list \
    --resource-group $resourceGroupName \
    --server $serverName \
    --query "[?sku.family == 'DC'].name" \
    --output tsv)
```

For each DC-series elastic pool returned by the previous command, list its databases:

```azurecli-interactive
for poolName in $dcPoolNames; do
    az sql elastic-pool list-dbs \
        --resource-group $resourceGroupName \
        --server $serverName \
        --name $poolName \
        --query "[].{PoolName:elasticPoolName, Database:name}" \
        --output table
done
```

Run the commands for each logical server that hosts Azure SQL databases in your environment.

---

## Choose a migration path

Choose the migration path that meets the security and application requirements of your workload. Use the following comparison as the starting point, and review the detailed guidance for the selected path before making production changes.

| Migration path | Use this option when | Attestation |
| --- | --- | --- |
| Azure SQL Database with VBS enclaves | You want to remain on Azure SQL Database and VBS enclaves meet your security requirements. | VBS enclaves in Azure SQL Database don't support attestation. |
| SQL Server on an Azure confidential VM with VBS enclaves | You require a hardware-enforced boundary that helps protect the guest operating system from host operator access. | Host Guardian Service (HGS) attestation is optional. |

## Migrate a single database to VBS enclaves

Use this path to retain enclave-enabled capabilities in Azure SQL Database.

1. Select a [supported standard-series (non-DC) hardware configuration](/azure/azure-sql/database/service-tiers-sql-database-vcore#select-hardware-configuration) that meets the performance and availability requirements of your workload.
1. [Move the database to the selected hardware configuration](/azure/azure-sql/database/service-tiers-sql-database-vcore#select-hardware-configuration).
1. [Enable VBS enclaves for the database](/azure/azure-sql/database/always-encrypted-enclaves-enable#enable-vbs-enclaves-using-azure-portal). Enabling VBS enclaves sets the `preferredEnclaveType` database property to `VBS`.
1. Review the [client driver requirements for VBS enclaves without attestation](always-encrypted-enclaves-client-development.md#client-drivers-for-always-encrypted-with-secure-enclaves), and update your application driver if necessary.
1. Update each application connection to use the `None` enclave attestation protocol, and remove the Microsoft Azure Attestation URL. The exact connection-string keywords depend on the client driver.
1. Complete the [post-migration validation](#validate-the-migration).

## Migrate an elastic pool to VBS enclaves

All databases in an elastic pool inherit the enclave configuration of the pool. Use this path to retain enclave-enabled capabilities for databases in an Azure SQL elastic pool.

1. Select a supported standard-series (non-DC) configuration that meets the performance and availability requirements of the pool. For information about changing pool configuration, see [Manage an elastic pool in Azure SQL Database](/azure/azure-sql/database/elastic-pool-manage).
1. [Enable VBS enclaves for the elastic pool](/azure/azure-sql/database/always-encrypted-enclaves-enable#enable-a-vbs-enclave-for-an-existing-database-or-elastic-pool). Enabling VBS enclaves sets the `preferredEnclaveType` pool property to `VBS`.
1. Review the [client driver requirements for VBS enclaves without attestation](always-encrypted-enclaves-client-development.md#client-drivers-for-always-encrypted-with-secure-enclaves), and update your application drivers if necessary.
1. Update each application connection to use the `None` enclave attestation protocol, and remove the Microsoft Azure Attestation URL. The exact connection-string keywords depend on the client driver.
1. Complete the [post-migration validation](#validate-the-migration) for every database in the pool.

## Migrate to SQL Server on an Azure confidential VM

Consider this path if you require a hardware-enforced boundary that helps protect the guest operating system from host operator access. Azure confidential VMs encrypt VM memory and provide different security properties from Intel SGX enclaves. Evaluate these differences against your security and compliance requirements.

1. [Deploy SQL Server to an Azure confidential VM](/azure/azure-sql/virtual-machines/windows/sql-vm-create-confidential-vm-how-to).
1. Choose whether to use enclave attestation:
    - [Plan for Always Encrypted with secure enclaves in SQL Server without attestation](always-encrypted-enclaves-no-attestation-plan.md).
    - [Plan for Host Guardian Service attestation](always-encrypted-enclaves-host-guardian-service-plan.md).
1. Configure Always Encrypted with VBS enclaves on the SQL Server instance by following the guidance for the attestation option you selected.
1. Plan the migration of your database, logins, keys, application connectivity, and dependent resources.
1. Choose a data migration option based on your database size, network configuration, downtime requirements, and supported database objects. Common options include:
    - **Azure Data Factory**: Use a copy activity with the [Azure SQL Database connector](/azure/data-factory/connector-azure-sql-database) as the source and the [SQL Server connector](/azure/data-factory/connector-sql-server) as the sink. ADF treats the Always Encrypted columns as binary or ciphertext values and moves them without needing access to the Column Master Key.
    - **Smart Bulk Copy**: Use [Smart Bulk Copy](https://techcommunity.microsoft.com/blog/modernizationbestpracticesblog/database-migration--reverse-migration-between-azure-sql-dbmi-and-sql-server-usin/3264451) to copy schema and data from Azure SQL Database to SQL Server. Review the tool's prerequisites and limitations before migration.
    - **BACPAC**: Consider a BACPAC for smaller databases whose objects are supported by data-tier applications. Always Encrypted data remains encrypted during export and import, and the BACPAC includes the Always Encrypted key metadata. For more information, see [Export and import databases using Always Encrypted](always-encrypted-migrate-using-bacpac.md), [Export a BACPAC file](../../../tools/sql-database-projects/concepts/data-tier-applications/export-bacpac-file.md), and [Import a BACPAC file to create a new database](../../../tools/sql-database-projects/concepts/data-tier-applications/import-bacpac-file-create-new-database.md).
1. Update application connection strings for the SQL Server instance and the selected attestation option.
1. Complete the [post-migration validation](#validate-the-migration).

## Validate the migration

Before you move the workload to production:

1. Verify that applications can connect with Always Encrypted enabled.
1. Run representative queries that use encrypted columns, including queries that require enclave computations if the target environment uses secure enclaves.
1. Verify that inserts, updates, deletes, and index operations on encrypted columns behave as expected.
1. Test application performance and adjust the target compute configuration if necessary.
1. Test your business continuity, disaster recovery, and failover procedures. All database replicas must support secure enclaves if the workload uses enclave-enabled operations.
1. Monitor the application for enclave, attestation, and query errors before completing the cutover.

## Related content

- [Always Encrypted with secure enclaves documentation](/azure/azure-sql/database/always-encrypted-with-secure-enclaves-landing)
- [Tutorial: Getting started using Always Encrypted with secure enclaves](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)
- [Configure and use Always Encrypted with secure enclaves](configure-always-encrypted-enclaves.md)

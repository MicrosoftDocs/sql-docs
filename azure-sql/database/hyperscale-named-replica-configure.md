---
title: Configure and manage Hyperscale named replicas
description: Learn how to configure and manage Hyperscale named replica so that a user can access the named replica but not other replicas.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: atsingh, dinethi, rsetlem
ms.date: 02/26/2024
ms.service: azure-sql-database
ms.subservice: scale-out
ms.custom: devx-track-azurecli, devx-track-azurepowershell
ms.topic: how-to
---
# Configure and manage Hyperscale named replicas
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides samples to configure and manage an Azure SQL Database Hyperscale [named replica](service-tier-hyperscale-replicas.md).

## Create a Hyperscale named replica

The following sample scenarios guide you to create a named replica `WideWorldImporters_NamedReplica` for database `WideWorldImporters`, using the Azure portal, T-SQL, PowerShell, or Azure CLI.

# [Portal](#tab/portal)

The following example creates a named replica `WideWorldImporters_NamedReplica` for database `WideWorldImporters` using T-SQL. The primary replica uses service level objective HS_Gen5_4, while the named replica uses HS_Gen5_2. Both use the same logical server named `contosoeast`.

1. In the [Azure portal](https://portal.azure.com), browse to the database for which you want to create the named replica.
1. On the **SQL Database** page, select your database, scroll to **Data management**, select **Replicas**, and then select **Create replica**.

    :::image type="content" source="media/named-replicas-configure-portal/azure-create-named-replicas.png" alt-text="Screenshot that shows create named replica step." lightbox="media/named-replicas-configure-portal/azure-create-named-replicas.png":::
1. Choose **Named replica** under **Replica configuration**. Select an existing server or create a new server for the named replica. Enter named replica database name and configure the **Compute + storage** options if necessary.

    :::image type="content" source="media/named-replicas-configure-portal/azure-choose-named-replica.png" alt-text="Screenshot that shows configuration of named replica." lightbox="media/named-replicas-configure-portal/azure-choose-named-replica.png":::
1. Optionally, configure a *zone redundant* Hyperscale named replica. For more information, see [Zone redundancy in Azure SQL Database Hyperscale named replicas](service-tier-hyperscale-replicas.md#zone-redundancy-for-hyperscale-named-replicas).
    1. In the **Configure database** page, select **Yes** for **Would you like to make this database zone redundant?**
    1. Add at least one High-Availability Secondary Replica to your configuration.
    1. Select **Apply**.
1. Select **Review + create**, review the information, and then select **Create**.
1. The named replica deployment process begins.

    :::image type="content" source="media/named-replicas-configure-portal/azure-deployment-named-replica.png" alt-text="Screenshot that shows named replica deployment status." lightbox="media/named-replicas-configure-portal/azure-deployment-named-replica.png":::

1. When the deployment is complete, the named replica displays its status.

    :::image type="content" source="media/named-replicas-configure-portal/azure-deployment-complete-named-replica.png" alt-text="Screenshot that shows named replica status after deployment." lightbox="media/named-replicas-configure-portal/azure-deployment-complete-named-replica.png":::

1. Return to the primary database page, and then select **Replicas**. Your named replica is listed under **Named replicas**.

    :::image type="content" source="media/named-replicas-configure-portal/azure-named-replicas.png" alt-text="Screenshot that shows the SQL database primary and named replica." lightbox="media/named-replicas-configure-portal/azure-named-replicas.png":::

# [T-SQL](#tab/tsql)

The following example creates a named replica `WideWorldImporters_NamedReplica` for database `WideWorldImporters` using T-SQL. The primary replica uses service level objective HS_Gen5_4, while the named replica uses HS_Gen5_2. Both use the same logical server named `contosoeast`.

```sql
ALTER DATABASE [WideWorldImporters]
ADD SECONDARY ON SERVER [contosoeast]
WITH (SERVICE_OBJECTIVE = 'HS_Gen5_2', SECONDARY_TYPE = Named, DATABASE_NAME = [WideWorldImporters_NamedReplica]);
```

# [PowerShell](#tab/azure-powershell)

The following example creates a named replica `WideWorldImporters_NamedReplica` for database `WideWorldImporters` using the PowerShell cmdlet [New-AzSqlDatabaseSecondary](/powershell/module/az.sql/new-azsqldatabasesecondary). The primary replica uses service level objective HS_Gen5_4, while the named replica uses HS_Gen5_2. Both use the same logical server named `contosoeast`.

```azurepowershell
New-AzSqlDatabaseSecondary -ResourceGroupName "MyResourceGroup" -ServerName "contosoeast" -DatabaseName "WideWorldImporters" -PartnerResourceGroupName "MyResourceGroup" -PartnerServerName "contosoeast" -PartnerDatabaseName "WideWorldImporters_NamedReplica" -SecondaryType Named -SecondaryServiceObjectiveName HS_Gen5_2
```

To configure a [zone redundant Hyperscale named replica](service-tier-hyperscale-replicas.md#zone-redundancy-for-hyperscale-named-replicas), you must specify both the `–ZoneRedundant` and `-HighAvailabilityReplicaCount` input parameters for `New-AzSqlDatabaseSecondary`.

```azurepowershell
New-AzSqlDatabaseSecondary -ResourceGroupName "MyResourceGroup" -ServerName "contosoeast" -DatabaseName "WideWorldImporters" -PartnerResourceGroupName "MyResourceGroup" -PartnerServerName "contosoeast" -PartnerDatabaseName "WideWorldImporters_NamedReplica" -SecondaryType Named -SecondaryServiceObjectiveName HS_Gen5_2 -HighAvailabilityReplicaCount 1 -ZoneRedundant 
```

To validate if named replica is created, use [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase):

```azurepowershell
Get-AzSqlDatabase -DatabaseName "WideWorldImporters_NamedReplica" -ResourceGroupName "MyResourceGroup" -ServerName "contosoeast"  
```

# [Azure CLI](#tab/azure-cli)

The following example creates a named replica `WideWorldImporters_NamedReplica` for database `WideWorldImporters` using the Azure CLI command [az sql db replica create](/cli/azure/sql/db/replica?view=azure-cli-latest&preserve-view=true#az-sql-db-replica-create). The primary replica uses service level objective HS_Gen5_4, while the named replica uses HS_Gen5_2. Both use the same logical server named `contosoeast`. 

```azurecli
az sql db replica create -g MyResourceGroup -n WideWorldImporters -s contosoeast --secondary-type named --partner-database WideWorldImporters_NamedReplica --partner-server contosoeast --service-objective HS_Gen5_2
```

To configure a [zone redundant Hyperscale named replica](service-tier-hyperscale-replicas.md#zone-redundancy-for-hyperscale-named-replicas), you must specify both the `–zone-redundant` and `ha-replicas` input parameters for `az sql db replica create`.

```azurecli
az sql db replica create -g MyResourceGroup -n WideWorldImporters -s contosoeast --secondary-type named --partner-database WideWorldImporters_NamedReplica --partner-server contosoeast --service-objective HS_Gen5_2 --ha-replicas 1 -zone-redundant
```

To validate if a named replica is being created:

```azurecli
az sql db show -g MyResourceGroup -n WideWorldImporters -s contosoeast 
```
<!--
# [REST API](#tab/restapi)

For complete details on the REST API for Azure SQL Database commands, see [Databases - Create Or Update](/rest/api/sql/databases/create-or-update).

```http
PUT https://management.azure.com/subscriptions/00000000-1111-2222-3333-444444444444/resourceGroups/Default-SQL-SouthEastAsia/providers/Microsoft.Sql/servers/testsvr/databases/testdb?api-version=2021-11-01

{
  "location": "southeastasia",
  "sku": {
    "name": "BC",
    "family": "Gen4",
    "capacity": 2
  }
}
```
-->
---

As there is no data movement involved, in most cases a named replica will be created in about a minute. Once the named replica is available, it will be visible from the Azure portal or any command-line tool like AZ CLI or PowerShell. A named replica is usable as a regular read-only database.

## Connect to a Hyperscale named replica

To connect to a Hyperscale named replica, you must use the connection string for that named replica, referencing its server and database names. There is no need to specify the option `ApplicationIntent=ReadOnly` as named replicas are always read-only.

Just like for HA replicas, even though the primary, HA, and named replicas share the same data on the same set of page servers, data caches on each named replica are kept in sync with the primary. The sync is maintained by the transaction log service, which forwards log records from the primary to named replicas. As the result, depending on the workload being processed by a named replica, application of the log records might happen at different speeds, and thus different replicas could have different data latency relative to the primary replica.

## Modify a Hyperscale named replica

You can define the service level objective of a named replica when you create it, via the `ALTER DATABASE` command or in any other supported way (Portal, AZ CLI, PowerShell). If you need to change the service level objective after the named replica has been created, you can do it using the `ALTER DATABASE ... MODIFY` command on the named replica itself. 

In the following example, `WideWorldImporters_NamedReplica` is the named replica of `WideWorldImporters` database.

# [Portal](#tab/portal)

Open named replica database page, and then select **Compute + storage**. Update the vCores.

:::image type="content" source="media/named-replicas-configure-portal/azure-update-named-replica.png" alt-text="Screenshot that shows named replica service level objective update." lightbox="media/named-replicas-configure-portal/azure-update-named-replica.png":::

# [T-SQL](#tab/tsql)

```sql
ALTER DATABASE [WideWorldImporters_NamedReplica] MODIFY (SERVICE_OBJECTIVE = 'HS_Gen5_4')
```

# [PowerShell](#tab/azure-powershell)

```azurepowershell
Set-AzSqlDatabase -ResourceGroup "MyResourceGroup" -ServerName "contosoeast" -DatabaseName "WideWorldImporters_NamedReplica" -RequestedServiceObjectiveName "HS_Gen5_4"
```

# [Azure CLI](#tab/azure-cli)

```azurecli
az sql db update -g MyResourceGroup -s contosoeast -n WideWorldImporters_NamedReplica --service-objective HS_Gen5_4
```
<!--
# [REST API](#tab/restapi)

For complete details on the REST API for Azure SQL Database commands, see [Databases - Create Or Update](/rest/api/sql/databases/create-or-update).

This sample command updates the database `testdb` to Hyperscale Gen5, 4 vCores.

```http
PUT https://management.azure.com/subscriptions/00000000-1111-2222-3333-444444444444/resourceGroups/Default-SQL-SouthEastAsia/providers/Microsoft.Sql/servers/testsvr/databases/testdb?api-version=2021-11-01

{
  "location": "eastus",
  "sku": {
    "name": "HS_Gen5",
    "capacity": 4
  }
}
```
-->

---

## Remove a Hyperscale named replica

To remove a Hyperscale named replica, you drop it just like you would a regular database.

# [Portal](#tab/portal)

Open named replica database page, and choose `Delete` option.

:::image type="content" source="media/named-replicas-configure-portal/azure-delete-named-replicas.png" alt-text="Screenshot that shows deletion of named replica." lightbox="media/named-replicas-configure-portal/azure-delete-named-replicas.png":::

# [T-SQL](#tab/tsql)

Make sure you are connected to the `master` database of the server with the named replica you want to drop, and then use the following command:

This command drops a database named `WideWorldImporters_NamedReplica`.

```sql
DROP DATABASE [WideWorldImporters_NamedReplica];
```

# [PowerShell](#tab/azure-powershell)

This command drops a database named `WideWorldImporters_NamedReplica` from the logical server `contosoeast`.

```azurepowershell
Remove-AzSqlDatabase -ResourceGroupName "MyResourceGroup" -ServerName "contosoeast" -DatabaseName "WideWorldImporters_NamedReplica"
```

# [Azure CLI](#tab/azure-cli)

This command drops a database named `WideWorldImporters_NamedReplica` from the logical server `contosoeast`.

```azurecli
az sql db delete -g MyResourceGroup -s contosoeast -n WideWorldImporters_NamedReplica
```
<!--
# [REST API](#tab/restapi)

For complete details on the REST API for Azure SQL Database commands, see [Databases - Delete](/rest/api/sql/databases/delete).

This command drops a database named `testdb` from the logical server `testsvr`.

```http
DELETE https://management.azure.com/subscriptions/00000000-1111-2222-3333-444444444444/resourceGroups/Default-SQL-SouthEastAsia/providers/Microsoft.Sql/servers/testsvr/databases/testdb?api-version=2021-11-01
```
-->
---

> [!IMPORTANT]  
> Named replicas will be automatically removed when the primary replica from which they have been created is deleted.

## Related content

- [Hyperscale secondary replicas](service-tier-hyperscale-replicas.md)
- [Configure isolated access to a Hyperscale named replica](hyperscale-named-replica-security-configure.md)
- [Azure SQL Database Hyperscale FAQ](service-tier-hyperscale-frequently-asked-questions-faq.yml)

---
title: Extend an Always On availability group
description: Learn how to use a Managed Instance link to replicate databases when extending an Always On availability group between SQL Server and Azure SQL Managed Instance.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: danil
ms.date: 09/28/2026
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
ai-usage: ai-assisted
---

# Extend an Always On availability group to Azure SQL Managed Instance (preview)

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to extend an Always On availability group with multiple databases between SQL Server and Azure SQL Managed Instance with the [Managed Instance link](managed-instance-link-feature-overview.md) by using SQL Server Management Studio (SSMS), PowerShell, or Azure CLI. 

This article covers [multiple-database link mode](managed-instance-link-feature-overview.md#link-modes), which replicates all databases in an availability group through one link. Single-database link mode replicates one database per link.

> [!NOTE]
> Support for linking multiple databases in an Always On availability group between SQL Server and Azure SQL Managed Instance is currently in [preview](doc-changes-updates-release-notes-whats-new.md#preview).

## Overview

When you extend an Always On availability group between SQL Server and Azure SQL Managed Instance, you create a link that replicates multiple databases in an availability group to the target replica. The link uses a distributed availability group to replicate changes in near-real time from the current primary replica to read-only database copies on the secondary replica. This ensures that the read-only copies on the secondary remain up-to-date with the primary.

You can use an existing availability group or start with standalone databases. When you select standalone databases in SSMS, the wizard creates a single-node availability group on the initial primary and replicates the selected databases through one link.

Either SQL Server or Azure SQL Managed Instance can be the initial primary. Creating the link from SQL Managed Instance requires SQL Server 2022 or SQL Server 2025 with the required cumulative update and a matching SQL Managed Instance [update policy](update-policy.md). The creation examples in this article start from SQL Server. They don't walk through creation from SQL Managed Instance. Failover with role reversal between SQL Server and Azure SQL Managed Instance is supported for instances configured with matching update policies.

## Supportability

The following requirements apply to extending an availability group through a multiple-database link during preview. SQL Server on both Windows and Linux is supported. You must install the required cumulative update (CU). Earlier builds don't support this feature.

| SQL Server version | Required update | Supported editions |
| --- | --- | --- |
| SQL Server 2022 (16.x) | CU27 or later | Enterprise and Developer |
| SQL Server 2025 (17.x) | CU9 or later | Enterprise and Developer |

Consider the following:

- Standard edition isn't supported because basic availability groups support only one database.
- SQL Server 2019 and earlier versions aren't supported for multiple-database link mode because they lack the required technology introduced in SQL Server 2022.
- To create the link *from* SQL Managed Instance or reverse roles back to SQL Server, your SQL managed instance must use the [update policy](update-policy.md) that matches your SQL Server version. For one-way replication and cutover from SQL Server, the destination update policy must match, or be higher than, your SQL Server version. 
   - SQL Server 2022 supports replication to instances configured with the **SQL Server 2022**, **SQL Server 2025**, and **Always-up-to-date** policies. 
   - SQL Server 2025 supports replication to instances configured with the **SQL Server 2025** and **Always-up-to-date** policies, but not **SQL Server 2022**. You can't replicate data or fail back to SQL Server after cutover if the policies don't match.

For SQL Server versions and editions that support single-database links, see [Managed Instance link version supportability](managed-instance-link-feature-overview.md#version-supportability).

> [!CAUTION]
> Every SQL Server replica in your availability group must use the same supported SQL Server version, have the required cumulative update or later installed, and have multiple-database link mode enabled. Don't mix replicas that support multiple-database link mode with replicas on earlier builds or with the feature disabled. Mixing these configurations can cause SQL Server to behave unpredictably.

## Prerequisites

To extend your availability group between SQL Server and Azure SQL Managed Instance, you need the following prerequisites:

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- A [supported SQL Server version and edition](#supportability) with the required service update installed. You can use an existing Always On availability group or standalone databases that SSMS places in a new single-node availability group. Contained availability groups aren't supported.
- Azure SQL Managed Instance with an [update policy](update-policy.md) appropriate for your scenario. A matching policy is required when SQL Managed Instance is the initial primary or for role reversal. [Get started](instance-create-quickstart.md) if you don't have a SQL managed instance.
- [SQL Server Management Studio (SSMS) 22.10.2 or later](/ssms/install/install).
- For scripted configuration, [Azure PowerShell](/powershell/azure/install-azure-powershell) with the Az module version 16.3.0 or later and Az.Sql version 7.1.0 or later, or [Azure CLI](/cli/azure/install-azure-cli) version 2.90.0 or later. You can also use [Azure Cloud Shell](/azure/cloud-shell/overview). Verify that the installed modules or CLI meet these version requirements.
- A properly [prepared environment](managed-instance-link-preparation.md).
- For a multiple-node availability group, a configured availability group listener. Use the listener's IP address when configuring the link, not an individual SQL Server replica's IP address. Using the listener lets the link continue working after a local availability group failover.
- No existing links on any SQL Server replica when you enable multiple-database link mode. Before you start, remove all links that use the older single-database link mode.
- Sufficient available database capacity and storage on the target managed instance for all databases in your availability group. Review [resource limits](resource-limits.md).

## Permissions

For SQL Server, you need **sysadmin** permissions.

For Azure SQL Managed Instance, you need to be a member of the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) role, or have the following custom role permissions: 

|Microsoft.Sql/ resource|Necessary permissions|
|---- | ---- |
|Microsoft.Sql/managedInstances| /read, /write|
|Microsoft.Sql/managedInstances/hybridCertificate | /action |
|Microsoft.Sql/managedInstances/databases| /read, /delete, /write, /completeRestore/action, /readBackups/action, /restoreDetails/read| 
|Microsoft.Sql/managedInstances/distributedAvailabilityGroups| /read, /write, /delete, /setRole/action| 
|Microsoft.Sql/managedInstances/endpointCertificates| /read|
|Microsoft.Sql/managedInstances/hybridLink| /read, /write, /delete|
|Microsoft.Sql/managedInstances/serverTrustCertificates | /write, /delete, /read | 

## Enable multiple-database link mode

Support for multiple-database link mode is disabled by default during preview. Use the built-in `sys.sp_multidb_milink` stored procedure to enable it on every SQL Server replica in the availability group, or on the SQL Server instance where you plan to create a single-node group.

> [!WARNING]
> Remove all existing links before enabling or disabling multiple-database link mode. Changing the setting while links are active can result in unpredictable SQL Server behavior. Don't mix single-database and multiple-database links. When changing modes, remove the links first, change the setting on every SQL Server replica, and then create new links.

Run the following command on **every SQL Server replica** to enable multiple-database link mode:

```sql
EXEC sys.sp_multidb_milink 1;
```

The setting persists across SQL Server restarts, so you only need to enable it once on each replica.

To check the setting, run the stored procedure without a parameter on each replica. It returns `1` when enabled and `0` when disabled:

```sql
EXEC sys.sp_multidb_milink;
```

If the stored procedure isn't available, verify that the replica has a [supported SQL Server version and cumulative update](#supportability) installed.

To disable multiple-database link mode, first remove all links, and then run the following command on every SQL Server replica:

```sql
EXEC sys.sp_multidb_milink 0;
```

## Prepare the availability group databases

Set each SQL Server database you want to replicate to the [full recovery model](/sql/relational-databases/backup-restore/view-or-change-the-recovery-model-of-a-database-sql-server), and then create a full backup. Both existing availability group databases and standalone databases require this preparation. Use the [SSMS backup procedure](managed-instance-link-configure-how-to-ssms.md#prepare-databases) in the link configuration guide.

> [!CAUTION]
> If your databases use Transparent Data Encryption (TDE), prepare the encryption certificates or keys on the destination before creating the link. Without them, the link can't replicate the encrypted databases.

For SQL Server databases, [migrate the TDE certificate to SQL Managed Instance](tde-certificate-migrate.md). For encrypted SQL Managed Instance databases linked to SQL Server, use a customer-managed key accessible to the destination SQL Server. Review [TDE preparation for the link](managed-instance-link-preparation.md#migrate-a-certificate-of-a-tde-protected-database-optional) for the requirements in each direction.

The link replicates all databases in the selected availability group. You can't choose a subset, so check the target SQL managed instance's available capacity before you create the link. The destination must not contain databases with the same names as the databases you want to replicate. Existing databases with different names are allowed, subject to the instance's capacity limits.

The link supports replicating user databases only. Replication of system databases isn't supported. To replicate instance-level objects stored in `master` or `msdb`, script them out and run T-SQL scripts on the destination instance.

## Configure the listener and certificates

For a multiple-node availability group, use the listener's IP address when configuring the link, both in SSMS and in scripts. The listener directs connections to the current primary replica. Don't use the IP address of an individual SQL Server replica as the link's partner endpoint. Without the listener, the link doesn't continue working after a local availability group failover. For a single-node availability group, including one created by the SSMS wizard for standalone databases, use that SQL Server instance's IP endpoint.

The SSMS wizard exchanges certificates between Azure SQL Managed Instance and only the current SQL Server primary replica. It doesn't configure certificate trust on the other SQL Server replicas. You must manually copy and configure the required certificates on every other SQL Server replica so that the link can continue working after a local availability group failover. This manual step applies to both SSMS and scripted configuration. Review [Establish trust between instances](managed-instance-link-configure-how-to-scripts.md#establish-trust-between-instances) for the certificate exchange steps.

## Prepare for scripted link creation

Use SSMS for the recommended setup experience. The wizard automates many configuration steps. If you don't need scripted automation, skip this section and continue to the **SSMS** tab in [Extend the availability group](#extend-the-availability-group).

Scripted setup is an advanced option that requires experience configuring availability groups, endpoints, and certificate trust. Complete these steps only if you're using PowerShell or Azure CLI with SQL Server as the initial primary.

The checklist covers both existing availability groups and standalone databases. After you prepare the databases, trust, and endpoint, reuse your existing  availability group or create one in step 4. Then create the distributed availability group. The PowerShell and Azure CLI link creation commands don't create the availability group for you.

For a script tailored to your environment, use the SSMS link wizard and select **Script** on its **Summary** page. Review the generated script and execute it separately.

1. [Enable multiple-database link mode](#enable-multiple-database-link-mode) on every SQL Server replica, or on the standalone SQL Server instance, and [prepare the databases](#prepare-the-availability-group-databases).
1. [Establish trust between instances](managed-instance-link-configure-how-to-scripts.md#establish-trust-between-instances). Follow the certificate creation, public-key exchange, root-certificate import, and certificate-chain validation steps. For a multiple-node group, apply the [certificate requirements to every SQL Server replica](#configure-the-listener-and-certificates), not only the current primary.
1. [Secure the database mirroring endpoint](managed-instance-link-configure-how-to-scripts.md#secure-the-database-mirroring-endpoint). If your availability group already has an endpoint, use [Alter an existing endpoint](managed-instance-link-configure-how-to-scripts.md#alter-an-existing-endpoint) instead of creating another one. Retain the configured endpoint port for the link creation command.
1. Prepare the availability group. If you already have an availability group containing all databases you want to replicate, reuse it and skip creating a new group. If you start with standalone databases, first [create an availability group on SQL Server](managed-instance-link-configure-how-to-scripts.md?tabs=sql-server#create-an-availability-group-on-sql-server). In the **SQL Server initial primary** tab, use the single-node `CREATE AVAILABILITY GROUP` example with `CLUSTER_TYPE = NONE`, but replace `FOR DATABASE [<DatabaseName>]` with the complete database list, such as `FOR DATABASE [DB01], [DB03], [DB05], [DB07]`. Set `<AGNameOnSQLServer>` to the name you want to give the new group. Run this script before continuing to distributed availability group creation. Don't run it against an existing group or change an existing group's cluster configuration.
1. [Create the distributed availability group on SQL Server](managed-instance-link-configure-how-to-scripts.md?tabs=sql-server#create-distributed-availability-group-on-sql-server). Use the **SQL Server initial primary** tab and start at the distributed availability group creation instructions. Set `<AGNameOnSQLServer>` to the availability group you reused or created in the preceding step. For a multiple-node group, use the listener's IP address for `<SQLServerIP>`. For a single-node group, use the SQL Server instance's endpoint. Retain `<DAGName>` as your link name and `<AGNameOnSQLMI>` as the managed-instance availability group name for the creation command below.
1. [Verify the availability groups](managed-instance-link-configure-how-to-scripts.md#verify-availability-groups) on SQL Server. Confirm that both the Always On availability group and the distributed availability group are present. Then return to [Extend the availability group](#extend-the-availability-group), select **PowerShell** or **Azure CLI**, and run the multiple-database creation command in this article instead of the other guide's single-database command.

## Extend the availability group

To retain log records needed for seeding, the recommended approach is to enable [trace flag 12381 on supported SQL Server builds](managed-instance-link-troubleshoot-how-to.md#prevent-premature-log-truncation-with-trace-flag-12381) before creating links, especially for large databases or many databases in multiple-database link mode. However, the flag isn't required, and there are alternative mitigations, listed in [Troubleshoot error 1412](managed-instance-link-troubleshoot-how-to.md#error-1412). With the flag enabled, log backups can continue, but retained log records aren't made reusable. Monitor SQL Server log growth and free disk space, and disable the flag as soon as seeding finishes for all links being created.

Use SSMS to automate link creation, or choose PowerShell or Azure CLI for advanced scripted configuration. The following examples use SQL Server as the initial primary. You can also start from SQL Managed Instance with a matching update policy, but that creation workflow isn't covered here.

For scripted configuration, complete the [scripted setup steps](#prepare-for-scripted-link-creation) to reuse or create an availability group containing all databases you want to replicate, and then create the distributed availability group before running the PowerShell or Azure CLI creation command. Alternatively, if you start with standalone databases, the SSMS procedure in this section automatically creates the single-node availability group as part of link setup.

For multiple-database link mode, explicitly specify `MultiDatabase` in scripts and supply **all database names** in the availability group. PowerShell defaults to `SingleDatabase` if `-LinkMode` is omitted. Use `-LinkMode MultiDatabase` in PowerShell or `--link-mode MultiDatabase` in Azure CLI.

> [!WARNING]
> Don't create a link with `MultiDatabase` link mode unless every SQL Server replica has the required cumulative update and multiple-database link mode enabled through the `sys.sp_multidb_milink` stored procedure. Using this mode with SQL Server builds that don't support it can cause SQL Server to behave unpredictably. Review [Supportability](#supportability) and [Enable multiple-database link mode](#enable-multiple-database-link-mode) first.

### [SSMS](#tab/ssms)

Use the **New SQL Managed Instance link** wizard in SSMS to create a link from an existing availability group or standalone databases to Azure SQL Managed Instance.

1. Open SSMS and connect to SQL Server. For a multiple-node availability group, connect through the listener's IP address. For standalone databases or a single-node group, connect to the SQL Server instance.
1. In **Object Explorer**, right-click a database you want to replicate, hover over **Azure SQL Managed Instance link**, and select **New...** to open the **New SQL Managed Instance link** wizard.

    :::image type="content" source="media/managed-instance-link-extend-availability-group/database-link-menu.png" alt-text="Screenshot of the database context menu in SSMS with the New Managed Instance link command selected." lightbox="media/managed-instance-link-extend-availability-group/database-link-menu.png":::

1. On the **Introduction** page of the wizard, select **Next**.
1. On the **Specify Link Options** page, verify that multiple-database link mode is enabled, and provide a name for your link. The mode checkbox is read-only: it reflects the `sys.sp_multidb_milink` setting on SQL Server. You can't enable the mode by selecting the checkbox. If the mode isn't enabled, check the SQL Server version and cumulative update, and [enable the feature on all replicas](#enable-multiple-database-link-mode) before continuing. Use lowercase letters for the link name. Hyphens are allowed except at the beginning or end. Select **Next**.

    :::image type="content" source="media/managed-instance-link-extend-availability-group/specify-link-options.png" alt-text="Screenshot of Specify Link Options showing the link name and the enabled, read-only multiple-database mode checkbox." lightbox="media/managed-instance-link-extend-availability-group/specify-link-options.png":::

1. On the **Requirements** page, the wizard validates requirements to establish a link to your secondary. Select **Next** after all the requirements are validated, or resolve any requirements that aren't met and then select **Re-run Validation**.
1. On the **Select Databases** page, choose either an existing availability group or standalone databases:
    - Select **AG01** to replicate all its databases, such as **DB01**, **DB03**, **DB05**, and **DB07**.
    - Or select standalone **DB10** and **DB11**. With multiple-database link mode enabled, SSMS creates a single-node availability group on the current SQL Server instance, places both databases in it, and replicates them through one link.

    Review the selection, and then select **Next**.

    :::image type="content" source="media/managed-instance-link-extend-availability-group/select-ag-or-standalone.png" alt-text="Screenshot of Select Databases offering the existing AG01 group or standalone databases DB10 and DB11." lightbox="media/managed-instance-link-extend-availability-group/select-ag-or-standalone.png":::

1. On the **Specify Secondary Replica** page, select **Add secondary replica**. If SQL managed instance is your secondary, sign in to Azure, and choose the subscription, resource group, and secondary SQL managed instance to connect to your instance.

    :::image type="content" source="media/managed-instance-link-extend-availability-group/specify-secondary-replica.png" alt-text="Screenshot of Specify Secondary Replica showing SQL Server as primary and SQL Managed Instance as secondary." lightbox="media/managed-instance-link-extend-availability-group/specify-secondary-replica.png":::

1. Review the endpoint settings and complete the remaining validation steps as described in [Configure link with SSMS](managed-instance-link-configure-how-to-ssms.md#create-link-to-replicate-database).
1. On the **Summary** page, review your configuration once more. Optionally, select **Script** to generate a script. Select **Finish** when you're ready to create the link.
1. After all steps finish, the **Results** page shows check marks next to the successfully completed actions. You can now close the window.

Multiple-database mode replicates the databases in your availability group through one link. This approach differs from selecting multiple databases in single-database mode, which creates a separate link for each database.

### [PowerShell](#tab/powershell)

Follow [Configure link with scripts](managed-instance-link-configure-how-to-scripts.md) to set up the link. Use [Prepare for scripted link creation](#prepare-for-scripted-link-creation) as your ordered checklist. At the link creation step, use the `New-AzSqlInstanceLink` example in this section instead of the single-database command. It specifies `-LinkMode MultiDatabase` and includes every database in the availability group.

Update the installed Az and Az.Sql modules:

```powershell
Update-Module -Name Az
Update-Module -Name Az.Sql
```

Check that Az is version 16.3.0 or later and Az.Sql is version 7.1.0 or later:

```powershell
Get-InstalledModule -Name Az | Select-Object Name, Version
Get-InstalledModule -Name Az.Sql | Select-Object Name, Version
```

Use [New-AzSqlInstanceLink](/powershell/module/az.sql/new-azsqlinstancelink) to create the SQL managed instance side of the link. Use the link and availability group names from the scripted setup steps, and include every database in your availability group. Replace the example resource names, database list, and endpoint with your values. For a multiple-node group, replace the example IP address with the listener's IP address and use the configured database mirroring endpoint port, not an individual replica's IP address or the listener's client connection port. For a single-node group, use that SQL Server instance's endpoint.

If you're using Azure Cloud Shell, ensure **PowerShell** is selected.

Configure the input variables as described in the following table. The script retrieves the resource group and builds the endpoint, then creates the multi-database link with SQL Managed Instance as the secondary.

| Variable | Description |
| --- | --- |
| `$ManagedInstanceName` | Name of your SQL managed instance. |
| `$DAGName` | Distributed availability group name on SQL Server, also used as the link name. Use lowercase letters and hyphens, with no leading or trailing hyphen. The name must not already identify another link on this managed instance. |
| `$AGNameOnSQLServer` | SQL Server availability group name from the setup steps. |
| `$AGNameOnSQLMI` | SQL Managed Instance availability group name from the setup steps. |
| `$DatabaseNames` | Exact names of every database in the availability group. |
| `$SQLEndpointIP` | Listener IP address for a multiple-node availability group, or the SQL Server instance's IP address for a single-node group. |
| `$EndpointPort` | Database mirroring endpoint port, normally `5022` unless customized. |
| `$ResourceGroup` | Resource group name retrieved by the script. Don't replace this assignment. |
| `$SQLPartnerEndpoint` | Endpoint built by the script from the IP address and port. Don't replace this assignment. |

```powershell
# Run in Azure Cloud Shell and select PowerShell.
$ManagedInstanceName = "<ManagedInstanceName>"

# Use the distributed availability group name from SQL Server as the link name.
# Use lowercase letters and hyphens, with no leading or trailing hyphen.
# The name must not already identify another link on this managed instance.
$DAGName = "<DAGName>"

# Enter the availability group names from the setup steps.
$AGNameOnSQLServer = "<AGNameOnSQLServer>"
$AGNameOnSQLMI = "<AGNameOnSQLMI>"

# Include every database in the availability group.
$DatabaseNames = @("DB01", "DB03", "DB05", "DB07")

# Use the listener IP for multiple-node AGs or the instance IP for single-node AGs.
$SQLEndpointIP = "<SQLEndpointIP>"
# Use the database mirroring endpoint port, normally 5022 unless customized.
$EndpointPort = "<EndpointPort>"

$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName
$SQLPartnerEndpoint = "TCP://" + $SQLEndpointIP + ":" + $EndpointPort

# Create the multi-database link with SQL Managed Instance as the secondary.
New-AzSqlInstanceLink -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -Name $DAGName -PartnerAvailabilityGroupName $AGNameOnSQLServer -InstanceAvailabilityGroupName $AGNameOnSQLMI -Database $DatabaseNames -PartnerEndpoint $SQLPartnerEndpoint -InstanceLinkRole "Secondary" -FailoverMode "Manual" -SeedingMode "Automatic" -LinkMode "MultiDatabase"
```

### [Azure CLI](#tab/azure-cli)

Follow [Configure link with scripts](managed-instance-link-configure-how-to-scripts.md) to set up the link. Use [Prepare for scripted link creation](#prepare-for-scripted-link-creation) as your ordered checklist. At the link creation step, use the `az sql mi link create` example in this section instead of the single-database command. It specifies `--link-mode MultiDatabase` and includes every database in the availability group.

Use Azure CLI version 2.90.0 or later. Check your installed version with [az version](/cli/azure/reference-index#az-version). Use [az sql mi link create](/cli/azure/sql/mi/link#az-sql-mi-link-create) to create the SQL managed instance side of the link, using the link and availability group names from the scripted setup steps.

Replace the example values and include every database in your availability group. For a multiple-node group, use the listener's IP address and database mirroring endpoint port. For a single-node group, use the SQL Server instance's endpoint:

If you're using Azure Cloud Shell, ensure **Bash** is selected.

Configure the input variables as described in the following table. The script builds the endpoint and creates the multi-database link with SQL Managed Instance as the secondary.

| Variable | Description |
| --- | --- |
| `ResourceGroupName` | Resource group that contains your SQL managed instance. |
| `ManagedInstanceName` | Name of your SQL managed instance. |
| `DAGName` | Distributed availability group name on SQL Server, also used as the link name. Use lowercase letters and hyphens, with no leading or trailing hyphen. The name must not already identify another link on this managed instance. |
| `AGNameOnSQLServer` | SQL Server availability group name from the setup steps. |
| `AGNameOnSQLMI` | SQL Managed Instance availability group name from the setup steps. |
| `DatabaseNames` | Exact names of every database in the availability group, using the database-list syntax shown in the script. |
| `SQLEndpointIP` | Listener IP address for a multiple-node availability group, or the SQL Server instance's IP address for a single-node group. |
| `EndpointPort` | Database mirroring endpoint port, normally `5022` unless customized. |
| `SQLPartnerEndpoint` | Endpoint built by the script from the IP address and port. Don't replace this assignment. |

```azurecli
# Run in Azure Cloud Shell and select Bash.
ResourceGroupName="<ResourceGroupName>"
ManagedInstanceName="<ManagedInstanceName>"

# Use the distributed availability group name from SQL Server as the link name.
# Use lowercase letters and hyphens, with no leading or trailing hyphen.
# The name must not already identify another link on this managed instance.
DAGName="<DAGName>"

# Enter the availability group names from the setup steps.
AGNameOnSQLServer="<AGNameOnSQLServer>"
AGNameOnSQLMI="<AGNameOnSQLMI>"

# Include every database in the availability group.
DatabaseNames="[{database-name:DB01},{database-name:DB03},{database-name:DB05},{database-name:DB07}]"

# Use the listener IP for multiple-node AGs or the instance IP for single-node AGs.
SQLEndpointIP="<SQLEndpointIP>"
# Use the database mirroring endpoint port, normally 5022 unless customized.
EndpointPort="<EndpointPort>"

SQLPartnerEndpoint="TCP://${SQLEndpointIP}:${EndpointPort}"

# Create the multi-database link with SQL Managed Instance as the secondary.
az sql mi link create --resource-group "$ResourceGroupName" --instance-name "$ManagedInstanceName" --name "$DAGName" --partner-availability-group-name "$AGNameOnSQLServer" --instance-availability-group-name "$AGNameOnSQLMI" --databases "$DatabaseNames" --partner-endpoint "$SQLPartnerEndpoint" --instance-link-role Secondary --failover-mode Manual --seeding-mode Automatic --link-mode MultiDatabase
```

---

## Verify replication

After you create the link or add databases, data replicates from the current primary to the current secondary replica. Either SQL Server or Azure SQL Managed Instance can be the initial primary. After role reversal, data replicates in the opposite direction. Depending on database size and network speed, each database might initially be in a **Restoring** state on the secondary replica. After initial seeding finishes, the database is restored to the secondary replica and ready for read-only workloads.

On either replica, use **Object Explorer** in SSMS to view the **Synchronized** state of each replicated database. Expand **Always On High Availability** and **Availability Groups** to view the distributed availability group created for the link.

When SQL Server is primary, you can continue transaction log backups during seeding if [trace flag 12381](managed-instance-link-troubleshoot-how-to.md#prevent-premature-log-truncation-with-trace-flag-12381) is enabled on a supported build. If you pause log backups to prevent premature truncation, resume them after initial seeding finishes. For each database without a log backup schedule, take the first [transaction log backup](managed-instance-link-configure-how-to-ssms.md#take-first-transaction-log-backup) only after initial seeding finishes, not during seeding. After seeding completes for all links being created, disable the flag if you enabled it and take [SQL Server transaction log backups regularly](managed-instance-link-best-practices.md#take-log-backups-regularly) while SQL Server remains primary. When Azure SQL Managed Instance is primary, it takes transaction log backups automatically. You don't need to take manual SQL Server log backups for these databases while SQL Server is secondary.

Premature log truncation during seeding can cause [errors 1408 and 1412](managed-instance-link-troubleshoot-how-to.md#errors-creating-a-link) in the SQL Managed Instance error log. On builds that support it, trace flag `12381` prevents this truncation. Disable it once seeding completes for all links being created, and monitor transaction log usage, growth rate, and free disk space while it's enabled. Log backups can continue while required records remain retained. This retention doesn't replace regular log backups after seeding. See [Prevent premature log truncation](managed-instance-link-troubleshoot-how-to.md#prevent-premature-log-truncation-with-trace-flag-12381).

## Add databases

Use the SSMS wizard to add databases from the current primary, whether it's SQL Server or SQL Managed Instance. The wizard automates the required changes. For advanced automation, use **PowerShell** or the **Azure CLI**. Adding a database is a single operation on the primary side.

Before adding databases, verify that the existing link uses multiple-database link mode and that the destination has sufficient available database capacity and storage, with no existing database names that conflict with the new databases. When SQL Server is primary, set each new database that isn't already in the availability group to the full recovery model, and create a full backup by using the [SSMS backup procedure](managed-instance-link-configure-how-to-ssms.md#prepare-databases).

### Add databases with SSMS

Use the **Add Database to Azure SQL Managed Instance Link** wizard to add databases to an existing multiple-database link:

1. Connect to the current primary in SSMS. In **Object Explorer**, expand **Always On High Availability** and **Availability Groups**.
1. Right-click the distributed availability group for your link, hover over **Azure SQL Managed Instance link**, and select **Add Database...**.

    :::image type="content" source="media/managed-instance-link-extend-availability-group/add-remove-database-menu.png" alt-text="Screenshot of the distributed availability group context menu in SSMS showing Add Database and Remove Database commands." lightbox="media/managed-instance-link-extend-availability-group/add-remove-database-menu.png":::

1. Proceed through **Introduction** and **Azure Login**, and then select your multiple-database link on the **Select Link** page.
1. On the **Select Databases** page, select the databases you want to add. You can only add databases with a **Ready** state. Databases already in the link are shown as **Already part of the selected link**. Resolve any eligibility issues before continuing.

    :::image type="content" source="media/managed-instance-link-extend-availability-group/select-additional-databases.png" alt-text="Screenshot of the Add Database wizard with DB10 and DB11 selected and existing link members retained." lightbox="media/managed-instance-link-extend-availability-group/select-additional-databases.png":::

1. Complete **Validation** and review **Summary**. Select **Finish** to execute the change, or select **Script** to generate a script without executing the change so you can review, customize, and run it separately. If you execute the change in the wizard, review **Results** before closing it.

### Add databases with scripts

Run the addition on the current primary. Follow the instructions for that instance.

#### When SQL Server is primary

Use T-SQL to [add each database to the availability group](/sql/database-engine/availability-groups/windows/availability-group-add-a-database#use-transact-sql). The link propagates the addition to SQL Managed Instance. No further action is needed on SQL Managed Instance. Don't run a PowerShell or Azure CLI update for this addition.

#### When SQL Managed Instance is primary

Use PowerShell or Azure CLI to update the link on SQL Managed Instance. The link automatically propagates the added databases to the availability group. No separate step on SQL Server is required. Supply the **complete intended membership**, including all existing databases you want to retain and the new databases. The supplied list replaces the current membership. Omitting a database removes it from the link's membership.

For example, to add `DB09` when the link already contains `DB01`, `DB03`, `DB05`, and `DB07`, retain those four names in the list, and also add `DB09` to the list. Replace the resource and database names with your values.

| PowerShell variable | Azure CLI variable | Description |
| --- | --- | --- |
| `$ResourceGroup` | `ResourceGroupName` | Resource group that contains the SQL managed instance. |
| `$ManagedInstanceName` | `ManagedInstanceName` | Name of the SQL managed instance that hosts the link. |
| `$DAGName` | `DAGName` | Existing link name, matching the distributed availability group name used during creation. |
| `$DatabaseNames` | `DatabaseNames` | Complete list of existing databases to retain and new databases to add. The example retains `DB01`, `DB03`, `DB05`, and `DB07`, and adds `DB09`. |

##### [PowerShell](#tab/powershell-script)

Use [Update-AzSqlInstanceLink](/powershell/module/az.sql/update-azsqlinstancelink) in PowerShell. Reuse `$ResourceGroup`, `$ManagedInstanceName`, and `$DAGName` from creation, or set them to the resource group, instance, and link you want to update:

```powershell
# Include every existing database to retain and each new database to add.
$DatabaseNames = @("DB01", "DB03", "DB05", "DB07", "DB09")

Update-AzSqlInstanceLink -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -Name $DAGName -Database $DatabaseNames
```

##### [Azure CLI](#tab/azure-cli-script)

Use [az sql mi link update](/cli/azure/sql/mi/link#az-sql-mi-link-update) in Azure CLI. Reuse `ResourceGroupName`, `ManagedInstanceName`, and `DAGName` from creation, or set them to the resource group, instance, and link you want to update:

```azurecli
# Include every existing database to retain and each new database to add.
DatabaseNames="[{database-name:DB01},{database-name:DB03},{database-name:DB05},{database-name:DB07},{database-name:DB09}]"

az sql mi link update --resource-group "$ResourceGroupName" --instance-name "$ManagedInstanceName" --name "$DAGName" --databases "$DatabaseNames"
```

---

Repeat the [replication verification steps](#verify-replication) for each newly added database. Follow the manual log backup steps only when SQL Server is primary.

## Remove databases

Use the SSMS wizard on the current primary to automate removal on both sides. Removing a database requires removing it from the link on SQL Managed Instance and from the availability group. Removing it on only one side doesn't complete the operation.

> [!WARNING]
> If you remove a database from the link on SQL Managed Instance but leave it in the availability group, the availability group becomes unhealthy. Complete removal on both sides. For scripted removal, follow the section for your current primary.

### Remove databases with SSMS

The following steps apply whether SQL Server or SQL Managed Instance is primary:

1. Connect to the current primary in SSMS. In **Object Explorer**, expand **Always On High Availability** and **Availability Groups**.
1. Right-click the distributed availability group for the link, hover over **Azure SQL Managed Instance link**, and select **Remove Database...**.

    :::image type="content" source="media/managed-instance-link-extend-availability-group/remove-databases-menu.png" alt-text="Screenshot of the distributed availability group menu with the Remove Database command selected." lightbox="media/managed-instance-link-extend-availability-group/remove-databases-menu.png":::

1. In the **Remove Database from Azure SQL Managed Instance Link** wizard, proceed through **Introduction** and **Azure Login**, and select the link on **Select Link**.
1. On **Select Databases**, select the databases to remove. For example, select **DB05** to remove it from the link, and then select **Next**.

    :::image type="content" source="media/managed-instance-link-extend-availability-group/remove-databases-select.png" alt-text="Screenshot of the Remove Database wizard with DB05 selected for removal from the link." lightbox="media/managed-instance-link-extend-availability-group/remove-databases-select.png":::

1. Complete **Validation**, review **Summary**, and select **Finish** to execute the removal or **Script** to review the generated commands first. Check **Results** for successful completion before closing the wizard.

### Remove databases with scripts

Remove the databases on both instances. The current primary determines which instance to update first.

The PowerShell and Azure CLI examples use the following variables:

| PowerShell variable | Azure CLI variable | Description |
| --- | --- | --- |
| `$ResourceGroup` | `ResourceGroupName` | Resource group that contains the SQL managed instance. |
| `$ManagedInstanceName` | `ManagedInstanceName` | Name of the SQL managed instance that hosts the link. |
| `$DAGName` | `DAGName` | Existing link name, matching the distributed availability group name used during creation. |
| `$DatabaseNames` | `DatabaseNames` | Complete list of databases to retain, excluding those to remove. The examples exclude `DB05` and retain `DB01`, `DB03`, `DB07`, and `DB09`. |

#### When SQL Server is primary

1. Use T-SQL to [remove the databases from the availability group](/sql/database-engine/availability-groups/windows/remove-a-primary-database-from-an-availability-group-sql-server#using-transact-sql).
1. Use PowerShell or Azure CLI to remove the databases from the link on SQL Managed Instance, as shown in this section.

For the SQL Managed Instance step, supply the **complete list of databases to retain**, omitting only those you want to remove. For example, if the link contains `DB01`, `DB03`, `DB05`, `DB07`, and `DB09`, the following command removes `DB05` and retains the other four. Replace the resource and database names with your values.

##### [PowerShell](#tab/powershell-script)

Use [Update-AzSqlInstanceLink](/powershell/module/az.sql/update-azsqlinstancelink). Reuse `$ResourceGroup`, `$ManagedInstanceName`, and `$DAGName` from creation, or set them to the resource group, instance, and link you want to update:

```powershell
# Include only the databases to retain, excluding DB05.
$DatabaseNames = @("DB01", "DB03", "DB07", "DB09")

Update-AzSqlInstanceLink -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -Name $DAGName -Database $DatabaseNames
```

##### [Azure CLI](#tab/azure-cli-script)

Use [az sql mi link update](/cli/azure/sql/mi/link#az-sql-mi-link-update). Reuse `ResourceGroupName`, `ManagedInstanceName`, and `DAGName` from creation, or set them to the resource group, instance, and link you want to update:

```azurecli
# Include only the databases to retain, excluding DB05.
DatabaseNames="[{database-name:DB01},{database-name:DB03},{database-name:DB07},{database-name:DB09}]"

az sql mi link update --resource-group "$ResourceGroupName" --instance-name "$ManagedInstanceName" --name "$DAGName" --databases "$DatabaseNames"
```

---

#### When SQL Managed Instance is primary

1. Use PowerShell or Azure CLI to remove the databases from the link on SQL Managed Instance, as shown in this section.
1. Use T-SQL on SQL Server to remove the databases from its availability group. Removal isn't complete until you finish this step.

For the SQL Managed Instance step, supply the **complete list of databases to retain**, omitting only those you want to remove. For example, if the link contains `DB01`, `DB03`, `DB05`, `DB07`, and `DB09`, the following command removes `DB05` and retains the other four. Replace the resource and database names with your values.

##### [PowerShell](#tab/powershell-script)

Use [Update-AzSqlInstanceLink](/powershell/module/az.sql/update-azsqlinstancelink). Reuse `$ResourceGroup`, `$ManagedInstanceName`, and `$DAGName` from creation, or set them to the resource group, instance, and link you want to update:

```powershell
# Include only the databases to retain, excluding DB05.
$DatabaseNames = @("DB01", "DB03", "DB07", "DB09")

Update-AzSqlInstanceLink -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -Name $DAGName -Database $DatabaseNames
```

##### [Azure CLI](#tab/azure-cli-script)

Use [az sql mi link update](/cli/azure/sql/mi/link#az-sql-mi-link-update). Reuse `ResourceGroupName`, `ManagedInstanceName`, and `DAGName` from creation, or set them to the resource group, instance, and link you want to update:

```azurecli
# Include only the databases to retain, excluding DB05.
DatabaseNames="[{database-name:DB01},{database-name:DB03},{database-name:DB07},{database-name:DB09}]"

az sql mi link update --resource-group "$ResourceGroupName" --instance-name "$ManagedInstanceName" --name "$DAGName" --databases "$DatabaseNames"
```

---

Confirm that the removed databases no longer belong to the link or the availability group. Removing a database from replication isn't the same as deleting its retained copy. Review the databases on both instances before deciding whether to delete a copy you no longer need.

## Fail over or cut over to Azure

Use the existing [failover procedures](managed-instance-link-failover-how-to.md) in SSMS or scripts to reverse roles between SQL Server and Azure SQL Managed Instance. Role reversal requires the SQL managed instance to use the update policy that matches your SQL Server version. For one-way replication and cutover to Azure SQL Managed Instance, its update policy must match or be higher than your SQL Server version. You can't replicate data or fail back to SQL Server afterward if the policies don't match. Review the [supported combinations](#supportability). For migration and cutover guidance, see [Migrate with the link](managed-instance-link-migrate.md).

## Monitor and troubleshoot replication

Use the following dynamic management views (DMVs) and catalog view on SQL Server to check the main availability group, replica connectivity, and each database's replication health:

| View | Information |
| --- | --- |
| [sys.availability_groups](/sql/relational-databases/system-catalog-views/sys-availability-groups-transact-sql) | Availability groups, excluding internal per-database replication groups. |
| [sys.dm_hadr_availability_replica_states](/sql/relational-databases/system-dynamic-management-objects/sys-dm-hadr-availability-replica-states-transact-sql) | Role, connectivity, and synchronization health for the main group and internal per-database replication groups. |
| [sys.dm_hadr_database_replica_states](/sql/relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql) | Database-level replication state and synchronization health. |
| `sys.dm_hadr_internal_availability_groups` | Internal replication groups created for individual databases in multiple-database link mode. |
| `sys.dm_hadr_internal_availability_replicas` | Replicas belonging to the internal per-database replication groups in multiple-database link mode. |

```sql
SELECT * FROM sys.availability_groups;
SELECT * FROM sys.dm_hadr_availability_replica_states;
SELECT * FROM sys.dm_hadr_database_replica_states;
SELECT * FROM sys.dm_hadr_internal_availability_groups;
SELECT * FROM sys.dm_hadr_internal_availability_replicas;
```

If the internal replication DMVs aren't available, or executing the `sys.sp_multidb_milink` stored procedure reports that it isn't available, verify the installed SQL Server version and cumulative update on that replica. For general connectivity and replication troubleshooting, see [Troubleshoot the Managed Instance link](managed-instance-link-troubleshoot-how-to.md).

## Limitations

Consider the following limitations when extending an availability group through a multiple-database link:

- Link names must use lowercase letters. Hyphens are allowed, but a name can't begin or end with a hyphen.
- Don't downgrade any SQL Server replica below SQL Server 2022 CU27 or SQL Server 2025 CU9, as applicable, while a link in multiple-database mode is active. Downgrading below the required CU can cause unpredictable issues even without a failover.
- Contained availability groups aren't supported.
- Single-database links and multiple-database links can't coexist on the same SQL Server instance.
- You can't change a link's mode in place. To switch between single-database and multiple-database link modes, remove all existing links, change the mode on every SQL Server replica, and then recreate the links in the new mode.
- When you create a link for an existing availability group, all databases in that group must be replicated. You can't select only a subset of the databases in that group.
- The remaining database capacity on the target SQL managed instance limits the number of databases you can replicate. General Purpose and Business Critical support up to 100 databases per instance, and Next-gen General Purpose supports up to 500. Existing databases count toward these limits. For example, an instance with a 100-database limit and 10 existing databases has capacity for 90 more databases. For more information, see [resource limits](resource-limits.md).
- Adding databases to the availability group beyond the target SQL managed instance's available database capacity can succeed on SQL Server, but replication to SQL Managed Instance fails. This condition can leave the link in an inconsistent state that requires manual removal of the nonreplicated databases from the availability group.
- Adding databases is propagated through the link, but removing a database on one side doesn't automatically remove it on the other side. If you remove a database from the availability group, its copy remains on SQL Managed Instance. If you remove a database from the link on SQL Managed Instance, the database remains in the availability group without replication through the link and requires manual cleanup.
- When adding databases to an existing link through SSMS, you can only add databases with a **Ready** state. You can't add databases that belong to another availability group or have a name that already exists on the destination.

## Related content

- [Managed Instance link overview](managed-instance-link-feature-overview.md)
- [Prepare your environment for the link](managed-instance-link-preparation.md)
- [Configure link with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Best practices for maintaining the link](managed-instance-link-best-practices.md)
- [Troubleshoot issues with the link](managed-instance-link-troubleshoot-how-to.md)

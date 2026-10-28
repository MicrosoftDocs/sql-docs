---
title: Upgrade a Free Tier Database to Hyperscale
description: Remove the Azure SQL Database free offer, then convert the existing database to Hyperscale with the Azure portal, Azure CLI, PowerShell, or an agent prompt.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: alpowers
ms.date: 10/28/2026
ms.service: azure-sql-database
ms.topic: how-to
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
ai-usage: ai-generated
monikerRange: "=azuresql || =azuresql-db"
---

# Upgrade a free tier database to Hyperscale

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

To move an existing [Azure SQL Database free tier](free-tier.md) database to [Hyperscale](service-tier-hyperscale.md), perform two separate updates:

1. Remove the free offer and wait for the update to complete. The database remains a General Purpose serverless database, without the free allowance.
1. Convert the database to Hyperscale and wait for conversion and cutover to complete.

Don't combine free-offer removal and the Hyperscale configuration in one update. Confirm that the free offer is disabled before you request Hyperscale.

> [!IMPORTANT]
> Removing the free offer is permanent for this database. You can't reapply the offer, even if Hyperscale conversion fails or you later move back to General Purpose. Paid General Purpose usage can accrue after removal, and Hyperscale billing begins after cutover. Review [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/azure-sql-database/single/) before you proceed.

## Understand the free-offer settings

The option to pay for overages isn't the same as removing the free offer.

| Setting | Effect |
| --- | --- |
| **Continue using database for additional charges**, or `BillOverUsage` for the free-limit exhaustion behavior. | Keeps the free offer and allows paid usage beyond the monthly allowance. This setting alone doesn't enable conversion to Hyperscale. |
| Remove the **Free database offer**, `--use-free-limit false` in Azure CLI, or `-UseFreeLimit:$false` in PowerShell. | Removes the free allowance permanently. After this update completes, you can request a supported Hyperscale configuration in a separate update. |

## Prerequisites

- Identify the subscription, resource group, logical server, and database that you want to upgrade.
- Use an Azure identity with permission to read and update the database, such as the [SQL DB Contributor](/azure/role-based-access-control/built-in-roles/databases#sql-db-contributor) role at the appropriate scope.
- Review [Hyperscale conversion prerequisites](convert-to-hyperscale.md#prerequisites) and [resource limits](resource-limits-vcore-single-databases.md). Confirm that your target configuration is available in the database's region before you remove the free offer.
- Plan for a brief connectivity interruption at cutover. Ensure that your application [retries transient connection failures](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors).
- For command-line steps, use a current [Azure CLI](/cli/azure/install-azure-cli) or [Az.Sql PowerShell module](/powershell/azure/install-az-ps) that supports the free-limit parameters.

The scripting examples use Hyperscale serverless on standard-series (Gen5) hardware, with a maximum of 2 vCores, a minimum of 0.5 vCores, and one high availability replica. These settings are examples, not a sizing recommendation. Replicas add cost.

The examples explicitly disable auto-pause with `-1`, so minimum compute charges continue during inactivity. Hyperscale serverless auto-pause and auto-resume are in preview. To use that preview instead, review [serverless auto-pause and auto-resume](serverless-tier-auto-pause-resume.md) for eligibility and configuration.

## Upgrade the database

Choose one method. Each method updates the existing database; you don't need to create a replacement database or move application data manually.

# [Portal](#tab/azure-portal)

First, remove the free offer without changing the service tier:

1. Open your database in the [Azure portal](https://portal.azure.com).
1. Select **Compute + storage**.
1. Use the control to remove the **Free database offer**. Don't select **Hyperscale** in this update, and don't substitute the option to continue using the database for additional charges.
1. Review the permanent removal and billing implications, and then select **Apply** to save the change while the database is still on General Purpose serverless.
1. Wait for the update to complete. Check the portal notifications for success or failure.
1. Reopen **Compute + storage** and confirm that the free offer is no longer applied. If you can't confirm removal, stop before changing the service tier. You can also check `useFreeLimit` with the Azure CLI example in this article.

Then, request Hyperscale in a separate update:

1. In **Compute + storage**, select **Hyperscale** from the **Service tier** list.
1. Choose **Serverless** or **Provisioned** for the **Compute tier**, and review the hardware configuration, vCores, and high availability replicas. Review auto-pause settings if you select serverless.
1. Review the estimated cost and **Cutover mode**. Automatic cutover completes conversion as soon as the database is ready. Manual cutover lets you choose when the final connectivity interruption occurs.
1. Select **Apply**.
1. On the database **Overview** page, open the ongoing-operation notification to monitor conversion. If you select manual cutover, select **Cutover** when the database is ready, within the documented cutover window. For details, see [Cutover](convert-to-hyperscale.md#cutover).
1. After cutover, confirm that the database is online and its active service tier is Hyperscale. For an additional check, use [Verify the upgrade](#verify-the-upgrade).

# [Azure CLI](#tab/azure-cli)

Run this example in Bash, such as Bash in [Azure Cloud Shell](/azure/cloud-shell/overview). For a local CLI session, sign in with `az login` first. The script uses Bash arrays and line continuations; don't paste it into PowerShell.

Replace the placeholders before running the script. It asks for confirmation before removing the free offer. Don't add `--no-wait`: the second update must wait for the first update to finish.

```azurecli-interactive
set -euo pipefail

subscription="<subscription-id>"
resourceGroup="<resource-group>"
server="<server-name>"
database="<database-name>"

scope=(--subscription "$subscription" --resource-group "$resourceGroup"
       --server "$server" --name "$database")

freeLimit=$(az sql db show "${scope[@]}" --query "to_string(useFreeLimit)" --output tsv)
if [[ "$freeLimit" == "true" ]]; then
    read -r -p "Permanently remove the free offer and enable paid billing? Type UPGRADE: " confirmation
    if [[ "$confirmation" != "UPGRADE" ]]; then
        echo "Upgrade cancelled before making changes." >&2
        exit 1
    fi

    az sql db update "${scope[@]}" --use-free-limit false --output none
elif [[ "$freeLimit" != "false" ]]; then
    echo "Can't determine the free-offer state. No update submitted." >&2
    exit 1
fi

freeLimit=$(az sql db show "${scope[@]}" --query "to_string(useFreeLimit)" --output tsv)
if [[ "$freeLimit" != "false" ]]; then
    echo "Free-offer removal isn't confirmed. Hyperscale update not submitted." >&2
    exit 1
fi

az sql db update "${scope[@]}" \
    --edition Hyperscale --compute-model Serverless \
    --family Gen5 --capacity 2 --min-capacity 0.5 \
    --auto-pause-delay -1 --ha-replicas 1 --output none

currentObjective=$(az sql db show "${scope[@]}" --query currentServiceObjectiveName --output tsv)
freeLimit=$(az sql db show "${scope[@]}" --query "to_string(useFreeLimit)" --output tsv)
if [[ "$currentObjective" != "HS_S_Gen5_2" || "$freeLimit" != "false" ]]; then
    echo "Active Hyperscale serverless configuration isn't confirmed. Check database operations." >&2
    exit 1
fi

az sql db show "${scope[@]}" \
    --query "{name:name,status:status,freeLimit:useFreeLimit,current:currentServiceObjectiveName,requested:requestedServiceObjectiveName,haReplicas:highAvailabilityReplicaCount}" \
    --output table
```

The conversion uses automatic cutover. To control cutover timing instead, follow the [Azure CLI manual cutover steps](convert-to-hyperscale.md?tabs=azure-cli#convert-a-database-to-hyperscale), after removing the free offer.

For parameter details, see [az sql db update](/cli/azure/sql/db#az-sql-db-update).

# [PowerShell](#tab/azure-powershell)

Sign in with `Connect-AzAccount`. Replace the placeholders before running this example in PowerShell. The script asks for confirmation before removing the free offer and stops on a failed cmdlet.

`UseFreeLimit` is a switch parameter. Pass `-UseFreeLimit:$false` to disable it; omitting the parameter doesn't request removal.

```powershell-interactive
$ErrorActionPreference = "Stop"

$subscription = "<subscription-id>"
$resourceGroup = "<resource-group>"
$server = "<server-name>"
$database = "<database-name>"

Set-AzContext -SubscriptionId $subscription | Out-Null
$scope = @{
    ResourceGroupName = $resourceGroup
    ServerName = $server
    DatabaseName = $database
}

$db = Get-AzSqlDatabase @scope
if ($db.UseFreeLimit -eq $true) {
    $confirmation = Read-Host "Permanently remove the free offer and enable paid billing? Type UPGRADE"
    if ($confirmation -cne "UPGRADE") {
        throw "Upgrade cancelled before making changes."
    }

    Set-AzSqlDatabase @scope -UseFreeLimit:$false | Out-Null
} elseif ($db.UseFreeLimit -ne $false) {
    throw "Can't determine the free-offer state. No update submitted."
}

$db = Get-AzSqlDatabase @scope
if ($db.UseFreeLimit -ne $false) {
    throw "Free-offer removal isn't confirmed. Hyperscale update not submitted."
}

$target = @{
    Edition = "Hyperscale"
    ComputeModel = "Serverless"
    ComputeGeneration = "Gen5"
    VCore = 2
    MinimumCapacity = 0.5
    AutoPauseDelayInMinutes = -1
    HighAvailabilityReplicaCount = 1
}
Set-AzSqlDatabase @scope @target | Out-Null

$db = Get-AzSqlDatabase @scope
if ($db.CurrentServiceObjectiveName -ne "HS_S_Gen5_2" -or $db.UseFreeLimit -ne $false) {
    throw "Active Hyperscale serverless configuration isn't confirmed. Check database operations."
}

$db | Select-Object DatabaseName, Status, UseFreeLimit,
    CurrentServiceObjectiveName, RequestedServiceObjectiveName, HighAvailabilityReplicaCount
```

Don't use `-AsJob` unless your automation explicitly waits for completion and checks the result before continuing. The conversion uses automatic cutover. For manual cutover, follow the [PowerShell manual cutover steps](convert-to-hyperscale.md?tabs=azure-powershell#convert-a-database-to-hyperscale), after removing the free offer.

For parameter details, see [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase).

# [Agent prompt](#tab/agent-prompt)

Use an agent with Azure management tools and access to your subscription. A prompt doesn't grant permissions or bypass Azure SQL validation. Replace the placeholders and review the proposed commands before you approve execution.

```agent
Upgrade the existing Azure SQL Database <database-name> on logical server
<server-name>, in resource group <resource-group> and subscription <subscription-id>,
from the free tier to Hyperscale. Don't create a replacement database.

1. Confirm the exact resource, current service objective, and useFreeLimit value.
   Read the current Microsoft Learn references for az sql db update or
   Set-AzSqlDatabase. Check Hyperscale conversion prerequisites, regional
   availability, target sizing, replicas, auto-pause eligibility, and pricing.
2. Show me the target configuration and estimated costs. Explain that removing
   the free offer is permanent, and paid General Purpose usage can accrue even
   if the subsequent Hyperscale conversion fails. Ask for my explicit approval
   before changing the database.
3. If useFreeLimit is true, submit ONLY the free-offer removal update:
   --use-free-limit false for Azure CLI, or -UseFreeLimit:$false for PowerShell.
   Don't combine it with a service-tier change. BillOverUsage isn't removal.
4. Wait for the update to finish successfully, then read the database again.
   Stop if useFreeLimit isn't explicitly false. If it was already false,
   don't try to reapply or remove the offer.
5. Submit the approved Hyperscale configuration as a separate update.
   Format commands for my shell. Wait for conversion and cutover to complete.
6. Verify useFreeLimit is false and the CURRENT service objective matches the
   approved Hyperscale configuration. Don't infer completion from the requested
   SKU or edition alone. Report the active configuration and operation outcome.
7. If any step fails, stop and show the error, current database state, billing
   implications, and a safe next step. Don't claim rollback restored the free offer.
```

---

## Verify the upgrade

After conversion and cutover complete, confirm that:

- The database is online.
- The free-limit property is `false`.
- The **current** service objective matches the requested Hyperscale configuration. For the scripting examples, expect `HS_S_Gen5_2`.
- The compute, auto-pause, and replica settings match your intended configuration.
- Your application can connect and run a representative query.

During conversion, the requested SKU or edition can describe the target before the active service objective changes. Don't treat the requested configuration alone as proof that cutover is complete.

To check the active service tier from a SQL connection, run this query in the upgraded database:

```sql
SELECT d.name AS database_name, slo.edition, slo.service_objective
FROM sys.database_service_objectives AS slo
INNER JOIN sys.databases AS d ON d.database_id = slo.database_id
WHERE d.name = DB_NAME();
```

Expect one row with `edition` set to `Hyperscale` and `service_objective` matching your target.

## Troubleshoot an incomplete upgrade

If free-offer removal fails, don't submit the Hyperscale update. Inspect the error, confirm permissions and the database state, and retry removal only after you resolve the cause.

If removal succeeds but conversion fails, the database remains outside the free offer and can incur charges. Read its current service objective and recent operations before retrying. Don't try to restore the free offer with `--use-free-limit true` or `-UseFreeLimit`.

Use [az sql db op list](/cli/azure/sql/db/op#az-sql-db-op-list), [Get-AzSqlDatabaseActivity](/powershell/module/az.sql/get-azsqldatabaseactivity), or the portal's ongoing-operation details to inspect conversion progress and errors. If conversion is still running or waiting for manual cutover, complete or resolve that operation before submitting another conversion request.

For eligible converted databases, [reverse migration from Hyperscale](reverse-migrate-from-hyperscale.md) can return the database to paid General Purpose. It doesn't restore the free offer.

## Related content

- [Azure SQL Database free tier FAQ](free-tier-faq.yml)
- [Convert an existing database to Hyperscale](convert-to-hyperscale.md)
- [Manage Hyperscale databases](manage-hyperscale-database.md)
- [Serverless compute tier](serverless-tier-overview.md)

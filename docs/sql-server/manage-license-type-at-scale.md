---
title: Automate the Transition to Pay-as-you-go SQL Licensing at Scale
description: Learn how to preview, automate, and enforce transitions to pay-as-you-go SQL licensing at scale across Azure SQL and SQL Server enabled by Azure Arc.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: rajpo
ms.date: 09/07/2026
ms.service: sql
ms.topic: how-to
ai-usage: ai-assisted

#customer intent: As a cloud administrator, I want to transition SQL resources to pay-as-you-go licensing at scale so that they use the correct billing model.
---

# Automate the transition to pay-as-you-go SQL licensing at scale

[!INCLUDE [appliesto-sqldb-sqlmi-sqlvm-arc](../includes/applies-to-version/appliesto-sqldb-sqlmi-sqlvm-arc.md)]

This article describes how to:

- Review and transition multiple SQL products to pay-as-you-go licensing with a single PowerShell script.
- Continuously enforce pay-as-you-go licensing for one resource type with Azure Policy.

The automation in this article applies to [SQL Server enabled by Azure Arc](azure-arc/overview.md), [SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview), [Azure SQL Database](/azure/azure-sql/database/sql-database-paas-overview), and [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview). The script can also process an Azure-SSIS Integration Runtime in Azure Data Factory (Azure-SSIS IR).

Changing a licensing agreement doesn't change the license type configured for your SQL resources. The workflows in this article help you transition resources to pay-as-you-go billing after SQL licenses with Software Assurance, SQL subscription licenses, or Service Provider License Agreement (SPLA) licensing no longer apply.

## Prerequisites

- An Azure account with access to the tenant and subscriptions that contain the SQL resources.
- PowerShell 5 or later with the [Az PowerShell module](/powershell/azure/install-azure-powershell).
- The [Azure CLI](/cli/azure/install-azure-cli) when you use the PowerShell script to update SQL resources.
- The **Contributor** role in each subscription that you modify. For least-privilege alternatives, review [Permissions by resource type](#permissions-by-resource-type).
- For scheduled mode, permission to create role assignments at the target subscription scope, or the required roles preassigned to the Automation account's managed identity.
- The authority to change billing settings and attest to the licensing terms for the target resources.

To avoid installing the tools locally, run the script in [Azure Cloud Shell](/azure/cloud-shell/overview). Cloud Shell includes Azure PowerShell and the Azure CLI and authenticates the session. Specify `-TenantId` when your account can access more than one tenant.

## Choose an automation method

Choose between:

- PowerShell for cross-resource transitions to pay-as-you-go licensing.
- Azure Policy for continuous pay-as-you-go enforcement for one resource type.

The following table provides details about each automation method:

| Scenario | Recommended method |
| --- | --- |
| Review or apply pay-as-you-go changes across multiple SQL resource types | [`manage-payg-transition.ps1`](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/manage-payg-transition/manage-payg-transition.ps1) |
| Transition all supported resources in a subscription or tenant to pay-as-you-go licensing | [`manage-payg-transition.ps1`](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/manage-payg-transition/manage-payg-transition.ps1) |
| Run a pay-as-you-go transition on a recurring schedule | [`manage-payg-transition.ps1`](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/manage-payg-transition/manage-payg-transition.ps1) in scheduled mode |
| Continuously enforce pay-as-you-go licensing for one resource type and remediate drift | A resource-specific Azure Policy sample |
| Change one resource manually | The product-specific licensing article |

## Review supported resources and license values

The procedures in this article set `-TargetLicenseType` to `PAYG`. The script translates this value to the pay-as-you-go value required by each resource provider. The source values show common license configurations that the transition can replace.

| Resource | Pay-as-you-go target value | Common source values | Supported by the script | Azure Policy sample |
| --- | --- | --- | --- | --- |
| SQL Server enabled by Azure Arc | `PAYG` | `Paid`, `LicenseOnly` | Yes | [Arc-enabled SQL Server license type compliance](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/compliance/arc-sql-license-type-compliance) |
| SQL Server on Azure Virtual Machines | `PAYG` | `AHUB` | Yes | [SQL Server on Azure VMs license type compliance](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/sql-vm/sql-iaas-license-type-compliance) |
| Azure SQL Database and elastic pools | `LicenseIncluded` | `BasePrice` | Yes | [Azure SQL Database license type compliance](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-sql-db/sql-paas-license-type-compliance) |
| Azure SQL Managed Instance and instance pools | `LicenseIncluded` | `BasePrice`, `HybridFailoverRights` | Yes | [SQL Managed Instance license type compliance](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-sql-db-managed-instance/sql-mi-license-type-compliance) |
| Azure-SSIS IR | `LicenseIncluded` | `BasePrice` | Yes | Not covered in this article |

The policy samples can overwrite additional source values, such as `DR` for SQL Server on Azure VMs. Review the selected sample before you deploy a policy assignment.

## Understand how the PowerShell script works

You can use the PowerShell script to modify all resources within a resource group, all resources within a subscription, and all resources across one or more subscriptions within an entire tenant.

The script is self-contained and works across the supported SQL resources. When `-TargetLicenseType` is set to the default `PAYG`, it translates that value to the pay-as-you-go value required by each resource type, as shown in [Review supported resources and license values](#review-supported-resources-and-license-values). 
By default, the generated working folder remains after the script finishes. To remove it after a run, specify `-cleanDownloads $true`.

The script prints a summary by resource type and lists the cause for each failed or skipped resource. When the script finds matching resources, it also creates a `ModifiedResources_<timestamp>.csv` report with `UpdateResult` and `UpdateError` columns.

The following parameters control the script's scope and execution:

| Parameter | Accepted value or default | Purpose |
| --- | --- | --- |
| `-Target` | `Arc`, `Azure`, or `Both` (default) | Selects SQL Server enabled by Azure Arc resources, Azure SQL resources and Azure-SSIS IR, or both groups. |
| `-RunMode` | `Single` (default) or `Scheduled` | Runs the transition once or configures daily Azure Automation runbooks. |
| `-TargetLicenseType` | `PAYG` | Selects pay-as-you-go as the target license model for the procedures in this article. |
| `-targetSubscription` | Subscription ID; all accessible subscriptions in the tenant by default | Limits the transition to one subscription. |
| `-targetResourceGroup` | Resource group name; all resource groups by default | Limits the transition to one resource group. |
| `-TenantId` | Tenant ID; current Az PowerShell context by default | Selects the Microsoft Entra tenant. |
| `-ReportOnly` | Switch; disabled by default | Reports qualifying resources without changing them. |
| `-WaitForCompletion` | Switch; disabled by default | Waits for submitted changes to reach a terminal state when supported. |
| `-UsePcoreLicense` | `No` (default) or `Yes` | Controls physical-core licensing for SQL Server enabled by Azure Arc. |
| `-AutomationAccResourceGroupName` | Required with `-RunMode Scheduled` | Selects the resource group for the Automation account. |
| `-AutomationAccountName` | `aaccAzureArcSQLLicenseType` by default | Sets the Automation account name. |
| `-Location` | Required with `-RunMode Scheduled` | Selects the Azure region for the Automation account and resource group. |
| `-cleanDownloads` | `$false` (default) or `$true` | Removes the generated working folder after the script finishes. |

The default execution behavior depends on the resource type:

| Resource | Default behavior | With `-WaitForCompletion` |
| --- | --- | --- |
| Azure SQL Database, Azure SQL Managed Instance | Submits an asynchronous request and reports `RequestSubmitted` | Waits and reports `Updated` |
| SQL Server enabled by Azure Arc | Submits an asynchronous extension update and reports `RequestSubmitted` | Polls the extension and reports `Succeeded`, `Failed`, or `TimedOut` |
| SQL Server on Azure Virtual Machines | Submits a direct Azure Resource Manager request and reports `RequestSubmitted` | Runs [az sql vm update](/cli/azure/sql/vm#az-sql-vm-update), waits, and reports `Updated` |
| Azure-SSIS IR | Waits and reports `Updated` | Same behavior |

## Preview the pay-as-you-go transition

The [`manage-payg-transition.ps1`](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/manage-payg-transition/manage-payg-transition.ps1) script provides one entry point for Azure SQL and SQL Server enabled by Azure Arc resources.

With the PowerShell script, you can specify a single subscription to scan. If you don't specify a subscription, the script scans all subscriptions your role can access.

1. Download the script:

   ```powershell
   $scriptUri = "https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/manage/manage-payg-transition/manage-payg-transition.ps1"
   Invoke-WebRequest -Uri $scriptUri -OutFile ".\manage-payg-transition.ps1"
   ```

1. Sign in to the tenant that contains the resources:

   ```powershell
    Connect-AzAccount -TenantId "<tenant-id>"
   ```

1. Run a read-only preview for a single subscription. Specify `-TenantId` even when you already have an Azure context so that the script doesn't use the wrong tenant:

   ```powershell
   .\manage-payg-transition.ps1 `
       -Target Both `
       -TenantId "<tenant-id>" `
       -targetSubscription "<subscription-id>" `
       -TargetLicenseType PAYG `
       -ReportOnly
   ```

1. If the script finds matching resources, open the newest `ModifiedResources_<timestamp>.csv` report. Confirm that every listed resource should change:

   ```powershell
   $report = Get-ChildItem -Filter "ModifiedResources_*.csv" |
       Sort-Object LastWriteTime -Descending |
       Select-Object -First 1

   Import-Csv $report.FullName | Format-Table
   ```

Resources that already use pay-as-you-go licensing don't appear in the report. If the script finds no matching resources, it doesn't create a CSV file. To reduce the scope, specify `-targetResourceGroup`. Set `-Target` to `Azure` to process Azure SQL resources and Azure-SSIS IR. Set it to `Arc` to process only SQL Server enabled by Azure Arc resources.

## Run a one-time pay-as-you-go transition with PowerShell

Run the pay-as-you-go transition only after you confirm the target scope and approve every resource in the preview report.

1. Remove `-ReportOnly`, and add `-WaitForCompletion` to wait for confirmed outcomes where the resource provider supports them:

   ```powershell
   .\manage-payg-transition.ps1 `
       -Target Both `
       -TenantId "<tenant-id>" `
       -targetSubscription "<subscription-id>" `
       -TargetLicenseType PAYG `
       -WaitForCompletion
   ```

1. Review the generated CSV report. Investigate every `Failed` or `TimedOut` result, and confirm the resource state for every submitted change.

The `PAYG` value sets pay-as-you-go as the target license model.

If you omit `-WaitForCompletion`, most accepted changes have an `UpdateResult` of `RequestSubmitted`. This result means that Azure accepted the request, not that the license type finished changing. Azure-SSIS IR always waits because its update command doesn't support asynchronous execution.

## Schedule recurring pay-as-you-go transitions with PowerShell

Scheduled mode creates or updates one Azure Automation runbook for each selected target, links each runbook to a daily schedule, and starts a one-time job immediately. Provide an Automation account resource group and Azure region. The default Automation account name is `aaccAzureArcSQLLicenseType`. Scheduled mode uses the Automation account's managed identity.

1. Schedule the transition for Azure SQL and SQL Server enabled by Azure Arc resources in one subscription:

   ```powershell
   .\manage-payg-transition.ps1 `
       -Target Both `
       -RunMode Scheduled `
       -TenantId "<tenant-id>" `
       -targetSubscription "<subscription-id>" `
       -TargetLicenseType PAYG `
       -AutomationAccResourceGroupName "<automation-resource-group>" `
       -AutomationAccountName "<automation-account>" `
       -Location "<azure-region>"
   ```

1. In the Automation account, confirm that the runbooks are published, linked to a daily schedule, and able to start a job with the selected parameters.

If the Azure target includes SQL Managed Instance pools, assign the Automation account's managed identity **Contributor** or a custom role with `Microsoft.Sql/instancePools/read` and `Microsoft.Sql/instancePools/write`. The scheduled setup doesn't assign a built-in role that grants these permissions.

## Enforce pay-as-you-go licensing with Azure Policy

Use a resource-specific Azure Policy sample to enforce pay-as-you-go licensing continuously and remediate configuration drift. Each sample contains a policy definition, a deployment script, and a remediation script.

> [!WARNING]
> A policy assignment can change every matching resource in its scope. Start with a narrowly scoped test subscription, and select only the current license types that you intend to replace.

The following table links the policy package and deployment script for each resource:

| Resource | Policy package | Deployment script |
| --- | --- | --- |
| SQL Server enabled by Azure Arc | [Arc-enabled SQL Server license type compliance](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/compliance/arc-sql-license-type-compliance) | [`deployment.ps1`](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/azure-arc-enabled-sql-server/compliance/arc-sql-license-type-compliance/scripts/deployment.ps1) |
| SQL Server on Azure Virtual Machines | [SQL Server on Azure VMs license type compliance](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/sql-vm/sql-iaas-license-type-compliance) | [`deployment.ps1`](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-vm/sql-iaas-license-type-compliance/scripts/deployment.ps1) |
| Azure SQL Managed Instance | [SQL Managed Instance license type compliance](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-sql-db-managed-instance/sql-mi-license-type-compliance) | [`deployment.ps1`](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/azure-sql-db-managed-instance/sql-mi-license-type-compliance/scripts/deployment.ps1) |
| Azure SQL Database | [Azure SQL Database license type compliance](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-sql-db/sql-paas-license-type-compliance) | [`deployment.ps1`](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/azure-sql-db/sql-paas-license-type-compliance/scripts/deployment.ps1) |

Use the values and roles for the selected resource type:

| Resource | Pay-as-you-go target value | Source values that can be overwritten | Primary required role | Important limitation |
| --- | --- | --- | --- | --- |
| SQL Server enabled by Azure Arc | `PAYG` | `Paid`; the portal definition also supports `LicenseOnly` | **Azure Extension for SQL Server Deployment** | The assignment applies one target to all matching hosts and isn't edition-aware. Scope mixed estates separately. |
| SQL Server on Azure Virtual Machines | `PAYG` | `AHUB`, `DR` | **Virtual Machine Contributor** | The target must meet Azure Hybrid Benefit or passive high-availability or disaster-recovery (HA/DR) licensing conditions. |
| Azure SQL Managed Instance | `LicenseIncluded` | `BasePrice`, `HybridFailoverRights` | **SQL Managed Instance Contributor** | `HybridFailoverRights` sets `licenseType` to `BasePrice` and `hybridSecondaryUsage` to `Passive`. |
| Azure SQL Database | `LicenseIncluded` | `BasePrice` | **SQL DB Contributor** | The policy excludes `master` and Basic databases. Azure SQL Database supports license type changes only for provisioned vCore databases, so exclude or exempt other database transaction unit (DTU)-based and serverless databases from the assignment. |

Each assignment also needs **Reader** and **Resource Policy Contributor**. The deployment scripts grant the required roles when the signed-in identity has permission to create role assignments. Use `-GrantMissingPermissions` with the remediation script to check and add missing roles.

The SQL VM, SQL Managed Instance, and SQL Database deployment scripts prompt for confirmation when the target value requires a licensing attestation. Use `-SkipLicenseConfirmation` only in an automated pipeline where the operator has already confirmed eligibility and accepts responsibility for compliance.

For Arc-enabled SQL Server, a `PAYG` policy also configures recurring billing consent. The consent remains on the extension if a later assignment changes the license type away from `PAYG`.

The policy packages use a shared deployment flow:

1. Download the package's `policy` and `scripts` folders by following its README instructions.
1. Sign in to the tenant that contains the assignment scope:

   ```powershell
   Connect-AzAccount -TenantId "<tenant-id>"
   ```

1. From the downloaded package folder, deploy the definition and assignment. Use the pay-as-you-go target value for your resource type from the preceding table. The following example assigns the policy at subscription scope:

   ```powershell
   .\scripts\deployment.ps1 `
       -SubscriptionId "<subscription-id>" `
       -TargetLicenseType "<pay-as-you-go-value>"
   ```

   If you omit `-SubscriptionId`, the script uses management group scope. Review the package defaults before you run the deployment.

1. Confirm that the policy assignment has a system-assigned managed identity and the required roles at the assignment scope.
1. Start remediation for existing noncompliant resources:

   ```powershell
   .\scripts\start-remediation.ps1 `
       -SubscriptionId "<subscription-id>" `
          -TargetLicenseType "<pay-as-you-go-value>" `
       -GrantMissingPermissions
   ```

1. In Azure Policy, review the assignment's compliance results and remediation task. Then confirm the license type on the remediated resources.

## Validate the pay-as-you-go transition

The CSV report includes an `UpdateResult` and an `UpdateError` for each selected resource. Interpret common results as follows:

| Result | Meaning | Next action |
| --- | --- | --- |
| `ReportOnly` | The script identified a qualifying resource in read-only mode and didn't submit a change. | Confirm that the resource should use pay-as-you-go licensing before you run the transition. |
| `RequestSubmitted` | Azure accepted an asynchronous request. | Wait for the resource operation to finish, and then validate the resource state. |
| `Updated` or `Succeeded` | The script observed a successful terminal result. | Confirm the license type on the resource. |
| `Failed` | The service rejected the update or returned a failed terminal state. | Review `UpdateError`, correct the problem, and rerun the preview. |
| `TimedOut` | The Arc extension didn't reach a terminal state before the polling limit. | Check the extension state. The update might complete after the script stops waiting. |

To confirm the current license type and provisioning state for SQL Server enabled by Azure Arc, run the following Azure Resource Graph query:

```powershell
Search-AzGraph -Query @"
resources
| where type =~ 'microsoft.hybridcompute/machines/extensions'
| where properties.type in~ ('WindowsAgent.SqlServer', 'LinuxAgent.SqlServer')
| project name = split(id, '/')[8],
          licenseType = properties.settings.LicenseType,
          state = properties.provisioningState
"@
```

After submitted operations finish, rerun the same scope with `-ReportOnly`:

```powershell
.\manage-payg-transition.ps1 `
    -Target Both `
    -TenantId "<tenant-id>" `
    -targetSubscription "<subscription-id>" `
    -TargetLicenseType PAYG `
    -ReportOnly
```

The script excludes resources that already use pay-as-you-go licensing. A fully converged scope reports no resources to update. Investigate any resource that still appears before you consider the transition complete.

## Review limitations and billing safeguards

- SQL Server on Azure VMs must be running. The script skips stopped or deallocated VMs and doesn't start them.
- The script excludes SQL Server on Azure VMs that use the `DR` license type, so it doesn't overwrite the license setting for a passive HA/DR replica.
- The script doesn't expose the recurring billing consent option of its embedded Arc handler. For CSP-managed Arc resources that require recurring billing consent, use the [Arc-enabled SQL Server license type compliance](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/compliance/arc-sql-license-type-compliance) policy, which configures consent when it applies `PAYG`.
- A default asynchronous run favors scale over immediate confirmation. Use `-WaitForCompletion` when you need the script to observe terminal results before it exits.
- By default, SQL Server on Azure VM updates use an asynchronous Azure Resource Manager request. If that request fails, the script falls back to `az sql vm update`, which waits for the operation to finish.

## Troubleshoot automation failures

Use the generated report and Azure operation status to identify resources that weren't updated.

- If the report is empty, confirm the tenant, subscription, and resource group. An empty report is expected when all in-scope resources already use pay-as-you-go licensing.
- If an Arc-enabled resource is skipped, restore connectivity for the Azure Connected Machine agent, confirm that the provisioning state for the Azure extension for SQL Server is `Succeeded`, and then rerun the preview.
- If `UpdateResult` is `RequestSubmitted`, don't treat the request as complete. Check the resource state or rerun the preview after the Azure operation finishes.
- If an Azure SQL or SQL Server on Azure VM operation fails, confirm that Azure CLI is installed, signed in to the same tenant, and authorized for the target subscription.
- If scheduled mode fails, confirm that the Automation account managed identity has the roles listed in the source sample.
- If policy remediation returns `PolicyAuthorizationFailed`, confirm that the assignment identity has the product-specific role, **Reader**, and **Resource Policy Contributor** at the assignment scope. Rerun the deployment script, or rerun the remediation script with `-GrantMissingPermissions` after the signed-in identity can create role assignments.

For the complete source, see [`manage-payg-transition.ps1`](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/manage-payg-transition/manage-payg-transition.ps1). For current limitations and test results, see the [`manage-payg-transition.ps1` test plan](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/manage-payg-transition/TESTPLAN.md).

## Permissions by resource type

### Permissions for the PowerShell script

The **Contributor** role is a superset of the permissions required by the script in this article and is the simplest option. For least-privilege access, assign the roles required for the resource types in scope:

| Resource type | Role or required permissions |
| --- | --- |
| SQL Server on Azure Virtual Machines | **Virtual Machine Contributor** |
| Azure SQL Managed Instance | **SQL Managed Instance Contributor** |
| Azure SQL Database | **SQL DB Contributor** |
| Azure SQL Database elastic pools | **SQL Server Contributor** |
| Azure SQL Managed Instance pools | **Contributor**, or a custom role with `Microsoft.Sql/instancePools/read` and `Microsoft.Sql/instancePools/write` |
| SQL Server enabled by Azure Arc | **Azure Connected Machine Resource Administrator** |
| Azure-SSIS IR | **Data Factory Contributor** |
| Reading and enumerating subscriptions and resources | **Reader**, unless another assigned role grants the required read permissions |

### Permissions for Azure Policy

The identity that runs `deployment.ps1` needs permission to create policy definitions at the selected management group and to create policy assignments, remediation tasks, managed identities, and role assignments at the assignment scope. If it can't create role assignments, assign the required roles to the policy assignment's managed identity before you start remediation, and run `deployment.ps1` with `-SkipManagedIdentityRoleAssignment`.

By default, `deployment.ps1` creates a system-assigned managed identity for the policy assignment and grants three roles at the assignment scope. The product-specific role depends on the selected policy sample:

| Policy sample | Product-specific role |
| --- | --- |
| SQL Server enabled by Azure Arc | **Azure Extension for SQL Server Deployment** |
| SQL Server on Azure Virtual Machines | **Virtual Machine Contributor** |
| Azure SQL Managed Instance | **SQL Managed Instance Contributor** |
| Azure SQL Database | **SQL DB Contributor** |

Each policy assignment identity also requires **Reader** and **Resource Policy Contributor**. The deployment script skips automatic role assignment only when you specify `-SkipManagedIdentityRoleAssignment`.

Before starting remediation, `start-remediation.ps1` checks the policy assignment identity for all three required roles. If a role is missing, the script stops. Assign the roles manually, or rerun the remediation script with `-GrantMissingPermissions` from an identity that can create role assignments at the assignment scope.

## Related content

- [Configure SQL Server enabled by Azure Arc](azure-arc/manage-configuration.md)
- [Change the license model for a SQL virtual machine in Azure](/azure/azure-sql/virtual-machines/windows/licensing-model-azure-hybrid-benefit-ahb-change)
- [Azure Hybrid Benefit for Azure SQL Database and SQL Managed Instance](/azure/azure-sql/azure-hybrid-benefit)

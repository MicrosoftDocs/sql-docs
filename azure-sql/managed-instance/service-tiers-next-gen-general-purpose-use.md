---
title: Use Next-gen General Purpose service tier
description: Learn how to use the Next-gen General Purpose service tier in Azure SQL Managed Instance, which is an architectural upgrade to the existing General Purpose service tier that can be used for new and existing instances.
author: urosmil
ms.author: urmilano
ms.reviewer: wiassaf, mathoma
ms.date: 05/21/2026
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: how-to
ai-usage: ai-assisted
ms.custom:
  - ignite-2025
---
# Use Next-gen General Purpose service tier - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article shows you how to use the Next-gen General Purpose service tier upgrade for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md). The Next-gen General Purpose service tier is an architectural upgrade to the existing General Purpose service tier that you can use for new and existing instances. 

## Overview

[!INCLUDE [azure-sql-managed-instance-compare-service-tiers](../includes/sql-managed-instance/azure-sql-managed-instance-next-gen-general-purpose-upgrade.md)]

## Upgrade existing instances 

To upgrade an existing instance to the Next-gen General Purpose service tier in the Azure portal, follow these steps:

1. Go to your SQL managed instance resource in the [Azure portal](https://portal.azure.com).
1. Select **Compute + storage** under **Settings** to open the **Compute + storage** pane.
1. On the **Compute + storage** pane, select **Enable** for *Next-gen General Purpose*:
   
   :::image type="content" source="media/service-tiers-next-gen-general-purpose-use/existing-instance.png" alt-text="Screenshot of the compute and storage page for your instance in the Azure portal, with next-gen general purpose selected.":::

1. After *Next-gen General Purpose* is enabled, you can use the sliders to modify the IOPS and memory for the instance. Review the *Cost per IOPS* and *Cost per GB* in the **Estimated costs per month** box.
1. Select **Apply** to save your changes.

## New instances

You can deploy new instances using the Next-gen General Purpose service tier upgrade in the Azure portal, Azure PowerShell, or Azure CLI. Follow the instructions for your preferred deployment method:

#### [Azure portal](#tab/portal)

You can use the Next-gen General Purpose tier upgrade for new instances when you deploy them in the Azure portal. To do so, follow these steps:

1. Go to the [Create Azure SQL Managed Instance](https://portal.azure.com/#create/Microsoft.SQLManagedInstance) pane in the Azure portal.
2. On the **Networking** tab, under **Virtual network / subnet**, select a subnet from the dropdown.
3. On the **Basics** tab, select **Configure Managed Instance** under **Compute + storage** to open the **Compute + storage** pane:

   :::image type="content" source="media/service-tiers-next-gen-general-purpose-use/new-instance-configure.png" alt-text="Screenshot of the Create Azure SQL Managed Instance page in the Azure portal, with Configure Managed Instance selected.":::

4. Select **Enabled** for *Next-gen General Purpose*. (Optionally) Use the slider to modify IOPS and memory for your instance. Review the *Cost per IOPS* and *Cost per GB* in the **Estimated costs per month** box:

   :::image type="content" source="media/service-tiers-next-gen-general-purpose-use/new-instance-configure-managed-instance.png" alt-text="Screenshot of the compute + storage page when you configure your new Azure SQL Managed Instance in the Azure portal.":::

   > [!NOTE]
   > Default IOPS and memory values are included in the price. You can increase resources at any time after deployment.

5. Select **Apply** to save your instance configuration and go back to the **Create Azure SQL Managed Instance** pane.
6. Fill out the rest of the values to configure your new instance.
7. Select **Review + create** to review your settings, and then select **Create** to deploy your new instance using the Next-gen General Purpose tier upgrade.

#### [PowerShell](#tab/powershell)

To create a new SQL managed instance with the Next-gen General Purpose service tier upgrade by using PowerShell, use [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) and set `-IsGeneralPurposeV2` to `$true`:

```powershell
$adminCredential = Get-Credential
$subnetId = "/subscriptions/<subscriptionId>/resourceGroups/<resourceGroupName>/providers/Microsoft.Network/virtualNetworks/<vnetName>/subnets/<subnetName>"

New-AzSqlInstance `
    -Name "my-nextgen-mi" `
    -ResourceGroupName "myResourceGroup" `
    -Location "eastus" `
    -AdministratorCredential $adminCredential `
    -SubnetId $subnetId `
    -LicenseType "LicenseIncluded" `
    -StorageSizeInGB 512 `
    -VCore 4 `
    -Edition "GeneralPurpose" `
    -ComputeGeneration "Gen5" `
    -IsGeneralPurposeV2 $true
```

#### [CLI](#tab/cli)

To create a new SQL managed instance with the Next-gen General Purpose service tier by using Azure CLI, use [az sql mi create](/cli/azure/sql/mi#az-sql-mi-create) and set `--gpv2` to `true`:

```azurecli
az sql mi create \
    --name my-nextgen-mi \
    --resource-group myResourceGroup \
    --location eastus \
    --admin-user adminuser \
    --admin-password "<your-password>" \
    --subnet "/subscriptions/<subscriptionId>/resourceGroups/<resourceGroupName>/providers/Microsoft.Network/virtualNetworks/<vnetName>/subnets/<subnetName>" \
    --capacity 4 \
    --tier GeneralPurpose \
    --family Gen5 \
    --storage 512 \
    --license-type LicenseIncluded \
    --gpv2 true
```

---

## Remarks

- Zone redundancy isn't available for the Next-gen General Purpose service tier upgrade.

## Related content

- To get started, see [Creating a SQL Managed Instance using the Azure portal](instance-create-quickstart.md)
- For pricing details, see 
    - [Azure SQL Managed Instance single instance pricing page](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/single/)
    - [Azure SQL Managed Instance pools pricing page](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/pools/)
- For details about the specific compute and storage sizes available in the General Purpose and Business Critical service tiers, see [vCore-based resource limits for Azure SQL Managed Instance](resource-limits.md).
- [SLA for Azure SQL Managed Instance](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance/)
- [Frequently asked questions](frequently-asked-questions-faq.yml#next-gen-general-purpose-service-tier-upgrade)
- [Modifiable configuration reference for Azure SQL Managed Instance](modifiable-configuration-reference.md)

---
title: Automatic registration with SQL IaaS Agent extension
description: Learn how to enable the automatic registration feature to automatically register all past and future SQL Server VMs with the SQL IaaS Agent extension using the Azure portal.
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma, randolphwest
ms.date: 07/31/2023
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
tags: azure-service-management
---
# Automatic registration with SQL IaaS Agent extension

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

By default, Azure VMs with SQL Server 2016 or later are automatically registered with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). You can enable the automatic registration feature for your subscription to easily and automatically register any SQL Server VMs not picked up by the CEIP service, such as older versions of SQL Server.

This article teaches you to enable the automatic registration feature. Alternatively, you can [register a single VM](sql-agent-extension-manually-register-single-vm.md), or [register your VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md) with the SQL IaaS Agent extension.

[!INCLUDE [SQL VM extension notes](../../includes/sql-vm-iaas-extension-permissions.md)]

## Overview

Register your SQL Server VM with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) to unlock a full feature set of benefits.

By default, Azure VMs with SQL Server 2016 or later are automatically registered with the SQL IaaS Agent extension with limited functionality when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). You can use the automatic registration feature to automatically register any SQL Server VMs not identified by the CEIP service.  The license type automatically defaults to that of the VM image. If you use a pay-as-you-go image for your VM, then your license type will be `PAYG`, otherwise your license type will be `AHUB` by default. For information about privacy, see the [SQL IaaS Agent extension privacy statements](sql-server-iaas-agent-extension-automate-management.md#in-region-data-residency).

Once automatic registration is enabled for a subscription all current and future VMs that have SQL Server installed are registered with the SQL IaaS Agent extension. This is done by running a monthly job that detects whether or not SQL Server is installed on all the unregistered VMs in the subscription. For unregistered VMs, the job copies the SQL IaaS Agent extension binaries to the VM, then runs a one-time utility to check for the SQL Server registry hive. If the SQL Server hive is detected, the virtual machine is registered with the extension. If no SQL Server hive exists in the registry, the binaries are removed.

Automatic registration offers limited functionality of the extension, such as license management. You can enable more features from the [SQL virtual machines](manage-sql-vm-portal.md) resource in the [Azure portal](https://portal.azure.com).

> [!CAUTION]  
> - If the SQL Server hive is not present in the registry, removing the binaries might be affected if there are [resource locks](/azure/governance/blueprints/concepts/resource-locking#locking-modes-and-states) in place.  
> - If you deployed a SQL Server VM with a marketplace image which has the SQL IaaS Agent extension preinstalled, and the extension is in a failed state or it was removed, automatic registration checks the registry to see if SQL Server is installed on the VM and then registers it with the extension.
> - If automatic registration is activated after [Centrally Managed-AHB (CM-AHB)](licensing-model-azure-hybrid-benefit-ahb-change.md#integration-with-centrally-managed-azure-hybrid-benefit) is enabled, you run the risk of unnecessary pay-as-you-go charges for your SQL Server on Azure VM workloads. To mitigate this risk, adjust your license assignments in CM-AHB to account for the additional usage that will be reported by the SQL IaaS Agent extension after auto-registration. We published an [open source tool](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-hybrid-benefit) that provides insights into the utilization of SQL Server licenses, including the utilization by the SQL Servers on Azure Virtual Machines that are not yet registered with the SQL IaaS Agent extension.



## Prerequisites

To enable automatic registration of your SQL Server VM with the extension, you'll need:

- An [Azure subscription](https://azure.microsoft.com/free/).
- The client credentials used to register the virtual machines to exist in any of the following Azure roles: **Virtual Machine contributor**, **Contributor**, or **Owner**.

Once automatic registration is enabled, SQL Server VMs are registered according to these conditions:

- VMs are deployed using an Azure Resource Model to a [supported](/lifecycle/products/?terms=windows%20server) [Windows Server virtual machine](/azure/virtual-machines/windows/quick-create-portal).
- They have [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) installed.
- VMs are deployed to the public or Azure Government cloud. Other clouds aren't currently supported.
- VMs are running.

> [!NOTE]  
> Automatic registration is supported for Ubuntu Linux VMs in Azure.

## Enable automatic registration

To enable automatic registration of your SQL Server VMs in the Azure portal, follow these steps:

1. Sign into the [Azure portal](https://portal.azure.com).
1. Navigate to the [**SQL virtual machines**](https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.SqlVirtualMachine%2FSqlVirtualMachines) resource page.
1. Select **Automatic SQL Server VM registration** to open the **Automatic registration** page.

   :::image type="content" source="media/sql-agent-extension-automatic-registration-all-vms/automatic-registration.png" alt-text="Screenshot showing how to select Automatic SQL Server VM registration to open the automatic registration page":::

1. Choose your subscription from the dropdown list.
1. Read through the terms and if you agree, select **I accept**.
1. Select **Register** to enable the feature and automatically register all current and future SQL Server VMs with the SQL IaaS Agent extension. This won't restart the SQL Server service on any of the VMs.

## Disable automatic registration

Use the [Azure CLI](/cli/azure/install-azure-cli) or [Azure PowerShell](/powershell/azure/install-az-ps) to disable the automatic registration feature. When the automatic registration feature is disabled, SQL Server VMs added to the subscription need to be manually registered with the SQL IaaS Agent extension. This won't unregister existing SQL Server VMs that have already been registered.

# [Azure CLI](#tab/azure-cli)

To disable automatic registration using Azure CLI, run the following command:

```azurecli-interactive
az feature unregister --namespace Microsoft.SqlVirtualMachine --name BulkRegistration
```

# [Azure PowerShell](#tab/azure-powershell)

To disable automatic registration using Azure PowerShell, run the following command:

```powershell-interactive
Unregister-AzProviderFeature -FeatureName BulkRegistration -ProviderNamespace Microsoft.SqlVirtualMachine
```

---

## Enable for multiple subscriptions

You can enable the automatic registration feature for multiple Azure subscriptions by using PowerShell.

To do so, follow these steps:

1. Save [this script](https://github.com/microsoft/tigertoolbox/blob/master/AzureSQLVM/EnableBySubscription.ps1).
1. Navigate to where you saved the script by using an administrative Command Prompt or PowerShell window.
1. Connect to Azure (`az login`).
1. Execute the script, passing in SubscriptionIds as parameters. If no subscriptions are specified, the script enables auto-registration for all the subscriptions in the  user account.

   The following command enables auto-registration for two subscriptions:

   ```console
   .\EnableBySubscription.ps1 -SubscriptionList a1a1a-aa11-11aa-a1a1-a11a111a1,b2b2b2-bb22-22bb-b2b2-b2b2b2bb
   ```

   The following command enables auto-registration for all subscriptions:

   ```console
   .\EnableBySubscription.ps1
   ```

Failed registration errors are stored in `RegistrationErrors.csv` located in the same directory where you saved and executed the `.ps1` script from.

## Next steps

- Review the benefits provided by the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md).
- [Manually register a single VM](sql-agent-extension-manually-register-single-vm.md)
- [Troubleshoot known issues with the extension](sql-agent-extension-troubleshoot-known-issues.md).
- Review the [SQL IaaS Agent extension privacy statements](sql-server-iaas-agent-extension-automate-management.md#in-region-data-residency).
- Review the [best practices checklist](performance-guidelines-best-practices-checklist.md) to optimize for performance and security.

For more information, review the following articles:

- [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Azure VMs](../windows/pricing-guidance.md)
- [What's new for SQL Server on Azure VMs](../windows/doc-changes-updates-release-notes-whats-new.md)

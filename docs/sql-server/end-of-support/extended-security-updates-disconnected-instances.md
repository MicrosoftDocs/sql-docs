---
title: "Register SQL Server instances for ESUs"
description: Learn how to register SQL Server instances in the Azure portal to receive Extended Security Updates (ESUs) for SQL Server 2014 and SQL Server 2016.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: install
ms.topic: concept-article
ms.custom:
  - references_regions
monikerRange: ">=sql-server-2017"
---
# Register SQL Server instances for ESUs
[!INCLUDE [sql-migration-end-of-support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

This article explains how to manually register [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances in the Azure portal to receive Extended Security Updates (ESUs) purchased through volume licensing. 

You must manually register [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances to receive ESUs in the following scenarios:
- You can't connect your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance to Azure Arc directly.
- Your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Azure VM is in a region that doesn't currently support ESUs for Azure VMs.

If those scenarios don't apply to your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance and you prefer to take advantage of the flexibility of ESUs enabled by Azure Arc, [connect your server to Azure Arc](../azure-arc/automatically-connect.md) or [register your SQL Server on Azure VM](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm) with the SQL IaaS Agent extension.

[!INCLUDE [2016-esu](../../includes/2016-esu.md)]

## Prerequisites

To register your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances and receive ESUs, you need the following prerequisites:

- If you don't already have an Azure subscription, create an account by using one of the following methods:

   - [Create a Microsoft Customer Agreement subscription](/azure/cost-management-billing/manage/create-subscription)
   - [Create an Enterprise Agreement subscription](/azure/cost-management-billing/manage/create-enterprise-subscription)
   - [Create an Azure account with pay-as-you-go pricing](https://azure.microsoft.com/pricing/purchase-options/pay-as-you-go/)
   - [Create a free Azure account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn)

- The user registering [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances resources must have the following permissions:

   - `Microsoft.AzureArcData/sqlServerInstances/read`
   - `Microsoft.AzureArcData/sqlServerInstances/write`

   Assign users to the `Azure Connected SQL Server Onboarding` role to grant these specific permissions, or assign them to built-in roles such as Contributor or Owner that have these permissions. For more information, see [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal).

- Register the `Microsoft.AzureArcData` resource provider in your Azure subscription:

   - Sign in to the Azure portal.

   - Navigate to your subscription, and select **Resource providers**.

   - If the `Microsoft.AzureArcData` resource provider isn't listed, add it to your subscription by using the **Register** option.

- If you use Azure policies that only allow the creation of specific resource types, you need to allow the `Microsoft.AzureArcData/sqlServerInstances` resource type. If you don't allow this resource type, the `SQLServerInstances_Update` operation fails with a **'deny' Policy action** log entry in the activity log of the subscription.

You can either register a [single SQL Server instance](#single-sql-server-instance), or upload a CSV file to register [multiple SQL Server instances in bulk](#multiple-sql-server-instances-in-bulk).

## Single SQL Server instance

1. Go to the [SQL Server instances](https://portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerInstances/resourceType/Microsoft.AzureArcData%2FsqlServerInstances) pane in the Azure portal.

1. To register a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], select **Add** in the navigation bar:

   :::image type="content" source="media/extended-security-updates-disconnected-instances/add-server.png" alt-text="Screenshot of the Add button in the navigation bar of the SQL Server instances pane.":::

1. Select **Register SQL Server instances** to add a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

   :::image type="content" source="media/extended-security-updates-disconnected-instances/extended-security-updates-add-connected-or-registered.png" alt-text="Screenshot for adding registered servers when registering a single instance." lightbox="media/extended-security-updates-disconnected-instances/extended-security-updates-add-connected-or-registered.png":::

1. Specify **Single SQL Server Instance**. This value is the default.

1. Choose the **Subscription** and **Resource group** for your registered [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

1. Enter the required information as detailed in the following table, and then select **Next**:

   | Value | Description | Additional information |
   | --- | --- | --- |
   | **Instance Name** | Enter the output of command `SELECT @@SERVERNAME`. | If you have a named instance, replace the backslash (`\`) with a hyphen (`-`). For example, `MyServer\Instance01` becomes `MyServer-Instance01`. |
   | **SQL Server Version** | Select your version from the dropdown list. | |
   | **Edition** | Select the applicable edition from the dropdown list: Datacenter, Developer (free to deploy if purchased ESUs), Enterprise, Standard, Web, Workgroup. | |
   | **Cores** | Enter the number of cores for this instance | |
   | **Host Type** | Select the applicable host type from the dropdown list: Virtual machine (on-premises), Physical Server (on-premises), Azure Virtual Machine, Amazon EC2, Google Compute Engine, Other. | |

1. Confirm that you have the rights to receive ESUs by using the checkbox.

## Multiple SQL Server instances in bulk

You can register multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances at once by uploading a .CSV file. After you [format your .CSV file correctly](#formatting-requirements-for-csv-file), follow these steps to bulk register your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances:

1. Go to the [SQL Server instances](https://portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerInstances/resourceType/Microsoft.AzureArcData%2FsqlServerInstances) pane in the Azure portal.

1. To register a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance, select **Add** in the navigation bar:

   :::image type="content" source="media/extended-security-updates-disconnected-instances/add-server.png" alt-text="Screenshot of the Add button in the navigation bar for bulk instance registration.":::

1. Select **Register SQL Server instances** to add a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

   :::image type="content" source="media/extended-security-updates-disconnected-instances/extended-security-updates-add-connected-or-registered.png" alt-text="Screenshot of the options for adding connected or registered servers when registering instances in bulk." lightbox="media/extended-security-updates-disconnected-instances/extended-security-updates-add-connected-or-registered.png":::

1. Select the option for **Multiple SQL Instances**.

1. Select the Browse icon to upload the CSV file containing multiple disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances.

1. Confirm that you have the rights to receive ESUs by using the checkbox provided.

After you add your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances, you see them in the portal after a few minutes. Because you added them manually, they always show the description **Registered**.

:::image type="content" source="media/extended-security-updates-disconnected-instances/extended-security-updates-connected-servers.png" alt-text="Screenshot of two registered SQL Server instances on the Azure Arc portal." lightbox="media/extended-security-updates-disconnected-instances/extended-security-updates-connected-servers.png":::

## Formatting requirements for CSV file

- Use commas to separate values.
- Don't use single or double quotes around values.
- Values can include letters, numbers, hyphens (`-`), and underscores (`_`). Don't use other special characters. If you have a named instance, replace the backslash (`\`) with a hyphen (`-`). For example, `MyServer\Instance01` becomes `MyServer-Instance01`.
- Column names are case-sensitive and must be named as follows:

  - name
  - version
  - edition
  - cores
  - hostType

### Example CSV file

The CSV file should look like this:

```csv
name,version,edition,cores,hostType
Server1-SQL2014,SQL Server 2014,Enterprise,12,Other Physical Server
Server2-SQL2014,SQL Server 2014,Enterprise,24,Other Physical Server
Server3-SQL2014,SQL Server 2014,Enterprise,12,Azure Virtual Machine
Server4-SQL2014,SQL Server 2014,Standard,8,Azure VMware Solution
```

## Link ESU invoice

Use the **Purchase Order Number** under Invoice Summary in your Microsoft invoice (as shown in the following screenshot) for the Invoice ID value to link the ESU purchase with the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances.

:::image type="content" source="media/extended-security-updates-disconnected-instances/extended-security-updates-invoice-sample.png" alt-text="Screenshot of Sample invoice with Purchase Order Number highlighted.":::

To link an ESU invoice to your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances and get access to extended updates, follow these steps. This example includes both **Connected** and **Registered** instances.

1. Go to the [SQL Server instances](https://portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerInstances/resourceType/Microsoft.AzureArcData%2FsqlServerInstances) pane in the Azure portal.

1. Use the checkboxes next to each [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance you want to link, and then select **Link ESU invoice**.

   :::image type="content" source="media/extended-security-updates-disconnected-instances/extended-security-updates-invoice-select.png" alt-text="Screenshot of all SQL Server instances on the Azure Arc section." lightbox="media/extended-security-updates-disconnected-instances/extended-security-updates-invoice-select.png":::

1. Enter the ESU invoice number in the **Invoice ID** section, and then select **Link invoice**.

   :::image type="content" source="media/extended-security-updates-disconnected-instances/extended-security-updates-invoice-save.png" alt-text="Screenshot of the invoice ID on the Link ESU invoice page.":::

1. The servers you linked to the ESU invoice now show a valid ESU expiration date.

   :::image type="content" source="media/extended-security-updates-disconnected-instances/extended-security-updates-invoice-linked.png" alt-text="Screenshot of SQL Server instances with a valid ESU expiration value." lightbox="media/extended-security-updates-disconnected-instances/extended-security-updates-invoice-linked.png":::

> [!IMPORTANT]  
> When registering an ESU VL product for disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] servers, select only the instances with the **Status** of `Registered`.

## Download ESUs

After you link your ESU invoice, you can manually download ESUs from the **Extended Security Updates** pane for your [SQL Server instance](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/SqlServerInstance) in the Azure portal.

## Frequently asked questions

For a full list of frequently asked questions, see the [Extended Security Updates: Frequently asked questions](extended-security-updates-frequently-asked-questions.md).

## Related content

- [SQL Server 2014 lifecycle page](/lifecycle/products/sql-server-2014)
- [SQL Server end of support page](sql-server-end-of-support-overview.md?WT.mc_id=akamseos)
- [Extended Security Updates frequently asked questions (FAQ)](/lifecycle/faq/extended-security-updates)
- [Microsoft Security Response Center (MSRC)](https://msrc.microsoft.com/security-guidance/summary)
- [Update Management overview](/azure/automation/update-management/overview)
- [Automated Patching for SQL Server on Azure virtual machines](/azure/azure-sql/virtual-machines/windows/automated-patching)
- [Microsoft Data Migration Guide](/data-migration/)
- [Azure migrate: lift-and-shift options to move your current SQL Server into an Azure VM](https://azure.microsoft.com/services/azure-migrate/)
- [Cloud adoption framework for SQL migration](/azure/cloud-adoption-framework/migrate/expanded-scope/sql-migration)
- [ESU-related scripts on GitHub](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/sql-server-extended-security-updates/scripts)

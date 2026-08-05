---
title: "What Are Extended Security Updates?"
description: Learn about Extended Security Updates enabled by Azure Arc, for your end-of-support and end-of-life SQL Server products such as SQL Server 2014 and SQL Server 2016.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: install
ms.topic: concept-article
ms.custom:
  - references_regions
---

# What are Extended Security Updates for SQL Server?

[!INCLUDE [SQL Server end of support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

This article provides information on how to receive Extended Security Updates (ESUs) for versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] that are out of extended support.

ESUs are available for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], and [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] ESUs introduce a [change to the price structure](#changes-to-sql-server-2016-esus).

For more information, see:
- [Plan your Windows Server and SQL Server end of support](https://www.microsoft.com/windows-server/extended-security-updates)
- [SQL Server end of support options](sql-server-end-of-support-overview.md)
- [Frequently asked ESU questions](extended-security-updates-frequently-asked-questions.yml)

## Overview

When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reaches the end of its support lifecycle, you can subscribe to Extended Security Updates. The subscription protects your servers for up to three years after the support lifecycle ends. Keep the subscription until you're ready to upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [migrate to Azure SQL](/azure/azure-sql/migration-guides/).

ESUs released for **[!INCLUDE [sssql14-md](../../includes/sssql14-md.md)]** and **[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]** include the most recent Cumulative Update (CU). If you only applied [General Distribution Release](/troubleshoot/sql/releases/servicing-models-sql-server#general-distribution-release-gdr) (GDR) updates during the normal support period, install and validate the latest CU at the time you subscribe to receive ESUs, instead of waiting until the first ESU is released. This preemptive validation avoids potential problems when installing the ESU later.

Microsoft makes ESUs available **if needed** once a security vulnerability is discovered and rated as **Critical** by the [Microsoft Security Response Center (MSRC)](https://msrc.microsoft.com/update-guide). Therefore, there's no regular release cadence for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESUs.

ESUs don't include:

- New features
- Functional improvements
- Customer-requested fixes

## Changes to SQL Server 2016 ESUs

Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], migrating your workload to SQL Server on Azure VMs no longer provides free access to ESUs for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] instances.

## ESU availability by platform

SQL Server instances connected to Azure Arc, or hosted on SQL Server on Azure VMs, can subscribe to receive ESUs.

Consider the following: 
- Coverage is continuous until canceled. You can cancel the subscription at any time.
- Azure bills on an hourly basis.
- The subscription is automatically canceled when you migrate your instance to Azure or upgrade to a supported version of SQL Server.
- You can install patches automatically or manually.

Alternatively, for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] only, you can migrate your SQL Server workloads to an Azure VM **as-is** and receive free ESUs through the Windows Update channel. For more information, see [Extend support for SQL Server 2014](/azure/azure-sql/virtual-machines/windows/extended-security-updates-sql-vm).

The method of receiving ESUs depends on where you're running your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

#### [SQL Server on Azure VMs](#tab/sql-vm)

SQL Server on Azure VM customers can subscribe to receive ESUs when they register with the [SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm). 

ESUs are available in all regions supported by the SQL IaaS Agent extension. 

SQL Server 2014 customers receive free ESUs when they migrate their workloads to SQL Server on Azure VMs. SQL Server 2016 customers can subscribe to receive ESUs when they register with the SQL IaaS Agent extension, but they aren't eligible for free ESUs.

To get started, see [ESUs for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/extended-security-updates-sql-vm).

#### [Other Azure environments](#tab/azure-other)

For other Azure resources, you have access to ESUs for [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] if you connect your instance to Azure Arc. If you can't connect your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance to Azure Arc, you don't qualify for this offer.

SQL Server 2014 customers receive free ESUs when they migrate their workloads to Azure VMware VMs. SQL Server 2016 customers can subscribe to receive ESUs when they migrate to Azure VMware VMs, but they aren't eligible for free ESUs.

To subscribe and receive ESUs, review the documentation for the specific service:

- [Azure VMware Solution (AVS)](/azure/azure-vmware/extended-security-updates-windows-sql-server)
- Azure Stack Hub
- [Azure Stack HCI](/azure-stack/hci/manage/azure-benefits?#enable-azure-benefits)

#### [All other environments](#tab/all-other)

For all other environments, including on-premises, non-Azure cloud infrastructure, or hosted environments, you can subscribe to receive ESUs after [connecting your servers to Azure Arc](../azure-arc/automatically-connect.md). If you can't connect your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance to Azure Arc, you don't qualify for this offer.

To qualify to receive ESUs, you must have Software Assurance under one of the following agreements:

- Enterprise Agreement (EA)
- Enterprise Agreement Subscription (EAS)
- Server and Cloud Enrollment (SCE)
- Enrollment for Education Solutions (EES)

Alternatively, you can connect your instances to Azure Arc and enable a pay-as-you-go billing option to receive ESUs without Software Assurance. For more information, see [Subscribe to ESUs enabled by Azure Arc](#subscribe-to-esus-for-sql-server-instances).

For resources: 
- To get started: [Connect your SQL Server instances to Azure Arc](../azure-arc/automatically-connect.md). 
- For details: [SQL Server ESUs enabled by Azure Arc](../azure-arc/extended-security-updates.md).
- For more information: [Extended Security Updates frequently asked questions](https://www.microsoft.com/windows-server/extended-security-updates).

> [!NOTE]  
> Connecting SQL Server instances to Azure Arc is free of charge. 

---

## Subscribe to ESUs for SQL Server instances

[!INCLUDE [esu-enable-sql-server-instances](../../includes/esu-enable-sql-server-instances.md)]


## ESU patch delivery

After you subscribe to ESUs, you receive patches through the same channels as regular updates, such as Microsoft Update, Windows Update, System Center Configuration Manager, or Azure Update Manager. You can also download the patches directly from the Azure portal.
 
Instances that have automatic updates enabled receive ESUs automatically.

## Register disconnected SQL Server instances in Azure portal

If you can't connect your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance to Azure Arc, you might be eligible to purchase ESUs through volume licensing. Contact your Microsoft account team for more information.

After purchasing, you can manually register your instance in the Azure portal to enable access to the ESUs. For more information, see [Register disconnected instances for ESUs](extended-security-updates-disconnected-instances.md).

## Support

ESU licenses don't include support for the underlying SQL Server version. Support for products covered by ESUs is limited to issues related to deploying, installing, and activating the updates released as part of the ESU subscription, such as bugs or regressions introduced by a specific update.


## Supported regions

In Azure, you can subscribe to ESUs in any region supported by SQL Server on Azure VMs, AVS, or Nutanix. For details, see [Product availability by region](https://azure.microsoft.com/explore/global-infrastructure/products-by-region/table?msockid=115cad88d78e69e1137fbbebd6016861).

Outside of Azure, you can subscribe to ESUs in any region that supports SQL Server enabled by Azure Arc. For details, see [Supported Azure regions](../azure-arc/overview.md#supported-azure-regions).

## Frequently asked questions

For a full list of frequently asked questions, see the [SQL Server ESUs: Frequently asked questions](extended-security-updates-frequently-asked-questions.yml).

## Related content

- [SQL Server 2014 lifecycle page](/lifecycle/products/sql-server-2014)
- [SQL Server 2016 lifecycle page](/lifecycle/products/sql-server-2016)
- [SQL Server end of support page](sql-server-end-of-support-overview.md)
- [SQL Server ESUs enabled by Azure Arc](../azure-arc/extended-security-updates.md)
- [Microsoft ESUs frequently asked questions (FAQ)](/lifecycle/faq/extended-security-updates)
- [Microsoft Security Response Center (MSRC)](https://msrc.microsoft.com/security-guidance/summary)
- [Microsoft Data Migration Guide](/data-migration/)
- [Azure migrate: lift-and-shift options to move your current SQL Server into an Azure VM](https://azure.microsoft.com/services/azure-migrate/)
- [ESU-related scripts on GitHub](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/sql-server-extended-security-updates/scripts)

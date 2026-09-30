---
title: SQL Server on Azure Local overview
description: Learn how SQL Server on Azure Local runs database workloads on your infrastructure with connected or disconnected management and local data residency.
author: MashaMSFT
ms.author: mathoma
ms.date: 09/28/2026
ms.topic: overview
ms.service: sql
ai-usage: ai-assisted
---

# SQL Server on Azure Local overview

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

SQL Server on Azure Local runs SQL Server workloads on Windows Server or Linux virtual machines (VMs) in your own infrastructure. You can keep data close to users and applications while choosing a management model that fits your connectivity, data residency, and governance requirements.

[Azure Local](/azure/azure-local/overview) extends Azure capabilities to infrastructure in your own environment. SQL Server runs inside virtual machines on Azure Local, allowing you to keep SQL Server workloads and data locally while using Azure-consistent infrastructure and management experiences.

## Deployment modes

SQL Server on Azure Local supports connected and disconnected environments. The management capabilities and prerequisites differ between these modes.

### Connected mode

In connected mode, Azure Local maintains connectivity to Azure. You can connect SQL Server instances to Azure Arc for centralized inventory, governance, monitoring, security, and licensing. Database workloads continue to run locally while Azure services provide management capabilities.

SQL Server on Azure Local in connected mode is generally available (GA). For deployment guidance, see [Deploy SQL Server on Azure Local](deploy.md).

### Disconnected operations

Azure Local disconnected operations support environments where connectivity to Azure or the internet is restricted or unavailable. SQL Server workloads and the Azure Local control plane run within your environment, without an ongoing dependency on the public-cloud control plane.

This mode is intended for regulated, secure, remote, or air-gapped environments. For deployment guidance, see [Deploy SQL Server on Azure Local with disconnected operations](deploy-disconnected.md).

> [!IMPORTANT]
> The SQL Server extension for Azure Arc isn't supported for SQL Server on Azure Local with disconnected operations. Capabilities provided by extension, including SQL Server inventory, best practices assessments, and SQL Server management experiences in Azure, aren't available in disconnected deployments.

## When to use SQL Server on Azure Local

Consider SQL Server on Azure Local when you need to:

- Meet data residency and compliance requirements by keeping sensitive data on-premises.
- Support low-latency applications with databases close to users, devices, factory locations, or application servers.
- Manage hybrid environments that span on-premises infrastructure and Azure.
- Modernize existing SQL Server deployments without moving databases to the public cloud.
- Centralize governance, monitoring, and security through Azure services in connected environments.

SQL Server on Azure Local also supports local AI and analytics scenarios. Applications can use operational data locally to reduce data movement and inference latency while meeting governance requirements. For more information, see [Build local AI applications](deploy.md#build-local-ai-applications).

## How connected deployments work

At a high level, a connected deployment consists of the following stages:

1. Deploy Azure Local.
1. Create Windows Server or Linux VMs and install SQL Server.
1. Connect SQL Server to Azure Arc for supported Azure management capabilities.
1. Configure monitoring, governance, security, and licensing as required.
1. Plan SQL Server availability, backup, and disaster recovery based on your workload requirements.
For the complete procedure, see [Deploy SQL Server on Azure Local](deploy.md).

## How disconnected deployments work

At a high level, a disconnected deployment consists of the following stages:

1. Deploy Azure Local with disconnected operations.
1. Create Windows Server or Linux VMs in the disconnected environment and install SQL Server.
1. Manage and monitor SQL Server by using supported local tools.
1. Configure SQL Server availability, backup, and disaster recovery based on your workload requirements.

For the complete procedure, see [Deploy SQL Server on Azure Local with disconnected operations](deploy-disconnected.md).

## Related content

- [Deploy SQL Server on Azure Local](deploy.md)
- [Deploy SQL Server on Azure Local with disconnected operations](deploy-disconnected.md)
- [SQL Server enabled by Azure Arc](../azure-arc/overview.md)
- [Azure Local disconnected operations overview](/azure/azure-local/manage/disconnected-operations-overview)
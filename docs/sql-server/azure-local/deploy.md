---
title: Deploy SQL Server on Azure Local
description: Deploy SQL Server in Windows Server or Linux virtual machines on Azure Local, and configure connected management, availability, security, and licensing.
author: MashaMSFT
ms.author: mathoma
ms.date: 09/28/2026
ms.topic: how-to
ms.service: sql
ai-usage: ai-assisted
---

# Deploy SQL Server on Azure Local

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article describes how to deploy SQL Server on Azure Local in connected mode. SQL Server runs on Windows Server or Linux virtual machines (VMs) in an Azure Local cluster that maintains connectivity to Azure. This configuration combines local execution and data residency with Azure-based management, monitoring, billing, and hybrid services.

For environments without ongoing connectivity to Azure, see [Deploy SQL Server on Azure Local with disconnected operations](deploy-disconnected.md).

## Prerequisites

Before you begin, confirm that you have:

- Supported hardware for Azure Local. Validate your environment with the [Azure Local environment checker](/azure/azure-local/manage/use-environment-checker?tabs=connectivity).
- An [Azure subscription](/azure/azure-portal/get-subscription-tenant-id) for management and hybrid services.
- Network connectivity between Azure Local and Azure that meets the [Azure Local firewall requirements](/azure/azure-local/concepts/firewall-requirements).
- Supported Windows Server or Linux images to [create VMs on Azure Local](/azure/azure-local/manage/create-arc-virtual-machines) and host SQL Server.
- Appropriate SQL Server licensing. Review [SQL Server licensing and billing through Azure Arc](../azure-arc/manage-license-billing.md).
- The permissions and connectivity required to [connect SQL Server to Azure Arc](../azure-arc/prerequisites.md) when you reach the onboarding stage.

The following workflow covers deploying Azure Local and creating the SQL Server VMs.

## Plan the SQL Server workload

Before sizing the VMs, capture the workload's requirements:

- Select the SQL Server version and edition based on database size, availability features, online operations, virtualization rights, and support lifecycle.
- Record peak CPU utilization, working-set memory, input/output operations per second (IOPS), throughput, latency, database growth, `tempdb` demand, log-generation rate, and concurrency.
- Define recovery point objectives (RPOs) and recovery time objectives (RTOs).
- Identify application dependencies, authentication methods, Transport Layer Security (TLS) requirements, ports, service accounts, maintenance windows, backup retention, and offline software dependencies.

## Design the SQL Server VMs

Use the following considerations when designing each VM:

| Design area | SQL Server guidance |
| --- | --- |
| Compute | Size virtual CPUs for sustained utilization and licensing efficiency. Avoid oversubscription that creates unpredictable query latency. |
| Memory | Reserve memory for the operating system, agents, drivers, and failover activity. Configure `max server memory` so SQL Server doesn't consume memory needed by other processes. |
| Storage | Use separate volumes for the operating system, data, transaction logs, `tempdb`, and backups. Validate latency, throughput, queue depth, capacity, resiliency, and free-space thresholds. |
| Network | Provide stable client, management, backup, and replica paths as required. Validate DNS, maximum transmission unit (MTU), firewall rules, TLS trust, and listener connectivity. |
| Placement | Place high-availability replicas on different Azure Local nodes or failure domains. Use anti-affinity where available. |

## Deployment workflow

The following stages take you from infrastructure deployment to connected SQL Server management.

| Stage | Outcome |
| --- | --- |
| [Acquire hardware and deploy Azure Local](#deploy-azure-local) | A supported Azure Local environment. |
| [Create VMs and install SQL Server](#create-vms-and-install-sql-server) | SQL Server running on Windows Server or Linux. |
| [Monitor and tune SQL Server](#monitor-and-tune-sql-server) | A baseline for SQL Server performance and health. |
| [Configure high availability](#configure-high-availability) | A workload-specific availability design, such as availability groups or failover cluster instances. |
| [Connect SQL Server to Azure Arc](#connect-sql-server-to-azure-arc) | Centralized inventory, governance, security, and licensing. |

### Deploy Azure Local

Deploy Azure Local by following the [Azure Local deployment overview](/azure/azure-local/deploy/deployment-introduction). Complete the applicable deployment prerequisites and deployment process before you create VMs for your SQL Server workloads.

### Create VMs and install SQL Server

1. Create [Windows Server or Linux VMs on Azure Local](/azure/azure-local/manage/create-arc-virtual-machines) for your SQL Server workloads.
1. Install SQL Server by following the instructions for your operating system:

   - [Install SQL Server on Windows](../../database-engine/install-windows/install-sql-server.md).
   - [Install SQL Server on Linux](../../linux/install-upgrade/setup.md).

1. Review the [SQL Server licensing and billing requirements](../azure-arc/manage-license-billing.md) and configure the appropriate licensing model.

## Monitor and tune SQL Server

Monitor SQL Server with dynamic management views, Extended Events, error logs, and a monitoring solution supported by the guest operating system. On Windows, you can also use SQL Server Agent alerts and Windows performance counters. SQL Server Agent alerts aren't supported on Linux; review the [SQL Server on Linux feature support](../../linux/sql-server-linux-editions-and-components-2025.md) for your version.

Establish a baseline for CPU utilization, memory pressure, storage latency, blocking, database growth, and backup health before tuning the workload. After connecting SQL Server to Azure Arc, you can also run a [best practices assessment](../azure-arc/assess.md), subject to its prerequisites.

## Configure high availability

Azure Local provides host-level resilience for VMs. Configure SQL Server high availability separately to meet your workload's database or instance availability requirements:

- [Always On availability groups](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) provide database-level protection. For Windows deployments, review [Windows Server Failover Clustering with SQL Server](../failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md).
- On Windows, [Always On failover cluster instances](../failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md) provide instance-level protection and require a supported shared-storage design. Review the [Storage Spaces Direct overview](/windows-server/storage/storage-spaces/storage-spaces-direct-overview) when planning storage.
- Use [cluster affinity rules](/windows-server/failover-clustering/cluster-affinity) to place SQL Server replicas on different physical nodes. This placement helps maintain availability if a host fails.
- For connected Windows clusters, review [Cloud Witness](/windows-server/failover-clustering/deploy-cloud-witness) as a quorum option.

For Linux, an availability group without a cluster manager provides read-scale replicas, not high availability. High-availability deployments require a supported cluster manager, such as Pacemaker, and production deployments require fencing. See [Configure availability groups on Linux](../../linux/business-continuity/availability-groups/configure.md). Validate the Linux distribution, cluster manager, fencing mechanism, and storage support for your Azure Local configuration; the Windows procedures don't apply unchanged to Linux.

Plan workload protection separately from infrastructure resilience. For Azure Backup and Azure Site Recovery options, review [Azure Local workload resiliency and disaster recovery](/azure/azure-local/manage/disaster-recovery-workloads-resiliency) and the support requirements for your workload.

## Connect SQL Server to Azure Arc

Connect SQL Server instances running on Azure Local to Azure Arc to create Azure resources for centralized management.

1. Validate the [Azure Arc prerequisites](../azure-arc/prerequisites.md).
1. Follow [Connect SQL Server to Azure Arc](../azure-arc/connect.md) to generate and run the onboarding script.
1. Confirm that the SQL Server resources appear in the Azure portal.

After onboarding, you can use the following capabilities, subject to their version and configuration requirements:

| Capability | Guidance |
| --- | --- |
| Inventory and reporting | View instances, databases, versions, editions, and host operating systems. Use Azure Resource Graph to query across your SQL Server estate. See [SQL Server enabled by Azure Arc](../azure-arc/overview.md). |
| Best practices assessment | Receive recommendations for performance and security. See [Best practices assessment](../azure-arc/assess.md). |
| Identity | Use [Microsoft Entra authentication](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md), subject to its version and operating-system requirements. The [managed identity setup for Microsoft Entra authentication](../azure-arc/managed-identity.md) requires Arc-enabled SQL Server 2025 on Windows Server and uses the Arc-enabled server's system-assigned identity. Microsoft Entra authentication isn't supported with failover cluster instances. |
| Security and governance | Configure [Microsoft Defender for Cloud](../azure-arc/configure-advanced-data-security.md), or [register and scan SQL Server with Microsoft Purview](/purview/register-scan-azure-arc-enabled-sql-server). |
| Operations | Manage configuration and run approved scripts across onboarded servers. See [SQL Server enabled by Azure Arc](../azure-arc/overview.md) and [Manage configuration](../azure-arc/manage-configuration.md). |

## Configure licensing through Azure Arc

Use SQL Server enabled by Azure Arc to declare and manage the licensing and billing model for your SQL Server instances:

- **Pay-as-you-go:** Subscribe through Azure and pay for the licensed cores reported by Azure Arc. This model can suit variable, temporary, or incremental workloads.
- **License with Software Assurance or subscription:** Declare an existing SQL Server license covered by active Software Assurance or a SQL Server subscription. Azure Arc records the license type and enables applicable management benefits.
- **Core scope:** Evaluate virtual-core licensing for individual VMs and eligible Enterprise physical-core licensing with unlimited virtualization. Validate eligibility before selecting a licensing model.
- **Configuration:** Set and review the license type through the Azure portal, Azure PowerShell, or Azure CLI. Apply changes at scale when required.
- **Reporting:** Keep the Azure Connected Machine agent and SQL Server extension healthy so inventory, usage reporting, and billing remain accurate.

On Linux, pay-as-you-go billing doesn't automatically detect passive availability-group replicas or failover cluster instances. All SQL Server instances are billed as active, regardless of their high-availability or disaster-recovery role. Account for this difference when planning Linux deployments. See [Pay-as-you-go considerations](../azure-arc/manage-license-billing.md#important-pay-as-you-go-considerations).

For requirements and procedures, see [Manage licensing and billing](../azure-arc/manage-license-billing.md), [Manage configuration](../azure-arc/manage-configuration.md), and [Manage the transition to pay-as-you-go](../azure-arc/manage-pay-as-you-go-transition.md).

## Build local AI applications

SQL Server 2025 can use [sp_invoke_external_rest_endpoint](../../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md) to call a compatible HTTPS chat-completion endpoint. Applications can combine SQL data with locally hosted models while keeping inference within your environment.

Before calling a local model endpoint:

- Enable the `external rest endpoint enabled` server configuration option. The procedure is disabled by default in SQL Server 2025.
- Grant the calling database principal `EXECUTE ANY EXTERNAL ENDPOINT` permission, and configure the authentication and credentials required by the endpoint.
- Deploy the model and make its HTTPS endpoint reachable from the SQL Server VM. Configure TLS certificates and certificate trust for production use; a test that bypasses certificate validation doesn't establish that SQL Server can connect securely.

For details, see the [REST endpoint permissions and prerequisites](../../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md#permissions) and [Foundry Local inference guidance](/azure/azure-sovereign-clouds/private/foundry-local/how-to-run-inference).

- **SQL Server integration:** Send authorized REST requests from SQL Server 2025 to a compatible chat-completion endpoint and process the response in your application workflow.
- **Local model inference:** Foundry Local on Azure Local is in preview. Use supported chat-capable catalog models or [bring-your-own models](/azure/azure-sovereign-clouds/private/foundry-local/concept-bring-your-own-models), and complete the offering's deployment and access prerequisites.
- **Developer tools:** Review [GitHub Copilot bring your own key](https://docs.github.com/en/copilot/concepts/models/bring-your-own-key) for supported model endpoints and client requirements before connecting Visual Studio Code to a local endpoint.

For more information, see the [Foundry Local documentation](/azure/foundry-local/) and the endpoint requirements in [sp_invoke_external_rest_endpoint](../../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md).

## Use Trusted launch VMs

[Trusted launch for Azure Local VMs](/azure/azure-local/manage/trusted-launch-vm-overview) supports Secure Boot and a virtual Trusted Platform Module (vTPM). If your SQL Server VMs use TPM-backed protection, review [automatic state transfer](/azure/azure-local/manage/trusted-launch-automatic-state-transfer) to understand how vTPM state is handled when VMs migrate or fail over between nodes.

SQL Server doesn't require a vTPM. If you use a vTPM for guest operating system protection or encryption, validate those requirements separately from your SQL Server availability design.

For requirements and limitations, see [Trusted launch for Azure Local VMs](/azure/azure-local/manage/trusted-launch-vm-overview).

## Related content

- [SQL Server on Azure Local overview](overview.md)
- [Deploy SQL Server on Azure Local with disconnected operations](deploy-disconnected.md)
- [Deploy SQL Server on Azure Local in the Azure Local documentation](/azure/azure-local/deploy/sql-server-23h2)
- [SQL Server enabled by Azure Arc frequently asked questions](../azure-arc/faq.yml)

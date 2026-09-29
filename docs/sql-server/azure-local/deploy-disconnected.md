---
title: Deploy SQL Server on Azure Local with disconnected operations
description: Plan and deploy SQL Server on Azure Local with disconnected operations, including local security, backups, monitoring, licensing, and offline updates.
author: MashaMSFT
ms.author: mathoma
ms.date: 09/28/2026
ms.topic: how-to
ms.service: sql
ai-usage: ai-assisted
---

# Deploy SQL Server on Azure Local with disconnected operations

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article describes how to plan, deploy, secure, and operate SQL Server on Windows Server or supported Linux virtual machines (VMs) in Azure Local with disconnected operations.

Before you begin:

- Get your organization's approval.
- Acquire the disconnected operations platform.
- Deploy the platform within your organization's security boundary.

SQL Server workloads, the local control plane, management data, identity integration, and governance remain on-premises. The environment doesn't require ongoing connectivity to Azure or the internet. Use approved staged or offline transfer workflows for updates, support bundles, registration data, and other required artifacts.

For deployments that maintain connectivity to Azure, see [Deploy SQL Server on Azure Local](deploy.md).

## When to use disconnected operations

Consider disconnected operations for:

- Sovereign, regulated, or isolated environments where public-cloud connectivity is prohibited or unreliable.
- Sites that require local execution, local administration, and control over data and operational telemetry.
- SQL Server workloads that need Azure Local virtualization and storage without a dependency on the public Azure control plane.

You can run transactional, analytics, business intelligence, and data warehouse workloads locally. Integrate the SQL Server VMs with your approved on-premises identity, monitoring, backup, security, patching, and software-distribution systems.

> [!IMPORTANT]
> The SQL Server extension for Azure Arc isn't supported with disconnected operations. Azure Arc SQL Server inventory, best practices assessment, and Azure-based SQL Server management capabilities aren't available through this extension. Use supported local tools to manage and monitor SQL Server in each VM.

## Prerequisites

Before deploying SQL Server, confirm that you have:

- These prerequisites don't require ongoing connectivity to Azure or the internet.
- An approved Azure Local disconnected operations environment deployed on supported hardware within your organization's security boundary. Review the [supported services](/azure/azure-local/manage/disconnected-operations-overview) and complete the [platform deployment](/azure/azure-local/manage/disconnected-operations-deploy) before continuing.
- Supported Windows Server or Linux images and sufficient compute and storage capacity to [create workload VMs](/azure/azure-local/manage/disconnected-operations-arc-vm).
- Local networking, identity, certificates, and time synchronization that meet the platform's [network requirements](/azure/azure-local/manage/disconnected-operations-network) and [identity requirements](/azure/azure-local/manage/disconnected-operations-identity).
- Appropriate SQL Server licenses and separate Azure Local disconnected operations platform licensing. Review [licensing and support boundaries](#licensing-and-support-boundaries) and [disconnected operations billing](/azure/azure-local/manage/disconnected-operations-billing).
- Approved SQL Server installation media, cumulative updates, drivers, management tools, and prerequisite packages, with a controlled offline process to transfer and verify these artifacts.
- Local monitoring and protected backup targets for SQL Server workloads, separate from control-plane backups.
- An approved offline servicing process for SQL Server and guest operating-system updates.


## Plan the SQL Server workload

Before sizing the VMs, capture the workload's requirements:

- Select the SQL Server version and edition based on database size, availability features, online operations, virtualization rights, and support lifecycle.
- Record peak CPU utilization, working-set memory, input/output operations per second (IOPS), throughput, latency, database growth, `tempdb` demand, log-generation rate, and concurrency.
- Define recovery point objectives (RPOs) and recovery time objectives (RTOs).
- Identify application dependencies, authentication methods, Transport Layer Security (TLS) requirements, ports, service accounts, maintenance windows, backup retention, and offline software dependencies.

## Design the SQL Server VMs

Use the following guidance to design your SQL Server VMs:

| Design area | SQL Server guidance |
| --- | --- |
| Compute | Size virtual CPUs for sustained utilization and licensing efficiency. Avoid oversubscription that creates unpredictable query latency. |
| Memory | Reserve memory for the operating system, agents, drivers, and failover activity. Configure `max server memory` so SQL Server doesn't consume memory needed by other processes. |
| Storage | Use separate volumes for the operating system, data, transaction logs, `tempdb`, and backups. Validate latency, throughput, queue depth, capacity, resiliency, and free-space thresholds. |
| Network | Provide stable client, management, backup, and replica paths as required. Validate DNS, maximum transmission unit (MTU), firewall rules, TLS trust, and listener connectivity. |
| Placement | Place high-availability replicas on different Azure Local nodes or failure domains. Use anti-affinity where available. |

## Install and configure SQL Server

1. Create supported Windows Server or Linux VMs by following the [disconnected operations VM guidance](/azure/azure-local/manage/disconnected-operations-arc-vm).
1. Transfer approved installation media, cumulative updates, drivers, management tools, and prerequisite packages through your controlled offline process.
1. Install SQL Server by using the instructions for [Windows](../../database-engine/install-windows/install-sql-server.md) or [Linux](../../linux/install-upgrade/setup.md). Install only the features you need. Use repeatable configuration files or automation, protect product keys and credentials, and document the build baseline.
1. Apply the approved cumulative update. Configure service accounts, authentication, TLS certificates, firewall rules, collation, `max server memory`, `tempdb`, database file growth, and backup compression.
1. Validate storage latency, CPU and memory pressure, connectivity, database integrity, backups, restore operations, and application behavior before production use.

## Configure high availability and disaster recovery

Design availability and recovery for the SQL Server workload separately from the Azure Local control plane.

### SQL Server availability options

- **Always On availability groups:** Use separate SQL Server VMs and place replicas across physical failure domains. For Windows deployments, configure Windows Server Failover Clustering, a local witness and quorum, listeners, and application retry behavior without depending on Azure Cloud Witness. See the [availability groups overview](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) and [getting started guidance](../../database-engine/availability-groups/windows/getting-started-with-always-on-availability-groups-sql-server.md).
- **Failover cluster instances on Windows:** Use supported shared storage and a supported clustered SQL Server configuration when you need instance-level protection. See [Always On failover cluster instances](../failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md) and [SQL Server failover cluster installation](../failover-clusters/install/sql-server-failover-cluster-installation.md).
- **Host resilience:** Use cluster-aware placement and anti-affinity so SQL Server replicas don't share the same physical node or failure domain.

For Windows clustering requirements, see [Windows Server Failover Clustering with SQL Server](../failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md).

For Linux, an availability group without a cluster manager provides read-scale replicas, not high availability. High-availability deployments require a supported cluster manager, such as Pacemaker, and production deployments require fencing. See [Configure availability groups on Linux](../../linux/business-continuity/availability-groups/configure.md). Confirm that your Linux distribution, cluster manager, fencing mechanism, and storage configuration are supported in your disconnected Azure Local environment before deploying. The Windows procedures linked here don't establish support for a Linux topology.

### Recovery design

Define recovery objectives for each database. Keep application-consistent SQL Server backups separate from control-plane backups. Replicate or export backups to an approved secondary security zone or site, protect encryption keys independently, and run scheduled restore tests.

For more information, see the [SQL Server restore and recovery overview](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md).

## Secure SQL Server

Apply a SQL Server security baseline that works without public-cloud services:

- On Windows, use least-privilege domain or managed service accounts where supported. Use the service identity configuration supported by your operating system, and restrict permissions for backup, monitoring, and application identities.
- Prefer integrated authentication where supported. On Linux, [Active Directory authentication](../../linux/security/authentication/active-directory-overview.md) uses Kerberos and requires SQL Server-specific service principal name (SPN) and keytab configuration. Platform identity integration doesn't configure SQL Server authentication. Restrict `sysadmin` membership, remove unused logins and features, disable weak protocols, and review permissions regularly.
- Require TLS for client connections. For availability-group replica traffic, configure authentication and encryption on the [database mirroring endpoints](../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md). Manage certificates through your local public key infrastructure (PKI), and monitor expiration and trust-chain health.
- Protect data with transparent data encryption (TDE) or volume encryption as appropriate. Encrypt backups, store keys separately, and test key recovery.
- Enable SQL Server Audit or equivalent controls for privileged activity, login failures, permission changes, sensitive-object access, and configuration changes.

### Disconnected security considerations

- Maintain an approved offline repository for SQL Server updates, drivers, tools, security definitions, and dependencies. Verify signatures and scan imported artifacts.
- For external REST calls, allow only approved local HTTPS endpoints. Use database-scoped credentials, restrict `EXECUTE ANY EXTERNAL ENDPOINT`, and audit requests. Review the permissions and endpoint requirements for [sp_invoke_external_rest_endpoint](../../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md).
- Document emergency access, credential rotation, certificate renewal, support-bundle sanitization, and recovery procedures that don't depend on public-cloud services.

## Monitor SQL Server

You can monitor Azure Local infrastructure, control-plane services, and VM health through configured external monitoring solutions. Install and configure the required agents and management packs for your monitoring solution. Disconnected operations doesn't provide a dedicated SQL Server monitoring service.

Monitor SQL Server inside each VM with native capabilities, such as dynamic management views, Extended Events, and error logs, or an approved on-premises monitoring platform. On Windows, you can also use SQL Server Agent alerts and Windows performance counters. SQL Server Agent alerts aren't supported on Linux; review the [SQL Server on Linux feature support](../../linux/sql-server-linux-editions-and-components-2025.md) for your version.

Collect the following signals, as appropriate for your workload:

- SQL Server availability, database status, backup age, and job failures.
- Blocking, deadlocks, waits, CPU utilization, and memory pressure.
- Storage latency, database and log growth, and `tempdb` health.
- High-availability replica synchronization.

Retain telemetry in approved local repositories. Define actionable thresholds and escalation procedures. For platform monitoring, see [Monitor disconnected operations](/azure/azure-local/manage/disconnected-operations-monitoring).

## Back up and recover SQL Server

> [!IMPORTANT]
> The disconnected operations backup capability protects control-plane VM data only. It doesn't back up SQL Server workloads or configured workload clusters. Configure SQL Server backups separately.

1. Select [recovery models](../../relational-databases/backup-restore/recovery-models-sql-server.md) based on your RPOs. Schedule full backups and, as needed, differential backups to protected local storage. For databases that use the full or bulk-logged recovery model, also schedule transaction-log backups. The simple recovery model doesn't support transaction-log backups or point-in-time recovery. Use compression, checksums, and encryption where appropriate.
1. Keep copies in a separate failure domain or site. Protect backup and encryption keys independently, and monitor capacity and backup age.
1. Test complete and alternate-host restores. When point-in-time recovery is required, use the full recovery model, maintain the log-backup chain, and test point-in-time restores. Under bulk-logged recovery, you can't restore to a point within a log backup that contains bulk-logged changes. Record achieved RPOs and RTOs, and address gaps.

For procedures, see [Back up and restore SQL Server databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md), [Create a full database backup](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md), and the [restore and recovery overview](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md).

## Service SQL Server offline

Maintain approved baselines for SQL Server cumulative updates, guest operating system patches, drivers, management tools, and security software: 

-  Test updates in a representative disconnected environment.
-  Back up the workloads before applying updates.
-  Patch high-availability replicas in a controlled sequence.
-  Validate failover, application connectivity, and performance after the update.
-  Microsoft publishes the applicable SQL Server cumulative update (CU), general distribution release (GDR), or security update through the standard [SQL Server servicing update channels](../../database-engine/install-windows/install-sql-server-servicing-updates.md) and the [SQL Server latest updates and version history](/troubleshoot/sql/releases/download-and-install-latest-updates).

Deliver SQL Server and guest updates through an approved mechanism for the guest operating system:

- **Windows:** Use an approved enterprise software-distribution tool, such as [Windows Server Update Services (WSUS)](/windows/deployment/update/waas-manage-updates-wsus) or Microsoft Configuration Manager, for the updates it supports.
- **Linux:** Use the distribution's package manager with an approved local repository or transferred packages and their dependencies. Follow the [offline installation and update guidance](../../linux/install-upgrade/setup.md).

Follow the separate [disconnected operations update guidance](/azure/azure-local/manage/disconnected-operations-update) for platform updates.

## Build local AI applications

SQL Server 2025 can connect to approved local chat-completion endpoints, such as compatible Foundry Local deployments. Applications can combine SQL data with on-premises model inference without requiring public-cloud connectivity.

Foundry Local on Azure Local is in preview. Complete its deployment and access prerequisites before integrating a model with SQL Server. Before calling a local model endpoint:

- Enable the `external rest endpoint enabled` server configuration option. The procedure is disabled by default in SQL Server 2025.
- Grant the calling database principal `EXECUTE ANY EXTERNAL ENDPOINT` permission, and configure the authentication and credentials required by the endpoint.
- Make the deployed model's HTTPS endpoint reachable from the SQL Server VM. Configure TLS certificates and certificate trust for production use. Don't rely on inference tests that bypass certificate validation.

Review the supported endpoints and security requirements before using [sp_invoke_external_rest_endpoint](../../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md). For model deployment and inference, see [Foundry Local on Azure Local](/azure/azure-sovereign-clouds/private/foundry-local/), [run inference](/azure/azure-sovereign-clouds/private/foundry-local/how-to-run-inference), and [bring your own models](/azure/azure-sovereign-clouds/private/foundry-local/concept-bring-your-own-models).

Local model inference doesn't establish that developer tools can operate without cloud connectivity. Validate each tool's authentication, client, and connectivity requirements separately before including it in a disconnected workflow.

## Licensing and support boundaries

Choose the SQL Server licensing model before finalizing VM sizing and availability topology. Evaluate edition requirements, virtual-core licensing, eligible physical-core licensing with unlimited virtualization, Software Assurance or subscription rights, and non-production entitlements. Azure Local disconnected operations platform capacity is licensed separately.

- **Edition:** Validate Standard and Enterprise feature requirements, including availability, scale, online operations, and virtualization rights.
- **License scope:** Compare per-VM virtual-core licensing with eligible physical-core models, particularly when consolidating SQL Server VMs.
- **Licensing model:** Validate SQL Server Software Assurance or subscription requirements and Azure Hybrid Benefit eligibility for disconnected operations.
- **Records:** Maintain inventories of hosts, VMs, assigned cores, editions, environments, and license evidence.

Review [disconnected operations billing](/azure/azure-local/manage/disconnected-operations-billing) for platform terms. The [SQL Server enabled by Azure Arc licensing documentation](../azure-arc/manage-license-billing.md) describes the connected management experience; it isn't a procedure for installing the unsupported SQL Server extension in disconnected operations.

## Related content

- [SQL Server on Azure Local overview](overview.md)
- [Deploy SQL Server on Azure Local in connected mode](deploy.md)
- [Azure Local disconnected operations overview](/azure/azure-local/manage/disconnected-operations-overview)

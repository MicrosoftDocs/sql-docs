---
title: Troubleshoot Stuck Management Operations
titleSuffix: Azure SQL Managed Instance
description: Learn how to diagnose and unblock Azure SQL Managed Instance management operations that are stuck in provisioning, starting, or updating states, including states caused by platform-side issues after an incident or failover.
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma, randolphwest
ms.date: 03/03/2026
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: troubleshooting
---

# Troubleshoot Azure SQL Managed Instance management operations stuck in provisioning/starting/updating states

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article helps you diagnose and unblock [management operations](management-operations-overview.md) on [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) that appear stuck in states such as **Provisioning**, **Starting**, **Updating**, **WaitingForManagedServerActivation**, **WaitingForOperationToComplete**, **NoComputeAvailable**, **CreatingPhysicalDatabase**, or **ReadyForCreationKickOff**.

These states are most commonly observed during create, start, scale, service-tier change, or post-failover operations. While most operations complete within their [expected duration](management-operations-duration.md), certain operations can take longer than expected because of customer-side configuration, capacity availability, or transient platform/control-plane conditions following an Azure incident or failover.

> [!NOTE]
> Customers can't restart internal control-plane agents or backend orchestrator components. When a stuck operation is caused by a platform-side condition, the resolution requires the platform to recover, or Microsoft Support to unblock the workflow.

## Symptoms

You might experience one or more of the following symptoms:

- A create, start, or scale operation has been running significantly longer than the [expected duration](management-operations-duration.md) for that operation.
- The portal **Overview** page for the managed instance shows a state such as **Provisioning**, **Starting**, **Updating**, or a long-lived "operation in progress" banner.
- An ARM `GET` on the managed instance, or on its operation, returns a state such as `WaitingForManagedServerActivation`, `WaitingForOperationToComplete`, `CreatingPhysicalDatabase`, `ReadyForCreationKickOff`, or `NoComputeAvailable` for an extended period.
- The instance was healthy before a region incident, planned maintenance, or [user-initiated failover](user-initiated-failover.md), and a subsequent management operation hasn't progressed.
- Repeated retries of the same operation produce no observable progress.

## Quick triage: platform vs configuration

Before opening a support request, run through the following checklist to determine whether the cause is likely customer configuration or a platform-side issue.

| Check | Action | Outcome |
| --- | --- | --- |
| Azure Service Health | Open **Service Health** in the Azure portal and look for active or recent incidents impacting **Azure SQL Managed Instance** in your region. | If an incident is active, follow the incident communication. Most operations resume automatically after platform recovery. |
| Subnet and networking | Verify the subnet still meets [networking requirements](connectivity-architecture-overview.md), including [service-aided subnet configuration](subnet-service-aided-configuration-enable.md), required NSG rules, route tables, and DNS. | Misconfigured networking can block create, start, and scale operations. |
| Conflicting operations | Confirm there is no other long-running operation in the same subnet/virtual cluster (for example, a long restore or a recently submitted scale). See [Management operations cross-impact](management-operations-overview.md#management-operations-cross-impact). | If a conflicting operation is in progress, the queued operation resumes automatically when the prior one completes. |
| Quota and capacity | Confirm subscription vCore quota and regional capacity are sufficient for the requested SKU. | Insufficient capacity can produce `NoComputeAvailable` states. See [Resource limits](resource-limits.md). |
| Operation age | Compare elapsed time with the [expected duration](management-operations-duration.md) for the operation type and instance size. | Operations within expected ranges aren't stuck - wait. |
| Activity log | In the Azure portal, open the resource group **Activity log** and locate the operation entry. Capture the **operation ID** and **correlation ID**. | These IDs are required for support escalation. |

If the operation has run beyond the expected duration, no incident is reported, and your configuration appears valid, the issue is likely platform-side and may require Microsoft Support.

## State-to-action matrix

The following table maps observable operation states to typical meanings, what you can check, and the recommended next step. State names match those reported by the Azure portal and ARM operation status.

| State | Typical meaning | What you can check | Recommended next step | Escalation threshold |
| --- | --- | --- | --- | --- |
| **Provisioning** | Standard creation step. The virtual cluster, VM group, and SQL Database Engine processes are being prepared. | Subnet configuration, vCore quota, region capacity. | Wait. New virtual cluster builds can take several hours. | More than 6 hours without progress, or significantly beyond the [expected create duration](management-operations-duration.md). |
| **ReadyForCreationKickOff** | The control plane has accepted the request and is queuing it for execution. | Recent operations in the same subnet (cross-impact), capacity. | Wait. The operation starts after queued work clears. | More than 1 hour stuck in this state. |
| **WaitingForManagedServerActivation** | The instance VM group is built; the SQL Database Engine process is being activated. | Service Health for active incidents, recent failover/maintenance. | Wait. Activation usually completes within minutes after VM group readiness. | More than 1 hour stuck in this state without an active incident. |
| **CreatingPhysicalDatabase** | System databases are being created on the new SQL Database Engine process. | Subnet/storage configuration. | Wait. | More than 2 hours without progress. |
| **WaitingForOperationToComplete** | A dependent backend step (seeding, replication, or failover) is still in progress. | Database size and instance workload (seeding throughput is sensitive to both). | Wait. Avoid initiating additional operations on the same instance/subnet. | Significantly beyond expected [seeding duration](management-operations-overview.md#seeding-speeds) for the database size. |
| **NoComputeAvailable** | The platform couldn't allocate the requested compute. | Region/SKU capacity, quota, planned maintenance. | Retry later. Consider an alternate region or SKU if the condition persists. See [Stop and start - capacity allocation](instance-stop-start-how-to.md#overview). | Multiple consecutive failures across several hours. |
| **Starting** | The instance is starting from a stopped state, or restarting after maintenance/failover. | Service Health, Activity log for the start operation. | Wait. Starts typically take about 20 minutes; longer if a virtual cluster build is required. | More than 4 hours stuck without progress. |
| **Updating** | A scale, service-tier change, hardware change, maintenance window, or post-failover reconfiguration is in progress. | Operation type and [expected duration](management-operations-duration.md), other conflicting operations. | Wait. Don't submit additional updates while one is in progress. | Significantly beyond expected duration with no Activity log progress. |

## Expected timeframes

Most management operations complete within the ranges documented in [Management operations duration](management-operations-duration.md). An operation should be considered abnormal when it:

- Exceeds the documented expected duration by a significant margin (for example, more than 2x).
- Hasn't produced any new Activity log entries for several hours.
- Remains in a single state with no progress while no Service Health incident is reported.

A new virtual cluster build can extend create and start operations to approximately 4 hours. Large databases and Business Critical service tier operations require seeding, which scales with database size and instance workload.

## Safe customer actions

When an operation is stuck, take only safe actions to avoid making the situation worse.

**Recommended:**

- Wait for the documented expected duration before taking any action.
- Check **Azure Service Health** for active incidents and follow the published guidance.
- Capture the **operation ID** and **correlation ID** from the **Activity log** for the failing operation. These IDs are required for any support engagement.
- Verify the subnet, NSG, route tables, and DNS configuration are still compliant with [networking requirements](connectivity-architecture-overview.md).

**Avoid:**

- Don't repeatedly toggle stop/start, retry scale, or initiate additional operations on the same instance while one is already in progress. These actions can extend recovery time and complicate diagnosis.
- Don't change the service tier, hardware configuration, or maintenance window while a prior operation is unfinished.
- Don't delete the instance to "reset" it unless you have validated backups and accept the data loss; deletion isn't a supported troubleshooting workaround.

**Cancellation:**

- Some management operations can be cancelled. See [Cancel management operations](management-operations-cancel.md) for the operations that support cancellation and the expected behavior.
- Stop and start operations can't be cancelled after they're initiated. See [Stop and start limitations](instance-stop-start-how-to.md#limitations-of-the-stop-and-start-feature).

## Monitor operation progress

Use the following channels to monitor operation progress:

- **Azure portal**: The instance **Overview** page shows the current state and the in-progress operation banner.
- **Activity log**: The resource group **Activity log** lists every management operation, with status, operation ID, correlation ID, and timestamps.
- **ARM operation status**: Query the long-running operation status using the ARM REST API. See [Monitor management operations](management-operations-monitor.md) for details on retrieving operation state programmatically.
- **Azure Service Health**: Subscribe to alerts for SQL Managed Instance in your region(s) so you're notified of incidents that may impact your instance.

## Data to collect for Microsoft Support

Before opening a support request, collect the following information. Providing it up front significantly reduces time to mitigation:

- **Subscription ID**, **resource group**, **managed instance name**, and **region**.
- **Operation ID** and **correlation ID** from the Activity log entry for the stuck operation.
- **Operation type** (create, start, stop, scale vCores, scale storage, change service tier, change hardware, change maintenance window, failover).
- **Timestamps**: when the operation was submitted and when it last showed progress.
- **Current observable state** as displayed in the portal and as returned by ARM.
- **Instance configuration**: service tier, hardware, vCores, storage size, zone redundancy, maintenance window, subnet, virtual network, and any recent configuration changes.
- **Recent events**: any failover, maintenance, scale, or restore performed in the last 24-48 hours; any Service Health incident that overlaps with the operation.
- **Screenshots** of the portal **Overview** and **Activity log** pages.

## When to open a support ticket

Open a support ticket when:

- The operation has exceeded the [expected duration](management-operations-duration.md) by a significant margin.
- No Service Health incident covers the symptom, or the published incident no longer mentions impact in your region.
- The operation has remained in the same state for longer than the escalation threshold in the [state-to-action matrix](#state-to-action-matrix), without progress in the Activity log.
- Production workloads are impacted and you need a defined response time.

Choose the support severity that matches your business impact. Production-down scenarios should use the severity supported by your [support plan](https://azure.microsoft.com/support/plans/).

## What Microsoft Support may do

To set expectations, Microsoft Support engineers can do the following at a high level (specific internal mechanics aren't published):

- Inspect the state of the backend workflow associated with your operation ID and confirm whether it's actively progressing, queued behind other work, or blocked.
- Unblock workflows that became stuck because of transient control-plane conditions following an incident or failover.
- Repair dependencies on shared platform components (for example, capacity, storage, or networking pipelines) that affect a specific operation.
- Coordinate with engineering when an issue requires code-level investigation.

Customers can't perform any of these actions directly because they require access to internal control-plane components.

## Post-incident guidance

After a regional incident, planned maintenance, or failover, some operations may be queued or briefly degraded while the platform recovers:

- Allow the platform to stabilize before submitting non-essential management operations.
- Avoid bulk operations across many instances in the same subnet/virtual cluster while recovery is in progress; they can extend overall recovery time. See [Management operations cross-impact](management-operations-overview.md#management-operations-cross-impact).
- For instances that returned to the **Ready** state, a brief delay before scaling, service-tier changes, or maintenance window changes reduces the chance of conflicts.
- Continue to monitor **Service Health** and the **Activity log** for the all-clear.

## Frequently asked questions

### Why does my operation say "Updating" hours after I started it?

Long-running update operations (scale vCores or storage, service-tier change, hardware change, maintenance window change) can require building a new VM group and seeding data. Business Critical service tier operations require seeding for almost every change. See [Seeding](management-operations-overview.md#seeding) and [Management operations duration](management-operations-duration.md).

### Can I cancel a stuck stop or start operation?

No. Stop and start operations can't be cancelled. See [Limitations of the stop and start feature](instance-stop-start-how-to.md#limitations-of-the-stop-and-start-feature).

### Will retrying the operation help?

Usually no. Retrying while an operation is in progress doesn't accelerate recovery and can extend it. Retry only after the prior operation has completed or failed.

### Does failing over the instance unblock a stuck operation?

No. [User-initiated failover](user-initiated-failover.md) is a separate operation and is itself a management operation that can be queued behind the in-progress one.

### My instance is in `NoComputeAvailable`. What can I do?

Wait and retry later. If the condition persists across multiple attempts and several hours, consider an alternate region or SKU, and open a support ticket if the impact is production-critical.

## Related content

- [Overview of management operations in Azure SQL Managed Instance](management-operations-overview.md)
- [Management operations duration](management-operations-duration.md)
- [Monitoring Azure SQL Managed Instance management operations](management-operations-monitor.md)
- [Canceling Azure SQL Managed Instance management operations](management-operations-cancel.md)
- [Stop and start an instance](instance-stop-start-how-to.md)
- [Known issues with Azure SQL Managed Instance](doc-changes-updates-known-issues.md)
- [Resource health for Azure SQL Managed Instance](resource-health-to-troubleshoot-connectivity.md)
- [Azure Service Health](/azure/service-health/overview)

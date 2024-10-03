---
title: Restart an instance with a manual failover
description: Learn how to restart an instance by manually failing over primary and secondary replicas of Azure SQL Managed Instance by using PowerShell, the Azure CLI or REST API. 
author: danimir
ms.author: danil
ms.reviewer: mathoma
ms.date: 09/27/2024
ms.service: azure-sql-managed-instance
ms.subservice: high-availability
ms.topic: how-to
ms.custom:
  - sqldbrb=1
  - devx-track-azurepowershell
---

# Restart an instance with a user-initiated manual failover - Azure SQL Managed Instance 
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article explains how to restart [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) by performing a manual user-initiated failover to a secondary compute node by using PowerShell, the Azure CLI or REST API. 

It's possible to fail over a primary node in the General Purpose (GP) and Business Critical (BC) service tiers, and to manually fail over a secondary read-only replica node in the BC service tier.

> [!NOTE]
> Failover in this article refers starting the SQL Server database engine process on a secondary node, and is not related to the cross-region failover of a [failover groups](failover-group-sql-mi.md).


## When to use manual failover

[Availability](high-availability-sla-local-zone-redundancy.md), a fundamental part of the SQL Managed Instance platform, works transparently for your database applications by providing local standby compute nodes to host the SQL Server database engine process. A failover occurs when the SQL Server database engine process is taken offline on the primary compute node and is brought online on a secondary compute node. Typically, failovers between SQL managed instance compute nodes are automatic and managed by the platform when a fault is detected, a node has degraded, or during regular monthly software updates. 

The entire failover operation consists of bringing the SQL Server database engine process online on a secondary node, validating the database state, and then finally redirecting the client connections to the new primary node. Client connections only fail for a short period of time, typically under a minute, during the final step of the failover operation.

You might execute a manual failover to restart the engine process on a different node for the following reasons:

- Failed logins, or slowness due to performance issues.
- Testing application for failover resiliency before deploying to production.
- Testing end-to-end systems for fault resiliency on automatic failovers.
- Testing how failover impacts existing database sessions.
- Query performance degradation (restarting the instance can help mitigate the performance issue).

Ensuring that your applications are failover resilient prior to deploying to production helps mitigate the risk of application faults in production and contributes to application availability for your customers. Learn more about testing your applications for cloud readiness with the following video: 
> [!VIDEO https://learn.microsoft.com/shows/data-exposed/testing-cloud-readiness-applications-with-failover-resiliency-for-sql-managed-instance/player]


The following table describes the expected behavior of the SQL Managed Instance during a failover operation based on the service tier and [availability model](high-availability-sla-local-zone-redundancy.md#overview): 

| Service tier | Availability model | Expected failover behavior | Potential failover behavior (exceptions) | 
| --- | --- | --- | --- |
| General Purpose | [Local redundancy](high-availability-sla-local-zone-redundancy.md#general-purpose-service-tier)  <br /> (Single availability zone) | SQL process restarts on the same VM. | SQL process restarts on a different VM. | 
| General Purpose | [Zone redundancy (preview)](high-availability-sla-local-zone-redundancy.md#general-purpose-service-tier-1) <br /> (Multiple availability zones) | SQL process restarts on the same VM. | SQL process restarts on a different VM. | 
| Business Critical | [Local redundancy](high-availability-sla-local-zone-redundancy.md#business-critical-service-tier) <br /> (Single availability zone) | Random secondary replica is promoted to primary. | N/A | 
| Business Critical | [Zone redundancy](high-availability-sla-local-zone-redundancy.md#business-critical-service-tier-1) <br /> (Multiple availability zones) | Random secondary replica is promoted to primary, either in the same or different availability zone. | N/A | 

## Permissions

Users initiating a failover need to have one of the following Azure roles:

- Subscription Owner role, or
- [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) role, or
- Custom role with the following permission:
  - `Microsoft.Sql/managedInstances/failover/action`


## Restart an instance with a manual fail over

You can restart an instance with a manual failover by using PowerShell, the Azure CLI, or REST API.

### [PowerShell](#tab/powershell)

The minimum version of Az.Sql needs to be [v2.9.0](https://www.powershellgallery.com/packages/Az.Sql/2.9.0). Consider using [Azure Cloud Shell](/azure/cloud-shell/overview) from the Azure portal that always has the latest PowerShell version available. 

As a pre-requirement, use the following PowerShell script to install required Azure modules. In addition, select the subscription where SQL Managed Instance you wish to failover is located.

```powershell
$subscription = 'enter your subscription ID here'
Install-Module -Name Az
Import-Module Az.Accounts
Import-Module Az.Sql

Connect-AzAccount
Select-AzSubscription -SubscriptionId $subscription
```

Use PowerShell command [Invoke-AzSqlInstanceFailover](/powershell/module/az.sql/invoke-azsqlinstancefailover) with the following example to initiate failover of the primary node, applicable to both BC and GP service tier.

```powershell
$ResourceGroup = 'enter resource group of your MI'
$ManagedInstanceName = 'enter MI name'
Invoke-AzSqlInstanceFailover -ResourceGroupName $ResourceGroup -Name $ManagedInstanceName
```

Use the following PowerShell command to failover read secondary node, applicable to BC service tier only.

```powershell
$ResourceGroup = 'enter resource group of your MI'
$ManagedInstanceName = 'enter MI name'
Invoke-AzSqlInstanceFailover -ResourceGroupName $ResourceGroup -Name $ManagedInstanceName -ReadableSecondary
```

### [Azure CLI](#tab/azure-cli)


Ensure to have the latest CLI scripts installed.

Use az sql mi failover CLI command with the following example to initiate failover of the primary node, applicable to both BC and GP service tier.

```cli
az sql mi failover -g myresourcegroup -n myinstancename
```

Use the following CLI command to failover read secondary node, applicable to BC service tier only.

```cli
az sql mi failover -g myresourcegroup -n myinstancename --replica-type ReadableSecondary
```

### [REST API](#tab/restapi)

For advanced users who would perhaps need to automate failovers of their SQL Managed Instances for purposes of implementing continuous testing pipeline, or automated performance mitigators, this function can be accomplished through initiating failover through an API call. See [SQL Managed Instances - Failover REST API](/rest/api/sql/managed-instances/failover) for details.

To initiate failover using REST API call, first generate the Auth Token using API client of your choice. The generated authentication token is used as Authorization property in the header of API request and it's mandatory.

The following code is an example of the API URI to call:

```HTTP
POST https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/managedInstances/{managedInstanceName}/failover?api-version=2019-06-01-preview
```

The following properties need to be passed in the API call:

| **API property** | **Parameter** |
| --- | --- |
| subscriptionId | Subscription ID to which managed instance is deployed |
| resourceGroupName | Resource group that contains managed instance |
| managedInstanceName | Name of managed instance |
| replicaType | (Optional) (Primary or ReadableSecondary). These parameters represent the type of replica to be failed over: primary or readable secondary. If not specified, failover is initiated on the primary replica by default. |
| api-version | Static value and currently needs to be "2019-06-01-preview" |

API responds with one of the following two:

- 202 Accepted
- One of the 400 request errors.

Operation status can be tracked through reviewing API responses in response headers. For more information, see [Status of asynchronous Azure operations](/azure/azure-resource-manager/management/async-operations).

---

## Monitor the failover

Monitoring the progress of a user-initiated failover differs for instances in the Business Critical and General Purpose service tiers. 

To monitor progress of a user-initiated failover, use: 
- [sys.dm_os_sys_info](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql)  to check system uptime for the *General Purpose* service tier. 
- `sys.dm_hadr_fabric_replica_states` to check available replicas for the *Business Critical* service tier.

The final step of the failover process is the redirection of client connections to the new primary node. The short loss of connectivity from your client during the failover, typically lasting under a minute, indicates failover has succeeded - regardless of service tier. 

### General Purpose service tier

The following T-SQL example shows the uptime for the SQL process on the node for the *General Purpose* service tier:

```sql
SELECT sqlserver_start_time, sqlserver_start_time_ms_ticks FROM sys.dm_os_sys_info
```
The SQL process start time is the time when the SQL Server database engine process was started on the node. The time restarts after failover completes. Run this query before and after you initiate a failover of an instance in the General Purpose service tier to monitor progress of the failover operation.

### Business Critical service tier

The following T-SQL example indicates which node is currently the primary replica for the *Business Critical* service tier:

```sql
SELECT DISTINCT replication_endpoint_url, fabric_replica_role_desc FROM sys.dm_hadr_fabric_replica_states
```

The node that hosts the primary replica changes after failover completes. Run this query before and after you initiate a failover of an instance in the Business Critical service tier to monitor progress of the failover operation.


> [!NOTE]
> The full failover process might take several minutes to complete during **high-intensity** workloads because the instance engine ensures that transactions on the secondary node are caught up to the transactions from the primary node before initiating the failover. 



## Limitations 

Consider the following functional limitations of user-initiated manual failovers:

- There can only be one (1) failover initiated on the same SQL Managed Instance every **15 minutes**.
- Failovers aren't allowed: 
   - Until the first full backup for a new database is completed by automated backup systems.
   - if there's database restore in progress.
- For instances in the Business Critical service tier: 
   - There must exist quorum of replicas for the failover request to be accepted.
   - It is not possible to specify which readable secondary replica to initiate the failover on.


## Related content

- Learn more about testing your applications for cloud readiness with [Testing App Cloud Readiness for Failover Resiliency with SQL Managed Instance](https://youtu.be/FACWYLgYDL8) video recording.
- Learn more about high availability of managed instance [High availability for Azure SQL Managed Instance](high-availability-sla-local-zone-redundancy.md).
- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md).

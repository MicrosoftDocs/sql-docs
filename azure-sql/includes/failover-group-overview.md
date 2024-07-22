---
title: failover group overview
description: Deduplicating content between SQL Database and SQL Managed Instance, in this case using an include file for an overview of the failover group feature.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: rsetlem, mlandzic, wiassaf, strrodic
ms.date: 12/15/2023
ms.topic: include
---

### Endpoint redirection

Failover groups provide read-write and read-only listener end-points that remain unchanged during geo-failovers. You don't have to change the connection string for your application after a geo-failover, because connections are automatically routed to the current primary. A geo-failover switches all secondary databases in the group to the primary role. After geo-failover completes, the DNS record is automatically updated to redirect the endpoints to the new region. 

### Offload read-only workloads

To reduce traffic to your primary databases, you can also use the secondary databases in a failover group to offload read-only workloads. Use the read-only listener to direct read-only traffic to a readable secondary database. 

### Recovering an application

To achieve full business continuity, adding regional database redundancy is only part of the solution. Recovering an application (service) end-to-end after a catastrophic failure requires recovery of all components that constitute the service and any dependent services. Examples of these components include the client software (for example, a browser with a custom JavaScript), web front ends, storage, and DNS. It's critical that all components are resilient to the same failures and become available within the recovery time objective (RTO) of your application. Therefore, you need to identify all dependent services and understand the guarantees and capabilities they provide. Then, you must take adequate steps to ensure that your service functions during the failover of the services on which it depends. 

## Failover policy

Failover groups support two failover policies:

- **Customer managed (recommended)** - Customers can perform a failover of a group when they notice an unexpected outage impacting one or more databases in the failover group. When using command line tools such as PowerShell, the Azure CLI, or the Rest API, the failover policy value for customer managed is `manual`. 
- **Microsoft managed** - In the event of a widespread outage that impacts a primary region, Microsoft initiates failover of all impacted failover groups that have their failover policy configured to be Microsoft-managed. Microsoft managed failover won't be initiated for individual failover groups or a subset of failover groups in a region. When using command line tools such as PowerShell, the Azure CLI, or the Rest API, the failover policy value for Microsoft-managed is `automatic`. 

Each failover policy has a unique set of use cases and corresponding expectations on the failover scope and data loss, as the following table summarizes: 

| Failover policy | Failover scope | Use case | Potential data loss |
| --- | --- | --- | --- |
| Customer managed <br> **(Recommended)** | Failover group(s) | One or more databases in a failover group(s) is impacted by an outage and become unavailable. You can choose to fail over. | Yes |
| Microsoft managed | All failover groups in the region | A widespread outage in a datacenter, availability zone, or region causes unavailability of databases and the Microsoft Azure SQL service team decides to trigger a forced failover. <br> Use this option only when you want to delegate the disaster recovery responsibility to Microsoft and the application is tolerant to RTO (downtime) of at least one hour or more.| Yes |

### Customer managed

On rare occasions, the built-in [availability or high availability](../database/high-availability-sla-local-zone-redundancy.md) isn't enough to mitigate an outage, and your databases in a failover group might be unavailable for a duration that isn't acceptable to the service level agreement (SLA) of the applications using the databases. Databases can be unavailable due to a localized issue impacting just a few databases, or it could be at the datacenter, availability zone, or region level. In any of these cases, to restore business continuity, you can initiate a forced failover.

_Setting your failover policy to customer managed is highly recommended_, as it keeps you in control of when to initiate a failover and restore business continuity. You can initiate a failover when you notice an unexpected outage impacting one or more databases in the failover group.

### Microsoft managed

With a Microsoft managed failover policy, disaster recovery responsibility is delegated to the Azure SQL service. For the Azure SQL service to initiate a forced failover, the following conditions must be met:

- Datacenter, availability zone, or region level outage caused by a natural disaster event, configuration changes, software bugs or hardware component failures and many databases in the region are impacted.  
- Grace period is expired. Because verifying the scale of, and mitigating, the outage depends on human actions, the grace period can't be set below one hour.
  
When these conditions are met, the Azure SQL service initiates forced failovers for all failover groups in the region that have the failover policy set to Microsoft managed. 

Set the failover policy to Microsoft managed only when:

- You want to delegate disaster recovery responsibility to the Azure SQL service. 
- The application is tolerant to your database being unavailable for at least one hour or more.
- It's acceptable to trigger forced failovers some time after the grace period expires as the actual time for the forced failover can vary significantly. 
- It's acceptable that all databases within the failover group fail over, regardless of their zone redundancy configuration or availability status. Although databases configured for zone redundancy are resilient to zonal failures and might not be impacted by an outage, they'll still be failed over if they're part of a failover group with a Microsoft managed failover policy.  
- It's acceptable to have forced failovers of databases in the failover group without taking into consideration the application's dependency on other Azure services or components used by the application, which can cause performance degradation or unavailability of the application.
- It's acceptable to incur an unknown amount of data loss, as the exact time of forced failover can't be controlled, and ignores the synchronization status of the secondary databases.
- All the primary and secondary database(s) in the failover group and any geo replication relationships have the same service tier, compute tier (provisioned or serverless) & compute size (DTUs or vCores). If the service level objective (SLO) of all the databases don't match, then the failover policy will be eventually updated from Microsoft Managed to Customer Managed by Azure SQL service. 

When a failover is triggered by Microsoft, an entry for the operation name **Failover Azure SQL failover group** is added to the [Azure Monitor activity log](/azure/azure-monitor/essentials/activity-log). The entry includes the name of the failover group under **Resource**, and **Event initiated by** displays a single hyphen (-) to indicate the failover was initiated by Microsoft.  This information can also be found on the **Activity log** page of the new primary server or instance in the Azure portal. 

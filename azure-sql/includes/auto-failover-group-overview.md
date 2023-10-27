---
title: Auto-failover group overview
description: Deduplicating content between SQL Database and SQL Managed Instance, in this case using an include file for an overview of the auto-failover group feature.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: emlisa, mlandzic, wiassaf
ms.date: 05/06/2023
ms.topic: include
---

### Endpoint redirection

Failover groups provide read-write and read-only listener end-points that remain unchanged during geo-failovers. You do not have to change the connection string for your application after a geo-failover, because connections are automatically routed to the current primary. A geo-failover switches all secondary databases in the group to the primary role. After the geo-failover is completed, the DNS record is automatically updated to redirect the endpoints to the new region. For geo-failover RPO and RTO, see [Overview of Business Continuity](../database/business-continuity-high-availability-disaster-recover-hadr-overview.md#recovery-time-objective-rto-and-recovery-point-objective-rpo).

### Offload read-only workloads

To reduce traffic to your primary databases, you can also use the secondary databases in a failover group to offload read-only workloads. Use the read-only listener to direct read-only traffic to a readable secondary database. 

### Failover policy

Failover groups support two types of failover policy:

- **Customer managed (Recommended)** - Customers can perform failover of failover groups when they notice an unexpected outage impacting one or more databases in the failover group. Failover policy value for customer managed in programmatic tools like PowerShell, CLI and REST API is `manual`. 
- **Microsoft managed** - Initiated by Microsoft for all failover groups that have set Microsoft managed policy but only in case of widespread outage impacting a primary region. Microsoft managed failover can't be initiated for individual failover groups or a subset of failover groups in a region. Failover policy value for microsoft managed in programmatic tools like PowerShell, CLI and REST API is `automatic`. 

> [!NOTE]
> It is highly recommended to set failover policy to customer managed to keep the control and decision to failover with you. Your disaster recovery plan should be based on customer managed failover policy.
> 

Each type of failover policy has a unique set of use cases, corresponding expectations on failover scope and data loss as summarized in following table. 

| Failover policy | Failover scope | Use case | Potential data loss |
| --- | --- | --- | --- |
| Customer managed <br> **(Recommended)** | Failover group(s) | One or more database in a failover group(s) are impacted by an outage and become unavailable and you can decide to failover. | Yes |
| Microsoft managed | All failover groups in the region | A widespread outage in a datacenter or availability zone or region causing unavailability of databases and Microsoft Azure SQL service team determines to trigger forced failover. <br> Use this option only when you want to delegate disaster recovery responsibility to Microsoft and the application is tolerant to RTO (downtime) of atleast one hour or more.| Yes |


#### Customer managed (recommended)
It is highly recommended to set the failover policy to Customer managed so that you can keep the control and decision to trigger failover. On rare occasions subset or all of the databases in a failover group cannot be mitigated by built in [availability or high availability](../database/high-availability-sla.md) and may be unavailable for duration that is not acceptable to the service level agreement (SLA) of the application using the databases. Databases can be unavailable due to a localized issue impacting just few databases or it could be at datacenter or availability zone or region level. In either of these cases, to restore business continuity initiate forced failovers.

#### Microsoft managed
With Microsoft managed failover policy, disaster recovery responsibility is delegated to Azure SQL service. For Azure SQL service to initiate forced failovers, following conditions must be met:

- Datacenter or availability zone or region level outage caused by natural disaster event or configuration change or software bug or hardware component failure and impacting many databases in the region.  
- Grace period is expired. Because verification of scale of the outage and how quickly it can be mitigated involves human actions, the grace period cannot be set below one hour.
  
When above conditions are met, Azure SQL service initiates forced failovers for all failover groups in the region that have the failover policy set to Microsoft managed. 

Set the failover policy to Microsoft managed  only when:

- You want to delegate disaster recovery responsibility to Azure SQL service. 
- The application is tolerant to database being unavailable for atleast one hour or more.
- It is acceptable for forced failovers to get triggered not exactly at the set grace period. Since forced failovers will be initiated after grace period expires, the actual time for the forced failover will vary. 
- It is acceptable to have databases with zone redundancy enabled be failed over irrespective of their availability status. It is highly likely that zone redundancy configuration will ensure the database is resilient to zonal failures but with Microsoft managed failover policy all databases will be failed over. 
- It is acceptable to have forced failovers of databases in the failover group without taking into consideration the application's dependency on other Azure services or components used by the application.
- It is acceptable to incur unbound amount of data loss as the exact time of forced failover cannot be controlled.

### Recovering an application

To achieve full business continuity, adding regional database redundancy is only part of the solution. Recovering an application (service) end-to-end after a catastrophic failure requires recovery of all components that constitute the service and any dependent services. Examples of these components include the client software (for example, a browser with a custom JavaScript), web front ends, storage, and DNS. It is critical that all components are resilient to the same failures and become available within the recovery time objective (RTO) of your application. Therefore, you need to identify all dependent services and understand the guarantees and capabilities they provide. Then, you must take adequate steps to ensure that your service functions during the failover of the services on which it depends. 
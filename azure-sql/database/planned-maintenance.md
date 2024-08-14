---
title: Plan for Azure maintenance events
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn how to prepare for planned maintenance events in Azure SQL Database and Azure SQL Managed Instance.
author: aamalvea
ms.author: aamalvea
ms.reviewer: wiassaf, mathoma, urosmil
ms.date: 06/19/2024
ms.service: azure-sql
ms.subservice: service-overview
ms.topic: conceptual
ms.custom:
  - sqldbrb=1
  - build-2024
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---

# Plan for Azure maintenance events in Azure SQL Database and Azure SQL Managed Instance
[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Learn how to prepare for planned maintenance events on your database in Azure SQL Database and Azure SQL Managed Instance.

## What is a planned maintenance event?

To keep Azure SQL Database and Azure SQL Managed Instance services secure, compliant, stable, and performant, updates are being performed through the service components almost continuously. Thanks to the modern and robust service architecture and innovative technologies like [hotpatching](https://aka.ms/azuresqlhotpatching), majority of updates are fully transparent and nonimpactful in terms of availability. Still, few types of updates cause short service interrupts and require special treatment.

During planned maintenance, members of the database quorum go offline one at a time, with the intent that there is one responding primary replica. For Business Critical and Premium databases, at least one secondary replica will also be online to ensure no client downtime.

When the primary replica needs to be brought offline, a reconfiguration process occurs.

- For Business Critical and Premium databases, one of the secondary replicas will become the new primary replica. 
- For General Purpose, Standard, and Basic databases the primary replica will move to another stateless compute node with sufficient free capacity.

## What to expect during a planned maintenance event

Maintenance event can produce single or multiple reconfigurations, depending on the constellation of the primary and secondary replicas at the beginning of the maintenance event. On average, 1.7 reconfigurations occur per planned maintenance event. Reconfigurations generally finish within 30 seconds. The average is eight seconds. If already connected, your application must reconnect to the new primary replica of your database. 

If a new connection is attempted while the database is undergoing a reconfiguration before the new primary replica is online, you get error 40613 (Database Unavailable): `Database '{databasename}' on server '{servername}' is not currently available. Please retry the connection later.` If your database has a long-running query, this query will be interrupted during a reconfiguration and will need to be restarted.

## Maintenance windows and advance notifications

The maintenance window feature allows for the configuration of predictable maintenance window schedules for eligible Azure SQL databases and SQL managed instances. You can also configure advance notifications before maintenance windows. For more information, see:

- [Maintenance windows for Azure SQL Database](maintenance-window.md)
- [Configure advance notifications for maintenance windows for Azure SQL Database](advance-notifications.md)
- [Maintenance windows for Azure SQL Managed Instance](../managed-instance/maintenance-window.md)
- [Configure advance notifications for maintenance windows for Azure SQL Managed Instance](../managed-instance/advance-notifications.md)

## How to simulate a planned maintenance event

Ensure that your client application is resilient to maintenance events before deploying to production.

Testing will mitigate the risk of application faults and contributes to application availability for your end users. You can test behavior of your client application during planned maintenance events by [Testing Application Fault Resiliency](./high-availability-sla-local-zone-redundancy.md#testing-application-fault-resiliency) via PowerShell, CLI, or REST API.

For Azure SQL Managed Instance, also review [initiating manual failover](https://aka.ms/mifailover-techblog). A manual failover produces identical behavior as maintenance event bringing primary replica offline.

## Retry logic

Any client production application that connects to a cloud database service should implement a robust connection [retry logic](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors). Proper automatic retry logic helps make reconfigurations as transparent as possible to the end users.

### Service Health Alert

If you want to receive alerts for service issues or planned maintenance activities, you can use Service Health alerts in the Azure portal with appropriate event type and action groups. For more information, see this [Receive alerts on Azure service notifications](/azure/service-health/alerts-activity-log-service-notifications-portal#create-service-health-alert-using-azure-portal).

You can also [monitor and configure alerts the Azure SQL Database Availability metric](monitoring-metrics-alerts.md#availability-metric) in the Azure portal.

## Resource health

If your database is experiencing sign-in failures, check the [Resource Health](/azure/service-health/resource-health-overview#get-started) window in the [Azure portal](https://portal.azure.com) for the current status. The Health History section contains the downtime reason for each event (when available).

## Related content

- Learn more about [Resource Health for Azure SQL Database](resource-health-to-troubleshoot-connectivity.md?view=azuresql-db&preserve-view=true) and [Resource Health for Azure SQL Managed Instance](../managed-instance/resource-health-to-troubleshoot-connectivity.md?view=azuresql-mi&preserve-view=true).
- For more information about retry logic, see [Retry logic for transient errors](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors).
- Configure maintenance window schedules with the [Maintenance window](maintenance-window.md) feature.

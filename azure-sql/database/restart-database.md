---
title: "Restart a Database or Elastic Pool (Preview)"
titleSuffix: Azure SQL Database
description: Learn about restarting a database or elastic pool in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mahyon, randolphwest
ms.date: 02/25/2026
ms.service: azure-sql-database
ms.subservice: high-availability
ms.topic: how-to
ms.custom:
  - azure-sql-split
monikerRange: "=azuresql || =azuresql-db"
---
# Restart a database or elastic pool (preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides steps to restart an Azure SQL Database or elastic pool from the Azure portal.

> [!IMPORTANT]  
> The restart feature in the Azure portal is in preview and not recommended for production use.

The restart operation is designed to resolve transient issues that might affect database connectivity or performance. Restarting a database or elastic pool temporarily takes it offline, causing a brief interruption in service. However, it doesn't affect the data stored within the database. The restart operation utilizes the same APIs that can be used to [test your application fault resiliency](high-availability-sla-local-zone-redundancy.md#test-application-fault-resiliency). 

Only one failover call is allowed every 15 minutes for each database or elastic pool. 

The restart operation is not recommended for use when there are wide-spread service issues. Before initiating a restart, check [Azure Service Health](/azure/service-health/overview) for any ongoing issues.

## Restart a database

1. Go to the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub). Navigate to your database.

1. Under the **Settings** section, select **Maintenance**.

1. In the **Restart** section, select the **Click here to restart the SQL database** link.

1. A confirmation dialog appears. Type in your database name and select **Restart** to initiate the restart process.

## Restart an elastic pool

1. Go to the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub). Navigate to your elastic pool.

1. Under the **Settings** section, select **Maintenance**.

1. In the **Restart** section, select the **Click here to restart the SQL elastic pool** link.

1. A confirmation dialog appears. Type in your elastic pool name and select **Restart** to initiate the restart process.

> [!IMPORTANT]  
> Restarting an elastic pool restarts all databases within the pool.

## Related content

- [Test application fault resiliency](high-availability-sla-local-zone-redundancy.md#test-application-fault-resiliency)

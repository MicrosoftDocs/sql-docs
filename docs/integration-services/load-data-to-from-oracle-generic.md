---
title: "Load data from or into Oracle database"
description: Learn how to load data from or into Oracle database
ms.date: 01/06/2025
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
ms.custom: intro-deployment
---

# Load data from or into Oracle database

This document focuses on how to export data from and load data into Oracle data database using the SSIS ADO.NET source and ADO.NET Destination components. An ADO.NET connection manager allows a package to access data sources with a .NET provider, specifically the OracleClient Data Provider.

Here are the main steps.

## Install Oracle Client

First, download [the latest Oracle Client for Microsoft Tools](https://www.oracle.com/technetwork/database/windows/downloads/index-090165.html) and install Oracle client.

> [!NOTE]
> Installing both 32-bit and 64-bit versions together may cause compatibility issues. Install only one version.

## Configure ADO.NET connection manager

1. Create ADO.NET connection for Oracle connection
1. Choose .Net Providers\OracleClient Data Provider, then enter server name, username, and password.

:::image type="content" source="media/load-data-to-from-oracle-generic/connection-manager.png" alt-text="Screenshot of ado.net connection manager.":::

For details, see [ADO.NET connection manager](connection-manager/ado-net-connection-manager.md).

## Configure ADO.NET Source

Export data using the ADO NET source. For details, see [ADO NET Source](data-flow/ado-net-source.md).

:::image type="content" source="media/load-data-to-from-oracle-generic/ado-source.png" alt-text="Screenshot of ado.net source.":::

## Configure ADO.NET Destination

Use ADO NET destination loads data into ADO.NET-compliant databases. For details, see [ADO NET Destination](data-flow/ado-net-destination.md).

:::image type="content" source="media/load-data-to-from-oracle-generic/ado-destination.png" alt-text="Screenshot of ado.net destination.":::

> [!NOTE]
> ADO.NET allows packages to access data sources with the .NET OracleClient Data Provider. When migrating from Microsoft's connector for Oracle, custom properties in [Oracle source](data-flow/oracle-source.md) and [Oracle destination](data-flow/oracle-destination.md) components set by Advanced Editor only aren't available in ADO.NET source and destination.

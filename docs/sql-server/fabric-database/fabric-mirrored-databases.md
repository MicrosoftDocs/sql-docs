---
title: "Mirroring and Database Mirroring in Microsoft Fabric"
description: Learn about mirrored databases, from mirrored SQL Server database to mirrored Azure SQL database, to allow for real-time analytics of data in Microsoft Fabric.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala, roblescarlos, maprycem
ms.date: 10/30/2024
ms.service: fabric
ms.topic: conceptual
monikerRange: "=azuresqldb-current||=fabric"
---
# Mirrored databases in Microsoft Fabric
[!INCLUDE [asdb-fabric](../../includes/applies-to-version/asdb-fabric.md)]

You can [mirror databases in Azure SQL Database to Microsoft Fabric](/fabric/database/mirrored-database/overview). You can continuously replicate your existing data estate directly into Fabric's OneLake, including data from Azure SQL database.

For more information, see:

- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Microsoft Fabric mirrored databases monitoring](/fabric/database/mirrored-database/monitor)
- [Explore data in your mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)

This feature is not currently available for Azure SQL Managed Instance or SQL Server instances.

## What are mirrored databases in Fabric?

Mirroring in Fabric provides an easy experience to speed the time-to-value for insights and decisions, and to break down data silos between technology solutions, without developing expensive Extract, Transform, and Load (ETL) processes to move data.

With the most up-to-date data in a queryable format in OneLake, you can now use all the different services in Fabric, such as running analytics with Spark, executing notebooks, data engineering, visualizing through Power BI Reports, and more.

With mirroring, you don't need to piece together different services from multiple vendors. Instead, you can enjoy a highly integrated, end-to-end, and easy-to-use product that is designed to simplify your analytics needs, and built for openness and collaboration between technology solutions that can read the open-source Delta Lake table format.

## The technology within mirrored databases

The mirroring feature uses similar change feed technology as the Azure Synapse Link, and shares some system objects.

> [!NOTE]
> Enabling mirroring via the Fabric portal will create a `changefeed` database user, a `changefeed` schema, and several tables within the `changefeed` schema in your source database. Do not alter any of these system-managed objects.

## Mirrored SQL database

You can also create a mirrored SQL database in Microsoft Fabric. With [SQL database in Fabric](/fabric/database/sql/overview), your data is automatically accessible from other Fabric experiences. SQL database in Microsoft Fabric, which uses the same SQL Database Engine as Microsoft SQL Server and is similar to Azure SQL Database, inherits most of the Fabric mirroring capabilities from Azure SQL database. Your SQL database in Fabric is automatically mirrored to OneLake and presented in a read-only, queryable format. You can use all the different services in Fabric, such as running analytics with Spark, executing notebooks, data engineering, visualizing through Power BI Reports, and more.

For more information, see [SQL database in Microsoft Fabric (Preview)](sql-database-in-fabric.md).

## Related content

- [What is Microsoft Fabric?](/fabric/get-started/microsoft-fabric-overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)

---
title: "Microsoft Fabric mirrored databases"
description: Learn about the mirrored databases in Microsoft Fabric, from SQL Server and Azure SQL Database, to allow for real-time analytics of data in Microsoft Fabric.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala, roblescarlos, maprycem
ms.date: 03/12/2024
ms.service: fabric
ms.topic: conceptual
monikerRange: "=azuresqldb-current||=fabric"
---
# Microsoft Fabric mirrored databases
[!INCLUDE [asdb-fabric](../../includes/applies-to-version/asdb-fabric.md)]

You can [mirror databases in Azure SQL Database to Microsoft Fabric](/fabric/database/mirrored-database/overview). You can continuously replicate your existing data estate directly into Fabric's OneLake, including data from Azure SQL Database.

For more information, see:

- [Microsoft Fabric mirrored databases (Preview)](/fabric/database/mirrored-database/overview)
- [Microsoft Fabric mirrored databases monitoring](/fabric/database/mirrored-database/monitor)
- [Explore data in your mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)

This feature is not currently available for Azure SQL Managed Instance or SQL Server instances.

## What are Fabric mirrored databases?

Mirroring in Fabric provides an easy experience to speed the time-to-value for insights and decisions, and to break down data silos between technology solutions, without developing expensive Extract, Transform, and Load (ETL) processes to move data.

With the most up-to-date data in a queryable format in OneLake, you can now use all the different services in Fabric, such as running analytics with Spark, executing notebooks, data engineering, visualizing through Power BI Reports, and more.

With Mirroring in Fabric, you don't need to piece together different services from multiple vendors. Instead, you can enjoy a highly integrated, end-to-end, and easy-to-use product that is designed to simplify your analytics needs, and built for openness and collaboration between technology solutions that can read the open-source Delta Lake table format.

## Fabric mirrored databases

The Fabric mirrored database feature uses similar change feed technology as the Azure Synapse Link, and shares some system objects.

> [!NOTE]
> Enabling Mirroring via the Fabric portal will create a `changefeed` database user, a `changefeed` schema, and several tables within the `changefeed` schema in your source database. Do not alter any of these objects, they are system-managed.

## Related content

- [What is Microsoft Fabric?](/fabric/get-started/microsoft-fabric-overview)
- [Model data in the default Power BI semantic model in Microsoft Fabric](/fabric/data-warehouse/model-default-power-bi-dataset)
- [What is the SQL analytics endpoint for a Lakehouse?](/fabric/data-engineering/lakehouse-sql-analytics-endpoint)
- [Direct Lake](/power-bi/enterprise/directlake-overview)

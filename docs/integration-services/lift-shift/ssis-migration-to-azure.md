---
title: "SQL Server Integration Services migration to Azure overview| Microsoft Docs"
description: This article highlights process and tools to migrate SQL Server Integration Services migration to Azure.
ms.date: "04/10/2020"
ms.topic: conceptual
ms.prod: sql
ms.prod_service: "integration-services"
ms.custom: ""
ms.technology: integration-services
author: chugugrace
ms.author: chugu
---

# Migrate on-premises SSIS workloads to SSIS in ADF

This article lists process and tools that can help migrate SQL Server Integration Services (SSIS) workloads to SSIS in ADF.

[Migration overview](https://docs.microsoft.com/azure/data-factory/scenario-ssis-migration-overview) highlights overall migration process of your ETL workloads from on-premises SSIS to SSIS in ADF.

The migration process consists of two phases:
[Assessment](https://docs.microsoft.com/azure/data-factory/scenario-ssis-migration-overview#assessment) and [Migration](https://docs.microsoft.com/azure/data-factory/scenario-ssis-migration-overview#migration).

## Assessment

Data Migration Assistant (DMA) is a freely downloadable tool for this purpose that can be installed and executed locally. DMA assessment project of type Integration Services can be created to assess SSIS packages in batches and identify compatibility issues.

Get [Database Migration Assistant](https://docs.microsoft.com/sql/dma/dma-overview), and [perform  package assessment](https://docs.microsoft.com/sql/dma/dma-assess-ssis).

## Migration

Depending on the storage types of source SSIS packages and the migration destination of database workloads, the steps to migrate SSIS packages and SQL Server Agent jobs that schedule SSIS package executions may vary. For more information, see [this page](https://docs.microsoft.com/azure/data-factory/scenario-ssis-migration-overview#migration).

## Next steps

- [Migrate SSIS packages to Azure SQL Managed Instance](https://docs.microsoft.com/azure/dms/how-to-migrate-ssis-packages-managed-instance).
- [Migrate SSIS jobs to Azure Data Factory (ADF) with SQL Server Management Studio (SSMS)](https://docs.microsoft.com/azure/data-factory/how-to-migrate-ssis-job-ssms).

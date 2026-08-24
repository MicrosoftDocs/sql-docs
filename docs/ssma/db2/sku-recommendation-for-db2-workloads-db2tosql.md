---
title: "Azure SQL SKU Sizing Recommendation (Preview) for Db2 Workloads (Db2ToSQL)"
description: Learn how to use the builtin SKU sizing recommendation engine to get the ideal Azure SQL target when migrating a Db2 database.
author: subhojit-msft
ms.author: subasak
ms.reviewer: randolphwest
ms.date: 10/27/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
---
# Azure SQL SKU sizing recommendation (preview) for Db2 workloads (Db2ToSQL)

The SKU recommendation feature in SQL Server Migration Assistant (SSMA) for Db2 helps you estimate the appropriate Azure SQL target (Azure SQL Managed Instance, SQL Server on an Azure Virtual Machine, or Azure SQL Database), based on the performance characteristics of their existing Db2 workloads.

> [!NOTE]  
> The SKU recommendation feature is currently in preview.

This feature is available for both Db2 LUW and Db2 z/OS sources.

## Prerequisites

- SSMA for Db2 (latest version)
- Access to the source Db2 server
- Awareness on performance metrics such as:

  - MIPS (million instructions per second)
  - IOPS (input/output operations per second)
  - Storage size
  - CPU count

## How to use

1. **Create or Open a Project**: Launch SSMA for Db2 and either create a new project or open an existing one.

1. **Connect to Source Server**: Connect to your Db2 LUW or z/OS source server.

1. **Initiate SKU Recommendation**: Right-click on the source server node in the Metadata Explorer and select **Recommend SKU**.

1. **Input Performance Parameters**: Based on the type of source server, you're prompted for the following inputs.

   - **z/OS**: In z/OS source server, a dialog box appears with the following prompts:

     - **Database MIPS**: Used to estimate CPU requirements. The range is 20 to 20,000.

     - **IOPS**: Autopopulated, but can be overridden. The range is 500 to 260,000.

     - **Ratio of MIPS to vCores**: Autopopulated, but can be overridden. Adjustable based on workload characteristics.

     - **Number of vCores**: This field is auto populated from the previously entered values.

   - **LUW**: In LUW source server, a dialog box appears with the following prompt:

     - **IOPS**: The range is 500-260,000 and needs to be manually populated.

1. **Submit and Generate Report:** Select **Submit** to run the recommendation engine.

A detailed report in HTML format is generated, showing the following information:

- Source system details
- Recommended configurations for each Azure target
- Justification for each recommendation

## Output example

The report includes:

- **Host Name**: for example, `D11FD037`
- **OS Version**: for example, `DSN11015`
- **MIPS**: for example, 5000
- **Recommended targets**:

  - Azure SQL Managed Instance: 64 vCores
  - Azure VM: Custom configuration
  - Azure SQL Database: Based on compatibility and performance needs

> [!NOTE]  
> SKU recommendations are based on performance and resource usage data only. They don't account for feature compatibility (for example, XA/DTC transactions). Review these requirements separately before choosing a SKU. Also, Storage estimates can differ between Db2 and Azure SQL due to varying compression mechanisms. The final storage footprint, and associated cost, can vary slightly from the source system.

## Related content

- [Migration guide: IBM Db2 to Azure SQL Database](/azure/azure-sql/migration-guides/database/db2-to-sql-database-guide)
- [Migration guide: IBM Db2 to SQL Server on Azure VM](/azure/azure-sql/migration-guides/virtual-machines/db2-to-sql-on-azure-vm-guide)
- [Migrate Db2 databases to SQL Server](migrating-db2-databases-to-sql-server-db2tosql.md)

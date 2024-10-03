---
title: "Tutorial: Integrating SSIS with Fabric Data Warehouse"
description: Learn how to integrate SSIS with Fabric Data Warehouse
author: chugugrace
ms.author: chugu
ms.date: 08/23/2024
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
ms.custom: intro-deployment
---
# Tutorial: Integrating SSIS with Fabric Data Warehouse

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

This document focuses on the best practices to use existing SSIS packages to work with Data warehouse in Fabric platform. 

## Introduction

****Microsoft Fabric**** is a comprehensive analytics platform that covers every aspect of an organization’s data estate. One of its key experiences is Fabric Data Warehouse, which serves as a simplified SaaS solution for a fully transactional warehouse. It stores data in OneLake using an open format called Delta Parquet, ensuring that data can be accessed by other experiences within Fabric and other client applications that connect using SQL drivers.

****Microsoft Fabric****, as an analytics platform, exclusively supports authentication through ****Microsoft Entra ID**** for users and Service Principals (SPNs). This deliberate choice ensures centralized and identity-based security, aligning with modern security practices. So, SQL authentication and other authentication methods aren't supported in Fabric Data Warehouse within the Fabric ecosystem.

## Integration with Fabric Data Warehouse
Microsoft SQL Server Integration Services (SSIS) is a component of the Microsoft SQL Server database that is an ETL solution. SSIS is widely used by enterprise customers to perform ETL on premises by many customers.

Two key modifications are required in SSIS package to work seamlessly with Fabric Data Warehouse, outlined as follows.

### Authentication
If you're using SQL Authentication or Windows Authentication, reconfigure it to utilize Microsoft Entra ID User or Service Principal Name (SPN). Keep in mind that if you’re using a User account, multifactor authentication (MFA) must be disabled, as SSIS doesn't support pop-up prompts.  It also needs respective drivers as mentioned below:

****To use [OLEDB connection manager](../connection-manager/ole-db-connection-manager.md)****:
- Install [OLE DB Driver for SQL Server](../../connect/oledb/features/using-azure-active-directory.md) version that supports Microsoft Entra ID
- Set Authentication to ****ActiveDirectoryServicePrincipal****  or ****ActiveDirectoryPassword****.
- OLEDB only works for [Execute SQL Task](../control-flow/execute-sql-task.md), doesn't work for [OLE DB Destination](../data-flow/ole-db-destination.md).
:::image type="content" border="false" source="media/ole-db-connection-1.png" alt-text="Screenshot of oledb connection manager part 1." lightbox="media/ole-db-connection-1.png":::
:::image type="content"  source="media/ole-db-connection-2.png" alt-text="Screenshot of oledb connection manager part 2." lightbox="media/ole-db-connection-2.png":::
    
****To use ADO.NET connection manager****:
- Use Microsoft OLE DB provider for SQL Server for [.NET Framework Data Provider for OLE DB](/dotnet/framework/data/adonet/data-providers). 
- Set Authentication to ****ActiveDirectoryServicePrincipal**** or ****ActiveDirectoryPassword****.
:::image type="content" source="media/ado-net-connection.png" alt-text="Screenshot of ado connection manager part 1." lightbox="media/ado-net-connection.png":::
 
### File ingestion 
The ****Fabric Data Warehous****e recommends utilizing the native T-SQL command ‘COPY INTO’ for efficient data insertion into the warehouse. So, any DFT operations that currently rely on ****Fast Insert Mode**** or ****BCP IN**** scripts should be replaced with the ****COPY INTO**** statement by utilizing [Execute SQL Task](../control-flow/execute-sql-task.md). 

### SSIS writing data into Data Warehouse in Fabric

It's a common ETL scenario where data is read from different sources like transactional databases, network file shares, local/network etc., perform transformation steps and write back to a designated DW like a SQL server, synapse dedicated pool or any other SQL compliant data store (like shown below in the diagram).

:::image type="content" border="false" source="media/etl-data-warehouse-destination.png" alt-text="Diagram of etl data warehouse as destination.":::

In order to make same SSIS package to write to Fabric Data Warehouse, First, update the authentication to Microsoft Entra ID based if not already used. Second, temporarily stage the data in an ADLS Gen2. Then pass the path to COPY INTO command in [Execute SQL Task](../control-flow/execute-sql-task.md).
    

[Flexible File Destination](../data-flow/flexible-file-destination.md) component enables an SSIS package to write data to [Azure Data Lake Storage Gen2 (ADLS Gen2)](/azure/storage/blobs/data-lake-storage-introduction). Inside Data Flow task, after loading and transformation, add a [Flexible File Destination](../data-flow/flexible-file-destination.md), in which you can define destination file name and location in ADLS Gen2. 

:::image type="content" source="media/flexible-file-1.png" alt-text="Screenshot of Flexible file destination part 1." lightbox="media/flexible-file-1.png":::
:::image type="content" source="media/flexible-file-2.png" alt-text="Screenshot of Flexible file destination part 2." lightbox="media/flexible-file-2.png":::
:::image type="content" source="media/flexible-file-3.png" alt-text="Screenshot of Flexible file destination part 3." lightbox="media/flexible-file-3.png":::

Data landed in Azure Data Lake Storage (ADLS) Gen2 can be ingested into Warehouse using COPY statement directly via [Execute SQL Task](../control-flow/execute-sql-task.md).

For example: 
```sql
COPY INTO <table_name>
FROM 'https://<Your_storage_account>.dfs.core.windows.net/<folder>/'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL=(IDENTITY= 'Storage Account Key', SECRET= '<Your_account_key>'),
    FIELDQUOTE = '"',
    FIELDTERMINATOR=',',
    ROWTERMINATOR='0x0A',
    ENCODING = 'UTF8'
)
```
:::image type="content" source="media/execute-sql-task.png" alt-text="Screenshot of Execute sql task." lightbox="media/execute-sql-task.png" :::

More detail instructions refer to [Ingest data into your Warehouse using the COPY statement](/fabric/data-warehouse/ingest-data-copy).

### Known limitations

Fabric data Warehouse supports a subset of T-SQL data types and not all T-SQL all commands are currently supported. Your packages might be failed due to unsupported features. For details, please check [Data types in Warehouse](/fabric/data-warehouse/data-types?branch=main) and [T-SQL surface area](/fabric/data-warehouse/tsql-surface-area).

### References 
[T-SQL surface area - Microsoft Fabric | Microsoft Learn](/fabric/data-warehouse/tsql-surface-area)

[Options to get data into the Lakehouse - Microsoft Fabric | Microsoft Learn](/fabric/data-engineering/load-data-lakehouse)

[Ingesting data into the warehouse - Microsoft Fabric | Microsoft Learn](/fabric/data-warehouse/ingest-data)

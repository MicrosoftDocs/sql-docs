---
title: "Tutorial: Integrating SSIS with Fabric Data Warehouse"
description: Learn how to integrate SSIS with Fabric Data Warehouse
ms.reviewer: randolphwest
ms.date: 12/29/2025
ms.service: sql
ms.subservice: integration-services
ms.topic: tutorial
ms.custom:
  - intro-deployment
  - sfi-image-nochange
---
# Tutorial: Integrating SSIS with Fabric Data Warehouse

[!INCLUDE [sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

This article focuses on the best practices to use existing SSIS packages to work with Data warehouse in Fabric platform.

## Introduction

Microsoft Fabric is a comprehensive analytics platform that covers every aspect of an organization's data estate. One of its key experiences is Fabric Data Warehouse, which serves as a simplified SaaS solution for a fully transactional warehouse. It stores data in OneLake using an open format called Delta Parquet, ensuring that data can be accessed by other experiences within Fabric and other client applications that connect using SQL drivers.

As an analytics platform, Microsoft Fabric exclusively supports authentication through Microsoft Entra ID for users and service principals (SPNs). This deliberate choice ensures centralized and identity-based security, aligning with modern security practices. So, SQL authentication and other authentication methods aren't supported in Fabric Data Warehouse within the Fabric ecosystem.

## Integration with Fabric Data Warehouse

Microsoft SQL Server Integration Services (SSIS) is a component of the Microsoft SQL Server database that's an ETL solution. Many enterprise customers widely use SSIS to perform on-premises ETL.

To work seamlessly with Fabric Data Warehouse, you need to make two key modifications to your SSIS package.

### Authentication

If you're using SQL Authentication or Windows Authentication, reconfigure it to use Microsoft Entra ID User or Service Principal Name (SPN). If you use a user account, disable multifactor authentication (MFA), because SSIS doesn't support pop-up prompts. You also need the respective drivers as mentioned in the following sections:

To use [OLEDB connection manager](../connection-manager/ole-db-connection-manager.md):

- Install [Use Microsoft Entra ID](../../connect/oledb/features/using-azure-active-directory.md) version that supports Microsoft Entra ID.

- Set Authentication to `ActiveDirectoryServicePrincipal` or `ActiveDirectoryPassword`.

- OLEDB only works for [Execute SQL Task](../control-flow/execute-sql-task.md), doesn't work for [OLE DB Destination](../data-flow/ole-db-destination.md).

  :::image type="content" source="media/ole-db-connection-1.png" alt-text="Screenshot of oledb connection manager part 1.":::

  :::image type="content" source="media/ole-db-connection-2.png" alt-text="Screenshot of oledb connection manager part 2." lightbox="media/ole-db-connection-2.png":::

To use ADO.NET connection manager:

- Use Microsoft OLE DB provider for SQL Server for [.NET Framework Data Provider for OLE DB](/dotnet/framework/data/adonet/data-providers).

- Set Authentication to `ActiveDirectoryServicePrincipal` or `ActiveDirectoryPassword`.

  :::image type="content" source="media/ado-net-connection.png" alt-text="Screenshot of ado connection manager part 1." lightbox="media/ado-net-connection.png":::

### File ingestion

You should use the native `COPY INTO` T-SQL command for efficient data insertion into your warehouse in Microsoft Fabric. Replace any DFT operations that currently rely on **Fast Insert Mode** or `BCP IN` scripts with the `COPY INTO` statement by using [Execute SQL Task](../control-flow/execute-sql-task.md).

### SSIS writing data into Data Warehouse in Fabric

In common ETL scenarios, you read data from different sources like transactional databases, network file shares, local or network locations. You can perform transformation steps and write the data back to a designated data warehouse like a SQL server, Synapse dedicated pool, or any other SQL compliant data store (as shown in the following diagram).

:::image type="content" source="media/etl-data-warehouse-destination.png" alt-text="Diagram of etl data warehouse as destination.":::

To make the same SSIS package write to Fabric Data Warehouse, first update the authentication to Microsoft Entra ID based if it's not already used. Second, temporarily stage the data in an ADLS Gen2. Then pass the path to the COPY INTO command in [Execute SQL Task](../control-flow/execute-sql-task.md).

[Flexible File Destination](../data-flow/flexible-file-destination.md) component enables an SSIS package to write data to [Azure Data Lake Storage Gen2 (ADLS Gen2)](/azure/storage/blobs/data-lake-storage-introduction). Inside Data Flow task, after loading and transformation, add a [Flexible File Destination](../data-flow/flexible-file-destination.md), in which you can define destination file name and location in ADLS Gen2.

:::image type="content" source="media/flexible-file-1.png" alt-text="Screenshot of Flexible file destination part 1.":::

:::image type="content" source="media/flexible-file-2.png" alt-text="Screenshot of Flexible file destination part 2.":::

:::image type="content" source="media/flexible-file-3.png" alt-text="Screenshot of Flexible file destination part 3.":::

You can ingest data landed in Azure Data Lake Storage (ADLS) Gen2 into Warehouse with the `COPY` statement directly via [Execute SQL Task](../control-flow/execute-sql-task.md).

For example (replace `<storage_account>`, `<storage_account_key>` and `account_key` with valid values):

```sql
COPY INTO table_name FROM 'https://<storage_account>.dfs.core.windows.net/<folder>/'
WITH (FILE_TYPE = 'CSV',
     CREDENTIAL = (IDENTITY = '<storage_account_key>',
                  SECRET = '<account_key>'),
     FIELDQUOTE = '"',
     FIELDTERMINATOR = ',',
     ROWTERMINATOR = '0x0A',
     ENCODING = 'UTF8'
);
```

:::image type="content" source="media/execute-sql-task.png" alt-text="Screenshot of Execute SQL Task." lightbox="media/execute-sql-task.png":::

For more detailed instructions, see [Ingest data into your Warehouse using the COPY statement](/fabric/data-warehouse/ingest-data-copy).

## Limitations

Fabric data Warehouse supports a subset of T-SQL data types and not all T-SQL commands are currently supported. Your packages might fail due to unsupported features. For details, check [Data types in Warehouse](/fabric/data-warehouse/data-types) and [T-SQL surface area in Fabric Data Warehouse](/fabric/data-warehouse/tsql-surface-area).

### Related content

- [T-SQL surface area in Fabric Data Warehouse](/fabric/data-warehouse/tsql-surface-area)
- [Options to get data into the Fabric Lakehouse](/fabric/data-engineering/load-data-lakehouse)
- [Ingest data into the Warehouse](/fabric/data-warehouse/ingest-data)

---
title: "Virtualize external data in SQL Server 2019 Big Data Clusters using Azure Data Studio: comma separated values (csv)"
description: Steps detailing the create external table wizard for virtualization of a CSV file in a Big Data Cluster
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mikeray
ms.date: 04/24/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: polybase
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
ms.metadata: seo-lt-2019
---

# Use the External Table Wizard with CSV files in a SQL Server Big Data Cluster

SQL Server 2019 Big Data Cluster has the ability to **virtualize data** from CSV files in HDFS. This process allows the data to stay in its original location, but can be queried from a SQL Server instance like any other table in SQL Server. This feature is made possible due to the use of PolyBase connectors, and minimizes the need for ETL processes. For more information on Data Virtualization, see [What is PolyBase?](../relational-databases/polybase/polybase-guide.md) document.

## Prerequisites

- [A deployed big data cluster](deployment-guidance.md)
- [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md)

## Select or upload a CSV file for data virtualization 

In Azure Data Studio (ADS) [connect to the SQL Server master instance](connect-to-big-data-cluster.md#master) of your Big Data Cluster. Once connected, expand the HDFS elements in the object explorer to locate the CSV file(s) you would like to data virtualize. For the purposes of this tutorial, a new directory named **Data** was created by right-clicking on the HDFS root directory context menu. A sample csv data file, **airline_delay_causes.csv**, obtained from the [US Department of Transportation](https://www.transtats.bts.gov/OT_Delay/OT_DelayCause1.asp?pn=1) was then uploaded to the **Data** directory for virtualization (right-click on the **Data** directory and select **Upload files** from the context menu). 

![example csv file in HDFS](media/data-virtualization/100-csv-sample-file-hdfs.png)

## Create the Storage Pool External Data Source in your target database

The Storage Pool External Data Source is not created in a database by default in your Big Data Cluster. Before using the external table wizard, create the default **SqlStoragePool** External Data Source in your target database with the following Transact-SQL query. Make sure you first change the context of the query to your target database.

```sql
-- Create the default storage pool source for SQL Big Data Cluster
IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlStoragePool')
    CREATE EXTERNAL DATA SOURCE SqlStoragePool
    WITH (LOCATION = 'sqlhdfs://controller-svc/default');
```

## Launch the External Table wizard

From ADS, right-click on the CSV file and select **Create External Table From CSV File** from the context menu. You can also create external tables from CSV files from a directory in HDFS if the files under the directory follow the same schema. This would allow the virtualization of the data at a directory level without the need to process individual files and get a joined result set over the combined data. This launches the Virtualize Data wizard. You can also launch the Virtualize Data wizard from the command palette by typing Ctrl+Shift+P (in Windows) and Cmd+Shift+P (in Mac).

![Virtualize data wizard](media/data-virtualization/110-csv-virtualize-data-wizard.png)

## Select Destination Database

In this step, you choose the destination database you wish to virtualize the data into. The drop-down field will contain all acceptable databases in the SQL Master instance. Here you can also name the new external table, its external file format and select the schema.

![Select destination database](media/data-virtualization/120-csv-select-destination.png)

## Preview Data

On this window, you will be able to see a preview of the first 50 rows of your CSV file for validation.

Once done viewing the preview, click **Next** to continue

![External Data Source credentials](media/data-virtualization/130-csv-preview-data.png)

## Modify Columns

In the next window, you will be able to modify the columns of the external table you intend to create. You are able to alter the column name, change the data type and allow for nullable rows. 

![External Data Source credentials](media/data-virtualization/140-csv-modify-columns.png)

## Summary

This step provides a summary of your selections. It provides the SQL Server name, database name, table name, table schema and external table information. In this step, you have the option to **Generate Script**, which will script out in T-SQL the syntax to create the External Data Source or **Create Table** which will create the External Data Source object.

![Summary screen](media/data-virtualization/150-csv-virtualize-data-summary.png)

If you click **Create Table** you will be able to see the External table created in the Destination database.

![External Data Sources](media/data-virtualization/160-csv-external-data-sources.png)

If you click, **Generate Script** you will see the T-SQL query being generated for creating the External Data Source object.

![Generate script](media/data-virtualization/170-csv-generated-script.png)

Once created the table can now be queried directly using T-SQL from the SQL Server instance. 

## Next steps

For more information on SQL Server Big Data Cluster and related scenarios, see [What is SQL Server Big Data Cluster?](big-data-cluster-overview.md).

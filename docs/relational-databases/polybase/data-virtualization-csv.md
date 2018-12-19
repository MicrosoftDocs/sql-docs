---
title: Virtualize external data in SQL Server 2019 CTP 2.0 | Microsoft Docs
description: This page details the steps for using the Create external table wizard for a CSV file
author: Abiola
ms.author: aboke
manager: craigg
ms.date: 12/13/2018
ms.topic: conceptual
ms.prod: sql
ms.technology: polybase
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---

# Use the External Table Wizard with CSV files

SQL Server 2019 also allows the ability to virtualize data from a CSV file in HDFS.  This process allows the data to stay in its original location, however you can **virtualize** the data in a SQL Server instance so that it can be queried there like any other table in SQL Server. This feature will minimize the need for ETL processes. This is possible with the use of Polybase connectors. For more information on Data Virtualization, refer to our [Get started with PolyBase](polybase-guide.md) Document.

## Launch the External Table wizard

Connect to the HDFS root using the IP address. Expand the elements in the object explorer. Then select one of the CSV from which you would like to virtualize the data into an existing SQL Server instance. Right-click on the file and select **Create External Table From CSV File** from the context menu. You can also create external tables from CSV files fom a folder in HDFS if the files under the folder follow the same schema. This would allow the virtualization of the data at a folder level without the need to process individual files and get a joined result set over the combined data. This launches the Virtualize Data wizard. You can also launch the Virtualize Data wizard from the command palette by typing Ctrl+Shift+P (in Windows) and Cmd+Shift+P (in Mac).

![Virtualize data wizard](media/data-virtualization/csv-virtualize-data-wizard.png)

## Connect to a SQL Server Master Instance

Here you can specify which SQL Master Instance you will connect too using the IP, Port, and Credential information. Previously saved connections can be access via the **Active SQL Server connections** drop-down box. 
> [!NOTE]
>If you are using a saved connection the other fields will be blocked


![Select a data source](media/data-virtualization/csv-connect-to-master.png)

Click Next to proceed to the next step in the wizard, which sets the Database Master Key.

## Select Destination Database

In this step, you will choose the destination databse you wish to virtualize the data into. The drop-down field will contain all acceptable databases in the SQL Master instance specified in the previous screen. Here you can also name the new external table and see the schema it will use.

![Create a database master key](media/data-virtualization/csv-select-destination.png)


## Preview Data

On this window, you will be able to see a preview of the first 50 rows of your CSV file for validation.

Once done viewing the preview, click "Next" to continue

![External data source credentials](media/data-virtualization/csv-preview-data.png)

## Modify Columns

In the next window, you will be able to Modify the columns of the external table you intend to create. you are able to alter the column name, Change the data type, and allow for Nullable rows. 

![External data source credentials](media/data-virtualization/csv-modify-columns.png)


## Summary

This step provides a summary of your selections. It provides the SQL Master Instance and Proposed External table information. In this step, you have the option to **"Generate Script"**, which will script out in T-SQL the syntax to create the external data source or **Create** which will create the External Data Source object.

![Summary screen](media/data-virtualization/csv-virtualize-data-summary.png)

If you click "Create" you will be able to see the External table created in the Destination database.

![External data sources](media/data-virtualization/csv-external-data-sources.png)

If you click, **Generate Script** you will see the T-SQL query being generated for creating the External Data Source object.

![Generate script](media/data-virtualization/csv-generated-script.png)

> [!NOTE]
> Generate Script should be only visible in the last page of the wizard. Currently it shows in all pages.

## Next steps

For more information on SQL Server Big Data Cluster and related scenarios, see [What is SQL Server Big Data Cluster?](../../big-data-cluster/big-data-cluster-overview.md).

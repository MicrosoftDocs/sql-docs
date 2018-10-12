---
title: How to Query HDFS in a SQL Server big data cluster | Microsoft Docs
description: This tutorial demonstrates how to query HDFS data in a SQL Server 2019 big data cluster (preview). You create an external table over data in the storage pool and then run a query.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/11/2018
ms.topic: tutorial
ms.prod: sql
---

# Tutorial: Query HDFS in a SQL Server big data cluster

This tutorial demonstrates how to Query HDFS data in a SQL Server 2019 big data cluster.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Create an external table pointing to data in HDFS within the SQL Server big data cluster.
> * Run a Transact-SQL query to join this data with high-value data in the SQL Server master instance.

If you prefer, you can download and run a script for the commands in this tutorial. For instructions, see the [Sample script](#script) section.

## Prerequisites

- [Deploy a big data cluster on Kubernetes](deployment-guidance.md).
- [Install Azure Data Studio and the SQL Server 2019 extension](deploy-big-data-tools.md).
- [Load sample data into the cluster](#sampledata).

### <a id="sampledata"></a> Load sample data

SQL Server big data cluster tutorials use a common set of sample data. You can load this sample data into your cluster with the following steps:

1. Download the sample backup file [tpcxbb_1gb.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/tpcxbb_1gb.bak) to your machine.

1. Navigate to the SQL Server 2019 big data cluster [samples directory](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster).

1. Download the [bootstrap-sample-db.sql](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/bootstrap-sample-db.sql) Transact-SQL script.

1. Download and run one of the following two sample scripts from the command line:

   * **Windows**: [bootstrap-sample-db.cmd](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/bootstrap-sample-db.cmd)
   * **Linux**: [bootstrap-sample-db.sh](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/bootstrap-sample-db.sh)

   > [!TIP]
   > You can get usage instructions by running the script with no parameters.

The script performs the following actions:

* Restores the sample database on the SQL Server master instance.
* Executes the **bootstrap-sample-db.sql** script.
* Creates the necessary database objects
* Exports the web_clickstreams and inventory tables to CSV files.
* Uploads the web_clickstreams CSV file to HDFS inside the SQL Server 2019 big data cluster.

## Create an external table to HDFS

The storage pool contains web clickstream data in a CSV file stored in HDFS. Use the following steps to define an external table that can access the data in that file.

1. In Azure Data Studio, connect to the SQL Server master instance of your big data cluster. For more information, see [Connect to the SQL Server master instance](deploy-big-data-tools.md#master).

2. In the server dashboard, select **New Query**.

3. Run the following Transact-SQL command to change the context to the **Sales** database in the master instance.

   ```sql
   USE Sales
   GO
   ```

4. Define the format of the CSV file to read from HDFS. Press F5 to run the statement.

   ```sql
   CREATE EXTERNAL FILE FORMAT csv_file
   WITH (
       FORMAT_TYPE = DELIMITEDTEXT,
       FORMAT_OPTIONS(
           FIELD_TERMINATOR = ',',
           STRING_DELIMITER = '"',
           FIRST_ROW = 2,
           USE_TYPE_DEFAULT = TRUE)
   );
   ```

5. Create an external table that can read the `/clickstream_data` from the storage pool. The **SqlStoragePool** is accessible from the master instance of a big data cluster.

   ```sql
   CREATE EXTERNAL TABLE [web_clickstreams_hdfs]
   ("wcs_click_date_sk" BIGINT , "wcs_click_time_sk" BIGINT , "wcs_sales_sk" BIGINT , "wcs_item_sk" BIGINT , "wcs_web_page_sk" BIGINT , "wcs_user_sk" BIGINT)
   WITH
   (
       DATA_SOURCE = SqlStoragePool,
       LOCATION = '/clickstream_data',
       FILE_FORMAT = csv_file
   );
   GO
   ```

## Query the data

Run the following query to join the HDFS data in the `web_clickstream_hdfs` external table with the relational data in the local `Sales` database.

```sql
SELECT  
    wcs_user_sk,
    SUM( CASE WHEN i_category = 'Books' THEN 1 ELSE 0 END) AS book_category_clicks,
    SUM( CASE WHEN i_category_id = 1 THEN 1 ELSE 0 END) AS [Home & Kitchen],
    SUM( CASE WHEN i_category_id = 2 THEN 1 ELSE 0 END) AS [Music],
    SUM( CASE WHEN i_category_id = 3 THEN 1 ELSE 0 END) AS [Books],
    SUM( CASE WHEN i_category_id = 4 THEN 1 ELSE 0 END) AS [Clothing & Accessories],
    SUM( CASE WHEN i_category_id = 5 THEN 1 ELSE 0 END) AS [Electronics],
    SUM( CASE WHEN i_category_id = 6 THEN 1 ELSE 0 END) AS [Tools & Home Improvement],
    SUM( CASE WHEN i_category_id = 7 THEN 1 ELSE 0 END) AS [Toys & Games],
    SUM( CASE WHEN i_category_id = 8 THEN 1 ELSE 0 END) AS [Movies & TV],
    SUM( CASE WHEN i_category_id = 9 THEN 1 ELSE 0 END) AS [Sports & Outdoors]
  FROM [dbo].[web_clickstreams_hdfs]
  INNER JOIN item it ON (wcs_item_sk = i_item_sk
                        AND wcs_user_sk IS NOT NULL)
GROUP BY  wcs_user_sk;
GO
```

## Clean up

Use the following command to remove the external table used in this tutorial.

```sql
DROP EXTERNAL TABLE [dbo].[web_clickstreams_hdfs];
GO
```

## <a id="script"></a> Sample script

To download a sample script of the commands in this tutorial, see the [Data virtualization samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/data-virtualization) on GitHub.

## Next steps

Advance to the next article to learn how to TBD.
> [!div class="nextstepaction"]
> [TBD](big-data-cluster-overview.md)

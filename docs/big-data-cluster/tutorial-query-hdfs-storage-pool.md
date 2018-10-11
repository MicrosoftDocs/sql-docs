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

In SQL Server 2019 big data clusters, the SQL Server engine has gained the ability to natively read HDFS files, such as CSV and parquet files, by using SQL Server instances collocated on each of the HDFS data nodes. This enables SQL Server to filter and aggregate data locally in parallel across all of the HDFS data nodes.

In this example, you are going to create an external table in the SQL Server Master instance that points to data in HDFS within the SQL Server Big data cluster. Then you will join the data in the external table with high value data in SQL Master instance.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Create an external table to HDFS data in the storage pool
> * Run a Transact-SQL query to join this data with relational SQL Server data

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Prerequisites

- [Deploy a big data cluster on Kubernetes](deployment-guidance.md).
- [Azure Data Studio and the SQL Server 2019 extension](deploy-big-data-tools.md).

## Create an external table to HDFS

The storage pool contains web clickstream data in a CSV file stored in HDFS. Use the following steps to define an external table that can access the data in that file.

1. In Azure Data Studio, connect to the SQL Server master instance of your big data cluster. For more information, see [Connect to the SQL Server master instance](deploy-big-data-tools.md#master).

1. In the server dashboard, select **New Query**.

1. Run the following Transact-SQL command to change the context to the **Sales** database in the master instance.

   ```sql
   USE Sales
   GO
   ```

1. Define the format of the CSV file to read from HDFS. Press F5 to run the statement.

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

1. Create an external table that can read the `/clickstream_data` from the storage pool. The **SqlStoragePool** is accessible from the master instance of a big data cluster.

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

## Sample script

To download a sample script of the commands in this tutorial, go to TBD-LINK.

## Next steps

Advance to the next article to learn how to TBD.
> [!div class="nextstepaction"]
> [TBD](big-data-cluster-overview.md)

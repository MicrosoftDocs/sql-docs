---
title: Report Across Scaled-Out Cloud Databases
description: Use cross database queries to report across multiple databases.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: bgavrilovic
ms.date: 06/13/2025
ms.service: azure-sql-database
ms.subservice: scale-out
ms.topic: how-to
ms.custom:
  - sqldbrb=1
monikerRange: "=azuresql || =azuresql-db"
---
# Report across scaled-out cloud databases (preview)

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

[!INCLUDE [elastic-query-shard-map-manager-mode-end-of-support](includes/elastic-query-shard-map-manager-mode-end-of-support.md)]

You can create reports from multiple databases from a single connection point using an [elastic query](elastic-query-overview.md). The databases must be horizontally partitioned (also known as "sharded").

If you have an existing database, see [Migrate existing databases to scale out](elastic-convert-to-use-elastic-tools.md).

To understand the SQL objects needed to query, see [Reporting across scaled-out cloud databases (preview)](elastic-query-horizontal-partitioning.md).

## Prerequisites

Download and run the [Get started with Elastic Database Tools](elastic-scale-get-started.md).

## Create a shard map manager using the sample app

Here you'll create a shard map manager along with several shards, followed by insertion of data into the shards. If you happen to already have shards set up with sharded data in them, you can skip the following steps and move to the next section.

1. Build and run the **Getting started with Elastic Database tools** sample application by following the steps in the article section [Download and run the sample app](elastic-scale-get-started.md#download-and-run-the-sample-app-1). Once you finish all the steps, you'll see the following command prompt:

    :::image type="content" source="media/elastic-query-getting-started/cmd-prompt.png" alt-text="Screenshot from the command prompt of the Elastic Database Tools starter kit.":::

1. In the command window, type `1` and press **Enter**. This creates the shard map manager, and adds two shards to the server. Then type `3` and press **Enter**; repeat the action four times. This inserts sample data rows in your shards.
1. The [Azure portal](https://portal.azure.com) should show three new databases in your server:

   :::image type="content" source="media/elastic-query-getting-started/portal.png" alt-text="Screenshot of the Visual Studio confirmation creating three databases.":::

   At this point, cross-database queries are supported through the Elastic Database client libraries. For example, use option `4` in the command window. The results from a multi-shard query are always a `UNION ALL` of the results from all shards.

   In the next section, we create a sample database endpoint that supports richer querying of the data across shards.

## Create an elastic query database

1. Open the [Azure portal](https://portal.azure.com) and sign in.
1. Create a new database in Azure SQL Database in the same server as your shard setup. Name the database `ElasticDBQuery`. 

   You can use an existing database. If you can do so, it must not be one of the shards that you would like to execute your queries on. This database is used for creating the metadata objects for an elastic database query.


## Create database objects

### Database-scoped master key and credentials

These are used to connect to the shard map manager and the shards:

1. Open SQL Server Management Studio or SQL Server Data Tools in Visual Studio.
1. Connect to `ElasticDBQuery` database and execute the following T-SQL commands:

    ```sql
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<master_key_password>';

    CREATE DATABASE SCOPED CREDENTIAL ElasticDBQueryCred
    WITH IDENTITY = '<username>',
    SECRET = '<password>';
    ```

    Both "username" and "password" should be the login information you created in previous steps.

### External data sources

To create an external data source, execute the following command on the `ElasticDBQuery` database:

```sql
CREATE EXTERNAL DATA SOURCE MyElasticDBQueryDataSrc WITH
    (TYPE = SHARD_MAP_MANAGER,
    LOCATION = '<server_name>.database.windows.net',
    DATABASE_NAME = 'ElasticScaleStarterKit_ShardMapManagerDb',
    CREDENTIAL = ElasticDBQueryCred,
    SHARD_MAP_NAME = 'CustomerIDShardMap'
) ;
```    

 `CustomerIDShardMap` is the name of the shard map, if you created the shard map and shard map manager using the elastic database tools sample. However, if you used your custom setup for this sample, then it should be the shard map name you chose in your application.

### External tables

Create an external table that matches the Customers table on the shards by executing the following command on `ElasticDBQuery` database:

```sql
CREATE EXTERNAL TABLE [dbo].[Customers]
( [CustomerId] [int] NOT NULL,
    [Name] [nvarchar](256) NOT NULL,
    [RegionId] [int] NOT NULL)
WITH
( DATA_SOURCE = MyElasticDBQueryDataSrc,
    DISTRIBUTION = SHARDED([CustomerId])
) ;
```

## Execute a sample elastic database T-SQL query

Once you have defined your external data source and your external tables, use T-SQL to query your external tables.

Execute this query on the `ElasticDBQuery` database:

```sql
select count(CustomerId) from [dbo].[Customers];
```

You'll notice that the query aggregates results from all the shards and gives the following output:

:::image type="content" source="media/elastic-query-getting-started/details.png" alt-text="Output details.":::

## Import elastic database query results to Excel

 You can import the results from of a query to an Excel file.

1. Launch Microsoft Excel.
1. Navigate to the **Data** ribbon.
1. Select **From Other Sources** and select **From SQL Server**.
1. In the **Data Connection Wizard**, type the server name and login credentials. Then select **Next**.
1. In the dialog box **Select the database that contains the data you want**, select the `ElasticDBQuery` database.
1. Select the `Customers` table in the list view and select **Next**. Then select **Finish**.
1. In the **Import Data** form, under **Select how you want to view this data in your workbook**, select **Table**. Select **OK**.

All the rows from `Customers` table, stored in different shards populate the Excel sheet.

You can now use Excel's powerful data visualization functions. You can use the connection string with your server name, database name, and credentials to connect your BI and data integration tools to the elastic query database. Make sure that SQL Server is supported as a data source for your tool. You can refer to the elastic query database and external tables just like any other SQL Server database and SQL Server tables that you would connect to with your tool.

### Cost

There's no additional charge for using the Elastic Database Query feature.

For pricing information, see [SQL Database Pricing Details](https://azure.microsoft.com/pricing/details/sql-database/).

## Related content

- [Azure SQL Database elastic query overview (preview)](elastic-query-overview.md)
- [Get started with cross-database queries (vertical partitioning) (preview)](elastic-query-getting-started-vertical.md)
- [Query across cloud databases with different schemas (preview)](elastic-query-vertical-partitioning.md)
- [Reporting across scaled-out cloud databases (preview)](elastic-query-horizontal-partitioning.md)
- [sp_execute_remote](/sql/relational-databases/system-stored-procedures/sp-execute-remote-azure-sql-database)
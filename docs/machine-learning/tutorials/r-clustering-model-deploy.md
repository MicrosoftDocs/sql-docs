---
title: "Tutorial: Deploy a clustering model in R"
titleSuffix: SQL machine learning
description: In part four of this four-part tutorial series, you'll deploy a clustering model in R with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: garye, jroth
ms.date: 05/21/2020
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Tutorial: Deploy a clustering model in R with SQL machine learning
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In part four of this four-part tutorial series, you'll deploy a clustering model, developed in R, into a database using SQL Server Machine Learning Services or on Big Data Clusters.
::: moniker-end
::: moniker range="=sql-server-2017"
In part four of this four-part tutorial series, you'll deploy a clustering model, developed in R, into a database using SQL Server Machine Learning Services.
::: moniker-end
::: moniker range="=sql-server-2016"
In part four of this four-part tutorial series, you'll deploy a clustering model, developed in R, into a database using SQL Server R Services.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In part four of this four-part tutorial series, you'll deploy a clustering model, developed in R, into a database using Azure SQL Managed Instance Machine Learning Services.
::: moniker-end

In order to perform clustering on a regular basis, as new customers are registering, you need to be able call the R script from any app. To do that, you can deploy the R script in a database by putting the R script inside a SQL stored procedure. Because your model executes in the database, it can easily be trained against data stored in the database.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Create a stored procedure that generates the model
> * Perform clustering
> * Use the clustering information

In [part one](r-clustering-model-introduction.md), you installed the prerequisites and restored the sample database.

In [part two](r-clustering-model-prepare-data.md), you learned how to prepare the data from a database to perform clustering.

In [part three](r-clustering-model-build.md), you learned how to create and train a K-Means clustering model in R.

## Prerequisites

* Part four of this tutorial series assumes you have fulfilled the prerequisites of [**part one**](r-clustering-model-introduction.md) and completed the steps in [**part two**](r-clustering-model-build.md) and [**part three**](r-clustering-model-build.md).

## Create a stored procedure that generates the model

Run the following T-SQL script to create the stored procedure. The procedure recreates the steps you developed in parts two and three of this tutorial series:

* classify customers based on their purchase and return history
* generate four clusters of customers using a K-Means algorithm

The procedure stores the resulting customer cluster mappings in the database table **customer_return_clusters**.

```sql
USE [tpcxbb_1gb]
DROP PROC IF EXISTS generate_customer_return_clusters;
GO
CREATE procedure [dbo].[generate_customer_return_clusters]
AS
/*
  This procedure uses R to classify customers into different groups
  based on their purchase & return history.
*/
BEGIN
    DECLARE @duration FLOAT
    , @instance_name NVARCHAR(100) = @@SERVERNAME
    , @database_name NVARCHAR(128) = db_name()
-- Input query to generate the purchase history & return metrics
    , @input_query NVARCHAR(MAX) = N'
SELECT ss_customer_sk AS customer,
    round(CASE 
            WHEN (
                    (orders_count = 0)
                    OR (returns_count IS NULL)
                    OR (orders_count IS NULL)
                    OR ((returns_count / orders_count) IS NULL)
                    )
                THEN 0.0
            ELSE (cast(returns_count AS NCHAR(10)) / orders_count)
            END, 7) AS orderRatio,
    round(CASE 
            WHEN (
                    (orders_items = 0)
                    OR (returns_items IS NULL)
                    OR (orders_items IS NULL)
                    OR ((returns_items / orders_items) IS NULL)
                    )
                THEN 0.0
            ELSE (cast(returns_items AS NCHAR(10)) / orders_items)
            END, 7) AS itemsRatio,
    round(CASE 
            WHEN (
                    (orders_money = 0)
                    OR (returns_money IS NULL)
                    OR (orders_money IS NULL)
                    OR ((returns_money / orders_money) IS NULL)
                    )
                THEN 0.0
            ELSE (cast(returns_money AS NCHAR(10)) / orders_money)
            END, 7) AS monetaryRatio,
    round(CASE 
            WHEN (returns_count IS NULL)
                THEN 0.0
            ELSE returns_count
            END, 0) AS frequency
FROM (
    SELECT ss_customer_sk,
        -- return order ratio
        COUNT(DISTINCT (ss_ticket_number)) AS orders_count,
        -- return ss_item_sk ratio
        COUNT(ss_item_sk) AS orders_items,
        -- return monetary amount ratio
        SUM(ss_net_paid) AS orders_money
    FROM store_sales s
    GROUP BY ss_customer_sk
    ) orders
LEFT OUTER JOIN (
    SELECT sr_customer_sk,
        -- return order ratio
        count(DISTINCT (sr_ticket_number)) AS returns_count,
        -- return ss_item_sk ratio
        COUNT(sr_item_sk) AS returns_items,
        -- return monetary amount ratio
        SUM(sr_return_amt) AS returns_money
    FROM store_returns
    GROUP BY sr_customer_sk
    ) returned ON ss_customer_sk = sr_customer_sk
 '
EXECUTE sp_execute_external_script
      @language = N'R'
    , @script = N'
# Define the connection string

connStr <- paste("Driver=SQL Server; Server=", instance_name,
                 "; Database=", database_name,
                 "; uid=Username;pwd=Password; ",
                 sep="" )

# Input customer data that needs to be classified.
# This is the result we get from the query.
library(RODBC)

ch <- odbcDriverConnect(connStr);

customer_data <- sqlQuery(ch, input_query)

sqlDrop(ch, "customer_return_clusters")

## create clustering model
clust <- kmeans(customer_data[,2:5],4)

## create clustering output for table
customer_cluster <- data.frame(cluster=clust$cluster,customer=customer_data$customer,orderRatio=customer_data$orderRatio,
			itemsRatio=customer_data$itemsRatio,monetaryRatio=customer_data$monetaryRatio,frequency=customer_data$frequency)

## write cluster output to DB table
sqlSave(ch, customer_cluster, tablename = "customer_return_clusters")

## clean up
odbcClose(ch)
'
    , @input_data_1 = N''
    , @params = N'@instance_name nvarchar(100), @database_name nvarchar(128), @input_query nvarchar(max), @duration float OUTPUT'
    , @instance_name = @instance_name
    , @database_name = @database_name
    , @input_query = @input_query
    , @duration = @duration OUTPUT;
END;

GO
```

## Perform clustering

Now that you've created the stored procedure, execute the following script to perform clustering.

```sql
--Empty table of the results before running the stored procedure
TRUNCATE TABLE customer_return_clusters;

--Execute the clustering
--This will load the table customer_return_clusters with cluster mappings
EXECUTE [dbo].[generate_customer_return_clusters];
```

Verify that it works and that we actually have the list of customers and their cluster mappings.

```sql
--Select data from table customer_return_clusters
--to verify that the clustering data was loaded
SELECT TOP (5) *
FROM customer_return_clusters;
```

```result
cluster  customer  orderRatio  itemsRatio  monetaryRatio  frequency
1        29727     0           0           0              0
4        26429     0           0           0.041979       1
2        60053     0           0           0.065762       3
2        97643     0           0           0.037034       3
2        32549     0           0           0.031281       4
```

## Use the clustering information

Because you stored the clustering procedure in the database, it can perform clustering efficiently against customer data stored in the same database. You can execute the procedure whenever your customer data is updated and use the updated clustering information.

Suppose you want to send a promotional email to customers in cluster 0, the group that was inactive (you can see how the four clusters were described in [part three](r-clustering-model-build.md#analyze-the-results) of this tutorial). The following code selects the email addresses of customers in cluster 0.

```sql
USE [tpcxbb_1gb]
--Get email addresses of customers in cluster 0 for a promotion campaign
SELECT customer.[c_email_address], customer.c_customer_sk
  FROM dbo.customer
  JOIN
  [dbo].[customer_clusters] as c
  ON c.Customer = customer.c_customer_sk
  WHERE c.cluster = 0
```

You can change the **c.cluster** value to return email addresses for customers in other clusters.

## Clean up resources

When you're finished with this tutorial, you can delete the tpcxbb_1gb database.

## Next steps

In part four of this tutorial series, you learned how to:

* Create a stored procedure that generates the model
* Perform clustering with SQL machine learning
* Use the clustering information

To learn more about using R in Machine Learning Services, see:

* [Run simple R scripts](quickstart-r-create-script.md)
* [R data structures, types and objects](quickstart-r-data-types-and-objects.md)
* [R functions](quickstart-r-functions.md)

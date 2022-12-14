---
title: "Tutorial: Prepare data to perform clustering in R"
titleSuffix: SQL machine learning
description: In part two of this four-part tutorial series, you'll prepare the data from a database to perform clustering in R with SQL machine learning.
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
# Tutorial: Prepare data to perform clustering in R with SQL machine learning
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In part two of this four-part tutorial series, you'll prepare the data from a database to perform clustering in R with SQL Server Machine Learning Services or on Big Data Clusters.
::: moniker-end
::: moniker range="=sql-server-2017"
In part two of this four-part tutorial series, you'll prepare the data from a database to perform clustering in R with SQL Server Machine Learning Services.
::: moniker-end
::: moniker range="=sql-server-2016"
In part two of this four-part tutorial series, you'll prepare the data from a database to perform clustering in R with SQL Server 2016 R Services.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In part two of this four-part tutorial series, you'll prepare the data from a database to perform clustering in R with Azure SQL Managed Instance Machine Learning Services.
::: moniker-end

In this article, you'll learn how to:

> [!div class="checklist"]
> * Separate customers along different dimensions using R
> * Load the data from the database into an R data frame

In [part one](r-clustering-model-introduction.md), you installed the prerequisites and restored the sample database.

In [part three](r-clustering-model-build.md), you'll learn how to create and train a K-Means clustering model in R.

In [part four](r-clustering-model-deploy.md), you'll learn how to create a stored procedure in a database that can perform clustering in R based on new data.

## Prerequisites

* Part two of this tutorial assumes you have completed [**part one**](r-clustering-model-introduction.md).

## Separate customers

Create a new RScript file in RStudio and run the following script.
In the SQL query, you're separating customers along the following dimensions:

* **orderRatio** = return order ratio (total number of orders partially or fully returned versus the total number of orders)
* **itemsRatio** = return item ratio (total number of items returned versus the number of items purchased)
* **monetaryRatio** = return amount ratio (total monetary amount of items returned versus the amount purchased)
* **frequency** = return frequency

In the **connStr** function, replace **ServerName** with your own connection information.

```r
# Define the connection string to connect to the tpcxbb_1gb database

connStr <- "Driver=SQL Server;Server=ServerName;Database=tpcxbb_1gb;uid=Username;pwd=Password"

#Define the query to select data
input_query <- "
SELECT ss_customer_sk AS customer
    ,round(CASE 
            WHEN (
                       (orders_count = 0)
                    OR (returns_count IS NULL)
                    OR (orders_count IS NULL)
                    OR ((returns_count / orders_count) IS NULL)
                    )
                THEN 0.0
            ELSE (cast(returns_count AS NCHAR(10)) / orders_count)
            END, 7) AS orderRatio
    ,round(CASE 
            WHEN (
                     (orders_items = 0)
                  OR (returns_items IS NULL)
                  OR (orders_items IS NULL)
                  OR ((returns_items / orders_items) IS NULL)
                 )
            THEN 0.0
            ELSE (cast(returns_items AS NCHAR(10)) / orders_items)
            END, 7) AS itemsRatio
    ,round(CASE 
            WHEN (
                     (orders_money = 0)
                  OR (returns_money IS NULL)
                  OR (orders_money IS NULL)
                  OR ((returns_money / orders_money) IS NULL)
                 )
            THEN 0.0
            ELSE (cast(returns_money AS NCHAR(10)) / orders_money)
            END, 7) AS monetaryRatio
    ,round(CASE 
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
    ) returned ON ss_customer_sk = sr_customer_sk";
```

## Load the data into a data frame

Now use the following script to return the results from the query to an R data frame.

```r
# Query using input_query and get the results back
# to data frame customer_data

library(RODBC)

ch <- odbcDriverConnect(connStr)

customer_data <- sqlQuery(ch, input_query)

# Take a look at the data just loaded
head(customer_data, n = 5);
```

You should see results similar to the following.

```results
  customer orderRatio itemsRatio monetaryRatio frequency
1    29727          0          0      0.000000         0
2    26429          0          0      0.041979         1
3    60053          0          0      0.065762         3
4    97643          0          0      0.037034         3
5    32549          0          0      0.031281         4
```

## Clean up resources

If you're not going to continue with this tutorial, delete the tpcxbb_1gb database.

## Next steps

In part two of this tutorial series, you learned how to:

* Separate customers along different dimensions using R
* Load the data from the database into an R data frame

To create a machine learning model that uses this customer data, follow part three of this tutorial series:

> [!div class="nextstepaction"]
> [Create a predictive model in R with SQL machine learning](r-clustering-model-build.md)

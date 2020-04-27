---
title: "Tutorial: Prepare data to perform clustering in R"
titleSuffix: SQL Database Machine Learning Services
description: In part two of this four-part tutorial series, you'll prepare the data from an SQL database to perform clustering in R with SQL Database Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.topic: tutorial
author: cawrites
ms.author: chadam
ms.reviewer: garye
ms.date: 04/16/2020
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-current||=sqlallproducts-allversions"
---

# Tutorial: Prepare data to perform clustering in R with SQL machine learning services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
In part two of this four-part tutorial series, you'll restore and prepare the data from a database using R. Later in this series, you'll use this data to train and deploy a clustering model in R with SQL Server Machine Learning Services or on Big Data Clusters.
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
In part two of this four-part tutorial series, you'll restore and prepare the data from a database using R. Later in this series, you'll use this data to train and deploy a clustering model in R with SQL Server Machine Learning Services.
::: moniker-end
In parts one and two of this series, you'll develop some R scripts in RStudio to prepare your data and train a machine learning model. Then, in part three, you'll run those R scripts inside a SQL database using stored procedures.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Import a sample database into an SQL database
> * Separate customers along different dimensions using R
> * Load the data from the SQL database into an R data frame

In [part one](r-clustering-model-introduction.md), you installed the prerequisites and restored the sample database.

In [part three](r-tutorial-clustering-model-build-data.md), you'll learn how to create and train a K-Means clustering model in R.

In [part four](r-tutorial-clustering-model-deploy-data.md), you'll learn how to create a stored procedure in an SQL database that can perform clustering in R based on new data.


## Prerequisites

* R IDE - This tutorial uses [RStudio Desktop](https://www.rstudio.com/products/rstudio/download/).

* SQL query tool - This tutorial assumes you're using [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/what-is).


## Separate customers

Create a new RScript file in RStudio and run the following script.
In the SQL query, you're separating customers along the following dimensions:

* **orderRatio** = return order ratio (total number of orders partially or fully returned versus the total number of orders)
* **itemsRatio** = return item ratio (total number of items returned versus the number of items purchased)
* **monetaryRatio** = return amount ratio (total monetary amount of items returned versus the amount purchased)
* **frequency** = return frequency

In the **paste** function, replace **Server**, **UID**, and **PWD** with your own connection information.

```r
# Define the connection string to connect to the tpcxbb_1gb database
connStr <- paste("Driver=SQL Server",
               "; Server=", "<SQL Database Server>",
               "; Database=tpcxbb_1gb",
               "; UID=", "<user>",
               "; PWD=", "<password>",
                  sep = "");


#Define the query to select data from SQL Server
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
    ) returned ON ss_customer_sk = sr_customer_sk
"
```

## Load the data into a data frame

Now use the following script to return the results from the query to an R data frame using the **rxSqlServerData** function.
As part of the process, you'll define the type for the selected columns (using colClasses) to make sure that the types are correctly transferred to R.

```r
# Query SQL Server using input_query and get the results back
# to data frame customer_returns
# Define the types for selected columns (using colClasses),
# to make sure that the types are correctly transferred to R
customer_returns <- rxSqlServerData(
                     sqlQuery=input_query,
                     colClasses=c(customer ="numeric",
                                  orderRatio="numeric",
                                  itemsRatio="numeric",
                                  monetaryRatio="numeric",
                                  frequency="numeric" ),
                     connectionString=connStr);

# Transform the data from an input dataset to an output dataset
customer_data <- rxDataStep(customer_returns);

# Take a look at the data just loaded from SQL Server
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

***If you're not going to continue with this tutorial***, delete the tpcxbb_1gb database from your SQL Database server.

## Next steps

In part one of this tutorial series, you completed these steps:

* Separate customers along different dimensions using R
* Load the data from the SQL database into an R data frame

To create a machine learning model that uses this customer data, follow part three of this tutorial series:

> [!div class="nextstepaction"]
> [Tutorial: Create a predictive model in R with SQL machine learning services)](r-tutorial-clustering-model-build-data.md)

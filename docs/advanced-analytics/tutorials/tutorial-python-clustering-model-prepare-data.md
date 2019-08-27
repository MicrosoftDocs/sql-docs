---
title: "Tutorial: Prepare data to perform clustering in Python"
description: In part two of this four-part tutorial series, you'll prepare the data from a SQL Server database to perform clustering in Python with SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.devlang: python
ms.date: 08/27/2019
ms.topic: tutorial
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# Tutorial: Prepare data to perform clustering in Python with SQL Server Machine Learning Services

In part two of this four-part tutorial series, you'll import and prepare the data from a SQL database using Python. Later in this series, you'll use this data to train and deploy a clustering model in Python with SQL Server Machine Learning Services.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Separate customers along different dimensions using R
> * Load the data from the Azure SQL database into an R data frame

In [part one](tutorial-python-clustering-model.md), you installed the prerequisites and imported the sample database.

In [part three](tutorial-python-clustering-model-build.md), you'll learn how to create and train a K-Means clustering model in Python.

In [part four](tutorial-python-clustering-model-deploy.md), you'll learn how to create a stored procedure in a SQL database that can perform clustering in Python based on new data.

## Prerequisites

* Part two of this tutorial assumes you have fulfilled the prerequisites of [**part one**](tutorial-python-clustering-model.md).

## Separate customers

Create a new file in Visual Studio Code and enter the following script.

Create a new script file in RStudio and run the following script.
In the SQL query, you're separating customers along the following dimensions:

* **orderRatio** = return order ratio (total number of orders partially or fully returned versus the total number of orders)
* **itemsRatio** = return item ratio (total number of items returned versus the number of items purchased)
* **monetaryRatio** = return amount ratio (total monetary amount of items returned versus the amount purchased)
* **frequency** = return frequency

In the **paste** function, replace **Server**, **UID**, and **PWD** with your own connection information.

```python
# Load packages.
import matplotlib.pyplot as plt
import numpy as np
import pandas as pd
import revoscalepy as revoscale
from scipy.spatial import distance as sci_distance
from sklearn import cluster as sk_cluster

def perform_clustering():
    ################################################################################################

    ## Connect to DB and select data

    ################################################################################################

    # Connection string to connect to SQL Server named instance.
    conn_str = 'Driver=SQL Server;Server=localhost;Database=tpcxbb_1gb;Trusted_Connection=True;'

    input_query = '''SELECT
    ss_customer_sk AS customer,
    ROUND(COALESCE(returns_count / NULLIF(1.0*orders_count, 0), 0), 7) AS orderRatio,
    ROUND(COALESCE(returns_items / NULLIF(1.0*orders_items, 0), 0), 7) AS itemsRatio,
    ROUND(COALESCE(returns_money / NULLIF(1.0*orders_money, 0), 0), 7) AS monetaryRatio,
    COALESCE(returns_count, 0) AS frequency
    FROM
    (
      SELECT
        ss_customer_sk,
        -- return order ratio
        COUNT(distinct(ss_ticket_number)) AS orders_count,
        -- return ss_item_sk ratio
        COUNT(ss_item_sk) AS orders_items,
        -- return monetary amount ratio
        SUM( ss_net_paid ) AS orders_money
      FROM store_sales s
      GROUP BY ss_customer_sk
    ) orders
    LEFT OUTER JOIN
    (
      SELECT
        sr_customer_sk,
        -- return order ratio
        count(distinct(sr_ticket_number)) as returns_count,
        -- return ss_item_sk ratio
        COUNT(sr_item_sk) as returns_items,
        -- return monetary amount ratio
        SUM( sr_return_amt ) AS returns_money
    FROM store_returns
    GROUP BY sr_customer_sk ) returned ON ss_customer_sk=sr_customer_sk'''


    # Define the columns we wish to import.
    column_info = {
        "customer": {"type": "integer"},
        "orderRatio": {"type": "integer"},
        "itemsRatio": {"type": "integer"},
        "frequency": {"type": "integer"}
    }
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

***If you're not going to continue with this tutorial***, delete the tpcxbb_1gb database from your Azure SQL Database server.

From the Azure portal, follow these steps:

1. From the left-hand menu in the Azure portal, select **All resources** or **SQL databases**.
1. In the **Filter by name...** field, enter **tpcxbb_1gb**, and select your subscription.
1. Select your **tpcxbb_1gb** database.
1. On the **Overview** page, select **Delete**.

## Next steps

In part two of this tutorial series, you completed these steps:

* Separate customers along different dimensions using R
* Load the data from the Azure SQL database into an R data frame

To create a machine learning model that uses this customer data, follow part three of this tutorial series:

> [!div class="nextstepaction"]
> [Tutorial: Create a predictive model in Python with SQL Server Machine Learning Services](tutorial-python-clustering-model-build.md)

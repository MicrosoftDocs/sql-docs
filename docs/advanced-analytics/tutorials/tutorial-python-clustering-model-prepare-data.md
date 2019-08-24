---
title: "Tutorial: Prepare data to perform clustering in Python"
description: In part one of this three-part tutorial series, you'll prepare the data from a SQL Server database to perform clustering in Python with SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.devlang: python
ms.date: 08/23/2019
ms.topic: tutorial
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# Tutorial: Prepare data to perform clustering in Python with SQL Server Machine Learning Services

In part one of this three-part tutorial series, you'll import and prepare the data from a SQL database using Python. Later in this series, you'll use this data to train and deploy a clustering model in Python with SQL Server Machine Learning Services.

*Clustering* can be explained as organizing data into groups where members of a group are similar in some way.
You'll use the **K-Means** algorithm to perform the clustering of customers in a dataset of product purchases and returns. By clustering customers, you can focus your marketing efforts more effectively by targeting specific groups.
K-Means clustering is an *unsupervised learning* algorithm that looks for patterns in data based on similarities.

In parts one and two of this series, you'll develop some Python scripts in Visual Studio Code to prepare your data and train a machine learning model. Then, in part three, you'll run those Python scripts inside a SQL database using stored procedures.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Import a sample database into an Azure SQL database
> * Separate customers along different dimensions using R
> * Load the data from the Azure SQL database into an R data frame

In [part two](tutorial-python-clustering-model-build.md), you'll learn how to create and train a K-Means clustering model in Python.

In [part three](tutorial-python-clustering-model-deploy.md), you'll learn how to create a stored procedure in a SQL database that can perform clustering in Python based on new data.

## Prerequisites - **CHANGE**

* Install [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) with the Python language option.

* Install [python](https://www.python.org/) and a Python development environment. This article assumes you're using [Visual Studio Code](https://code.visualstudio.com/download) with the [Python Extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python).

* Install [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/what-is) or [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-ssms) (SSMS) on the client computer you use to connect to SQL Server. You can use other database management or query tools, but this article assumes Azure Data Studio or SSMS.

* You need some Python libraries that aren't included with SQL Server Machine Learning Services. Follow the instructions at [How to install Python client libraries for remote access to a Machine Learning Server](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter).

  ```python
  pip install matplotlib
  pip install scipy
  pip install sklearn
  ```

## Import the sample database

The sample dataset used in this tutorial has been saved to a **.bak** database backup file for you to download and use. This dataset is derived from the [tpcx-bb](http://www.tpc.org/tpcx-bb/default.asp) dataset provided by the [Transaction Processing Performance Council (TPC)](http://www.tpc.org/default.asp).

1. Download the file [tpcxbb_1gb.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/tpcxbb_1gb.bak) to the SQL Server backup folder. For the default database instance, the folder is:

   `C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\`

1. Open SSMS, connect to SQL Server, and open a new query window. Then run the following commands to restore the database.

   ```sql
   USE master;
   GO
   
   RESTORE DATABASE tpcxbb_1gb
   FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\tpcxbb_1gb.bak'
   WITH MOVE 'tpcxbb_1gb' TO 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\tpcxbb_1gb.mdf'
       , MOVE 'tpcxbb_1gb_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\tpcxbb_1gb.ldf';
   GO
   ```

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

In part one of this tutorial series, you completed these steps:

* Import a sample database into an Azure SQL database
* Separate customers along different dimensions using R
* Load the data from the Azure SQL database into an R data frame

To create a machine learning model that uses this customer data, follow part two of this tutorial series:

> [!div class="nextstepaction"]
> [Tutorial: Create a predictive model in Python with SQL Server Machine Learning Services](tutorial-python-clustering-model-build.md)

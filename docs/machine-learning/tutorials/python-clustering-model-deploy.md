---
title: "Python tutorial: Deploy cluster model"
titleSuffix: SQL machine learning
description: In part four of this four-part tutorial series, you'll deploy a clustering model in Python with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning
ms.devlang: python
ms.date: 05/21/2020
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf

ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Python tutorial: Deploy a model to categorize customers with SQL machine learning
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In part four of this four-part tutorial series, you'll deploy a clustering model, developed in Python, into a database using SQL Server Machine Learning Services or on Big Data Clusters.
::: moniker-end
::: moniker range="=sql-server-2017"
In part four of this four-part tutorial series, you'll deploy a clustering model, developed in Python, into a database using SQL Server Machine Learning Services.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In part four of this four-part tutorial series, you'll deploy a clustering model, developed in Python, into a database using Azure SQL Managed Instance Machine Learning Services.
::: moniker-end

In order to perform clustering on a regular basis, as new customers are registering, you need to be able call the Python script from any App. To do that, you can deploy the Python script in a database by putting the Python script inside a SQL stored procedure. Because your model executes in the database, it can easily be trained against data stored in the database.

In this section, you'll move the Python code you just wrote onto the server and deploy clustering.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Create a stored procedure that generates the model
> * Perform clustering on the server
> * Use the clustering information

In [part one](python-clustering-model.md), you installed the prerequisites and restored the sample database.

In [part two](python-clustering-model-prepare-data.md), you learned how to prepare the data from a database to perform clustering.

In [part three](python-clustering-model-build.md), you learned how to create and train a K-Means clustering model in Python.

## Prerequisites

* Part four of this tutorial series assumes you have fulfilled the prerequisites of [**part one**](python-clustering-model.md), and completed the steps in [**part two**](python-clustering-model-prepare-data.md) and [**part three**](python-clustering-model-build.md).

## Create a stored procedure that generates the model

Run the following T-SQL script to create the stored procedure. The procedure recreates the steps you developed in parts one and two of this tutorial series:

* classify customers based on their purchase and return history
* generate four clusters of customers using a K-Means algorithm

```sql
USE [tpcxbb_1gb]
GO

CREATE procedure [dbo].[py_generate_customer_return_clusters]
AS

BEGIN
    DECLARE

-- Input query to generate the purchase history & return metrics
     @input_query NVARCHAR(MAX) = N'
SELECT
  ss_customer_sk AS customer,
  CAST( (ROUND(COALESCE(returns_count / NULLIF(1.0*orders_count, 0), 0), 7) ) AS FLOAT) AS orderRatio,
  CAST( (ROUND(COALESCE(returns_items / NULLIF(1.0*orders_items, 0), 0), 7) ) AS FLOAT) AS itemsRatio,
  CAST( (ROUND(COALESCE(returns_money / NULLIF(1.0*orders_money, 0), 0), 7) ) AS FLOAT) AS monetaryRatio,
  CAST( (COALESCE(returns_count, 0)) AS FLOAT) AS frequency
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
    GROUP BY sr_customer_sk
  ) returned ON ss_customer_sk=sr_customer_sk
 '

EXEC sp_execute_external_script
      @language = N'Python'
    , @script = N'

import pandas as pd
from sklearn.cluster import KMeans

#get data from input query
customer_data = my_input_data

#We concluded in step 2 in the tutorial that 4 would be a good number of clusters
n_clusters = 4

#Perform clustering
est = KMeans(n_clusters=n_clusters, random_state=111).fit(customer_data[["orderRatio","itemsRatio","monetaryRatio","frequency"]])
clusters = est.labels_
customer_data["cluster"] = clusters

OutputDataSet = customer_data
'
    , @input_data_1 = @input_query
    , @input_data_1_name = N'my_input_data'
             with result sets (("Customer" int, "orderRatio" float,"itemsRatio" float,"monetaryRatio" float,"frequency" float,"cluster" float));
END;
GO
```

## Perform clustering

Now that you've created the stored procedure, execute the following script to perform clustering using the procedure.

```sql
--Create a table to store the predictions in

DROP TABLE IF EXISTS [dbo].[py_customer_clusters];
GO

CREATE TABLE [dbo].[py_customer_clusters] (
    [Customer] [bigint] NULL
  , [OrderRatio] [float] NULL
  , [itemsRatio] [float] NULL
  , [monetaryRatio] [float] NULL
  , [frequency] [float] NULL
  , [cluster] [int] NULL
  ,
    ) ON [PRIMARY]
GO

--Execute the clustering and insert results into table
INSERT INTO py_customer_clusters
EXEC [dbo].[py_generate_customer_return_clusters];

-- Select contents of the table to verify it works
SELECT * FROM py_customer_clusters;
```

## Use the clustering information

Because you stored the clustering procedure in the database, it can perform clustering efficiently against customer data stored in the same database. You can execute the procedure whenever your customer data is updated and use the updated clustering information.

Suppose you want to send a promotional email to customers in cluster 0, the group that was inactive (you can see how the four clusters were described in [part three](python-clustering-model-build.md#analyze-the-results) of this tutorial). The following code selects the email addresses of customers in cluster 0.

```sql
USE [tpcxbb_1gb]
--Get email addresses of customers in cluster 0 for a promotion campaign
SELECT customer.[c_email_address], customer.c_customer_sk
  FROM dbo.customer
  JOIN
  [dbo].[py_customer_clusters] as c
  ON c.Customer = customer.c_customer_sk
  WHERE c.cluster = 0
```

You can change the **c.cluster** value to return email addresses for customers in other clusters.

## Clean up resources

When you're finished with this tutorial, you can delete the tpcxbb_1gb database.

## Next steps

In part four of this tutorial series, you completed these steps:

* Create a stored procedure that generates the model
* Perform clustering on the server
* Use the clustering information

To learn more about using Python in SQL machine learning, see:

* [Quickstart: Create and run simple Python scripts](quickstart-python-create-script.md)
* [Other Python tutorials for SQL machine learning](python-tutorials.md)
* [Install Python packages with sqlmlutils](../package-management/install-additional-python-packages-on-sql-server.md)
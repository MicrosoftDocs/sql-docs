---
title: "Python tutorial: Prepare data"
description: In part two of this four-part tutorial series, you'll use Python to prepare data to predict ski rentals in SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.date: 09/03/2019
ms.topic: tutorial
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Python Tutorial: Prepare data to train a linear regression model in SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In part two of this four-part tutorial series, you'll prepare data from a SQL Server database using Python. Later in this series, you'll use this data to train and deploy a linear regression model in Python with SQL Server Machine Learning Services.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Load the data from the SQL Server database into a **pandas** data frame
> * Prepare the data in Python by removing some columns

In [part one](python-ski-rental-linear-regression.md), you learned how to restore the sample database.

In [part three](python-ski-rental-linear-regression-train-model.md), you'll learn how to train a linear regression machine learning model in Python.

In [part four](python-ski-rental-linear-regression-deploy-model.md), you'll learn how to store the model to SQL Server, and then create stored procedures from the Python scripts you developed in parts two and three. The stored procedures will run in SQL Server to make predictions based on new data.

## Prerequisites

* Part two of this tutorial assumes you have completed [part one](python-ski-rental-linear-regression.md) and its prerequisites.

## Explore and prepare the data

To use the data in Python, you'll load the data from the SQL Server database into a pandas data frame.

Create a new Python notebook in Azure Data Studio and run the following script. Replace `<SQL Server>` with your own SQL Server name.

The Python script below imports the dataset from the **dbo.rental_data** table in your database to a pandas data frame **df**.

```python
import pandas as pd
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error
from revoscalepy import RxComputeContext, RxInSqlServer, RxSqlServerData
from revoscalepy import rx_import

# Connection string to your SQL Server instance
conn_str = 'Driver=SQL Server;Server=<SQL Server>;Database=TutorialDB;Trusted_Connection=True;'

# Define the columns you will import
 column_info = {
         "Year" : { "type" : "integer" },
         "Month" : { "type" : "integer" },
         "Day" : { "type" : "integer" },
         "RentalCount" : { "type" : "integer" },
         "WeekDay" : {
             "type" : "factor",
             "levels" : ["1", "2", "3", "4", "5", "6", "7"]
         },
         "Holiday" : {
             "type" : "factor",
             "levels" : ["1", "0"]
         },
         "Snow" : {
             "type" : "factor",
             "levels" : ["1", "0"]
         }
     }

# Get the data from the SQL Server table
data_source = RxSqlServerData(table="dbo.rental_data",
                               connection_string=conn_str, column_info=column_info)
computeContext = RxInSqlServer(
     connection_string = conn_str,
     num_tasks = 1,
     auto_cleanup = False
)

RxInSqlServer(connection_string=conn_str, num_tasks=1, auto_cleanup=False)

# import data source and convert to pandas dataframe
df = pd.DataFrame(rx_import(input_data = data_source))
print("Data frame:", df)

# Get all the columns from the dataframe.
columns = df.columns.tolist()

# Filter the columns to remove ones we don't want to use in the training
columns = [c for c in columns if c not in ["Year"]]
```

You should see results similar to the following.

```results
Rows Processed: 453
Data frame:      Day  Holiday  Month  RentalCount  Snow  WeekDay  Year
0     20        1      1          445     2        2  2014
1     13        2      2           40     2        5  2014
2     10        2      3          456     2        1  2013
3     31        2      3           38     2        2  2014
4     24        2      4           23     2        5  2014
5     11        2      2           42     2        4  2015
6     28        2      4          310     2        1  2013
...
[453 rows x 7 columns]
```

## Next steps

In part two of this tutorial series, you completed these steps:

* Load the data from the SQL Server database into a **pandas** data frame
* Prepare the data in Python by removing some columns

To train a machine learning model that uses data from the TutorialDB database, follow part three of this tutorial series:

> [!div class="nextstepaction"]
> [Python Tutorial: Train a linear regression model](python-ski-rental-linear-regression-train-model.md)
---
title: "Python tutorial: Prepare data"
description: In part two of this four-part tutorial series, you'll use Python to prepare data to predict ski rentals in SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.date: 01/02/2020
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

In the connection string, replace connection details as needed.

```python
import pyodbc
import pandas
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error

# Connection string to your SQL Server instance
conn_str = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server}; SERVER=localhost; DATABASE=TutorialDB; Trusted_Connection=yes')

query_str = 'SELECT Year, Month, Day, Rentalcount, Weekday, Holiday, Snow FROM dbo.rental_data'

df = pandas.read_sql(sql=query_str, con=conn_str)

print("Data frame:", df)

# Get all the columns from the dataframe.
columns = df.columns.tolist()

# Filter the columns to remove ones we don't want to use in the training
columns = [c for c in columns if c not in ["Year"]]
```

You should see results similar to the following.

```results
Data frame:      Year  Month  Day  RentalCount  WeekDay  Holiday  Snow
0    2014      1   20          445        2        1     0
1    2014      2   13           40        5        0     0
2    2013      3   10          456        1        0     0
3    2014      3   31           38        2        0     0
4    2014      4   24           23        5        0     0
..    ...    ...  ...          ...      ...      ...   ...
448  2013      2   19           57        3        0     1
449  2015      3   18           26        4        0     0
450  2015      3   24           29        3        0     1
451  2014      3   26           50        4        0     1
452  2015     12    6          377        1        0     1

[453 rows x 7 columns]
```

## Next steps

In part two of this tutorial series, you completed these steps:

* Load the data from the SQL Server database into a **pandas** data frame
* Prepare the data in Python by removing some columns

To train a machine learning model that uses data from the TutorialDB database, follow part three of this tutorial series:

> [!div class="nextstepaction"]
> [Python Tutorial: Train a linear regression model](python-ski-rental-linear-regression-train-model.md)
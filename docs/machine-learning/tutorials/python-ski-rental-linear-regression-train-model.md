---
title: "Python tutorial: Train model"
titleSuffix: SQL machine learning
description: In part three of this four-part tutorial series, you'll train a linear regression model in Python to predict ski rentals with SQL machine learning. 
ms.service: sql
ms.subservice: machine-learning
ms.date: 06/15/2022
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Python tutorial: Train a linear regression model with SQL machine learning
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In part three of this four-part tutorial series, you'll train a linear regression model in Python. In the next part of this series, you'll deploy this model in a SQL Server database with Machine Learning Services or on SQL Server 2019 Big Data Clusters.
::: moniker-end
::: moniker range="=sql-server-2017"
In part three of this four-part tutorial series, you'll train a linear regression model in Python. In the next part of this series, you'll deploy this model in a SQL Server database with Machine Learning Services.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In part three of this four-part tutorial series, you'll train a linear regression model in Python. In the next part of this series, you'll deploy this model in an Azure SQL Managed Instance database with Machine Learning Services.
::: moniker-end

In this article, you'll learn how to:

> [!div class="checklist"]
> * Train a linear regression model
> * Make predictions using the linear regression model

In [part one](python-ski-rental-linear-regression.md), you learned how to restore the sample database.

In [part two](python-ski-rental-linear-regression-prepare-data.md), you learned how to load the data from a database into a Python data frame, and prepare the data in Python.

In [part four](python-ski-rental-linear-regression-deploy-model.md), you'll learn how to store the model in a database, and then create stored procedures from the Python scripts you developed in parts two and three. The stored procedures will run in on the server to make predictions based on new data.

## Prerequisites

* Part three of this tutorial assumes you have completed [part one](python-ski-rental-linear-regression.md) and its prerequisites.

## Train the model

In order to predict, you have to find a function (model) that best describes the dependency between the variables in our dataset. This called training the model. The training dataset will be a subset of the entire dataset from the pandas data frame `df` that you created in part two of this series.

You will train model `lin_model` using a linear regression algorithm.

```python
# Store the variable we'll be predicting on.
target = "Rentalcount"

# Generate the training set.  Set random_state to be able to replicate results.
train = df.sample(frac=0.8, random_state=1)

# Select anything not in the training set and put it in the testing set.
test = df.loc[~df.index.isin(train.index)]

# Print the shapes of both sets.
print("Training set shape:", train.shape)
print("Testing set shape:", test.shape)

# Initialize the model class.
lin_model = LinearRegression()

# Fit the model to the training data.
lin_model.fit(train[columns], train[target])
```

You should see results similar to the following.

```results
Training set shape: (362, 7)
Testing set shape: (91, 7)
```

## Make predictions

Use a predict function to predict the rental counts using the model `lin_model`.

```python
# Generate our predictions for the test set.
lin_predictions = lin_model.predict(test[columns])
print("Predictions:", lin_predictions)

# Compute error between our test predictions and the actual values.
lin_mse = mean_squared_error(lin_predictions, test[target])
print("Computed error:", lin_mse)
```

You should see results similar to the following.

```results
Predictions: [124.41293228 123.8095075  117.67253182 209.39332151 135.46159387
 199.50603805 472.14918499  90.15781602 216.61319499 120.30710327
  89.47591091 127.71290441 207.44065517 125.68466139 201.38119194
 204.29377218 127.4494643  113.42721447 127.37388762  94.66754136
  90.21979191 173.86647615 130.34747586 111.81550069 118.88131715
 124.74028405 211.95038051 202.06309706 123.53053083 167.06313191
 206.24643852 122.64812937 179.98791527 125.1558454  168.00847713
 120.2305587  196.60802649 117.00616326 173.20010759  89.9563518
  92.11048236 120.91052805 175.47818992 129.65196995 120.97443971
 175.95863082 127.24800008 135.05866542 206.49627783  91.63004147
 115.78280925 208.92841718 213.5137192  212.83278197  96.74415948
  95.1324457  199.9089665  206.10791806 126.16510228 120.0281266
 209.08150631 132.88996619 178.84110582 128.85971386 124.67637239
 115.58134503  96.82167192 514.61789505 125.48319717 207.50359894
 121.64080826 201.9381774  113.22575025 202.46505762  90.7002328
  92.31194658 201.25627228 516.97252195  91.36660136 599.27093251
 199.6445585  123.66905128 117.4710676  173.12259514 129.60359486
 209.59478573 206.29481361 210.69322009 205.50255751 210.88011563
 207.65572019]
Computed error: 35003.54030828391
```

## Next steps

In part three of this tutorial series, you completed these steps:

* Train a linear regression model
* Make predictions using the linear regression model

To deploy the machine learning model you've created, follow part four of this tutorial series:

> [!div class="nextstepaction"]
> [Python Tutorial: Deploy a machine learning model](python-ski-rental-linear-regression-deploy-model.md)

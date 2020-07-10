---
title: "Python tutorial: Train model"
titleSuffix: SQL machine learning
description: In part three of this four-part tutorial series, you'll train a linear regression model in Python to predict ski rentals with SQL machine learning. 
ms.prod: sql
ms.technology: machine-learning
ms.date: 05/21/2020
ms.topic: tutorial
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current||=sqlallproducts-allversions"
---
# Python tutorial: Train a linear regression model with SQL machine learning
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
In part three of this four-part tutorial series, you'll train a linear regression model in Python. In the next part of this series, you'll deploy this model in a SQL Server database with Machine Learning Services or on Big Data Clusters.
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
In part three of this four-part tutorial series, you'll train a linear regression model in Python. In the next part of this series, you'll deploy this model in a SQL Server database with Machine Learning Services.
::: moniker-end
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"
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

In order to predict, you have to find a function (model) that best describes the dependency between the variables in our dataset. This called training the model. The training dataset will be a subset of the entire dataset from the pandas data frame **df** that you created in part two of this series.

You will train model **lin_model** using a linear regression algorithm.

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

Use a predict function to predict the rental counts using the model **lin_model**.

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
Predictions: [ 40.  38. 240.  39. 514.  48. 297.  25. 507.  24.  30.  54.  40.  26.
  30.  34.  42. 390. 336.  37.  22.  35.  55. 350. 252. 370. 499.  48.
  37. 494.  46.  25. 312. 390.  35.  35. 421.  39. 176.  21.  33. 452.
  34.  28.  37. 260.  49. 577. 312.  24.  24. 390.  34.  64.  26.  32.
  33. 358. 348.  25.  35.  48.  39.  44.  58.  24. 350. 651.  38. 468.
  26.  42. 310. 709. 155.  26. 648. 617.  26. 846. 729.  44. 432.  25.
  39.  28. 325.  46.  36.  50.  63.]
Computed error: 2.9960763804270902e-27
```

## Next steps

In part three of this tutorial series, you completed these steps:

* Train a linear regression model
* Make predictions using the linear regression model

To deploy the machine learning model you've created, follow part four of this tutorial series:

> [!div class="nextstepaction"]
> [Python Tutorial: Deploy a machine learning model](python-ski-rental-linear-regression-deploy-model.md)

---
title: "Tutorial: Prepare data to train a predictive model in R"
titleSuffix: SQL Database Machine Learning Services
description: In part two of this four-part tutorial series, you'll prepare the data to train a predictive model in R with SQL machine learning.
ms.prod: sql
ms.technology: machine-learning
ms.topic: tutorial
author: cawrites
ms.author: chadam
ms.reviewer: garye
ms.date: 04/27/2020
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"	

---

# Tutorial: Prepare data to train a predictive model in R with SQL machine learning Services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
In part two of this four-part tutorial series, you'll prepare data from a database using R. Later in this series, you'll use this data to train and deploy a linear regression model in R with SQL Server Machine Learning Services or on Big Data Clusters.
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
In part two of this four-part tutorial series, you'll prepare data from a database using R. Later in this series, you'll use this data to train and deploy a linear regression model in Python with SQL Server Machine Learning Services.
::: moniker-end

In this article, you'll learn how to:

> [!div class="checklist"]
> * Import a sample database into a database
> * Load the data from the SQL database into an R data frame
> * Prepare the data in R by identifying some columns as categorical

In [part one](r-tutorial-predictive-model-introduction.md), you learned how to restore the sample database.

In [part three](r-tutorial-predictive-model-train.md), you'll learn how to train a linear regression machine learning model in R.

In [part four](r-tutorial-predictive-model-deploy.md), you'll learn how to store the model in a database, and then create stored procedures from the R scripts you developed in parts two and three. The stored procedures will run on the server to make predictions based on new data.

## Prerequisites

* Part two of this tutorial assumes you have completed [part one](r-tutorial-predictive-model-introduction.md) and its prerequisites.

## Prepare the data

In this sample database, most of the preparation has already been done, but you'll do one more preparation here.
Use the following R script to identify three columns as *categories* by changing the data types to *factor*.



```r
#Changing the three factor columns to factor types
rentaldata$Holiday <- factor(rentaldata$Holiday);
rentaldata$Snow    <- factor(rentaldata$Snow);
rentaldata$WeekDay <- factor(rentaldata$WeekDay);



#Visualize the dataset after the change
str(rentaldata);
```

You should see results similar to the following.

```results
data.frame':      453 obs. of  7 variables:
$ Year       : int  2014 2014 2013 2014 2014 2015 2013 2014 2013 2015 ...
$ Month      : num  1 2 3 3 4 2 4 3 4 3 ...
$ Day        : num  20 13 10 31 24 11 28 8 5 29 ...
$ RentalCount: num  445 40 456 38 23 42 310 240 22 360 ...
$ WeekDay    : Factor w/ 7 levels "1","2","3","4",..: 2 5 1 2 5 4 1 7 6 1 ...
$ Holiday    : Factor w/ 2 levels "0","1": 2 1 1 1 1 1 1 1 1 1 ...
$ Snow       : Factor w/ 2 levels "0","1": 1 1 1 1 1 1 1 1 1 1 ...
```

The data is now prepared for training.

## Clean up resources

If you're not going to continue with this tutorial, delete the TutorialDB database from your Azure SQL Database server.

## Next steps

In part two of this tutorial series, you learned:

* Import a sample database
* Load the data from SQL Server into an R data frame
* Prepare the data in R by identifying some columns as categorical

To create a machine learning model that uses data from the TutorialDB database, follow part three of this tutorial series:

> [!div class="nextstepaction"]
> [Tutorial: Create a predictive model in R with SQL machine learning services](r-tutorial-predictive-model-train.md)

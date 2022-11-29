---
title: "Tutorial: Prepare data to train a predictive model in R"
titleSuffix: SQL machine learning
description: In part two of this four-part tutorial series, you'll prepare the data to train a predictive model in R with SQL machine learning.
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
# Tutorial: Prepare data to train a predictive model in R with SQL machine learning
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In part two of this four-part tutorial series, you'll prepare data from a database using R. Later in this series, you'll use this data to train and deploy a predictive model in R with SQL Server Machine Learning Services or on Big Data Clusters.
::: moniker-end
::: moniker range="=sql-server-2017"
In part two of this four-part tutorial series, you'll prepare data from a database using R. Later in this series, you'll use this data to train and deploy a predictive model in R with SQL Server Machine Learning Services.
::: moniker-end
::: moniker range="=sql-server-2016"
In part two of this four-part tutorial series, you'll prepare data from a database using R. Later in this series, you'll use this data to train and deploy a predictive model in R with SQL Server R Services.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In part two of this four-part tutorial series, you'll prepare data from a database using R. Later in this series, you'll use this data to train and deploy a predictive model in R with Azure SQL Managed Instance Machine Learning Services.
::: moniker-end

In this article, you'll learn how to:

> [!div class="checklist"]
> * Restore a sample database into a database
> * Load the data from the database into an R data frame
> * Prepare the data in R by identifying some columns as categorical

In [part one](r-predictive-model-introduction.md), you learned how to restore the sample database.

In [part three](r-predictive-model-train.md), you'll learn how to train a machine learning model in R.

In [part four](r-predictive-model-deploy.md), you'll learn how to store the model in a database, and then create stored procedures from the R scripts you developed in parts two and three. The stored procedures will run on the server to make predictions based on new data.

## Prerequisites

Part two of this tutorial assumes you have completed [**part one**](r-predictive-model-introduction.md) and its prerequisites.

## Load the data into a data frame

To use the data in R, you'll load the data from the database into a data frame (`rentaldata`).

Create a new RScript file in RStudio and run the following script. Replace **ServerName** with your own connection information.

```r
#Define the connection string to connect to the TutorialDB database
connStr <- "Driver=SQL Server;Server=ServerName;Database=TutorialDB;uid=Username;pwd=Password"


#Get the data from the table
library(RODBC)

ch <- odbcDriverConnect(connStr)

#Import the data from the table
rentaldata <- sqlFetch(ch, "dbo.rental_data")

#Take a look at the structure of the data and the top rows
head(rentaldata)
str(rentaldata)
```

You should see results similar to the following.

```results
   Year  Month  Day  RentalCount  WeekDay  Holiday  Snow
1  2014    1     20      445         2        1      0
2  2014    2     13       40         5        0      0
3  2013    3     10      456         1        0      0
4  2014    3     31       38         2        0      0
5  2014    4     24       23         5        0      0
6  2015    2     11       42         4        0      0
'data.frame':       453 obs. of  7 variables:
$ Year       : int  2014 2014 2013 2014 2014 2015 2013 2014 2013 2015 ...
$ Month      : num  1 2 3 3 4 2 4 3 4 3 ...
$ Day        : num  20 13 10 31 24 11 28 8 5 29 ...
$ RentalCount: num  445 40 456 38 23 42 310 240 22 360 ...
$ WeekDay    : num  2 5 1 2 5 4 1 7 6 1 ...
$ Holiday    : int  1 0 0 0 0 0 0 0 0 0 ...
$ Snow       : num  0 0 0 0 0 0 0 0 0 0 ...
```

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

If you're not going to continue with this tutorial, delete the TutorialDB database.

## Next steps

In part two of this tutorial series, you learned how to:

* Load the sample data into an R data frame
* Prepare the data in R by identifying some columns as categorical

To create a machine learning model that uses data from the TutorialDB database, follow part three of this tutorial series:

> [!div class="nextstepaction"]
> [Create a predictive model in R with SQL machine learning](r-predictive-model-train.md)

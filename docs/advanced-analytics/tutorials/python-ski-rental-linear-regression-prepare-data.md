---
title: 'Python tutorial: Prepare data (linear regression)'
description: 
ms.prod: sql
ms.technology: machine-learning
ms.date: 08/27/2019
ms.topic: tutorial
author: dphansen
ms.author: davidph
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

In [part three](python-ski-rental-linear-regression-build-compare.md), you'll learn how to create and train multiple machine learning models in Python, and then choose the most accurate one.

In [part four](python-ski-rental-linear-regression-deploy.md), you'll learn how to store the model to SQL Server, and then create stored procedures from the Python scripts you developed in parts two and three. The stored procedures will run in SQL Server to make predictions based on new data.

## Prerequisites

* Part two of this tutorial assumes you have completed [part one](python-ski-rental-linear-regression.md) and its prerequisites.

## Restore the sample database

The sample dataset used in this tutorial has been saved to a **.bak** database backup file for you to download and use.

1. Download the file [TutorialDB.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/TutorialDB.bak).

1. Follow the directions in [Restore a database from a backup file](../../azure-data-studio/tutorial-backup-restore-sql-server.md#restore-a-database-from-a-backup-file) in Azure Data Studio, using these details:

   * Import from the **TutorialDB.bak** file you downloaded
   * Name the target database "TutorialDB"

1. You can verify that the dataset exists after you have restored the database by querying the **dbo.rental_data** table:

    ```sql
    USE TutorialDB;
    SELECT * FROM [dbo].[rental_data];
    ```

## Next steps

In part one of this tutorial series, you completed these steps:

* Installed the pre-requisites
* Import a sample database into an SQL Server

To prepare the data from the TutorialDB database, follow part two of this tutorial series:

> [!div class="nextstepaction"]
> [Python Tutorial: Prepare data to train a linear regression model](python-ski-rental-linear-regression-prepare-data.md)
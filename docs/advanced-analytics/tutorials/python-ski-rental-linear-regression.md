---
title: "Python tutorial: Ski rentals"
description: In part three of this four-part tutorial series, you'll build a linear regression model in Python to predict ski rentals in SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.date: 09/03/2019
ms.topic: tutorial
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In this four-part tutorial series, you will use Python and linear regression in [SQL Server Machine Learning Services](../what-is-sql-server-machine-learning.md) to predict the number of ski rentals. The tutorial use a [Python notebook in Azure Data Studio](../../azure-data-studio/sql-notebooks.md), but you can also use your own Python integrated development environment (IDE).

Imagine you own a ski rental business and you want to predict the number of rentals that you'll have on a future date. This information will help you get your stock, staff, and facilities ready.

In the first part of this series, you'll get set up with the prerequisites. In parts two and three, you'll develop some Python scripts in a Jupyter notebook to prepare your data and train a machine learning model. Then, in part three, you'll run those Python scripts inside SQL Server using T-SQL stored procedures.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Import a sample database into SQL Server 

In [part two](python-ski-rental-linear-regression-prepare-data.md), you'll learn how to load the data from SQL Server into a Python data frame, and prepare the data in Python.

In [part three](python-ski-rental-linear-regression-train-model.md), you'll learn how to train a linear regression model in Python.

In [part four](python-ski-rental-linear-regression-deploy-model.md), you'll learn how to store the model to SQL Server, and then create stored procedures from the Python scripts you developed in parts two and three. The stored procedures will run in SQL Server to make predictions based on new data.

## Prerequisites

* SQL Server Machine Learning Services - For how to install Machine Learning Services, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](../../linux/sql-server-linux-setup-machine-learning.md?toc=%2Fsql%2Fadvanced-analytics%2Ftoc.json).

* Python IDE - This tutorial uses a Python notebook in [Azure Data Studio](../../azure-data-studio/what-is.md). For more information, see [How to use notebooks in Azure Data Studio](../../azure-data-studio/sql-notebooks.md). 

    You can also use your own Python IDE, such as a Jupyter notebook or [Visual Studio Code](https://code.visualstudio.com/docs) with the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python) and the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql). 

* [revoscalepy](../python/ref-py-revoscalepy.md) package - The **revoscalepy** package is included in SQL Server Machine Learning Services. To use the package on a client computer, see [Set up a data science client for Python development](../python/setup-python-client-tools-sql.md) for options to install this package locally.

    If you're using a Python notebook in Azure Data Studio, follow these additional steps to use **revoscalepy**:

    1. Open Azure Data Studio
    1. From the **File** menu, select **Preferences** and then **Settings**
    1. Expand **Extensions** and select **Notebook configuration**
    1. Under **Python Path**, enter the path where you installed the libraries (for example, `C:\path-to-python-for-mls`)
    1. Make sure **Use Existing Python** is checked
    1. Restart Azure Data Studio

    If you're using a different Python IDE, follow similar steps for your IDE.

* SQL query tool - This tutorial assumes you're using [Azure Data Studio](../../azure-data-studio/what-is.md). You can also use [SQL Server Management Studio](../../ssms/sql-server-management-studio-ssms.md) (SSMS).

* Additional Python packages - The examples in this tutorial series use Python packages that you may or may not have installed. Use the following **pip** commands to install these packages if necessary.

    ```console
    pip install pandas
    pip install sklearn
    pip install pickle
    ```

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

* Installed the prerequisites
* Import a sample database into an SQL Server

To prepare the data from the TutorialDB database, follow part two of this tutorial series:

> [!div class="nextstepaction"]
> [Python Tutorial: Prepare data to train a linear regression model](python-ski-rental-linear-regression-prepare-data.md)
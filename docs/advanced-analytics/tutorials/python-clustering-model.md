---
title: "Python tutorial: Categorize users"
description: In this four-part tutorial series, you'll  cluster customers, using K-Means, in a SQL database using Python with SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.devlang: python
ms.date: 12/17/2019
ms.topic: tutorial
author: garyericson
ms.author: garye
ms.reviewer: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Tutorial: Categorizing customers using k-means clustering with SQL Server Machine Learning Services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In this four-part tutorial series, you'll use Python to develop and deploy a K-Means clustering model in [SQL Server Machine Learning Services](../what-is-sql-server-machine-learning.md) to cluster customer data.

In part one of this series, you'll set up the prerequisites for the tutorial and then restore a sample dataset to a SQL database. Later in this series, you'll use this data to train and deploy a clustering model in Python with SQL Server Machine Learning Services.

In parts two and three of this series, you'll develop some Python scripts in an Azure Data Studio notebook to analyze and prepare your data and train a machine learning model. Then, in part four, you'll run those Python scripts inside a SQL database using stored procedures.

*Clustering* can be explained as organizing data into groups where members of a group are similar in some way. For this tutorial series, imagine you own a retail business. You'll use the **K-Means** algorithm to perform the clustering of customers in a dataset of product purchases and returns. By clustering customers, you can focus your marketing efforts more effectively by targeting specific groups.
K-Means clustering is an *unsupervised learning* algorithm that looks for patterns in data based on similarities.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Restore a sample database into a SQL Server instance

In [part two](python-clustering-model-prepare-data.md), you'll learn how to prepare the data from a SQL database to perform clustering.

In [part three](python-clustering-model-build.md), you'll learn how to create and train a K-Means clustering model in Python.

In [part four](python-clustering-model-deploy.md), you'll learn how to create a stored procedure in a SQL database that can perform clustering in Python based on new data.

## Prerequisites

* [SQL Server Machine Learning Services](../what-is-sql-server-machine-learning.md) with the Python language option - Follow the installation instructions in the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](https://docs.microsoft.com/sql/linux/sql-server-linux-setup-machine-learning?toc=%2fsql%2fadvanced-analytics%2ftoc.json&view=sql-server-linux-ver15).

* [Azure Data Studio](../../azure-data-studio/what-is.md). You'll use a notebook in Azure Data Studio for both Python and SQL. For more information about notebooks, see [How to use notebooks in Azure Data Studio](../../azure-data-studio/sql-notebooks.md).

  * Python - You can also use your own Python IDE, such as a Jupyter notebook or [Visual Studio Code](https://code.visualstudio.com/docs) with the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python) and the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql).
  * SQL - You can also use [SQL Server Management Studio](../../ssms/sql-server-management-studio-ssms.md) (SSMS).

* Additional Python packages - The examples in this tutorial series use Python packages that you may or may not have installed.

  Open a **Command Prompt** and change to the installation path for the version of Python you use in Azure Data Studio. For example, `cd %LocalAppData%\Programs\Python\Python37-32`. Then run the following commands to install any of these packages that are not already installed.

  ```console
  pip install matplotlib
  pip install pandas
  pip install pyodbc
  pip install scipy
  pip install sklearn
  ```

## Restore the sample database

The sample dataset used in this tutorial has been saved to a **.bak** database backup file for you to download and use. This dataset is derived from the [tpcx-bb](http://www.tpc.org/tpcx-bb/default.asp) dataset provided by the [Transaction Processing Performance Council (TPC)](http://www.tpc.org/default.asp).

1. Download the file [tpcxbb_1gb.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/tpcxbb_1gb.bak).

1. Follow the directions in [Restore a database from a backup file](../../azure-data-studio/tutorial-backup-restore-sql-server.md#restore-a-database-from-a-backup-file) in Azure Data Studio, using these details:

   * Import from the **tpcxbb_1gb.bak** file you downloaded
   * Name the target database "tpcxbb_1gb"

1. You can verify that the dataset exists after you have restored the database by querying the **dbo.customer** table:

    ```sql
    USE tpcxbb_1gb;
    SELECT * FROM [dbo].[customer];
    ```

## Clean up resources

If you're not going to continue with this tutorial, delete the tpcxbb_1gb database from SQL Server.

## Next steps

In part one of this tutorial series, you completed these steps:

* Restore a sample database into a SQL Server instance

To prepare the data for the machine learning model, follow part two of this tutorial series:

> [!div class="nextstepaction"]
> [Tutorial: Prepare data to perform clustering in Python with SQL Server Machine Learning Services](python-clustering-model-prepare-data.md)

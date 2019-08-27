---
title: "Tutorial: Perform clustering in Python"
description: In this four-part tutorial series, you'll perform clustering of customers in a SQL database using Python with SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.devlang: python
ms.date: 08/27/2019
ms.topic: tutorial
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# Tutorial: Perform clustering in Python with SQL Server Machine Learning Services

In this four-part tutorial series, you'll use Python to develop and deploy a K-Means clustering model in [SQL Server Machine Learning Services](../what-is-sql-server-machine-learning.md) to cluster customer data.

In part one of this series, you'll set up the prerequisites for the tutorial and then import a sample dataset to a SQL database. Later in this series, you'll use this data to train and deploy a clustering model in Python with SQL Server Machine Learning Services.

*Clustering* can be explained as organizing data into groups where members of a group are similar in some way.
You'll use the **K-Means** algorithm to perform the clustering of customers in a dataset of product purchases and returns. By clustering customers, you can focus your marketing efforts more effectively by targeting specific groups.
K-Means clustering is an *unsupervised learning* algorithm that looks for patterns in data based on similarities.

In parts two and three of this series, you'll develop some Python scripts in Visual Studio Code to prepare your data and train a machine learning model. Then, in part four, you'll run those Python scripts inside a SQL database using stored procedures.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Import a sample database into an Azure SQL database

In [part two](tutorial-python-clustering-model-prepare-data.md), you'll learn how to prepare the data from a SQL database to perform clustering.

In [part three](tutorial-python-clustering-model-build.md), you'll learn how to create and train a K-Means clustering model in Python.

In [part four](tutorial-python-clustering-model-deploy.md), you'll learn how to create a stored procedure in a SQL database that can perform clustering in Python based on new data.

## Prerequisites

* SQL Server Machine Learning Services with the Python language option - Follow the installation instructions in the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](https://docs.microsoft.com/sql/linux/sql-server-linux-setup-machine-learning?toc=%2fsql%2fadvanced-analytics%2ftoc.json&view=sql-server-linux-ver15).

* Python IDE - This tutorial uses a Python notebook in [Azure Data Studio](../../azure-data-studio/what-is.md). For more information, see [How to use notebooks in Azure Data Studio](../../azure-data-studio/sql-notebooks.md).

  You can also use your own Python IDE, such as a Jupyter notebook or [Visual Studio Code](https://code.visualstudio.com/docs) with the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python) and the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql).

* revoscalepy package - See [revoscalepy](../python/ref-py-revoscalepy.md) for options to install this package locally.

* SQL query tool - This tutorial assumes you're using [Azure Data Studio](../../azure-data-studio/what-is.md). You can also use [SQL Server Management Studio](../../ssms/sql-server-management-studio-ssms.md) (SSMS).

* Additional Python packages - Use the following Python script to install additional Python packages that are used by the Python examples in this tutorial series.

  ```python
  pip install matplotlib
  pip install scipy
  pip install sklearn
  ```

## Import the sample database

The sample dataset used in this tutorial has been saved to a **.bak** database backup file for you to download and use. This dataset is derived from the [tpcx-bb](http://www.tpc.org/tpcx-bb/default.asp) dataset provided by the [Transaction Processing Performance Council (TPC)](http://www.tpc.org/default.asp).

1. Download the file [tpcxbb_1gb.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/tpcxbb_1gb.bak) to the SQL Server backup folder. For the default database instance, the folder is:

   `C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\`

1. Open Azure Data Studio, connect to SQL Server, and open a new query window.

1. Run the following commands to restore the database.

   ```sql
   USE master;
   GO

   RESTORE DATABASE tpcxbb_1gb
   FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\tpcxbb_1gb.bak'
   WITH MOVE 'tpcxbb_1gb' TO 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\tpcxbb_1gb.mdf'
      , MOVE 'tpcxbb_1gb_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\tpcxbb_1gb.ldf';
   GO
   ```

## Clean up resources

***If you're not going to continue with this tutorial***, delete the tpcxbb_1gb database from your Azure SQL Database server.

From the Azure portal, follow these steps:

1. From the left-hand menu in the Azure portal, select **All resources** or **SQL databases**.
1. In the **Filter by name...** field, enter **tpcxbb_1gb**, and select your subscription.
1. Select your **tpcxbb_1gb** database.
1. On the **Overview** page, select **Delete**.

## Next steps

In part one of this tutorial series, you completed these steps:

* Import a sample database into an Azure SQL database

To prepare the data for the machine learning model, follow part two of this tutorial series:

> [!div class="nextstepaction"]
> [Tutorial: Prepare data to perform clustering in Python with SQL Server Machine Learning Services](tutorial-python-clustering-model-prepare-data.md)

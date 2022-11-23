---
title: "Python tutorial: Ski rentals"
titleSuffix: SQL machine learning
description: In this four-part tutorial series, you'll build a linear regression model in Python to predict ski rentals with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning
ms.date: 06/15/2022
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Python tutorial: Predict ski rental with linear regression with SQL machine learning
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In this four-part tutorial series, you will use Python and linear regression in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) or on [SQL Server 2019 Big Data Clusters](../../big-data-cluster/machine-learning-services.md) to predict the number of ski rentals. The tutorial uses a [Python notebook in Azure Data Studio](../../azure-data-studio/notebooks/notebooks-guidance.md).
::: moniker-end
::: moniker range="=sql-server-2017"
In this four-part tutorial series, you will use Python and linear regression in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) to predict the number of ski rentals. The tutorial uses a [Python notebook in Azure Data Studio](../../azure-data-studio/notebooks/notebooks-guidance.md).
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In this four-part tutorial series, you will use Python and linear regression in [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview) to predict the number of ski rentals. The tutorial uses a [Python notebook in Azure Data Studio](../../azure-data-studio/notebooks/notebooks-guidance.md).
::: moniker-end

Imagine you own a ski rental business and you want to predict the number of rentals that you'll have on a future date. This information will help you get your stock, staff, and facilities ready.

In the first part of this series, you'll get set up with the prerequisites. In parts two and three, you'll develop some Python scripts in a notebook to prepare your data and train a machine learning model. Then, in part three, you'll run those Python scripts inside the database using T-SQL stored procedures.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Import a sample database

In [part two](python-ski-rental-linear-regression-prepare-data.md), you'll learn how to load the data from a database into a Python data frame, and prepare the data in Python.

In [part three](python-ski-rental-linear-regression-train-model.md), you'll learn how to train a linear regression model in Python.

In [part four](python-ski-rental-linear-regression-deploy-model.md), you'll learn how to store the model in a database, and then create stored procedures from the Python scripts you developed in parts two and three. The stored procedures will run on the server to make predictions based on new data.

## Prerequisites

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
* SQL Server Machine Learning Services - To install Machine Learning Services, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](../../linux/sql-server-linux-setup-machine-learning.md?toc=%2Fsql%2Fmachine-learning%2Ftoc.json). You can also [enable Machine Learning Services on SQL Server 2019 Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017"
* SQL Server Machine Learning Services - To install Machine Learning Services, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md). 
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
* Azure SQL Managed Instance Machine Learning Services - For information, see the [Azure SQL Managed Instance Machine Learning Services overview](/azure/azure-sql/managed-instance/machine-learning-services-overview).

* [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) for restoring the sample database to Azure SQL Managed Instance.
::: moniker-end

* Python IDE - This tutorial uses a Python notebook in [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md). For more information, see [How to use notebooks in Azure Data Studio](../../azure-data-studio/notebooks/notebooks-guidance.md).

* SQL query tool - This tutorial assumes you're using [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md).

* Additional Python packages - The examples in this tutorial series use the following Python packages that may not be installed by default:

  * pandas
  * pyodbc
  * sklearn

  To install these packages:
  1. In your Azure Data Studio notebook, select **Manage Packages**.
  2. In the **Manage Packages** pane, select the **Add new** tab.
  3. For each of the following packages, enter the package name, select **Search**, then select **Install**.

  As an alternative, you can open a **Command Prompt**, change to the installation path for the version of Python you use in Azure Data Studio (for example, `cd %LocalAppData%\Programs\Python\Python37-32`), then run `pip install` for each package.

## Restore the sample database

The sample database used in this tutorial has been saved to a **.bak** database backup file for you to download and use.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
> [!NOTE]
> If you are using Machine Learning Services on SQL Server 2019 Big Data Clusters, see how to [Restore a database into the big data cluster master instance](../../big-data-cluster/data-ingestion-restore-database.md).
::: moniker-end

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
1. Download the file [TutorialDB.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/TutorialDB.bak).

1. Follow the directions in [Restore a database from a backup file](../../azure-data-studio/tutorial-backup-restore-sql-server.md#restore-a-database-from-a-backup-file) in Azure Data Studio, using these details:

   * Import from the `TutorialDB.bak` file you downloaded.
   * Name the target database `TutorialDB`.

1. You can verify that the restored database exists by querying the `dbo.rental_data` table:

   ```sql
   USE TutorialDB;
   SELECT * FROM [dbo].[rental_data];
   ```

::: moniker-end
::: moniker range="=azuresqldb-mi-current"
1. Download the file [TutorialDB.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/TutorialDB.bak).

1. Follow the directions in [Restore a database to Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-get-started-restore) in SQL Server Management Studio, using these details:

   * Import from the `TutorialDB.bak` file you downloaded.
   * Name the target database `TutorialDB`.

1. You can verify that the restored database exists by querying the `dbo.rental_data` table:

   ```sql
   USE TutorialDB;
   SELECT * FROM [dbo].[rental_data];
   ```
::: moniker-end

## Clean up resources

If you're not going to continue with this tutorial, delete the `TutorialDB` database.

## Next steps

In part one of this tutorial series, you completed these steps:

* Installed the prerequisites
* Import a sample database

To prepare the data from the TutorialDB database, follow part two of this tutorial series:

> [!div class="nextstepaction"]
> [Python Tutorial: Prepare data to train a linear regression model](python-ski-rental-linear-regression-prepare-data.md)
---
title: "Python tutorial: Categorize customers"
titleSuffix: SQL machine learning
description: In this four-part tutorial series, the goal is to cluster customers, using K-Means, in a database using Python with SQL machine learning.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: monamaki, randolphwest, anmunde
ms.date: 05/29/2024
ms.service: sql
ms.subservice: machine-learning
ms.topic: tutorial
ms.devlang: python
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Python tutorial: Categorizing customers using k-means clustering with SQL machine learning

[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In this four-part tutorial series, use Python to develop and deploy a K-Means clustering model in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) or on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md) to categorize customer data.
::: moniker-end
::: moniker range="=sql-server-2017"
In this four-part tutorial series, use Python to develop and deploy a K-Means clustering model in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) to cluster customer data.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In this four-part tutorial series, use Python to develop and deploy a K-Means clustering model in [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview) to cluster customer data.
::: moniker-end

In part one of this series, set up the prerequisites for the tutorial and then restore a sample dataset to a database. Later in this series, use this data to train and deploy a clustering model in Python with SQL machine learning.

In parts two and three of this series, develop some Python scripts in an Azure Data Studio notebook to analyze and prepare your data and train a machine learning model. Then, in part four, run those Python scripts inside a database using stored procedures.

*Clustering* can be explained as organizing data into groups where members of a group are similar in some way. For this tutorial series, imagine you own a retail business. Use the **K-Means** algorithm to perform the clustering of customers in a dataset of product purchases and returns. By clustering customers, you can focus your marketing efforts more effectively by targeting specific groups. K-Means clustering is an *unsupervised learning* algorithm that looks for patterns in data based on similarities.

In this article, learn how to:

> [!div class="checklist"]
> - Restore a sample database

In [part two](python-clustering-model-prepare-data.md), learn how to prepare the data from a database to perform clustering.

In [part three](python-clustering-model-build.md), learn how to create and train a K-Means clustering model in Python.

In [part four](python-clustering-model-deploy.md), learn how to create a stored procedure in a database that can perform clustering in Python based on new data.

## Prerequisites

::: moniker range=">=sql-server-ver16||>=sql-server-linux-ver16"
- [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) with the Python language option - Follow the installation instructions in the [Windows installation guide](../install/sql-machine-learning-services-windows-install-sql-2022.md) or the [Linux installation guide](../../linux/sql-server-linux-setup-machine-learning-sql-2022.md?toc=%252fsql%252fmachine-learning%252ftoc.json&view=sql-server-linux-ver16&preserve-view=true).
::: moniker-end
::: moniker range="=sql-server-ver15||=sql-server-linux-ver15"
- [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) with the Python language option - Follow the installation instructions in the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](../../linux/sql-server-linux-setup-machine-learning.md?toc=%252fsql%252fmachine-learning%252ftoc.json&view=sql-server-linux-ver15&preserve-view=true). You can also [enable Machine Learning Services on SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017"
- [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) with the Python language option - Follow the installation instructions in the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md).
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
- Azure SQL Managed Instance Machine Learning Services. For information, see the [Azure SQL Managed Instance Machine Learning Services overview](/azure/azure-sql/managed-instance/machine-learning-services-overview).

- [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) for restoring the sample database to Azure SQL Managed Instance.
::: moniker-end

- [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md). use a notebook in Azure Data Studio for both Python and SQL. For more information about notebooks, see [How to use notebooks in Azure Data Studio](../../azure-data-studio/notebooks/notebooks-guidance.md).

- Additional Python packages - The examples in this tutorial series use Python packages that you might or might not have installed.

  Open an **Administrative Command Prompt** and change to the installation path for the version of Python you use in Azure Data Studio. For example, `cd %LocalAppData%\Programs\Python\Python37-32`. Then run the following commands to install any of these packages that aren't already installed. Ensure these packages are installed in the correct Python installation location. You can use the option `-t` to specify the destination directory.

  ```console
  pip install matplotlib
  pip install pandas
  pip install pyodbc
  pip install scipy
  pip install scikit-learn
  ```

::: moniker range=">=sql-server-ver15"
  Run the following **icacls** commands to grant **READ & EXECUTE** access to the installed libraries to **SQL Server Launchpad Service** and SID **S-1-15-2-1 (ALL_APPLICATION_PACKAGES)**.

  ```cmd
    icacls "C:\Program Files\Python310\Lib\site-packages" /grant "NT Service\MSSQLLAUNCHPAD":(OI)(CI)RX /T
    icacls "C:\Program Files\Python310\Lib\site-packages" /grant *S-1-15-2-1:(OI)(CI)RX /T
  ```
::: moniker-end

## Restore the sample database

The sample dataset used in this tutorial has been saved to a **.bak** database backup file for you to download and use. This dataset is derived from the [tpcx-bb](https://www.tpc.org/tpcx-bb/default5.asp) dataset provided by the [Transaction Processing Performance Council (TPC)](https://www.tpc.org).

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
> [!NOTE]  
> If you are using Machine Learning Services on Big Data Clusters, see how to [Restore a database into the SQL Server big data cluster master instance](../../big-data-cluster/data-ingestion-restore-database.md).
::: moniker-end

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
1. Download the file [tpcxbb_1gb.bak](https://aka.ms/sqlmldocument/tpcxbb_1gb.bak).

1. Follow the directions in [Restore a database from a backup file](../../azure-data-studio/tutorial-backup-restore-sql-server.md#restore-a-database-from-a-backup-file) in Azure Data Studio, using these details:

   - Import from the `tpcxbb_1gb.bak` file you downloaded.
   - Name the target database `tpcxbb_1gb`.

1. You can verify that the dataset exists after you have restored the database by querying the `dbo.customer` table:

    ```sql
    USE tpcxbb_1gb;
    SELECT * FROM [dbo].[customer];
    ```

::: moniker-end
::: moniker range="=azuresqldb-mi-current"
1. Download the file [tpcxbb_1gb.bak](https://aka.ms/sqlmldocument/tpcxbb_1gb.bak).

1. Follow the directions in [Restore a database to a SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-get-started-restore) in SQL Server Management Studio, using these details:

   - Import from the `tpcxbb_1gb.bak` file you downloaded.
   - Name the target database `tpcxbb_1gb`.

1. You can verify that the dataset exists after you have restored the database by querying the `dbo.customer` table:

    ```sql
    USE tpcxbb_1gb;
    SELECT * FROM [dbo].[customer];
    ```

::: moniker-end

## Clean up resources

If you're not going to continue with this tutorial, delete the `tpcxbb_1gb` database.

## Next step

In part one of this tutorial series, you completed these steps:

- Restore a sample database

To prepare the data for the machine learning model, follow part two of this tutorial series:

> [!div class="nextstepaction"]
> [Python tutorial: Prepare data to perform clustering](python-clustering-model-prepare-data.md)

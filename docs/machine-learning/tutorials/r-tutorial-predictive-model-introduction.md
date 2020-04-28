---
title: "Tutorial: SQL Database predictive model in R"
titleSuffix: SQL Database Machine Learning Services
description: In this four-part tutorial series, you'll prepare the data to train a predictive model in R with  SQL machine learning services.
ms.prod: sql
ms.technology: machine-learning
ms.topic: tutorial
author: cawrites
ms.author: chadam
ms.custom: seo-lt-2019
ms.reviewer: garye
ms.date: 04/27/2020
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Tutorial: Prepare data to train a predictive model in R with SQL machine learning.
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]



::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
In this four-part tutorial series, you will use R and linear regression in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) or on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md) to predict the number of ski rentals. The tutorial use a [Notebook in Azure Data Studio](../../azure-data-studio/sql-notebooks.md).
::: moniker-end

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
In this four-part tutorial series, you will use R and linear regression in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) to predict the number of ski rentals. The tutorial use a [Notebook in Azure Data Studio](../../azure-data-studio/sql-notebooks.md).
::: moniker-end

Imagine you own a ski rental business and you want to predict the number of rentals that you'll have on a future date. This information will help you get your stock, staff, and facilities ready.

In the first part of this series, you'll get set up with the prerequisites. In parts two and three, you'll develop some R scripts in a notebook to prepare your data and train a machine learning model. Then, in part three, you'll run those R scripts inside SQL Server using T-SQL stored procedures.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Import a sample database into SQL Server 


In [part two](r-tutorial-predictive-model-prepare-data.md), you'll learn how to load the data from a database into a Python data frame, and prepare the data in R.

In [part three](r-tutorial-predictive-model-train.md), you'll learn how to train a linear regression model in R.

In [part four](r-tutorial-predictive-model-deploy.md), you'll learn how to store the model in a database, and then create stored procedures from the R scripts you developed in parts two and three. The stored procedures will run on the server to make predictions based on new data.


## Prerequisites

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
* SQL Server Machine Learning Services - For how to install Machine Learning Services, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](../../linux/sql-server-linux-setup-machine-learning.md?toc=%2Fsql%2Fmachine-learning%2Ftoc.json). You can also [enable Machine Learning Services on SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
* SQL Server Machine Learning Services - For how to install Machine Learning Services, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md). 
::: moniker-end

* R IDE - This tutorial uses [RStudio Desktop](https://www.rstudio.com/products/rstudio/download/).

* SQL query tool - This tutorial assumes you're using [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/what-is.md). For more information, see [How to use notebooks in Azure Data Studio](../../azure-data-studio/sql-notebooks.md)..

## Restore the sample database

The sample database used in this tutorial has been saved to a **.bak** database backup file for you to download and use.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
> [!NOTE]
> If you are using Machine Learning Services on Big Data Clusters, see how to [Restore a database into the SQL Server big data cluster master instance](../../big-data-cluster/data-ingestion-restore-database.md).
::: moniker-end

1. Download the file [TutorialDB.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/TutorialDB.bak).

1. Follow the directions in [Restore a database from a backup file](../../azure-data-studio/tutorial-backup-restore-sql-server.md#restore-a-database-from-a-backup-file) in Azure Data Studio, using these details:

   * Import from the **TutorialDB.bak** file you downloaded
   * Name the target database "TutorialDB"

1. You can verify that the restored database exists by querying the **dbo.rental_data** table:

   ```sql
   USE TutorialDB;
   SELECT * FROM [dbo].[rental_data];
   ```

1. Enable external scripts by running the following SQL commands:

    ```sql
    sp_configure 'external scripts enabled', 1;
    RECONFIGURE WITH override;
    ```

## Next steps

In part one of this tutorial series, you completed these steps:

* Installed the prerequisites
* Import a sample database into an SQL Server

To prepare the data for the machine learning model, follow part two of this tutorial series:

[!div class="nextstepaction"]
[R tutorial: Prepare data to train a predictive model in R](r-tutorial-predictive-model-prepare-data.md)

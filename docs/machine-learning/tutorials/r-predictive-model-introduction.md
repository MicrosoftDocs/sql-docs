---
title: "Tutorial: Develop predictive model in R"
titleSuffix: SQL machine learning
description: In this four-part tutorial series, you'll develop data to train a predictive model in R with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: garye, jroth
ms.date: 05/26/2020
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Tutorial: Develop a predictive model in R with SQL machine learning
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In this four-part tutorial series, you will use R and a machine learning model in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) or on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md) to predict the number of ski rentals.
::: moniker-end
::: moniker range="=sql-server-2017"
In this four-part tutorial series, you will use R and a machine learning model in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) to predict the number of ski rentals.
::: moniker-end
::: moniker range="=sql-server-2016"
In this four-part tutorial series, you will use R and a machine learning model in [SQL Server R Services](../r/sql-server-r-services.md) to predict the number of ski rentals.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In this four-part tutorial series, you will use R and a machine learning model in [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview) to predict the number of ski rentals.
::: moniker-end

Imagine you own a ski rental business and you want to predict the number of rentals that you'll have on a future date. This information will help you get your stock, staff, and facilities ready.

In the first part of this series, you'll get set up with the prerequisites. In parts two and three, you'll develop some R scripts in a notebook to prepare your data and train a machine learning model. Then, in part three, you'll run those R scripts inside a database using T-SQL stored procedures.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Restore a sample database 

In [part two](r-predictive-model-prepare-data.md), you'll learn how to load the data from a database into a Python data frame, and prepare the data in R.

In [part three](r-predictive-model-train.md), you'll learn how to train a machine learning model model in R.

In [part four](r-predictive-model-deploy.md), you'll learn how to store the model in a database, and then create stored procedures from the R scripts you developed in parts two and three. The stored procedures will run on the server to make predictions based on new data.

## Prerequisites

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
* SQL Server Machine Learning Services - To install Machine Learning Services, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](../../linux/sql-server-linux-setup-machine-learning.md?toc=%2Fsql%2Fmachine-learning%2Ftoc.json). You can also [enable Machine Learning Services on SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017"
* SQL Server Machine Learning Services - To install Machine Learning Services, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md). 
::: moniker-end
::: moniker range="=sql-server-2016"
* SQL Server 2016 R Services. To install R Services, see the [Windows installation guide](../install/sql-r-services-windows-install.md). 
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
* Azure SQL Managed Instance Machine Learning Services. For information, see the [Azure SQL Managed Instance Machine Learning Services overview](/azure/azure-sql/managed-instance/machine-learning-services-overview).

* [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) for restoring the sample database to Azure SQL Managed Instance.
::: moniker-end

* R IDE - This tutorial uses [RStudio Desktop](https://www.rstudio.com/products/rstudio/download/).

* RODBC - This driver is used in the R scripts you'll develop in this tutorial. If it's not already installed, install it using the R command `install.packages("RODBC")`. For more information on RODBC, see [CRAN - Package RODBC](https://CRAN.R-project.org/package=RODBC).

* SQL query tool - This tutorial assumes you're using [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md). For more information, see [How to use notebooks in Azure Data Studio](../../azure-data-studio/notebooks/notebooks-guidance.md).

## Restore the sample database

The sample database used in this tutorial has been saved to a **.bak** database backup file for you to download and use.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
> [!NOTE]
> If you are using Machine Learning Services on Big Data Clusters, see how to [Restore a database into the SQL Server big data cluster master instance](../../big-data-cluster/data-ingestion-restore-database.md).
::: moniker-end

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
1. Download the file [TutorialDB.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/TutorialDB.bak).

1. Follow the directions in [Restore a database from a backup file](../../azure-data-studio/tutorial-backup-restore-sql-server.md#restore-a-database-from-a-backup-file) in Azure Data Studio, using these details:

   * Import from the **TutorialDB.bak** file you downloaded
   * Name the target database "TutorialDB"

1. You can verify that the restored database exists by querying the **dbo.rental_data** table:

   ```sql
   USE TutorialDB;
   SELECT * FROM [dbo].[rental_data];
   ```
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
1. Download the file [TutorialDB.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/TutorialDB.bak).

1. Follow the directions in [Restore a database to a Managed Instance](/azure/sql-database/sql-database-managed-instance-get-started-restore) in SQL Server Management Studio, using these details:

   * Import from the **TutorialDB.bak** file you downloaded
   * Name the target database "TutorialDB"

1. You can verify that the restored database exists by querying the **dbo.rental_data** table:

   ```sql
   USE TutorialDB;
   SELECT * FROM [dbo].[rental_data];
   ```
::: moniker-end

## Clean up resources

If you're not going to continue with this tutorial, delete the TutorialDB database.
## Next steps

In part one of this tutorial series, you completed these steps:

* Installed the prerequisites
* Restored a sample database

To prepare the data for the machine learning model, follow part two of this tutorial series:

> [!div class="nextstepaction"]
> [Prepare data to train a predictive model in R](r-predictive-model-prepare-data.md)
---
title: "Tutorial: Prepare data to perform clustering in R"
titleSuffix: SQL Database Machine Learning Services
description: In part one of this four-part tutorial series, you'll prepare the data from a SQL database to perform clustering in R with SQL machine learning services.
ms.prod: sql
ms.technology: machine-learning
ms.topic: tutorial
author: cawrites
ms.author: chadam
ms.reviewer: garye
ms.date: 04/16/2020
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Tutorial: Prepare data to perform clustering in R with SQL machine learning services

In part one of this four-part tutorial series, you'll import and prepare the data from a SQL database using R. Later in this series, you'll use this data to train and deploy a clustering model in R with SQL machine learning.

*Clustering* can be explained as organizing data into groups where members of a group are similar in some way. For this tutorial series, imagine you own a retail business. You'll use the **K-Means** algorithm to perform the clustering of customers in a dataset of product purchases and returns. By clustering customers, you can focus your marketing efforts more effectively by targeting specific groups. K-Means clustering is an *unsupervised learning* algorithm that looks for patterns in data based on similarities.

In parts two and three of this series, you'll develop some R scripts in an Azure Data Studio notebook to analyze and prepare your data and train a machine learning model. Then, in part three, you'll run those R scripts inside a SQL database using stored procedures.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Import a sample database into an SQL database
> * Separate customers along different dimensions using R
> * Load the data from the SQL database into an R data frame

In [part two](r-tutorial-clustering-model-build-data.md), you'll learn how to create and train a K-Means clustering model in R.

In [part three](r-tutorial-clustering-model-deploy-data.md), you'll learn how to create a stored procedure in an Azure SQL database that can perform clustering in R based on new data.

## Prerequisites

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"


* [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) with the Python language option - Follow the installation instructions in the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](https://docs.microsoft.com/sql/linux/sql-server-linux-setup-machine-learning?toc=%2fsql%2fmachine-learning%2ftoc.json&view=sql-server-linux-ver15). You can also [enable Machine Learning Services on SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
* [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) with the Python language option - Follow the installation instructions in the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md).
::: moniker-end

* [Azure Data Studio](../../azure-data-studio/what-is.md). You'll use a notebook in Azure Data Studio for both Python and SQL. For more information about notebooks, see [How to use notebooks in Azure Data Studio](../../azure-data-studio/sql-notebooks.md)

* RevoScaleR package - See [RevoScaleR](https://docs.microsoft.com/sql/advanced-analytics/r/ref-r-revoscaler?view=sql-server-2017#versions-and-platforms) for options to install this package locally.

* R IDE - This tutorial uses [RStudio Desktop](https://www.rstudio.com/products/rstudio/download/).




## Import the sample database

The sample dataset used in this tutorial has been saved to a **.bak** database backup file for you to download and use. This dataset is derived from the [tpcx-bb](http://www.tpc.org/tpcx-bb/default.asp) dataset provided by the [Transaction Processing Performance Council (TPC)](http://www.tpc.org/default.asp).

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
> [!NOTE]
> If you are using Machine Learning Services on Big Data Clusters, see how to [Restore a database into the SQL Server big data cluster master instance](../../big-data-cluster/data-ingestion-restore-database.md).
::: moniker-end


1. Download the file [tpcxbb_1gb.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/tpcxbb_1gb.bak).

1. Follow the directions in [Restore a database from a backup file](../../azure-data-studio/tutorial-backup-restore-sql-server.md#restore-a-database-from-a-backup-file) in Azure Data Studio, using these details:

## Clean up resources

If you're not going to continue with this tutorial, delete the tpcxbb_1gb database.

## Next steps

In part one of this tutorial series, you completed these steps:

* Restore a sample database

To prepare the data for the machine learning model, follow part two of this tutorial series:

[!div class="nextstepaction"]

[R tutorial: Prepare data to perform clustering](r-clustering-model-prepare-data.md)


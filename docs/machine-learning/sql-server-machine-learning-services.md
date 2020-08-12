---
title: What is SQL Server Machine Learning Services (Python and R)?
titleSuffix: 
description: Machine Learning Services is a feature in SQL Server that gives the ability to run Python and R scripts with relational data. You can use open-source packages and frameworks, and the Microsoft Python and R packages for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server Machine Learning Services and how to get started.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 06/19/2020
ms.topic: overview
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# What is SQL Server Machine Learning Services (Python and R)?
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

Machine Learning Services is a feature in SQL Server that gives the ability to run Python and R scripts with relational data. You can use open-source packages and frameworks, and the [Microsoft Python and R packages](#packages), for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server Machine Learning Services and how to get started.

For machine learning on other SQL platforms, see the [SQL machine learning documentation](index.yml).

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
> [!NOTE]
> For executing Java in SQL Server, see the [Language Extensions documentation](../language-extensions/language-extensions-overview.md).
::: moniker-end

## Execute Python and R scripts in SQL Server

SQL Server Machine Learning Services lets you execute Python and R scripts in-database. You can use it to prepare and clean data, do feature engineering, and train, evaluate, and deploy machine learning models within a database. The feature runs your scripts where the data resides and eliminates transfer of the data across the network to another server.

You can execute Python and R scripts on a SQL Server instance with the stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

Base distributions of Python and R are included in Machine Learning Services. You can install and use open-source packages and frameworks, such as PyTorch, TensorFlow, and scikit-learn, in addition to the Microsoft packages.

Machine Learning Services uses an extensibility framework to run Python and R scripts in SQL Server. Learn more about how this works:

+ [Extensibility framework](concepts/extensibility-framework.md)
+ [Python extension](concepts/extension-python.md)
+ [R extension](concepts/extension-r.md)

## Get started with Machine Learning Services

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
1. [Install SQL Server Machine Learning Services on Windows](install/sql-machine-learning-services-windows-install.md) or [on Linux](../linux/sql-server-linux-setup-machine-learning.md?toc=/sql/machine-learning/toc.json). You can also use [Machine Learning Services on Big Data Clusters](../big-data-cluster/machine-learning-services.md).

1. Configure your development tools. You can use [run Python and R scripts in Azure Data Studio notebooks](install/sql-machine-learning-azure-data-studio.md). You can also run T-SQL in [Azure Data Studio](../azure-data-studio/what-is.md).

1. Write your first Python or R script.

    + [Python tutorials for SQL machine learning](tutorials/python-tutorials.md)
    + [R tutorials for SQL machine learning](tutorials/r-tutorials.md)
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
1. [Install SQL Server Machine Learning Services on Windows](install/sql-machine-learning-services-windows-install.md).

1. Configure your development tools. You can use [run Python and R scripts in Azure Data Studio notebooks](install/sql-machine-learning-azure-data-studio.md). You can also use T-SQL in [Azure Data Studio](../azure-data-studio/what-is.md).

1. Write your first Python or R script.

    + [Python tutorials for SQL machine learning](tutorials/python-tutorials.md)
    + [R tutorials for SQL machine learning](tutorials/r-tutorials.md)
::: moniker-end

<a name="versions"></a>

## Python and R versions

The following lists the versions of Python and R that are included in Machine Learning Services.

| SQL Server version | Python version | R version |
|-|-|-|
| SQL Server 2017 | 3.5.2 | 3.3.3 |
| SQL Server 2019 | 3.7.3 | 3.5.2 |

For the R version in SQL Server 2016, see the [R version section in What is R Services?](r/sql-server-r-services.md?view=sql-server-2016#version)

<a name="packages"></a>

## Python and R packages

You can use open-source packages and frameworks, in addition to Microsoft's enterprise packages. Most common open-source Python and R packages are pre-installed in Machine Learning Services. The following Python and R packages from Microsoft are also included:

| Language | Package | Description |
|-|-|-|
| Python | [revoscalepy](python/ref-py-revoscalepy.md) | The primary package for scalable Python. Data transformations and manipulation, statistical summarization, visualization, and many forms of modeling. Additionally, functions in this package automatically distribute workloads across available cores for parallel processing. |
| Python | [microsoftml](python/ref-py-microsoftml.md) | Adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis. | 
| R | [RevoScaleR](r/ref-r-revoscaler.md) | The primary package for scalable R. Data transformations and manipulation, statistical summarization, visualization, and many forms of modeling. Additionally, functions in this package automatically distribute workloads across available cores for parallel processing. |
| R | [MicrosoftML (R)](r/ref-r-microsoftml.md) | Adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis. |
| R | [olapR](r/ref-r-olapr.md) | R functions used for MDX queries against a SQL Server Analysis Services OLAP cube. |
| R | [sqlrutils](r/ref-r-sqlrutils.md) | A mechanism to use R scripts in a T-SQL stored procedure, register that stored procedure with a database, and run the stored procedure from an [R development environment](r/set-up-a-data-science-client.md). |
| R | [Microsoft R Open](https://mran.microsoft.com/rro) | Microsoft R Open (MRO) is the enhanced distribution of R from Microsoft. It is a complete open-source platform for statistical analysis and data science. It is based on and 100% compatible with R, and includes additional capabilities for improved performance and reproducibility. |

For more information on which packages are installed with Machine Learning Services and how to install other packages, see:

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
+ [Get Python package information](package-management/python-package-information.md)
+ [Install Python packages with sqlmlutils](package-management/install-additional-python-packages-on-sql-server.md)
+ [Get R package information](package-management/r-package-information.md)
+ [Install new R packages with sqlmlutils](package-management/install-additional-r-packages-on-sql-server.md).
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
+ [Get Python package information](package-management/python-package-information.md)
+ [Install packages with Python tools on SQL Server](package-management/install-python-packages-standard-tools.md)
+ [Get R package information](package-management/r-package-information.md)
+ [Use T-SQL (CREATE EXTERNAL LIBRARY) to install R packages on SQL Server](package-management/install-r-packages-with-tsql.md).
::: moniker-end

## Next steps

+ [Install SQL Server Machine Learning Services on Windows](install/sql-machine-learning-services-windows-install.md) or [on Linux](../linux/sql-server-linux-setup-machine-learning.md?toc=/sql/machine-learning/toc.json)
+ [Python tutorials for SQL machine learning](tutorials/python-tutorials.md)
+ [R tutorials for SQL machine learning](tutorials/r-tutorials.md)

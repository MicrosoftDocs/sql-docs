---
title: What is SQL Server Machine Learning Services (Python and R)?
titleSuffix: 
description: Machine Learning Services is a feature in SQL Server that gives the ability to run Python and R scripts with relational data. You can use open-source packages and frameworks, and the Microsoft Python and R packages for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.date: 08/07/2019
ms.topic: overview
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# What is SQL Server Machine Learning Services (Python and R)?
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Machine Learning Services is a feature in SQL Server that gives the ability to run Python and R scripts with relational data. You can use open-source packages and frameworks, and the [Microsoft Python and R packages](#packages) for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server Machine Learning Services.

In Azure SQL Database, [Machine Learning Services](https://docs.microsoft.com/azure/sql-database/sql-database-machine-learning-services-overview) is currently in public preview.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
> [!NOTE]
> For executing Java in SQL Server, see the [Language Extensions documentation](../language-extensions/language-extensions-overview.md).
::: moniker-end

## What is Machine Learning Services?

SQL Server Machine Learning Services lets you execute Python and R scripts in-database. You can use it to prepare and clean data, do feature engineering, and train, evaluate, and deploy machine learning models within a database. The feature runs your scripts where the data resides and eliminates transfer of the data across the network to another server.

Base distributions of Python and R are included in Machine Learning Services. You can install and use open-source packages and frameworks, such as PyTorch, TensorFlow, and scikit-learn, in addition to the Microsoft packages [revoscalepy](python/ref-py-revoscalepy.md) and [microsoftml](python/ref-py-microsoftml.md) for Python, and [RevoScaleR](r/ref-r-revoscaler.md), [MicrosoftML](r/ref-r-microsoftml.md), [olapR](r/ref-r-olapr.md), and [sqlrutils](r/ref-r-sqlrutils.md) for R.

Machine Learning Services uses an extensibility framework to run Python and R scripts in SQL Server. Learn more about how this works:

+ [Extensibility framework](concepts/extensibility-framework.md)
+ [Python extension](concepts/extension-python.md)
+ [R extension](concepts/extension-r.md)

## What can I do with Machine Learning Services?

You can use Machine Learning Services to build and training machine learning and deep learning models within SQL Server. You can also deploy existing models to Machine Learning Services and use relational data for predictions.

Examples of the type of predictions that you can use SQL Server Machine Learning Services for, include:

|||
|-|-|
|Classification/Categorization|Automatically divide customer feedback into positive and negative categories|
|Regression/Predict continuous values|Predict the price of houses based on size and location|
|Anomaly Detection|Detect fraudulent banking transactions |
|Recommendations|Suggest products that online shoppers may want to buy, based on their previous purchases|

### How to execute Python and R scripts

There are two ways to execute Python and R scripts in Machine Learning Services:

+ The most common way is to use the T-SQL stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

+ You can also use your preferred Python or R client and write scripts that push the execution (referred to as a *remote compute context*) to a remote SQL Server. See how to set up a data science client for [Python development](python/setup-python-client-tools-sql.md) and [R development](r/set-up-a-data-science-client.md) for more information.

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

+ [Get Python package information](package-management/python-package-information.md)
+ [Install Python packages with sqlmlutils](package-management/install-additional-python-packages-on-sql-server.md)
+ [Get R package information](package-management/r-package-information.md)
+ [Install new R packages with sqlmlutils](package-management/install-additional-r-packages-on-sql-server.md).

## How do I get started with Machine Learning Services?

1. [Install SQL Server Machine Learning Services](install/sql-machine-learning-services-windows-install.md)

1. Configure your development tools. You can use:

    + [Azure Data Studio](../azure-data-studio/what-is.md) or [SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-ssms.md) to use T-SQL and the stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to execute your Python or R script.
    + Python or R on your own development laptop or workstation to execute scripts. You can either pull data down locally or push the execution remotely to SQL Server with [revoscalepy](python/ref-py-revoscalepy.md) and [RevoScaleR](r/ref-r-revoscaler.md). See how to set up a data science client for [Python development](python/setup-python-client-tools-sql.md) and [R development](r/set-up-a-data-science-client.md) for more information.

1. Write your first Python or R script

    + Quickstart: [Create and run simple R scripts in SQL](tutorials/quickstart-r-create-script.md)
    + Quickstart: [Create and train a predictive model in R](tutorials/quickstart-r-train-score-model.md)
    + Tutorial: [Use Python in T-SQL](tutorials/sqldev-in-database-python-for-sql-developers.md): Explore data, perform feature engineering, train and deploy models, and make predictions (five-part series)
    + Tutorial: [Use R in T-SQL](tutorials/sqldev-in-database-r-for-sql-developers.md): Explore data, perform feature engineering, train and deploy models, and make predictions (five-part series)
    + Tutorial: [Use Machine Learning Services in R tools](tutorials/walkthrough-data-science-end-to-end-walkthrough.md): Explore data, create graphs and plots, perform feature engineering, train and deploy models, and make predictions (six-part series)

## Next steps

+ [Install SQL Server Machine Learning Services](install/sql-machine-learning-services-windows-install.md)
+ Set up a data science client for [Python development](python/setup-python-client-tools-sql.md) and [R development](r/set-up-a-data-science-client.md)

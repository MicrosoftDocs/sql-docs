---
title: What is SQL Server 2016 R Services?
titleSuffix: 
description: R Services is a feature in SQL Server 2016 that gives the ability to run R scripts with relational data. You can use open-source packages and frameworks, and the Microsoft R packages for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server R Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 09/16/2021
ms.topic: overview
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=sql-server-2016"
ms.custom:
  - intro-overview
---
# What is SQL Server 2016 R Services?

[!INCLUDE[SQL Server 2016 only](../../includes/applies-to-version/sqlserver2016-only.md)]

R Services is a feature in SQL Server 2016 that gives the ability to run R scripts with relational data. You can use open-source packages and frameworks, and the [Microsoft R packages](#packages) for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server R Services.

> [!Note]
> R Services was renamed to [Machine Learning Services](../sql-server-machine-learning-services.md) in SQL Server 2017 and later, and supports both Python and R.

## What is R Services?

SQL Server R Services lets you execute R scripts in-database. You can use it to prepare and clean data, do feature engineering, and train, evaluate, and deploy machine learning models within a database. The feature runs your scripts where the data resides and eliminates transfer of the data across the network to another server.

Base distributions of R are included in R Services. You can use open-source packages and frameworks in addition to the Microsoft packages [RevoScaleR](../r/ref-r-revoscaler.md), [MicrosoftML](../r/ref-r-microsoftml.md), [olapR]../r/ref-r-olapr.md), and [sqlrutils](../r/ref-r-sqlrutils.md) for R.

R Services uses an extensibility framework to run R scripts in SQL Server. Learn more about how this works:

+ [Extensibility framework](../concepts/extensibility-framework.md)
+ [R extension](../concepts/extension-r.md)

## What can I do with R Services?

You can use R Services to build and training machine learning and deep learning models within SQL Server. You can also deploy existing models to R Services and use relational data for predictions.

Examples of the type of predictions that you can use SQL Server R Services for, include:

|Prediction type|Example|
|-|-|
|Classification/Categorization|Automatically divide customer feedback into positive and negative categories|
|Regression/Predict continuous values|Predict the price of houses based on size and location|
|Anomaly Detection|Detect fraudulent banking transactions |
|Recommendations|Suggest products that online shoppers may want to buy, based on their previous purchases|

### How to execute R scripts

There are two ways to execute R scripts in R Services:

+ The most common way is to use the T-SQL stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

+ You can also use your preferred R client and write scripts that push the execution (referred to as a *remote compute context*) to a remote SQL Server. See how to [set up a data science client R development](../r/set-up-data-science-client.md) for more information.

<a name="version"></a>

## R versions

The following lists the versions of the R runtime that are included in SQL Server 2016 R Services.

SQL Server version | Default R runtime versions |
|-|-|
| SQL Server 2016 RTM - SP2 CU13 | 3.2.2 |
| SQL Server 2016 SP2 CU14 and later | 3.2.2 and 3.5.2 |

Cumulative Update (CU) 14 for SQL Server 2016 Service Pack (SP) 2 and later include newer R runtimes. For more information, see [Change the default language runtime version](../install/change-default-language-runtime-version.md).

For other versions of R, or to run Python, use [Machine Learning Services for SQL Server 2017 and later](../sql-server-machine-learning-services.md).

<a name="packages"></a>

## R packages

You can use open-source packages and frameworks, in addition to Microsoft's enterprise packages. Most common open-source R packages are pre-installed in R Services. The following R packages from Microsoft are also included:

| Package | Description |
|-|-|
| [RevoScaleR](../r/ref-r-revoscaler.md) | The primary package for scalable R. Data transformations and manipulation, statistical summarization, visualization, and many forms of modeling. Additionally, functions in this package automatically distribute workloads across available cores for parallel processing. |
| [MicrosoftML (R)](../r/ref-r-microsoftml.md) | Adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis. |
| [olapR](../r/ref-r-olapr.md) | R functions used for MDX queries against a SQL Server Analysis Services OLAP cube. |
| [sqlrutils](../r/ref-r-sqlrutils.md) | A mechanism to use R scripts in a T-SQL stored procedure, register that stored procedure with a database, and run the stored procedure from an [R development environment](../r/set-up-data-science-client.md). |
| [Microsoft R Open](https://mran.microsoft.com/rro) | Microsoft R Open (MRO) is the enhanced distribution of R from Microsoft. It is a complete open-source platform for statistical analysis and data science. It is based on and 100% compatible with R, and includes additional capabilities for improved performance and reproducibility. |

## How do I get started with R Services?

1. [Install SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)

1. Configure your development tools. You can use:

    + [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md) or [SQL Server Management Studio (SSMS)](../../ssms/sql-server-management-studio-ssms.md) to use T-SQL and the stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to execute your R script.
    + R on your own development laptop or workstation to execute scripts. You can either pull data down locally or push the execution remotely to SQL Server with [RevoScaleR](../r/ref-r-revoscaler.md). See how to [set up a data science client R development](../r/set-up-data-science-client.md) for more information.

1. Write your first R script

    + Quickstart: [Create and run simple R scripts in SQL Server](../tutorials/quickstart-r-create-script.md)
    + Quickstart: [Create and train a predictive model in R](../tutorials/quickstart-r-train-score-model.md)
    + Tutorial: [Use R in T-SQL](../tutorials/r-taxi-classification-introduction.md): Explore data, perform feature engineering, train and deploy models, and make predictions (five-part series)
    + Tutorial: [Use R Services in R tools](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md): Explore data, create graphs and plots, perform feature engineering, train and deploy models, and make predictions (six-part series)

## Next steps

+ [Install SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)
+ [Set up a data science client for R development](../r/set-up-data-science-client.md)

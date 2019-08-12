---
title: What is SQL Server 2016 R Services?
titleSuffix: 
description: R Services is a feature in SQL Server 2016 that gives the ability to run R scripts with relational data. You can use open-source packages and frameworks, and the Microsoft R packages for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server R Services.
ms.prod: sql
ms.technology: machine-learning
ms.date: 08/12/2019 
ms.topic: overview
author: dphansen
ms.author: davidph
monikerRange: "=sql-server-2016||=sqlallproducts-allversions"
---
# What is SQL Server 2016 R Services?
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

R Services is a feature in SQL Server 2016 that gives the ability to run R scripts with relational data. You can use open-source packages and frameworks, and the [Microsoft R packages](#packages) for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server R Services.

> [!Note]
> R Services was renamed to [SQL Server Machine Learning Services](../what-is-sql-server-machine-learning.md) in SQL Server 2017 and later, when Python was added.

## What is R Services?

SQL Server R Services lets you execute R scripts in-database. You can use it to prepare and clean data, do feature engineering, and train, evaluate, and deploy machine learning models within a database. The feature runs your scripts where the data resides and eliminates transfer of the data across the network to another server.

Base distributions of R are included in Machine Learning Services. You can use open-source packages and frameworks in addition to the Microsoft packages [RevoScaleR](r/ref-r-revoscaler.md), [MicrosoftML](r/ref-r-microsoftml.md), [olapR](r/ref-r-olapr.md), and [sqlrutils](r/ref-r-sqlrutils.md) for R.

Machine Learning Services uses an extensibility framework to run R scripts in SQL Server. Learn more about how this works:

+ [Extensibility framework](concepts/extensibility-framework.md)
+ [R extension](concepts/extension-r.md)

Microsoft packages [revoscalepy](python/ref-py-revoscalepy.md) and [microsoftml](python/ref-py-microsoftml.md) for Python, and [RevoScaleR](r/ref-r-revoscaler.md), [MicrosoftML](r/ref-r-microsoftml.md), [olapR](r/ref-r-olapr.md), and [sqlrutils](r/ref-r-sqlrutils.md) for R.

## What can I do with R Services?

You can use R Services to build and training machine learning and deep learning models within SQL Server. You can also deploy existing models to R Services and use relational data for predictions.

Examples of the type of predictions that you can use SQL Server R Services for, include:

|||
|-|-|
|Classification/Categorization|Automatically divide customer feedback into positive and negative categories|
|Regression/Predict continuous values|Predict the price of houses based on size and location|
|Anomaly Detection|Detect fraudulent banking transactions |
|Recommendations|Suggest products that online shoppers may want to buy, based on their previous purchases|


## Components

SQL Server 2016 is R only. The following table describes the features in SQL Server 2016.

| Component | Description |
|-----------|-------------|
| SQL Server Launchpad service | A service that manages communications between the external R runtimes and the SQL Server instance. |
| R packages | [**RevoScaleR**](ref-r-revoscaler.md) is the primary library for scalable R. Functions in this library are among the most widely used. Data transformations and manipulation, statistical summarization, visualization, and many forms of modeling and analyses are found in these libraries. Additionally, functions in these libraries automatically distribute workloads across available cores for parallel processing, with the ability to work on chunks of data that are coordinated and managed by the calculation engine.  <br/>[**MicrosoftML (R)**](ref-r-microsoftml.md) adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis. <br/>[**sqlRUtils**](ref-r-sqlrutils.md) provides helper functions for putting R scripts into a T-SQL stored procedure, registering a stored procedure with a database, and running the stored procedure from an R development environment.<br/>[**olapR**](ref-r-olapr.md) is for specifying MDX queries in R.|
| Microsoft R Open (MRO) | [**MRO**](https://mran.microsoft.com/open) is Microsoft's open-source distribution of R. The package and interpreter are included. Always use the version of MRO installed by Setup. |
| R tools | R console windows and command prompts are standard tools in an R distribution.  |
| R Samples and scripts |  Open-source R and RevoScaleR packages include built-in data sets so that you can create and run script using pre-installed data |
| Pre-trained models in R | Pre-trained models are created for specific use cases and maintained by the data science engineering team at Microsoft. You can use the pre-trained models as-is to score positive-negative sentiment in text, or detect features in images, using new data inputs that you provide. The models run in R Services, but cannot be installed through SQL Server Setup. For more information, see [Install pre-trained machine learning models on SQL Server](../install/sql-pretrained-models-install.md). |

## Using R Services

Developers and analysts often have code running on top of a local SQL Server instance. By adding Machine Learning Services and enabling external script execution, you gain the ability to run R code in SQL Server modalities: wrapping script in stored procedures, storing models in a SQL Server table, or combining T-SQL and R functions in queries.

The most common approach for in-database analytics is to use [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), passing R script as an input parameter.

Classic client-server interactions are another approach. From any client workstation that has an IDE, you can install [Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client), and then write code that pushes execution (referred to as a *remote compute context*) to data and operations to a remote SQL Server. 

Finally, if you are using a [standalone server](r-server-standalone.md) and the Developer edition, you can build solutions on a client workstation using the same libraries and interpreters, and then deploy production code on SQL Server Machine Learning Services (In-Database). 

## How to get started

Start with setup, attach the binaries to your favorite development tool, and write your first script.

**Step 1:** Install and configure the software. 

+ [Install SQL Server 2016 R Services (In-Database)](../install/sql-r-services-windows-install.md)

**Step 2:** Gain hands-on experience using either one of these tutorials:

+ [Tutorial: Learn in-database analytics using R](../tutorials/sqldev-in-database-r-for-sql-developers.md)
+ [Tutorial: End-to-end walkthrough with R](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md)

**Step 3:** Add your favorite R packages and use them together with packages provided by Microsoft

+ [R Package management for SQL Server](install-additional-r-packages-on-sql-server.md)


## See also

 [Install SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)

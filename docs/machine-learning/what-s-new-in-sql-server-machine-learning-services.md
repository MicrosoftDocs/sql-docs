---
title: What's new in SQL Server Machine Learning Services?
titleSuffix: 
description: New feature announcements for each release of SQL Server Machine Learning Services and SQL Server 2016 R Services.
ms.date: 05/24/2022
ms.topic: overview
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
- sqlseattle
- intro-whats-new
- event-tier1-build-2022
ms.service: sql
ms.subservice: machine-learning-services
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# What's new in SQL Server Machine Learning Services?
[!INCLUDE [SQL Server 2016 and later](../includes/applies-to-version/sqlserver2016.md)]

This articles describes what new capabilities and features are included in each version of [SQL Server Machine Learning Services](sql-server-machine-learning-services.md). Machine learning capabilities are added to SQL Server in each release as we continue to expand, extend, and deepen the integration between the data platform, advanced analytics, and data science. 

> [!NOTE]
> Feature capabilities and installation options vary between versions of SQL Server. Use the version selector dropdown to choose the appropriate version of SQL Server.

::: moniker range=">=sql-server-ver16||>=sql-server-linux-ver16"

## New in SQL Server 2022

 Beginning with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], runtimes for R, Python, and Java, are no longer installed with SQL Setup. Instead, install any desired custom runtime(s) and packages. For more information, see [Install SQL Server 2022 Machine Learning Services (Python and R) on Windows](install/sql-machine-learning-services-windows-install-sql-2022.md) or [Install SQL Server Machine Learning Services (Python and R) on Linux](../linux/sql-server-linux-setup-machine-learning.md).

::: moniker-end

::: moniker range="=sql-server-ver15||=sql-server-linux-ver15"
## New in SQL Server 2019

This release adds the top-requested features for Python and R machine learning operations in SQL Server. For more information about all of the features in this release, see [What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-2019.md) and [Release Notes for SQL Server 2019](../sql-server/sql-server-2019-release-notes.md).

For the what's new documentation on Java and C# in SQL Server 2019, see the [What's new in SQL Server Language Extensions?](../language-extensions/language-extensions-whats-new.md).

Below are the new features for SQL Server Machine Learning Services, available on both **Windows** and **Linux**:

- Linux platform support was added in Machine Learning Services for Python and R. Get started with [Install SQL Server Machine Learning Services on Linux](../linux/sql-server-linux-setup-machine-learning.md).
- [Loopback connection to SQL Server from a Python or R script](connect/loopback-connection.md). 
- [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md) for Python and R.
- The [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) introduces two new parameters that enable you to easily generate multiple models from partitioned data. Learn more in this tutorial, [Create partition-based models in R](tutorials/r-tutorial-create-models-per-partition.md).
- Failover cluster support is available for the Launchpad service, assuming SQL Server Launchpad service is started on all nodes. For more information, see [SQL Server failover cluster installation](../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md).
- Isolation mechanism changes for Machine Learning Services. For more information, see [SQL Server 2019 on Windows: Isolation changes for Machine Learning Services](install/sql-server-machine-learning-services-2019.md).

::: moniker-end

::: moniker range=">=sql-server-2017"
## New in SQL Server 2017

This release adds [Python support and industry-leading machine learning algorithms](https://cloudblogs.microsoft.com/sqlserver/2017/04/19/python-in-sql-server-2017-enhanced-in-database-machine-learning/). Renamed to reflect the new scope, SQL Server 2017 marks the introduction of [SQL Server Machine Learning Services (In-Database)](sql-server-machine-learning-services.md), with language support for both Python and R. 

For feature announcements all-up, see [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md).

### R enhancements

The R component of SQL Server Machine Learning Services is the next generation of SQL Server 2016 R Services, with updated versions of base R, RevoScaler, and other packages.

New capabilities for R include [**package management**](package-management/install-r-packages-with-tsql.md), with the following highlights: 

+ Database roles help DBAs manage packages and assign permissions for package installation.
+ [CREATE EXTERNAL LIBRARY](../t-sql/statements/create-external-library-transact-sql.md) helps DBAs manage packages in the familiar T-SQL language.
+ [RevoScaleR](package-management/install-r-packages-with-revoscaler.md) functions help install, remove, or list packages owned by users. For more information, see [How to use RevoScaleR functions to find or install R packages on SQL Server](package-management/install-r-packages-with-revoscaler.md).

### R libraries

| Package | Description |
|---------|-------------|
| [**MicrosoftML**](r/ref-r-microsoftml.md) | In this release, MicrosoftML is included in a default R installation, eliminating the upgrade step required in the previous SQL Server 2016 R Services. MicrosoftML provides state-of-the-art machine learning algorithms and data transformations that can be scaled or run in remote compute contexts. Algorithms include customizable deep neural networks, fast decision trees and decision forests, linear regression, and logistic regression.  |

### Python integration for in-database analytics

Python is a language that offers great flexibility and power for a variety of machine learning tasks. Open-source libraries for Python include several platforms for customizable neural networks, as well as popular libraries for natural language processing. 

Because Python is integrated with the database engine, you can keep analytics close to the data and eliminate the costs and security risks associated with data movement. You can deploy machine learning solutions based on Python using tools like Visual Studio. Your production applications can get predictions, models, or visuals from the Python 3.5 runtime using SQL Server data access methods.

T-SQL and Python integration is supported through the [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) system stored procedure. You can call any Python code using this stored procedure. Code runs in a secure, dual architecture that enables enterprise-grade deployment of Python models and scripts, callable from an application using a simple stored procedure. Additional performance gains are achieved by streaming data from SQL to Python processes and MPI ring parallelization.

You can use the T-SQL [PREDICT](../t-sql/queries/predict-transact-sql.md) function to perform [native scoring](predictions/native-scoring-predict-transact-sql.md) on a pre-trained model that has been previously saved in the required binary format.

### Python libraries

| Package | Description |
|---------|-------------|
|[**revoscalepy**](python/ref-py-revoscalepy.md)| Python-equivalent of RevoScaleR. You can create Python models for linear and logistic regressions, decision trees, boosted trees, and random forests, all parallelizable, and capable of being run in remote compute contexts. This package supports use of multiple data sources and remote compute contexts. The data scientist or developer can execute Python code on a remote SQL Server, to explore data or build models without moving data. |
|[**microsoftml**](python/ref-py-microsoftml.md) |Python-equivalent of the MicrosoftML R package. |

### Pre-trained models

[**Pre-trained models**](install/sql-pretrained-models-install.md) are available for both Python and R. Use these models for image recognition and positive-negative sentiment analysis, to generate predictions on your own data. 

### Standalone Server as a shared feature in SQL Server Setup

This release also adds [SQL Server Machine Learning Server (Standalone)](r/r-server-standalone.md), a fully independent data science server, supporting statistical and predictive analytics in R and Python. As with R Services, this server is the next version of SQL Server 2016 R Server (Standalone). With the standalone server, you can distribute and scale R or Python solutions with no dependencies on SQL Server.
::: moniker-end

::: moniker range=">=sql-server-2016"
## New in SQL Server 2016

This release introduced machine learning capabilities into SQL Server through **SQL Server 2016 R Services**, an in-database analytics engine for processing R script on resident data within a database engine instance.

Additionally, **SQL Server 2016 R Server (Standalone)** was released as a way to install R Server on a Windows server. Initially, SQL Server Setup provided the only way to install R Server for Windows. In later releases, developers and data scientists who wanted R Server on Windows could use another standalone installer to achieve the same goal. The standalone server in SQL Server is functionally equivalent to the standalone server product, [Microsoft R Server for Windows](/machine-learning-server/install/r-server-install-windows).

For feature announcements all-up, see [What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md).

| Release |Feature update |
|---------|----------------|
| CU additions | [**Real-time scoring**](predictions/real-time-scoring.md) relies on native C++ libraries to read a model stored in an optimized binary format, and then generate predictions without having to call the R runtime. This makes scoring operations much faster. With real-time scoring, you can run a stored procedure or perform real-time scoring from R code. Real-time scoring is also available for SQL Server 2016, if the instance is upgraded to the latest release of [!INCLUDE[rsql-platform-md](../includes/rsql-platform-md.md)]. |
| Initial release | [**R integration for in-database analytics**](r/sql-server-r-services.md). <br/><br/> R packages for calling R functions in T-SQL, and vice versa. RevoScaleR functions provide R analytics at scale by chunking data into component parts, coordinating and managing distributed processing, and aggregating results. In SQL Server 2016 R Services (In-Database), the RevoScaleR engine is integrated with a database engine instance, brining data and analytics together in the same processing context. <br/><br/>T-SQL and R integration through [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). You can call any R code using this stored procedure. This secure infrastructure enables enterprise-grade deployment of Rn models and scripts that can be called from an application using a simple stored procedure. Additional performance gains are achieved by streaming data from SQL to R processes and MPI ring parallelization. <br/><br/>You can use the T-SQL [PREDICT](../t-sql/queries/predict-transact-sql.md) function to perform [native scoring](predictions/native-scoring-predict-transact-sql.md) on a pre-trained model that has been previously saved in the required binary format.|

::: moniker-end

::: moniker range=">=sql-server-2017"
## Linux support

SQL Server 2019 adds Linux support for R and Python when you install the machine learning packages with a database engine instance. For more information, see [Install SQL Server Machine Learning Services on Linux](../linux/sql-server-linux-setup-machine-learning.md).

On Linux, SQL Server 2017 does not have R or Python integration, but you can use [Native scoring](predictions/native-scoring-predict-transact-sql.md) on Linux because that functionality is available through T-SQL [PREDICT](../t-sql/queries/predict-transact-sql.md), which runs on Linux. Native scoring enables high-performance scoring from a pretrained model, without calling or even requiring an R runtime.
::: moniker-end

## Next steps

+ [Install SQL Server Machine Learning Services (In-Database)](install/sql-machine-learning-services-windows-install.md)
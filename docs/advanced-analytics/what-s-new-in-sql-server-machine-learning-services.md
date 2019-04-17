---
title: What's new - SQL Server Machine Learning Services | Microsoft Docs
description: New feature announcements for each release of SQL Server 2016 R Services, R Server, SQL Server 2017 Machine Learning Services.
ms.date: 03/29/2019
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
ms.custom: sqlseattle
ms.prod: sql
ms.technology: machine-learning
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---
# What's new in SQL Server Machine Learning Services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Machine learning capabilities are added to SQL Server in each release as we continue to expand, extend, and deepen the integration between the data platform, advanced analytics, and data science. 

::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
## New in SQL Server 2019 preview

This release adds the top-requested features for R and Python machine learning operations in SQL Server. For more information about all of the features in this release, see [What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-ver15.md) and [Release Notes for SQL Server 2019](../sql-server/sql-server-ver15-release-notes.md).

| Release | Feature update |
|---------|----------------|
| CTP 2.4 | Linux support for [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md) for R, Python, and Java. |
| | The environment variable that specifies the location of the Java interpreter has changed from `JAVA_HOME` to `JRE_HOME`. |
| CTP 2.3 | New supported [Java data types](java/java-sql-datatypes.md). |
| | On Windows only, Java code can be accessed in an external library using the [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md) statement. Equivalent functionality will be available on Linux in an upcoming CTP. Learn more: [How to call Java from SQL Server](java/howto-call-java-from-sql.md). |
| | On Windows only, Python code can be accessed in an external library using the [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md) statement. Equivalent functionality will be available on Linux in an upcoming CTP. |
| CTP 2.2 | No changes. |
| CTP 2.1 | No changes. |
| CTP 2.0 | Linux platform support for R and Python machine learning. Get started with [Install SQL Server Machine Learning Services on Linux](../linux/sql-server-linux-setup-machine-learning.md). |
|   | [Java language extension](java/extension-java.md) on both Windows and Linux is new in SQL Server 2019 preview. You can make compiled Java code available to SQL Server by assigning permissions and setting the path. Client apps with access SQL Server can use data and run your code by calling [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql), the same procedure used for R and Python integration on SQL Server. | 
|  | The [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) introduces two new parameters that enable you to easily generate multiple models from partitioned data. Learn more in this tutorial, [Create partition-based models in R](tutorials/r-tutorial-create-models-per-partition.md). |
|   | Failover cluster support is now supported on Windows and Linux, assuming SQL Server Launchpad service is started on all nodes. For more information, see [SQL Server failover cluster installation](../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md). |

::: moniker-end

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"
## New in SQL Server 2017

This release adds [Python support and industry-leading machine learning algorithms](https://cloudblogs.microsoft.com/sqlserver/2017/04/19/python-in-sql-server-2017-enhanced-in-database-machine-learning/). Renamed to reflect the new scope, SQL Server 2017 marks the introduction of [SQL Server Machine Learning Services (In-Database)](what-is-sql-server-machine-learning.md), with language support for both Python and R. 

For feature announcements all-up, see [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md).

### R enhancements

The R component of SQL Server 2017 Machine Learning Services is the next generation of SQL Server 2016 R Services, with updated versions of base R, RevoScaler, and other packages.

New capabilities for R include [**package management**](r/install-additional-r-packages-on-sql-server.md), with the following highlights: 

+ Database roles help DBAs manage packages and assign permissions for package installation.
+ [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql) helps DBAs manage packages in the familiar T-SQL language.
+ [RevoScaleR](r/use-revoscaler-to-manage-r-packages.md) functions help install, remove, or list packages owned by users. For more information, see [How to use RevoScaleR functions to find or install R packages on SQL Server](r/use-revoscaler-to-manage-r-packages.md).

### R libraries

| Package | Description |
|---------|-------------|
| [**MicrosoftML**](r/ref-r-microsoftml.md) | In this release, MicrosoftML is included in a default R installation, eliminating the upgrade step required in the previous SQL Server 2016 R Services. MicrosoftML provides state-of-the-art machine learning algorithms and data transformations that can be scaled or run in remote compute contexts. Algorithms include customizable deep neural networks, fast decision trees and decision forests, linear regression, and logistic regression.  |

### Python integration for in-database analytics

Python is a language that offers great flexibility and power for a variety of machine learning tasks. Open-source libraries for Python include several platforms for customizable neural networks, as well as popular libraries for natural language processing. Now, this widely-used language is supported in SQL Server 2017 Machine Learning.

Because Python is integrated with the database engine, you can keep analytics close to the data and eliminate the costs and security risks associated with data movement. You can deploy machine learning solutions based on Python using tools like Visual Studio. Your production applications can get predictions, models, or visuals from the Python 3.5 runtime using SQL Server data access methods.

T-SQL and Python integration is supported through the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) system stored procedure. You can call any Python code using this stored procedure. Code runs in a secure, dual architecture that enables enterprise-grade deployment of Python models and scripts, callable from an application using a simple stored procedure. Additional performance gains are achieved by streaming data from SQL to Python processes and MPI ring parallelization.

You can use the T-SQL [PREDICT](../t-sql/queries/predict-transact-sql.md) function to perform [native scoring](sql-native-scoring.md) on a pre-trained model that has been previously saved in the required binary format.

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

## New in SQL Server 2016

This release introduced machine learning capabilities into SQL Server through **SQL Server 2016 R Services**, an in-database analytics engine for processing R script on resident data within a database engine instance.

Additionally, **SQL Server 2016 R Server (Standalone)** was released as a way to install R Server on a Windows server. Initially, SQL Server Setup provided the only way to install R Server for Windows. In later releases, developers and data scientists who wanted R Server on Windows could use another standalone installer to achieve the same goal. The standalone server in SQL Server is functionally equivalent to the standalone server product, [Microsoft R Server for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows).

For feature announcements all-up, see [What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md).

| Release |Feature update |
|---------|----------------|
| CU additions | [**Real-time scoring**](real-time-scoring.md) relies on native C++ libraries to read a model stored in an optimized binary format, and then generate predictions without having to call the R runtime. This makes scoring operations much faster. With real-time scoring, you can run a stored procedure or perform real-time scoring from R code. Real-time scoring is also available for SQL Server 2016, if the instance is upgraded to the latest release of [!INCLUDE[rsql-platform-md](../includes/rsql-platform-md.md)]. |
| Initial release | [**R integration for in-database analytics**](r/sql-server-r-services.md). <br/><br/> R packages for calling R functions in T-SQL, and vice versa. RevoScaleR functions provide R analytics at scale by chunking data into component parts, coordinating and managing distributed processing, and aggregating results. In SQL Server 2016 R Services (In-Database), the RevoScaleR engine is integrated with a database engine instance, brining data and analytics together in the same processing context. <br/><br/>T-SQL and R integration through [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql). You can call any R code using this stored procedure. This secure infrastructure enables enterprise-grade deployment of Rn models and scripts that can be called from an application using a simple stored procedure. Additional performance gains are achieved by streaming data from SQL to R processes and MPI ring parallelization. <br/><br/>You can use the T-SQL [PREDICT](../t-sql/queries/predict-transact-sql.md) function to perform [native scoring](sql-native-scoring.md) on a pre-trained model that has been previously saved in the required binary format.|

## Linux support roadmap

SQL Server 2019 CTP 2.3 adds Linux support for R, Python, and Java when you install the machine learning packages with a database engine instance. For more information, see [Install SQL Server Machine Learning Services on Linux](../linux/sql-server-linux-setup-machine-learning.md).

On Linux, SQL Server 2017 does not have R or Python integration, but you can use [Native scoring](sql-native-scoring.md) on Linux because that functionality is available through T-SQL [PREDICT](../t-sql/queries/predict-transact-sql.md), which runs on Linux. Native scoring enables high-performance scoring from a pretrained model, without calling or even requiring an R runtime.

<a name="azure-sql-database-roadmap"></a>

## Machine Learning Services in Azure SQL Database

Machine Learning Services (with R) in Azure SQL Database is in public preview. For more information, see [Quickstart: Use Machine Learning Services (with R) in Azure SQL Database (preview)](https://docs.microsoft.com/azure/sql-database/sql-database-connect-query-r).

## Next steps

+ [Install SQL Server 2017 Machine Learning Services (In-Database)](install/sql-machine-learning-services-windows-install.md)
+ [Machine learning tutorials and samples](tutorials/machine-learning-services-tutorials.md)

---
title: Machine Learning Services in SQL Server | Microsoft Docs
description: Overview introduction to SQL Server 2017 Machine Learning services, R and Python support for in-database analytics
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/27/2018  
ms.topic: overview
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Machine Learning Services in SQL Server 2017
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server 2017 Machine Learning Services is an add-on to a database engine instance, used for executing R and Python code within a SQL Server database as stored procedures, as T-SQL script containing R or Python statements, or as R or Python code containing T-SQL. 

If you previously used SQL Server 2016 R Services, SQL Server 2017 Machine Learning services is the next generation of R support in SQL Server, providing updated versions of RevoScaleR, MicrosoftML, and other R libraries introduced in 2016.

The key value proposition of Machine Learning Services is the power of its enterprise R and Python packages to deliver advanced analytics at scale, and the ability to bring calculations and processing to where the data resides, eliminating the need to pull data across the network.

## Components

SQL Server 2017 supports R and Python. The following table describes the components.

| Component | Description |
|-----------|-------------|
| SQL Server Launchpad service | A service that manages communications between the external R and Python runtimes and the database engine instance. |
| R packages | [**RevoScaleR**](r/revoscaler-overview.md) is the primary library for scaleable R. Functions in this library are among the most widely used. Data transformations and manipulation, statistical summarization, visualization, and many forms of modeling and analyses are found in these libraries. Additionally, functions in these libraries automatically distribute workloads across available cores for parallel processing, with the ability to work on chunks of data that are coordinated and managed by the calculation engine.  <br/>[**MicrosoftML (R)**](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package) adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis. <br/>[**sqlRUtils**](generating-an-r-stored-procedure-for-r-code-using-the-sqlrutils-package.md) provides helper functions for putting R scripts into a T-SQL stored procedure, registering a stored procedure with a database, and running the stored procedure from an R development environment.<br/>[**olapR**](r/how-to-create-mdx-queries-using-olapr.md) is for building or executing an MDX query in R script.|
| Microsoft R Open (MRO) | [**MRO**](https://mran.microsoft.com/open) is Microsoft's open-source distribution of R. The package and interpreter are included. Always use the version of MRO installed by Setup. |
| R tools | R console windows and command prompts are standard tools in an R distribution.  |
| R Samples and scripts |  Open-source R and RevoScaleR packages include built-in data sets so that you can create and run script using pre-installed data. |
| Python packages | [**revoscalepy**](python/what-is-revoscalepy.md) is the primary library for scaleable Python with functions for data manipulation, transformation, visualzation, and analysis. <br/>[**microsoftml (Python)**](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis.  |
| Python tools | The built-in Python command line tool is useful for ad hoc testing and tasks.  |
| Anaconda | Anaconda is an open-source distribution of Python and essential packages. |
| Python samples and scripts | As with R, Python includes built-in data sets  and scripts.  |
| Pre-trained models in R and Python | Pre-trained models are supported and usable on a standalone server, but you cannot install them through SQL Server Setup. For more information, see [Install pre-trained machine learning models on SQL Server](install/sql-pretrained-models-install.md). |


## Use cases

**In-database analytics**

Developers and analysts often have code running on top of a local SQL Server instance. If you have SQL Server and an IDE such as [Visual Studio with R](https://docs.microsoft.com/visualstudio/rtvs/) or [Visual Studio with Python](https://docs.microsoft.com/visualstudio/python/installing-python-support-in-visual-studio) on the same computer, you can build, train, and test models locally in either language. Local access simplifies package management: as an admin, you can load additional third-party packages using built-in permissions for that role.

The most common approach for in-database analytics is to use [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to run R or Python script.

**Client-server configurations**

From any client workstation that has an IDE, install [Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client) or the [Python libraries](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter), and then write code that pushes execution (referred to as a *remote compute context*) to data and operations to a remote SQL Server. 

Similarly, if you are using the Developer edition, you can build solutions on a client workstation using the same libraries and interpreters, and then deploy production code on SQL Server Machine Learning Services (In-Database). 

## How to get started

**Step 1:** Install and configure the software. 

+ [Install SQL Server 2017 Machine Learning Services (In-Database)](install/sql-machine-learning-services-windows-install.md)

**Step 2:** Get hands-on experience using either one of these tutorials:

+ [Tutorial: Run Python in T-SQL](tutorials/run-python-using-t-sql.md)
+ [Tutorial: Run R in T-SQL](tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)

**Step 3:** Add your favorite R and Python packages and use them together with packages provided by Microsoft

+ [R Package management for SQL Server](r/install-additional-r-packages-on-sql-server.md)

## Version history

SQL Server 2017 Machine Learning Services is the next generation of SQL Server 2016 R Services, enhanced to include Python. The following table is a complete list of all versions, from inception to the current release. 

| Product name | Engine version | Release date |
|--------------|---------|--------------|
| SQL Server 2017 Machine Learning Services (In-Database) | R Server 9.2.1 <br/> Python Server 9.2 | October 2017 |
| SQL Server 2017 Machine Learning Server (Standalone) | R Server 9.2.1 <br/> Python Server 9.2 | October 2017 |
| SQL Server 2016 R Services (In-Database) | R Server 9.1  | July 2017  |
| SQL Server 2016 R Server (Standalone)  |  R Server 9.1 | July 2017 |

## Portability and related products

Portability of your custom R and Python code is addressed through package distribution and interpreters that are built into multiple products. The same packages that ship in SQL Server are also available in several other Microsoft products and services, including a non-SQL version called [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/). 

Free clients that include our R and Python interpreters include [Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client) and the [Python libraries](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter).

Packages and interpreters are also available on several [Azure virtual machines](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-azure-vm-on-linux), Azure Machine Learning, and Azure services like [HDInsight](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-on-azure-hdinsight). 

 +  [Provision an Azure Virtual Machine](r/provision-the-r-server-only-sql-server-2016-enterprise-vm-on-azure.md)
  
  The Azure marketplace includes multiple virtual machine images that include Machine Learning Server or R Server. Creating a virtual machine in Microsoft Azure is the fastest way to get to development and deployment of predictive models. Images come with features for scaling and sharing already configured, which makes it easier to embed analytics inside applications and to integrate with backend systems.

+ [Data Science Virtual Machine](https://azure.microsoft.com/services/virtual-machines/data-science-virtual-machines/)

  The latest version of the Data Science Virtual machine includes Machine Learning Server, SQL Server, plus an array of the most popular tools for machine learning, all preinstalled and tested. Create Jupyter notebooks, develop solutions in Julia, and use GPU-enabled deep learning libraries like MXNet, CNTK, and TensorFlow.

## See also

[Install SQL Server Machine Learning Services](install/sql-machine-learning-services-windows-install.md)
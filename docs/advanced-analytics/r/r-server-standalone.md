---
title: Standalone R Server or Machine Learning Server installation - SQL Server Machine Learning Services
description: Overview introduction to standalone R Server and Machine Learning Server in SQL Server Setup
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/18/2018  
ms.topic: overview
author: dphansen
ms.author: davidph
manager: cgronlun
---
# R Server (Standalone) and Machine Learning Server (Standalone) in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server provides installation support for a standalone R Server or Machine Learning Server that runs independently of SQL Server. Depending on your SQL Server version, a standalone server has a foundation of open-source R and possibly Python, overlaid with high-performance libraries from Microsoft that add statistical and predictive analytics at scale. Libraries also enable machine learning tasks scripted in R or Python. 

In SQL Server 2016, this feature is called **R Server (Standalone)** and is R-only. In SQL Server 2017, it's called **Machine Learning Server (Standalone)** and includes both R and Python.  

> [!Note]
> As installed by SQL Server Setup, a standalone server is functionally equivalent to the non-SQL-branded versions of [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server), supporting the same user scenarios, including remote execution, operationalization and web services, and the complete collection of R and Python libraries.

## Components

SQL Server 2016 is R only. SQL Server 2017 supports R and Python. The following table describes the features in each version.

| Component | Description |
|-----------|-------------|
| R packages | [**RevoScaleR**](ref-r-revoscaler.md) is the primary library for scalable R with functions for data manipulation, transformation, visualization, and analysis.  <br/>[**MicrosoftML**](ref-r-microsoftml.md) adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis. <br/>[**sqlRUtils**](ref-r-sqlrutils.md) provides helper functions for putting R scripts into a T-SQL stored procedure, registering a stored procedure with a database, and running the stored procedure from an R development environment.<br/>[**mrsdeploy**](operationalization-with-mrsdeploy.md) offers web service deployment (in SQL Server 2017 only). <br/>[**olapR**](ref-r-olapr.md) is for specifying MDX queries in R.|
| Microsoft R Open (MRO) | [**MRO**](https://mran.microsoft.com/open) is Microsoft's open-source distribution of R. The package and interpreter are included. Always use the version of MRO bundled in setup. |
| R tools | R console windows and command prompts are standard tools in an R distribution. Find them at \Program files\Microsoft SQL Server\140\R_SERVER\bin\x64. |
| R Samples and scripts |  Open-source R and RevoScaleR packages include built-in data sets so that you can create and run script using pre-installed data. Look for them at \Program files\Microsoft SQL Server\140\R_SERVER\library\datasets and \library\RevoScaleR. |
| Python packages | [**revoscalepy**](../python/ref-py-revoscalepy.md) is the primary library for scalable Python with functions for data manipulation, transformation, visualization, and analysis. <br/>[**microsoftml**](../python/ref-py-microsoftml.md) adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis.  |
| Python tools | The built-in Python command-line tool is useful for ad hoc testing and tasks. Find the tool at \Program files\Microsoft SQL Server\140\PYTHON_SERVER\python.exe. |
| Anaconda | Anaconda is an open-source distribution of Python and essential packages. |
| Python samples and scripts | As with R, Python includes built-in data sets  and scripts. Find the revoscalepy data at \Program files\Microsoft SQL Server\140\PYTHON_SERVER\lib\site-packages\revoscalepy\data\sample-data. |
| Pre-trained models in R and Python | Pre-trained models are created for specific use cases and maintained by the data science engineering team at Microsoft. You can use the pre-trained models as-is to score positive-negative sentiment in text, or detect features in images, using new data inputs that you provide. Pre-trained models are supported and usable on a standalone server, but you cannot install them through SQL Server Setup. For more information, see [Install pretrained machine learning models on SQL Server](../install/sql-pretrained-models-install.md). |

## Using a standalone server

R and Python developers typically choose a standalone server to move beyond the memory and processing constraints of open-source R and Python. R and Python libraries executing on a standalone server can load and process large amounts of data on multiple cores and aggregate the results into a single consolidated output. High-performance functions are engineered for both scale and utility: delivering predictive analytics, statistical modeling, data visualizations, and leading-edge machine learning algorithms in a commercial server product engineered and supported by Microsoft.

As an independent server decoupled from SQL Server, the R and Python environment is configured, secured, and accessed using the underlying operating system and standard tools provided in the standalone server, not SQL Server. There is no built-in support for SQL Server relational data. If you want to use SQL Server data, you can create data source objects and connections as you would from any client.

As an adjunct to SQL Server, a standalone server is also useful as a powerful development environment if you need both local and remote computing. The R and Python packages on a standalone server are the same as those provided with a database engine installation, allowing for code portability and [compute-context switching](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-compute-context).

## How to get started

Start with setup, attach the binaries to your favorite development tool, and write your first script.

### Step 1: Install the software

Install either one of these versions:

+ [SQL Server 2017 Machine Learning Server (standalone)](../install/sql-machine-learning-standalone-windows-install.md)
+ [SQL Server 2016 R Server (Standalone) - R only](../install/sql-r-standalone-windows-install.md)

### Step 2: Configure a development tool

On a standalone server, it's common to work locally using a development installed on the same computer.

+ [Set up R tools](set-up-a-data-science-client.md)
+ [Set up Python tools](../python/setup-python-client-tools-sql.md)

### Step 3: Write your first script

Write R or Python script using functions from RevoScaleR, revoscalepy, and the machine learning algorithms.
  
  + [Explore R and RevoScaleR in 25 Functions](https://docs.microsoft.com/machine-learning-server/r/tutorial-r-to-revoscaler): Start with basic R commands and then progress to the RevoScaleR distributable analytical functions that provide high performance and scaling to R solutions. Includes parallelizable versions of many of the most popular R modeling packages, such as k-means clustering, decision trees, and decision forests, and tools for data manipulation.

  + [Quickstart: An example of binary classification with the microsoftml Python package](https://docs.microsoft.com/machine-learning-server/python/quickstart-binary-classification-with-microsoftml): Create a binary classification model using the functions from microsoftml and the well-known breast cancer dataset.

Choose the best language for the task. R is best for statistical computations that are difficult to implement using SQL. For set-based operations over data, leverage the power of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to achieve maximum performance. Use the in-memory database engine for very fast computations over columns.

### Step 4: Operationalize your solution

Standalone servers can use the [operationalization](https://docs.microsoft.com//machine-learning-server/what-is-operationalization) functionality of the non-SQL-branded [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server). You can configure a standalone server for operationalization, which gives you these benefits: deploy and host your code as web services, run diagnostics, test web service capacity.

### Step 5: Maintain your server

SQL Server releases cumulative updates on a regular basis. Applying the cumulative updates adds security and functional enhancements to an existing installation. 

Descriptions of new or changed functionality can be found in the [CAB Downloads](../install/sql-ml-cab-downloads.md) article and on the web pages for [SQL Server 2016 cumulative updates](https://support.microsoft.com/help/3177312/sql-server-2016-build-versions) and [SQL Server 2017 cumulative updates](https://support.microsoft.com/help/4047329). 

For more information on how to apply updates to an existing instance, see [Apply updates](../install/sql-machine-learning-standalone-windows-install.md#apply-cu) in the installation instructions.

## See also

 [Install R Server (Standalone) or Machine Learning Server (Standalone)](../install/sql-machine-learning-standalone-windows-install.md)


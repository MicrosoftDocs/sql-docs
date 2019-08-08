---
title: What is SQL Server Machine Learning Services (Python and R)?
titleSuffix: 
description: Machine Learning Services is a feature in SQL Server that gives the ability to run Python and R scripts with relational data. You can use open source packages and frameworks, or the Microsoft Python and R packages for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server Machine Learning Services.
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

Machine Learning Services is a feature in SQL Server that gives the ability to run Python and R scripts with relational data. You can use open source packages and frameworks, as well as the [Microsoft Python and R packages](#packages) for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network. This article explains the basics of SQL Server Machine Learning Services.

In Azure SQL Database, [Machine Learning Services](https://docs.microsoft.com/azure/sql-database/sql-database-machine-learning-services-overview) is currently in public preview.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
> [!NOTE]
> For executing Java in SQL Server, see the [Language Extensions documentation](../language-extensions/language-extensions-overview.md).
::: moniker-end

## What is Machine Learning Services?

SQL Server Machine Learning Services let you execute Python and R scripts in-database. You can use it to prepare and clean data, do feature engineering, and train, evaluate, and deploy machine learning models within a database. The feature runs your scripts where the data resides and eliminate transfer of the data across the network to another server. 

Base distributions of Python and R are included in Machine Learning Services. You can use open-source packages and frameworks, such as PyTorch, TensorFlow, and scikit-learn, in addition to Microsoft packages [revoscalepy](python/ref-py-revoscalepy.md) and [microsoftml](python/ref-py-microsoftml.md) for Python, and [RevoScaleR](r/ref-r-revoscaler.md), [MicrosoftML](r/ref-r-microsoftml.md), [olapR](r/ref-r-olapr.md), and [sqlrutils](r/ref-r-sqlrutils.md) for R.

Machine Learning Services use an extensibility framework to run Python and R scripts in SQL Server. Learn more about how this works:

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

## How to execute Python and R scripts

There are two ways to execute Python and R scripts in Machine Learning Services:

+ The most common way is to use the T-SQL stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

+ You can also use your preferred Python or R client and write scripts that pushes the execution (referred to as a *remote compute context*) to a remote SQL Server. See how to set up a data science client for [Python development](python/setup-python-client-tools-sql.md) and [R development](r/set-up-a-data-science-client.md) for more information.

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

## How do I get started with Machine Learning Services?

1. [Install SQL Server Machine Learning Services](install/sql-machine-learning-services-windows-install.md)

1. Configure your development tools. There are 

    + You can use 
    + You can use Python or R on your own laptop or development workstation and run R or Python code on SQL Server locally and remotely

### Step 1: Install the software

+ [SQL Server Machine Learning Services](install/sql-machine-learning-services-windows-install.md)
 
### Step 2: Configure a development tool

Data scientists typically use R or Python on their own laptop or development workstation, to explore data, and build and tune predictive models until a good predictive model is achieved. With in-database analytics in SQL Server, there is no need to change this process. After installation is complete, you can run R or Python code on SQL Server locally and remotely.

![rsql_keyscenario2](r/media/rsql-keyscenario2.png) 

+ **Use the IDE you prefer**. You can link the R and Python libraries to your development tool of choice. For more information, see [Set up R tools](r/set-up-a-data-science-client.md) and [Set up Python tools](python/setup-python-client-tools-sql.md).  

+ **Work remotely or locally**. Data scientists can connect to SQL Server and bring the data to the client for local analysis, as usual. However, a better solution is to use the **RevoScaleR** or **revoscalepy** APIs to push computations to the SQL Server computer, avoiding costly and insecure data movement.

+ **Embed R or Python scripts in SQL Server stored procedures**. When your code is fully optimized, wrap it in a stored procedure to avoid unnecessary data movement and optimize data processing tasks.

### Step 3: Write your first script

Call R or Python functions from within T-SQL script:

+ [R: Learn in-database analytics using R](tutorials/sqldev-in-database-r-for-sql-developers.md)
+ [R: End-to-end walkthrough with R](tutorials/walkthrough-data-science-end-to-end-walkthrough.md)
+ [Python: Run Python using T-SQL](tutorials/run-python-using-t-sql.md)
+ [Python: Learn in-database analytics using Python](tutorials/sqldev-in-database-python-for-sql-developers.md)

Choose the best language for the task. R is best for statistical computations that are difficult to implement using SQL. For set-based operations over data, leverage the power of SQL Server  to achieve maximum performance. Use the in-memory database engine for very fast computations over columns.

### Step 4: Optimize your solution

When the model is ready to scale on enterprise data, the data scientist often works with the DBA or SQL developer to optimize processes such as:

+ Feature engineering
+ Data ingestion and data transformation
+ Scoring

Traditionally, data scientists using R have had problems with both performance and scale, especially when using large dataset. That is because the common runtime implementation is single-threaded and can accommodate only those data sets that fit into the available memory on the local computer. Integration with SQL Server Machine Learning Services provides multiple features for better performance, with more data:

+ **RevoScaleR**: This R package contains implementations of some of the most popular R functions, redesigned to provide parallelism and scale. The package also includes functions that further boost  performance and scale by pushing computations to the SQL Server computer, which typically has far greater memory and computational power.

+ **revoscalepy**. This Python library implements the most popular functions in RevoScaleR, such as remote compute contexts, and many algorithms that support distributed processing.

For more information about performance, see this [performance case study](r/performance-case-study-r-services.md) and [R and data optimization](r/r-and-data-optimization-r-services.md).

### Step 5: Deploy and Consume

After the script or model is ready for production use, a database developer might embed the code or model in a stored procedure so that the saved R or Python code can be called from an application. Storing and running R code from SQL Server has many benefits: you can use the convenient SQL Server interface, and all computations take place in the database, avoiding unnecessary data movement.

![rsql_keyscenario1](r/media/rsql-keyscenario1.png)

+ **Secure and extensible**. SQL Server uses a new extensibility architecture that keeps your database engine secure and isolates R and Python sessions. You also have control over the users who can execute scripts, and you can specify which databases can be accessed by code. You can control the amount of resources allocated to the runtime, to prevent massive computations from jeopardizing the overall server performance.

+ **Scheduling and auditing**. When external script jobs are run in SQL Server, you can control and audit the data used by data scientists. You can also schedule jobs and author workflows containing external R or Python scripts, just like you would schedule any other T-SQL job or stored procedure.

To take advantage of the resource management and security features in SQL Server, the deployment process might include these tasks:

+ Converting your code to a function that can run optimally in a stored procedure
+ Setting up security and locking down packages used by a particular task
+ Enabling resource governance (requires the Enterprise edition)

For more information, see [Resource Governance for R](r/resource-governance-for-r-services.md) and [R Package Management for SQL Server](r/install-additional-r-packages-on-sql-server.md).

## Portability and related products

Portability of your custom R and Python code is addressed through package distribution and interpreters that are built into multiple products. The same packages that ship in SQL Server are also available in several other Microsoft products and services, including a non-SQL version called [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/). 

Free clients that include our R and Python interpreters are [Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client) and the [Python libraries](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter).

On Azure, Microsoft's R and Python packages and interpreters are also available on Azure Machine Learning, and Azure services like [HDInsight](https://docs.microsoft.com/azure/hdinsight/r-server/r-server-overview), and [Azure virtual machines](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-azure-vm-on-linux). The [Data Science Virtual Machine](https://azure.microsoft.com/services/virtual-machines/data-science-virtual-machines/) includes a fully equipped development workstation with tools from multiple vendors as well as the libraries and interpreters from Microsoft.

## See also

[Install SQL Server Machine Learning Services](install/sql-machine-learning-services-windows-install.md)

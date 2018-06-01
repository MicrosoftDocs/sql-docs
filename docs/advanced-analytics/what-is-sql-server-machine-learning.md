---
title: What is SQL Server Machine Learning Services? | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: overview
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# What is SQL Server Machine Learning Services?
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server Machine Learning Services is an embedded, predictive analytics and data science engine that can execute R and Python code within a SQL Server database as stored procedures, as T-SQL script containing R or Python statements, or as R or Python code containing T-SQL. 

The key value proposition of Machine Learning Services is the power of its proprietary packages to deliver advanced analytics at scale, and the ability to bring calculations and processing to where the data resides, eliminating the need to pull data across the network.

There are two options for using machine learning capabilities in SQL Server: 

+ [**SQL Server Machine Learning Services (In-Database)**](r/sql-server-r-services.md) operates within the database engine instance, where the calculation engine is fully integrated with the database engine. Most installations are this option.
+ [**SQL Server Machine Learning Server (Standalone)**](r/r-server-standalone.md) is Machine Learning Server for Windows that runs independently of the database engine. Although you use SQL Server Setup to install the server, the feature is not instance-aware. Functionally, it is equivalent to the non-SQL-Server [Microsoft Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install).

## R and Python packages

Support for each language is through proprietary Microsoft packages used for creating and training models of various types, scoring data, and parallel processing using the underlying system resources.

Because the proprietary packages are built on open-source R and Python distributions, script or code that you run in SQL Server can also call base functions and use third-party packages compatible with the language version provided in SQL Server (Python 3.5 and recent versions of R, currently 3.3.3).

| R  | Python | Description |
|-----------|----------------|-------------|
| [RevoScaleR](r/revoscaler-overview.md) | [revoscalepy](python/what-is-revoscalepy.md)   | Functions in these libraries are among the most widely used. Data transformations and manipulation, statistical summarization, visualization, and many forms of modeling and analyses are found in these libraries. Additionally, functions in these libraries automatically distribute workloads across available cores for parallel processing, with the ability to work on chunks of data that are coordinated and managed by the calculation engine. |
| [MicrosoftML](using-the-microsoftml-package.md) | [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) | Industry-leading machine learning algorithms for image featurization, classification problems, and more. |
| [olapR](r/how-to-create-mdx-queries-using-olapr.md) | none | Build or execute an MDX query in R script.
| [sqlRUtils](r/generating-an-r-stored-procedure-for-r-code-using-the-sqlrutils-package.md) | none | Functions for putting R scripts into a T-SQL stored procedure, registering a stored procedure with a database, and running the stored procedure from an R development environment.
| [mrsdeploy](operationalization-with-mrsdeploy.md) | none | Primarily used on a non-SQL installation of Machine Learning Server, such as the [(Standalone) version](r/r-server-standalone.md). Use this package to deploy and host web services, build scale-out topologies with dedicated web and compute nodes, toggle between local and remote sessions, run diagnostics, and more. For an (In-Database) installation, use this package in a client capacity: for example, to access a web service on a remote server dedicated to running just Machine Learning Services workloads. |

Portability of your custom R and Python code is addressed through package distribution and interpreters that are built into multiple products. The same packages that ship in SQL Server are also available in several other Microsoft products and services, including a non-SQL version called [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/). Free clients that include our R and Python interpreters include [Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client) and the [Python libraries](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter).

Packages and interpreters are also available on several [Azure virtual machines](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-azure-vm-on-linux), Azure Machine Learning, and Azure services like [HDInsight](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-on-azure-hdinsight). 


## Use cases

**In-database analytics**

Developers and analysts often have code running on top of a local SQL Server instance. If you have SQL Server and an IDE such as [Visual Studio with R](https://docs.microsoft.com/visualstudio/rtvs/) or [Visual Studio with Python](https://docs.microsoft.com/visualstudio/python/installing-python-support-in-visual-studio) on the same computer, you can build, train, and test models locally in either language. Local access simplifies package management: as an admin, you can load additional third-party packages using built-in permissions for that role.

The most common approach for in-database analytics is to use [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to run R or Python script. The tutorials listed in [Next steps](#next-steps) will get you started.

**Client-server configurations**

From any client workstation that has an IDE, install [Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client) or the [Python libraries](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter), and then write code that pushes execution (referred to as a *remote compute context*) to data and operations to a remote SQL Server. 

Similarly, if you are using the Developer edition, you can build solutions on a client workstation using the same libraries and interpreters, and then deploy production code on SQL Server Machine Learning Services (In-Database). 

## Version history

SQL Server 2017 Machine Learning Services is the next generation of SQL Server 2016 R Services, enhanced to include Python. The following table is a complete list of all versions, from inception to the current release. 

| Product name | Engine version | Release date |
|--------------|---------|--------------|
| SQL Server 2017 Machine Learning Services (In-Database) | R Server 9.2.1 <br/> Python Server 9.2 | October 2017 |
| SQL Server 2017 Machine Learning Server (Standalone) | R Server 9.2.1 <br/> Python Server 9.2 | October 2017 |
| SQL Server 2016 R Services (In-Database) | R Server 9.1  | July 2017  |
| SQL Server 2016 R Server (Standalone)  |  R Server 9.1 | July 2017 |



## Documentation for each version

Recent releases of SQL Server documentation are version-agnostic. For SQL Server Machine Learning Services, Python is only available in 2017 and later, while R support is in all versions. Unless noted otherwise, you can assume R documentation applies to both 2016 and 2017 versions.


## Related machine learning products

 +  [Provision an Azure Virtual Machine](r/provision-the-r-server-only-sql-server-2016-enterprise-vm-on-azure.md)
  
  The Azure marketplace includes multiple virtual machine images that include Machine Learning Server or R Server. Creating a virtual machine in Microsoft Azure is the fastest way to get to development and deployment of predictive models. Images come with features for scaling and sharing already configured, which makes it easier to embed analytics inside applications and to integrate with backend systems.

+ [Data Science Virtual Machine](https://azure.microsoft.com/services/virtual-machines/data-science-virtual-machines/)

  The latest version of the Data Science Virtual machine includes Machine Learning Server, SQL Server, plus an array of the most popular tools for machine learning, all preinstalled and tested. Create Jupyter notebooks, develop solutions in Julia, and use GPU-enabled deep learning libraries like MXNet, CNTK, and TensorFlow.

<a name="next-steps"></a>

## Next steps

**Step 1:** Install and configure the software. 

+ [Install SQL Server 2017 Machine Learning Services (In-Database)](install/sql-machine-learning-services-windows-install.md)

**Step 2:** Get started with code using either one of these tutorials:

+ [Tutorial: Run Python in T-SQL](tutorials/run-python-using-t-sql.md)
+ [Tutorial: Run R in T-SQL](tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)

**Step 3:** Add your favorite R and Python packages and use them together with packages provided by Microsoft

+ [R Package management for SQL Server](r/install-additional-r-packages-on-sql-server.md)

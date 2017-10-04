---
title: "Architecture and Overview | Microsoft Docs"
ms.custom: ""
ms.date: "06/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# Architecture and Overview of Machine Learning Services

This topic describes the goals of the extensibility framework that supports execution of Python and R script in SQL Server.

It also provides an overview of how the architecture is designed to meet these goals, how R and Python are supported and executed by SQL Server, and the benefits of integration.

Overall, the extensibility framework is almost identical for R and Python, with some minor differences in the details of the launchers that are called, configuration options, and so forth. For further information about the implementation for a specific language, see these topics:

- [Architecture Overview for SQL Server R Services](r/architecture-overview-sql-server-r.md)
- [Architecture Overview for Python in SQL Server](python/architecture-overview-sql-server-python.md)


## Background

In SQL Server 2016, numerous changes were introduced to the database engine to support execution of R scripts using SQL Server. In SQL Server 2017, this underlying infrastructure was improved to add support for the Python language.

The goal of the extensibility framework was to create a better interface between SQL Server and data science languages such as R and Python, both to reduce the friction that occurs when data science solutions are moved into production, and to protect data that might be exposed during the data science development process.

By executing a trusted scripting language within a secure framework managed by SQL Server, the database developer can maintain security while allowing data scientists to use enterprise data.

  ![Goals of integration with SQL Server](media/ml-service-value-add.png "Machine Learning Services Value Add")

- Current users of R or Python should be able to port their code and execute it in SQL Server with relatively minor modifications.
- The data security model in SQL Server is extended to data used by external script languages. In other words, someone running R or Python script should not be able to use any data that could not be accessed by that user in a SQL query.
- The database administrator should be able to manage resources used by external scripts, manage users, and manage and monitor external code libraries.
- The system must support solutions based entirely on open source distributions of R and Python, but use proprietary components developed by Microsoft to provide greater security and performance.

## Architecture core concepts

To meet these goals, the architecture of SQL Server 2016 R Services and SQL Server 2017 Machine Learning Services for R and Python is based on these core concepts:

+ **Multi-process architecture**

  Both R and Python are open-source languages with rich and enthusiastic community support. Therefore, it is important to maintain full interoperability with open source R and Python.

  Open source distributions of R and Python are installed with SQL Server under license, and can function independently from SQL Server if needed.

   In addition, Microsoft provides a set of proprietary libraries that provide integration with SQL Server, including data translation, compression, and optimization targeted at each supported language.

+ **Security**

   Better security means support for both integrated Windows authentication and password-based SQL logins, as well as secure handling of credentials, reliance on SQL Server for data protection, and use of the SQL Server Trusted Launchpad to manage external script execution and secure data used in scripts.

+ **Scalability and performance**

  Integration with SQL Server is key to improving the usefulness of R and Python in the enterprise. Any R or Python script can be run by calling a stored procedure, and the results are returned as tabular results directly to SQL Server, making it easy to generate or consume machine learning from any application that can send a SQL query and handle the results.

  Performance optimization relies on two equally powerful aspects of the platform: resource governance and parallel processing using SQL Server, and distributed computing provided by the algorithms in **RevoScaleR** and **revoscalepy**.


## Solution development and deployment

In addition to these core goals for the extensibility platform, the machine learning services in SQL Server are designed to provide strong integration with the database engine and the BI stack, with these benefits:

+ Performance and resource management through traditional monitoring tools
+ Easy use of Python and R data by BI suites or any application that can consume SQL query results
+ Much lower barrier for enterprise development of machine learning solutions

Let's see how it works in practice.

  ![ML solution development process](media/ml-solution-development-process.png "Develop and deploy using Machine Learning Services")

1. Data is kept within the compliance boundary and use of data can be managed and monitored by SQL Server. Meanwhile, the DBA has full control over who can install packages or run scripts on the server. If so desired, the DBA can also delegate permissions on a database level to data scientists or managers.
2. Data scientists can build and test solutions in their preferred R or Python environments, disconnected from the server.
3. The SQL developer can use familiar tools such as Management Studio or Visual Studio to integrate the R or Python code with SQL Server. The tight integration means that the savvy developer can choose the best tool to optimize each task. For example, you might use SQL for some feature engineering tasks and R for others. You might embed Python script in an Integration Services task to perform sophisticated text analytics.
4. Tested and ready-to-deploy solutions can be optimized using SQL Server technologies, such as columnstore indexes, for better performance. Newer features let you batch-train many small models in parallel on partitioned data set, or score millions of rows in using native SQL code optimized for machine learning tasks.
5. Ready to lift off? You can easily expose your predictive solutions to the BI stack or external applications by using stored procedures.

## Related products

Not sure which machine learning solution meets your needs? In addition to embedded analytics in SQL Server 2016 and SQL Server 2017, Microsoft provides the following machine learning platforms and services:

+ [Microsoft R Server](https://msdn.microsoft.com/microsoft-r/rserver)

  A multi-platform environment for developing, distributing, and managing machine learning jobs
+ [Data Science Virtual Machine](https://docs.microsoft.com/azure/machine-learning/machine-learning-data-science-virtual-machine-overview)

  All the tools you need for machine learning, preinstalled. Use Jupyter notebooks, Python, or R.
  
  Try the new [Windows 2016 preview edition](http://aka.ms/dsvm/win2016), which includes GPU versions of popular deep learning frameworks such as CNTK and mxNet, as well as support for Windows containers!
+ [Azure Cognitive Services](https://azure.microsoft.com/services/cognitive-services/)

  A variety of cloud services for adding AI and ML into your applications, including natural language indexing of video, facial recognition, emotion detection, text analytics, machine translation, and much, more
+ [Azure Machine Learning](https://azure.microsoft.com/services/machine-learning/)

  A cloud-based drag-and-drop interface for designing machine learning workflows, coupled with the ability to automate and integrate with applications via web services and PowerShell

## See Also

[R Server Standalone](https://docs.microsoft.com/sql/advanced-analytics/r/r-server-standalone)

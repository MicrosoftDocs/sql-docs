---
title: "Getting Started | Microsoft Docs"
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
# Getting Started with Machine Learning

Microsoft provides an integrated, scalable set of machine learning solutions for both on-premises and the cloud:

+ **Integrated**, because you can run R or Python in SQL Server. This lets you easily merge enterprise processes for ETL and reporting with data science tasks such as feature engineering, model creation, and scoring.
+ **Scalable**, because the data scientist can develop and test a solution on a laptop, and then deploy it to multiple platforms for distributed or parallel processing of key operations such as model training and scoring. Supported platforms include SQL Server on Windows, Hadoop, and Spark.

This article provides links to resources for each product in the Microsoft Machine Learning platform.

## Machine learning in SQL Server

+ SQL Server 2016

  SQL Server 2016 supports running R code in SQL Server using stored procedures. This makes it easy to automate machine learning tasks by using SQL tools. Or, you can run R code from a remote laptop or R development environment, while using the SQL Server computer as the _compute context_.

  This integration provides security for your data and lets you manage and balance resources used by R.

    + [Getting Started wth SQL Server R Services](r/getting-started-with-sql-server-r-services.md)
    + [Set up SQL Server R Services or Machine Learning Services](../advanced-analytics/r/set-up-sql-server-r-services-in-database.md)

+ SQL Server 2017

  Beginning with SQL Server 2017 CTP 2.0, support for Python has been added, and the name was changed to Machine Learning Services (In-Database) to reflect support for a wider range of machine learning solutions.

    + [Architecture Overview for Python in SQL Server](python/architecture-overview-sql-server-python.md)
    + [Set up SQL Server R Services or Machine Learning Services](../advanced-analytics/r/set-up-sql-server-r-services-in-database.md)

## Microsoft R Server

The option to install Microsoft R Server is provided in SQL Server 2016 to support enterprise customers who want  distributed, scalable R processing but don't necessarily require close integration with the SQL Server database engine.
  
  + [Introducing Microsoft R Server](https://msdn.microsoft.com/microsoft-r/rserver)
  
You can also install R Server through platform-specific installers available from MSDN:

  + [R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows)
  + [R Server for Linux](https://msdn.microsoft.com/microsoft-r/rserver-install-linux-server)
  + [R Server for Hadoop](https://msdn.microsoft.com/microsoft-r/rserver-install-hadoop)

> [!IMPORTANT]
> If you want to run Python using R Server, be sure to install the latest version, **Machine Learning Server**, which is available only through SQL Server 2017 setup:
> 
>    + [Set up Microsoft R Server or Machine Learning Server](../advanced-analytics/r/create-a-standalone-r-server.md)

## Related products

+ [Set Up a Data Science Client](../advanced-analytics/r/set-up-a-data-science-client.md)

  If you already installed one of the machine learning server products, this article provides information about how to set up a separate computer for development and testing of solutions, including tools and required libraries.

+ [Data Science Virtual Machine](../advanced-analytics/r/provision-the-r-server-only-sql-server-2016-enterprise-vm-on-azure.md)

  Jump-start your entry into machine learning by getting this complete machine learning solution from the Azure eMarketplace. which includes the server and all development tools, a Python Anaconda distribution, Jupyter notebooks, and the latest version of Microsoft R.
  
  The latest version of the Data Science Virtual Machine (DSVM) runs on Windows 2016 Preview Edition, to provide the clean customizable look of Windows 10. It comes pre-configured with NVIDIA drivers, CUDA Toolkit 8.0, and the NVIDIA cuDNN library for GPU workloads.

## Resources for learning

+ [Machine Learning Tutorials](../advanced-analytics/tutorials/machine-learning-services-tutorials.md)

  Start here to find a list of all resources for learning about machine learning solutions using SQL Server 2017 and SQL Server 2017.

### R tutorials

+ [Use R in SQL Server](/tutorials/sql-server-r-tutorials.md)

   Learn how to run R in SQL Server, create and use remote compute contexts, or perform simulations in R using SQL Server.
   
   Includes "all code provided" tutorials so that you can run a complete R solution from SQL Server Management Studio, without ever opening an R IDE!

+ [Explore R and Scale R in 25 Short Functions](https://msdn.microsoft.com/microsoft-r/microsoft-r-tutorial-r2revoscaler)

   New to R? Wondering how Microsoft R (or RevoScaleR) compares to standard R? See these quick-starts for R Server.

### Python tutorials

+ [Use Python in SQL Server](/tutorials/sql-server-r-tutorials.md)

  Learn how to run Python in SQL Server. Build a model using Python and use it to score SQL Server data.

   An end-to-end solution for SQL developers provides all the code you need to run Python from SQL Server Management Studio.

+ [Publish and Consume Python Code](../advanced-analytics/python/publish-consume-python-code.md)

  Need to deploy a model to a web service using Machine Learning Server? See this walkthrough, complete with code.

### Product samples with code

These solutions from the SQL Server development team run in either R or Python, and demonstrate common scenarios for integrating machine learning with business applications.

+ [Build an intelligent app with SQL Server and R](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction)

+ [Build an intelligent app with SQL Server and Python](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)

### Data science solution templates

The solution templates from the Microsoft Data Science team represent complete sample solutions tailored for specific industries or scenarios. They are intended to serve as building blocks to help you implement a solution fast, or to demonstrate best practices. Each template includes sample data and customizable code.

+ [Solution templates](../advanced-analytics/tutorials/data-science-scenarios-and-solution-templates.md)

## See Also

[Get Started with SQL Server R Services](../advanced-analytics/r/getting-started-with-sql-server-r-services.md)

[Get Started with Microsoft R Server](../advanced-analytics/r/getting-started-with-microsoft-r-server-standalone.md)

---
title: "Getting Started | Microsoft Docs"
ms.custom: ""
ms.date: "06/09/2017"
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

+ **Integrated**, because you can run R or Python in SQL Server. This lets you easily merge enterprise work flows for ETL and reporting with data science tasks such as feature engineering, model creation, and scoring.
+ **Scalable**, because the data scientist can develop and test a solution on a laptop, and then deploy it to multiple platforms for distributed or parallel processing of key operations such as model training and scoring. Supported platforms include SQL Server on Windows, Hadoop, and Spark. 

This article provides links to resources for each product in the Microsoft Machine Learning platform.

## Products and Platforms

**SQL Server 2016**

SQL Server 2016 supports running R code in SQL Server, or using the SQL Server as the remote compute context for R jobs. This  provides security for your data and lets you manage and balance resources used by R.

**SQL Server 2017**

In SQL Server 2017 CTP 2.0, support for Python was added, and the name was changed to Machine Learning Services (In-Database).

+ [Set up SQL Server R Services or Machine Learning Services](../advanced-analytics/r/set-up-sql-server-r-services-in-database.md)

**Microsoft R Server**

If you don't need to use SQL Server data, install Microsoft R Server to enjoy distributed, scalable R processing. You can install R Server either through [platform-specific installers](), or through SQL Server setup.
+ [Introducing Microsoft R Server](https://msdn.microsoft.com/microsoft-r/rserver)

**Microsoft Machine Learning Server**

If you want to try out Python integration, be sure to install the latest version, **Machine Learning Server**, which is available only through SQL Server 2017 setup: [Set up Microsoft R Server or Machine Learning Server](../advanced-analytics/r/create-a-standalone-r-server.md)

### Related Products

Use this standalone installer if you want to set up a server for machine learning but don't need SQL Server
+ [Microsoft R Server](https://msdn.microsoft.com/library/mt674874.aspx)

If you already installed one of the machine learning server products, this article provides information about how to set up a separate computer for development and testing of solutions, including tools and required libraries. 

+ [Set Up a Data Science Client](../advanced-analytics/r/set-up-a-data-science-client.md)

Try out the complete machine learning solution on this virtual machine, which includes the server and all development tools, a Python Anaconda distribution, Jupyter notebooks, and the latest version of Microsoft R.

+ [Data Science Virtual Machine](../advanced-analytics/r/provision-the-r-server-only-sql-server-2016-enterprise-vm-on-azure.md)

## Demos, Blogs, and Tutorials

These demos illustrate how a ski rental business might use machine learning to predict future rentals and schedule staff to meet demand.

+ [Build an intelligent app with SQL Server and R](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction)

+ [Build an intelligent app with SQL Server and Python](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)

See this page for links to tutorials and end-to-end solutions for using R in SQL Server.

Current examples also include how to run Python in SQL Server, or build a model using Python and use it to score SQL Server data.

+ [Machine Learning Tutorials](../advanced-analytics/tutorials/machine-learning-services-tutorials.md)

Need to deploy a model to a web service using Machine Learning Server? See this walkthrough, complete with code:

+ [Publish and Consume Python Code](../advanced-analytics/python/publish-consume-python-code.md)


New to R? Wondering what RevoScaleR offers that R doesn't? See this R Server quick-start.

+ [Explore R and Scale R in 25 Short Functions](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started-tutorial)

Templates are sample solutions that demonstrate best practices and provide building blocks to help you implement a solution fast. Each template is designed to solve a specific problem, and includes sample data, and customizable code.

+ [Solution templates](../advanced-analytics/tutorials/data-science-scenarios-and-solution-templates.md)

## See Also

[Get Started with SQL Server R Services](../advanced-analytics/r/getting-started-with-sql-server-r-services.md)

[Get Started with Microsoft R Server](../advanced-analytics/r/getting-started-with-microsoft-r-server-standalone.md)

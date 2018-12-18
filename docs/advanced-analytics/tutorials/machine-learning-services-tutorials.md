---
title: SQL Server R and Python tutorials - SQL Server Machine Learning
description: Introduction and overview of R and Python tutorials for SQL Server machine learning.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/22/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Tutorials for SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article provides a comprehensive list of the tutorials, demos, and sample applications that use machine learning features in SQL Server 2016 or SQL Server 2017. Start here to learn how to run R or Python from T-SQL, how to use remote and local compute contexts, and how to operationalize your R and Python code for a SQL production environment.

+ [Python tutorials](../tutorials/sql-server-python-tutorials.md)

+ [R tutorials](../tutorials/sql-server-r-tutorials.md)

## <a name ="bkmk_samples"></a>R and Python samples

These samples and demos provided by the SQL Server and R Server development team highlight ways that you can use embedded analytics in real-world applications.

| Link | Description | Applies to |
|------|-------------|------------|
| [Perform customer clustering using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/customerclustering/) | Use unsupervised learning to segment customers based on sales data. This example uses the scalable rxKmeans algorithm from Microsoft R to build the clustering model. | SQL Server 2016 or SQL Server 2017 |
| [Perform customer clustering using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/customerclustering/) | Learn how to use the Kmeans algorithm to perform unsupervised clustering of customers. This example uses the Python language in-database.| SQL Server 2017 |
| [Build a predictive model using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction) | Learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand. This example uses the Microsoft algorithms to build logistic regression and decision trees models. | SQL Server 2016 or SQL Server 2017 |
| [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/) | Build the ski rental analysis application using Python, to help plan for future demand. This example uses the new Python library, **revoscalepy**, to create a linear regression model. | SQL Server 2017 |
| [How to use Tableau with SQL Server Machine Learning Services](https://blogs.msdn.microsoft.com/mlserver/2017/12/14/how-to-use-tableau-with-sql-server-machine-learning-services-with-r-and-python/) | Analyze social media and create Tableau graphs, using SQL Server and R. | SQL Server 2016 or SQL Server 2017 |

## <a name="bkmk_solutions"></a>Solution templates

The Microsoft Data Science Team has provided customizable solution templates that can be used to jump-start solutions for common scenarios. Each solution is tailored to a specific task or industry problem. Most of the solutions are designed to run either in SQL Server, or in a cloud environment such as Azure Machine Learning. Other solutions can run on Linux or in Spark or Hadoop clusters, by using Microsoft R Server or Machine Learning Server.

All code is provided, along with instructions on how to train and deploy a model for scoring using SQL Server stored procedures.

+ [Fraud detection](https://gallery.cortanaanalytics.com/Tutorial/Online-Fraud-Detection-Template-with-SQL-Server-R-Services-1)
+ [Custom churn prediction](https://gallery.cortanaanalytics.com/Tutorial/Customer-Churn-Prediction-Template-with-SQL-Server-R-Services-1)
+ [Predictive maintenance](https://gallery.cortanaanalytics.com/Tutorial/Predictive-Maintenance-Template-with-SQL-Server-R-Services-1)
+ [Predict hospital length of stay](https://gallery.cortanaintelligence.com/Solution/Predicting-Length-of-Stay-in-Hospitals-1)

For more information, see [Machine Learning Templates with SQL Server 2016 R Services](https://blogs.technet.microsoft.com/machinelearning/2016/03/23/machine-learning-templates-with-sql-server-2016-r-services/).

## Recommended reading

+ [Why did we build it?](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2017/01/10/sql-server-r-services-why-did-we-build-it/)

    Want to know the real story behind R Services? Read this article from the development and PM team that explains the origin and goals of SQL Server R Services.

+ [Tutorials and sample data for Microsoft R](https://docs.microsoft.com/machine-learning-server/r/tutorial-introduction)

    Learn about Microsoft R, and what the RevoScaleR package offers in this collection of quick tutorials. Learn how to write R code once and deploy anywhere, using RevoScaleR data sources and remote compute contexts.

+ [Get started with MicrosoftML](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-the-microsoftml-package)

  Learn how to use the new algorithms in the MicrosoftML package for advanced modeling and scalable data transformations, optimized for multiple compute contexts.

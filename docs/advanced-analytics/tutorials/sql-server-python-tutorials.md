---
title: "SQL Server Python tutorials | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "09/19/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2017"
dev_langs: 
  - "Python"
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# SQL Server Python tutorials

This article provides a list of tutorials and samples that demonstrate the use of Python with SQL Server 2017. Through these samples and demos, you will learn:

+ How to run Python from T-SQL
+ What are remote and local compute contexts, and how you can execute Python code using the SQL Server computer
+ How to wrap Python code in a stored procedure
+ Optimizing Python code for a SQL production environment
+ Real-world scenarios for embedding machine learning in applications

For information about requirements and setup, see [Prerequisites](#bkmk_Prerequisites).

## <a name="bkmk_pythontutorials"></a>Python tutorials

+ [Running Python in T-SQL](run-python-using-t-sql.md)

   Learn the basics of how to call Python in T-SQL, using the extensibility mechanism pioneered in SQL Server 2016.

+ [Create a machine learning model in Python using revoscalepy](use-python-revoscalepy-to-create-model.md)

   You'll create a model using **rxLinMod**, from the new **revoscalepy** library. You'll launch the code from a remote Python terminal but the modeling will take place in the SQL Server compute context.

+ [In-Database Python analytics for SQL developers](sqldev-in-database-python-for-sql-developers.md)

  NEW! Build a complete Python solution using T-SQL stored procedures. All Python code is included.

+ [Deploy and consume a Python model](..\python\publish-consume-python-code.md)

  Learn how to deploy a Python model using the latest version of Microsoft Machine Learning Server.

## Python samples

These samples and demos provided by the SQL Server development team highlight ways that you can use embedded analytics in real-world applications.

+ [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)

  Learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand.

  > [!TIP]
  > Now includes native scoring from Python models!

+ [Perform customer clustering using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/customerclustering/)

    Learn how to use the Kmeans algorithm to perform unsupervised clustering of customers.

## <a name="bkmk_Prerequisites"></a>Prerequisites

To use these tutorials, you must have installed SQL Server 2017 Machine Learning Services (In-Database). SQL Server 2017 supports either R or Python. However, you must install the extensibility framework that supports machine learning, and select Python as the language to install. You can install both R and Python on the same computer.

> [!NOTE]
>
> Support for Python is a new feature in SQL Server 2017, and requires CTP 2.0 or later. Although the feature is in pre-release and not supported for production environments, we invite you to try it out and send feedback.

**SQL Server 2017**

After running SQL Server setup, don't forget these important steps:

+ Enable the external script execution feature by running `sp_configure 'external scripts enabled', 1`.
+ Restart the server.
+ Ensure that the service that calls the external runtime has necessary permissions.
+ Ensure that your SQL login or Windows user account has necessary permissions to connect to the server, to read data, and to create any database objects required by the sample.

If you run into trouble, see this article for some common issues: [Troubleshooting Machine Learning Services](../machine-learning-troubleshooting-faq.md)

## See also

[R tutorials for SQL Server](sql-server-r-tutorials.md)

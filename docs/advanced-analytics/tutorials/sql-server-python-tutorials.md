---
title: SQL Server Python tutorials | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# SQL Server Python tutorials
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

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

   This lesson demonstrates how you can run code from a remote Python terminal, using SQL Server compute context. You should be somewhat familiar with Python tools and environments. Sample code is provided that creates a model using **rxLinMod**, from the new **revoscalepy** library. 

+ [In-Database Python analytics for SQL developers](sqldev-in-database-python-for-sql-developers.md)

    This end-to-end walkthrough demonstrates the process of building a complete Python solution using T-SQL stored procedures. All Python code is included.


## Python samples

These samples and demos provided by the SQL Server development team highlight ways that you can use embedded analytics in real-world applications.

+ [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)

  Learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand.

  > [!TIP]
  > Now includes native scoring from Python models!

+ [Perform customer clustering using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/customerclustering/)

    Learn how to use the Kmeans algorithm to perform unsupervised clustering of customers.

## See also

[R tutorials for SQL Server](sql-server-r-tutorials.md)

---
title: Java language extension in SQL Server 2019 | Microsoft Docs
description: Run Java code on SQL Server 2019 using the Java language extension.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# Java language extension in SQL Server 2019 

The Java language extension runs in the extensibility framework as an add-on to a database engine instance. The framework is an architecture for executing external code: Java (starting in SQL Server 2019), Python (starting in SQL Server 2017) and R (starting in SQL Server 2016). Code execution is isolated from core engine processes, but fully available to relational data as stored procedures, as T-SQL script containing Java, or as Java containing T-SQL. In contrast with R and Python, there are no data science models or machine learning algorithms in the Java extension.

To use the Java extension to run your own code, follow these steps.

## 1 - Feature installation

Run SQL Server 2019 Setup on Windows or Linux to install a database engine instance, adding the Machine Learning Services feature. This feature provides the extensibility framework and adds the security layer that isolates code execution, and protects data from unauthorized access.

Be sure to complete the enable external script execution step in the installation instructions.

## 2 - Wrap code in a stored procedure

Connect to the relational database providing your data and create a stored procedure that calls [sp_execute_external_script]() system stored procedure. Save the stored procedure. Permissions on the database apply to Java code execution. 

## 3 - Call the procedure from your app

Programmatic access to the code occurs when you call the procedure from your code. For one example, see [Using a Stored Procedure with Input Parameters (JDBC)](https://docs.microsoft.com/sql/connect/jdbc/using-a-stored-procedure-with-input-parameters?view=sql-server-2017).

## See also

[Install SQL Server Machine Learning Services on Windows](install/sql-machine-learning-services-windows-install.md)

[Install SQL Server Machine Learning Services on Linux](../../linux/sql-server-linux-setup-machine-learning.md)
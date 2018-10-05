---
title: Give users permission to SQL Server Machine Learning Services | Microsoft Docs
description: How to give users permission to SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/05/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Connect to SQL Server Machine Learning Services from your data science client computer
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how you can connect to SQL Server Machine Learning Services from your data science client computer.

## Create an ODBC data source on your data science client

You might create a machine learning solution on a data science client computer. If you need to run code by using the SQL Server computer as the compute context, you have two options: access the instance by using a SQL login, or by using a Windows account.

+ For SQL logins: Ensure that the login has appropriate permissions on the database where you are reading data. You can do this by adding *Connect to* and *SELECT* permissions, or by adding the login to the `db_datareader` role. To create objects, assign `DDL_admin` rights. If you must save data to tables, add to the `db_datawriter` role.

+ For Windows authentication: You might need to create an ODBC data source on the data science client that specifies the instance name and other connection information. For more information, see [ODBC data source administrator](../../odbc/admin/odbc-data-source-administrator.md).

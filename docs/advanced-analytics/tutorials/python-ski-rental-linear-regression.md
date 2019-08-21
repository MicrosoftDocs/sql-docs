---
title: 'Python tutorial: Ski rentals (linear regression)'
description: 
ms.prod: sql
ms.technology: machine-learning
ms.date: 08/21/2019
ms.topic: tutorial
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In this four-part tutorial series, you will use Python and linear regression in SQL Server Machine Learning Services to predicts the the number of ski rentals. You will use your own Python integrated development environment (IDE), such as a Jupyter notebook in Azure Data Studio.

Imagine you own a ski rental business and you want to predict the number of rentals that you'll have on a future date. This information will help you get your stock, staff, and facilities ready.

In the first part of this series, you'll get set up with the pre-requsites. In parts two and three, you'll develop some Python scripts in a Jupyter notebook to prepare your data and train a machine learning model. Then, in part three, you'll run those Python scripts inside SQL Server using T-SQL stored procedures.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Import a sample database into SQL Server 
> * Use Python notebooks in Azure Data Studio

In [part two](python-ski-rental-linear-regression-data-prepare.md), you'll learn how to load the data from SQL Server into a Python data frame, and prepare the data in Python.

In [part three](python-ski-rental-linear-regression-build-compare.md), you'll learn how to create and train multiple machine learning models in Python, and then choose the most accurate one.

In [part four](python-ski-rental-linear-regression-deploy.md), you'll learn how to store the model to SQL Server, and then create stored procedures from the Python scripts you developed in parts two and three. The stored procedures will run in SQL Server to make predictions based on new data.

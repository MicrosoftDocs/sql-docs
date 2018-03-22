---
title: "Machine Learning Services with Python | Microsoft Docs"
ms.date: "03/16/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: python
ms.technology: 
  
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 
caps.latest.revision: 38
author: "HeidiSteen"
ms.author: "heidist"
manager: "cgronlun"
ms.workload: "On Demand"
---
# Machine Learning Services with Python
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Python is a language that offers great flexibility and power for a variety of machine learning tasks. Open source libraries for Python include several platforms for customizable neural networks, as well as popular libraries for natural language processing. Now, this widely-used language is supported in SQL Server 2017 Machine Learning.

Because Python is integrated with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, you can keep analytics close to the data and eliminate the costs and security risks associated with data movement.  You can deploy machine learning solutions based on Python using convenient, familiar tools such as Visual Studio. Your production applications can get predictions, models, or visuals from the Python 3.5 runtime by simply calling a T-SQL stored procedure.

This release includes the Anaconda distribution of Python, as well as the [revoscalepy](../python/what-is-revoscalepy.md) library, to improve the scale and performance of your machine learning solutions.

You can install everything you need to get started with Python through SQL Server 2017 setup:

+ [**Machine Learning Services (In-Database)**](../install/sql-machine-learning-services-windows-install.md): Install this feature, together with the SQL Server database engine, to enable secure execution of Python scripts on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.
  
     When you select this feature, extensions are installed in the database engine to support execution of Python scripts, and a new service is created, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], to manage communications between the Python runtime and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

+ [**Machine Learning Server (Standalone)**](../install/sql-machine-learning-standalone-windows-install.md): If you do not need SQL Server integration, install this feature to get Python and R support for distributed machine learning.

## See also

+ [SQL Server Machine Learning and R Services (In-Database)](../r/sql-server-r-services.md)
+ [SQL Server Machine Learning and R Server (Standalone)](../r/r-server-standalone.md)
+ [Python architecture](architecture-overview-sql-server-python.md)
+ [Python Tutorials](../tutorials/sql-server-python-tutorials.md)

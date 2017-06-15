---
title: "SQL Server R Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "04/13/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: ba1dea65-40ea-484a-b767-53680c954934
caps.latest.revision: 38
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Machine Learning Services with Python

Python is a language that offers great flexibility and power for a variety of machine learning tasks. Open source libraries for Python include several platforms for customizable neural networks, as well as natural language processing. Now, this popular language is supported in SQL Server 2017 CTP 2.0.

Because Python is integrated with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, you can keep analytics close to the data and eliminate the costs and security risks associated with data movement.  You can deploy machine learning solutions based on Python using convenient, familiar tools such as Visual Studio, and your production applications can get predictions, models, or visuals from the Python 3.5 runtime by simply calling a T-SQL stored procedure.

This release includes the Anaconda distribution of Python, as well as the new **revoscalepy** library, to improve the scale and performance of your machine learning solutions.

You can install everything you need to get started with Python through SQL Server setup:

+ **Machine Learning Services (In-Database):** Install this feature, together with the SQL Server database engine, to enable secure execution of R scripts on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.
  
     When you select this feature, extensions are installed in the database engine to support execution of Python scripts, and a new service is created, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], to manage communications between the Python runtime and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

+ **Machine Learning Server (Standalone):** If you do not need SQL Server integration, install this feature to get Python support and use the deploy and consume features of mrsdeploy.
  
     Do not install this feature on the same computer that is running SQL Server Machine Learning Services.


## Additional Resources

[Set Up Python Machine Learning Services In-Database](setup-python-machine-learning-services.md)

[Tutorials](../tutorials/machine-learning-services-tutorials.md)




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

Python is a language that offers great flexibility and power for a variety of machine learning tasks. Open source libraries for Python include several popular platforms for neural networks and text processing. Now, this popular language is supported in SLQ Server vNext, and in Microsoft Machine Learning Server 9.1.0.

Because Python is integrated with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, you can keep analytics close to the data and eliminate the costs and security risks associated with data movement.  You can deploy machine learning solutions based on Python using convenient, familiar tools such as Visual Studio, and your production applications can get predictions, models, or visuals from the Python 3.5 runtime by simply calling a T-SQL stored procedure.

This release includes the Anaconda distribution of Python, as well as the new RevoScalePy libraries to improve the scale and performance of your machine learning solutions.  
  
You can install everything you need to get started with Python through SQL Server setup: 
  
+ **Machine Learning Services (In-Database):** Install this feature, together with the SQL Server database engine, to enable secure execution of R scripts on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.  
  
     When you select this feature, extensions  are installed in the database engine to support execution of Python scripts, and a new service is created, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], to manage communications between the Python runtime and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  

+ **Microsoft Machine Learning Server (Standalone)** lets you set up a separate Windows computer to run Python without using SQL Server. 


## Additional Resources  
  

Set Up Python Machine Learning Services In-Database  

Use SQL Server setup to install Python 3.5 as part of SQL Server.  
  
Tutorials

Learn how to create SQL Server data sources in your Python code, and use the SQL Server as the remote compute context. 
  
## See Also  


---
title: "Getting Started with Microsoft R Server (Standalone) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "r-server"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 52347d0d-ce60-4bb8-98d2-6163e87716b0
caps.latest.revision: 21
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Getting Started with Microsoft R Server (Standalone)
  Microsoft R Server (Standalone)  helps you bring the popular open source R language into the enterprise, to enable high-performance analytics solutions and integration with other business applications.  

  
## Install Microsoft R Server 

How you install Microsoft R Server depends on whether you need to use SQL Server data in your applications. If so, you should install using SQL Server setup. If you will not be using SQL Server data, or don't need to run R code in-database, you can use either SQL Server setup, or the new standalone installer.
 
 
+ Install Microsoft R Server (Standalone) from SQL Server setup. A separate instance of the R binaries is created for R Server, and the instance is licensed via the SQL Server Enterprise Edition support policy. For more information,  see [Create a Standalone R Server](../../advanced-analytics/r-services/create-a-standalone-r-server.md).  

+ Use the new standalone Windows installer to create a brand new instance of Microsoft R Server that uses the Microsoft Modern Software Lifecycle support policy. For more information, see [Run Microsoft R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows).

+ If you have an existing instance of R Server (Standalone) or R Services that you want to upgrade, you must also download and run the  Windows-based installer for the update. For more information, see [Run Microsoft R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows).
  
## Install Additional R Tools  

 We recommend the free [Microsoft R Client](http://aka.ms/rclient/download) (download).  

 You can also use your preferred R development environment to develop solutions for SQL Server R Services or Microsoft R Server. For details, see [Setup or Configure R Tools](../../advanced-analytics/r-services/setup-or-configure-r-tools.md). 
 

### Location of R Server Binaries

Depending on the method you use to install Microsoft R Server, the default location is different. Before you start using your favorite development environment, verify where you installed the R libraries:

+ Microsoft R Server installed using the new Windows installer

  `C:\Program Files\Microsoft\R Server\R_SERVER`

+ R Server (Standalone) installed via SQL Server setup

  `C:\Program Files\Microsoft SQL Server\130\R_SERVER`

+ R Services (In-Database)

  `C:\Program Files\Microsoft SQL Server\<instance_name>\R_SERVICES`
      
## Start Using R on Microsoft R Server  

 After you have set up the server components and configured your R IDE to use the R Server binaries, you can begin developing your solution using the new APIs, such as the RevoScaleR package, MicrosoftML, and olapR.
    
To get started with R Server, see this guide in the MSDN Library: [R Server - Getting Started](https://msdn.microsoft.com/microsoft-r/microsoft-r-get-started-node)   
  
-   [ScaleR](https://msdn.microsoft.com/microsoft-r/scaler-getting-started): Explore this collection of distributable analytical functions that provide high performance and scaling to R solutions. Includes parallelizable versions of many of the most popular R modeling packages, such as k-means clustering, decision trees, and decision forests, and tools for data manipulation. For more information, see [Explore R and ScaleR in 25 Functions](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started-tutorial)  
    
- [MicrosoftML](https://msdn.microsoft.com/library/mt790482.aspx): The MicrosoftML package is a set of new machine learning algorithms and transformations developed at Microsoft that are fast and scalable. For more information, see [MicrosoftML functions](https://msdn.microsoft.com/microsoft-r/microsoftml/microsoftml).
  


  
## See Also  
 [Getting Started with SQL Server R Services](../../advanced-analytics/r-services/getting-started-with-sql-server-r-services.md)  
  
  

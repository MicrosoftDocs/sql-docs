---
title: "Set Up  a Data Science Client | Microsoft Docs"
ms.custom: ""
ms.date: "02/10/2017"
ms.prod: "r-server"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d15ee956-918f-40e0-b986-2bf929ef303a
caps.latest.revision: 14
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Set Up  a Data Science Client
  After you have configured an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by installing **R Services (In-Database)**, you'll want to set up an R development environment that is capable of connecting to the server for remote execution and deployment. 
  
  This environment must include the ScaleR packages, and optionally con include a client development environment.
  
 ## Where to Get ScaleR 
  
  Your client environment must include Microsoft R Open, as well as the additional RevoScaleR packages that support distributed execution of R on SQL Server.  There are several ways you can install these packages:
  
+ Install [Microsoft R Client](http://aka.ms/rclient/download). Additional setup instructions are provided here: [Get Started with Microsoft R Client](https://msdn.microsoft.com/microsoft-r/r-client-get-started)
+ Install Microsoft R Server. You can get Microsoft R Server from SQL Server setup, or by using the new standalone Windows installer. For more information, see [Create a Standalone R Server](../../advanced-analytics/r-services/create-a-standalone-r-server.md) and [Introduction to R Server](https://msdn.microsoft.com/microsoft-r/rserver).

If you have an R Server licensing agreement, we recommend the use of Microsoft R Server (Standalone), to avoid limitations on R processing threads and in-memory data.


## How to Set Up the R Development Environment

You can use any R development environment of your choice that is compatible with Windows. 

+ R Tools for Visual Studio supports integration with Microsoft R Open
+ RStudio is a popular free environment  

After installation, you will need to reconfigure your environment to use the Microsoft R Open libraries by default, or you will not have access to the ScaleR libraries. For more information, see [Getting Started with Microsoft R Client](http://msdn.microsoft.com/microsoft-r/r-client-get-started).
 
## More Resources
  
 For a detailed walkthrough of how to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance for remote execution of R code, see this tutorial: [Data Science Deep Dive: Using the RevoScaleR Packages](../../advanced-analytics/r-services/data-science-deep-dive-using-the-revoscaler-packages.md).  
 

To start using Microsoft R Client and the ScaleR packages with SQL Server, see  [ScaleR Getting Started](https://msdn.microsoft.com/microsoft-r/scaler-getting-started#).  
  
## See Also  
 [Set up SQL Server R Services &#40;In-Database&#41;](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md)  
  
  

---
title: "SQL Server R Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "12/16/2016"
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
# SQL Server R Services
  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] provides a platform for developing and deploying intelligent applications that uncover new insights. You can use the rich and powerful R language and the many packages from the community to create models and generate predictions using your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data. Because [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] integrates the R language with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can keep analytics close to the data and eliminate the costs and security risks associated with data movement.  
  
 [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] supports the open source R language with a comprehensive set of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools and technologies that offer superior performance, security, reliability and manageability. You can deploy R solutions using convenient, familiar  tools, and your production applications can call the R runtime and retrieve predictions and visuals using [!INCLUDE[tsql](../../includes/tsql-md.md)]. You also get the [ScaleR](https://msdn.microsoft.com/microsoft-r/scaler/scaler) libraries to improve the scale and performance of your R solutions.  
  
Through SQL Server setup, you can install both server and client components.  
  
+   **R Services (In-Database):** Install this feature during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable secure execution of R scripts on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.  
  
     When you select this feature, extensions  are installed in the database engine to support execution of R scripts, and a new service is created, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], to manage communications between the R runtime and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
+   **Microsoft R Server (Standalone):** A distribution of open source R combined with proprietary packages that support parallel processing and other performance improvements. Both R Services (In-Database) and Microsoft R Server (Standalone) include the base R runtime and packages, plus the **ScaleR**  libraries for enhanced connectivity and performance. 
  
+    [Microsoft R Client](https://msdn.microsoft.com/microsoft-r/index#mrc)  is available as a separate, free installer.  You can use Microsoft R Client to develop solutions that can be deployed to R Services running on SQL Server, or to Microsoft R Server running on Windows, Teradata, or Hadoop. 
     

  > [!NOTE]
  >  If you need to run your R code in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], be sure to install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] as described [here](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md).
  >  
  > Microsoft R Server \(Standalone\) is a separate option designed for using the ScaleR libraries on a Windows computer that is not running SQL Server. 
  >   
  >  However, if you have Enterprise Edition, we recommend that you install Microsoft R Server \(Standalone\) on a laptop or other computer used for R development, to create R solutions that can easily be deployed to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is running R Services \(In-Database\).
  
## Additional Resources  
  
 [Getting Started with SQL Server R Services](../../advanced-analytics/r-services/getting-started-with-sql-server-r-services.md)   
 Describes common scenarios for uses of R with SQL Server.  
  
[Set Up SQL Server R Services In-Database](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md)  
Install R and associated database components as part of SQL Server setup.  
  
[SQL Server R Services Tutorials](../../advanced-analytics/r-services/sql-server-r-services-tutorials.md)  
Learn how to create SQL Server data sources in your R code, and how to use remote compute contexts. Other tutorials aimed at SQL developers demonstrate how to train and deploy an R model in SQL Server.  
  
## See Also  
  
 [Getting Started with Microsoft R Server &#40;Standalone&#41;](../../advanced-analytics/r-services/getting-started-with-microsoft-r-server-standalone.md)  
 
 [Set up a Standalone R Server)](../../advanced-analytics/r-services/create-a-standalone-r-server.md) 
  

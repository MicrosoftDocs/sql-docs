---
title: "Managing and Monitoring R Solutions | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/24/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d455f22a-190f-4a28-9088-98a843cd5db2
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Managing and Monitoring R Solutions
  Database administrators must integrate competing projects and priorities into a single point of contact: the database server. They must provide data access not just to data scientists but to a variety of report developers, business analysts, and business data consumers, while maintaining the health of operational and reporting data stores. In the enterprise, the DBA is a critical part of building and deploying an effective infrastructure for data science. [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] provides many benefits to the database administrator who supports the data science role.  
  
-   **Security.** The architecture of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] keeps your databases secure and isolates the execution of R sessions from the operation of the database instance .  
  
     You can specify who has permission to execute the R scripts and ensure that the data used in R jobs is managed using the same security roles that are defined in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **Reliability.** R sessions are executed in a separate process to ensure that your server continues to run as usual even if the R session encounters issues. Low privilege physical user accounts are used to contain and isolate R instances.   
  
-   **Resource governance.** You can control the amount of resources allocated to the R runtime, to prevent massive computations from jeopardizing the overall server performance.  
  
  
## In This Section  
 [Monitoring R Services](../../advanced-analytics/r-services/monitoring-r-services.md)
 
 [Resource Governance for R Services](../../advanced-analytics/r-services/resource-governance-for-r-services.md)
 
[Installing and Managing R Packages](../../advanced-analytics/r-services/installing-and-managing-r-packages.md)
  
[Configuration](../../advanced-analytics/r-services/configuration-sql-server-r-services.md) 

+ [Configure and Manage Advanced Analytics Extensions](../../advanced-analytics/r-services/configure-and-manage-advanced-analytics-extensions.md)  
  
+  [Modify the User Account Pool for SQL Server R Services](../../advanced-analytics/r-services/modify-the-user-account-pool-for-sql-server-r-services.md)  

 [Security Considerations for the R Runtime in SQL Server](../../advanced-analytics/r-services/security-considerations-for-the-r-runtime-in-sql-server.md)  
  
 
  
## See Also  
 [SQL Server R Services Features and Tasks](../../advanced-analytics/r-services/sql-server-r-services-features-and-tasks.md)  
  
  

---
title: "R Server (Standalone) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/07/2017"
ms.prod: "r-server"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: ca9e48f1-67b8-4905-9b78-56752d7a4e81
caps.latest.revision: 21
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# R Server (Standalone)
  **Microsoft R Server (Standalone)** is  part of the enterprise-class analytics platform provided by Microsoft R Services.  It extends R by providing support, scalability, and security. [!INCLUDE[rsql_platform](../../includes/rsql-platform-md.md)] also addresses the in-memory limitations of open source R by adding parallel and chunked processing of data, enabling users to analyze data at enterprise scale, and use data much bigger than can fit in memory.  
 
 You can use Microsoft R Server on multiple platforms. For more information, see these resources in the MSDN library:  

+ [Introducing Microsoft R Server](https://msdn.microsoft.com/microsoft-r/rserver)
+ [R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows)

You can also use Microsoft R Server as a development client, to get the RevoScaleR libraries and tools needed to create R solutions that can be deployed to SQL Server.
  
  
## How to Get Microsoft R Server  
 
 There are multiple options for installing Microsoft R Server, depending on whether you need to use SQL Server data, and how often you want to get updates:
 
+ **Microsoft R Server (Standalone)**: Run the SQL Server setup wizard and choose this option if you want to set up a development machine for enterprise R solutions. You can run your solutions locally, on the computer where you install the Standalone instance, or you can connect to a SQL Server instance using SQL Server R Services and run the solution with the server as the compute context. 

    When you install Microsoft R Server (Standalone), the instance is licensed via the SQL Server Enterprise Edition support policy. This option offers updates and customer support over a longer period, with feature updates tied to the SQL Server release schedule. This ensures that the applications you create using Microsoft R Server are always compatible with the instance of SQL Server that is running R Services (In-Database). For more information,  see [Create a Standalone R Server](../../advanced-analytics/r-services/create-a-standalone-r-server.md).  
    
    You do not need to install Microsoft R Server (Standalone) on a computer that already has SQL Server R Services installed. 

+ **Microsoft R Server with the Modern Software Lifecycle support policy**: You can now use a new Windows installer to set up a new instance of Microsoft R Server. Set up Microsoft R Server using this option if you do not need to use in-database analytics, or if you want more frequent updates using the Microsoft Modern Software Lifecycle policy. 

    Although licensing for this server edition requires a SQL Server Enterprise agreement, you are not tied to the SQL Server release cycle. This ensures that you get more frequent updates of R. For more information about this edition of Microsoft R server, see [Run Microsoft R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows).
 
    By using this installer, you can also convert an existing instance of SQL Server R Services, to bind it to the Microsoft Modern Software Lifecycle support policy, and thus get more frequent updates. To perform an update requires that you have an existing installation that is compatible. For more information, see [Using SqlBindR to Upgrade an Instance of R Services](../../advanced-analytics/r-services/use-sqlbindr-exe-to-upgrade-an-instance-of-r-services.md).
 
    
+ **Azure virtual machines with R Server**: The Azure marketplace includes multiple virtual machine images that include R Server. Creating a new Azure virtual machine is the fastest way to set up a server for operationalizing predictive models. Some images come with scaling and sharing features (formerly known as DeployR) already configured, which makes it easier to embed R analytics inside applications and integrate R with backend systems. For more information, see [Provision an R Server Virtual Machine](../../advanced-analytics/r-services/provision-the-r-server-only-sql-server-2016-enterprise-vm-on-azure.md)
  
For samples, tutorials, and more information about Microsoft R, see [Microsoft R Products](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started).   
  
## See Also  
 [SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md)   
 [Making R the Enterprise Standard for Cross Platform Analytics On-Premise and in the Cloud](http://blogs.technet.com/b/machinelearning/archive/2016/01/12/making-r-the-enterprise-standard-for-cross-platform-analytics-both-on-premises-and-in-the-cloud.aspx)  
  
  

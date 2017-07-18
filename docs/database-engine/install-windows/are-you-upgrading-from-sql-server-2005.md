---
title: "Are you upgrading from SQL Server 2005? | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "07/18/2017"
ms.prod: 
 - "sql-server-2016"
 - "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "setup-install"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: ad40e66f-71fe-4ee6-9ce3-17127e7b1d7a
caps.latest.revision: 21
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Are you upgrading from SQL Server 2005?
 The end of extended support for SQL Server 2005 is one reason to upgrade now to a newer version of SQL Server and to Azure SQL Database. Upgrading enables you to maintain security and compliance, achieve breakthrough performance, and optimize your data platform infrastructure.  
  
 For more info, guidance, and tools to plan and automate your upgrade or migration, see [SQL Server 2005 end of support](http://www.microsoft.com/en-us/server-cloud/products/sql-server-2005/).  
  
## Why upgrade?  
  
> [!IMPORTANT]  
>  Extended support for SQL Server 2005 ends on April 12, 2016. If you're still running SQL Server 2005 after April 12, 2016, you'll no longer receive security updates.  
  
## Choose your upgrade option  
If you’re upgrading relational databases from SQL Server 2005, here are your options for relational storage on the Microsoft platform.  
  
To see a more comprehensive analysis of these options, [click here](http://sql05upgrade.azurewebsites.net/).  
  
|Relational storage option|Benefits|Other factors to consider|  
|-------------------------------|--------------|-------------------------------|  
|**SQL Server on premises**<br /><br /> Consider this option for database applications of any kind, from transactional systems to data warehouses.|You have the most control over features and scalability because you manage both hardware and software.<br /><br /> If you’re upgrading from SQL Server 2005, this is the most similar environment.|You have to make the biggest up-front investment and provide the most ongoing management, because you have to buy, maintain, and manage your own hardware and software.<br /><br /> For more information, see [SQL Server](https://www.microsoft.com/EN-US/server-cloud/products/sql-server-2017/).|  
|**SQL Server hosted on Azure virtual machines**<br /><br /> Consider this option if you want the following:<br /><br /> Benefits of migrating to a hosted environment.<br /><br /> Control over the operating environment.<br /><br /> Familiar feature set of SQL Server.|You can deploy quickly from a library of virtual machine images.<br /><br /> You get the full SQL Server feature set.<br /><br /> You save the cost of hardware and of server software. You pay only for hourly usage.|You have to configure and manage both the SQL Server and the operating system software.<br /><br /> <br /><br /> For more information, see [SQL Server on Azure Virtual Machines overview](https://azure.microsoft.com/documentation/articles/virtual-machines-sql-server-infrastructure-services/).<br /><br /> For info about migrating, see [Migrate a database to SQL Server on an Azure VM](https://azure.microsoft.com/documentation/articles/virtual-machines-migrate-onpremises-database/).|  
|**Azure SQL Database hosted database service**<br /><br /> Consider this option if you want a lower-cost solution with less maintenance.<br /><br /> This option is particularly well suited for apps that don’t require the same capacity at all times, or that have to provide external access.|You can deploy quickly and scale up easily.<br /><br /> You pay only for hourly usage.<br /><br /> The cost of the service includes not only storage, but high availability and automated backups.|Azure SQL Database lacks some SQL Server features that are not applicable in a hosted cloud environment. For more info, see [Azure SQL Database Transact-SQL information](https://azure.microsoft.com/documentation/articles/sql-database-transact-sql-information/).<br /><br /> Azure SQL Database also has a maximum database size of 500 GB, compared to 524 PB for SQL Server.<br /><br /> For more information, see [SQL Database](https://azure.microsoft.com/services/sql-database/).<br /><br /> For information about migrating, see [Migrating a SQL Server database to Azure SQL Database](https://azure.microsoft.com/documentation/articles/sql-database-cloud-migrate/).|  
  
 You may also want to consider a non-relational or NoSQL solution for certain data and applications.  
  
|Non-relational solution|Benefits|  
|------------------------------|--------------|  
|**Azure Cosmos DB**<br /><br /> Consider this option for modern, scalable, mobile and web applications that use JSON data and require a combination of robust querying and transactional data processing.<br /><br /> For more info, see [Cosmos DB](http://azure.microsoft.com/services/cosmos-db/).<br /><br /> For info about importing data, see [Import data to Cosmos DB](http://docs.microsoft.com/azure/cosmos-db/import-data/).|Your documents are indexed and you can use familiar SQL syntax to query them.<br /><br /> The database is schema-free.<br /><br /> You can add properties to documents without having to rebuild indexes.<br /><br /> You get JSON and JavaScript support right inside the database engine.<br /><br /> You get native support for geospatial data and integration with other Azure Services including Azure Search, HDInsight, and Data Factory.<br /><br /> You get low latency, high performance storage with reserved throughput levels.|  
|**Azure table storage**<br /><br /> Consider this option to store petabytes of semi-structured data in a cost-effective solution.<br /><br /> For more info, see [Table Storage](https://azure.microsoft.com/services/storage/tables/).|You can evolve your apps and your table schema without taking the data offline.<br /><br /> You can scale up without sharding your dataset.<br /><br /> You get geo-redundant storage that replicates data across multiple regions.|  
  
## Plan your upgrade  
  
-   Read about how to plan your upgrade in the following series of blog posts from the SQL Server team.  
  
    -   [Planning an efficient upgrade from SQL Server 2005: Step 1 of 3](http://blogs.technet.com/b/dataplatforminsider/archive/2015/12/10/planning-an-efficient-upgrade-from-sql-server-2005-step-1-of-3.aspx)  
  
    -   [Planning an efficient upgrade from SQL Server 2005: Step 2 of 3](http://blogs.technet.com/b/dataplatforminsider/archive/2015/12/15/planning-an-efficient-upgrade-from-sql-server-2005-step-2-of-3.aspx)  
  
    -   [Planning an efficient upgrade from SQL Server 2005: Step 3 of 3](http://blogs.technet.com/b/dataplatforminsider/archive/2015/12/17/planning-an-efficient-upgrade-from-sql-server-2005-step-3-of-3.aspx)  
  
-   Review the requirements and considerations under [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md), including the [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   Read about how to upgrade.  
  
    -   Review the available upgrade methods and learn how to plan and test in the topic [Upgrade Database Engine](../../database-engine/install-windows/upgrade-database-engine.md).  
  
        > [!IMPORTANT]  
        >  You can't upgrade a SQL Server 2005 instance to a SQL Server 2017 server in place. You have to install an instance of SQL Server 2017, then migrate your SQL Server 2005 databases to the new installation. For more info, see the section "New Installation Upgrade" in the topic [Choose a Database Engine Upgrade Method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md).  
   
  
-   For more info, guidance, and tools to plan and automate your upgrade or migration, see [SQL Server 2005 end of support](http://www.microsoft.com/en-us/server-cloud/products/sql-server-2005/).  
  
## Get SQL Server  
 To download an evaluation copy of SQL Server, [click here](http://www.microsoft.com/evalcenter/evaluate-sql-server-2016).  
  
## Next Steps Also  
 [SQL Server 2017](http://www.microsoft.com/sql-server/sql-server-2017)   
 [SQL Server 2005 end of support](http://www.microsoft.com/en-us/server-cloud/products/sql-server-2005/)   
  
  
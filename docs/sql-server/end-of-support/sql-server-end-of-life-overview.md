---
title: "End of support options"
description: Learn about the different options available for the end of support, and end of life for your SQL Server products, such as SQL Server 2005, SQL Server 2008, and SQL Server 2008 R2. 
ms.custom: ""
ms.date: "12/09/2019"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
ms.reviewer: pmasl
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---
# SQL Server end of support options 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
 
Each version of SQL Server is backed by a minimum of 10 years support, which includes 5 years in mainstream support, and five years in extended support, including regular security updates. End of support means the end of security updates, which can cause security and compliance issues as well as put applications and their business at risk. Additionally, support options are limited for customers who call into Microsoft Support for a product that has reached the end of its support cycle. 

Once your SQL Server has reached the end of its support lifecycle, you have the following options:
- Upgrade to a current version of SQL Server for the most advanced security, performance and innovation. 
- Purchase an [Extended Security Update subscription](sql-server-extended-security-updates.md) for your servers and remain protected until you are ready to upgrade to a newer version of SQL Server, for up to three years after the end of support date. 
- [Migrate your existing SQL Server 2008 and 2008 R2 workloads as-is to Azure virtual machines](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-2008-eos-extend-support) to automatically subscribe to an additional three years of Extended Security Updates at no additional cost apart from the cost of the virtual machine. 
- Migrate your workload to an Azure cloud service such as to an Azure SQL Database single database or managed instance. 


  
For more information, guidance, and tools to plan and automate your upgrade or migration, see [SQL Server 2005 end of support](https://www.microsoft.com/sql-server/sql-server-2005) and [SQL Server 2008 end of support](https://www.microsoft.com/cloud-platform/windows-sql-server-2008).  

## Stay on-premises 

If you would like to maintain your current system and stay on-premises, you can either choose to upgrade your SQL Server to a newer and supported version, or you can purchase Extended Security Updates for up to three years after the end of the support date. 




  
## Why upgrade?  
  
> [!IMPORTANT]  
>  Extended support for SQL Server 2005 ended on April 12, 2016. If you're still running SQL Server 2005 after April 12, 2016, you no longer receive security updates.  

> [!IMPORTANT]  
>  Extended support for SQL Server 2008 and 2008r2 ended on July 09, 2019. If you're still running SQL Server 2008 or 2008R2 after July 09, 2019, you will no longer receive security updates. More information can be found in the blog [Announcing new options for SQL Server 2008](https://azure.microsoft.com/blog/announcing-new-options-for-sql-server-2008-and-windows-server-2008-end-of-support/). To extend support for free, you can migrate your SQL Server to an Azure VM. For more information see [Extend support for SQL Server 2008 and 2008 R2 with Azure](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-2008-eos-extend-support).  
  
## Choose your upgrade option  
If you're upgrading relational databases from an older version of SQL Server, here are your options for relational storage on the Microsoft platform.  
  
To see a more comprehensive analysis of these options, see [PaaS vs IaaS](/azure/sql-database/sql-database-paas-vs-sql-server-iaas).  
  
|Relational storage option|Benefits|Other factors to consider|  
|-------------------------------|--------------|-------------------------------|  
|**SQL Server hosted on Azure virtual machines**<br /><br /> Consider this option if you want the following:<br /><br /> Benefits of migrating to a hosted environment.<br /><br /> Control over the operating environment.<br /><br /> Familiar feature set of SQL Server.|**You can [extend support](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-2008-eos-extend-support) for SQL Server 2008 and 2008 R2 for free for up to 3 years.** <br /><br /> You can deploy quickly from a library of virtual machine images.<br /><br /> You get the full SQL Server feature set.<br /><br /> You save the cost of hardware and of server software. You pay only for hourly usage.|You have to manage both SQL Server and the operating system software.<br /><br /> <br /><br /> For more information, see [SQL Server on Azure Virtual Machines overview](https://azure.microsoft.com/documentation/articles/virtual-machines-sql-server-infrastructure-services/).<br /><br /> For info about migrating, see [Migrate a database to SQL Server on an Azure VM](https://azure.microsoft.com/documentation/articles/virtual-machines-migrate-onpremises-database/).|  
|**Azure SQL Database managed instance (PaaS)** <br /><br /> Consider this option if you want a lower-cost solution with less maintenance.<br /><br /> A managed instance is similar to an instance of the Microsoft SQL Server database engine, offering shared resources for databases and additional instance-scoped features. <br /><br />Managed instance supports database migration from on-premises with minimal to no database change.|Get the benefit of cross-database queries within the same managed instance, as well as CLR and SQL job support. <br /><br /> 99.995% availability guaranteed.<br /><br /> The cost of the service includes not only storage, but high availability, patching and automated backups.|There are some Transact-SQL (T-SQL) differences between Azure SQL Database managed instance and SQL Server on-premises. For more info, see [Azure SQL Database managed instance T-SQL information](/azure/sql-database/sql-database-managed-instance-transact-sql-information).<br /><br /> For more information about SQL Database managed instance, see [Azure SQL Database managed instance overview](/azure/sql-database/sql-database-managed-instance-index) and [Azure SQL Database managed instance capabilities](/azure/sql-database/sql-database-managed-instance).<br /><br /> For information about migrating, see [Migrating a SQL Server to Azure SQL Database managed instance](/azure/sql-database/sql-database-managed-instance-migrate).|  
|**Azure SQL Database single database or elastic pool (PaaS)** <br /><br /> Consider this option if you want a lower-cost solution with less maintenance.<br /><br /> This option is particularly well suited for cloud-designed applications when developer productivity and fast time-to-market for new solutions are critical,  or that have to provide external access. <br /><br />The most commonly used SQL Server features are available, but not as many as for an Azure SQL Database managed instance. |You can deploy quickly and scale up easily.<br /><br /> 99.995% availability guaranteed.<br /><br /> You can pay for usage by the second or the hour. <br /><br /> The cost of the service includes not only storage, but high availability patching, and automated backups.|There are some Transact-SQL (T-SQL) differences between Azure SQL Database and SQL Server on-premises. For more info, see [Azure SQL Database Transact-SQL information](https://azure.microsoft.com/documentation/articles/sql-database-transact-sql-information/).<br /><br /> Azure SQL Database also has a maximum database size of 100 TB, compared to 524 PB for SQL Server. For more information, see [Resource limits for single databases](https://docs.microsoft.com/azure/sql-database/sql-database-vcore-resource-limits-single-databases)<br /><br /> For more information about SQL Database, see [Azure SQL Database overview](https://azure.microsoft.com/services/sql-database/) and [Azure SQL Database documentation](/azure/sql-database/sql-database-technical-overview).<br /><br /> For information about migrating, see [Migrating a SQL Server database to Azure SQL Database](/azure/sql-database/sql-database-single-database-migrate).|  
|**SQL Server on premises**<br /><br /> Consider this option for database applications of any kind, from transactional systems to data warehouses.|You have the most control over features and scalability because you manage both hardware and software.<br /><br /> If you're upgrading from an older instance of SQL Server, this is the most similar environment.|You have to make the biggest up-front investment and provide the most ongoing management, because you have to buy, maintain, and manage your own hardware and software.<br /><br /> For more information, see [SQL Server](https://www.microsoft.com/evalcenter/evaluate-sql-server-2017-rtm).|  

You may also want to consider a non-relational or NoSQL solution for certain data and applications.  
  
|Non-relational solution|Benefits|  
|------------------------------|--------------|  
|**Azure Cosmos DB**<br /><br /> Consider this option for modern, scalable, mobile, and web applications that use JSON data and require a combination of robust querying and transactional data processing.<br /><br /> For more info, see [Cosmos DB](https://azure.microsoft.com/services/cosmos-db/).<br /><br /> For info about importing data, see [Import data to Cosmos DB](https://docs.microsoft.com/azure/cosmos-db/import-data/).|Your documents are indexed and you can use familiar SQL syntax to query them.<br /><br /> The database is schema-free.<br /><br /> You can add properties to documents without having to rebuild indexes.<br /><br /> You get JSON and JavaScript support right inside the database engine.<br /><br /> You get native support for geospatial data and integration with other Azure Services including Azure Search, HDInsight, and Data Factory.<br /><br /> You get low latency, high-performance storage with reserved throughput levels.|  
|**Azure table storage**<br /><br /> Consider this option to store petabytes of semi-structured data in a cost-effective solution.<br /><br /> For more info, see [Table Storage](https://azure.microsoft.com/services/storage/tables/).|You can evolve your apps and your table schema without taking the data offline.<br /><br /> You can scale up without sharding your dataset.<br /><br /> You get geo-redundant storage that replicates data across multiple regions.|  
  
## Plan your upgrade  
  
-   Read about how to plan your upgrade your SQL Server 2005 instance in the following series of blog posts from the SQL Server team. 
    - Planning an efficient upgrade from SQL Server 2005: [Step 1 of 3](https://blogs.technet.com/b/dataplatforminsider/archive/2015/12/10/planning-an-efficient-upgrade-from-sql-server-2005-step-1-of-3.aspx), [Step 2 of 3](https://blogs.technet.com/b/dataplatforminsider/archive/2015/12/15/planning-an-efficient-upgrade-from-sql-server-2005-step-2-of-3.aspx), [Step 3 of 3](https://blogs.technet.com/b/dataplatforminsider/archive/2015/12/17/planning-an-efficient-upgrade-from-sql-server-2005-step-3-of-3.aspx)
- Prepare for [SQL Server 2008 end of support](https://www.microsoft.com/sql-server/sql-server-2008).
  
-   Review the requirements and considerations under [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md), including the [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   Read about how to upgrade.  
  
    -   Review the available upgrade methods and learn how to plan and test in the article [Upgrade Database Engine](../../database-engine/install-windows/upgrade-database-engine.md).  
  
        > [!IMPORTANT]  
        >- You can't upgrade a SQL Server 2005 instance to a SQL Server 2017 server in place. You have to install an instance of SQL Server 2017, then migrate your SQL Server 2005 databases to the new installation. For more info, see the section "New Installation Upgrade" in the article [Choose a Database Engine Upgrade Method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md).  
        >- It is possible to upgrade SQL 2008 and SQL 2008r2 in place to SQL 2017. For more information, see [Supported version and edition upgrades](supported-version-and-edition-upgrades-2017.md). 


-    For more info, guidance, and tools to plan and automate your upgrade or migration, see [SQL Server 2005 end of support](https://www.microsoft.com/sql-server/sql-server-2005) and [SQL Server 2008 end of support](https://www.microsoft.com/cloud-platform/windows-sql-server-2008).  
  

## Lifecycle dates

The following table provides an approximation of lifecycle dates for SQL Server products. For greater details and accuracy, see the [Microsoft Lifecycle Policy](https://support.microsoft.com/hub/4095338/microsoft-lifecycle-policy) page. 

| **Version**     | **Release year** | **Mainstream Support end year** | **Extended Support end year** |
| :---------------| :--------------- |:------------------------------- |
| [SQL Server 2019](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202019) | 2019 | 2025 | 2030 |
| [SQL Server 2017](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202017) | 2017 | 2022 | 2027 |
| [SQL Server 2016](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202016) | 2016 | 2021 | 2026 |
| [SQL Server 2014](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202014) | 2014 | 2019 | 2024 |
| [SQL Server 2012](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202012) | 2012 | 2017 | 2022 |
| [SQL Server 2008 R2](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202008%20R2) | 2010 | 2012 | [2019](https://www.microsoft.com/sql-server/sql-server-2008) |
| [SQL Server 2008](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202008) | 2008 | 2012 | [2019](https://www.microsoft.com/sql-server/sql-server-2008) |
| [SQL Server 2005](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202005) | 2006 | 2011 | [2016](https://www.microsoft.com/sql-server/sql-server-2005) |
| [SQL Server 2000](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202000) | 2000 | 2005 | [2013](https://blogs.technet.microsoft.com/cdnitmanagers/2012/12/06/sql-server-2000-end-of-support-april-2013/) |

  > [!IMPORTANT]
  > If any discrepancy exists between this table, and the Microsoft Lifecycle page, then the Microsoft Lifecycle supersedes this table, as this table is meant to be used as an approximate reference.  



## Get SQL Server  
 To download an evaluation copy of SQL Server, see [SQL Server downloads](https://www.microsoft.com/sql-server/sql-server-downloads).  
  
## Next Steps  
 [SQL Server 2017](https://www.microsoft.com/sql-server/sql-server-2017)   
 [SQL Server 2005 end of support](https://www.microsoft.com/sql-server/sql-server-2005)   
 [SQL Server 2008 end of support](https://www.microsoft.com/cloud-platform/windows-sql-server-2008)
  
  

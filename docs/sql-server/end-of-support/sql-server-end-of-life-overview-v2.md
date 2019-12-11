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

Once your SQL Server has reached the end of its support lifecycle, you can choose to:
- Upgrade to current version of SQL Server.
- Purchase an [Extended Security Updates subscription](https://www.microsoft.com/en-us/cloud-platform/extended-security-updates). 
- Migrate your workload to an Azure virtual machine as-is for [free Extended Security Updates](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-2008-eos-extend-support).
- Migrate your workload to an [Azure SQL Database service](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-paas-vs-sql-server-iaas). 


For more information, guidance, and tools to plan and automate your upgrade or migration, see [SQL Server 2005 end of support](https://www.microsoft.com/sql-server/sql-server-2005) and [SQL Server 2008 end of support](https://www.microsoft.com/cloud-platform/windows-sql-server-2008).  

## Upgrade SQL Server

The first option is to upgrade your existing SQL Server to a newer and supported version of SQL Server. 

### Benefits
- Get the most out of innovation by taking advantage of the latest features and security improvements. 
- You have the most control over features and scalability because you manage both hardware and software.
- If you're upgrading from an older instance of SQL Server, this is the most similar environment.
- Applicable for database applications of any kind, including OLTP systems and data warehousing. 


### Considerations
0
- You have to make the biggest up-front investment and provide the most ongoing management, because you have to buy, maintain, and manage your own hardware and software.
- There could be downtime, plus the inherent risk of running into issues during an in-place upgrade process.
- If you're on Windows 2008 or 2008 R2, you will also need to upgrade the OS as the newer versions of SQL may not be supported on Windows 2008 or 2008 R2. There is added risk during the OS upgrade process, so doing a side-by-side migration may be the more prudent, yet more costly, approach. Additionally, in-place OS upgrades are not supported on failover cluster instances.

### Resources

- What's new in [SQL Server 2016](../what-s-new-in-sql-server-2016.md), [SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md), and [SQL Server 2019](/sql-server/what-s-new-in-sql-server-ver15). 
- [Upgrade SQL Server using Installation Wizard](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)
- Hardware requirements for [SQL Server 2017 and prior](../install/hardware-and-software-requirements-for-installing-sql-server.md) and [SQL Server 2019](../install/hardware-and-software-requirements-for-installing-sql-server-ver15.md). 
- Supported version and edition upgrades for [SQL Server 2016](../../database-engine/install-windows/supported-version-and-edition-upgrades.md), [SQL Server 2017](../../database-engine/install-windows/supported-version-and-edition-upgrades-2017.md), and [SQL Server 2019](../../database-engine/install-windows/supported-version-and-edition-upgrades-2019.md)
- Tools:
    - [Database Experimentation Assistant](../../dea/database-experimentation-assistant-overview.md) can help evaluate the target version of SQL Server for a specific workload. 
    - [Data Migration Assistant](../../dma/dma-overview.md) can help detect compatibility issues that can impact database functionality in your new version of SQL Server. 


|            |          |          |          | 
|:---------- | :------- | :--------| :------- | 
| What's new | [SQL Server 2016](../what-s-new-in-sql-server-2016.md) | [SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md) | [SQL Server 2019](/sql-server/what-s-new-in-sql-server-ver15) | 
|Supported version & edition upgrades | [SQL Server 2016](../../database-engine/install-windows/supported-version-and-edition-upgrades.md) | [SQL Server 2017](../../database-engine/install-windows/supported-version-and-edition-upgrades-2017.md) | [SQL Server 2019](../../database-engine/install-windows/supported-version-and-edition-upgrades-2019.md) | 
| Hardware requirements | [SQL Server 2017 and prior](../install/hardware-and-software-requirements-for-installing-sql-server.md) | [SQL Server 2019](../install/hardware-and-software-requirements-for-installing-sql-server-ver15.md) | 
| Tools | [Database Experimentation Assistant](../../dea/database-experimentation-assistant-overview.md) can help evaluate the target version of SQL Server for a specific workload.  | [Data Migration Assistant](../../dma/dma-overview.md) can help detect compatibility issues that can impact database functionality in your new version of SQL Server. | 
| [Upgrade SQL Server using Installation Wizard](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md) | 

## Extend support 

### Benefits

### Considerations

## Azure virtual machine

### Benefits

### Considerations


## Azure SQL Database single database

### Benefits

### Considerations

## Azure SQL Database managed instance

### Benefits

### Considerations

 

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
  
  

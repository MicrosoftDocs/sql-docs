---
title: Restore database to SQL Server
titleSuffix: Azure SQL Managed Instance
description: Learn how to restore a database to SQL Server 2022 from Azure SQL Managed Instance
author: danimir
ms.author: danil
ms.reviewer: mathoma, danil
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
ms.custom: 
---
# Restore database to SQL Server with backup portability (Azure SQL Managed Instance)
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you to restore your database backup from your managed instance to SQL Server 2022 by using the backup portability feature of Azure SQL Managed Instance. 


## Overview

The backup portability feature gives you an easy way to copy or move your databases from SQL Managed Instance to SQL Server instances hosted on-premises, virtual machines in Azure, or in other clouds. The backup portability feature is enabled by default for all new and existing instances starting November 2022, and it's possible to opt out at any time. 

The backup portability feature unlocks the following scenarios: 

- Disaster recovery with two-way failover between Azure SQL Managed Instance and SQL Server 2022 (currently in preview). Review [Managed Instance link](managed-instance-link-feature-overview.md) to learn more. 
- Hybrid and multi-cloud mobility
- Migration roll back to SQL Server, if necessary. 
- Leveraging cloud elasticity during peak season - seasonal migrations to and from Azure. 
- Refresh non-production environments outside of SQL Managed Instance. 
- Provide database copies to end customers and auditors. 
- Restore to SQL Server as an insurance policy. 

To ensure backup compatibility with SQL Server, instances with the backup portability feature enabled may not have access to some newly introduced SQL Server features that impact the database engine and would break backup file compatibility with SQL Server. See [limitations](#limitations) for more details.  If you anticipate the need to copy your database from SQL Managed Instance to other SQL Server form factors, for operational, contractual, or regulatory reasons, your instance is a good candidate for keeping the backup portability feature enabled. 

For more details, check the [backup and restore](frequently-asked-questions-faq.yml#backup-and-restore) section of the frequently asked questions article. 

## Take backup on SQL Managed Instance 


## Restore to SQL Server 




## Limitations

Consider the following limitations:

- Once the backup portability feature has been disabled for an instance, it's no longer possible to enable it again on that instance. 
- When restoring to SQL Server, you must use the 'WITH MOVE` qualifier, and provide explicit paths for the data files. 
- Databases backed up with service-managed TDE keys are not supported with the backup portability feature, and cannot be restored to SQL Server. Alternatively, databases backed up with custom-managed keys can be restored to SQL Server. 

Additionally, in the future, instances that have backup portability enabled may not get SQL Server 2022 database engine updates that impact the backup portability feature. Customers can disable the backup portability feature to get all the latest updates, but should consider the trade off to their business between gaining the new feature while losing the ability to restore their database to SQL Server. 

The following table details the type of updates that may be impacted with the backup portability feature enabled: 

|Type of upgrade | Portability ON | Portability off | 
| ----------- | ------------------ | ------------- | 
| Security patches| Yes<sup>1</sup>| Yes | 
| Bug fixes | Yes<sup>1</sup> | Yes | 
| New PaaS features | Yes | Yes | 
| New SQL engine features with no on-disk metadata changes | Subject to triage<sup>2</sup> | Yes
| New SQL engine features with DB version bump | No | Yes | 

<sup>1</sup> Everything from SQL Server Cumulative  Updates, as well as updates specific to Azure SQL. 
<sup>2/sup> Subject to review and triage by the product group, as it requires porting. 


## Next steps

- To learn how to create your first managed instance, see [Quickstart guide](instance-create-quickstart.md).
- For a features and comparison list, see [SQL common features](../database/features-comparison.md).
- For more information about VNet configuration, see [SQL Managed Instance VNet configuration](connectivity-architecture-overview.md).
- For a quickstart that creates a managed instance and restores a database from a backup file, see [Create a managed instance](instance-create-quickstart.md).
- For a tutorial about using Azure Database Migration Service for migration, see [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance).
- For advanced monitoring of SQL Managed Instance database performance with built-in troubleshooting intelligence, see [Monitor Azure SQL Managed Instance using Azure SQL Analytics](/azure/azure-monitor/insights/azure-sql).
- For pricing information, see [SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/managed/).
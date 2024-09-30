---
title: SQL Data Sync retirement migration
description: This tutorial explains options and alternatives to SQL Data Sync.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 09/27/2024
ms.service: azure-sql-database
ms.subservice: sql-data-sync
ms.topic: conceptual
---
# SQL Data Sync retirement: Migrate to alternative solutions

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

SQL Data Sync will be retired on 30 September 2027. By then, consider migrating to alternatives data replication/synchronization solutions. This article details alternative solutions to migrate your data synchronization options.

## Alternatives to SQL Data Sync

The right solution depends on your individual use case. We'll detail the alternative solutions including Azure Data Factory, Azure Functions, read replicas or SQL features like linked server, mirroring, Always On availability groups, or transactional replication.

[SQL Data Sync](sql-data-sync-agent-overview.md) is a service built on Azure SQL Database that lets you synchronize the data you select bi-directionally across multiple databases, both on premises and in the cloud. The three main scenarios for SQL Data Sync are:

- **Hybrid data synchronization**: You use SQL Data Sync to synchronize databases in SQL Server and Azure SQL Database.
- **Distributed applications**: You use SQL Data Sync to separate the workload across different databases for reporting purposes.
- **Globally-distributed applications**: You use SQL Data Sync to synchronize data across different countries and regions.

The alternatives to replace SQL Data Sync depend on the scenario and platforms. Different use cases and platforms have different alternatives.

### Hybrid data synchronization

This scenario is most used to synchronize data from a SQL Server instance (on-premises or Azure Virtual Machine) to Azure SQL Database or Azure SQL Managed Instance. The possible migration paths are:

- **Always On availability groups** tutorials:
  - [Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
  - [Managed Instance link feature overview - Azure SQL Managed Instance](../managed-instance/managed-instance-link-feature-overview.md?view=azuresqldb-mi&preserve-view=true)

- **Azure Data Factory** tutorials:
  - [Copy on-premises data using the Azure Copy Data tool - Azure Data Factory](/azure/data-factory/tutorial-copy-data-tool)
  - [Copy data in bulk using Azure portal - Azure Data Factory](/azure/data-factory/tutorial-bulk-copy-portal)

- **Transactional replication** tutorial:  
  - [Transactional replication - SQL Server](/sql/relational-databases/replication/transactional/transactional-replication)

- **Linked server** tutorial:
  - [Linked servers (Database Engine) - SQL Server](/sql/relational-databases/linked-servers/linked-servers-database-engine)

### Distributed Applications

This scenario focuses on duplicating data to another environment for reading purposes, most predominantly on Azure SQL Database or Azure SQL Managed Instance. The possible migration paths are:

- **Read replicas** tutorial:  
  - [Read queries on replicas - Azure SQL Database & Azure SQL Managed Instance](/azure/azure-sql/database/read-scale-out)

- **Always On availability groups** tutorial:  
  - [Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)

- **Database copy** tutorial:
  - [Copy a database - Azure SQL Database](/azure/azure-sql/database/database-copy)

### Globally distributed applications

This scenario synchronizes data across different regions for reading purposes. Most used on Azure SQL Database.

- **Azure Data Factory** tutorials:  
  - [Azure Data Factory tutorials](/azure/data-factory/data-factory-tutorials)
  - [Change data capture](/azure/data-factory/concepts-change-data-capture)
  - [Mapping data flows](/azure/data-factory/data-flow-tutorials)
  - [Incrementally copy data from a source data store to a destination data store](/azure/data-factory/tutorial-incremental-copy-overview)

- **Readable active geo-replication** tutorial:  
  - [Active geo-replication - Azure SQL Database](/azure/azure-sql/database/active-geo-replication-overview)

### Other alternatives to consider

- **Azure Functions** tutorial:  
  - [Azure Functions Overview](/azure/azure-functions/functions-overview)

- **Fabric mirrored databases** tutorials:  
  - [Microsoft Fabric mirrored databases from Azure SQL Database](/fabric/database/mirrored-database/azure-sql-database)
  - [Explore data in your mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)
  - [Explore data in your mirrored database directly in OneLake](/fabric/database/mirrored-database/explore-data-directly)

## Migration by platform

| **Source**     | **Destination** | &nbsp; | &nbsp; |
|:--|:--|:--|:--|
|                | **SQL Server** | **Azure SQL Managed Instance** | **Azure SQL Database** |
| **SQL Server** |  Always On availability groups<br /> Azure Data Factory<br /> Transactional replication<br /> Linked server |  Always On availability groups<br /> Azure Data Factory<br /> Transactional replication<br /> Linked server |  Azure Data Factory<br /> Azure Functions<br /> Transactional replication |
| **Azure SQL Managed Instance** |  Always On availability groups<br /> Azure Data Factory<br /> Transactional replication<br /> Linked server | Always On availability groups<br /> Azure Data Factory<br /> Transactional replication<br /> Linked server<br /> Read replicas| Azure Data Factory<br /> Azure Functions<br /> Transactional replication |
| **Azure SQL Database**  | Azure Data Factory<br /> Transactional replication| Azure Data Factory<br /> Transactional replication<br /> Linked Server| Azure Data Factory<br /> Azure Functions<br /> Active geo-replication<br /> Copy Database |

## Related content

- [Active geo-replication](active-geo-replication-overview.md)
- [Azure Functions](/azure/azure-functions/functions-overview)
- [Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [Managed Instance link feature overview - Azure SQL Managed Instance](../managed-instance/managed-instance-link-feature-overview.md?view=azuresql-mi&preserve-view=true)
- [Microsoft Fabric mirrored databases from Azure SQL Database](/fabric/database/mirrored-database/azure-sql-database)

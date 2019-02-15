---
title: "Replication Backward Compatibility | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "transactional replication, backward compatibility"
  - "backward compatibility [SQL Server replication]"
  - "merge replication backward compatibility [SQL Server replication]"
  - "replication [SQL Server], backward compatibility"
  - "backward compatibility [SQL Server], replication"
  - "snapshot replication [SQL Server], backward compatibility"
  - "compatibility [SQL Server replication]"
ms.assetid: 091c51dc-8b32-4b4f-847e-b317456c8394
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Replication Backward Compatibility
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Backward compatibility is important to understand if you are upgrading, or if you have more than one version of SQL Server in a replication topology. 

The general rules are: 

-   A Distributor can be any version as long as it is greater than or equal to the Publisher version (in many cases the Distributor is the same instance as the Publisher).    
-   A Publisher can be any version as long as it less than or equal to the Distributor version.    
-   Subscriber version depends on the type of publication:    
    - A Subscriber to a transactional publication can be any version within two versions of the Publisher version. For example: a SQL Server 2012 (11.x) Publisher can have SQL Server 2014 (12.x) and SQL Server 2016 (13.x) Subscribers; and a SQL Server 2016 (13.x) Publisher can have SQL Server 2014 (12.x) and SQL Server 2012 (11.x) Subscribers.     
    - A Subscriber to a merge publication can be all versions equal to or lower than the Publisher version which are supported as per the versions life cycle support cycle.  


## Replication Matrix
[!INCLUDE[repl matrix](../../includes/replication-compat-matrix.md)]


## Additional Resources
 [Deprecated Features in SQL Server Replication](../../relational-databases/replication/deprecated-features-in-sql-server-replication.md)  
 Replication features that have been retained in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] for backward compatibility, but, which will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [Breaking Changes in SQL Server Replication](../../relational-databases/replication/breaking-changes-in-sql-server-replication.md)  
 Replication feature changes that might require changes to applications. 

 [Upgrade Replicated Databases](../../database-engine/install-windows/upgrade-replicated-databases.md)  
 Steps and considerations when upgrading SQL Servers participating in a replication topology. 
  
  

---
title: Backup and Restore for Oracle Publishers
description: Backup and restore Oracle publishers in SQL Server replication with confidence. Follow these guidelines to keep publications, distributors, and subscribers in sync.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "recovery [SQL Server replication], Oracle publishing"
  - "backups [SQL Server replication], Oracle publishing"
  - "Oracle publishing [SQL Server replication], backup and restore"
  - "restoring [SQL Server replication], Oracle publishing"
---
# Backup and restore for Oracle publishers
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  Follow these guidelines when backing up and restoring:  
  
-   Ensure the Log Reader Agent isn't running and that no other database activity occurs on the published tables while you're backing up the Publisher.  
  
-   Back up the Publisher and Distributor at the same time.  
  
-   Reinitialize all subscriptions if you must restore the Publisher or Distributor.  
  
-   To restore a Subscriber from a backup without reinitializing subscriptions, the transactions delivered to the distribution database after the last subscription database backup was completed must still be available. The length of time transactions are available depends on distribution retention settings. For information about these settings, see [Subscription Expiration and Deactivation](../../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
-   If a database restore causes the Publisher or Distributor to become out of sync, the replication agents log error messages. You must drop and recreate all relevant publications and subscriptions:  
  
    1.  Script the definition of the publications and subscriptions. For more information, see [Scripting Replication](../../../relational-databases/replication/scripting-replication.md).  
  
         If the definition of the publications changed between the versions of the Publisher and Distributor states, modify the scripts.  
  
    2.  Drop the publications and subscriptions.  
  
    3.  Run the scripts created in step 1.  
  
     If you must drop and reconfigure the Publisher, drop the **MSSQLSERVERDISTRIBUTOR** public synonym and the configured Oracle replication user by using the **CASCADE** option to remove all replication objects from the Oracle Publisher.  
  
## Related content

- [Back Up and Restore Replicated Databases](../administration/back-up-and-restore-replicated-databases.md)
- [Configure an Oracle Publisher](configure-an-oracle-publisher.md)
- [Oracle Publishing Overview](oracle-publishing-overview.md)

---
title: "Administrative considerations for Oracle Publishers"
description: Considerations for the administration of Oracle Publishers publishing changes to SQL Server subscribers. 
ms.custom: seo-lt-2019
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle publishing [SQL Server replication], administrative considerations"
  - "administering replication, Oracle publishing"
ms.assetid: cfd81fb5-419b-4a1b-97c4-be7c9d4ee289
author: "MashaMSFT"
ms.author: "mathoma"
---
# Administrative Considerations for Oracle Publishers
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  After an Oracle Publisher is configured and the replication change tracking mechanisms are in place, administrators of the Oracle database system can still use standard Oracle database utilities and perform typical system administration tasks. However, you should be aware of the effects on the published data of performing certain administrative tasks.  
  
 With the exception of dropping or modifying a column that is published for replication, or dropping or modifying any replication objects, these considerations do not apply to snapshot publications.  
  
## Importing and loading data  
 Triggers are used in change tracking for transactional publications on Oracle. Changes to published tables can be replicated to Subscribers only if the replication triggers fire when an update, insert, or delete occurs. The Oracle utilities Oracle Import and SQL*Loader both have options that affect whether triggers will fire when rows are inserted into replicated tables with these utilities.  
  
### Oracle Import  
 With Oracle Import, you can set the option **ignore** to 'y' or 'n' (the default is 'n'). If **ignore** is set to 'n', the table is dropped and re-created during import. This removes replication triggers and disables replication. If **ignore** is set to 'y', import will attempt to load the rows into the existing table, which fires the replication triggers. Therefore, ensure **ignore** is set to 'y' when importing into a replicated table with the Import tool.  
  
### SQL*Loader  
 With SQL\*Loader, you can set the option **direct** to 'true' or 'false' (the default is 'false'). If **direct** is set to 'false', rows are inserted using conventional INSERT statements, which fire replication triggers. If **direct** is set to 'true', the load is optimized, and triggers are not fired. Therefore, ensure **direct** is set to 'false' when loading into a replicated table with the SQL*Loader tool.  
  
## Making changes to published objects  
 The following actions require no special considerations:  
  
-   Rebuilding indexes on published tables.  
  
-   Adding user triggers to a published table.  
  
 The following action requires you to stop all activity on the published tables:  
  
-   Moving a published table.  
  
 The following actions require you to drop the publication, perform the operation, and then recreate the publication:  
  
-   Truncating a published table.  
  
-   Renaming a published table.  
  
-   Adding a column to a published table.  
  
-   Dropping or modifying a column that is published for replication.  
  
-   Performing non-logged operations.  
  
## Dropping or modifying replication objects  
 You must drop and reconfigure the Publisher if you drop or modify any Publisher level tracking tables, triggers, sequences, or stored procedures. For a partial list of these objects, see [Objects Created on the Oracle Publisher](../../../relational-databases/replication/non-sql/objects-created-on-the-oracle-publisher.md).  
  
 For information about dropping and reconfiguring the Publisher, see the section "Changes are made that require reconfiguration of the Publisher" in the topic [Troubleshooting Oracle Publishers](../../../relational-databases/replication/non-sql/troubleshooting-oracle-publishers.md).  
  
## See Also  
 [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md)   
 [Design Considerations and Limitations for Oracle Publishers](../../../relational-databases/replication/non-sql/design-considerations-and-limitations-for-oracle-publishers.md)   
 [Oracle Publishing Overview](../../../relational-databases/replication/non-sql/oracle-publishing-overview.md)  
  
  

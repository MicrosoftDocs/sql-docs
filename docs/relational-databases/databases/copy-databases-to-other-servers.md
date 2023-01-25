---
title: "Copy Databases to Other Servers | Microsoft Docs"
description: Learn how to copy a SQL Server database from one computer to another for testing, to make it available to remote-branch operations, or for other reasons.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "servers [SQL Server], copying databases between"
  - "bulk exporting [SQL Server], between servers"
  - "database copying [SQL Server]"
  - "migrating databases [SQL Server]"
  - "moving databases"
  - "copying databases"
  - "bulk importing [SQL Server], between servers"
ms.assetid: 978406d6-a3c8-4902-b1f4-4ced75234be5
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Copy Databases to Other Servers
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  It is sometimes useful to copy a database from one computer to another, whether for testing, checking consistency, developing software, running reports, creating a mirror database, or, possibly, to make the database available to remote-branch operations.  
  
 There are several ways to copy a database:  
  
-   Using the Copy Database Wizard  
  
     You can use the Copy Database Wizard to copy or move databases between servers or to upgrade a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database to a later version. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
-   Restoring a database backup  
  
     To copy an entire database, you can use the BACKUP and RESTORE [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Typically, restoring a full backup of a database is used to copy the database from one computer to another for a variety of reasons. For information on using backup and restore to copy a database, see [Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md).  
  
    > [!NOTE]  
    >  To set up a mirror database for database mirroring, you must restore the database onto the mirror server by using RESTORE DATABASE *<database_name>* WITH NORECOVERY. For more information, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md).  
  
-   Using the Generate Scripts Wizard to publish databases  
  
     You can use the Generate Scripts Wizard to transfer a database from a local computer to a Web hosting provider. For more information, see [Generate and Publish Scripts Wizard](../../ssms/scripting/generate-and-publish-scripts-wizard.md).  
  

---
title: "Upgrade the SSIS Catalog (SSISDB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.ssis.dbupgradewizard.f1"
ms.assetid: 170c3dc9-f5bf-4599-ae56-d24a620f23e8
caps.latest.revision: 8
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Upgrade the SSIS Catalog (SSISDB)
  Run the SSISDB Upgrade Wizard to upgrade the SSIS Catalog database, SSISDB, when the database is older than the current version of the SQL Server instance. This occurs when one of the following conditions is true.  
  
-   You restored the database from an older version of SQL Server.  
  
-   You did not remove the database from an Always On Availability Group before upgrading the SQL Server instance. This prevents the automatic upgrade of the database. For more info, see [Upgrading SSISDB in an availability group](../../integration-services/service/always-on-for-ssis-catalog-ssisdb.md#Upgrade).  
  
 The wizard can only upgrade the database on a local  server instance.  
  
## Upgrade the SSIS Catalog (SSISDB) by running the SSISDB Upgrade Wizard  
  
1.  Backup the SSIS Catalog database, SSISDB.  
  
2.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the local server, and then expand **Integration Services Catalogs**.  
  
3.  Right-click on **SSISDB**, and then select **Database Upgrade** to launch the SSISDB Upgrade Wizard.  
  
     ![Launch the SSISDB upgrade wizard](../../integration-services/service/media/ssisdb-upgrade-wizard-1.png "Launch the SSISDB upgrade wizard")  
  
4.  On the **Select Instance** page, select a SQL Server instance on the local server.  
  
    > [!IMPORTANT]  
    >  The wizard can only upgrade the database on a local server instance.  
  
     Select the checkbox to indicate that you have backed up the SSISDB database before running the wizard.  
  
     ![Select the server in the SSISDB Upgrade Wizard](../../integration-services/service/media/ssisdb-upgrade-wizard-2.png "Select the server in the SSISDB Upgrade Wizard")  
  
5.  Select **Upgrade** to upgrade the SSIS Catalog database.  
  
6.  On the **Result** page, review the results.  
  
     ![Review the results in the SSISDB Upgrade Wizard](../../integration-services/service/media/ssisdb-upgrade-wizard-3.png "Review the results in the SSISDB Upgrade Wizard")  
  
  
---
title: "Integration Services (SSIS) Server and Catalog | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "packages [Integration Services], managing"
  - "managing packages [Integration Services]"
ms.assetid: 6d667bba-7c25-492a-8f4d-70ebaca28f40
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services (SSIS) Server and Catalog

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  After you design and test packages in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], you can deploy the projects that contain the packages to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server is an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that hosts the **SSISDB** database. The database stores the following objects: packages, projects, parameters, permissions, server properties, and operational history.  
  
 The **SSISDB** database exposes the object information in public views that you can query. The database also provides stored procedures that you can call to manage the objects.  
  
 Before you can deploy the projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, you need to create the **SSISDB** catalog.  
  
 For an overview of the SSISDB catalog functionality, see [SSIS Catalog](../../integration-services/catalog/ssis-catalog.md).  
  
## High Availability  
 Like other user databases, the **SSISDB** database supports database mirroring and replication. For more information about mirroring and replication, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).  
  
 You can also provide high-availability of SSISDB and its contents by making use of SSIS and Always On Availability Groups. For more information, see [Always On for SSIS Catalog (SSISDB](ssis-catalog.md#always-on-for-ssis-catalog-ssisdb). Also see this blog post by Matt Masson, [SSIS with Always On](https://go.microsoft.com/fwlink/?LinkId=255873), at blogs.msdn.com.  
  
##  <a name="ssms"></a> Integration Services Server in SQL Server Management Studio  
 When you connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that hosts the **SSISDB** database, you see the following objects in Object Explorer:  
  
-   **SSISDB Database**  
  
     The **SSISDB** database appears under the **Databases** node in Object Explore. You can query the views and call the stored procedures that manage the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server and the objects that are stored on the server.  
  
-   **Integration Services Catalogs**  
  
     Under the **Integration Services Catalogs** node there are folders for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects and environments.  
  
## Related Tasks  
  
-   [View the List of Packages on the Integration Services Server](../../integration-services/catalog/view-the-list-of-packages-on-the-integration-services-server.md)  
  
-   [Deploy Integration Services (SSIS) Projects and Packages](../../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md)  
  
-   [Run Integration Services (SSIS) Packages](../../integration-services/packages/run-integration-services-ssis-packages.md)  
  
## Related Content  
 Blog entry, [SSIS with Always On](https://go.microsoft.com/fwlink/?LinkId=255873), at blogs.msdn.com.  
  
  

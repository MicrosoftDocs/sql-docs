---
title: "Master Data Services Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "database [Master Data Services], about the database"
  - "database [Master Data Services]"
ms.assetid: 5f590cc1-6ec2-4b8c-a598-03de0f6051a0
author: leolimsft
ms.author: lle
manager: craigg
---
# Master Data Services Database

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  The database contains all of the information for the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] system. It is central to a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] deployment. The [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database:  
  
-   Stores the settings, database objects, and data required by the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] system.  
  
-   Contains staging tables that are used to process data from source systems.  
  
-   Provides a schema and database objects to store master data from source systems.  
  
-   Supports versioning functionality, including business rule validation and e-mail notifications.  
  
-   Provides views for subscribing systems that need to retrieve data from the database.  
  
## In This Section  
  
-   [Leaf Member Staging Table &#40;Master Data Services&#41;](../master-data-services/leaf-member-staging-table-master-data-services.md)  
  
-   [Consolidated Member Staging Table &#40;Master Data Services&#41;](../master-data-services/consolidated-member-staging-table-master-data-services.md)  
  
-   [Relationship Staging Table &#40;Master Data Services&#41;](../master-data-services/relationship-staging-table-master-data-services.md)  
  
-   [Staging Process Errors &#40;Master Data Services&#41;](../master-data-services/staging-process-errors-master-data-services.md)  
  
## See Also  
 [Create a Master Data Services Database](../master-data-services/install-windows/create-a-master-data-services-database.md)   
 [Database Object Security &#40;Master Data Services&#41;](../master-data-services/database-object-security-master-data-services.md)   
 [Database Logins, Users, and Roles &#40;Master Data Services&#41;](../master-data-services/database-logins-users-and-roles-master-data-services.md)  
  
  

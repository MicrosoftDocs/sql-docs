---
title: "Database Object Security (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "database [Master Data Services], object security"
  - "security [Master Data Services], database objects"
ms.assetid: dd5ba503-7607-45d9-ad0d-909faaade179
author: leolimsft
ms.author: lle
manager: craigg
---
# Database Object Security (Master Data Services)
  In the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database, data is stored in multiple database tables and is visible in views. Information that you might have secured in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] Web application is visible to users with access to the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
 Specifically, employee salary information might be contained in an Employee model, or company financial information might be in an Account model. You can deny a user access to these models in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] user interface, but users with access to the database can view this data.  
  
 You can grant permissions to database objects to make specific data available to users. For more information on granting permissions, see [GRANT Object Permissions &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-object-permissions-transact-sql). For more information about securing SQL server, see [Securing SQL Server](../relational-databases/security/securing-sql-server.md).  
  
 The following tasks require access to the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database:  
  
-   [Staging Data](#Staging)  
  
-   [Validating Data Against Business Rules](#rules)  
  
-   [Deleting Versions](#Versions)  
  
-   [Immediately Applying Hierarchy Member Permissions](#Hierarchy)  
  
-   [Changing the System Administrator Account](#SysAdmin)  
  
-   [Configuring System Settings](#SysSettings)  
  
##  <a name="Staging"></a> Staging Data  
 In the following table, each securable has "name" as part of the name. This indicates the name of the staging table that is specified when an entity is created. For more information, see [Data Import &#40;Master Data Services&#41;](overview-importing-data-from-tables-master-data-services.md)  
  
|Action|Securables|Permissions|  
|------------|----------------|-----------------|  
|Load leaf members and their attributes into the staging table.|stg.name_Leaf|Required: INSERT<br /><br /> Optional: SELECT and UPDATE|  
|Load the data from the Leaf staging table into the appropriate MDS database tables.|stg.udp_name_Leaf|EXECUTE|  
|Load consolidated members and their attributes into the staging table.|stg.name_Consolidated|Required: INSERT<br /><br /> Optional: SELECT and UPDATE|  
|Load the data from the Consolidated staging table into the appropriate MDS database tables.|stg.udp_name_Consolidated|EXECUTE|  
|Load leaf and consolidated members' relationships to each other in an explicit hierarchy into the staging table.|stg.name_Relationship|Required: INSERT<br /><br /> Optional: SELECT and UPDATE|  
|Load the data from the Relationship staging table into the appropriate MDS tables.|stg.udp_name_Relationship|EXECUTE|  
|View errors that occurred when data from the staging tables was being inserted into the MDS database tables.|stg.udp_name_Relationship|SELECT|  
  
 For more information, see [Data Import &#40;Master Data Services&#41;](overview-importing-data-from-tables-master-data-services.md).  
  
##  <a name="rules"></a> Validating Data Against Business Rules  
  
|Action|Securable|Permissions|  
|------------|---------------|-----------------|  
|Validate a version of data against business rules|mdm.udpValidateModel|EXECUTE|  
  
 For more information, see [Validation Stored Procedure &#40;Master Data Services&#41;](../../2014/master-data-services/validation-stored-procedure-master-data-services.md).  
  
##  <a name="Versions"></a> Deleting Versions  
  
|Action|Securables|Permissions|  
|------------|----------------|-----------------|  
|Determine the ID of the version you want to delete|mdm.viw_SYSTEM_SCHEMA_VERSION|SELECT|  
|Delete a version of a model|mdm.udpVersionDelete|EXECUTE|  
  
 For more information, see [Delete a Version &#40;Master Data Services&#41;](../../2014/master-data-services/delete-a-version-master-data-services.md).  
  
##  <a name="Hierarchy"></a> Immediately Applying Hierarchy Member Permissions  
  
|Action|Securables|Permissions|  
|------------|----------------|-----------------|  
|Immediately apply member permissions|mdm.udpSecurityMemberProcessRebuildModel|EXECUTE|  
  
 For more information, see [Immediately Apply Member Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/immediately-apply-member-permissions-master-data-services.md).  
  
##  <a name="SysAdmin"></a> Changing the System Administrator Account  
  
|Action|Securables|Permissions|  
|------------|----------------|-----------------|  
|Determine the SID of the new administrator|mdm.tblUser|SELECT|  
|Change the system administrator account|mdm.udpSecuritySetAdministrator|EXECUTE|  
  
 For more information, see [Change the System Administrator Account &#40;Master Data Services&#41;](../../2014/master-data-services/change-the-system-administrator-account-master-data-services.md).  
  
##  <a name="SysSettings"></a> Configuring System Settings  
 There are system settings that you can configure to control behavior in [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]. You can adjust these settings in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] or if you have UPDATE access, you can adjust these settings directly in the mdm.tblSystemSetting database table. For more information, see [System Settings &#40;Master Data Services&#41;](../../2014/master-data-services/system-settings-master-data-services.md).  
  
## See Also  
 [Security &#40;Master Data Services&#41;](../../2014/master-data-services/security-master-data-services.md)  
  
  

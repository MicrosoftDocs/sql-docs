---
title: Database Logins, Users, and Roles
description: Master Data Services includes logins, users, and roles installed on the SQL Server Database Engine instance that hosts the Master Data Services database.
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "security [Master Data Services], database roles"
  - "database [Master Data Services], users"
  - "security [Master Data Services], database users"
  - "database [Master Data Services], roles"
  - "database [Master Data Services], logins"
  - "security [Master Data Services], database logins"
ms.assetid: 72ee383e-a619-461b-9f9d-1cac162ab0c5
author: CordeliaGrey
ms.author: jiwang6
---
# Database Logins, Users, and Roles (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] includes logins, users, and roles that are automatically installed on the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] instance that hosts the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. These logins, users, and roles should not be modified.  
  
## Logins  
  
|Login|Description|  
|-----------|-----------------|  
|**mds_dlp_login**|Allows creation of UNSAFE assemblies. For more information, see [Creating an Assembly](../relational-databases/clr-integration/assemblies/creating-an-assembly.md).<br /><br /> -Disabled login with randomly-generated password.<br /><br /> -Maps to dbo for the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.<br /><br /> -For msdb, mds_clr_user maps to this login.|  
|**mds_email_login**|Enabled login used for notifications.<br /><br /> For msdb and the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database, mds_email_user maps to this login.|  
  
## msdb Users  
  
|User|Description|  
|----------|-----------------|  
|**mds_clr_user**|Not used. Maps to mds_dlp_login.|  
|**mds_email_user**|Used for notifications.<br /><br /> -Maps to mds_email_login.<br /><br /> -Is a member of the role: DatabaseMailUserRole.|  
  
## Master Data Services Database Users  
  
|User|Description|  
|----------|-----------------|  
|**mds_email_user**|Used for notifications.<br /><br /> -Has SELECT permission for the mdm schema.<br /><br /> -Has EXECUTE permission for the mdm.MemberGetCriteria user defined table type.<br /><br /> -Has EXECUTE permission for the mdm.udpNotificationQueueActivate stored procedure.|  
|**mds_schema_user**|Owns the mdm and mdq schemas. The default schema is mdm.<br /><br /> Does not have a login mapped to it.|  
|**mds_ssb_user**|Used to execute Service Broker tasks.<br /><br /> -Has DELETE, INSERT, REFERENCES, SELECT, and UPDATE permission all schemas.<br /><br /> -Does not have a login mapped to it.|  
  
## Master Data Services Database Role  
  
|Role|Description|Permissions|  
|----------|-----------------|-----------------|  
|**mds_exec**|This role contains the account you designate in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] when you create a [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application and designate an account for the application pool.|EXECUTE permission on all schemas.<br /><br /> <br /><br /> ALTER, INSERT, and SELECT permission on these tables:<br /><br /> mdm.tblStgMember<br /><br /> mdm.tblStgMemberAttribute<br /><br /> mdm.tbleStgRelationship<br /><br /> <br /><br /> SELECT permission on these tables:<br /><br /> mdm.tblUser<br /><br /> mdm.tblUserGroup<br /><br /> mdm.tblUserPreference<br /><br /> <br /><br /> SELECT permission on these views:<br /><br /> mdm.viw_SYSTEM_SECURITY_NAVIGATION<br /><br /> mdm.viw_SYSTEM_SECURITY_ROLE_ACCCESSCONTROL<br /><br /> mdm.viw_SYSTEM_SECURITY_ROLE_ACCCESSCONTROL_MEMBER<br /><br /> mdm.viw_SYSTEM_SECURITY_USER_MODEL|  
  
## Schemas  
  
|Role|Description|  
|----------|-----------------|  
|**mdm**|Contains all [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database and Service Broker objects other than the functions contained in the mdq schema.|  
|**mdq**|Contains [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database functions related to filtering member results based on regular expressions or similarity, and for formatting notification emails.|  
|**stg**|Contains [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database tables, stored procedures, and views related to the staging process. Do not delete any of these objects. For more information about the staging process, see [Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md).|  
  
## See Also  
 [Database Object Security &#40;Master Data Services&#41;](../master-data-services/database-object-security-master-data-services.md)  
  
  

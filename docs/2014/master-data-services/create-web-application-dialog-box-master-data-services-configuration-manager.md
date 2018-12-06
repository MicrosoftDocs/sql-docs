---
title: "Create Web Application Dialog Box (Master Data Services Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.mds.configmanager.createapp.f1"
ms.assetid: e045b41a-4836-47f6-8e78-2b09494b461f
author: leolimsft
ms.author: lle
manager: craigg
---
# Create Web Application Dialog Box (Master Data Services Configuration Manager)
  Use the **Create Web Application** dialog box to create the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application. This web application is created in the site that you selected on the **Web Configuration** page.  
  
## Web Application  
 The web server serves the content for this web application from the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] **WebApplication** folder in the file system. This location is specified during Setup, and by default the path is *drive*:\Program Files\Microsoft SQL Server\120\Master Data Services\WebApplication.  
  
|Control Name|Description|  
|------------------|-----------------|  
|Virtual path|Select the virtual path under which you want to create the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application. A virtual path is part of the URL that is used to access a web application.<br /><br /> This list is filtered to display only virtual paths of applications under which the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application can be created. You cannot create a [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application under another [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application.|  
|Alias|Type a name for the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application or use the default name. This name is used in a URL to access the web application from a web browser.|  
  
## Application Pool  
  
|Control Name|Description|  
|------------------|-----------------|  
|**Name**|Type a unique, friendly name for a new application pool, or use the default name. The [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application is added to this application pool.<br /><br /> Application pools provide boundaries that prevent applications in one application pool from affecting applications in another application pool.|  
|**User name**|Type a domain and user name from Active Directory. This account is the identity of the application pool that the web application runs in. This account should be the same account specified as the service account when the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database was created.<br /><br /> This account is added to the mds_exec database role in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database for database access. For more information, see [Database Logins, Users, and Roles &#40;Master Data Services&#41;](database-logins-users-and-roles-master-data-services.md). It is also added to a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] Windows group, **MDS_ServiceAccounts**, which is granted permission to the temporary compilation directory, **MDSTempDir**, in the file system. For more information, see [Folder and File Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/folder-and-file-permissions-master-data-services.md).|  
|**Password**|Type the password for the specified user account.|  
|**Confirm password**|Retype the password for the specified user account. The **Password** and **Confirm password** fields must contain the same password.|  
  
## See Also  
 [Web Configuration Page &#40;Master Data Services Configuration Manager&#41;](../../2014/master-data-services/web-configuration-page-master-data-services-configuration-manager.md)   
 [Set up the Database and Website for Master Data Services](../../2014/master-data-services/set-up-the-database-and-website-for-master-data-services.md)   
 [Web Application Requirements &#40;Master Data Services&#41;](install-windows/web-application-requirements-master-data-services.md)   
 [Create a Master Data Manager Web Application &#40;Master Data Services&#41;](install-windows/create-a-master-data-manager-web-application-master-data-services.md)  
  
  

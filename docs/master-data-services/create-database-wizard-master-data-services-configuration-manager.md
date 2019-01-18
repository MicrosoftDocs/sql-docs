---
title: "Create Database Wizard (Master Data Services Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.mds.configmanager.createdbwiz.f1"
ms.assetid: 45fe7a23-a46c-4d40-8bca-3431fbfc5c9d
author: leolimsft
ms.author: lle
manager: craigg
---
# Create Database Wizard (Master Data Services Configuration Manager)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Use the **Create Database** wizard to create a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
## Database Server  
 Specify information to connect to a local or remote [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] instance to host the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. To connect to a remote instance, it must be enabled for remote connections.  
  
|Control Name|Description|  
|------------------|-----------------|  
|**SQL Server instance**|Specify the name of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] instance that you want to host the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. This can be a default or named instance on either a local or a remote computer. Specify the information by typing:<br /><br /> A period (.) to connect to the default instance on your local computer.<br /><br /> The server name or IP address to connect to the default instance on the specified local or remote computer.<br /><br /> The server name or IP address, and the instance name to connect to the named instance on the specified local or remote computer. Specify this information in the format *server_name*\\*instance_name*.|  
|**Authentication type**|Select the type of authentication to use when connecting to the specified SQL Server instance. The credentials you use to connect must be part of the **sysadmin** server role for the specified [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. For more information about the sysadmin role, see [Server-Level Roles](../relational-databases/security/authentication-access/server-level-roles.md).<br /><br /> The authentication types include:<br /><br /> **Current User - Integrated Security**: Uses Integrated Windows Authentication to connect by using the credentials of the current Windows user account. [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] uses the Windows credentials of the user who logged on to the computer and opened the application. You cannot specify different Windows credentials in the application. If you want to connect with different Windows credentials, you must log on to the computer as that user and then open [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)].<br /><br /> **SQL Server Account**: Uses a SQL Server account to connect. When you select this option, the **User name** and **Password** fields are enabled and you must specify credentials for a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] account on the specified [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance.|  
|**User name**|Specify the name of the user account that will be used to connect to the specified [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. The account must be part of the **sysadmin** role on the specified [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance.<br /><br /> When the **Authentication type** is **Current User - Integrated Security**, the **User name** box is read-only and it displays the name of the Windows user account that is logged on to the computer.<br /><br /> When the **Authentication type** is **SQL Server Account**, the **User name** box is enabled and you must specify credentials for a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] account on the specified [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance.|  
|**Password**|Specify the password associated with the user account:<br /><br /> When the **Authentication type** is **Current User - Integrated Security**, the **Password** box is read-only and credentials of the specified Windows user account are used to connect.<br /><br /> When the **Authentication type** is **SQL Server Account**, the **Password** box is enabled and you must specify the password associated with the specified user account.|  
|**Test Connection**|Verify that the specified user account can connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance and that the account has permission to create a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database for that instance. If you do not click **Test Connection**, the connection will be tested when you click **Next**.|  
  
## Database  
 Specify a database name and collation options for the new database. Collations in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provide sorting rules, case, and accent sensitivity properties for your data. Collations that are used with character data types such as char and varchar dictate the code page and corresponding characters that can be represented for that data type. For more information about database collation, see [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).  
  
|Control Name|Description|  
|------------------|-----------------|  
|**Database name**|Specify a name for the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.|  
|**SQL Server default collation**|Select to use the current database collation setting of the specified [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance for the new database.|  
|**Windows collation**|Specify the Windows collation settings to use for the new database. Windows collations define rules for storing character data based on an associated Windows locale. For more information about Windows collations and the associated options, see [Windows Collation Name &#40;Transact-SQL&#41;](../t-sql/statements/windows-collation-name-transact-sql.md).<br /><br /> Note: The **Windows collation** list and associated options enable only after you clear the **SQL Server default collation** box.|  
  
## Administrator Account  
  
|Control Name|Description|  
|------------------|-----------------|  
|**User name**|Specify the default Super User for [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]. A Super User has access to all functional areas and can add, delete, and update all models. For information about the Super User permission and the other types of administrators in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] , see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).|  
  
## Summary  
 Displays a summary of the selected options. Review your selections, and then click **Next** to begin creating the database with the specified settings.  
  
## Progress and Finish  
 Displays the progress of the creation process. After the database is created, click **Finish** to close the database wizard and return to the **Databases** page. The new database is selected and you can view and modify its system settings.  
  
## See Also  
 [Database Configuration Page &#40;Master Data Services Configuration Manager&#41;](../master-data-services/database-configuration-page-master-data-services-configuration-manager.md)   
[Master Data Services Installation and Configuration](../master-data-services/master-data-services-installation-and-configuration.md) 
 [Database Requirements &#40;Master Data Services&#41;](../master-data-services/install-windows/database-requirements-master-data-services.md)  
  
  

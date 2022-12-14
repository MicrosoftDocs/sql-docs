---
description: "Import Registered Server Information (SQL Server Management Studio)"
title: Import Registered Server Information
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.importregisteredservers.f1"
helpviewer_keywords: 
  - "transferring registered server information"
  - "Registered Servers [SQL Server], importing"
  - "importing registered server information"
ms.assetid: cc497a14-1360-4887-b70c-002f042823b6
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# Import Registered Server Information (SQL Server Management Studio)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This topic describes how to import saved registered server information in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. Exporting and then importing registered server files lets you easily configure several computers with the same servers in Registered Servers. This is useful when managing a large number of servers from computers in several locations, or when you want to configure basic connection settings for a less-experienced user.  
  
> [!NOTE]  
>  You cannot import registered server information into [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
##  <a name="SSMSProcedure"></a>  
  
#### To import registered server information  
  
1.  In Registered Servers, click the server type on the Registered Servers toolbar. The server type must be the same as the registered server export file type. For example, if you have exported [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] registered server information, you must click **SQL Server** on the Registered Servers toolbar.  
  
2.  Right-click a server group, and select **Import**.  
  
3.  In the **Import Registered Servers** dialog box, select the registered servers file to import, and then click **OK**.  
  
     **Import file**  
     Type the name of the import file in the text box, or click the Browse button (**...**) to locate the import file on the client computer. If you select an existing file, the registered server information is appended to the file. The import file can only be a previously exported registered server file. Registered server files have a .regsrvr extension.  
  
     **Select the server group to import to**  
     Select the root node or a particular server group to which the registered server entries in the file will be imported. You can import all registered servers, registered servers in a particular server group, or a single registered server to the export file. The import functionality is recursive; for example, if server group A contains server group B, and server group B contains server groups C and D, importing server group A exports all entries in A, B, C, and D.  
  
     The server group displays only the server groups of the current registered server tree.  
  
     If you select to import an existing server group or server, a message confirms that you want to overwrite the existing server or server group.  
  
 Server registrations that use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication store passwords on a per-user basis. After importing the server registrations, users must enter the password for each server the first time they connect, storing the passwords in their lists of registered servers. This is not necessary for servers registered through Windows Authentication.  
  
## See Also  
 [Change a Server's Registration &#40;SQL Server Management Studio&#41;](./change-a-server-s-registration-sql-server-management-studio.md)   
 [Export Registered Server Information &#40;SQL Server Management Studio&#41;](./export-registered-server-information-sql-server-management-studio.md)   
 [Create a New Registered Server &#40;SQL Server Management Studio&#41;](./create-a-new-registered-server-sql-server-management-studio.md)  
  

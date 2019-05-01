---
title: "Export Registered Server Information (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.exportregisteredservers.f1"
helpviewer_keywords: 
  - "Registered Servers [SQL Server], exporting"
  - "exporting registered server information"
  - "transferring registered server information"
ms.assetid: b65e168f-b6bf-489c-b8ad-3b8644acf0b6
author: "markingmyname"
ms.author: "maghan"
manager: craigg
---
# Export Registered Server Information (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  This topic describes how to save and export registered server information in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]and distribute it to other employees or servers. You can use this export feature to present a consistent user interface on multiple computers.  
  
 Exporting and then importing Registered Server files lets you easily configure several computers with the same servers in Registered Servers. This is useful when managing a large number of servers from computers in several locations, or when you want to configure a less experienced user with basic connection settings.  
  
> [!NOTE]  
>  Server registrations that use SQL Server Authentication store passwords on a per-user basis. After exporting and importing the server registrations, users must enter the password for each server the first time they connect, storing the passwords in their lists of registered servers. This is not necessary for servers registered using Windows Authentication.  
  
##  <a name="SSMSProcedure"></a>  
  
#### To export registered server information  
  
1.  In Registered Servers, right-click a server group, and then click **Export**.  
  
    > [!NOTE]  
    >  You can export an individual server, all of the registered server tree, or a subset of the registered server tree.  
  
2.  In the **Export Registered Servers** dialog box, make the following selections.  
  
     **Server group**  
     Specify the server group which will be exported. Export all registered servers, registered servers in a particular server group, or a single registered server to the export file. The export functionality is recursive; for example, if server group A contains server group B, and server group B contains server groups C and D, exporting server group A exports all entries in A, B, C, and D.  
  
     The server group displays only the server groups of the current registered server tree.  
  
     **Export file**  
     Type the name of the export file in the text box or use the Browse button (**...**) to locate an export file on the client computer. If you select an existing file, the registered server information is appended to the file. Use the .regsrvr extension. If you want your registered server information to be available to other users or another computer, you can save the file on the network. Other users can access the file and import part or all of the registered server information. If you select an existing file as your export file, the contents of the file are overwritten with the server registration information.  
  
     **Do not include user names and passwords in the export file**  
     Exclude user names when exporting the file.  
  
    > [!IMPORTANT]  
    >  Although the export files are encrypted, if user names and SQL Server Authentication passwords are included in the file, access to the file should be carefully controlled. User names and passwords are therefore excluded from the export file by default.  
  
## See Also  
 [Import Registered Server Information &#40;SQL Server Management Studio&#41;](../../tools/sql-server-management-studio/import-registered-server-information-sql-server-management-studio.md)   
 [Create a New Registered Server &#40;SQL Server Management Studio&#41;](../../tools/sql-server-management-studio/create-a-new-registered-server-sql-server-management-studio.md)  
  
  

---
title: "Connection to SQL Server for Delete | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 030b10c2-6b88-4c2c-bf67-22994be25a60
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Connection to SQL Server for Delete
  When a login without a database role that includes write permission (for example the **db_owner** role) to the MSXDBCDC database attempts to delete an Oracle CDC instance, the Connect to SQL Server dialog box is displayed.  
  
 In this dialog box you must enter the credentials for a login that has write permission to the MSXDBCDC database, such the **db_owner** database role to delete the Oracle CDC instance.  
  
 Enter the following information in the Connect to SQL Server dialog box.  
  
 **Server Name**  
 Type the name of the server where the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is located.  
  
 **Authentication**  
 Select one of the following:  
  
-   **Windows Authentication**  
  
-   **SQL Server Authentication**: If you select this option, you must type the **Login** and **Password** for the user in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you are connecting to.  
  
 **Options**  
 Click the arrow to view available options to be configured. You can choose to leave these options with their default value. The available options are:  
  
-   **Connection Timeout**: Type the time (in seconds) the program waits for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection to be established before producing a timeout error. The default value is **15**.  
  
-   **Execution Timeout**: Type the time (in seconds) that the program waits for SQL command execution to finish before producing a timeout error. The default value is **30**.  
  
-   **Encrypt Connection**: Select **Encrypt Connection** to ensure that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection that being established is encrypted to guarantee privacy.  
  
-   **Advanced**: Click **Advanced** and type any additional connection properties into the Advanced Connection Properties dialog box, if necessary.  
  
## See Also  
 [SQL Server Connection Required Permissions for the CDC Service](sql-server-connection-required-permissions-for-the-cdc-service.md)  
  
  

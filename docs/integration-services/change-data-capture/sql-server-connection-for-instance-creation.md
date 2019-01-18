---
title: "SQL Server Connection for Instance Creation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 81d0e7e2-d8f0-4bd9-9565-218ce996f28e
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# SQL Server Connection for Instance Creation
  One of the first steps when creating an Oracle CDC Instance is the creation of a CDC database on the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. This CDC database is enabled for SQL Server CDC and this enablement requires a login that is a member of the `sysadmin` fixed-server role.  
  
 When a user that starts the **Create an Oracle CDC Instance** wizard is not a member of the `sysadmin` fixed-server role, the **Connect to SQL Server** dialog box opens and requests the credentials for a member of the `sysadmin` role to carry out the Enable the database for SQL Server CDC task. When the CDC database is created, the `sysadmin` login is discarded and work resumes with the original [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login used when the Oracle Designer Console was entered.  
  
## Task List  
 Enter the following information in the **Connect to SQL Server** dialog box.  
  
 **Server Name**  
 Type the name of the server where the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is located.  
  
 **Authentication**  
 Select one of the following:  
  
-   **Windows Authentication**  
  
-   **SQL Server Authentication**: If you select this option, you must type the **Login** and **Password** for the user in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you are connecting to.  
  
 The login must have a database role that allows access to the MSXCDCDB database. It is recommended that the login also have access to any additional databases being used or the user will not be able to view the data in those databases.  
  
 **Options**  
 Click the arrow to view available options to be configured. You can choose to leave these options with their default value. The available options are:  
  
-   **Connection Timeout**: Type the time (in seconds) that the CDC Service for Oracle waits for a connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before timing out. The default value is **15**.  
  
-   **Execution Timeout**: Type the time (in seconds) that the Oracle CDC Windows Service waits for a command to execute before timing out. The default value is **30**.  
  
-   **Encrypt Connection**: Select **Encrypt Connection** for communication between the Oracle CDC Service and the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance using an encrypted connection.  
  
-   **Advanced**: Click **Advanced** and type any additional connection properties into the Advanced Connection Properties dialog box, if necessary.  
  
     For information about the Advanced Connection Properties dialog box, see [Advanced Connection Properties](../../integration-services/change-data-capture/advanced-connection-properties.md).  
  
## See Also  
 [Create the SQL Server Change Database](../../integration-services/change-data-capture/create-the-sql-server-change-database.md)   
 [SQL Server Connection Required Permissions for the CDC Designer](../../integration-services/change-data-capture/sql-server-connection-required-permissions-for-the-cdc-designer.md)  
  
  

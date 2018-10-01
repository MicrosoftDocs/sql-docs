---
title: "Prepare SQL Server for CDC | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "prepSqlSrv"
ms.assetid: 20b51dbf-a545-4234-87ae-4228268a0fb2
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Prepare SQL Server for CDC
  The Oracle CDC service requires all target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances to contain the MSXDBCDC database. You create this database using the Prepare SQL Server action in the CDC Service Configuration Console. This creates a special script that is run to create the required tables, stored procedures, and other required artifacts for this database. This task is done one time only for each target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 For more information about the MSXDBCDC database, see The MSXDBCDC Database.  
  
 In the CDC Service Configuration Console, click **Prepare SQL Server**. If this option is not available, make sure that **Local CDC Services** is selected in the left pane of the console.  
  
## Options  
  
### Server Name  
 Type the name of the server where the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is located.  
  
### Authentication  
 Select one of the following:  
  
-   **Windows Authentication**  
  
-   **SQL Server Authentication**: If you select this option, you must type the **User Name** and **Password** for the user in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you are connecting to.  
  
 To prepare the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance for Oracle CDC, the login must have write permission to the MSXDBCDC database. Enter the credentials for a login that has write permission to the MSXDBCDC database, such as a member of the `sysasmin` role.  
  
### Options  
 Click the arrow to view available options to be configured. You can choose to leave these options with their default value. The available options are:  
  
-   **Connection Timeout**: Type the time (in seconds) that the CDC Service for Oracle waits for a connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before timing out. The default value is **15**.  
  
-   **Execution Timeout**: Type the time (in seconds) that the Oracle CDC Windows Service waits for a command to execute before timing out. The default value is **30**.  
  
-   **Encrypt Connection**: Select **Encrypt Connection** for communication between the Oracle CDC Service and the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance using an encrypted connection.  
  
-   **Advanced**: Type any additional connection properties, if necessary.  
  
### View Script  
 Click **View Script** to view a read-only version of the setup script. A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator can copy this script into the SQL Server Management Console to edit it, if necessary. For more information about the Prepare SQL Server Script, see [Prepare SQL Server for Oracle CDC-View Script](../../integration-services/change-data-capture/prepare-sql-server-for-oracle-cdc-view-script.md).  
  
## See Also  
 [How to Work with CDC Services](../../integration-services/change-data-capture/how-to-work-with-cdc-services.md)   
 [How to Prepare SQL Server for CDC](../../integration-services/change-data-capture/how-to-prepare-sql-server-for-cdc.md)  
  
  

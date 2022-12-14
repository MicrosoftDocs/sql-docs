---
description: "Create the SQL Server Change Database"
title: "Create the SQL Server Change Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "oraIns"
ms.assetid: 4f79c24a-e99a-4a06-8637-51eeec406259
author: chugugrace
ms.author: chugu
---
# Create the SQL Server Change Database

  When you start the New Instance wizard, the Create CDC Database page opens. Use the Create CDC Database page to provide information about the new CDC instance and create a new Change database.  
  
 When you create a new CDC database it is enabled for SQL Server CDC and this enablement requires a login that is a member of the `sysadmin` fixed-server role.  
  
 When a user that starts the Create an Oracle CDC Instance wizard is not a member of the `sysadmin` fixed-server role, the Connect to SQL Server dialog box opens and requests the credentials for a member of the sysadmin role to carry out the Enable the database for SQL Server CDC task. When the CDC database is created, the `sysadmin` login is discarded and work resumes with the original [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login used when the Oracle Designer Console was entered.  
  
> [!IMPORTANT]  
>  For tasks other than Enable the database for SQL Server CDC of, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login used for running the New Instance Wizard must have the `dbcreator` fixed-server role and UPDATE permissions on the MSXDBCDC database.  
  
 For information on entering the data in the Connect to SQL Server dialog box, see [SQL Server Connection for Instance Creation](../../integration-services/change-data-capture/sql-server-connection-for-instance-creation.md).  
  
## Options  
 **Oracle CDC Instance**  
 Type the following information about the CDC instance you are creating.  
  
-   **Name**: Type a name for the new service. This will also be the name of the new Change database.  
  
-   **Description**: Type a description for the new instance to help you identify it. This is optional.  
  
 **SQL Server Change Database**  
 This section is used to create the database.  
  
1.  **Change Database**: The name of the new Change database. The name of the database is the same as the name that you gave to the instance. This read-only field displays the full path to the database.  
  
2.  **Create Database**: Click **Create Database** to create the database.  
  
     To create the database, the login must have the `sysasmin` server role. See the security note above for more information.  
  
     After you create the database, you can click **Next** to [Connect to an Oracle Source Database](../../integration-services/change-data-capture/connect-to-an-oracle-source-database.md).  
  
## See Also  
 [How to Create the SQL Server Change Database Instance](../../integration-services/change-data-capture/how-to-create-the-sql-server-change-database-instance.md)   
 [The Oracle CDC Service](../../integration-services/change-data-capture/the-oracle-cdc-service.md)  
  
  

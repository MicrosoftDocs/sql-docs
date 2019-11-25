---
title: "How to Work with CDC Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: db5c718a-6e7f-48ec-82a3-9d5b131716e5
author: janinezhang
ms.author: janinez
manager: craigg
---
# How to Work with CDC Services
  This procedure describes how to use the CDC Service Configuration Console to prepare a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to work with Oracle CDC Services and to create a new CDC service.  
  
### To work with CDC services  
  
1.  From the **Start** menu, select the **CDC Service Configuration for Oracle**.  
  
2.  From the left pane, select **Local CDC Services** (the root level).  
  
3.  You carry out the one or both of the following tasks:  
  
    -   **Prepare SQL Server**  
  
         Select this option from the **Actions** pane on the right side of the CDC Service Configuration Console.  
  
         You can also right-click **Local CDC Services** and select **Prepare SQL Server**.  
  
         The Preparing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Instance for Oracle CDC dialog box opens.  
  
         To prepare the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance for Oracle CDC services, the login must have a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login with the `dbcreator` fixed server role. This login is used to create the MSXDBCDC database that is required for adding Oracle CDC Services and, later on, Oracle CDC Instances.  
  
         For information on how to use this dialog box, see [Prepare SQL Server for CDC](prepare-sql-server-for-cdc.md). For information on how to enable a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance for CDC, see [How to Prepare SQL Server for CDC](how-to-prepare-sql-server-for-cdc.md).  
  
    -   **Create a new CDC service**  
  
         Click **New Service** from the **Actions** pane on the right side of the CDC Service Configuration Console.  
  
         You can also right-Click **Local CDC Services** and select **New Service**.  
  
         The New Oracle CDC Service dialog box opens.  
  
         For information on how to use this dialog box, see [Create and Edit an Oracle CDC Service](create-and-edit-an-oracle-cdc-service.md). For information on how to create or edit a CDC service, see [How to Create and Edit a CDC Service](how-to-create-and-edit-a-cdc-service.md).  
  
         The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login used by the Oracle CDC Service only needs to be a member of the `public` fixed-server role, no other privileges are needed. However, to create the Oracle CDC Service, the login must have write permission to the MSXDBCDC database, for example the **db_owner** database role must be assigned to the login. When a login without write permission to the MSXDBDCDC database attempts to create a new Oracle CDC instance an error message is displayed. Click **OK** in that dialog box to display the Connect to SQL Server dialog box.  
  
         For information on how to enter the credentials for a login that has write permission to the MSXDBCDC database, such the **db_owner** database role, see [Create and Edit an Oracle CDC Service](create-and-edit-an-oracle-cdc-service.md) and [Connection to SQL Server](connection-to-sql-server.md).  
  
## See Also  
 [Work with CDC Services](work-with-cdc-services.md)  
  
  

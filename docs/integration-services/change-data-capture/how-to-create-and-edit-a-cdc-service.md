---
title: "How to Create and Edit a CDC Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 1b3d47a5-dc89-482d-bbc7-fff04f194c43
author: janinezhang
ms.author: janinez
manager: craigg
---
# How to Create and Edit a CDC Service
  These procedures describe how to create and edit a new Oracle CDC Service from the CDC Service Configuration Console.  
  
 This procedure requires a Windows user with administrator privileges on the computer where the Oracle CDC service is configured.  
  
### To create a new CDC service  
  
1.  From the **Start** menu, select **CDC Service Configuration for Oracle**.  
  
2.  From the left pane, right click Local CDC Services and select New Service  
  
     You can also click **New Service** from the **Actions** pane.  
  
3.  Type or enter the required information in the New Oracle CDC Service dialog box. See [Create and Edit an Oracle CDC Service](../../integration-services/change-data-capture/create-and-edit-an-oracle-cdc-service.md) for information on how to enter information in the New Oracle CDC Service dialog box.  
  
     The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login provided in the New Oracle CDC Service dialog box is used by the Oracle CDC Service when the service runs. This login only needs to be a member of the public fixed-server role, no other privileges are needed. Once new Oracle CDC Instances are added, that login receives **db_owner** access to the associated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC databases.  
  
4.  When you finish entering the required information, click **OK**.  
  
     To create the Oracle CDC Windows Service definition, the program needs update access to the MSXDBCDC database in the associated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. When you click **OK**, a dialog box prompts the user to enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login with an update access to the MSXDBCDC database.  
  
     For information about the data you must type into the Connect to SQL Server dialog box, see [Connection to SQL Server](../../integration-services/change-data-capture/connection-to-sql-server.md).  
  
5.  Click **OK** to close the New Oracle CDC Service dialog box.  
  
### To edit a CDC service  
  
1.  From the **Start** menu, select **CDC Service Configuration for Oracle**.  
  
2.  From the left pane, select **Local CDC Services** then right-click the local service you want to edit and select **Properties**.  
  
     You can also select the service you are working with in the middle and then from the **Actions** pane, click **Properties**.  
  
3.  Type or enter the required information in the CDC Service Properties dialog box. See [Create and Edit an Oracle CDC Service](../../integration-services/change-data-capture/create-and-edit-an-oracle-cdc-service.md) for information on how to enter information in the CDC Service Properties dialog box.  
  
4.  When you finish entering the required information, Click **OK**, the Connect to SQL Server dialog box opens.  
  
     When a login without write permission to the MSXDBDCDC database attempts to create a new Oracle CDC instance an error message is displayed. Click **OK** in that dialog box to display the Connect to SQL Server dialog box. In this dialog box you must enter the credentials for a login that has write permission to the MSXDBCDC database, such the **db_owner** database role.  
  
     For information about the data you must type into the Connect to SQL Server dialog box, see [Connection to SQL Server](../../integration-services/change-data-capture/connection-to-sql-server.md).  
  
5.  Click **OK** in the Connect to Oracle dialog box. Both dialog boxes close and the service is updated and registered.  
  
## See Also  
 [Change Data Capture Designer for Oracle by Attunity](../../integration-services/change-data-capture/change-data-capture-designer-for-oracle-by-attunity.md)   
 [Create and Edit an Oracle CDC Service](../../integration-services/change-data-capture/create-and-edit-an-oracle-cdc-service.md)  
  
  

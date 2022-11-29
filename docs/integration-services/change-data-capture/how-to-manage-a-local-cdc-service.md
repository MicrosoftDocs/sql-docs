---
description: "How to Manage a Local CDC Service"
title: "How to Manage a Local CDC Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: 7f9be649-cd93-40c1-bc48-0480106f207c
author: chugugrace
ms.author: chugu
---
# How to Manage a Local CDC Service

  This procedure describes how to use the CDC Service Configuration Console to manage specific CDC services.  
  
### To manage a specific CDC Service  
  
1.  From the **Start** menu, select the **CDC Service Configuration for Oracle**.  
  
2.  From the left pane in the CDC Service Configuration Console, expand **Local CDC Services**.  
  
3.  Select the CDC service you want to work with.  
  
     You can also right-click the CDC service you want to work with and select the desired action.  
  
     **OR**  
  
     Select **Local CDC Services** from the left pane in the CDC Service Configuration Console then select the service you want to work with from the middle section of the CDC Service Configuration Console.  
  
     You can also right-click the CDC service you want to work with and select the desired action.  
  
4.  You can carry out the following tasks when working with a CDC service.  
  
    -   **Delete the service**  
  
         From the **Actions** pane on the right side of the CDC Service Configuration Console, click **Delete** to delete the service.  
  
         You can also right-click the CDC service you want to delete and select **Delete**.  
  
         **Note**: If the service is running when deleting the service, the service is stopped before being deleted.  
  
         To delete an Oracle CDC Windows Service definition, the program needs update access to the MSXDBCDC database in the associated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. When you click **OK** to delete the service, the program attempts to delete the Oracle CDC Service registration in the MSXDBCDC database. If it fails due to lack of permissions, a dialog box is displayed to prompt the user to enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login with an update access to the MSXDBCDC database.  
  
         For information about the data you must enter in the Connect to SQL Server dialog box, see [Manage an Oracle CDC Service](../../integration-services/change-data-capture/manage-an-oracle-cdc-service.md) and [Connection to SQL Server for Delete](../../integration-services/change-data-capture/connection-to-sql-server-for-delete.md).  
  
    -   **Edit the CDC Service Properties**  
  
         From the **Actions** pane on the right side of the CDC Service Configuration Console, click **Properties**.  
  
         You can also right-click the CDC service where you want to edit the properties and select **Properties**.  
  
## See Also  
 [Manage an Oracle CDC Service](../../integration-services/change-data-capture/manage-an-oracle-cdc-service.md)  
  
  

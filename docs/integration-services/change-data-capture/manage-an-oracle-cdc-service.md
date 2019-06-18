---
title: "Manage an Oracle CDC Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "createSrv"
ms.assetid: 5972cee3-b1a9-4c56-aed6-bdddf84af283
author: janinezhang
ms.author: janinez
manager: craigg
---
# Manage an Oracle CDC Service

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  You can use the CDC Service Configuration Console to manage a specific CDC Service.  
  
 **To select the CDC service you want to work with**  
  
1.  From the left pane in the CDC Service Configuration Console, expand **Local CDC Services**.  
  
2.  Select the CDC service you want to work with.  
  
     You can also right-click the CDC service you want to work with and select the desired action. See [What can you do with a CDC Service](../../integration-services/change-data-capture/manage-an-oracle-cdc-service.md#BKMK_WhatcandowithCDCService).  
  
 **OR**  
  
1.  Select **Local CDC Services** from the left pane in the CDC Service Configuration Console.  
  
2.  From the middle section of the CDC Service Configuration Console, select the service you want to work with.  
  
     You can also right-click the CDC service you want to work with and select the desired action. See [What can you do with a CDC Service](../../integration-services/change-data-capture/manage-an-oracle-cdc-service.md#BKMK_WhatcandowithCDCService).  
  
##  <a name="BKMK_WhatcandowithCDCService"></a> What can you do with a CDC Service  
 You can carry out the following actions when working with a CDC service.  
  
### Delete the service  
 From the **Actions** pane on the right side of the CDC Service Configuration Console, click **Delete** to delete the service.  
  
 You can also right-click the CDC service you want to delete and select **Delete**.  
  
 **Note**: If the service is running when deleting the service, the service is stopped before being deleted.  
  
 To delete the Oracle CDC Windows Service definition, the program needs update access to the MSXDBCDC database in the associated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. When you click OK to delete the service, the program attempts to delete the Oracle CDC Service registration in the MSXDBCDC database. If the program cannot delete the Oracle CDC Service registration because it does not have the proper permissions, it prompts the user to enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login with update permissions to the MSXDBCDC database.  
  
 For information about the data you must enter in the Connect to SQL Server dialog box, see [Connection to SQL Server for Delete](../../integration-services/change-data-capture/connection-to-sql-server-for-delete.md).  
  
### Edit the CDC Service Properties  
 From the **Actions** pane on the right side of the CDC Service Configuration Console, click **Properties**.  
  
 You can also right-click the CDC service where you want to edit the properties and select **Properties**.  
  
## See Also  
 [How to Manage a Local CDC Service](../../integration-services/change-data-capture/how-to-manage-a-local-cdc-service.md)  
  
  

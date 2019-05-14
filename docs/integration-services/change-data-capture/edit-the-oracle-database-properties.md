---
title: "Edit the Oracle Database Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "oraProp"
ms.assetid: 58dc99f1-ee6b-4508-bb66-2bc589611ff7
author: janinezhang
ms.author: janinez
manager: craigg
---
# Edit the Oracle Database Properties

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Use the Oracle tab in the Properties editor to make changes to the description you provided in the Create CDC database page in the New Instance wizard and to make changes to the Oracle Log Mining database connection information.  
  
 The following describes the information in the **Oracle** tab.  
  
 **Name**  
 The name of the CDC Instance that you entered in the Create CDC Database page in the New Instance wizard. This field is read only and you cannot edit this information.  
  
 **Description**  
 You can edit the description of the new instance or add one if you did not do so when creating the CDC Instance.  
  
 **Oracle Connect String**  
 The Oracle connect string to the computer with the Oracle database you are using. This field is read only and you cannot edit this information. This is because some changes to the connect string may point the Oracle CDC Instance to another Oracle database entirely, corrupting the CDC Instance state as stored in the **cdc.xdbcdc_config** table. If minor changes are needed, you can change the connect string directly in the configuration table using SQL Server Management Studio.  
  
 **Oracle Log Mining Authentication**  
 To enter the authentication credentials for the Oracle database that contains the log miner, select one of the following under **Authentication**:  
  
-   **Windows Authentication**: Select this to use the current Windows domain credentials. You can use this option only if the Oracle database is configured to work with Windows authentication.  
  
-   **Oracle Authentication**: If you select this option, you must type the **User Name** and **Password** for the user in the Oracle database you are connecting to.  
  
 You can view the Oracle database properties in the viewer. When using the viewer the information is read only. The viewer also includes a list of the captured columns in the table. For information on how to access the viewer, see [How to Manage a CDC Instance](../../integration-services/change-data-capture/how-to-manage-a-cdc-instance.md).  
  
## See Also  
 [How to Manage a CDC Service from the CDC Designer Console](../../integration-services/change-data-capture/how-to-manage-a-cdc-service-from-the-cdc-designer-console.md)   
 [Connect to an Oracle Source Database](../../integration-services/change-data-capture/connect-to-an-oracle-source-database.md)   
 [Connect to Oracle](../../integration-services/change-data-capture/connect-to-oracle.md)  
  
  

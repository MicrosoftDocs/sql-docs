---
description: "How to Manage a CDC Instance"
title: "How to Manage a CDC Instance | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: 5d9e677f-b872-497d-9cde-472184a214ab
author: chugugrace
ms.author: chugu
---
# How to Manage a CDC Instance

  This procedure describes how to use the CDC Designer Console to manage CDC instance operations at runtime.  
  
### To manage CDC instance operations  
  
1.  From the **Start** menu, select the **CDC Designer Console**.  
  
2.  In the left pane, expand **Change Data Capture** then expand the service that contains the instance you want to view.  
  
3.  Select the name of an instance you want to work with.  
  
4.  From the **Actions** pane on the right side of the CDC Designer Console, click on the operation you want to carry out.  
  
     You can also right-click the name of the instance in the left pane and select the operation you want to carry out.  
  
     You can carry out the following tasks:  
  
    -   **Start**: To start capturing changes.  
  
    -   **Stop**: To stop capturing changes  
  
    -   **Reset**: Click **Reset** to reset the CDC instance to its initial (empty) state. This option is available when the CDC instance is stopped. All changes in the change tables and the CDC instance internal state are deleted. When the CDC instance is started later on, change capture will start from that point in time and only includes transactions that started after the CDC instance started.  
  
    -   **Delete**: To delete the CDC instance.  
  
    -   **Oracle Logging Script**: Click **Oracle Logging Script** to display the Oracle Logging script dialog box with the Oracle supplemental-logging script. For information on what you can do in this dialog box, see [Oracle Supplemental Logging Script](../../integration-services/change-data-capture/oracle-supplemental-logging-script.md).  
  
         **Note**: When you run the supplemental logging scripts, the Oracle Credentials for Running Script dialog box opens where you provide a valid Oracle user name and password. For information on how to provide the proper Oracle credentials, see [Oracle Credentials for Running Script](../../integration-services/change-data-capture/oracle-credentials-for-running-script.md).  
  
    -   **CDC Instance Deployment**: To generate a deployment script for the CDC Instance. For information about this dialog box, see [CDC Instance Deployment Script](../../integration-services/change-data-capture/cdc-instance-deployment-script.md).  
  
     For more information about these tasks, see [Manage a CDC Instance](../../integration-services/change-data-capture/manage-a-cdc-instance.md).  
  
 You can also select **Properties** to edit the CDC instance configuration properties. For more information about editing the CDC instance properties, see [Edit Instance Properties](../../integration-services/change-data-capture/edit-instance-properties.md).  
  
  

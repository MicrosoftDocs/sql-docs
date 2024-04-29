---
title: "Oracle Credentials for Running Script"
description: "Oracle Credentials for Running Script"
author: chugugrace
ms.author: chugu
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
---
# Oracle Credentials for Running Script

[!INCLUDE [oracle-cdc-retirement](../includes/attunity-oracle-cdc-retirement.md)]

  To run the Oracle supplemental logging script from the Oracle CDC Designer console, the program prompts you for the credentials of the Oracle user who is running the script. To run this script, the Oracle user must have ALTER TABLE permission for all the tables to be captured and SELECT permission on the DBA_LOG_GROUPS view.  
  
## Task List  
 Enter the following in this dialog box:  
  
 **Authentication**  
  
 Select one of the following:  
  
-   **Windows Authentication**: Select this to use the current Windows domain credentials. You can use this option only if the Oracle database is configured to work with Windows authentication.  
  
-   **Oracle Authentication**: If you select this option, you must type the **User Name** and **Password** for the user in the Source Oracle database you are connecting to.  
  
## See Also  
 [How to Manage a CDC Instance](../../integration-services/change-data-capture/how-to-manage-a-cdc-instance.md)   
 [Review and Generate Supplemental Logging Scripts](../../integration-services/change-data-capture/review-and-generate-supplemental-logging-scripts.md)  
  
  

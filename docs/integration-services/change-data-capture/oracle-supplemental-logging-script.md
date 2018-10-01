---
title: "Oracle Supplemental Logging Script | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 5e6ee618-b89b-46c7-92ad-4fc5ef7b777a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Oracle Supplemental Logging Script
  This dialog box shows the script the Oracle supplemental logging script.  
  
 When you prepare a CDC Instance for use, the CDC Designer creates an Oracle SQL script that sets up supplemental logging for the tables to be captured. The supplemental logging script tells Oracle that when a specific table is updated, the change records it writes to the transaction log should contain the data of all columns of interest, not just the columns that changed.  
  
 Depending on the Oracle DBA policies in your organization, running the supplemental logging script may require a review and approval by an Oracle DBA.  
  
## Options  
 The following are the available options for how to execute the script.  
  
 **Run Script**  
 Runs the supplemental logging script on the tables defined for the CDC instance. To run this script, the Oracle user must have ALTER TABLE permission for all the tables to be captured and SELECT permission on the DBA_LOG_GROUPS view. In addition, the Oracle user must have the credentials for an Oracle database use with the required permissions. You can let the program prompt you for the Oracle credentials and then have it run the script.  
  
 **Save As**  
 Saves the script to a text file. This is used if an Oracle database administrator (DBA) needs to examine and execute the supplemental logging script, the program lets you save the script to a text file that you can later send to the Oracle DBA by email or by other means to be execute later (by using the SQL*Plus Oracle utility or other tool for running scripts on an Oracle database).  
  
 **Copy**  
 Copies the script to the clipboard. When ready you can paste the script in any location you need in case an Oracle database administrator (DBA) needs to examine and execute the supplemental logging script.  
  
## See Also  
 [How to Manage a CDC Instance](../../integration-services/change-data-capture/how-to-manage-a-cdc-instance.md)   
 [Manage a CDC Instance](../../integration-services/change-data-capture/manage-a-cdc-instance.md)  
  
  

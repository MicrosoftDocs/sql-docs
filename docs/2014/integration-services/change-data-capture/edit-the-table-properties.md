---
title: "Edit the Table Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "editTabProps"
ms.assetid: 95ea72ba-8e40-4177-a963-0fb4d10c56e3
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Edit the Table Properties
  Use this dialog box to edit the specific columns from the selected table where changes are being captured. You can also edit the **Security Role** and **Capture Instance** information.  
  
### To edit the columns to include in the CDC instance  
  
1.  Do one or both of the following:  
  
    -   Select the check box next to any additional column you want to include.  
  
    -   Clear the check box next to any column that you no longer want to include.  
  
### To edit the Security Role  
  
1.  Type a new name or edit the name of the security role in the **Security Role** field.  
  
### To create a new capture instance  
  
1.  In the **Security Role** section, **Name** field enter a name for the capture instance. By default, the name entered in the field is the name of the current capture instance with **_NEW** added to the end of the name (for example, **old_instance_NEW**).  
  
2.  Save the capture instance as one of the following:  
  
    -   **New Capture Instance**: In this case a new capture instance is saved and the old capture instance is not deleted.  
  
         **Note**: You can have no more than two capture instances per table. If there are already two capture instances, this option is not available.  
  
    -   **Replace Existing**: In this case the current capture instance is deleted and replaced by the capture instance you created. If there are two capture instances defined for this table, you must select one to replace.  
  
 **Note**: You can remove a capture instance from the list of tables in the **Table** tab.  
  
 After you finish entering the information in this dialog box, click **OK** to accept the changes.  
  
## See Also  
 [How to Edit the CDC Instance Properties](how-to-edit-the-cdc-instance-properties.md)   
 [Make Changes to the Tables Selected for Capturing Changes](make-changes-to-the-tables-selected-for-capturing-changes.md)  
  
  

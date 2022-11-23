---
description: "Assessment Report (AccessToSQL)"
title: "Assessment Report (AccessToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Assessment Report dialog box"
  - "Conversion Report dialog box"
ms.assetid: ba6f53aa-0049-4c49-8bb8-607a8bfaa737
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
  - "ssma.access.assessmentreport.f1"
---
# Assessment Report (AccessToSQL)
The Assessment Report window shows the results of the conversion of database objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, and can also help you estimate the complexity and cost of your migration projects.  
  
To create an assessment report, select objects to convert in the source metadata explorer, right-click **Databases**, and then select **Create Report**. You can also display this report automatically after you convert schemas. However, the report name will be Conversion Report. For more information, see [Project Settings (GUI) (SSMA Common)](../sybase/project-settings-gui-sybasetosql.md).  
  
## Options  
**Explorer pane**  
Contains a hierarchy of objects in the assessment report. Expand folders to view individual objects and subcomponents. When you click a category or object, conversion statistics for that category or object appear in the details pane.  
  
**Details pane**  
Shows conversion statistics or error and warning messages for the selected object. For example, if the Tables folder is selected, the details pane will show the numbers of foreign keys, indexes, primary keys, and tables that were converted.  
  
**Messages pane**  
Shows the errors, warnings, and information messages that were generated when the assessment report was created. Messages are grouped by number.  
  
To view message details, click either **Errors**, **Warnings**, or **Messages**, and then expand a message. SSMA will display the list of objects that have this error. Click an object to display all conversion details for the object.  
  
## See Also  
[User Interface Reference(Access)](./user-interface-reference-accesstosql.md)  

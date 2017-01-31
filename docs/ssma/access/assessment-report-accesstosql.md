---
title: "Assessment Report (AccessToSQL) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
helpviewer_keywords: 
  - "Assessment Report dialog box"
  - "Conversion Report dialog box"
ms.assetid: ba6f53aa-0049-4c49-8bb8-607a8bfaa737
caps.latest.revision: 14
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# Assessment Report (AccessToSQL)
The Assessment Report window shows the results of the conversion of database objects to [!INCLUDE[tsql](../../includes/tsql_md.md)] syntax, and can also help you estimate the complexity and cost of your migration projects.  
  
To create an assessment report, select objects to convert in the source metadata explorer, right-click **Databases**, and then select **Create Report**. You can also display this report automatically after you convert schemas. However, the report name will be Conversion Report. For more information, see [Project Settings (GUI) (SSMA Common)](http://msdn.microsoft.com/en-us/cf06baf1-8714-48a3-95dc-781f6ca53693).  
  
## Options  
**Explorer pane**  
Contains a hierarchy of objects in the assessment report. Expand folders to view individual objects and subcomponents. When you click a category or object, conversion statistics for that category or object appear in the details pane.  
  
**Details pane**  
Shows conversion statistics or error and warning messages for the selected object. For example, if the Tables folder is selected, the details pane will show the numbers of foreign keys, indexes, primary keys, and tables that were converted.  
  
**Messages pane**  
Shows the errors, warnings, and information messages that were generated when the assessment report was created. Messages are grouped by number.  
  
To view message details, click either **Errors**, **Warnings**, or **Messages**, and then expand a message. SSMA will display the list of objects that have this error. Click an object to display all conversion details for the object.  
  
## See Also  
[User Interface Reference(Access)](http://msdn.microsoft.com/en-us/af24c303-4a41-449b-9c86-d6558a97e839)  
  

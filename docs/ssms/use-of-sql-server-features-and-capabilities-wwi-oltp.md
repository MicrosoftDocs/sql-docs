---
title: "Arguments for External Tools"
description: "Arguments for External Tools"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "arguments [SQL Server Management Studio]"
  - "external tools [SQL Server Management Studio]"
author: "markingmyname"
ms.author: "maghan"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
---

# Arguments for External Tools

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Arguments are variables that the Studio environment supplies values for when an external tool is launched from the **Tools** menu. External tools such as Notepad can be added to the **Tools** menu using the **External Tools** dialog box.  
  
The following table lists arguments for external tools.  
  
|Name|Argument|Description|  
|--------|------------|---------------|  
|**Item Path**|$(ItemPath)|The complete file name of the current source (defined as drive + path + file name); blank if a non-source window is active.|  
|**Item Directory**|$(ItemDir)|The directory of the current source (defined as drive + path); blank if a non-source window is active.|  
|**Item File Name**|$(ItemFilename)|The file name of the current source (defined as file name); blank if a non-source window is active.|  
|**Item extension**|$(ItemExt)|The file name extension of the current source.|  
|**Current Line***|$(CurLine)|The current line position of the cursor in the editor.|  
|**Current Column***|$(CurCol)|The current column position of the cursor in the editor.|  
|**Current Text***|$(CurText)|The current text (the word under the current cursor position, or a single-line selection, if there is one).|  
|**Target Path**|$(TargetPath)|The complete file name of the target (defined as drive + path + file name).|  
|**Target Directory**|$(TargetDir)|The directory of the target.|  
|**Target Name**|$(TargetName)|The file name of the target.|  
|**Target Extension**|$(TargetExt)|The file name extension of the target.|  
|**Project Directory**|$(ProjDir)|The directory of the current project (defined as drive + path).|  
|**Project File Name**|$(ProjFileName)|The file name of the current project (defined as drive + path + file name).|  
|**Solution Directory**|$(SolutionDir)|The directory of the current solution (defined as drive + path).|  
|**Solution File Name**|$(SolutionFileName)|The file name of the current solution (defined as drive + path + file name).|  
  
*The current line, current column, or current text is based on the position of the cursor in the text editor as shown in the status bar.  
  
## See Also  
[External Tools Dialog Box](../ssms/external-tools-dialog-box.md)  
[General User Interface Elements](../ssms/general-user-interface-elements.md)  
  

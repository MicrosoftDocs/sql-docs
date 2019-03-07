---
title: "Arguments for External Tools | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "arguments [SQL Server Management Studio]"
  - "external tools [SQL Server Management Studio]"
ms.assetid: 3991c13a-f23f-450b-a2ba-19391c399735
author: stevestein
ms.author: sstein
manager: craigg
---
# Arguments for External Tools
  Arguments are variables that the Studio environment supplies values for when an external tool is launched from the **Tools** menu. External tools such as Notepad can be added to the **Tools** menu using the **External Tools** dialog box.  
  
 The following table lists arguments for external tools.  
  
|Name|Argument|Description|  
|----------|--------------|-----------------|  
|**Item Path**|$(ItemPath)|The complete file name of the current source (defined as drive + path + file name); blank if a non-source window is active.|  
|**Item Directory**|$(ItemDir)|The directory of the current source (defined as drive + path); blank if a non-source window is active.|  
|**Item File Name**|$(ItemFilename)|The file name of the current source (defined as file name); blank if a non-source window is active.|  
|**Item extension**|$(ItemExt)|The file name extension of the current source.|  
|**Current Line** <sup>1</sup>|$(CurLine)|The current line position of the cursor in the editor.|  
|**Current Column**1|$(CurCol)|The current column position of the cursor in the editor.|  
|**Current Text**1|$(CurText)|The current text (the word under the current cursor position, or a single-line selection, if there is one).|  
|**Target Path**|$(TargetPath)|The complete file name of the target (defined as drive + path + file name).|  
|**Target Directory**|$(TargetDir)|The directory of the target.|  
|**Target Name**|$(TargetName)|The file name of the target.|  
|**Target Extension**|$(TargetExt)|The file name extension of the target.|  
|**Project Directory**|$(ProjDir)|The directory of the current project (defined as drive + path).|  
|**Project File Name**|$(ProjFileName)|The file name of the current project (defined as drive + path + file name).|  
|**Solution Directory**|$(SolutionDir)|The directory of the current solution (defined as drive + path).|  
|**Solution File Name**|$(SolutionFileName)|The file name of the current solution (defined as drive + path + file name).|  
  
 <sup>1</sup> The current line, current column, or current text is based on the position of the cursor in the text editor as shown in the status bar.  
  
## See Also  
 [External Tools Dialog Box](external-tools-dialog-box.md)   
 [General User Interface Elements](general-user-interface-elements.md)  
  
  

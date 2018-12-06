---
title: "XMLA Query Editor (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.editor.xmla.f1"
helpviewer_keywords: 
  - "XMLA Query Editor"
  - "Query Editor [XMLA]"
ms.assetid: 14623019-7839-4038-9d12-2f8953d2ec04
author: minewiskan
ms.author: owend
manager: craigg
---
# XMLA Query Editor (Analysis Services - Multidimensional Data)
  Use the XMLA Query Editor to design and execute statements and scripts written in the Multidimensional Expressions (XMLA) language.  
  
## Features  
  
-   Type scripts in the query editor pane of XMLA Query Editor.  
  
-   To execute scripts press **F5**, or click **Execute** on the toolbar, or on the **Query** menu, click **Execute**. If a portion of the code is selected, only that portion is executed. If no code is selected, the entire content of the XMLA Query Editor is executed.  
  
-   View metadata for cubes and other objects contained in an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database in the metadata pane.  
  
-   For help with XMLA syntax, select a keyword in XMLA Query Editor, and then click **F1**.  
  
-   For dynamic help with XMLA syntax, on the **Help** menu, click **Dynamic Help** to open the Dynamic Help component. With Dynamic Help, help topics appear in the **Dynamic Help** window when keywords are typed in Query Editor.  
  
## SQL Server Analysis Services Editors Toolbar  
 When XMLA Query Editor is open, the **SQL Server Analysis Services Editors** toolbar appears with the following buttons.  
  
|Term|Definition|  
|----------|----------------|  
|**Connect**|Opens the **Connect to Server** dialog box, to establish a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.|  
|**Disconnect**|Disconnects the XMLA Query Editor from an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.|  
|**Change Connection**|Opens the **Connect to Server** dialog box, to establish a connection to a different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.|  
|**New Query with Current Connection**|Opens a new XMLA Query Editor window, using the connection information from the current XMLA Query Editor window.|  
|**Available Databases**|Changes the connection to a different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database on the same instance.|  
|**Execute**|Executes the selected code, or if no code is selected, this option executes the entire code in the XMLA Query Editor.|  
|**Parse**|Checks the syntax of the selected code. If no code is selected, this option checks the syntax of the entire XMLA Query Editor window.|  
|**Cancel Executing Query**|Sends a cancellation request to the server. Some queries cannot be canceled immediately, but must wait for a suitable cancellation condition. When queries are canceled, delays may occur while transactions are rolled back.|  
  
## XMLA Query Editor Window  
 The following options are available in XMLA Query Editor:  
  
|Term|Definition|  
|----------|----------------|  
|**Query editor window**|Type XMLA statements and scripts to be executed by the XMLA Query Editor.<br /><br /> The context menu for the query editor provides the following options:<br /><br /> **Cut**: Copies the current selection to the clipboard and removes the selection from the query editor window.<br />**Copy**: Copies the current selection to the clipboard.<br />**Paste**: Pastes the contents of the clipboard to the current selection.<br />**Connect**: Opens the **Connect to Server** dialog box, to establish a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.<br />**Disconnect**: Disconnects the current query editor from an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.<br />**Disconnect All Queries**: Disconnects all open query editors.<br />**Change Connection**: Opens the **Connect to Server** dialog box, to establish a connection to a different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.<br />**Open Server in Object Explorer**: Opens the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance to which the current query editor is connected in **Object Explorer**.<br />**Execute**: Executes the selected code, or if no code is selected, executes the entire code in the current query editor.<br />**Properties Window**: Displays the **Properties** window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] for the current query window.<br />**Query Options**: Displays the **Query Options** dialog box.|  
|**Results window**|Displays the results of an XMLA statement or script in text.|  
|**Messages window**|Displays information about how an XMLA statement or script executed. For example, this window displays any errors encountered during execution or the number of cells retrieved after execution.|  
  
## See Also  
 [MDX Query Editor &#40;Analysis Services - Multidimensional Data&#41;](mdx-query-editor-analysis-services-multidimensional-data.md)   
 [DMX Query Editor &#40;Analysis Services - Data Mining&#41;](dmx-query-editor-analysis-services-data-mining.md)   
 [Query and Text Editors &#40;SQL Server Management Studio&#41;](../relational-databases/scripting/query-and-text-editors-sql-server-management-studio.md)   
 [SQL Server Management Studio Keyboard Shortcuts](../ssms/sql-server-management-studio-keyboard-shortcuts.md)  
  
  

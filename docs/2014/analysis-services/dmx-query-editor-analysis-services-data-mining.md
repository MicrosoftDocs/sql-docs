---
title: "DMX Query Editor (Analysis Services - Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.sqlserverstudio.startpage.dmx.f1"
ms.assetid: 7ac877a1-0f29-46b9-9a51-73b02172bef1
author: minewiskan
ms.author: owend
manager: craigg
---
# DMX Query Editor (Analysis Services - Data Mining)
  Use the DMX Query Editor to design and execute statements written in the Data Mining Extensions (DMX) language.  
  
## Features  
  
-   Type scripts in the query editor pane of DMX Query Editor.  
  
-   To execute scripts press **F5**, or click **Execute** on the toolbar or on the **Query** menu. If a portion of the code is selected, only that portion is executed. If no code is selected, the entire content of the DMX Query Editor is executed.  
  
-   View metadata for cubes and other objects contained in an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database in the metadata pane  
  
-   For help with DMX syntax, select a keyword in DMX Query Editor, and then click **F1**.  
  
-   For dynamic help with DMX syntax, on the **Help** menu, click **Dynamic Help** to open the Dynamic Help component. With Dynamic Help, help topics appear in the **Dynamic Help** window when keywords are typed in Query Editor.  
  
## SQL Server Analysis Services Editors Toolbar  
 When DMX Query Editor is open, the **SQL Server Analysis Services Editors** toolbar appears with the buttons listed in the following table.  
  
|Term|Definition|  
|----------|----------------|  
|**Connect**|Opens the **Connect to Server** dialog box, to establish a connection to an Analysis Services instance.|  
|**Disconnect**|Disconnects the DMX Query Editor from an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.|  
|**Change Connection**|Opens the **Connect to Server** dialog box, to establish a connection to a different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.|  
|**New Query with Current Connection**|Opens a new DMX Query Editor window, using the connection information from the current DMX Query Editor window.|  
|**Available Databases**|Changes the connection to a different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database on the same instance.|  
|**Execute**|Executes the selected code, or if no code is selected, this option executes the entire code in the DMX Query Editor.|  
|**Parse**|Checks the syntax of the selected code. If no code is selected, this option checks the syntax of the entire DMX Query Editor window.|  
|**Cancel Executing Query**|Sends a cancellation request to the server. Some queries cannot be canceled immediately, but must wait for a suitable cancellation condition. When canceled, delays may occur while transactions are rolled back.|  
  
## DMX Query Editor Window  
 The options listed in the following table are available in DMX Query Editor.  
  
|Term|Definition|  
|----------|----------------|  
|**Query editor window**|Type DMX statements and scripts to be executed by the DMX Query Editor.<br /><br /> The context menu for the query editor provides the following options:<br /><br /> **Cut**: Copies the current selection to the clipboard and removes the selection from the query editor window.<br /><br /> **Copy**: Copies the current selection to the clipboard.<br /><br /> **Paste**: Pastes the contents of the clipboard to the current selection.<br /><br /> **Connect**: Opens the **Connect to Server** dialog box, to establish a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.<br /><br /> **Disconnect**: Disconnects the current query editor from an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.<br /><br /> **Disconnect All Queries**: Disconnects all open query editors.<br /><br /> **Change Connection**: Opens the **Connect to Server** dialog box, to establish a connection to a different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.<br /><br /> **Open Server in Object Explorer**: Opens the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance to which the current query editor is connected in **Object Explorer**.<br /><br /> **Execute**: Executes the selected code, or if no code is selected, executes the entire code in the current query editor.<br /><br /> **Properties Window**: Displays the **Properties** window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] for the current query window.<br /><br /> **Query Options**: Displays the **Query Options** dialog box.|  
|**Metadata window**|Displays metadata for the currently connected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.|  
|**Cube**|Select a cube in the currently connected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database to display the metadata associated with the cube in the **Metadata** tab.|  
|**Metadata**|Displays the metadata for the cube selected in **Cube**, including measure groups and measures, key performance indicators, dimensions, hierarchies, levels, members, and member properties. To retrieve the fully qualified key of an object, either:<br /><br /> Drag the object from the **Metadata** tab to the query pane.<br /><br /> Or:<br /><br /> Right-click the object and select **Copy**, and then right-click the query pane and select **Paste**.|  
|**Functions**|Displays the metadata for DMX functions available to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, as retrieved from the DMSCHEMA_MINING_FUNCTIONS schema rowset.<br /><br /> To retrieve the syntax of a function, either:<br /><br /> Drag the object from the **Functions** tab to the query pane.<br /><br /> Or:<br /><br /> Right-click the function and select **Copy**, and then right-click the query pane and select **Paste**.|  
|**Results window**|Displays the results of a DMX statement in a grid.|  
|**Messages window**|Displays information about how a DMX statement executed. For example, this window displays any errors encountered during execution or the number of cells retrieved after execution.|  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Data Mining Extensions &#40;DMX&#41; Reference](/sql/dmx/data-mining-extensions-dmx-reference)   
 [MDX Query Editor &#40;Analysis Services - Multidimensional Data&#41;](mdx-query-editor-analysis-services-multidimensional-data.md)   
 [XMLA Query Editor &#40;Analysis Services - Multidimensional Data&#41;](xmla-query-editor-analysis-services-multidimensional-data.md)   
 [Query and Text Editors &#40;SQL Server Management Studio&#41;](../relational-databases/scripting/query-and-text-editors-sql-server-management-studio.md)   
 [SQL Server Management Studio Keyboard Shortcuts](../ssms/sql-server-management-studio-keyboard-shortcuts.md)   
 [Customize Menus and Shortcut Keys](../ssms/customize-menus-and-shortcut-keys.md)   
 [Color Coding in Query Editors](../relational-databases/scripting/color-coding-in-query-editors.md)  
  
  

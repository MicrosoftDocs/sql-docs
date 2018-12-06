---
title: "MDX Query Editor (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.sqlserverstudio.startpage.mdx.f1"
helpviewer_keywords: 
  - "Query Editor [MDX]"
  - "MDX Query Editor"
ms.assetid: 777f2c23-1c1c-4b72-9d19-48a4866551f8
author: minewiskan
ms.author: owend
manager: craigg
---
# MDX Query Editor (Analysis Services - Multidimensional Data)
  Use the MDX Query Editor to design and execute statements and scripts written in the Multidimensional Expressions (MDX) language.  
  
## Features  
  
-   Type scripts in the query editor pane of MDX Query Editor.  
  
-   To execute scripts press **F5**, or click **Execute** on the toolbar, or on the **Query** menu, click **Execute**. If a portion of the code is selected, only that portion is executed. If no code is selected, the entire content of the MDX Query Editor is executed.  
  
-   View metadata for cubes and other objects contained in an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database in the metadata pane.  
  
-   For help on MDX syntax, select a keyword in MDX Query Editor, and then click **F1**.  
  
-   For dynamic help with MDX syntax, on the **Help** menu, click **Dynamic Help**, to open the Dynamic Help component. With Dynamic Help, help topics appear in the **Dynamic Help** window when keywords are typed in Query Editor.  
  
## SQL Server Analysis Services Editors Toolbar  
 When MDX Query Editor is open, the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Editors toolbar appears with the buttons listed in the following table.  
  
|Term|Definition|  
|----------|----------------|  
|**Connect**|Opens the **Connect to Server** dialog box, to establish a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.|  
|**Disconnect**|Disconnects the MDX Query Editor from an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.|  
|**Change Connection**|Opens the **Connect to Server** dialog box, to establish a connection to a different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.|  
|**New Query with Current Connection**|Opens a new MDX Query Editor window, using the connection information from the current MDX Query Editor window.|  
|**Available Databases**|Change the connection to a different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database on the same instance.|  
|**Execute**|Executes the selected code, or if no code is selected, executes the entire code in the MDX Query Editor.|  
|**Parse**|Checks the syntax of the selected code. If no code is selected, checks the syntax of the entire MDX Query Editor window.|  
|**Cancel Executing Query**|Sends a cancellation request to the server. Some queries cannot be canceled immediately, but must wait for a suitable cancellation condition. When queries are canceled, delays may occur while transactions are rolled back.|  
  
## MDX Query Editor Window  
 The options listed in the following table are available in MDX Query Editor.  
  
|Term|Definition|  
|----------|----------------|  
|**Query editor window**|Type MDX statements and scripts to be executed by the MDX Query Editor.<br /><br /> The context menu for the query editor provides the following options:<br /><br /> **Cut**: Copies the current selection to the clipboard and removes the selection from the query editor window.<br /><br /> **Copy**: Copies the current selection to the clipboard.<br /><br /> **Paste**: Pastes the contents of the clipboard to the current selection.<br /><br /> **Connect**: Opens the **Connect to Server** dialog box, to establish a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.<br /><br /> **Disconnect**: Disconnects the current query editor from an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.<br /><br /> **Disconnect All Queries**: Disconnects all currently open query editors.<br /><br /> **Change Connection**: Opens the **Connect to Server** dialog box, to establish a connection to a different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.<br /><br /> **Open Server in Object Explorer**: Opens the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance to which the current query editor is connected in **Object Explorer**.<br /><br /> **Execute**: Execute the selected code, or if no code is selected, executes the entire code in the current query editor.<br /><br /> **Properties Window**: Displays the **Properties** window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] for the current query window.<br /><br /> **Query Options**: Displays the **Query Options** dialog box.|  
|**Metadata window**|Displays metadata for the currently connected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.|  
|**Cube**|Select a cube in the currently connected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database to display the metadata associated with the cube in the **Metadata** tab.|  
|**Metadata**|Displays the metadata for the cube selected in **Cube**, including measure groups and measures, key performance indicators, dimensions, hierarchies, levels, members, and member properties. To retrieve the fully qualified key of an object, either:<br /><br /> Drag the object from the **Metadata** tab to the query pane.<br /><br /> Right-click the object and select **Copy**, then right-click the query pane and select **Paste**.|  
|**Functions**|Displays the metadata for MDX functions available to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, as retrieved from the MDSCHEMA_FUNCTIONS schema rowset. To retrieve the syntax of a function, either:<br /><br /> Drag the object from the **Functions** tab to the query pane.<br /><br /> Right-click the function and select **Copy**, and then right-click the query pane and select **Paste**.|  
|**Results window**|Displays the results of an MDX statement or script in a grid.|  
|**Messages window**|Displays information about how a MDX statement or script executed. For example, this window displays any errors encountered during execution or the number of cells retrieved after execution.|  
  
  

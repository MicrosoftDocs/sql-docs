---
title: "Add Reference Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.addreference.f1"
  - "sql12.asvs.bidevstudio.assembly.addassembly.f1"
helpviewer_keywords: 
  - "Add Reference dialog box"
ms.assetid: 457958c4-6baa-474d-99a0-34c195ceba09
author: minewiskan
ms.author: owend
manager: craigg
---
# Add Reference Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Add Reference** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to add a reference to a [!INCLUDE[msCoName](../includes/msconame-md.md)] .NET Framework assembly or to another project to your development project. You can display the **Add Reference** dialog box by right-clicking the **Assemblies** folder of an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project in **Solution Explorer** and selecting **New Assembly Reference** from the context menu.  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**.NET**|Select this tab to add a reference to a registered component. This tab displays a grid that contains a list of registered .NET Framework components. Select one or more items from the grid, and then click **Add** to add the selected .NET components to **Selected products and components**. The grid contains the following columns:<br /><br /> **Component name**: The full, or "friendly," name of the component. Select the title to sort by the component name.<br /><br /> **Version**: The listed version number of the component. Select the title to sort by the version.<br /><br /> **Runtime**: The version of the .NET Framework on which the component is based. Select the title to sort by the runtime version.<br /><br /> **Path**: The file name of the component and the path where it is located. Select the title to sort by the path.|  
|**Browse**|Click to browse the file system for an assembly not listed in the **.NET** or **Recent** tabs. This tab displays the following options:<br /><br /> **Look in**: Select a folder from this drop-down list. Selecting a folder from this list displays the contents of the folder in the primary pane.<br /><br /> **Go to last folder visited**: Returns **Look in** to the previous location.<br /><br /> **Up One Level**: Locates the next higher folder in the hierarchy.<br /><br /> **Create New Folder**: Select to create a new child folder under the folder selected in **Look in**.<br /><br /> **View Menu**: Select to change the view of contents in the primary pane.  For more information about the options in **View Menu**, see the "Viewing files and folders overview" topic in the [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows documentation. The following options are available:<br />Thumbnails<br />Tiles<br />Icons<br />List<br />Details<br /><br /> **Primary pane**: Displays the contents of the folder selected in **Look in**. Select one or more items, and then click **Add** to add the selected files to **Selected products and components**. For more information about the options and context menu in the primary pane, see the "Viewing files and folders overview" topic in the Windows documentation.<br /><br /> **File name**: Use this option to filter the files and folders that are displayed. Type a full or partial file name on which to filter. You can use the asterisk (\*) as a wildcard. Type multiple file names (with each file name framed in double quotes (") and delimited by a space) to select multiple files. You can also select previously reviewed files by selecting a file name from the drop-down list. Tip: You can locate Web servers and network computers by entering a URL or network path in **File name**. For example, "http://mywebsite" displays the files available at the "mywebsite" Web location and "\\\myserver\myshare" displays the files available at the "myshare" location on "myserver".<br /><br /> **Files of type**: Use this option to filter the contents of the folder or directory selected in **Look in** for a particular file type.|  
|**Recent**|Displays a list of component references recently added to projects in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. Select one or more items from the grid, and then click **Add** to add the selected .NET Framework assemblies to **Selected products and components**. The grid contains the following columns:<br /><br /> **Component name**: The full, or "friendly," name of the component. Select the title to sort by the component name.<br /><br /> **Version**: The listed version number of the component. Select the title to sort by the version.<br /><br /> **Runtime**: The version of the .NET Framework on which the component is based. Select the title to sort by the runtime version.<br /><br /> **Path**: The file name of the component and the path where it is located. Select the title to sort by the path.|  
|**Add**|Click to add a component selected in the **.NET**, **Browse**, or **Recent** tabs to **Selected projects and components**.|  
|**Remove**|Click to remove a component selected in **Selected projects and components**.|  
|**Selected projects and components**|Displays a list of component references to be added to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. Select one or more items from the grid, and then click **Remove** to remove the selected components from the grid. The grid contains the following columns:<br /><br /> **Component name**: The full, or "friendly," name of the component. Select the title to sort by the component name.<br /><br /> **Version**: The listed version number of the component. Select the title to sort by the version.<br /><br /> **Runtime**: The version of the .NET Framework on which the component is based. Select the title to sort by the runtime version.<br /><br /> **Path**: The file name of the component and the path where it is located. Select the title to sort by the path.|  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Multidimensional Model Assemblies Management](multidimensional-models/multidimensional-model-assemblies-management.md)  
  
  

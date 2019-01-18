---
title: "Integration Services User Interface | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Integration Services, SSIS Designer"
  - "tools [Integration Services], SSIS Designer"
  - "SSIS Designer"
  - "SSIS, SSIS Designer"
  - "Integration Services, SSIS Designer"
ms.assetid: d2c48cff-46f4-4c70-b1f3-c88f9b8757f3
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Integration Services User Interface
  In addition to the design surfaces on the [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer tabs, the user interface provides access to the following windows and dialog boxes for adding features to packages and configuring the properties of package objects:  
  
-   The dialog boxes and windows that you use to add functionality such as logging and configurations to packages.  
  
-   The custom editors for configuring the properties of package objects. Almost every type of container, task, and data flow component has its own custom editor.  
  
-   The **Advanced Editor** dialog box, a generic editor that provides more detailed configuration options for many data flow components.  
  
 [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] also provides windows and dialog boxes for configuring the environment and working with packages.  
  
## Dialog Boxes and Windows  
 After you open a package or create a new package in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, the following dialog boxes and windows are available.  
  
 This table lists the dialog boxes that are available from the **SSIS** menu and the design surfaces of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
|Dialog box|Purpose|Access|  
|----------------|-------------|------------|  
|**Getting Started**|Access samples, tutorials, and videos.|On the design surface of the **Control Flow** tab or the **Data Flow** tab, right-click and then click **Getting Started**.<br /><br /> To automatically display the **Getting Started** window when you create a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, select **Always show in new project** at the bottom of the window.|  
|**Configure SSIS Logs**|Configure logging for a package and its tasks by adding logs and setting logging details.|On the **SSIS** menu, click **Logging**.<br /><br /> -or-<br /><br /> Right-click anywhere on the design surface of the **Control Flow** tab, and then click **Logging**.|  
|**Package Configuration Organizer**|Add and edit package configurations. You run the Package Configuration Wizard from this dialog box.|On the **SSIS** menu, click **Package Configurations**.<br /><br /> -or-<br /><br /> Right-click anywhere on the design surface of the **Control Flow** tab, and then click **Package Configurations**.|  
|**Digital Signing**|Sign a package or remove the signature from the package.|On the **SSIS** menu, click **Digital Signing**.<br /><br /> -or-<br /><br /> Right-click anywhere on the design surface of the **Control Flow** tab, and then click **Digital Signing**.|  
|**Set Breakpoints**|Enable breakpoints on tasks and set breakpoint properties.|On the design surface of the **Control Flow** tab, right-click a task or container, and then click **Edit Breakpoints**. To set a breakpoint on the package, right-click anywhere on the design surface of the **Control Flow** tab, and then click **Edit Breakpoints**.|  
  
 The **Getting Started** window provides links to samples, tutorials, and videos. To add links to additional content, modify the SamplesSites.xml file that is included with the current release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. It is recommended that you not modify the \<GettingStartedSamples> element value that specifies the RSS feed URL. The file is located in the *\<drive>*:\Program Files\Microsoft SQL Server\110\DTS\Binn folder. On a 64-bit computer, the file is located in the *\<drive>*:\Program Files(x86)\Microsoft SQL Server\110\DTS\Binn folder  
  
 If the SamplesSites.xml file does become corrupted, replace the xml in the file with the following default xml.  
  
 `<?xml version="1.0" ?>`  
  
 `- <SamplesSites>`  
  
 `<GettingStartedSamples>https://go.microsoft.com/fwlink/?LinkID=203147</GettingStartedSamples>`  
  
 `- <ToolboxSamples>`  
  
 `<Site>https://go.microsoft.com/fwlink/?LinkID=203286&query=SSIS%20{0}</Site>`  
  
 `</ToolboxSamples>`  
  
 `</SamplesSites>`  
  
 This table lists the windows that are available from the **SSIS** and **View** menus and the design surfaces of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
|Window|Purpose|Access|  
|------------|-------------|------------|  
|**Variables**|Add and manage custom variables.|On the **SSIS** menu, click **Variables**.<br /><br /> -or-<br /><br /> Right-click anywhere in the design surface of the **Control Flow** and **Data Flow** tabs, and then click **Variables**.<br /><br /> -or-<br /><br /> On the **View** menu, point to **Other Windows**, and then click **Variables**.|  
|**Log Events**|View log entries at run time.|On the **SSIS** menu, click **Log Events**.<br /><br /> -or-<br /><br /> Right-click anywhere in the design surface of the **Control Flow** and **Data Flow** tabs, and then click **Log Events**.<br /><br /> -or-<br /><br /> On the **View** menu, point to **Other Windows**, and then click **Log Events**.|  
  
## Custom Editors  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides a custom dialog box for most containers, tasks, sources, transformations, and destinations.  
  
 The following table describes how to access custom dialog boxes.  
  
|Editor type|Access|  
|-----------------|------------|  
|Container. For more information, see [Integration Services Containers](control-flow/integration-services-containers.md).|On the design surface of the **Control Flow** tab, double-click the container.|  
|Task. For more information, see [Integration Services Tasks](control-flow/integration-services-tasks.md).|On the design surface of the **Control Flow** tab, double-click the task.|  
|Source.|On the design surface of the **Data Flow** tab, double-click the source.|  
|Transformation. For more information, see [Integration Services Transformations](data-flow/transformations/integration-services-transformations.md).|On the design surface of the **Data Flow** tab, double-click the transformation.|  
|Destination.|On the design surface of the **Data Flow** tab, double-click the destination.|  
  
## Advanced Editor  
 The **Advanced Editor** dialog box is a user interface for configuring data flow components. It reflects the properties of the component using a generic layout. The **Advanced Editor** dialog box is not available to [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] transformations that have multiple inputs.  
  
 To open this editor, click **ShowAdvanced Editor** in the **Properties** window or right-click a data flow component, and then click **ShowAdvanced Editor**.  
  
 If you create a custom source, transformation, or destination but do not want to write a custom user interface, you can use the **Advanced Editor** instead.  
  
## SQL Server Data Tools Features  
 [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] provides windows, dialog boxes, and menu options for working with [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages.  
  
 The following is a summary of the available windows and menus:  
  
-   The **Solution Explorer** window lists projects, including the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project in which you develop [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages, and project files.  
  
     To sort by name the packages contained in a project, right-click the **SSIS Packages** node and then click **Sort by name**.  
  
-   The **Toolbox** window lists the control flow and data flow items for building control flows and data flows.  
  
-   The **Properties** window lists object properties.  
  
-   The **Format** menu provides options for sizing and aligning controls in a package.  
  
-   The **Edit** menu provides copy and paste functionality for copying objects on the design surfaces.  
  
-   The **View** menu provides options for modifying the graphical representation of objects in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer  
  
 For more information about additional windows and menus, see the Visual Studio documentation.  
  
## Related Tasks  
 For information about how to create packages in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], see [Create Packages in SQL Server Data Tools](create-packages-in-sql-server-data-tools.md)  
  
## See Also  
 [SSIS Designer](ssis-designer.md)  
  
  

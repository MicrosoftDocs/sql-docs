---
title: "Tabular Model Designer (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.ASVS.BIDTOOLSET.TOPLEVSEMMODUIENTRY.F1"
ms.assetid: 45735c57-2a95-4e45-8994-7242df6c9c5f
author: minewiskan
ms.author: owend
manager: craigg
---
# Tabular Model Designer (SSAS Tabular)
  The tabular model designer is part of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], integrated with Microsoft [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] 2010 or later, with additional project type templates specifically for developing professional tabular model solutions.  
  
 Sections in this topic:  
  
-   [Benefits](#bkmk_benefits)  
  
-   [Project Templates](#bkmk_proj_temp)  
  
-   [Windows and Menus](#bkmk_wind_men)  
  
-   [Visual Studio Integration](#bkmk_vsint)  
  
##  <a name="bkmk_benefits"></a> Benefits  
 When you install [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], new project templates for creating tabular models are added to the available project types. After creating a new tabular model project by using one of the templates, you can begin model authoring by using the tabular model designer tools and wizards.  
  
 In addition to new templates and tools for authoring professional multidimensional and tabular model solutions, the [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] environment provides debugging and project lifecycle capabilities that ensure you create the most powerful BI solutions for your organization. For more information about [!INCLUDE[vsprvs](../includes/vsprvs-md.md)], see [Getting Started with Visual Studio](https://go.microsoft.com/fwlink/?LinkId=206389).  
  
##  <a name="bkmk_proj_temp"></a> Project Templates  
 When you install [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the following tabular model project templates are added to the Business Intelligence project types:  
  
 **Analysis Services Tabular Project**  
 This template can be used to create a new, blank tabular model project.  
  
 **Import from Server (Tabular)**  
 This template can be used to create a new tabular model project by extracting the metadata from an existing tabular model in Analysis Services.  
  
 **Import from PowerPivot**  
 This template is used for creating a new tabular model project by extracting the metadata and data from a [!INCLUDE[ssGeminiClient](../includes/ssgeminiclient-md.md)] file.  
  
> [!NOTE]  
>  Tabular model projects require an Analysis Services server instance in tabular mode be running locally or on the network.  
  
##  <a name="bkmk_wind_men"></a> Windows and Menus  
 The [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] tabular model authoring environment includes the following:  
  
### Designer Window  
 The designer window is used to author tabular models by providing a visual representation of the model. When you open the Model.bim file, the model opens in the designer window. You can author a model in the designer window by using two different view modes:  
  
 **Data View**  
 The data view displays tables in a tabular, grid format. You can also define measures by using the measure grid, which can be shown for each table in Data View only.  
  
 **Diagram View**  
 The diagram view displays tables, with relationships between them, in a graphical format. Columns, measures, hierarchies, and KPIs can be filtered, and you can choose to view the model by using a defined perspective.  
  
 Most model authoring tasks can be performed in either view.  
  
### Solution Explorer  
 The Solution Explorer window presents the active solution as a logical container for a tabular model project and its associated items. The model project (.smproj) contains only a References object (empty) and the Model.bim file. You can open project items for modification and perform other management tasks directly from this view.  
  
 Tabular model solutions typically contain only one project; however, a solution can contain other projects too, for example, Integration Services or Reporting services project. You can add any number of files provided they are not of the same type as tabular model project files and their Build Action property is set to None, or Copy to Output property is set to Do Not Copy.  
  
 To view Solution Explorer, click the **View** menu, and then click **Solution Explorer**.  
  
### Properties Window  
 The Properties window lists the properties of the selected object. The following objects have properties that can be viewed and edited in the Properties window:  
  
-   Model.bim  
  
-   Table  
  
-   Column  
  
-   Measure  
  
 Project Properties display only the project name and project folder in the Properties window. Projects also have additional deployment Options and deployment server settings that you can set using a modal properties dialog box. To view these properties, in **Solution Explorer**, right click the project, and then click **Properties**.  
  
 Fields in the Properties window have embedded controls that open when you click them. The type of edit control depends on the particular property. Controls include edit boxes, dropdown lists, and links to custom dialog boxes. Properties that are shown as dimmed are read-only.  
  
 To view the **Properties** window, click the **View** menu, and then click **Properties Window**.  
  
### Error List  
 The Error List window contains messages about the model state:  
  
-   Notifications about security best practices.  
  
-   Requirements for data processing.  
  
-   Semantic error information for calculated columns, measures, and row filters for roles. For calculated columns, you can double-click the error message to navigate to the source of the error.  
  
-   DirectQuery validation errors.  
  
 By default, the **Error List** does not appear unless an error is returned. You can, however, view the **Error List** window at any time. To view the **Error List** window, click the **View** menu, and then click **Error List**.  
  
### Output  
 Build and deployment information is displayed in the **Output** Window (in addition to the modal progress dialog). To view the **Output** window, click the **View** menu, and then click Output.  
  
### Menu Items  
 When you install [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], additional menu items specifically for authoring tabular models are added to the Visual Studio menu bar. The **Model** menu can be used to launch the Data Import Wizard, view existing connections, process workspace data, and browse the model workspace in [!INCLUDE[msCoName](../includes/msconame-md.md)] Excel. The **Table** menu is used to create and manage relationships between tables, create and manage measures, specify data table settings, specify calculation options, and specify other table properties. With the **Column** menu, you can add and delete columns in a table, hide and unhide columns, and specify other column properties such as data types and filters. You can build and deploy tabular model solutions on the **Build** menu. Copy/Paste functions are included on the **Edit** menu.  
  
 In addition to these menu items, additional settings are added to the Analysis Services options on the Tools menu items.  
  
### Toolbar  
 The Analysis Services toolbar provides quick and easy access to the most frequently used model authoring commands.  
  
##  <a name="bkmk_vsint"></a> Visual Studio Integration  
 **Source Control**  
 Analysis Services projects integrate with the selected source control plug-in. If you have configured Visual Studio to use source control, you can use check in/check out from Solution Explorer. To configure to use Team Foundation Server, see [Configure Visual Studio with Team Foundation Version Control](https://msdn.microsoft.com/library/ms253064.aspx). Many third-party source control plug-ins are supported as well.  
  
 **Fonts**  
 Tabular models use the [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] environment font to control the fonts in the display. It can be necessary to change this font if the default [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] font does not have all of the Unicode characters you need for your language. To change fonts, click the **Tools** menu, then click **Options**, and then click **Fonts and Colors**.  
  
 **Keyboard Shortcuts**  
 The Analysis Services keyboard shortcuts can be configured/remapped through the Tools->Options->Keyboard dialog. Some global [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] shortcuts, such as build, save, debug, new project, etc. are supported in the tabular model designer context. Other tabular model designer specific shortcuts are in the Analysis Services context.  
  
## See Also  
 [Tabular Model Projects &#40;SSAS Tabular&#41;](tabular-models/tabular-model-projects-ssas-tabular.md)   
 [Properties &#40;SSAS Tabular&#41;](tabular-models/properties-ssas-tabular.md)  
  
  

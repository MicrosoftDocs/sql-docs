---
title: "Toolbar (Browser Tab, Dimension Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: d0abb2a7-e981-4b0a-a442-80c819aca2ae
author: minewiskan
ms.author: owend
manager: craigg
---
# Toolbar (Browser Tab, Dimension Designer) (Analysis Services - Multidimensional Data)
  Use the **Toolbar** pane to perform common operations on the **Browser** tab of **Dimension Designer**.  
  
## Options  
 **Process**  
 Click to open the **Process** dialog box and process the selected dimension.  
  
 **Reconnect**  
 Click to reconnect the **Browser** tab to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance and database that contains the dimension, if the session for the **Browser** tab is disconnected due to connection loss or timeout.  
  
 **Refresh**  
 Click to reload the **Browser** tab with data and metadata for the dimension.  
  
 **Member Properties**  
 Click to display and select member properties to display in the **Level and Members** pane. Selecting **(Show All)** displays all listed member properties.  
  
 Each member property selected in **Member Properties** is displayed in **Level and Members** as a separate column, to allow editing of member property values in writeback mode.  
  
 **Filter Members**  
 Click to display the **Filter Members** dialog box and filter members displayed in **Level and Members** for the selected hierarchy.  
  
 **Writeback**  
 Select to set the **Level and Members** pane in dimension writeback mode.  
  
 **Decrease Indent**  
 Click to make the selected member in **Level and Members** a sibling of its parent member.  
  
> [!NOTE]  
>  This option is enabled only if writeback mode is enabled.  
  
 **Increase Indent**  
 Click to make the selected member in **Level and Members** a child of the sibling immediately preceding the selected member.  
  
> [!NOTE]  
>  This option is enabled only if writeback mode is enabled.  
  
 **Hierarchy**  
 Select the hierarchy from which to display members in the **Level and Members** pane.  
  
 **Language**  
 Select the language used to display members in the **Level and Members** pane.  
  
 If a language is selected for which a translation has not been defined, the default language is used to display members in the **Level and Members** pane. However, writeback mode is disabled if a language is selected for which a translation has not been defined.  
  
  

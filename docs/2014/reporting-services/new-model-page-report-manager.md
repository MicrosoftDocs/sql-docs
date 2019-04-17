---
title: "New Model Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 27d5bf66-b0e7-489e-a830-ffe2ec8e5350
author: markingmyname
ms.author: maghan
manager: kfile
---
# New Model Page (Report Manager)
  Use this page to generate a default report model from a shared data source. You can only generate report models from [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] multidimensional data sources, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] relational data sources, and Oracle relational data sources.  
  
 Models that you generate in Report Manager are based on the schema of the shared data source. Entities, folders, and fields are created for all tables and columns in the data source. You cannot exclude items, nor can you set options that determine how the model is generated. If you want to customize or refine a model, you must use Model Designer instead.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the New Model page  
  
1.  Open Report Manager, and locate the shared data source from which you want to generate a model.  
  
2.  Hover over the data source, and click the drop-down arrow.  
  
3.  In the drop-down menu, perform one of the following steps:  
  
    -   Click **Generate Report Model** to open the New Model page.  
  
    -   Click **Manage** to open the General properties page for the report. Then click **Generate Model** to open the New Model page.  
  
## Options  
 **Name**  
 Specifies the name of the model. A name must contain at least one alphanumeric character. It can also include spaces and some symbols. Do not use the following characters when specifying a name:  
  
 ; ? : \@ & = + , $ / * \< > | " /  
  
 **Description**  
 Shows a description of the model. Users who view this item through Report Manager see this description when browsing the folder hierarchy.  
  
 **Change Location**  
 Shows the folder location for the new model. You can click the **Change Location** button to select a different location.  
  
  

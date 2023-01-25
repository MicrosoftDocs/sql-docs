---
description: "Data Mining Query Transformation"
title: "Data Mining Query Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dataminingquerytrans.f1"
  - "sql13.dts.designer.dmquerytransformation.miningmodel.f1"
  - "sql13.dts.designer.dmquerytransformation.query.f1"
helpviewer_keywords: 
  - "Data Mining Query transformation"
  - "prediction queries [Integration Services]"
ms.assetid: 7960133b-a3e1-48af-ba43-55ed78c38e71
author: chugugrace
ms.author: chugu
---
# Data Mining Query Transformation

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]

> [!IMPORTANT]
> Data mining was deprecated in SQL Server 2017 Analysis Services and now discontinued in SQL Server 2022 Analysis Services. Documentation is not updated for deprecated and discontinued features. To learn more, see [Analysis Services backward compatibility](/analysis-services/analysis-services-backward-compatibility).

  The Data Mining Query transformation performs prediction queries against data mining models. This transformation contains a query builder for creating Data Mining Extensions (DMX) queries. The query builder lets you create custom statements for evaluating the transformation input data against an existing mining model using the DMX language. For more information, see [Data Mining Extensions &#40;DMX&#41; Reference](../../../dmx/data-mining-extensions-dmx-reference.md).  
  
 One transformation can execute multiple prediction queries if the models are built on the same data mining structure. For more information, see [Data Mining Query Tools](/analysis-services/data-mining/data-mining-query-tools).  
  
## Configuration of the Data Mining Query Transformation  
 The Data Mining Query transformation uses an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] connection manager to connect to the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] project or the instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] that contains the mining structure and mining models. For more information, see [Analysis Services Connection Manager](../../../integration-services/connection-manager/analysis-services-connection-manager.md).  
  
 This transformation has one input and one output. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../set-the-properties-of-a-data-flow-component.md)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Data Mining Query Transformation Editor (Mining Model Tab)
  Use the **Mining Model** tab of the **Data Mining Query Transformation Editor** dialog box to select the data mining structure and its mining models.  
  
### Options  
 **Connection**  
 Select an existing Analysis Services connection by using the list box, or create a new connection by using the **New** button described as follows.  
  
 **New**  
 Create a new connection by using the **Add Analysis Services Connection Manager** dialog box.  
  
 **Mining structure**  
 Select from the list of available mining model structures.  
  
 **Mining models**  
 View the list of mining models associated with the selected data mining structure.  
  
## Data Mining Query Transformation Editor (Query Tab)
  Use the **Query** tab of the **Data Mining Query Transformation Editor** dialog box to create a prediction query.  
  
### Options  
 **Data mining query**  
 Type a Data Mining Extensions (DMX) query directly into the text box.  
  
 **Build New Query**  
 Click **Build New Query** to create a Data Mining Extensions (DMX) query by using the graphical query builder.  

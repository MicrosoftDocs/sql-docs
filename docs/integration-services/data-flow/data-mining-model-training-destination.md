---
title: "Data Mining Model Training Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dataminingmodeltrainingdest.f1"
  - "sql13.dts.designer.dmmtrainingtransformation.connection.f1"
  - "sql13.dts.designer.dmmtrainingtransformation.columns.f1"
helpviewer_keywords: 
  - "destinations [Integration Services], Data Mining Model Training"
  - "Data Mining Model Training destination"
  - "mining models [Analysis Services], training"
  - "training mining models"
ms.assetid: 6bc8cbe2-46af-4f7b-93d6-86779313c9d7
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Mining Model Training Destination

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Data Mining Model Training destination trains data mining models by passing the data that the destination receives through the data mining model algorithms. Multiple data mining models can be trained by one destination if the models are built on the same data mining structure. For more information, see [Mining Structure Columns](../../analysis-services/data-mining/mining-structure-columns.md) and [Mining Model Columns](../../analysis-services/data-mining/mining-model-columns.md).  
  
## Configuration of the Data Mining Model Training Destination  
 If a case level column of the target structure and the models built on the structure has the content type KEY TIME or KEY SEQUENCE, the input data must be sorted on that column. For example, models built using the Microsoft Time Series algorithm use the content type KEY TIME. If input data is not sorted, the processing of the model may fail. If the data requires sorting, you can use a Sort transformation earlier in the data flow to sort the data. This requirement does not apply to columns with the KEY content type. For more information, see [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md) and [Sort Transformation](../../integration-services/data-flow/transformations/sort-transformation.md).  
  
> [!NOTE]  
>  The input to the Data Mining Model training destination must be sorted. To sort the data, you can include a Sort destination upstream from the Data Mining Model Training destination in the data flow. For more information, see [Sort Transformation](../../integration-services/data-flow/transformations/sort-transformation.md).  
  
 This destination has one input and no output.  
  
 The Data Mining Model Training destination uses an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager to connect to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that contains the mining structure and mining models that the destination trains. For more information, see [Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md).  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Data Mining Model Training Destination Custom Properties](../../integration-services/data-flow/data-mining-model-training-destination-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Data Mining Model Training Editor (Connection Tab)
  Use the **Connection** page of the **Data Mining Model Training Editor** dialog box to select a mining model to train.  
  
### Options  
 **Connection manager**  
 Select from the list of existing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connections, or create a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection by using the **New** button described as follows.  
  
 **New**  
 Create a new connection by using the **Add Analysis Services Connection Manager** dialog box.  
  
 **Mining structure**  
 Select from the list of available mining structures, or create a new structure by clicking **New**.  
  
 **New**  
 Create a new mining structure and mining model by using the **Data Mining Wizard**.  
  
 **Mining models**  
 View the list of mining models associated with the selected mining structure.  
  
## Data Mining Model Training Editor (Columns Tab)
  Use the **Columns** page of the **Data Mining Model Training Editor** dialog box to map input columns to columns in the mining structure.  
  
## Options  
 **Available Input Columns**  
 View the list of available input columns. Drag input columns to map them to mining structure columns.  
  
 **Mining Structure Columns**  
 View the list of mining structure columns. Drag mining structure columns to map them to available input columns.  
  
 **Input Column**  
 View input columns selected from the table above. To change or remove a mapping selection, use the list of **Available Input Columns**.  
  
 **Mining Structure Columns**  
 View each available destination column, whether mapped or not.  
  

---
title: "Percentage Sampling Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.percentagesamplingtrans.f1"
  - "sql13.dts.designer.percentagesamplingtransformation.f1"
helpviewer_keywords: 
  - "testing mining models"
  - "sampling seeds [Integration Services]"
  - "data mining [Analysis Services], sample data sets"
  - "Percentage Sampling transformation"
  - "sample data sets [Integration Services]"
  - "datasets [Integration Services], sample"
  - "training mining models"
ms.assetid: 59767e52-f732-4b3f-8602-be50d0a64ef2
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Percentage Sampling Transformation
  The Percentage Sampling transformation creates a sample data set by selecting a percentage of the transformation input rows. The sample data set is a random selection of rows from the transformation input, to make the resultant sample representative of the input.  
  
> [!NOTE]  
>  In addition to the specified percentage, the Percentage Sampling transformation uses an algorithm to determine whether a row should be included in the sample output. This means that the number of rows in the sample output may not exactly reflect the specified percentage. For example, specifying 10 percent for an input data set that has 25,000 rows may not generate a sample with 2,500 rows; the sample may have a few more or a few less rows.  
  
 The Percentage Sampling transformation is especially useful for data mining. By using this transformation, you can randomly divide a data set into two data sets: one for training the data mining model, and one for testing the model.  
  
 The Percentage Sampling transformation is also useful for creating sample data sets for package development. By applying the Percentage Sampling transformation to a data flow, you can uniformly reduce the size of the data set while preserving its data characteristics. The test package can then run more quickly because it uses a small, but representative, data set.  
  
## Configuration the Percentage Sampling Transformation  
 You can specify a sampling seed to modify the behavior of the random number generator that the transformation uses to select rows. If the same sampling seed is used, the transformation always creates the same sample output. If no seed is specified, the transformation uses the tick count of the operating system to create the random number. Therefore, you might choose to use a standard seed when you want to verify the transformation results during the development and testing of a package, and then change to use a random seed when the package is moved into production.  
  
 This transformation is similar to the Row Sampling transformation, which creates a sample data set by selecting a specified number of the input rows. For more information, see [Row Sampling Transformation](../../../integration-services/data-flow/transformations/row-sampling-transformation.md).  
  
 The Percentage Sampling transformation includes the **SamplingValue** custom property. This property can be updated by a property expression when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../../integration-services/expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
 The transformation has one input and two outputs. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Percentage Sampling Transformation Editor
  Use the **Percentage Sampling Transformation Editor** dialog box to split part of an input into a sample using a specified percentage of rows. This transformation divides the input into two separate outputs.  
  
### Options  
 **Percentage of rows**  
 Specify the percentage of rows in the input to use as a sample.  
  
 The value of this property can be specified by using a property expression.  
  
 **Sample output name**  
 Provide a unique name for the output that will include the sampled rows. The name provided will be displayed within the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer.  
  
 **Unselected output name**  
 Provide a unique name for the output that will contain the rows excluded from the sampling. The name provided will be displayed within the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer.  
  
 **Use the following random seed**  
 Specify the sampling seed for the random number generator that the transformation uses to create a sample. This is only recommended for development and testing. The transformation uses the Microsoft Windows tick count if a random seed is not specified.  
  
  

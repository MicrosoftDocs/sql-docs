---
title: "Row Sampling Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.rowsamplingtrans.f1"
helpviewer_keywords: 
  - "sampling seeds [Integration Services]"
  - "random seeds"
  - "random sampling"
  - "sample data sets [Integration Services]"
  - "Row Sampling transformation"
  - "packages [Integration Services], samples"
  - "datasets [Integration Services], sample"
ms.assetid: b6caafd3-30b2-4368-82af-a44611d4cd39
author: janinezhang
ms.author: janinez
manager: craigg
---
# Row Sampling Transformation
  The Row Sampling transformation is used to obtain a randomly selected subset of an input dataset. You can specify the exact size of the output sample, and specify a seed for the random number generator.  
  
 There are many applications for random sampling. For example, a company that wanted to randomly select 50 employees to receive prizes in a lottery could use the Row Sampling transformation on the employee database to generate the exact number of winners.  
  
 The Row Sampling transformation is also useful during package development for creating a small but representative dataset. You can test package execution and data transformation with richly representative data, but more quickly because a random sample is used instead of the full dataset. Because the sample dataset used by the test package is always the same size, using the sample subset also makes it easier to identify performance problems in the package.  
  
 This transformation is similar to the Percentage Sampling transformation, which creates a sample dataset by selecting a percentage of the input rows. See [Percentage Sampling Transformation](percentage-sampling-transformation.md).  
  
## Configuring the Row Sampling Transformation  
 The Row Sampling transformation creates a sample dataset by selecting a specified number of the transformation input rows. Because the selection of rows from the transformation input is random, the resultant sample is representative of the input. You can also specify the seed that is used by the random number generator, to affect how the transformation selects rows.  
  
 Using the same random seed on the same transformation input always creates the same sample output. If no seed is specified, the transformation uses the tick count of the operating system to create the random number. Therefore, you could use the same seed during testing, to verify the transformation results during the development and testing of the package, and then change to a random seed when the package is moved into production.  
  
 The Row Sampling transformation includes the `SamplingValue` custom property. This property can be updated by a property expression when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](transformation-custom-properties.md).  
  
 This transformation has one input and two outputs. It has no error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Row Sampling Transformation Editor** dialog box, see [Row Sampling Transformation Editor &#40;Sampling Page&#41;](../../row-sampling-transformation-editor-sampling-page.md).  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../../common-properties.md)  
  
-   [Transformation Custom Properties](transformation-custom-properties.md)  
  
## Related Tasks  
 [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md)  
  
  

---
title: "Copy Column Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.copycolumntrans.f1"
  - "sql13.dts.designer.copymaptransformation.f1"
helpviewer_keywords: 
  - "columns [Integration Services], copying"
  - "copying columns"
  - "Copy Column transformation"
ms.assetid: 1c72a313-9026-46bc-a57f-c6b3f47346f8
author: janinezhang
ms.author: janinez
manager: craigg
---
# Copy Column Transformation

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Copy Column transformation creates new columns by copying input columns and adding the new columns to the transformation output. Later in the data flow, different transformations can be applied to the column copies. For example, you can use the Copy Column transformation to create a copy of a column and then convert the copied data to uppercase characters by using the Character Map transformation, or apply aggregations to the new column by using the Aggregate transformation.  
  
## Configuration of the Copy Column Transformation  
 You configure the Copy Column transformation by specifying the input columns to copy. You can create multiple copies of a column, or create copies of multiple columns in one operation.  
  
 This transformation has one input and one output. It does not support an error output.  
  
 You can set properties through the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Copy Column Transformation Editor
  Use the **Copy Column Transformation Editor** dialog box to select columns to copy and to assign names for the new output columns.  
  
> [!NOTE]  
>  When you are simply copying all of your source data to a destination, it may not be necessary to use the Copy Column transformation. In some scenarios, you can connect a source directly to a destination, when no data transformation is required. In these circumstances it is often preferable to use the SQL Server Import and Export Wizard to create your package for you. Later you can enhance and reconfigure the package as needed. For more information, see [SQL Server Import and Export Wizard](~/integration-services/import-export-data/welcome-to-sql-server-import-and-export-wizard.md).  
  
### Options  
 **Available Input Columns**  
 Select columns to copy by using the check boxes. Your selections add input columns to the table below.  
  
 **Input Column**  
 Select columns to copy from the list of available input columns. Your selections are reflected in the check box selections in the **Available Input Columns** table.  
  
 **Output Alias**  
 Type an alias for each new output column. The default is **Copy of**, followed by the name of the input column; however, you can choose any unique, descriptive name.  
  
## See Also  
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  

---
title: "Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.scriptcomponentdetails.f1"
helpviewer_keywords: 
  - "Script transformation"
  - "scripts [Integration Services], transformations"
  - "Script component [Integration Services], about Script component"
  - "Script component [Integration Services]"
ms.assetid: 131c2d0c-2e33-4785-94af-ada5c049821e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Script Component
  The Script component hosts script and enables a package to include and run custom script code. You can use the Script component in packages for the following purposes:  
  
-   Apply multiple transformations to data instead of using multiple transformations in the data flow. For example, a script can add the values in two columns and then calculate the average of the sum.  
  
-   Access business rules in an existing .NET assembly. For example, a script can apply a business rule that specifies the range of values that are valid in an `Income` column.  
  
-   Use custom formulas and functions in addition to the functions and operators that the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] expression grammar provides. For example, validate credit card numbers that use the LUHN formula.  
  
-   Validate column data and skip records that contain invalid data. For example, a script can assess the reasonableness of a postage amount and skip records with extremely high or low amounts.  
  
 The Script component provides an easy and quick way to include custom functions in a data flow. However, if you plan to reuse the script code in multiple packages, you should consider programming a custom component instead of using the Script component. For more information, see [Developing a Custom Data Flow Component](../../extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md).  
  
> [!NOTE]  
>  If the Script component contains a script that tries to read the value of a column that is NULL, the Script component fails when you run the package. We recommend that your script use the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptBuffer.IsNull%2A> method to determine whether the column is NULL before trying to read the column value.  
  
 The Script component can be used as a source, a transformation, or a destination. This component supports one input and multiple outputs. Depending on how the component is used, it supports either an input or outputs or both. The script is invoked by every row in the input or output.  
  
-   If used as a source, the Script component supports multiple outputs.  
  
-   If used as a transformation, the Script component supports one input and multiple outputs.  
  
-   If used as a destination, the Script component supports one input.  
  
 The Script component does not support error outputs.  
  
 After you decide that the Script component is the appropriate choice for your package, you have to configure the inputs and outputs, develop the script that the component uses, and configure the component itself.  
  
## Understanding the Script Component Modes  
 In the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer, the Script component has two modes: metadata-design mode and code-design mode. In metadata-design mode, you can add and modify the Script component inputs and outputs, but you cannot write code. After all the inputs and outputs are configured, you switch to code-design mode to write the script. The Script component automatically generates base code from the metadata of the inputs and outputs. If you change the metadata after the Script component generates the base code, your code may no longer compile because the updated base code may be incompatible with your code.  
  
## Writing the Script that the Component Uses  
 The Script component uses [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] Tools for Applications (VSTA) as the environment in which you write the scripts. You access VSTA from the **Script Transformation Editor**. For more information, see [Script Transformation Editor &#40;Script Page&#41;](../../script-transformation-editor-script-page.md).  
  
 The Script component provides a VSTA project that includes an auto-generated class, named ScriptMain, that represents the component metadata. For example, if the Script component is used as a transformation that has three outputs, ScriptMain includes a method for each output. ScriptMain is the entry point to the script.  
  
 VSTA includes all the standard features of the [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] environment, such as the color-coded [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] editor, IntelliSense, and Object Browser. The script that the Script component uses is stored in the package definition. When you are designing the package, the script code is temporarily written to a project file.  
  
 VSTA supports the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual C# and [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Basic programming languages.  
  
 For information about how to program the Script component, see [Extending the Data Flow with the Script Component](script-component.md). For more specific information about how to configure the Script component as a source, transformation, or destination, see [Developing Specific Types of Script Components](../../extending-packages-scripting-data-flow-script-component-types/developing-specific-types-of-script-components.md). For additional examples such as an ODBC destination that demonstrate the use of the Script component, see [Additional Script Component Examples](../../extending-packages-scripting-data-flow-script-component-examples/additional-script-component-examples.md).  
  
> [!NOTE]  
>  Unlike earlier versions where you could indicate whether the scripts were precompiled, all scripts are precompiled in [!INCLUDE[ssISversion10](../../../includes/ssisversion10-md.md)] and later versions. When a script is precompiled, the language engine is not loaded at run time and the package runs more quickly. However, precompiled binary files consume significant disk space.  
  
## Configuring the Script Component  
 You can configure the Script component in the following ways:  
  
-   Select the input columns to reference.  
  
    > [!NOTE]  
    >  You can configure only one input when you use the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer.  
  
-   Provide the script that the component runs.  
  
-   Specify the script language.  
  
-   Provide comma-separated lists of read-only and read/write variables.  
  
-   Add more outputs, and add output columns to which the script assigns.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
### Configuring the Script Component in the Designer  
 For more information about the properties that you can set in the **Script Transformation Editor** dialog box, click one of the following topics:  
  
-   [Script Transformation Editor &#40;Input Columns Page&#41;](../../script-transformation-editor-input-columns-page.md)  
  
-   [Script Transformation Editor &#40;Inputs and Outputs Page&#41;](../../script-transformation-editor-inputs-and-outputs-page.md)  
  
-   [Script Transformation Editor &#40;Script Page&#41;](../../script-transformation-editor-script-page.md)  
  
-   [Script Transformation Editor &#40;Connection Managers Page&#41;](../../script-transformation-editor-connection-managers-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md)  
  
### Configuring the Script Component Programmatically  
 For more information about the properties that you can set in the **Properties** window or programmatically, click one of the following topics:  
  
-   [Common Properties](../../common-properties.md)  
  
-   [Transformation Custom Properties](transformation-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md)  
  
## Related Content  
 [Integration Services Transformations](integration-services-transformations.md)  
  
 [Extending the Data Flow with the Script Component](script-component.md)  
  
  

---
description: "Script Component"
title: "Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.scriptcomponentdetails.f1"
  - "sql13.dts.designer.scriptcomponent.f1"
  - "sql13.dts.designer.scriptcomponent.connections.f1"
  - "sql13.dts.designer.scriptcomponent.inputcolumn.f1"
  - "sql13.dts.designer.scriptcomponent.columnproperties.f1"
  - "sql13.dts.designer.scriptcomponent.script.f1"
helpviewer_keywords: 
  - "Script transformation"
  - "scripts [Integration Services], transformations"
  - "Script component [Integration Services], about Script component"
  - "Script component [Integration Services]"
ms.assetid: 131c2d0c-2e33-4785-94af-ada5c049821e
author: chugugrace
ms.author: chugu
---
# Script Component

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  The Script component hosts script and enables a package to include and run custom script code. You can use the Script component in packages for the following purposes:  
  
-   Apply multiple transformations to data instead of using multiple transformations in the data flow. For example, a script can add the values in two columns and then calculate the average of the sum.  
  
-   Access business rules in an existing .NET assembly. For example, a script can apply a business rule that specifies the range of values that are valid in an **Income** column.  
  
-   Use custom formulas and functions in addition to the functions and operators that the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] expression grammar provides. For example, validate credit card numbers that use the LUHN formula.  
  
-   Validate column data and skip records that contain invalid data. For example, a script can assess the reasonableness of a postage amount and skip records with extremely high or low amounts.  
  
 The Script component provides an easy and quick way to include custom functions in a data flow. However, if you plan to reuse the script code in multiple packages, you should consider programming a custom component instead of using the Script component. For more information, see [Developing a Custom Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md).  
  
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
 The Script component uses [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] Tools for Applications (VSTA) as the environment in which you write the scripts. You access VSTA from the **Script Transformation Editor**. For more information, see [Script Transformation Editor &#40;Script Page&#41;]().  
  
 The Script component provides a VSTA project that includes an auto-generated class, named ScriptMain, that represents the component metadata. For example, if the Script component is used as a transformation that has three outputs, ScriptMain includes a method for each output. ScriptMain is the entry point to the script.  
  
 VSTA includes all the standard features of the [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] environment, such as the color-coded [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] editor, IntelliSense, and Object Browser. The script that the Script component uses is stored in the package definition. When you are designing the package, the script code is temporarily written to a project file.  
  
 VSTA supports the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual C# and [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Basic programming languages.  
  
 For information about how to program the Script component, see [Extending the Data Flow with the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md). For more specific information about how to configure the Script component as a source, transformation, or destination, see [Developing Specific Types of Script Components](../../../integration-services/extending-packages-scripting-data-flow-script-component-types/developing-specific-types-of-script-components.md). For additional examples such as an ODBC destination that demonstrate the use of the Script component, see [Additional Script Component Examples](../../../integration-services/extending-packages-scripting-data-flow-script-component-examples/additional-script-component-examples.md).  
  
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
 For more information about how to set these properties in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
### Configuring the Script Component Programmatically  
 For more information about the properties that you can set in the **Properties** window or programmatically, click one of the following topics:  
  
-   [Common Properties](../set-the-properties-of-a-data-flow-component.md)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## Select Script Component Type
  Use the **Select Script Component Type** dialog box to specify whether to create a Script Transformation that is preconfigured for use as a source, a transformation, or a destination.  
  
 To learn more about the Script component, see [Configuring the Script Component in the Script Component Editor](../../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md). To learn about programming the Script component, see [Extending the Data Flow with the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md).  
  
### Options  
 Your selection of **Source**, **Destination**, or **Transformation** affects the configuration of the Script Transformation and the pages of the Script Transformation Editor.  
  
## Script Transformation Editor (Connection Managers Page)
  Use the **Connection Managers** page of the **Script Transformation Editor** to specify any connections that will be used by the script.  
  
 To learn more about the Script component, see [Configuring the Script Component in the Script Component Editor](../../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md). To learn about programming the Script component, see [Extending the Data Flow with the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md).  
  
### Options  
 **Connection managers**  
 View the list of connections available for use by the script.  
  
 **Name**  
 Type a unique and descriptive name for the connection.  
  
 **Connection Manager**  
 Select from the list of available connection managers, or select **\<New connection>** to open the **Add SSIS Connection Manager** dialog box.  
  
 **Description**  
 Type a description for the connection.  
  
 **Add**  
 Add another connection to the **Connection managers** list.  
  
 **Remove**  
 Remove the selected connection from the **Connection managers** list.  
  
## Script Transformation Editor (Input Columns Page)
  Use the **Input Columns** page of the **Script Transformation Editor** dialog box to set properties on input columns.  
  
> [!NOTE]  
>  The **Input Columns** page is not displayed for Source components, which have outputs but no inputs.  
  
 To learn more about the Script component, see [Configuring the Script Component in the Script Component Editor](../../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md). To learn about programming the Script component, see [Extending the Data Flow with the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md).  
  
### Options  
 **Input name**  
 Select from the list of available inputs.  
  
 **Available Input Columns**  
 Using the check boxes, specify the columns that the script transformation will use.  
  
 **Input Column**  
 Select from the list of available input columns for each row. Your selections are reflected in the check box selections in the **Available Input Columns**table.  
  
 **Output Alias**  
 Type an alias for each output column. The default is the name of the input column; however, you can choose any unique, descriptive name.  
  
 **Usage Type**  
 Specify whether the Script Transformation will treat each column as **ReadOnly** or **ReadWrite**.  
  
## Script Transformation Editor (Inputs and Outputs Page)
  Use the **Inputs and Outputs** page of the **Script Transformation Editor** dialog box to add, remove, and configure inputs and outputs for the Script Transformation.  
  
> [!NOTE]  
>  Source components have outputs and no inputs, while destination components have inputs but no outputs. Transformations have both inputs and outputs.  
  
 To learn more about the Script component, see [Configuring the Script Component in the Script Component Editor](../../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md). To learn about programming the Script component, see [Extending the Data Flow with the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md).  
  
### Options  
 **Inputs and outputs**  
 Select an input or output on the left to view its properties in the table on the right. Properties available for editing vary according to the selection. Many of the properties displayed are read-only. For more information on the individual properties, see the following topics.  
  
 [Common Properties](../set-the-properties-of-a-data-flow-component.md)  
  
 [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 **Add Output**  
 Add an additional output to the list.  
  
 **Add Column**  
 Select a folder in which to place the new output column, and then add the column by clicking **Add Column**.  
  
 **Remove Output**  
 Select an output, and then remove it by clicking **Remove Output**.  
  
 **Remove Column**  
 Select a column, and then remove it by clicking **Remove Column**.  
  
## Script Transformation Editor (Script Page)
  Use the **Script** tab of the **Script Transformation Editor** dialog box to specify a script and related properties.  
  
 To learn more about the Script component, see [Configuring the Script Component in the Script Component Editor](../../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md). To learn about programming the Script component, see [Extending the Data Flow with the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md).  
  
### Options  
 **Properties**  
 View and modify the properties of the Script transformation. Many of the properties displayed are read-only. You can modify the following properties:  
  
|Value|Description|  
|-----------|-----------------|  
|**Description**|Describe the script transformation in terms of its purpose.|  
|**LocaleID**|Specify the locale to provide region-specific information for ordering, and for date and time conversion.|  
|**Name**|Type a descriptive name for the component.|  
|**ValidateExternalMetadata**|Indicate whether the Script transformation validates column metadata against external data sources at design time. A value of **false** delays validation until the time of execution.|  
|**ReadOnlyVariables**|Type a comma-separated list of variables for read-only access by the Script transformation.<br /><br /> Note: Variable names are case-sensitive.|  
|**ReadWriteVariables**|Type a comma-separated list of variables for read/write access by the Script transformation.<br /><br /> Note: Variable names are case-sensitive.|  
|**ScriptLanguage**|Select the script language to be used by the Script component.<br /><br /> To set the default script language for Script components and Script tasks, use the **Scripting language** option on the **General** page of the **Options** dialog box.|  
|**UserComponentTypeName**|Specifies the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponentHost> class and the **Microsoft.SqlServer.TxScript** assembly that support the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] infrastructure.|  
  
 **Edit Script**  
 Use [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] Tools for Applications (VSTA) to create or modify a script.  
  
## Related Content  
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
 [Extending the Data Flow with the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md)  
  

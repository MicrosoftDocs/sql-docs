---
title: "Creating a Synchronous Transformation with the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "synchronous outputs [Integration Services]"
  - "transformation components [Integration Services]"
  - "Script component [Integration Services], transformation components"
ms.assetid: aa1bee1a-ab06-44d8-9944-4bff03d73016
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Creating a Synchronous Transformation with the Script Component
  You use a transformation component in the data flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package to modify and analyze data as it passes from source to destination. A transformation with synchronous outputs processes each input row as it passes through the component. A transformation with asynchronous outputs waits until it has received all input rows to complete its processing. This topic discusses a synchronous transformation. For information about asynchronous transformations, see [Creating an Asynchronous Transformation with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-an-asynchronous-transformation-with-the-script-component.md). For more information about the difference between synchronous and asynchronous components, see [Understanding Synchronous and Asynchronous Transformations](../understanding-synchronous-and-asynchronous-transformations.md).  
  
 For an overview of the Script component, see [Extending the Data Flow with the Script Component](../extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md).  
  
 The Script component and the infrastructure code that it generates for you simplify significantly the process of developing a custom data flow component. However, to understand how the Script component works, you may find it useful to read the steps that you must follow in developing a custom data flow component in the section on [Developing a Custom Data Flow Component](../extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md), and especially [Developing a Custom Transformation Component with Synchronous Outputs](../extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-synchronous-outputs.md).  
  
## Getting Started with a Synchronous Transformation Component  
 When you add a Script component to the Data Flow pane of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, the **Select Script Component Type** dialog box opens and prompts you to select a Source, Destination, or Transformation component type. In this dialog box, select **Transformation**.  
  
## Configuring a Synchronous Transformation Component in Metadata-Design Mode  
 After you select the option to create a transformation component, you configure the component by using the **Script Transformation Editor**. For more information, see [Configuring the Script Component in the Script Component Editor](../extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md).  
  
 To set the script language for the Script component, you set the **ScriptLanguage** property on the **Script** page of the **Script Transformation Editor**.  
  
> [!NOTE]  
>  To set the default scripting language for the Script component, use the **Scripting language** option on the **General** page of the **Options** dialog box. For more information, see [General Page](../general-page-of-integration-services-designers-options.md).  
  
 A data flow transformation component has one input, and supports one or more outputs. Configuring the input and outputs for the component is one of the steps that you must complete in metadata design mode, by using the **Script Transformation Editor**, before you write your custom script.  
  
### Configuring Input Columns  
 A transformation component has one input.  
  
 On the **Input Columns** page of the **Script Transformation Editor,** the column list shows the available columns from the output of the upstream component in the data flow. Select the columns that you want to transform or pass through. Mark any columns that you want to transform in place as Read/Write.  
  
 For more information about the **Input Columns** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Input Columns Page&#41;](../script-transformation-editor-input-columns-page.md).  
  
### Configuring Inputs, Outputs, and Output Columns  
 A transformation component supports one or more outputs.  
  
 On the **Inputs and Outputs** page of the **Script Transformation Editor**, you can see that a single output has been created, but the output has no columns. On this page of the editor, you may need or want to configure the following items.  
  
-   Create one or more additional outputs, such as a simulated error output for rows that contain unexpected values. Use the **Add Output** and **Remove Output** buttons to manage the outputs of your synchronous transformation component. All input rows are directed to all available outputs unless you indicate that you intend to redirect each row to one output or the other. You indicate that you intend to redirect rows by specifying a non-zero integer value for the `ExclusionGroup` property on the outputs. The specific integer value entered in `ExclusionGroup` to identify the outputs is not significant, but you must use the same integer consistently for the specified group of outputs.  
  
    > [!NOTE]  
    >  You can also use a non-zero `ExclusionGroup` property value with a single output when you do not want to output all rows. However, in this case,  you must explicitly call the **DirectRowTo\<outputbuffer>** method for each row that you want to send to the output.  
  
-   Assign a more descriptive name to the input and outputs. The Script component uses these names to generate the typed accessor properties that you will use to refer to the input and outputs in your script.  
  
-   Leave columns as is for synchronous transformations. Typically a synchronous transformation does not add columns to the data flow. Data is modified in place in the buffer, and the buffer is passed on to the next component in the data flow. If this is the case, you do not have to add and configure output columns explicitly on the transformation's outputs. The outputs appear in the editor without any explicitly defined columns.  
  
-   Add new columns to simulated error outputs for row-level errors. Ordinarily multiple outputs in the same `ExclusionGroup` have the same set of output columns. However, if you are creating a simulated error output, you may want to add more columns to contain error information. For information about how the data flow engine processes error rows, see [Using Error Outputs in a Data Flow Component](../extending-packages-custom-objects/data-flow/using-error-outputs-in-a-data-flow-component.md). Note that in the Script component you must write your own code to fill the additional columns with appropriate error information. For more information, see [Simulating an Error Output for the Script Component](../extending-packages-scripting-data-flow-script-component-examples/simulating-an-error-output-for-the-script-component.md).  
  
 For more information about the **Inputs and Outputs** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Inputs and Outputs Page&#41;](../script-transformation-editor-inputs-and-outputs-page.md).  
  
### Adding Variables  
 If you want to use existing variables in your script, you can add them in the `ReadOnlyVariables` and `ReadWriteVariables` property fields on the **Script** page of the **Script Transformation Editor**.  
  
 When you add multiple variables in the property fields, separate the variable names by commas. You can also select multiple variables by clicking the ellipsis (**...**) button next to the `ReadOnlyVariables` and `ReadWriteVariables` property fields, and then selecting the variables in the **Select variables** dialog box.  
  
 For general information about how to use variables with the Script component, see [Using Variables in the Script Component](../extending-packages-scripting/data-flow-script-component/using-variables-in-the-script-component.md).  
  
 For more information about the **Script** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Script Page&#41;](../script-transformation-editor-script-page.md).  
  
## Scripting a Synchronous Transformation Component in Code-Design Mode  
 After you have configured the metadata for your component, you can write your custom script. In the **Script Transformation Editor**, on the **Script** page, click **Edit Script** to open the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA) IDE where you can add your custom script. The scripting language that you use depends on whether you selected [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C# as the script language for the **ScriptLanguage** property on the **Script** page.  
  
 For important information that applies to all kinds of components created by using the Script component, see [Coding and Debugging the Script Component](../extending-packages-scripting/data-flow-script-component/using-variables-in-the-script-component.md).  
  
### Understanding the Auto-generated Code  
 When you open the VSTA IDE after you create and configuring a transformation component, the editable `ScriptMain` class appears in the code editor with a stub for the `ProcessInputRow` method. The `ScriptMain` class is where you will write your custom code, and `ProcessInputRow` is the most important method in a transformation component.  
  
 If you open the **Project Explorer** window in VSTA, you can see that the Script component has also generated read-only `BufferWrapper` and `ComponentWrapper` project items. The `ScriptMain` class inherits from the `UserComponent` class in the `ComponentWrapper` project item.  
  
 At run time, the data flow engine invokes the `ProcessInput` method in the `UserComponent` class, which overrides the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.ProcessInput%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> parent class. The `ProcessInput` method in turn loops through the rows in the input buffer and calls the `ProcessInputRow` method one time for each row.  
  
### Writing Your Custom Code  
 A transformation component with synchronous outputs is the simplest of all data flow components to write. For example, the single-output example shown later in this topic consists of the following custom code:  
  
```vb  
Row.City = UCase(Row.City)  
```  
  
```csharp  
Row.City = (Row.City).ToUpper();  
  
```  
  
 To finish creating a custom synchronous transformation component, you use the overridden `ProcessInputRow` method to transform the data in each row of the input buffer. The data flow engine passes this buffer, when full, to the next component in the data flow.  
  
 Depending on your requirements, you may also want to write script in the `PreExecute` and `PostExecute` methods, available in the `ScriptMain` class, to perform preliminary or final processing.  
  
### Working with Multiple Outputs  
 Directing input rows to one of two or more possible outputs does not require much more custom code than the single-output scenario discussed earlier. For example, the two-output example shown later in this topic consists of the following custom code:  
  
```vb  
Row.City = UCase(Row.City)  
If Row.City = "REDMOND" Then  
    Row.DirectRowToMyRedmondAddresses()  
Else  
    Row.DirectRowToMyOtherAddresses()  
End If  
```  
  
```csharp  
Row.City = (Row.City).ToUpper();  
  
if (Row.City=="REDMOND")  
{  
    Row.DirectRowToMyRedmondAddresses();  
}  
else  
{  
    Row.DirectRowToMyOtherAddresses();  
}  
```  
  
 In this example, the Script component generates the **DirectRowTo\<OutputBufferX>** methods for you, based on the names of the outputs that you configured. You can use similar code to direct error rows to a simulated error output.  
  
## Examples  
 The examples here demonstrate the custom code that is required in the `ScriptMain` class to create a synchronous transformation component.  
  
> [!NOTE]  
>  These examples use the **Person.Address** table in the `AdventureWorks` sample database and pass its first and fourth columns, the **intAddressID** and **nvarchar(30)City** columns, through the data flow. The same data is used in the source, transformation, and destination samples in this section. Additional prerequisites and assumptions are documented for each example.  
  
### Single Output Synchronous Transformation Example  
 This example demonstrates a synchronous transformation component with a single output. This transformation passes through the **AddressID** column and converts the **City** column to uppercase.  
  
 If you want to run this sample code, you must configure the package and the component as follows:  
  
1.  Add a new Script component to the Data Flow designer surface and configure it as a transformation.  
  
2.  Connect the output of a source or of another transformation to the new transformation component in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This output should provide data from the **Person.Address** table of the `AdventureWorks` sample database that contains the **AddressID** and **City** columns.  
  
3.  Open the **Script Transformation Editor**. On the **Input Columns** page, select the **AddressID** and **City** columns. Mark the **City** column as Read/Write.  
  
4.  On the **Inputs and Outputs** page, rename the input and output with more descriptive names, such as **MyAddressInput** and **MyAddressOutput**. Notice that the `SynchronousInputID` of the output corresponds to the `ID` of the input. Therefore you do not have to add and configure output columns.  
  
5.  On the **Script** page, click **Edit Script** and enter the script that follows. Then close the script development environment and the **Script Transformation Editor**.  
  
6.  Create and configure a destination component that expects the **AddressID** and **City** columns, such as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination, or the sample destination component demonstrated in [Creating a Destination with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md). Then connect the output of the transformation to the destination component. You can create a destination table by running the following [!INCLUDE[tsql](../../includes/tsql-md.md)] command in the `AdventureWorks` database:  
  
    ```  
    CREATE TABLE [Person].[Address2]([AddressID] [int] NOT NULL,  
        [City] [nvarchar](30) NOT NULL)  
    ```  
  
7.  Run the sample.  
  
```vb  
Public Class ScriptMain  
    Inherits UserComponent  
  
    Public Overrides Sub MyAddressInput_ProcessInputRow(ByVal Row As MyAddressInputBuffer)  
  
        Row.City = UCase(Row.City)  
  
    End Sub  
  
End Class  
```  
  
```csharp  
public class ScriptMain:  
    UserComponent  
  
{  
    public override void MyAddressInput_ProcessInputRow(MyAddressInputBuffer Row)  
    {  
  
        Row.City = (Row.City).ToUpper();  
  
    }  
  
}  
```  
  
### Two-Output Synchronous Transformation Example  
 This example demonstrates a synchronous transformation component with two outputs. This transformation passes through the **AddressID** column and converts the **City** column to uppercase. If the city name is Redmond, it directs the row to one output; it directs all other rows to another output.  
  
 If you want to run this sample code, you must configure the package and the component as follows:  
  
1.  Add a new Script component to the Data Flow designer surface and configure it as a transformation.  
  
2.  Connect the output of a source or of another transformation to the new transformation component in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This output should provide data from the **Person.Address** table of the `AdventureWorks` sample database that contains at least the **AddressID** and **City** columns.  
  
3.  Open the **Script Transformation Editor**. On the **Input Columns** page, select the **AddressID** and **City** columns. Mark the **City** column as Read/Write.  
  
4.  On the **Inputs and Outputs** page, create a second output. After you add the new output, make sure that you set its `SynchronousInputID` to the `ID` of the input. This property is already set on the first output, which is created by default. For each output, set the `ExclusionGroup` property to the same non-zero value to indicate that you will split the input rows between two mutually exclusive outputs. You do not have to add any output columns to the outputs.  
  
5.  Rename the input and outputs with more descriptive names, such as **MyAddressInput**, **MyRedmondAddresses**, and **MyOtherAddresses**.  
  
6.  On the **Script** page, click **Edit Script** and enter the script that follows. Then close the script development environment and the **Script Transformation Editor**.  
  
7.  Create and configure two destination components that expect the **AddressID** and **City** columns, such as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination, a Flat File destination, or the sample destination component demonstrated in [Creating a Destination with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md). Then connect each of the outputs of the transformation to one of the destination components. You can create destination tables by running a [!INCLUDE[tsql](../../includes/tsql-md.md)] command similar to the following (with unique table names) in the `AdventureWorks` database:  
  
    ```  
    CREATE TABLE [Person].[Address2](  
        [AddressID] [int] NOT NULL,  
        [City] [nvarchar](30) NOT NULL  
    ```  
  
8.  Run the sample.  
  
```vb  
Public Class ScriptMain  
    Inherits UserComponent  
  
    Public Overrides Sub MyAddressInput_ProcessInputRow(ByVal Row As MyAddressInputBuffer)  
  
        Row.City = UCase(Row.City)  
  
        If Row.City = "REDMOND" Then  
            Row.DirectRowToMyRedmondAddresses()  
        Else  
            Row.DirectRowToMyOtherAddresses()  
        End If  
  
    End Sub  
  
End Class  
```  
  
```csharp  
public class ScriptMain:  
    UserComponent  
  
public override void MyAddressInput_ProcessInputRow(MyAddressInputBuffer Row)  
    {  
  
        Row.City = (Row.City).ToUpper();  
  
        if (Row.City == "REDMOND")  
        {  
            Row.DirectRowToMyRedmondAddresses();  
        }  
        else  
        {  
            Row.DirectRowToMyOtherAddresses();  
        }  
  
    }  
}  
```  
  
|![](./media/creating-a-synchronous-transformation-with-the-script-component/dts-16.gif)  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/msconame-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Understanding Synchronous and Asynchronous Transformations](../understanding-synchronous-and-asynchronous-transformations.md) 
 [Creating an Asynchronous Transformation with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-an-asynchronous-transformation-with-the-script-component.md)
 [Developing a Custom Transformation Component with Synchronous Outputs](../extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-synchronous-outputs.md)  
  
  

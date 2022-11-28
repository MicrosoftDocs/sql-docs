---
description: "Creating an Asynchronous Transformation with the Script Component"
title: "Creating an Asynchronous Transformation with the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "asynchronous outputs [Integration Services]"
  - "transformation components [Integration Services]"
  - "Script component [Integration Services], transformation components"
ms.assetid: 0d814404-21e4-4a68-894c-96fa47ab25ae
author: chugugrace
ms.author: chugu
---
# Creating an Asynchronous Transformation with the Script Component

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  You use a transformation component in the data flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package to modify and analyze data as it passes from source to destination. A transformation with synchronous outputs processes each input row as it passes through the component. A transformation with asynchronous outputs may wait to complete its processing until the transformation has received all input rows, or the transformation may output certain rows before it has received all input rows. This topic discusses an asynchronous transformation. If your processing requires a synchronous transformation, see [Creating a Synchronous Transformation with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-synchronous-transformation-with-the-script-component.md). For more information about the differences between synchronous and asynchronous components, see [Understanding Synchronous and Asynchronous Transformations](../../integration-services/understanding-synchronous-and-asynchronous-transformations.md).  
  
 For an overview of the Script component, see [Extending the Data Flow with the Script Component](../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md).  
  
 The Script component and the infrastructure code that it generates for you simplify the process of developing a custom data flow component. However, to understand how the Script component works, you may find it useful to read through the steps that you must follow in developing a custom data flow component in the [Developing a Custom Data Flow Component](../../integration-services/extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md) section, and especially [Developing a Custom Transformation Component with Synchronous Outputs](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-synchronous-outputs.md).  
  
## Getting Started with an Asynchronous Transformation Component  
 When you add a Script component to the Data Flow tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, the **Select Script Component Type** dialog box appears, prompting you to preconfigure the component as a source, transformation, or destination. In this dialog box, select **Transformation**.  
  
## Configuring an Asynchronous Transformation Component in Metadata-Design Mode  
 After you select the option to create a transformation component, you configure the component by using the **Script Transformation Editor**. For more information, see [Configuring the Script Component in the Script Component Editor](../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md).  
  
 To select the script language that the Script component will use, you set the **ScriptLanguage** property on the **Script** page of the **Script Transformation Editor** dialog box.  
  
> [!NOTE]  
>  To set the default scripting language for the Script component, use the **Scripting language** option on the **General** page of the **Options** dialog box. For more information, see [General Page](../general-page-of-integration-services-designers-options.md).  
  
 A data flow transformation component has one input and supports one or more outputs. Configuring the input and outputs of your component is one of the steps that you must complete in metadata design mode, by using the **Script Transformation Editor**, before you write your custom script.  
  
### Configuring Input Columns  
 A transformation component created by using the Script component has a single input.  
  
 On the **Input Columns** page of the **Script Transformation Editor**, the columns list shows the available columns from the output of the upstream component in the data flow. Select the columns that you want to transform or pass through. Mark any columns that you want to transform in place as Read/Write.  
  
 For more information about the **Input Columns** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Input Columns Page&#41;](../data-flow/transformations/script-component.md).  
  
### Configuring Inputs, Outputs, and Output Columns  
 A transformation component supports one or more outputs.  
  
 Frequently a transformation with asynchronous outputs has two outputs. For example, when you count the number of addresses located in a specific city, you may want to pass the address data through to one output, while sending the result of the aggregation to another output. The aggregation output also requires a new output column.  
  
 On the **Inputs and Outputs** page of the **Script Transformation Editor**, you see that a single output has been created by default, but no output columns have been created. On this page of the editor, you can configure the following items:  
  
-   You may want to create one or more additional outputs, such as an output for the result of an aggregation. Use the **Add Output** and **Remove Output** buttons to manage the outputs of your asynchronous transformation component. Set the **SynchronousInputID** property of each output to zero to indicate that the output does not simply pass through data from an upstream component or transform it in place in the existing rows and columns. It is this setting that makes the outputs asynchronous to the input.  
  
-   You may want to assign a friendly name to the input and outputs. The Script component uses these names to generate the typed accessor properties that you will use to refer to the input and outputs in your script.  
  
-   Frequently an asynchronous transformation adds columns to the data flow. When the **SynchronousInputID** property of an output is zero, indicating that the output does not simply pass through data from an upstream component or transform it in place in the existing rows and columns, you must add and configure output columns explicitly on the output. Output columns do not have to have the same names as the input columns to which they are mapped.  
  
-   You may want to add more columns to contain additional information. You must write your own code to fill the additional columns with data. For information about reproducing the behavior of a standard error output, see [Simulating an Error Output for the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-examples/simulating-an-error-output-for-the-script-component.md).  
  
 For more information about the **Inputs and Outputs** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Inputs and Outputs Page&#41;](../data-flow/transformations/script-component.md).  
  
### Adding Variables  
 If there are any existing variables whose values you want to use in your script, you can add them in the ReadOnlyVariables and ReadWriteVariables property fields on the **Script** page of the **Script Transformation Editor**.  
  
 When you add multiple variables in the property fields, separate the variable names by commas. You can also select multiple variables by clicking the ellipsis (**...**) button next to the **ReadOnlyVariables** and **ReadWriteVariables** property fields, and then selecting the variables in the **Select variables** dialog box.  
  
 For general information about how to use variables with the Script component, see [Using Variables in the Script Component](../../integration-services/extending-packages-scripting/data-flow-script-component/using-variables-in-the-script-component.md).  
  
 For more information about the **Script** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Script Page&#41;](../data-flow/transformations/script-component.md).  
  
## Scripting an Asynchronous Transformation Component in Code-Design Mode  
 After you have configured all the metadata for your component, you can write your custom script. In the **Script Transformation Editor**, on the **Script** page, click **Edit Script** to open the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA) IDE where you can add your custom script. The scripting language that you use depends on whether you selected [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C# as the script language for the **ScriptLanguage** property on the **Script** page.  
  
 For important information that applies to all kinds of components created by using the Script component, see [Coding and Debugging the Script Component](../../integration-services/extending-packages-scripting/data-flow-script-component/coding-and-debugging-the-script-component.md).  
  
### Understanding the Auto-generated Code  
 When you open the VSTA IDE after creating and configuring a transformation component, the editable **ScriptMain** class appears in the code editor with stubs for the ProcessInputRow and the CreateNewOutputRows methods. The ScriptMain class is where you will write your custom code, and ProcessInputRow is the most important method in a transformation component. The **CreateNewOutputRows** method is more typically used in a source component, which is like an asynchronous transformation in that both components must create their own output rows.  
  
 If you open the VSTA **Project Explorer** window, you can see that the Script component has also generated read-only **BufferWrapper** and **ComponentWrapper** project items. The ScriptMain class inherits from the UserComponent class in the **ComponentWrapper** project item.  
  
 At run time, the data flow engine calls the PrimeOutput method in the **UserComponent** class, which overrides the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.PrimeOutput%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> parent class. The PrimeOutput method in turn calls the CreateNewOutputRows method.  
  
 Next, the data flow engine invokes the ProcessInput method in the UserComponent class, which overrides the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.ProcessInput%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> parent class. The ProcessInput method in turn loops through the rows in the input buffer and calls the ProcessInputRow method one time for each row.  
  
### Writing Your Custom Code  
 To finish creating a custom asynchronous transformation component, you must use the overridden ProcessInputRow method to process the data in each row of the input buffer. Because the outputs are not synchronous to the input, you must explicitly write rows of data to the outputs.  
  
 In an asynchronous transformation, you can use the AddRow method to add rows to the output as appropriate from within the ProcessInputRow or ProcessInput methods. You do not have to use the CreateNewOutputRows method. If you are writing a single row of results, such as aggregation results, to a particular output, you can create the output row beforehand by using the CreateNewOutputRows method, and fill in its values later after processing all input rows. However it is not useful to create multiple rows in the CreateNewOutputRows method, because the Script component only lets you use the current row in an input or output. The CreateNewOutputRows method is more important in a source component where there are no input rows to process.  
  
 You may also want to override the ProcessInput method itself, so that you can do additional preliminary or final processing before or after you loop through the input buffer and call ProcessInputRow for each row. For example, one of the code examples in this topic overrides ProcessInput to count the number of addresses in a specific city as ProcessInputRow loops through rows**.** The example writes the summary value to the second output after all rows have been processed. The example completes the output in ProcessInput because the output buffers are no longer available when PostExecute is called.  
  
 Depending on your requirements, you may also want to write script in the PreExecute and PostExecute methods available in the ScriptMain class to perform any preliminary or final processing.  
  
> [!NOTE]  
>  If you were developing a custom data flow component from scratch, it would be important to override the PrimeOutput method to cache references to the output buffers so that you could add rows of data to the buffers later. In the Script component, this is not necessary because you have an automatically generated class representing each output buffer in the **BufferWrapper** project item.  
  
## Example  
 This example demonstrates the custom code that is required in the ScriptMain class to create an asynchronous transformation component.  
  
> [!NOTE]  
>  These examples use the **Person.Address** table in the **AdventureWorks** sample database and pass its first and fourth columns, the **intAddressID** and **nvarchar(30)City** columns, through the data flow. The same data is used in the source, transformation, and destination samples in this section. Additional prerequisites and assumptions are documented for each example.  
  
 This example demonstrates an asynchronous transformation component with two outputs. This transformation passes through the **AddressID** and **City** columns to one output, while it counts the number of addresses located in a specific city (Redmond, Washington, U.S.A.), and then outputs the resulting value to a second output.  
  
 If you want to run this sample code, you must configure the package and the component as follows:  
  
1.  Add a new Script component to the Data Flow designer surface and configure it as a transformation.  
  
2.  Connect the output of a source or of another transformation to the new transformation component in the designer. This output should provide data from the **Person.Address** table of the **AdventureWorks** sample database that contains at least the **AddressID** and **City** columns.  
  
3.  Open the **Script Transformation Editor**. On the **Input Columns** page, select the **AddressID** and **City** columns.  
  
4.  On the **Inputs and Outputs** page, add and configure the **AddressID** and **City** output columns on the first output. Add a second output, and add an output column for the summary value on the second output. Set the SynchronousInputID property of the first output to 0, because this example copies each input row explicitly to the first output. The SynchronousInputID property of the newly-created output is already set to 0.  
  
5.  Rename the input, the outputs, and the new output column to give them more descriptive names. The example uses **MyAddressInput** as the name of the input, **MyAddressOutput** and **MySummaryOutput** for the outputs, and **MyRedmondCount** for the output column on the second output.  
  
6.  On the **Script** page, click **Edit Script** and enter the script that follows. Then close the script development environment and the **Script Transformation Editor**.  
  
7.  Create and configure a destination component for the first output that expects the **AddressID** and **City** columns, such as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination, or the sample destination component demonstrated in [Creating a Destination with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md), . Then connect the first output of the transformation, **MyAddressOutput**, to the destination component. You can create a destination table by running the following [!INCLUDE[tsql](../../includes/tsql-md.md)] command in the **AdventureWorks** database:  
  
    ```sql
    CREATE TABLE [Person].[Address2]([AddressID] [int] NOT NULL,  
        [City] [nvarchar](30) NOT NULL)  
    ```  
  
8.  Create and configure another destination component for the second output. Then connect the second output of the transformation, **MySummaryOutput**, to the destination component. Because the second output writes a single row with a single value, you can easily configure a destination with a Flat File connection manager that connects to a new file that has a single column. In the example, this destination column is named **MyRedmondCount**.  
  
9. Run the sample.  
  
```vb  
Public Class ScriptMain  
    Inherits UserComponent  
  
    Private myRedmondAddressCount As Integer  
  
    Public Overrides Sub CreateNewOutputRows()  
  
        MySummaryOutputBuffer.AddRow()  
  
    End Sub  
  
    Public Overrides Sub MyAddressInput_ProcessInput(ByVal Buffer As MyAddressInputBuffer)  
  
        While Buffer.NextRow()  
            MyAddressInput_ProcessInputRow(Buffer)  
        End While  
  
        If Buffer.EndOfRowset Then  
            MyAddressOutputBuffer.SetEndOfRowset()  
            MySummaryOutputBuffer.MyRedmondCount = myRedmondAddressCount  
            MySummaryOutputBuffer.SetEndOfRowset()  
        End If  
  
    End Sub  
  
    Public Overrides Sub MyAddressInput_ProcessInputRow(ByVal Row As MyAddressInputBuffer)  
  
        With MyAddressOutputBuffer  
            .AddRow()  
            .AddressID = Row.AddressID  
            .City = Row.City  
        End With  
  
        If Row.City.ToUpper = "REDMOND" Then  
            myRedmondAddressCount += 1  
        End If  
  
    End Sub  
  
End Class  
```  
  
```csharp  
public class ScriptMain:  
    UserComponent  
  
{  
    private int myRedmondAddressCount;  
  
    public override void CreateNewOutputRows()  
    {  
  
        MySummaryOutputBuffer.AddRow();  
  
    }  
  
    public override void MyAddressInput_ProcessInput(MyAddressInputBuffer Buffer)  
    {  
  
        while (Buffer.NextRow())  
        {  
            MyAddressInput_ProcessInputRow(Buffer);  
        }  
  
        if (Buffer.EndOfRowset())  
        {  
            MyAddressOutputBuffer.SetEndOfRowset();  
            MySummaryOutputBuffer.MyRedmondCount = myRedmondAddressCount;  
            MySummaryOutputBuffer.SetEndOfRowset();  
        }  
  
    }  
  
    public override void MyAddressInput_ProcessInputRow(MyAddressInputBuffer Row)  
    {  
  
        {  
            MyAddressOutputBuffer.AddRow();  
            MyAddressOutputBuffer.AddressID = Row.AddressID;  
            MyAddressOutputBuffer.City = Row.City;  
        }  
  
        if (Row.City.ToUpper() == "REDMOND")  
        {  
            myRedmondAddressCount += 1;  
        }  
  
    }  
  
}  
  
```  
  
## See Also  
 [Understanding Synchronous and Asynchronous Transformations](../../integration-services/understanding-synchronous-and-asynchronous-transformations.md)   
 [Creating a Synchronous Transformation with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-synchronous-transformation-with-the-script-component.md)   
 [Developing a Custom Transformation Component with Asynchronous Outputs](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-asynchronous-outputs.md)  
  

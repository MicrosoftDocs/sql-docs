---
title: "Enhancing an Error Output with the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "01/04/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "transformations [Integration Services], components"
  - "Script component [Integration Services], examples"
  - "error outputs [Integration Services], enhancing"
  - "Script component [Integration Services], transformation components"
ms.assetid: f7c02709-f1fa-4ebd-b255-dc8b81feeaa5
author: janinezhang
ms.author: janinez
manager: craigg
---
# Enhancing an Error Output with the Script Component

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  By default, the two extra columns in an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] error output, ErrorCode and ErrorColumn, contain only numeric codes that represent an error number and the ID of the column in which the error occurred. These numeric values may be of limited use without the corresponding error description and column name.  
  
 This topic describes how to add the error description and the column name to existing error output data in the data flow by using the Script component. The example adds the error description that corresponds to a specific predefined [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] error code by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.GetErrorDescription%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface, available through the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.ComponentMetaData%2A> property of the Script component. Then the example adds the column name that corresponds to the captured lineage ID by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData130.GetIdentificationStringByID%2A> method of the same interface.  
  
> [!NOTE]  
>  If you want to create a component that you can more easily reuse across multiple Data Flow tasks and multiple packages, consider using the code in this Script component sample as the starting point for a custom data flow component. For more information, see [Developing a Custom Data Flow Component](../../integration-services/extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md).  
  
## Example  
 The example shown here uses a Script component configured as a transformation to add the error description and the column name to existing error output data in the data flow.  
  
 For more information about how to configure the Script component for use as a transformation in the data flow, see [Creating a Synchronous Transformation with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-synchronous-transformation-with-the-script-component.md) and [Creating an Asynchronous Transformation with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-an-asynchronous-transformation-with-the-script-component.md).  
  
#### To configure this Script Component example  
  
1.  Before creating the new Script component, configure an upstream component in the data flow to redirect rows to its error output when an error or truncation occurs. For testing purposes, you may want to configure a component in a manner that ensures that errors will occur-for example, by configuring a Lookup transformation between two tables where the lookup will fail.  
  
2.  Add a new Script component to the Data Flow designer surface and configure it as a transformation.  
  
3.  Connect the error output from the upstream component to the new Script component.  
  
4.  Open the **Script Transformation Editor**, and on the **Script** page, for the **ScriptLanguage** property, select the script language.  
  
5.  Click **Edit Script** to open the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA) IDE and add the sample code shown below.  
  
6.  Close VSTA.  
  
7.  In the Script Transformation Editor, on the **Input Columns** page, select the ErrorCode and ErrorColumn columns.  
  
8.  On the **Inputs and Outputs** page, add two new columns.  
  
    -   Add a new output column of type **String** named **ErrorDescription**. Increase the default length of the new column to 255 to support long messages.  
  
    -   Add another new output column of type **String** named **ColumnName**. Increase the default length of the new column to 255 to support long values.  
  
9. Close the **Script Transformation Editor.**  
  
10. Attach the output of the Script component to a suitable destination. A Flat File destination is the easiest to configure for ad hoc testing.  
  
11. Run the package.  

```vb
Public Class ScriptMain      ' VB
    Inherits UserComponent
    Public Overrides Sub Input0_ProcessInputRow(ByVal Row As Input0Buffer)

        Row.ErrorDescription = _
            Me.ComponentMetaData.GetErrorDescription(Row.ErrorCode)

        Dim componentMetaData130 = TryCast(Me.ComponentMetaData, IDTSComponentMetaData130)

        If componentMetaData130 IsNot Nothing Then

            If 0 = Row.ErrorColumn Then
                ' 0 means no specific column is identified by ErrorColumn, this time.
                Row.ColumnName = "Check the row for a violation of a foreign key constraint."
            Else
                Row.ColumnName = componentMetaData130.GetIdentificationStringByID(Row.ErrorColumn)
            End If
        End If
    End Sub
End Class
```

```csharp
public class ScriptMain:      // C#
    UserComponent
{
    public override void Input0_ProcessInputRow(Input0Buffer Row)
    {
        Row.ErrorDescription = this.ComponentMetaData.GetErrorDescription(Row.ErrorCode);

        var componentMetaData130 = this.ComponentMetaData as IDTSComponentMetaData130;
        if (componentMetaData130 != null)
        {
            // 0 means no specific column is identified by ErrorColumn, this time.
            if (Row.ErrorColumn == 0)
            {
                Row.ColumnName = "Check the row for a violation of a foreign key constraint.";
            }
            else
            {
                Row.ColumnName = componentMetaData130.GetIdentificationStringByID(Row.ErrorColumn);
            }
        }
    }
}
```

## See Also  
 [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)   
 [Using Error Outputs in a Data Flow Component](../../integration-services/extending-packages-custom-objects/data-flow/using-error-outputs-in-a-data-flow-component.md)   
 [Creating a Synchronous Transformation with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-synchronous-transformation-with-the-script-component.md)   
  
  

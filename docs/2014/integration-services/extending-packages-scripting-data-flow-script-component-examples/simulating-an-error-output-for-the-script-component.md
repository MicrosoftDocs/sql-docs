---
title: "Simulating an Error Output for the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Script component [Integration Services], error output"
  - "error outputs [Integration Services], Script component"
ms.assetid: f8b6ecff-ac99-4231-a0e7-7ce4ad76bad0
author: janinezhang
ms.author: janinez
manager: craigg
---
# Simulating an Error Output for the Script Component
  Although you cannot directly configure an output as an error output in the Script component for automatic handling of error rows, you can reproduce the functionality of a built-in error output by creating an additional output and using conditional logic in your script to direct rows to this output when appropriate. You may want to imitate the behavior of a built-in error output by adding two additional output columns to receive the error number and the ID of the column in which an error occurred.  
  
 If you want to add the error description that corresponds to a specific predefined [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] error code, you can use the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.GetErrorDescription%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface, available through the Script component's <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.ComponentMetaData%2A> property.  
  
## Example  
 The example shown here uses a Script component configured as a transformation that has two synchronous outputs. The purpose of the Script component is to filter error rows from address data in the AdventureWorks sample database. This fictitious example assumes that we are preparing a promotion for North American customers and need to filter out addresses that are not located in North America.  
  
#### To configure the example  
  
1.  Before creating the new Script component, create a connection manager and configure a data flow source that selects address data from the AdventureWorks sample database. For this example, which only looks at the CountryRegionName column, you can simply use the Person.vStateCountryProvinceRegion view, or you can select data by joining the Person.Address, Person.StateProvince, and Person.CountryRegion tables.  
  
2.  Add a new Script component to the Data Flow designer surface and configure it as a transformation. Open the **Script Transformation Editor**.  
  
3.  On the **Script** page, set the **ScriptLanguage** property to the script language that you want to use to code the script.  
  
4.  Click **Edit Script** to open [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA).  
  
5.  In the `Input0_ProcessInputRow` method, type or paste the sample code shown below.  
  
6.  Close VSTA.  
  
7.  On the **Input Columns** page, select the columns that you want to process in the Script transformation. This example uses only the CountryRegionName column. Available input columns that you leave unselected will simply be passed through unchanged in the data flow.  
  
8.  On the **Inputs and Outputs** page, add a new, second output, and set its `SynchronousInputID` value to the ID of the input, which is also the value of the `SynchronousInputID` property of the default output. Set the `ExclusionGroup` property of both outputs to the same non-zero value (for example, 1) to indicate that each row will be directed to only one of the two outputs. Give the new error output a distinctive name, such as "MyErrorOutput."  
  
9. Add additional output columns to the new error output to capture the desired error information, which may include the error code, the ID of the column in which the error occurred, and possibly the error description. This example creates the new columns, ErrorColumn and ErrorMessage. If you are catching predefined [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] errors in your own implementation, make sure to add an ErrorCode column for the error number.  
  
10. Note the ID value of the input column or columns that the Script component will check for error conditions. This example uses this column identifier to populate the ErrorColumn value.  
  
11. Close the **Script Transformation Editor.**  
  
12. Attach the outputs of the Script component to suitable destinations. Flat file destinations are the easiest to configure for ad hoc testing.  
  
13. Run the package.  
  
```vb  
Public Overrides Sub Input0_ProcessInputRow(ByVal Row As Input0Buffer)  
  
  If Row.CountryRegionName <> "Canada" _  
      And Row.CountryRegionName <> "United States" Then  
  
    Row.ErrorColumn = 68 ' ID of CountryRegionName column  
    Row.ErrorMessage = "Address is not in North America."  
    Row.DirectRowToMyErrorOutput()  
  
  Else  
  
    Row.DirectRowToOutput0()  
  
  End If  
  
End Sub  
```  
  
```csharp  
public override void Input0_ProcessInputRow(Input0Buffer Row)  
{  
  
  if (Row.CountryRegionName!="Canada"&&Row.CountryRegionName!="United States")  
  
  {  
    Row.ErrorColumn = 68; // ID of CountryRegionName column  
    Row.ErrorMessage = "Address is not in North America.";  
    Row.DirectRowToMyErrorOutput();  
  
  }  
  else  
  {  
  
    Row.DirectRowToOutput0();  
  
  }  
  
}  
```  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Error Handling in Data](../data-flow/error-handling-in-data.md)   
 [Using Error Outputs in a Data Flow Component](../extending-packages-custom-objects/data-flow/using-error-outputs-in-a-data-flow-component.md)   
 [Creating a Synchronous Transformation with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-a-synchronous-transformation-with-the-script-component.md) 
  
  

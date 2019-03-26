---
title: "Using Variables in the Script Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Foreach Loop containers"
  - "Script task [Integration Services], variables"
  - "scripts [Integration Services], variables"
  - "Variables property"
  - "Variable object"
  - "SSIS Script task, variables"
  - "variables [Integration Services], Script task"
ms.assetid: 593b5961-4bfa-4ce1-9531-a251c34e89d3
author: janinezhang
ms.author: janinez
manager: craigg
---
# Using Variables in the Script Task
  Variables make it possible for the Script task to exchange data with other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../../integration-services/integration-services-ssis-variables.md).  
  
 The Script task uses the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.Variables%2A> property of the **Dts** object to read from and write to <xref:Microsoft.SqlServer.Dts.Runtime.Variable> objects in the package.  
  
> [!NOTE]  
>  The <xref:Microsoft.SqlServer.Dts.Runtime.Variable.Value%2A> property of the <xref:Microsoft.SqlServer.Dts.Runtime.Variable> class is of type **Object**. Because the Script task has **Option Strict** enabled, you must cast the <xref:Microsoft.SqlServer.Dts.Runtime.Variable.Value%2A> property to the appropriate type before you can use it.  
  
 You add existing variables to the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptTask.ReadOnlyVariables%2A> and <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptTask.ReadWriteVariables%2A> lists in the **Script Task Editor** to make them available to the custom script. Keep in mind that variable names are case-sensitive. Within the script, you access variables of both types through the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.Variables%2A> property of the **Dts** object. Use the **Value** property to read from and write to individual variables. The Script task transparently manages locking as the script reads and modifies the values of variables.  
  
 You can use the <xref:Microsoft.SqlServer.Dts.Runtime.Variables.Contains%2A> method of the <xref:Microsoft.SqlServer.Dts.Runtime.Variables> collection returned by the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.Variables%2A> property to check for the existence of a variable before using it in your code.  
  
 You can also use the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.VariableDispenser%2A> property (Dts.VariableDispenser) to work with variables in the Script task. When using the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.VariableDispenser%2A>, you must handle both the locking semantics and the casting of data types for variable values in your own code. You may need to use the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.VariableDispenser%2A> property instead of the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.Variables%2A> property if you want to work with a variable that is not available at design time but is created programmatically at run time.  
  
## Using the Script Task within a Foreach Loop Container  
 When a Script task runs repeatedly within a Foreach Loop container, the script usually needs to work with the contents of the current item in the enumerator. For example, when using a Foreach File enumerator, the script needs to know the current file name; when using a Foreach ADO enumerator, the script needs to know the contents of the columns in the current row of data.  
  
 Variables make this communication between the Foreach Loop container and the Script task possible. On the **Variable Mappings** page of the **Foreach Loop Editor**, assign variables to each item of data that is returned by a single enumerated item. For example, a Foreach File enumerator returns only a file name at Index 0 and therefore requires only one variable mapping, whereas an enumerator that returns several columns of data in each row requires you to map a different variable to each column that you want to use in the Script task.  
  
 After you have mapped enumerated items to variables, then you must add the mapped variables to the **ReadOnlyVariables** property on the **Script** page of the **Script Task Editor** to make them available to your script. For an example of a Script task within a Foreach Loop container that processes the image files in a folder, see [Working with Images with the Script Task](../../../integration-services/extending-packages-scripting-task-examples/working-with-images-with-the-script-task.md).  
  
## Variables Example  
 The following example demonstrates how to access and use variables in a Script task to determine the path of package workflow. The sample assumes that you have created integer variables named `CustomerCount` and `MaxRecordCount` and added them to the **ReadOnlyVariables** collection in the **Script Task Editor**. The `CustomerCount` variable contains the number of customer records to be imported. If its value is greater than the value of `MaxRecordCount`, the Script task reports failure. When a failure occurs because the `MaxRecordCount` threshold has been exceeded, the error path of the workflow can implement any required clean-up.  
  
 To successfully compile the sample, you need to add a reference to the Microsoft.SqlServer.ScriptTask assembly.  
  
```vb  
Public Sub Main()  
  
    Dim customerCount As Integer  
    Dim maxRecordCount As Integer  
  
    If Dts.Variables.Contains("CustomerCount") = True AndAlso _  
        Dts.Variables.Contains("MaxRecordCount") = True Then  
  
        customerCount = _  
            CType(Dts.Variables("CustomerCount").Value, Integer)  
        maxRecordCount = _  
            CType(Dts.Variables("MaxRecordCount").Value, Integer)  
  
    End If  
  
    If customerCount > maxRecordCount Then  
            Dts.TaskResult = ScriptResults.Failure  
    Else  
            Dts.TaskResult = ScriptResults.Success  
    End If  
  
End Sub  
```  
  
```csharp  
using System;  
using System.Data;  
using Microsoft.SqlServer.Dts.Runtime;  
  
public class ScriptMain  
{  
  
    public void Main()  
    {  
        int customerCount;  
        int maxRecordCount;  
  
        if (Dts.Variables.Contains("CustomerCount")==true&&Dts.Variables.Contains("MaxRecordCount")==true)  
  
        {  
            customerCount = (int) Dts.Variables["CustomerCount"].Value;  
            maxRecordCount = (int) Dts.Variables["MaxRecordCount"].Value;  
  
        }  
  
        if (customerCount>maxRecordCount)  
        {  
            Dts.TaskResult = (int)ScriptResults.Failure;  
        }  
        else  
        {  
            Dts.TaskResult = (int)ScriptResults.Success;  
        }  
  
    }  
  
}  
  
```  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Variables](../../../integration-services/integration-services-ssis-variables.md)   
 [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)  
  
  

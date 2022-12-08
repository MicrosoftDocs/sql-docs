---
description: "Detecting an Empty Flat File with the Script Task"
title: "Detecting an Empty Flat File with the Script Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "flat files"
  - "Script task [Integration Services], empty flat files"
  - "SSIS Script task, empty flat files"
  - "Script task [Integration Services], examples"
ms.assetid: 1b4defb8-886a-483d-8056-d1b91d37bc90
author: chugugrace
ms.author: chugu
---
# Detecting an Empty Flat File with the Script Task

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The Flat File source does not determine whether a flat file contains rows of data before attempting to process it. You may want to improve the efficiency of a package, especially of a package that iterates over numerous flat files, by skipping files that do not contain any rows of data. The Script task can look for an empty flat file before the package begins to process the data flow.  
  
> [!NOTE]  
>  If you want to create a task that you can more easily reuse across multiple packages, consider using the code in this Script task sample as the starting point for a custom task. For more information, see [Developing a Custom Task](../../integration-services/extending-packages-custom-objects/task/developing-a-custom-task.md).  
  
## Description  
 The following example uses methods from the **System.IO** namespace to test the flat file specified in a Flat File connection manager to determine whether the file is empty, or whether it contains only expected non-data rows such as column headers or an empty line. The script checks the size of the file first; if the size is zero bytes, the file is empty. If the file size is greater than zero, the script reads lines from the file until there are no more lines, or until the number of lines exceeds the expected number of non-data rows. If the number of lines in the file is less than or equal to the expected number of non-data rows, then the file is considered empty. The result is returned as a Boolean value in a user variable, the value of which can be used for branching in the package's control flow. The **FireInformation** method also displays the result in the **Output** window of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA).  
  
#### To configure this Script Task example  
  
1.  Create and configure a flat file connection manager named **EmptyFlatFileTest**.  
  
2.  Create an integer variable named `FFNonDataRows` and set its value to the number of non-data rows expected in the flat file.  
  
3.  Create a Boolean variable named `FFIsEmpty`.  
  
4.  Add the `FFNonDataRows` variable to the Script task's **ReadOnlyVariables** property.  
  
5.  Add the `FFIsEmpty` variable to the Script task's **ReadWriteVariables** property.  
  
6.  In your code, import the **System.IO** namespace.  
  
 If you are iterating over files with a Foreach File enumerator, instead of using a single Flat File connection manager, you will need to modify the sample code below to obtain the file name and path from the variable in which the enumerated value is stored instead of from the connection manager.  
  
### Code  
  
```vb  
Public Sub Main()  
  
  Dim nonDataRows As Integer = _  
      DirectCast(Dts.Variables("FFNonDataRows").Value, Integer)  
  Dim ffConnection As String = _  
      DirectCast(Dts.Connections("EmptyFlatFileTest").AcquireConnection(Nothing), _  
      String)  
  Dim flatFileInfo As New FileInfo(ffConnection)  
  ' If file size is 0 bytes, flat file does not contain data.  
  Dim fileSize As Long = flatFileInfo.Length  
  If fileSize > 0 Then  
    Dim lineCount As Integer = 0  
    Dim line As String  
    Dim fsFlatFile As New StreamReader(ffConnection)  
    Do Until fsFlatFile.EndOfStream  
      line = fsFlatFile.ReadLine  
      lineCount += 1  
      ' If line count > expected number of non-data rows,  
      '  flat file contains data (default value).  
      If lineCount > nonDataRows Then  
        Exit Do  
      End If  
      ' If line count <= expected number of non-data rows,  
      '  flat file does not contain data.  
      If lineCount <= nonDataRows Then  
        Dts.Variables("FFIsEmpty").Value = True  
      End If  
    Loop  
  Else  
    Dts.Variables("FFIsEmpty").Value = True  
  End If  
  
  Dim fireAgain As Boolean = False  
  Dts.Events.FireInformation(0, "Script Task", _  
      String.Format("{0}: {1}", ffConnection, _  
      Dts.Variables("FFIsEmpty").Value.ToString), _  
      String.Empty, 0, fireAgain)  
  
  Dts.TaskResult = ScriptResults.Success  
  
End Sub  
```  
  
```csharp  
public void Main()  
        {  
  
            int nonDataRows = (int)(Dts.Variables["FFNonDataRows"].Value);  
            string ffConnection = (string)(Dts.Connections["EmptyFlatFileTest"].AcquireConnection(null) as String);  
            FileInfo flatFileInfo = new FileInfo(ffConnection);  
            // If file size is 0 bytes, flat file does not contain data.  
            long fileSize = flatFileInfo.Length;  
            if (fileSize > 0)  
            {  
  
                int lineCount = 0;  
                string line;  
                StreamReader fsFlatFile = new StreamReader(ffConnection);  
                while (!(fsFlatFile.EndOfStream))  
                {  
                    Console.WriteLine (fsFlatFile.ReadLine());  
                    lineCount += 1;  
                    // If line count > expected number of non-data rows,  
                    //  flat file contains data (default value).  
                    if (lineCount > nonDataRows)  
                    {  
                        break;  
                    }  
                    // If line count <= expected number of non-data rows,  
                    //  flat file does not contain data.  
                    if (lineCount <= nonDataRows)  
                    {  
                        Dts.Variables["FFIsEmpty"].Value = true;  
                    }  
                }  
            }  
            else  
            {  
                Dts.Variables["FFIsEmpty"].Value = true;  
            }  
  
            bool fireAgain = false;  
            Dts.Events.FireInformation(0, "Script Task", String.Format("{0}: {1}", ffConnection, Dts.Variables["FFIsEmpty"].Value), String.Empty, 0, ref fireAgain);  
  
            Dts.TaskResult = (int)ScriptResults.Success;  
  
        }  
```  
  
## See Also  
 [Script Task Examples](../../integration-services/extending-packages-scripting-task-examples/script-task-examples.md)  
  
  

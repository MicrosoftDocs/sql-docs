---
title: "Working with Excel Files with the Script Task | Microsoft Docs"
ms.custom: ""
ms.date: "05/15/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Script task [Integration Services], Excel files"
  - "Script task [Integration Services], examples"
  - "Excel [Integration Services]"
ms.assetid: b8fa110a-2c9c-4f5a-8fe1-305555640e44
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Working with Excel Files with the Script Task
  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides the Excel connection manager, Excel source, and Excel destination for working with data stored in spreadsheets in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel file format. The techniques described in this topic use the Script task to obtain information about available Excel databases (workbook files) and tables (worksheets and named ranges).
  
> [!IMPORTANT]
> For detailed info about connecting to Excel files, and about limitations and known issues for loading data from or to Excel files, see [Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md).
 
> [!TIP]  
>  If you want to create a task that you can reuse across multiple packages, consider using the code in this Script task sample as the starting point for a custom task. For more information, see [Developing a Custom Task](../../integration-services/extending-packages-custom-objects/task/developing-a-custom-task.md).  

##  <a name="configuring"></a> Configuring a Package to Test the Samples  
 You can configure a single package to test all the samples in this topic. The samples use many of the same package variables and the same [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] classes.  
  
### To configure a package for use with the examples in this topic  
  
1.  Create a new [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and open the default package for editing.  
  
2.  **Variables**. Open the **Variables** window and define the following variables:  
  
    -   `ExcelFile`, of type **String**. Enter the complete path and filename to an existing Excel workbook.  
  
    -   `ExcelTable`, of type **String**. Enter the name of an existing worksheet or named range in the workbook named in the value of the `ExcelFile` variable. This value is case-sensitive.  
  
    -   `ExcelFileExists`, of type **Boolean**.  
  
    -   `ExcelTableExists`, of type **Boolean**.  
  
    -   `ExcelFolder`, of type **String**. Enter the complete path of a folder that contains at least one Excel workbook.  
  
    -   `ExcelFiles`, of type **Object**.  
  
    -   `ExcelTables`, of type **Object**.  
  
3.  **Imports statements**. Most of the code samples require you to import one or both of the following [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] namespaces at the top of your script file:  
  
    -   **System.IO**, for file system operations.  
  
    -   **System.Data.OleDb**, to open Excel files as data sources.  
  
4.  **References**. The code samples that read schema information from Excel files require an additional reference in the script project to the **System.Xml** namespace.  
  
5.  Set the default scripting language for the Script component by using the **Scripting language** option on the **General** page of the **Options** dialog box. For more information, see [General Page](../general-page-of-integration-services-designers-options.md).  
  
##  <a name="example1"></a> Example 1 Description: Check Whether an Excel File Exists  
 This example determines whether the Excel workbook file specified in the `ExcelFile` variable exists, and then sets the Boolean value of the `ExcelFileExists` variable to the result. You can use this Boolean value for branching in the workflow of the package.  
  
### To configure this Script Task example  
  
1.  Add a new Script task to the package and change its name to **ExcelFileExists**.  
  
2.  In the **Script Task Editor**, on the **Script** tab, click **ReadOnlyVariables** and enter the property value using one of the following methods:  
  
    -   Type **ExcelFile**.  
  
         -or-  
  
    -   Click the ellipsis (**...**) button next to the property field, and in the **Select variables** dialog box, select the **ExcelFile** variable.  
  
3.  Click **ReadWriteVariables** and enter the property value using one of the following methods:  
  
    -   Type **ExcelFileExists**.  
  
         -or-  
  
    -   Click the ellipsis (**...**) button next to the property field, and in the **Select variables** dialog box, select the **ExcelFileExists** variable.  
  
4.  Click **Edit Script** to open the script editor.  
  
5.  Add an **Imports** statement for the **System.IO** namespace at the top of the script file.  
  
6.  Add the following code.  
  
### Example 1 Code  
  
```vb  
Public Class ScriptMain  
  Public Sub Main()  
    Dim fileToTest As String  
  
    fileToTest = Dts.Variables("ExcelFile").Value.ToString  
    If File.Exists(fileToTest) Then  
      Dts.Variables("ExcelFileExists").Value = True  
    Else  
      Dts.Variables("ExcelFileExists").Value = False  
    End If  
  
    Dts.TaskResult = ScriptResults.Success  
  End Sub  
End Class  
```  
  
```csharp  
public class ScriptMain  
{  
  public void Main()  
  {  
    string fileToTest;  
  
    fileToTest = Dts.Variables["ExcelFile"].Value.ToString();  
    if (File.Exists(fileToTest))  
    {  
      Dts.Variables["ExcelFileExists"].Value = true;  
    }  
    else  
    {  
      Dts.Variables["ExcelFileExists"].Value = false;  
    }  
  
    Dts.TaskResult = (int)ScriptResults.Success;  
  }  
}  
```  
  
##  <a name="example2"></a> Example 2 Description: Check Whether an Excel Table Exists  
 This example determines whether the Excel worksheet or named range specified in the `ExcelTable` variable exists in the Excel workbook file specified in the `ExcelFile` variable, and then sets the Boolean value of the `ExcelTableExists` variable to the result. You can use this Boolean value for branching in the workflow of the package.  
  
### To configure this Script Task example  
  
1.  Add a new Script task to the package and change its name to **ExcelTableExists**.  
  
2.  In the **Script Task Editor**, on the **Script** tab, click **ReadOnlyVariables** and enter the property value using one of the following methods:  
  
    -   Type **ExcelTable** and **ExcelFile** separated by commas**.**  
  
         -or-  
  
    -   Click the ellipsis (**...**) button next to the property field, and in the **Select variables** dialog box, select the **ExcelTable** and **ExcelFile** variables.  
  
3.  Click **ReadWriteVariables** and enter the property value using one of the following methods:  
  
    -   Type **ExcelTableExists**.  
  
         -or-  
  
    -   Click the ellipsis (**...**) button next to the property field, and in the **Select variables** dialog box, select the **ExcelTableExists** variable.  
  
4.  Click **Edit Script** to open the script editor.  
  
5.  Add a reference to the **System.Xml** assembly in the script project.  
  
6.  Add **Imports** statements for the **System.IO** and **System.Data.OleDb** namespaces at the top of the script file.  
  
7.  Add the following code.  
  
### Example 2 Code  
  
```vb  
Public Class ScriptMain  
  Public Sub Main()  
    Dim fileToTest As String  
    Dim tableToTest As String  
    Dim connectionString As String  
    Dim excelConnection As OleDbConnection  
    Dim excelTables As DataTable  
    Dim excelTable As DataRow  
    Dim currentTable As String  
  
    fileToTest = Dts.Variables("ExcelFile").Value.ToString  
    tableToTest = Dts.Variables("ExcelTable").Value.ToString  
  
    Dts.Variables("ExcelTableExists").Value = False  
    If File.Exists(fileToTest) Then  
      connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" & _  
        "Data Source=" & fileToTest & _  
        ";Extended Properties=Excel 12.0"  
      excelConnection = New OleDbConnection(connectionString)  
      excelConnection.Open()  
      excelTables = excelConnection.GetSchema("Tables")  
      For Each excelTable In excelTables.Rows  
        currentTable = excelTable.Item("TABLE_NAME").ToString  
        If currentTable = tableToTest Then  
          Dts.Variables("ExcelTableExists").Value = True  
        End If  
      Next  
    End If  
  
    Dts.TaskResult = ScriptResults.Success  
  End Sub  
End Class  
```  
  
```csharp  
public class ScriptMain  
{  
    public void Main()  
        {  
            string fileToTest;  
            string tableToTest;  
            string connectionString;  
            OleDbConnection excelConnection;  
            DataTable excelTables;  
            string currentTable;  
  
            fileToTest = Dts.Variables["ExcelFile"].Value.ToString();  
            tableToTest = Dts.Variables["ExcelTable"].Value.ToString();  
  
            Dts.Variables["ExcelTableExists"].Value = false;  
            if (File.Exists(fileToTest))  
            {  
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" +  
                "Data Source=" + fileToTest + ";Extended Properties=Excel 12.0";  
                excelConnection = new OleDbConnection(connectionString);  
                excelConnection.Open();  
                excelTables = excelConnection.GetSchema("Tables");  
                foreach (DataRow excelTable in excelTables.Rows)  
                {  
                    currentTable = excelTable["TABLE_NAME"].ToString();  
                    if (currentTable == tableToTest)  
                    {  
                        Dts.Variables["ExcelTableExists"].Value = true;  
                    }  
                }  
            }  
  
            Dts.TaskResult = (int)ScriptResults.Success;  
  
        }  
}  
```  
  
##  <a name="example3"></a> Example 3 Description: Get a List of Excel Files in a Folder  
 This example fills an array with the list of Excel files found in the folder specified in the value of the `ExcelFolder` variable, and then copies the array into the `ExcelFiles` variable. You can use the Foreach from Variable enumerator to iterate over the files in the array.  
  
### To configure this Script Task example  
  
1.  Add a new Script task to the package and change its name to **GetExcelFiles**.  
  
2.  Open the **Script Task Editor**, on the **Script** tab, click **ReadOnlyVariables** and enter the property value using one of the following methods:  
  
    -   Type **ExcelFolder**  
  
         -or-  
  
    -   Click the ellipsis (**...**) button next to the property field, and in the **Select variables** dialog box, select the ExcelFolder variable.  
  
3.  Click **ReadWriteVariables** and enter the property value using one of the following methods:  
  
    -   Type **ExcelFiles**.  
  
         -or-  
  
    -   Click the ellipsis (**...**) button next to the property field, and in the **Select variables** dialog box, select the ExcelFiles variable.  
  
4.  Click **Edit Script** to open the script editor.  
  
5.  Add an **Imports** statement for the **System.IO** namespace at the top of the script file.  
  
6.  Add the following code.  
  
### Example 3 Code  
  
```vb  
Public Class ScriptMain  
  Public Sub Main()  
    Const FILE_PATTERN As String = "*.xlsx"  
  
    Dim excelFolder As String  
    Dim excelFiles As String()  
  
    excelFolder = Dts.Variables("ExcelFolder").Value.ToString  
    excelFiles = Directory.GetFiles(excelFolder, FILE_PATTERN)  
  
    Dts.Variables("ExcelFiles").Value = excelFiles  
  
    Dts.TaskResult = ScriptResults.Success  
  End Sub  
End Class  
```  
  
```csharp  
public class ScriptMain  
{  
  public void Main()  
  {  
    const string FILE_PATTERN = "*.xlsx";  
  
    string excelFolder;  
    string[] excelFiles;  
  
    excelFolder = Dts.Variables["ExcelFolder"].Value.ToString();  
    excelFiles = Directory.GetFiles(excelFolder, FILE_PATTERN);  
  
    Dts.Variables["ExcelFiles"].Value = excelFiles;  
  
    Dts.TaskResult = (int)ScriptResults.Success;  
  }  
}  
```  
  
### Alternate Solution  
 Instead of using a Script task to gather a list of Excel files into an array, you can also use the ForEach File enumerator to iterate over all the Excel files in a folder. For more information, see [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md).  
  
##  <a name="example4"></a> Example 4 Description: Get a List of Tables in an Excel File  
 This example fills an array with the list of worksheets and named ranges found in the Excel workbook file specified by the value of the `ExcelFile` variable, and then copies the array into the `ExcelTables` variable. You can use the Foreach from Variable Enumerator to iterate over the tables in the array.  
  
> [!NOTE]  
>  The list of tables in an Excel workbook includes both worksheets (which have the $ suffix) and named ranges. If you have to filter the list for only worksheets or only named ranges, you may have to add additional code for this purpose.  
  
### To configure this Script Task example  
  
1.  Add a new Script task to the package and change its name to **GetExcelTables**.  
  
2.  Open the **Script Task Editor**, on the **Script** tab, click **ReadOnlyVariables** and enter the property value using one of the following methods:  
  
    -   Type **ExcelFile**.  
  
         -or-  
  
    -   Click the ellipsis (**...**) button next to the property field, and in the **Select variables** dialog box, select the ExcelFile variable.  
  
3.  Click **ReadWriteVariables** and enter the property value using one of the following methods:  
  
    -   Type **ExcelTables**.  
  
         -or-  
  
    -   Click the ellipsis (**...**) button next to the property field, and in the **Select variables** dialog box, select the ExcelTablesvariable.  
  
4.  Click **Edit Script** to open the script editor.  
  
5.  Add a reference to the **System.Xml** namespace in the script project.  
  
6.  Add an **Imports** statement for the **System.Data.OleDb** namespace at the top of the script file.  
  
7.  Add the following code.  
  
### Example 4 Code  
  
```vb  
Public Class ScriptMain  
  Public Sub Main()  
    Dim excelFile As String  
    Dim connectionString As String  
    Dim excelConnection As OleDbConnection  
    Dim tablesInFile As DataTable  
    Dim tableCount As Integer = 0  
    Dim tableInFile As DataRow  
    Dim currentTable As String  
    Dim tableIndex As Integer = 0  
  
    Dim excelTables As String()  
  
    excelFile = Dts.Variables("ExcelFile").Value.ToString  
    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" & _  
        "Data Source=" & excelFile & _  
        ";Extended Properties=Excel 12.0"  
    excelConnection = New OleDbConnection(connectionString)  
    excelConnection.Open()  
    tablesInFile = excelConnection.GetSchema("Tables")  
    tableCount = tablesInFile.Rows.Count  
    ReDim excelTables(tableCount - 1)  
    For Each tableInFile In tablesInFile.Rows  
      currentTable = tableInFile.Item("TABLE_NAME").ToString  
      excelTables(tableIndex) = currentTable  
      tableIndex += 1  
    Next  
  
    Dts.Variables("ExcelTables").Value = excelTables  
  
    Dts.TaskResult = ScriptResults.Success  
  End Sub  
End Class  
```  
  
```csharp  
public class ScriptMain  
{  
  public void Main()  
        {  
            string excelFile;  
            string connectionString;  
            OleDbConnection excelConnection;  
            DataTable tablesInFile;  
            int tableCount = 0;  
            string currentTable;  
            int tableIndex = 0;  
  
            string[] excelTables = new string[5];  
  
            excelFile = Dts.Variables["ExcelFile"].Value.ToString();  
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" +  
                "Data Source=" + excelFile + ";Extended Properties=Excel 12.0";  
            excelConnection = new OleDbConnection(connectionString);  
            excelConnection.Open();  
            tablesInFile = excelConnection.GetSchema("Tables");  
            tableCount = tablesInFile.Rows.Count;  
  
            foreach (DataRow tableInFile in tablesInFile.Rows)  
            {  
                currentTable = tableInFile["TABLE_NAME"].ToString();  
                excelTables[tableIndex] = currentTable;  
                tableIndex += 1;  
            }  
  
            Dts.Variables["ExcelTables"].Value = excelTables;  
  
            Dts.TaskResult = (int)ScriptResults.Success;  
        }  
}  
```  
  
### Alternate Solution  
 Instead of using a Script task to gather a list of Excel tables into an array, you can also use the ForEach ADO.NET Schema Rowset Enumerator to iterate over all the tables (that is, worksheets and named ranges) in an Excel workbook file. For more information, see [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md).  
  
##  <a name="testing"></a> Displaying the Results of the Samples  
 If you have configured each of the examples in this topic in the same package, you can connect all the Script tasks to an additional Script task that displays the output of all the examples.  
  
### To configure a Script task to display the output of the examples in this topic  
  
1.  Add a new Script task to the package and change its name to **DisplayResults**.  
  
2.  Connect each of the four example Script tasks to one another, so that each task runs after the preceding task completes successfully, and connect the fourth example task to the **DisplayResults** task.  
  
3.  Open the **DisplayResults** task in the **Script Task Editor**.  
  
4.  On the **Script** tab, click **ReadOnlyVariables** and use one of the following methods to add all seven variables listed in [Configuring a Package to Test the Samples](#configuring):  
  
    -   Type the name of each variable separated by commas.  
  
         -or-  
  
    -   Click the ellipsis (**...**) button next to the property field, and in the **Select variables** dialog box, selecting the variables.  
  
5.  Click **Edit Script** to open the script editor.  
  
6.  Add **Imports** statements for the **Microsoft.VisualBasic** and **System.Windows.Forms** namespaces at the top of the script file.  
  
7.  Add the following code.  
  
8.  Run the package and examine the results displayed in a message box.  
  
### Code to Display the Results  
  
```vb  
Public Class ScriptMain  
  Public Sub Main()  
    Const EOL As String = ControlChars.CrLf  
  
    Dim results As String  
    Dim filesInFolder As String()  
    Dim fileInFolder As String  
    Dim tablesInFile As String()  
    Dim tableInFile As String  
  
    results = _  
      "Final values of variables:" & EOL & _  
      "ExcelFile: " & Dts.Variables("ExcelFile").Value.ToString & EOL & _  
      "ExcelFileExists: " & Dts.Variables("ExcelFileExists").Value.ToString & EOL & _  
      "ExcelTable: " & Dts.Variables("ExcelTable").Value.ToString & EOL & _  
      "ExcelTableExists: " & Dts.Variables("ExcelTableExists").Value.ToString & EOL & _  
      "ExcelFolder: " & Dts.Variables("ExcelFolder").Value.ToString & EOL & _  
      EOL  
  
    results &= "Excel files in folder: " & EOL  
    filesInFolder = DirectCast(Dts.Variables("ExcelFiles").Value, String())  
    For Each fileInFolder In filesInFolder  
      results &= " " & fileInFolder & EOL  
    Next  
    results &= EOL  
  
    results &= "Excel tables in file: " & EOL  
    tablesInFile = DirectCast(Dts.Variables("ExcelTables").Value, String())  
    For Each tableInFile In tablesInFile  
      results &= " " & tableInFile & EOL  
    Next  
  
    MessageBox.Show(results, "Results", MessageBoxButtons.OK, MessageBoxIcon.Information)  
  
    Dts.TaskResult = ScriptResults.Success  
  End Sub  
End Class  
```  
  
```csharp  
public class ScriptMain  
{  
  public void Main()  
        {  
            const string EOL = "\r";  
  
            string results;  
            string[] filesInFolder;  
            //string fileInFolder;  
            string[] tablesInFile;  
            //string tableInFile;  
  
            results = "Final values of variables:" + EOL + "ExcelFile: " + Dts.Variables["ExcelFile"].Value.ToString() + EOL + "ExcelFileExists: " + Dts.Variables["ExcelFileExists"].Value.ToString() + EOL + "ExcelTable: " + Dts.Variables["ExcelTable"].Value.ToString() + EOL + "ExcelTableExists: " + Dts.Variables["ExcelTableExists"].Value.ToString() + EOL + "ExcelFolder: " + Dts.Variables["ExcelFolder"].Value.ToString() + EOL + EOL;  
  
            results += "Excel files in folder: " + EOL;  
            filesInFolder = (string[])(Dts.Variables["ExcelFiles"].Value);  
            foreach (string fileInFolder in filesInFolder)  
            {  
                results += " " + fileInFolder + EOL;  
            }  
            results += EOL;  
  
            results += "Excel tables in file: " + EOL;  
            tablesInFile = (string[])(Dts.Variables["ExcelTables"].Value);  
            foreach (string tableInFile in tablesInFile)  
            {  
                results += " " + tableInFile + EOL;  
            }  
  
            MessageBox.Show(results, "Results", MessageBoxButtons.OK, MessageBoxIcon.Information);  
  
            Dts.TaskResult = (int)ScriptResults.Success;  
        }  
}  
```  
  
## See Also  
 [Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md)  
 [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md)  
  
  

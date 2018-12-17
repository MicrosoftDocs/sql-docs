---
title: "Loading the Output of a Local Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "data flow [Integration Services], loading results"
  - "loading data flow results"
ms.assetid: aba8ecb7-0dcf-40d0-a2a8-64da0da94b93
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Loading the Output of a Local Package
  Client applications can read the output of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages when the output is saved to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destinations by using [!INCLUDE[vstecado](../../includes/vstecado-md.md)], or when the output is saved to a flat file destination by using the classes in the **System.IO** namespace. However, a client application can also read the output of a package directly from memory, without the need for an intermediate step to persist the data. The key to this solution is the `Microsoft.SqlServer.Dts.DtsClient` namespace, which contains specialized implementations of the `IDbConnection`, `IDbCommand`, and **IDbDataParameter** interfaces from the **System.Data** namespace. The assembly Microsoft.SqlServer.Dts.DtsClient.dll is installed by default in **%ProgramFiles%\Microsoft SQL Server\100\DTS\Binn**.  
  
> [!NOTE]  
>  The procedure described in this topic requires that the DelayValidation property of the Data Flow task and of any parent objects be set to its default value of **False**.  
  
## Description  
 This procedure demonstrates how to develop a client application in managed code that loads the output of a package with a DataReader destination directly from memory. The steps summarized here are demonstrated in the code sample that follows.  
  
#### To load data package output into a client application  
  
1.  In the package, configure a DataReader destination to receive the output that you want to read into the client application. Give the DataReader destination a descriptive name, since you will use this name later in your client application. Make a note of the name of the DataReader destination.  
  
2.  In the development project, set a reference to the `Microsoft.SqlServer.Dts.DtsClient` namespace by locating the assembly **Microsoft.SqlServer.Dts.DtsClient.dll**. By default, this assembly is installed in **C:\Program Files\Microsoft SQL Server\100\DTS\Binn**. Import the namespace into your code by using the C# `Using` or the [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] `Imports` statement.  
  
3.  In your code, create an object of type `DtsClient.DtsConnection` with a connection string that contains the command-line parameters required by **dtexec.exe** to run the package. For more information, see [dtexec Utility](../packages/dtexec-utility.md). Then open the connection with this connection string. You can also use the **dtexecui** utility to create the required connection string visually.  
  
    > [!NOTE]  
    >  The sample code demonstrates loading the package from the file system by using the `/FILE <path and filename>` syntax. However you can also load the package from the MSDB database by using the `/SQL <package name>` syntax, or from the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package store by using the `/DTS \<folder name>\<package name>` syntax.  
  
4.  Create an object of type `DtsClient.DtsCommand` that uses the previously created `DtsConnection` and set its `CommandText` property to the name of the DataReader destination in the package. Then call the `ExecuteReader` method of the command object to load the package results into a new DataReader.  
  
5.  Optionally, you can indirectly parameterize the output of the package by using the collection of `DtsDataParameter` objects on the `DtsCommand` object to pass values to variables defined in the package. Within the package, you can use these variables as query parameters or in expressions to affect the results returned to the DataReader destination. You must define these variables in the package in the **DtsClient** namespace before you can use them with the `DtsDataParameter` object from a client application. (You may need to click the **Choose Variable Columns** toolbar button in the **Variables** window to display the **Namespace** column.) In your client code, when you add a `DtsDataParameter` to the `Parameters` collection of the `DtsCommand`, omit the DtsClient namespace reference from the variable name. For example:  
  
    ```  
    command.Parameters.Add(new DtsDataParameter("MyVariable", 1));  
    ```  
  
6.  Call the `Read` method of the DataReader repeatedly as needed to loop through the rows of output data. Use the data, or save the data for later use, in the client application.  
  
    > [!IMPORTANT]  
    >  The `Read` method of this implementation of the DataReader returns `true` one more time after the last row of data has been read. This makes it difficult to use the usual code that loops through the DataReader while `Read` returns `true`. If your code attempts to close the DataReader or the connection after reading the expected number of rows, without an additional, final call to the `Read` method, the code will raise an unhandled exception. However, if your code attempts to read data on this final iteration through a loop, when `Read` still returns `true` but the last row has been passed, the code will raise an unhandled `ApplicationException` with the message, "The SSIS IDataReader is past the end of the resultset." This behavior is different from that of other DataReader implementations. Therefore, when using a loop to read through the rows in the DataReader while `Read` returns `true`, you need to write code to catch, test, and discard this anticipated `ApplicationException` on the last successful call to the `Read` method. Or, if you know in advance the number of rows expected, you can process the rows, and then call the `Read` method one more time before closing the DataReader and the connection.  
  
7.  Call the `Dispose` method of the `DtsCommand` object. This is particularly important if you have used any `DtsDataParameter` objects.  
  
8.  Close the DataReader and the connection objects.  
  
## Example  
 The following example runs a package that calculates a single aggregate value and saves the value to a DataReader destination, and then reads this value from the DataReader and displays the value in a text box on a Windows Form.  
  
 The use of parameters is not required when loading the output of a package into a client application. If you do not want to use a parameter, you can omit the use of the variable in the **DtsClient** namespace, and omit the code that uses the `DtsDataParameter` object.  
  
#### To create the test package  
  
1.  Create a new [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package. The sample code uses "DtsClientWParamPkg.dtsx" as the name of the package.  
  
2.  Add a variable of type String in the DtsClient namespace. The sample code use Country as the name of the variable. (You may need to click the **Choose Variable Columns** toolbar button in the **Variables** window to display the **Namespace** column.)  
  
3.  Add an OLE DB connection manager that connects to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database.  
  
4.  Add a data flow task to the package and switch to the Data Flow design surface.  
  
5.  Add an OLE DB source to the data flow and configure it to use the OLE DB connection manager created previously, and the following SQL command:  
  
    ```  
    SELECT * FROM Sales.vIndividualCustomer WHERE CountryRegionName = ?  
    ```  
  
6.  Click `Parameters` and, in the **Set Query Parameters** dialog box, map the single input parameter in the query, Parameter0, to the DtsClient::Country variable.  
  
7.  Add an Aggregate transformation to the data flow, and connect the output of the OLE DB source to the transformation. Open the Aggregate Transformation Editor and configure it to peform a "Count all" operation on all input columns (*) and to output the aggregated value with the alias CustomerCount.  
  
8.  Add a DataReader destination to the data flow and connect the output of the Aggregate transformation to the DataReader destination. The sample code uses "DataReaderDest" as the name of the DataReader. Select the single available input column, CustomerCount, for the destination.  
  
9. Save the package. The test application created next will run the package and retrieve its output directly from memory.  
  
#### To create the test application  
  
1.  Create a new Windows Forms application.  
  
2.  Add a reference to the `Microsoft.SqlServer.Dts.DtsClient` namespace by browsing to the assembly of the same name in **%ProgramFiles%\Microsoft SQL Server\100\DTS\Binn**.  
  
3.  Copy and paste the following sample code into the code module for the form.  
  
4.  Modify the value of the `dtexecArgs` variable as required so that it contains the command-line parameters required by **dtexec.exe** to run the package. The sample code loads the package from the file system.  
  
5.  Modify the value of the `dataReaderName` variable as required so that it contains the name of the DataReader destination in the package.  
  
6.  Put a button and a text box on the form. The sample code uses `btnRun` as the name of the button, and `txtResults` as the name of the text box.  
  
7.  Run the application and click the button. After a brief pause while the package runs, you should see the aggregate value calculated by the package (the count of customers in Canada) displayed in the text box on the form.  
  
### Sample Code  
  
```vb  
Imports System.Data  
Imports Microsoft.SqlServer.Dts.DtsClient  
  
Public Class Form1  
  
  Private Sub btnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click  
  
    Dim dtexecArgs As String  
    Dim dataReaderName As String  
    Dim countryName As String  
  
    Dim dtsConnection As DtsConnection  
    Dim dtsCommand As DtsCommand  
    Dim dtsDataReader As IDataReader  
    Dim dtsParameter As DtsDataParameter  
  
    Windows.Forms.Cursor.Current = Cursors.WaitCursor  
  
    dtexecArgs = "/FILE ""C:\...\DtsClientWParamPkg.dtsx"""  
    dataReaderName = "DataReaderDest"  
    countryName = "Canada"  
  
    dtsConnection = New DtsConnection()  
    With dtsConnection  
      .ConnectionString = dtexecArgs  
      .Open()  
    End With  
  
    dtsCommand = New DtsCommand(dtsConnection)  
    dtsCommand.CommandText = dataReaderName  
  
    dtsParameter = New DtsDataParameter("Country", DbType.String)  
    dtsParameter.Direction = ParameterDirection.Input  
    dtsCommand.Parameters.Add(dtsParameter)  
  
    dtsParameter.Value = countryName  
  
    dtsDataReader = dtsCommand.ExecuteReader(CommandBehavior.Default)  
  
    With dtsDataReader  
      .Read()  
      txtResults.Text = .GetInt32(0).ToString("N0")  
    End With  
  
    'After reaching the end of data rows,  
    ' call the Read method one more time.  
    Try  
      dtsDataReader.Read()  
    Catch ex As Exception  
      MessageBox.Show("Exception on final call to Read method:" & ControlChars.CrLf & _  
      ex.Message & ControlChars.CrLf & _  
      ex.InnerException.Message, "Exception on final call to Read method", _  
      MessageBoxButtons.OK, MessageBoxIcon.Error)  
    End Try  
  
    ' The following method is a best practice, and is  
    '  required when using DtsDataParameter objects.  
    dtsCommand.Dispose()  
  
    Try  
      dtsDataReader.Close()  
    Catch ex As Exception  
      MessageBox.Show("Exception closing DataReader:" & ControlChars.CrLf & _  
      ex.Message & ControlChars.CrLf & _  
      ex.InnerException.Message, "Exception closing DataReader", _  
      MessageBoxButtons.OK, MessageBoxIcon.Error)  
    End Try  
  
    Try  
      dtsConnection.Close()  
    Catch ex As Exception  
      MessageBox.Show("Exception closing connection:" & ControlChars.CrLf & _  
      ex.Message & ControlChars.CrLf & _  
      ex.InnerException.Message, "Exception closing connection", _  
      MessageBoxButtons.OK, MessageBoxIcon.Error)  
    End Try  
  
    Windows.Forms.Cursor.Current = Cursors.Default  
  
  End Sub  
  
End Class  
```  
  
```csharp  
using System;  
using System.Windows.Forms;  
using System.Data;  
using Microsoft.SqlServer.Dts.DtsClient;  
  
namespace DtsClientWParamCS  
{  
  public partial class Form1 : Form  
  {  
    public Form1()  
    {  
      InitializeComponent();  
      this.btnRun.Click += new System.EventHandler(this.btnRun_Click);  
    }  
  
    private void btnRun_Click(object sender, EventArgs e)  
    {  
      string dtexecArgs;  
      string dataReaderName;  
      string countryName;  
  
      DtsConnection dtsConnection;  
      DtsCommand dtsCommand;  
      IDataReader dtsDataReader;  
      DtsDataParameter dtsParameter;  
  
      Cursor.Current = Cursors.WaitCursor;  
  
      dtexecArgs = @"/FILE ""C:\...\DtsClientWParamPkg.dtsx""";  
      dataReaderName = "DataReaderDest";  
      countryName = "Canada";  
  
      dtsConnection = new DtsConnection();  
      {  
        dtsConnection.ConnectionString = dtexecArgs;  
        dtsConnection.Open();  
      }  
  
      dtsCommand = new DtsCommand(dtsConnection);  
      dtsCommand.CommandText = dataReaderName;  
  
      dtsParameter = new DtsDataParameter("Country", DbType.String);  
      dtsParameter.Direction = ParameterDirection.Input;  
      dtsCommand.Parameters.Add(dtsParameter);  
  
      dtsParameter.Value = countryName;  
  
      dtsDataReader = dtsCommand.ExecuteReader(CommandBehavior.Default);  
  
      {  
        dtsDataReader.Read();  
        txtResults.Text = dtsDataReader.GetInt32(0).ToString("N0");  
      }  
  
      //After reaching the end of data rows,  
      // call the Read method one more time.  
      try  
      {  
        dtsDataReader.Read();  
      }  
      catch (Exception ex)  
      {  
        MessageBox.Show(  
          "Exception on final call to Read method:\n" + ex.Message + "\n" + ex.InnerException.Message,  
          "Exception on final call to Read method", MessageBoxButtons.OK, MessageBoxIcon.Error);  
      }  
  
      // The following method is a best practice, and is  
      //  required when using DtsDataParameter objects.  
      dtsCommand.Dispose();  
  
      try  
      {  
        dtsDataReader.Close();  
      }  
      catch (Exception ex)  
      {  
        MessageBox.Show(  
          "Exception closing DataReader:\n" + ex.Message + "\n" + ex.InnerException.Message,  
          "Exception closing DataReader", MessageBoxButtons.OK, MessageBoxIcon.Error);  
      }  
  
      try  
      {  
        dtsConnection.Close();  
      }  
      catch (Exception ex)  
      {  
        MessageBox.Show(  
          "Exception closing connection:\n" + ex.Message + "\n" + ex.InnerException.Message,  
          "Exception closing connection", MessageBoxButtons.OK, MessageBoxIcon.Error);  
      }  
  
      Cursor.Current = Cursors.Default;  
  
    }  
  }  
}  
```  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Understanding the Differences between Local and Remote Execution](../run-manage-packages-programmatically/understanding-the-differences-between-local-and-remote-execution.md)   
 [Loading and Running a Local Package Programmatically](../run-manage-packages-programmatically/loading-and-running-a-local-package-programmatically.md)   
 [Loading and Running a Remote Package Programmatically](../run-manage-packages-programmatically/loading-and-running-a-remote-package-programmatically.md)  
  
  

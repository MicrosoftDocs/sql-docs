---
title: "Creating a Destination with the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Script component [Integration Services], destination components"
  - "destinations [Integration Services], components"
  - "input columns [Integration Services]"
ms.assetid: 214e22e8-7e7d-4876-b690-c138e5721b81
author: janinezhang
ms.author: janinez
manager: craigg
---
# Creating a Destination with the Script Component
  You use a destination component in the data flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package to save data received from upstream sources and transformations to a data source. Ordinarily the destination component connects to the data source through an existing connection manager.  
  
 For an overview of the Script component, see [Extending the Data Flow with the Script Component](../extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md.  
  
 The Script component and the infrastructure code that it generates for you simplify significantly the process of developing a custom data flow component. However, to understand how the Script component works, you may find it useful to read through the steps for developing a custom data flow components in the [Developing a Custom Data Flow Component](../extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md) section, and especially [Developing a Custom Destination Component](../extending-packages-custom-objects-data-flow-types/developing-a-custom-destination-component.md).  
  
## Getting Started with a Destination Component  
 When you add a Script component to the Data Flow tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, the **Select Script Component Type** dialog box opens and prompts you to select a **Source**, **Destination**, or **Transformation** script. In this dialog box, select **Destination**.  
  
 Next, connect the output of a transformation to the destination component in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. For testing, you can connect a source directly to a destination without any transformations.  
  
## Configuring a Destination Component in Metadata-Design Mode  
 After you select the option to create a destination component, you configure the component by using the **Script Transformation Editor**. For more information, see [Configuring the Script Component in the Script Component Editor](../extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md).  
  
 To select the script language that the Script destination will use, you set the **ScriptLanguage** property on the **Script** page of the **Script Transformation Editor** dialog box.  
  
> [!NOTE]  
>  To set the default scripting language for the Script component, use the **Scripting language** option on the **General** page of the **Options** dialog box. For more information, see [General Page](../general-page-of-integration-services-designers-options.md).  
  
 A data flow destination component has one input and no outputs. Configuring the input for the component is one of the steps that you must complete in metadata design mode, by using the **Script Transformation Editor**, before you write your custom script.  
  
### Adding Connection Managers  
 Ordinarily a destination component uses an existing connection manager to connect to the data source to which it saves data from the data flow. On the **Connection Managers** page of the **Script Transformation Editor**, click **Add** to add the appropriate connection manager.  
  
 However, a connection manager is just a convenient unit that encapsulates and stores the information that is required to connect to a data source of a particular type. You must write your own custom code to load or save your data, and possibly to open and close the connection to the data source.  
  
 For general information about how to use connection managers with the Script component, see [Connecting to Data Sources in the Script Component](../extending-packages-scripting/data-flow-script-component/connecting-to-data-sources-in-the-script-component.md).  
  
 For more information about the **Connection Managers** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Connection Managers Page&#41;](../script-transformation-editor-connection-managers-page.md).  
  
### Configuring Inputs and Input Columns  
 A destination component has one input and no outputs.  
  
 On the **Input Columns** page of the **Script Transformation Editor**, the column list shows the available columns from the output of the upstream component in the data flow. Select the columns that you want to save.  
  
 For more information about the **Input Columns** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Input Columns Page&#41;](../script-transformation-editor-input-columns-page.md).  
  
 The **Inputs and Outputs** page of the **Script Transformation Editor** shows a single input, which you can rename. You will refer to the input by its name in your script by using the accessor property created in the auto-generated code.  
  
 For more information about the **Inputs and Outputs** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Inputs and Outputs Page&#41;](../script-transformation-editor-inputs-and-outputs-page.md).  
  
### Adding Variables  
 If you want to use existing variables in your script, you can add them in the `ReadOnlyVariables` and `ReadWriteVariables` property fields on the **Script** page of the **Script Transformation Editor**.  
  
 When you add multiple variables in the property fields, separate the variable names by commas. You can also select multiple variables by clicking the ellipsis (**...**) button next to the `ReadOnlyVariables` and `ReadWriteVariables` property fields, and then selecting the variables in the **Select variables** dialog box.  
  
 For general information about how to use variables with the Script component, see [Using Variables in the Script Component](../extending-packages-scripting/data-flow-script-component/using-variables-in-the-script-component.md).  
  
 For more information about the **Script** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Script Page&#41;](../script-transformation-editor-script-page.md).  
  
## Scripting a Destination Component in Code-Design Mode  
 After you have configured the metadata for your component, you can write your custom script. In the **Script Transformation Editor**, on the **Script** page, click **Edit Script** to open the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA) IDE where you can add your custom script. The scripting language that you use depends on whether you selected [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C# as the script language for the **ScriptLanguage** property on the **Script** page.  
  
 For important information that applies to all kinds of components created by using the Script component, see [Coding and Debugging the Script Component](../extending-packages-scripting/data-flow-script-component/coding-and-debugging-the-script-component.md).  
  
### Understanding the Auto-generated Code  
 When you open the VSTA IDE after you create and configuring a destination component, the editable `ScriptMain` class appears in the code editor with a stub for the `ProcessInputRow` method. The `ScriptMain` class is where you will write your custom code, and `ProcessInputRow` is the most important method in a destination component.  
  
 If you open the **Project Explorer** window in VSTA, you can see that the Script component has also generated read-only `BufferWrapper` and `ComponentWrapper` project items. The `ScriptMain` class inherits from `UserComponent` class in the `ComponentWrapper` project item.  
  
 At run time, the data flow engine invokes the `ProcessInput` method in the `UserComponent` class, which overrides the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.ProcessInput%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> parent class. The `ProcessInput` method in turn loops through the rows in the input buffer and calls the `ProcessInputRow` method one time for each row.  
  
### Writing Your Custom Code  
 To finish creating a custom destination component, you may want to write script in the following methods available in the `ScriptMain` class.  
  
1.  Override the `AcquireConnections` method to connect to the external data source. Extract the connection object, or the required connection information, from the connection manager.  
  
2.  Override the `PreExecute` method to prepare to save the data. For example, you may want to create and configure a `SqlCommand` and its parameters in this method.  
  
3.  Use the overridden `ProcessInputRow` method to copy each input row to the external data source. For example, for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination, you can copy the column values into the parameters of a `SqlCommand` and execute the command one time for each row. For a flat file destination, you can write the values for each column to a `StreamWriter`, separating the values by the column delimiter.  
  
4.  Override the `PostExecute` method to disconnect from the external data source, if required, and to perform any other required cleanup.  
  
## Examples  
 The examples that follow demonstrate code that is required in the `ScriptMain` class to create a destination component.  
  
> [!NOTE]
>  These examples use the **Person.Address** table in the `AdventureWorks` sample database and pass its first and fourth columns, the **int*AddressID*** and **nvarchar(30)City** columns, through the data flow. The same data is used in the source, transformation, and destination samples in this section. Additional prerequisites and assumptions are documented for each example.  
  
### ADO.NET Destination Example  
 This example demonstrates a destination component that uses an existing [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to save data from the data flow into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 If you want to run this sample code, you must configure the package and the component as follows:  
  
1.  Create an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that uses the `SqlClient` provider to connect to the `AdventureWorks` database.  
  
2.  Create a destination table by running the following [!INCLUDE[tsql](../../includes/tsql-md.md)] command in the `AdventureWorks` database:  
  
    ```  
    CREATE TABLE [Person].[Address2]([AddressID] [int] NOT NULL,  
        [City] [nvarchar](30) NOT NULL)  
    ```  
  
3.  Add a new Script component to the Data Flow designer surface and configure it as a destination.  
  
4.  Connect the output of an upstream source or transformation to the destination component in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. (You can connect a source directly to a destination without any transformations.) This output should provide data from the **Person.Address** table of the `AdventureWorks` sample database that contains at least the **AddressID** and **City** columns.  
  
5.  Open the **Script Transformation Editor**. On the **Input Columns** page, select the **AddressID** and **City** input columns.  
  
6.  On the **Inputs and Outputs** page, rename the input with a more descriptive name such as **MyAddressInput**.  
  
7.  On the **Connection Managers** page, add or create the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager with a name such as **MyADONETConnectionManager**.  
  
8.  On the **Script** page, click **Edit Script** and enter the script that follows. Then close the script development environment.  
  
9. Close the **Script Transformation Editor** and run the sample.  
  
```vb  
Imports System.Data.SqlClient  
...  
Public Class ScriptMain  
    Inherits UserComponent  
  
    Dim connMgr As IDTSConnectionManager100  
    Dim sqlConn As SqlConnection  
    Dim sqlCmd As SqlCommand  
    Dim sqlParam As SqlParameter  
  
    Public Overrides Sub AcquireConnections(ByVal Transaction As Object)  
  
        connMgr = Me.Connections.MyADONETConnectionManager  
        sqlConn = CType(connMgr.AcquireConnection(Nothing), SqlConnection)  
  
    End Sub  
  
    Public Overrides Sub PreExecute()  
  
        sqlCmd = New SqlCommand("INSERT INTO Person.Address2(AddressID, City) " & _  
            "VALUES(@addressid, @city)", sqlConn)  
        sqlParam = New SqlParameter("@addressid", SqlDbType.Int)  
        sqlCmd.Parameters.Add(sqlParam)  
        sqlParam = New SqlParameter("@city", SqlDbType.NVarChar, 30)  
        sqlCmd.Parameters.Add(sqlParam)  
  
    End Sub  
  
    Public Overrides Sub MyAddressInput_ProcessInputRow(ByVal Row As MyAddressInputBuffer)  
        With sqlCmd  
            .Parameters("@addressid").Value = Row.AddressID  
            .Parameters("@city").Value = Row.City  
            .ExecuteNonQuery()  
        End With  
    End Sub  
  
    Public Overrides Sub ReleaseConnections()  
  
        connMgr.ReleaseConnection(sqlConn)  
  
    End Sub  
  
End Class  
```  
  
```csharp  
using System.Data.SqlClient;  
public class ScriptMain:  
    UserComponent  
  
{  
    IDTSConnectionManager100 connMgr;  
    SqlConnection sqlConn;  
    SqlCommand sqlCmd;  
    SqlParameter sqlParam;  
  
    public override void AcquireConnections(object Transaction)  
    {  
  
        connMgr = this.Connections.MyADONETConnectionManager;  
        sqlConn = (SqlConnection)connMgr.AcquireConnection(null);  
  
    }  
  
    public override void PreExecute()  
    {  
  
        sqlCmd = new SqlCommand("INSERT INTO Person.Address2(AddressID, City) " +  
            "VALUES(@addressid, @city)", sqlConn);  
        sqlParam = new SqlParameter("@addressid", SqlDbType.Int);  
        sqlCmd.Parameters.Add(sqlParam);  
        sqlParam = new SqlParameter("@city", SqlDbType.NVarChar, 30);  
        sqlCmd.Parameters.Add(sqlParam);  
  
    }  
  
    public override void MyAddressInput_ProcessInputRow(MyAddressInputBuffer Row)  
    {  
        {  
            sqlCmd.Parameters["@addressid"].Value = Row.AddressID;  
            sqlCmd.Parameters["@city"].Value = Row.City;  
            sqlCmd.ExecuteNonQuery();  
        }  
    }  
  
    public override void ReleaseConnections()  
    {  
  
        connMgr.ReleaseConnection(sqlConn);  
  
    }  
  
}  
```  
  
### Flat File Destination Example  
 This example demonstrates a destination component that uses an existing Flat File connection manager to save data from the data flow to a flat file.  
  
 If you want to run this sample code, you must configure the package and the component as follows:  
  
1.  Create a Flat File connection manager that connects to a destination file. The file does not have to exist; the destination component will create it. Configure the destination file as a comma-delimited file that contains the **AddressID** and **City** columns.  
  
2.  Add a new Script component to the Data Flow designer surface and configure it as a destination.  
  
3.  Connect the output of an upstream source or transformation to the destination component in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. (You can connect a source directly to a destination without any transformations.) This output should provide data from the **Person.Address** table of the `AdventureWorks` sample database, and should contain at least the **AddressID** and **City** columns.  
  
4.  Open the **Script Transformation Editor**. On the **Input Columns** page, select the **AddressID** and **City** columns.  
  
5.  On the **Inputs and Outputs** page, rename the input with a more descriptive name, such as **MyAddressInput**.  
  
6.  On the **Connection Managers** page, add or create the Flat File connection manager with a descriptive name such as **MyFlatFileDestConnectionManager**.  
  
7.  On the **Script** page, click **Edit Script** and enter the script that follows. Then close the script development environment.  
  
8.  Close the **Script Transformation Editor** and run the sample.  
  
```vb  
Imports System.IO  
...  
Public Class ScriptMain  
    Inherits UserComponent  
  
    Dim copiedAddressFile As String  
    Private textWriter As StreamWriter  
    Private columnDelimiter As String = ","  
  
    Public Overrides Sub AcquireConnections(ByVal Transaction As Object)  
  
        Dim connMgr As IDTSConnectionManager100 = _  
            Me.Connections.MyFlatFileDestConnectionManager  
        copiedAddressFile = CType(connMgr.AcquireConnection(Nothing), String)  
  
    End Sub  
  
    Public Overrides Sub PreExecute()  
  
        textWriter = New StreamWriter(copiedAddressFile, False)  
  
    End Sub  
  
    Public Overrides Sub MyAddressInput_ProcessInputRow(ByVal Row As MyAddressInputBuffer)  
  
        With textWriter  
            If Not Row.AddressID_IsNull Then  
                .Write(Row.AddressID)  
            End If  
            .Write(columnDelimiter)  
            If Not Row.City_IsNull Then  
                .Write(Row.City)  
            End If  
            .WriteLine()  
        End With  
  
    End Sub  
  
    Public Overrides Sub PostExecute()  
  
        textWriter.Close()  
  
    End Sub  
  
End Class  
```  
  
```csharp  
using System.IO;  
public class ScriptMain:  
    UserComponent  
  
{  
    string copiedAddressFile;  
    private StreamWriter textWriter;  
    private string columnDelimiter = ",";  
  
    public override void AcquireConnections(object Transaction)  
    {  
  
        IDTSConnectionManager100 connMgr = this.Connections.MyFlatFileDestConnectionManager;  
        copiedAddressFile = (string) connMgr.AcquireConnection(null);  
  
    }  
  
    public override void PreExecute()  
    {  
  
        textWriter = new StreamWriter(copiedAddressFile, false);  
  
    }  
  
    public override void MyAddressInput_ProcessInputRow(MyAddressInputBuffer Row)  
    {  
  
        {  
            if (!Row.AddressID_IsNull)  
            {  
                textWriter.Write(Row.AddressID);  
            }  
            textWriter.Write(columnDelimiter);  
            if (!Row.City_IsNull)  
            {  
                textWriter.Write(Row.City);  
            }  
            textWriter.WriteLine();  
        }  
  
    }  
  
    public override void PostExecute()  
    {  
  
        textWriter.Close();  
  
    }  
  
}  
```  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Creating a Source with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-a-source-with-the-script-component.md)   
 [Developing a Custom Destination Component](../extending-packages-custom-objects-data-flow-types/developing-a-custom-destination-component.md)  
  
  

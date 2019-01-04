---
title: "Creating a Source with the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Script component [Integration Services], source components"
  - "output columns [Integration Services]"
  - "sources [Integration Services], components"
ms.assetid: 547c4179-ea82-4265-8c6f-04a2aa77a3c0
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Creating a Source with the Script Component
  You use a source component in the data flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package to load data from a data source to pass on to downstream transformations and destinations. Ordinarily you connect to the data source through an existing connection manager.  
  
 For an overview of the Script component, see [Extending the Data Flow with the Script Component](../extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md.  
  
 The Script component and the infrastructure code that it generates for you simplify significantly the process of developing a custom data flow component. However, to understand how the Script component works, you may find it useful to read through the steps that are involved in developing a custom data flow component. See the section [Developing a Custom Data Flow Component](../extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md), especially the topic [Developing a Custom Source Component](../extending-packages-custom-objects-data-flow-types/developing-a-custom-source-component.md).  
  
## Getting Started with a Source Component  
 When you add a Script component to the Data Flow pane of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, the **Select Script Component Type** dialog box opens and prompts you to select a Source, Destination, or Transformation script. In this dialog box, select **Source**.  
  
## Configuring a Source Component in Metadata-Design Mode  
 After selecting to create a source component, you configure the component by using the **Script Transformation Editor**. For more information, see [Configuring the Script Component in the Script Component Editor](../extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md).  
  
 A data flow source component has no inputs and supports one or more outputs. Configuring the outputs for the component is one of the steps that you must complete in metadata design mode, by using the **Script Transformation Editor**, before you write your custom script.  
  
 You can also specify the script language by setting the **ScriptLanguage** property on the **Script** page of the **Script Transformation Editor**.  
  
> [!NOTE]  
>  To set the default script language for Script components and Script Tasks, use the **Scripting language** option on the **General** page of the **Options** dialog box. For more information, see [General Page](../general-page-of-integration-services-designers-options.md).  
  
### Adding Connection Managers  
 Ordinarily a source component uses an existing connection manager to connect to the data source from which it loads data into the data flow. On the **Connection Managers** page of the **Script Transformation Editor**, click **Add** to add the appropriate connection manager.  
  
 However, a connection manager is only a convenient unit that encapsulates and stores the information that it must have to connect to a data source of a particular type. You must write your own custom code to load or save your data, and possibly to open and close the connection to the data source also.  
  
 For general information about how to use connection managers with the Script component, see [Connecting to Data Sources in the Script Component](../extending-packages-scripting/data-flow-script-component/connecting-to-data-sources-in-the-script-component.md).  
  
 For more information about the **Connection Managers** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Connection Managers Page&#41;](../script-transformation-editor-connection-managers-page.md).  
  
### Configuring Outputs and Output Columns  
 A source component has no inputs and supports one or more outputs. On the **Inputs and Outputs** page of the **Script Transformation Editor**, a single output has been created by default, but no output columns have been created. On this page of the editor, you may need or want to configure the following items.  
  
-   You must add and configure output columns manually for each output. Select the Output Columns folder for each output, and then use the **Add Column** and **Remove Column** buttons to manage the output columns for each output of the source component. Later, you will refer to the output columns in your script by the names that you assign here, by using the typed accessor properties created for you in the auto-generated code.  
  
-   You may want to create one or more additional outputs, such as a simulated error output for rows that contain unexpected values. Use the **Add Output** and **Remove Output** buttons to manage the outputs of the source component. All input rows are directed to all available outputs unless you also specify an identical non-zero value for the `ExclusionGroup` property of those outputs where you intend to direct each row to only one of the outputs that share the same `ExclusionGroup` value. The particular integer value selected to identify the `ExclusionGroup` is not significant.  
  
    > [!NOTE]  
    >  You can also use a non-zero `ExclusionGroup` property value with a single output when you do not want to output all rows. In this case, however, you must explicitly call the **DirectRowTo\<outputbuffer>** method for each row that you want to send to the output.  
  
-   You may want to assign a friendly name to the outputs. Later, you will refer to the outputs by their names in your script, by using the typed accessor properties created for you in the auto-generated code.  
  
-   Ordinarily multiple outputs in the same `ExclusionGroup` have the same output columns. However, if you are creating a simulated error output, you may want to add more columns to store error information. For information about how the data flow engine processes error rows, see [Using Error Outputs in a Data Flow Component](../extending-packages-custom-objects/data-flow/using-error-outputs-in-a-data-flow-component.md). In the Script component, however, you must write your own code to fill the additional columns with appropriate error information. For more information, see [Simulating an Error Output for the Script Component](../extending-packages-scripting-data-flow-script-component-examples/simulating-an-error-output-for-the-script-component.md).  
  
 For more information about the **Inputs and Outputs** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Inputs and Outputs Page&#41;](../script-transformation-editor-inputs-and-outputs-page.md).  
  
### Adding Variables  
 If there are any existing variables whose values you want to use in your script, you can add them in the `ReadOnlyVariables` and `ReadWriteVariables` property fields on the **Script** page of the **Script Transformation Editor**.  
  
 When you enter multiple variables in the property fields, separate the variable names by commas. You can also enter multiple variables by clicking the ellipsis (**...**) button next to the `ReadOnlyVariables` and `ReadWriteVariables` property fields and selecting variables in the **Select variables** dialog box.  
  
 For general information about how to use variables with the Script component, see [Using Variables in the Script Component](../extending-packages-scripting/data-flow-script-component/using-variables-in-the-script-component.md).  
  
 For more information about the **Script** page of the **Script Transformation Editor**, see [Script Transformation Editor &#40;Script Page&#41;](../script-transformation-editor-script-page.md).  
  
## Scripting a Source Component in Code-Design Mode  
 After you have configured the metadata for your component, open the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA) IDE to code your custom script. To open VSTA, click **Edit Script** on the **Script** page of the **Script Transformation Editor**. You can write your script by using either [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C#, depending on the script language selected for the **ScriptLanguage** property.  
  
 For important information that applies to all kinds of components created by using the Script component, see [Coding and Debugging the Script Component](../extending-packages-scripting/data-flow-script-component/coding-and-debugging-the-script-component.md).  
  
### Understanding the Auto-generated Code  
 When you open the VSTA IDE after creating and configuring a source component, the editable `ScriptMain` class appears in the code editor. You write your custom code in the `ScriptMain` class.  
  
 The `ScriptMain` class includes a stub for the `CreateNewOutputRows` method. The `CreateNewOutputRows` is the most important method in a source component.  
  
 If you open the **Project Explorer** window in VSTA, you can see that the Script component has also generated read-only `BufferWrapper` and `ComponentWrapper` project items. The `ScriptMain` class inherits from `UserComponent` class in the `ComponentWrapper` project item.  
  
 At run time, the data flow engine invokes the `PrimeOutput` method in the `UserComponent` class, which overrides the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponentHost.PrimeOutput%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> parent class. The `PrimeOutput` method in turn calls the following methods:  
  
1.  The `CreateNewOutputRows` method, which you override in `ScriptMain` to add rows from the data source to the output buffers, which are empty at first.  
  
2.  The `FinishOutputs` method, which is empty by default. Override this method in `ScriptMain` to perform any processing that is required to complete the output.  
  
3.  The private `MarkOutputsAsFinished` method, which calls the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptBuffer.SetEndOfRowset%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptBuffer> parent class to indicate to the data flow engine that the output is finished. You do not have to call `SetEndOfRowset` explicitly in your own code.  
  
### Writing Your Custom Code  
 To finish creating a custom source component, you may want to write script in the following methods available in the `ScriptMain` class.  
  
1.  Override the `AcquireConnections` method to connect to the external data source. Extract the connection object, or the required connection information, from the connection manager.  
  
2.  Override the `PreExecute` method to load data, if you can load all the source data at the same time. For example, you can execute a `SqlCommand` against an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and load all the source data at the same time into a `SqlDataReader`. If you must load the source data one row at a time (for example, when reading a text file), you can load the data as you loop through rows in `CreateNewOutputRows`.  
  
3.  Use the overridden `CreateNewOutputRows` method to add new rows to the empty output buffers and to fill in the values of each column in the new output rows. Use the `AddRow` method of each output buffer to add an empty new row, and then set the values of each column. Typically you copy values from the columns loaded from the external source.  
  
4.  Override the `PostExecute` method to finish processing the data. For example, you can close the `SqlDataReader` that you used to load data.  
  
5.  Override the `ReleaseConnections` method to disconnect from the external data source, if required.  
  
## Examples  
 The following examples demonstrate the custom code that is required in the `ScriptMain` class to create a source component.  
  
> [!NOTE]  
>  These examples use the **Person.Address** table in the `AdventureWorks` sample database and pass its first and fourth columns, the **intAddressID** and **nvarchar(30)City** columns, through the data flow. The same data is used in the source, transformation, and destination samples in this section. Additional prerequisites and assumptions are documented for each example.  
  
### ADO.NET Source Example  
 This example demonstrates a source component that uses an existing [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to load data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table into the data flow.  
  
 If you want to run this sample code, you must configure the package and the component as follows:  
  
1.  Create an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that uses the `SqlClient` provider to connect to the `AdventureWorks` database.  
  
2.  Add a new Script component to the Data Flow designer surface and configure it as a source.  
  
3.  Open the **Script Transformation Editor**. On the **Inputs and Outputs** page, rename the default output with a more descriptive name such as **MyAddressOutput**, and add and configure the two output columns, **AddressID** and **City**.  
  
    > [!NOTE]  
    >  Be sure to change the data type of the **City** output column to DT_WSTR.  
  
4.  On the **Connection Managers** page, add or create the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager and give it a name such as **MyADONETConnection**.  
  
5.  On the **Script** page, click **Edit Script** and enter the script that follows. Then close the script development environment and the **Script Transformation Editor**.  
  
6.  Create and configure a destination component, such as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination, or the sample destination component demonstrated in [Creating a Destination with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md), that expects the **AddressID** and **City** columns. Then connect the source component to the destination. (You can connect a source directly to a destination without any transformations.) You can create a destination table by running the following [!INCLUDE[tsql](../../includes/tsql-md.md)] command in the `AdventureWorks` database:  
  
    ```  
    CREATE TABLE [Person].[Address2]([AddressID] [int] NOT NULL,  
        [City] [nvarchar](30) NOT NULL)  
    ```  
  
7.  Run the sample.  
  
    ```vb  
    Imports System.Data.SqlClient  
    ...  
    Public Class ScriptMain  
        Inherits UserComponent  
  
        Dim connMgr As IDTSConnectionManager100  
        Dim sqlConn As SqlConnection  
        Dim sqlReader As SqlDataReader  
  
        Public Overrides Sub AcquireConnections(ByVal Transaction As Object)  
  
            connMgr = Me.Connections.MyADONETConnection  
            sqlConn = CType(connMgr.AcquireConnection(Nothing), SqlConnection)  
  
        End Sub  
  
        Public Overrides Sub PreExecute()  
  
            Dim cmd As New SqlCommand("SELECT AddressID, City, StateProvinceID FROM Person.Address", sqlConn)  
            sqlReader = cmd.ExecuteReader  
  
        End Sub  
  
        Public Overrides Sub CreateNewOutputRows()  
  
            Do While sqlReader.Read  
                With MyAddressOutputBuffer  
                    .AddRow()  
                    .AddressID = sqlReader.GetInt32(0)  
                    .City = sqlReader.GetString(1)  
                End With  
            Loop  
  
        End Sub  
  
        Public Overrides Sub PostExecute()  
  
            sqlReader.Close()  
  
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
        SqlDataReader sqlReader;  
  
        public override void AcquireConnections(object Transaction)  
        {  
            connMgr = this.Connections.MyADONETConnection;  
            sqlConn = (SqlConnection)connMgr.AcquireConnection(null);  
  
        }  
  
        public override void PreExecute()  
        {  
  
            SqlCommand cmd = new SqlCommand("SELECT AddressID, City, StateProvinceID FROM Person.Address", sqlConn);  
            sqlReader = cmd.ExecuteReader();  
  
        }  
  
        public override void CreateNewOutputRows()  
        {  
  
            while (sqlReader.Read())  
            {  
                {  
                    MyAddressOutputBuffer.AddRow();  
                    MyAddressOutputBuffer.AddressID = sqlReader.GetInt32(0);  
                    MyAddressOutputBuffer.City = sqlReader.GetString(1);  
                }  
            }  
  
        }  
  
        public override void PostExecute()  
        {  
  
            sqlReader.Close();  
  
        }  
  
        public override void ReleaseConnections()  
        {  
  
            connMgr.ReleaseConnection(sqlConn);  
  
        }  
  
    }  
    ```  
  
### Flat File Source Example  
 This example demonstrates a source component that uses an existing Flat File connection manager to load data from a flat file into the data flow. The flat file source data is created by exporting it from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 If you want to run this sample code, you must configure the package and the component as follows:  
  
1.  Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard to export the **Person.Address** table from the `AdventureWorks` sample database to a comma-delimited flat file. This sample uses the file name ExportedAddresses.txt.  
  
2.  Create a Flat File connection manager that connects to the exported data file.  
  
3.  Add a new Script component to the Data Flow designer surface and configure it as a source.  
  
4.  Open the **Script Transformation Editor**. On the **Inputs and Outputs** page, rename the default output with a more descriptive name such as **MyAddressOutput**. Add and configure the two output columns, **AddressID** and **City**.  
  
5.  On the **Connection Managers** page, add or create the Flat File connection manager, using a descriptive name such as **MyFlatFileSrcConnectionManager**.  
  
6.  On the **Script** page, click **Edit Script** and enter the script that follows. Then close the script development environment and the **Script Transformation Editor**.  
  
7.  Create and configure a destination component, such as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination, or the sample destination component demonstrated in [Creating a Destination with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md). Then connect the source component to the destination. (You can connect a source directly to a destination without any transformations.) You can create a destination table by running the following [!INCLUDE[tsql](../../includes/tsql-md.md)] command in the `AdventureWorks` database:  
  
    ```  
    CREATE TABLE [Person].[Address2]([AddressID] [int] NOT NULL,  
        [City] [nvarchar](30) NOT NULL)  
    ```  
  
8.  Run the sample.  
  
    ```vb  
    Imports System.IO  
    ...  
    Public Class ScriptMain  
        Inherits UserComponent  
  
        Private textReader As StreamReader  
        Private exportedAddressFile As String  
  
        Public Overrides Sub AcquireConnections(ByVal Transaction As Object)  
  
            Dim connMgr As IDTSConnectionManager100 = _  
                Me.Connections.MyFlatFileSrcConnectionManager  
            exportedAddressFile = _  
                CType(connMgr.AcquireConnection(Nothing), String)  
  
        End Sub  
  
        Public Overrides Sub PreExecute()  
            MyBase.PreExecute()  
            textReader = New StreamReader(exportedAddressFile)  
        End Sub  
  
        Public Overrides Sub CreateNewOutputRows()  
  
            Dim nextLine As String  
            Dim columns As String()  
  
            Dim delimiters As Char()  
            delimiters = ",".ToCharArray  
  
            nextLine = textReader.ReadLine  
            Do While nextLine IsNot Nothing  
                columns = nextLine.Split(delimiters)  
                With MyAddressOutputBuffer  
                    .AddRow()  
                    .AddressID = columns(0)  
                    .City = columns(3)  
                End With  
                nextLine = textReader.ReadLine  
            Loop  
  
        End Sub  
  
        Public Overrides Sub PostExecute()  
            MyBase.PostExecute()  
            textReader.Close()  
  
        End Sub  
  
    End Class  
    ```  
  
    ```csharp  
    using System.IO;  
    public class ScriptMain:  
        UserComponent  
  
    {  
        private StreamReader textReader;  
        private string exportedAddressFile;  
  
        public override void AcquireConnections(object Transaction)  
        {  
  
            IDTSConnectionManager100 connMgr = this.Connections.MyFlatFileSrcConnectionManager;  
            exportedAddressFile = (string)connMgr.AcquireConnection(null);  
  
        }  
  
        public override void PreExecute()  
        {  
            base.PreExecute();  
            textReader = new StreamReader(exportedAddressFile);  
        }  
  
        public override void CreateNewOutputRows()  
        {  
  
            string nextLine;  
            string[] columns;  
  
            char[] delimiters;  
            delimiters = ",".ToCharArray();  
  
            nextLine = textReader.ReadLine();  
            while (nextLine != null)  
            {  
                columns = nextLine.Split(delimiters);  
                {  
                    MyAddressOutputBuffer.AddRow();  
                    MyAddressOutputBuffer.AddressID = columns[0];  
                    MyAddressOutputBuffer.City = columns[3];  
                }  
                nextLine = textReader.ReadLine();  
            }  
  
        }  
  
        public override void PostExecute()  
        {  
  
            base.PostExecute();  
            textReader.Close();  
  
        }  
  
    }  
    ```  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Creating a Destination with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md)   
 [Developing a Custom Source Component](../extending-packages-custom-objects-data-flow-types/developing-a-custom-source-component.md)  
  
  

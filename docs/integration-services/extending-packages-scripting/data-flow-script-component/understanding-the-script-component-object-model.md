---
title: "Understanding the Script Component Object Model | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Script component [Integration Services], object model"
ms.assetid: 2a0aae82-39cc-4423-b09a-72d2f61033bd
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Understanding the Script Component Object Model
  As discussed in [Coding and Debugging the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/coding-and-debugging-the-script-component.md), the Script component project contains three project items:  
  
1.  The **ScriptMain** item, which contains the **ScriptMain** class in which you write your code. The **ScriptMain** class inherits from the **UserComponent** class.  
  
2.  The **ComponentWrapper** item, which contains the **UserComponent** class, an instance of <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> that contains the methods and properties that you will use to process data and to interact with the package. The **ComponentWrapper** item also contains **Connections** and **Variables** collection classes.  
  
3.  The **BufferWrapper** item, which contains classes that inherits from <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptBuffer> for each input and output, and typed properties for each column.  
  
 As you write your code in the **ScriptMain** item, you will use the objects, methods, and properties discussed in this topic. Each component will not use all the methods listed here; however, when used, they are used in the sequence shown.  
  
 The <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> base class does not contain any implementation code for the methods discussed in this topic. Therefore it is unnecessary, but harmless, to add a call to the base class implementation to your own implementation of the method.  
  
 For information about how to use the methods and properties of these classes in a particular type of Script component, see the section [Additional Script Component Examples](../../../integration-services/extending-packages-scripting-data-flow-script-component-examples/additional-script-component-examples.md). The example topics also contain complete code samples.  
  
## AcquireConnections Method  
 Sources and destinations generally must connect to an external data source. Override the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.AcquireConnections%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> base class to retrieve the connection or the connection information from the appropriate connection manager.  
  
 The following example returns a **System.Data.SqlClient.SqlConnection** from an ADO.NET connection manager.  
  
```vb  
Dim connMgr As IDTSConnectionManager100  
Dim sqlConn As SqlConnection  
  
Public Overrides Sub AcquireConnections(ByVal Transaction As Object)  
  
    connMgr = Me.Connections.MyADONETConnection  
    sqlConn = CType(connMgr.AcquireConnection(Nothing), SqlConnection)  
  
End Sub  
```  
  
 The following example returns a complete path and file name from a Flat File Connection Manager, and then opens the file by using a **System.IO.StreamReader**.  
  
```vb  
Private textReader As StreamReader  
Public Overrides Sub AcquireConnections(ByVal Transaction As Object)  
  
    Dim connMgr As IDTSConnectionManager100 = _  
        Me.Connections.MyFlatFileSrcConnectionManager  
    Dim exportedAddressFile As String = _  
        CType(connMgr.AcquireConnection(Nothing), String)  
    textReader = New StreamReader(exportedAddressFile)  
  
End Sub  
```  
  
## PreExecute Method  
 Override the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.PreExecute%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> base class whenever you have processing that you must perform one time only before you start processing rows of data. For example, in a destination, you may want to configure the parameterized command that the destination will use to insert each row of data into the data source.  
  
```vb  
    Dim sqlConn As SqlConnection  
    Dim sqlCmd As SqlCommand  
    Dim sqlParam As SqlParameter  
...  
    Public Overrides Sub PreExecute()  
  
        sqlCmd = New SqlCommand("INSERT INTO Person.Address2(AddressID, City) " & _  
            "VALUES(@addressid, @city)", sqlConn)  
        sqlParam = New SqlParameter("@addressid", SqlDbType.Int)  
        sqlCmd.Parameters.Add(sqlParam)  
        sqlParam = New SqlParameter("@city", SqlDbType.NVarChar, 30)  
        sqlCmd.Parameters.Add(sqlParam)  
  
    End Sub  
```  
  
```csharp  
SqlConnection sqlConn;   
SqlCommand sqlCmd;   
SqlParameter sqlParam;   
  
public override void PreExecute()   
{   
  
    sqlCmd = new SqlCommand("INSERT INTO Person.Address2(AddressID, City) " + "VALUES(@addressid, @city)", sqlConn);   
    sqlParam = new SqlParameter("@addressid", SqlDbType.Int);   
    sqlCmd.Parameters.Add(sqlParam);   
    sqlParam = new SqlParameter("@city", SqlDbType.NVarChar, 30);   
    sqlCmd.Parameters.Add(sqlParam);   
  
}  
```  
  
## Processing Inputs and Outputs  
  
### Processing Inputs  
 Script components that are configured as transformations or destinations have one input.  
  
#### What the BufferWrapper Project Item Provides  
 For each input that you have configured, the **BufferWrapper** project item contains a class that derives from <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptBuffer> and has the same name as the input. Each input buffer class contains the following properties, functions, and methods:  
  
-   Named, typed accessor properties for each selected input column. These properties are read-only or read/write depending on the **Usage Type** specified for the column on the **Input Columns** page of the **Script Transformation Editor**.  
  
-   A **\<column>_IsNull** property for each selected input column. This property is also read-only or read/write depending on the **Usage Type** specified for the column.  
  
-   A **DirectRowTo\<outputbuffer>** method for each configured output. You will use these methods when filtering rows to one of several outputs in the same **ExclusionGroup**.  
  
-   A **NextRow** function to get the next input row, and an **EndOfRowset** function to determine whether the last buffer of data has been processed. You typically do not need these functions when you use the input processing methods implemented in the **UserComponent** base class. The next section provides more information about the **UserComponent** base class.  
  
#### What the ComponentWrapper Project Item Provides  
 The ComponentWrapper project item contains a class named **UserComponent** that derives from <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent>. The **ScriptMain** class in which you write your custom code derives in turn from **UserComponent**. The **UserComponent** class contains the following methods:  
  
-   An overridden implementation of the **ProcessInput** method. This is the method that the data flow engine calls next at run time after the **PreExecute** method, and it may be called multiple times. **ProcessInput** hands off processing to the **\<inputbuffer>_ProcessInput** method. Then the **ProcessInput** method checks for the end of the input buffer and, if the end of the buffer has been reached, calls the overridable **FinishOutputs** method and the private **MarkOutputsAsFinished** method. The **MarkOutputsAsFinished** method then calls **SetEndOfRowset** on the last output buffer.  
  
-   An overridable implementation of the **\<inputbuffer>_ProcessInput** method. This default implementation simply loops through each input row and calls **\<inputbuffer>_ProcessInputRow**.  
  
-   An overridable implementation of the **\<inputbuffer>_ProcessInputRow** method. The default implementation is empty. This is the method that you will normally override to write your custom data processing code.  
  
#### What Your Custom Code Should Do  
 You can use the following methods to process input in the **ScriptMain** class:  
  
-   Override **\<inputbuffer>_ProcessInputRow** to process the data in each input row as it passes through.  
  
-   Override **\<inputbuffer>_ProcessInput** only if you have to do something additional while looping through input rows. (For example, you have to test for **EndOfRowset** to take some other action after all rows have been processed.) Call **\<inputbuffer>_ProcessInputRow** to perform the row processing.  
  
-   Override **FinishOutputs** if you have to do something to the outputs before they are closed.  
  
 The **ProcessInput** method ensures that these methods are called at the appropriate times.  
  
### Processing Outputs  
 Script components configured as sources or transformations have one or more outputs.  
  
#### What the BufferWrapper Project Item Provides  
 For each output that you have configured, the BufferWrapper project item contains a class that derives from <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptBuffer> and has the same name as the output. Each input buffer class contains the following properties and methods:  
  
-   Named, typed, write-only accessor properties for each output column.  
  
-   A write-only **\<column>_IsNull** property for each selected output column that you can use to set the column value to **null**.  
  
-   An **AddRow** method to add an empty new row to the output buffer.  
  
-   A **SetEndOfRowset** method to let the data flow engine know that no more buffers of data are expected. There is also an **EndOfRowset** function to determine whether the current buffer is the last buffer of data. You generally do not need these functions when you use the input processing methods implemented in the **UserComponent** base class.  
  
#### What the ComponentWrapper Project Item Provides  
 The ComponentWrapper project item contains a class named **UserComponent** that derives from <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent>. The **ScriptMain** class in which you write your custom code derives in turn from **UserComponent**. The **UserComponent** class contains the following methods:  
  
-   An overridden implementation of the **PrimeOutput** method. The data flow engine calls this method before **ProcessInput** at run time, and it is only called one time. **PrimeOutput** hands off processing to the **CreateNewOutputRows** method. Then, if the component is a source (that is, the component has no inputs), **PrimeOutput** calls the overridable **FinishOutputs** method and the private **MarkOutputsAsFinished** method. The **MarkOutputsAsFinished** method calls **SetEndOfRowset** on the last output buffer.  
  
-   An overridable implementation of the **CreateNewOutputRows** method. The default implementation is empty. This is the method that you will normally override to write your custom data processing code.  
  
#### What Your Custom Code Should Do  
 You can use the following methods to process outputs in the **ScriptMain** class:  
  
-   Override **CreateNewOutputRows** only when you can add and populate output rows before processing input rows. For example, you can use **CreateNewOutputRows** in a source, but in a transformation with asynchronous outputs, you should call **AddRow** during or after the processing of input data.  
  
-   Override **FinishOutputs** if you have to do something to the outputs before they are closed.  
  
 The **PrimeOutput** method ensures that these methods are called at the appropriate times.  
  
## PostExecute Method  
 Override the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.PostExecute%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> base class whenever you have processing that you must perform one time only after you have processed the rows of data. For example, in a source, you may want to close the **System.Data.SqlClient.SqlDataReader** that you have used to load data into the data flow.  
  
> [!IMPORTANT]  
>  The collection of **ReadWriteVariables** is available only in the **PostExecute** method. Therefore you cannot directly increment the value of a package variable as you process each row of data. Instead, increment the value of a local variable, and set the value of the package variable to the value of the local variable in the **PostExecute** method after all data has been processed.  
  
## ReleaseConnections Method  
 Sources and destinations typically must connect to an external data source. Override the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.ReleaseConnections%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> base class to close and release the connection that you have opened previously in the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.AcquireConnections%2A> method.  
  
```vb  
    Dim connMgr As IDTSConnectionManager100  
...  
    Public Overrides Sub ReleaseConnections()  
  
        connMgr.ReleaseConnection(sqlConn)  
  
    End Sub  
```  
  
```csharp  
IDTSConnectionManager100 connMgr;  
  
public override void ReleaseConnections()  
{  
  
    connMgr.ReleaseConnection(sqlConn);  
  
}  
```  
  
## See Also  
 [Configuring the Script Component in the Script Component Editor](../../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md)   
 [Coding and Debugging the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/coding-and-debugging-the-script-component.md)  
  
  

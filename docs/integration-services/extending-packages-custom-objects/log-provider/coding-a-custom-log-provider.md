---
title: "Coding a Custom Log Provider | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "custom log providers [Integration Services], coding"
ms.assetid: 979a29ca-956e-4fdd-ab47-f06e84cead7a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Coding a Custom Log Provider
  After you have created a class that inherits from the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase> base class, and applied the <xref:Microsoft.SqlServer.Dts.Runtime.DtsLogProviderAttribute> attribute to the class, you must override the implementation of the properties and methods of the base class to provide your custom functionality.  
  
 For working samples of custom log providers, see [Developing a User Interface for a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/developing-a-user-interface-for-a-custom-log-provider.md).  
  
## Configuring the Log Provider  
  
### Initializing the Log Provider  
 You override the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.InitializeLogProvider%2A> method to cache references to the connections collection and the events interface. You can use these cached references later in other methods of the log provider.  
  
### Using the ConfigString Property  
 At design time, a log provider receives configuration information from the **Configuration** column. This configuration information corresponds to the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.ConfigString%2A> property of the log provider. By default, this column contains a text box from which you can retrieve any string information. Most of the log providers included with [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] use this property to store the name of the connection manager that the provider uses to connect to an external data source. If your log provider uses the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.ConfigString%2A> property, use the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Validate%2A> method to validate this property and make sure that the property is set correctly.  
  
### Validating the Log Provider  
 You override the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Validate%2A> method to make sure that the provider has been configured correctly and is ready for execution. Typically, a minimum level of validation is to make sure that the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.ConfigString%2A> is set correctly. Execution cannot continue until the log provider returns <xref:Microsoft.SqlServer.Dts.Runtime.DTSExecResult.Success> from the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Validate%2A> method.  
  
 The following code example shows an implementation of <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Validate%2A> that makes sure that the name of a connection manager name is specified, that the connection manager exists in the package, and that the connection manager returns a file name in the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.ConfigString%2A> property.  
  
```csharp  
public override DTSExecResult Validate(IDTSInfoEvents infoEvents)  
{  
    if (this.ConfigString.Length == 0 || connections.Contains(ConfigString) == false)  
    {  
        infoEvents.FireError(0, "MyTextLogProvider", "The ConnectionManager " + ConfigString + " specified in the ConfigString property cannot be found in the collection.", "", 0);  
        return DTSExecResult.Failure;  
    }  
    else  
    {  
        string fileName = connections[ConfigString].AcquireConnection(null) as string;  
  
        if (fileName == null || fileName.Length == 0)  
        {  
            infoEvents.FireError(0, "MyTextLogProvider", "The ConnectionManager " + ConfigString + " specified in the ConfigString property cannot be found in the collection.", "", 0);  
            return DTSExecResult.Failure;  
        }  
    }  
    return DTSExecResult.Success;  
}  
```  
  
```vb  
Public Overrides Function Validate(ByVal infoEvents As IDTSInfoEvents) As DTSExecResult  
    If Me.ConfigString.Length = 0 Or connections.Contains(ConfigString) = False Then  
        infoEvents.FireError(0, "MyTextLogProvider", "The ConnectionManager " + ConfigString + " specified in the ConfigString property cannot be found in the collection.", "", 0)  
        Return DTSExecResult.Failure  
    Else   
        Dim fileName As String =  connections(ConfigString).AcquireConnectionCType(as string, Nothing)  
  
        If fileName = Nothing Or fileName.Length = 0 Then  
            infoEvents.FireError(0, "MyTextLogProvider", "The ConnectionManager " + ConfigString + " specified in the ConfigString property cannot be found in the collection.", "", 0)  
            Return DTSExecResult.Failure  
        End If  
    End If  
    Return DTSExecResult.Success  
End Function  
```  
  
### Persisting the Log Provider  
 Ordinarily you do not have to implement custom persistence for a connection manager. Custom persistence is required only when the properties of an object use complex data types. For more information, see [Developing Custom Objects for Integration Services](../../../integration-services/extending-packages-custom-objects/developing-custom-objects-for-integration-services.md).  
  
## Logging with the Log Provider  
 There are three run-time methods that must be overridden by all log providers: <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.OpenLog%2A>, <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Log%2A>, and <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.CloseLog%2A>.  
  
> [!IMPORTANT]  
>  During the validation and execution of a single package, the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.OpenLog%2A> and <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.CloseLog%2A> methods are called more than one time. Make sure that your custom code does not cause earlier log entries to be overwritten by the next opening and closing of the log. If you have selected to log validation events in your test package, the first logged event that you should see is OnPreValidate; if instead the first logged event that you see is PackageStart, the initial validation events have been overwritten.  
  
### Opening the Log  
 Most log providers connect to an external data source, such as a file or database, to store the event information that is collected during package execution. As with any other object in the runtime, connecting to the external data source is typically accomplished by using connection manager objects.  
  
 The <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.OpenLog%2A> method is called at the start of package execution.Override this method to establish a connection to the external data source.  
  
 The following sample code shows a log provider that opens a text file for writing during <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.OpenLog%2A>. It opens the file by calling the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.AcquireConnection%2A> method of the connection manager that was specified in the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.ConfigString%2A> property.  
  
```csharp  
public override void OpenLog()  
{  
    if(!this.connections.Contains(this.ConfigString))  
        throw new Exception("The ConnectionManager " + this.ConfigString + " does not exist in the Connections collection.");  
  
    this.connectionManager = connections[ConfigString];  
    string filePath = this.connectionManager.AcquireConnection(null) as string;  
  
    if(filePath == null || filePath.Length == 0)  
        throw new Exception("The ConnectionManager " + this.ConfigString + " is not a valid FILE ConnectionManager");  
  
    //  Create a StreamWriter to append to.  
    sw = new StreamWriter(filePath,true);  
  
    sw.WriteLine("Open log" + System.DateTime.Now.ToShortTimeString());  
}  
```  
  
```vb  
Public Overrides  Sub OpenLog()  
    If Not Me.connections.Contains(Me.ConfigString) Then  
        Throw New Exception("The ConnectionManager " + Me.ConfigString + " does not exist in the Connections collection.")  
    End If  
  
    Me.connectionManager = connections(ConfigString)  
    Dim filePath As String =  Me.connectionManager.AcquireConnectionCType(as string, Nothing)  
  
    If filePath = Nothing Or filePath.Length = 0 Then  
        Throw New Exception("The ConnectionManager " + Me.ConfigString + " is not a valid FILE ConnectionManager")  
    End If  
  
    '  Create a StreamWriter to append to.  
    sw = New StreamWriter(filePath,True)  
  
    sw.WriteLine("Open log" + System.DateTime.Now.ToShortTimeString())  
End Sub  
```  
  
### Writing Log Entries  
 The <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Log%2A> method is called every time that an object in the package raises an event by calling a Fire\<event> method on one of the event interfaces. Each event is raised with information about its context and usually an explanatory message. However, not every call to the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Log%2A> method includes information for every method parameter. For example, some standard events whose names are self-explanatory do not provide MessageText, and DataCode and DataBytes are intended for optional supplemental information.  
  
 The following code example implements the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Log%2A> method, and writes the events to the stream that was opened in the previous section.  
  
```csharp  
public override void Log(string logEntryName, string computerName, string operatorName, string sourceName, string sourceID, string executionID, string messageText, DateTime startTime, DateTime endTime, int dataCode, byte[] dataBytes)  
{  
    sw.Write(logEntryName + ",");  
    sw.Write(computerName + ",");  
    sw.Write(operatorName + ",");  
    sw.Write(sourceName + ",");  
    sw.Write(sourceID + ",");  
    sw.Write(messageText + ",");  
    sw.Write(dataBytes + ",");  
    sw.WriteLine("");  
}  
```  
  
```vb  
Public Overrides  Sub Log(ByVal logEnTryName As String, ByVal computerName As String, ByVal operatorName As String, ByVal sourceName As String, ByVal sourceID As String, ByVal executionID As String, ByVal messageText As String, ByVal startTime As DateTime, ByVal endTime As DateTime, ByVal dataCode As Integer, ByVal dataBytes() As Byte)  
    sw.Write(logEnTryName + ",")  
    sw.Write(computerName + ",")  
    sw.Write(operatorName + ",")  
    sw.Write(sourceName + ",")  
    sw.Write(sourceID + ",")  
    sw.Write(messageText + ",")  
    sw.Write(dataBytes + ",")  
    sw.WriteLine("")  
End Sub  
```  
  
### Closing the Log  
 The <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.CloseLog%2A> method is called at the end of package execution, after all the objects in the package have completed execution, or, when the package stops because of errors.  
  
 The following code example demonstrates an implementation of the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.CloseLog%2A> method that closes the file stream that was opened during the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.OpenLog%2A> method.  
  
```csharp  
public override void CloseLog()  
{  
    if (sw != null)  
    {  
        sw.WriteLine("Close log" + System.DateTime.Now.ToShortTimeString());  
        sw.Close();  
    }  
}  
```  
  
```vb  
Public Overrides  Sub CloseLog()  
    If Not sw Is Nothing Then  
        sw.WriteLine("Close log" + System.DateTime.Now.ToShortTimeString())  
        sw.Close()  
    End If  
End Sub  
```  
 
## See Also  
 [Creating a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/creating-a-custom-log-provider.md)   
 [Developing a User Interface for a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/developing-a-user-interface-for-a-custom-log-provider.md)  
  
  

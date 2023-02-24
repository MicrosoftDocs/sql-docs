---
description: "Adding Connections Programmatically"
title: "Adding Connections Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services 
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "connection managers [Integration Services], packages"
  - "Integration Services packages, connections"
  - "connections [Integration Services], packages"
  - "SSIS packages, connections"
  - "packages [Integration Services], connections"
  - "intrinsic properties [Integration Services]"
  - "SQL Server Integration Services packages, connections"
  - "ConnectionManager class"
  - "SSIS connection managers"
  - "adding package connections"
ms.assetid: d90716d1-4c65-466c-b82c-4aabbee1e3e5
author: chugugrace
ms.author: chugu
---
# Adding Connections Programmatically

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> class represents physical connections to external data sources. The <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> class isolates the implementation details of the connection from the runtime. This enables the runtime to interact with each connection manager in a consistent and predictable manner. Connection managers contain a set of stock properties that all connections have in common, such as the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.Name%2A>, <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.ID%2A>, <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.Description%2A>, and <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.ConnectionString%2A>. However, the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.ConnectionString%2A> and <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.Name%2A> properties are ordinarily the only properties required to configure a connection manager. Unlike other programming paradigms, where connection classes expose methods such as **Open** or **Connect** to physically establish a connection to the data source, the run-time engine manages all the connections for the package while it runs.  
  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Connections> class is a collection of the connection managers that have been added to that package and are available for use at run time. You can add more connection managers to the collection by using the <xref:Microsoft.SqlServer.Dts.Runtime.Connections.Add%2A> method of the collection, and supplying a string that indicates the connection manager type. The <xref:Microsoft.SqlServer.Dts.Runtime.Connections.Add%2A> method returns the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> instance that was added to the package.  
  
## Intrinsic Properties  
 The <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> class exposes a set of properties that are common to all connections. However, sometimes you need access to properties that are unique to the specific connection type. The <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.Properties%2A> collection of the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> class provides access to these properties. The properties can be retrieved from the collection using the indexer or the property name and the **GetValue** method, and the values are set using the **SetValue** method. The properties of the underlying connection object properties can also be set by acquiring an actual instance of the object and setting its properties directly. To get the underlying connection, use the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.InnerObject%2A> property of the connection manager. The following line of code shows a C# line that creates an ADO.NET connection manager that has the underlying class, <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.ConnectionManagerAdoNetClass>.  
  
 `ConnectionManagerAdoNetClass cmado = cm.InnerObject as ConnectionManagerAdoNet;`  
  
 This casts the managed connection manager object to its underlying connection object. If you are using C++, the **QueryInterface** method of the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> object is called and the interface of the underlying connection object is requested.  
  
 The following table lists the connection managers included with [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. and the string that is used in the `package.Connections.Add("xxx")` statement. For a list of all connection managers, see [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md).  
  
|String|Connection manager|  
|------------|------------------------|  
|"OLEDB"|Connection manager for OLE DB connections.|  
|"ODBC"|Connection manager for ODBC connections.|  
|"ADO"|Connection manager for ADO connections.|  
|"ADO.NET:SQL"|Connection manager for ADO.NET (SQL data provider) connections.|  
|"ADO.NET:OLEDB"|Connection manager for ADO.NET (OLE DB data provider) connections.|  
|"FLATFILE"|Connection manager for flat file connections.|  
|"FILE"|Connection manager for file connections.|  
|"MULTIFLATFILE"|Connection manager for multiple flat file connections.|  
|"MULTIFILE"|Connection manager for multiple file connections.|  
|"SQLMOBILE"|Connection manager for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact connections.|  
|"MSOLAP100"|Connection manager for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connections.|  
|"FTP"|Connection manager for FTP connections.|  
|"HTTP"|Connection manager for HTTP connections.|  
|"MSMQ"|Connection manager for Message Queuing (also known as MSMQ) connections.|  
|"SMTP"|Connection manager for SMTP connections.|  
|"WMI"|Connection manager for Microsoft Windows Management Instrumentation (WMI) connections.|  
  
 The following code example demonstrates adding an OLE DB and FILE connection to the <xref:Microsoft.SqlServer.Dts.Runtime.Package.Connections%2A> collection of a <xref:Microsoft.SqlServer.Dts.Runtime.Package>. The example then sets the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.ConnectionString%2A>, <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.Name%2A>, and <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.Description%2A> properties.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      // Create a package, and retrieve its connections.  
      Package pkg = new Package();  
      Connections pkgConns = pkg.Connections;  
  
      // Add an OLE DB connection to the package, using the   
      // method defined in the AddConnection class.  
      CreateConnection myOLEDBConn = new CreateConnection();  
      myOLEDBConn.CreateOLEDBConnection(pkg);  
  
      // View the new connection in the package.  
      Console.WriteLine("Connection description: {0}",  
         pkg.Connections["SSIS Connection Manager for OLE DB"].Description);  
  
      // Add a second connection to the package.  
      CreateConnection myFileConn = new CreateConnection();  
      myFileConn.CreateFileConnection(pkg);  
  
      // View the second connection in the package.  
      Console.WriteLine("Connection description: {0}",  
        pkg.Connections["SSIS Connection Manager for Files"].Description);  
  
      Console.WriteLine();  
      Console.WriteLine("Number of connections in package: {0}", pkg.Connections.Count);  
  
      Console.Read();  
    }  
  }  
  // <summary>  
  // This class contains the definitions for multiple  
  // connection managers.  
  // </summary>  
  public class CreateConnection  
  {  
    // Private data.  
    private ConnectionManager ConMgr;  
  
    // Class definition for OLE DB Provider.  
    public void CreateOLEDBConnection(Package p)  
    {  
      ConMgr = p.Connections.Add("OLEDB");  
      ConMgr.ConnectionString = "Provider=SQLOLEDB.1;" +  
        "Integrated Security=SSPI;Initial Catalog=AdventureWorks;" +  
        "Data Source=(local);";  
      ConMgr.Name = "SSIS Connection Manager for OLE DB";  
      ConMgr.Description = "OLE DB connection to the AdventureWorks database.";  
    }  
    public void CreateFileConnection(Package p)  
    {  
      ConMgr = p.Connections.Add("File");  
      ConMgr.ConnectionString = @"\\<yourserver>\<yourfolder>\books.xml";  
      ConMgr.Name = "SSIS Connection Manager for Files";  
      ConMgr.Description = "Flat File connection";  
    }  
  }  
  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Runtime  
  
Module Module1  
  
  Sub Main()  
  
    ' Create a package, and retrieve its connections.  
    Dim pkg As New Package()  
    Dim pkgConns As Connections = pkg.Connections  
  
    ' Add an OLE DB connection to the package, using the   
    ' method defined in the AddConnection class.  
    Dim myOLEDBConn As New CreateConnection()  
    myOLEDBConn.CreateOLEDBConnection(pkg)  
  
    ' View the new connection in the package.  
    Console.WriteLine("Connection description: {0}", _  
      pkg.Connections("SSIS Connection Manager for OLE DB").Description)  
  
    ' Add a second connection to the package.  
    Dim myFileConn As New CreateConnection()  
    myFileConn.CreateFileConnection(pkg)  
  
    ' View the second connection in the package.  
    Console.WriteLine("Connection description: {0}", _  
      pkg.Connections("SSIS Connection Manager for Files").Description)  
  
    Console.WriteLine()  
    Console.WriteLine("Number of connections in package: {0}", pkg.Connections.Count)  
  
    Console.Read()  
  
  End Sub  
  
End Module  
  
' This class contains the definitions for multiple  
' connection managers.  
  
Public Class CreateConnection  
  ' Private data.  
  Private ConMgr As ConnectionManager  
  
  ' Class definition for OLE DB provider.  
  Public Sub CreateOLEDBConnection(ByVal p As Package)  
    ConMgr = p.Connections.Add("OLEDB")  
    ConMgr.ConnectionString = "Provider=SQLOLEDB.1;" & _  
      "Integrated Security=SSPI;Initial Catalog=AdventureWorks;" & _  
      "Data Source=(local);"  
    ConMgr.Name = "SSIS Connection Manager for OLE DB"  
    ConMgr.Description = "OLE DB connection to the AdventureWorks database."  
  End Sub  
  
  Public Sub CreateFileConnection(ByVal p As Package)  
    ConMgr = p.Connections.Add("File")  
    ConMgr.ConnectionString = "\\<yourserver>\<yourfolder>\books.xml"  
    ConMgr.Name = "SSIS Connection Manager for Files"  
    ConMgr.Description = "Flat File connection"  
  End Sub  
  
End Class  
```  
  
 **Sample Output:**  
  
 `Connection description: OLE DB connection to the AdventureWorks database.`  
  
 `Connection description: Flat File connection.`  
  
 `Number of connections in package: 2`  
  
## External Resources  
 Technical article, [Connection Strings](https://go.microsoft.com/fwlink/?LinkId=220743), on carlprothman.net.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)   
 [Create Connection Managers](../connection-manager/integration-services-ssis-connections.md)  
  

---
title: "Establishing Connections in ADOMD.NET | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "opening connections"
  - "closing connections"
  - "connections [ADOMD.NET]"
  - "ADOMD.NET, connections"
ms.assetid: 7b9610f5-6641-42cc-af4e-bd35771913d1
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Connections in ADOMD.NET
  In ADOMD.NET, you use the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object to open connections with analytical data sources, such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases. When the connection is no longer needed, you should explicitly close the connection.  
  
## Opening a Connection  
 To open a connection in ADOMD.NET, you must first specify a connection string to a valid analytical data source and database. Then, you must explicitly open the connection to that data source.  
  
### Specifying a Multidimensional Data Source  
 To specify an analytical data source and database, you set the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A> property of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object. The connection string specified for the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A> property is an OLE DB–compliant string. ADOMD.NET uses the specified connection string to determine how to connect to the server.  
  
 The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A> property can be set on either an existing <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object or during the creation an instance of an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object. The following code demonstrates how to set the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A> property when you create an ADOMD connection:  
  
```vb  
Dim advwrksConnection As New AdomdConnection("Data Source=localhost;Catalog=AdventureWorksAS")  
System.Diagnostics.Debug.Writeline(advwrksConnection.ConnectionString)  
```  
  
```csharp  
AdomdConnection advwrksConnection = new AdomdConnection("Data Source=localhost;Catalog=AdventureWorksAS");  
System.Diagnostics.Debug.Writeline(advwrksConnection.ConnectionString);  
```  
  
### Opening a Connection to the Data Source  
 After you have specified the connection string, you must use the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Open%2A> method to open the connection. When you open a <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object, you can set various levels of security for the connection. The security level that is used for the connection depends on the value of the **ProtectionLevel** connection string setting. For more information about opening secure connections in ADOMD.NET, see [Establishing Secure Connections in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/connections-in-adomd-net-establishing-secure-connections.md).  
  
## Working with a Connection  
 Each open connection exists in a session, which provides support for stateful operations. A session can be shared by more than one open connection. Sharing a session enables more than one client to share the same context. For more information, see [Working with Connections and Sessions in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/connections-in-adomd-net-working-with-connections-and-sessions.md).  
  
 You can use an open connection to retrieve metadata, data, and run commands. For more information, see [Retrieving Metadata from an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-from-an-analytical-data-source.md), [Retrieving Data from an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-from-an-analytical-data-source.md), and [Executing Commands Against an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/executing-commands-against-an-analytical-data-source.md).  
  
 When the connection is open, you can retrieve data, retrieve metadata, and run commands from within a read-committed transaction, in which shared locks are held while the data is being read to avoid dirty reads. The data can still be changed before the end of the transaction, resulting in non-repeatable reads or phantom data. For more information, see [Performing Transactions in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/connections-in-adomd-net-performing-transactions.md).  
  
## Closing a Connection  
 We recommended that you explicitly close an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object as soon as you no longer need the connection. To explicitly close the connection, you use the **Close** and **Dispose** methods of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object.  
  
 A connection that is not explicitly closed, but is allowed to fall out of scope, may not release server resources quickly enough to enable high-concurrency [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] client applications to efficiently open new connections. Depending on how you created the connection, the session used by the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object can remain active if the connection is not explicitly closed.  
  
 For more information about sessions, see [Working with Connections and Sessions in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/connections-in-adomd-net-working-with-connections-and-sessions.md).  
  
> [!IMPORTANT]  
>  In the **Finalize** method of any implemented class, do not call the **Close** or **Dispose** methods of an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object, <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object, or any other managed object. In a finalizer, only release unmanaged resources that are directly owned by the implemented class. If the implemented class does not own any unmanaged resources, do not include a **Finalize** method in the class definition.  
  
## See Also  
 [ADOMD.NET Client Programming](../../analysis-services/multidimensional-models-adomd-net-client/adomd-net-client-programming.md)  
  
  
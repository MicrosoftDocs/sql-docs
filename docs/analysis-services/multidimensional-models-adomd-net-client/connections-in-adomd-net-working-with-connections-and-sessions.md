---
title: "Working with Connections and Sessions in ADOMD.NET | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
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
  - "sessions [ADOMD.NET]"
  - "connections [ADOMD.NET]"
ms.assetid: 72b43c06-f3e4-42c3-a696-4a3419c3b884
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Connections in ADOMD.NET - Working with Connections and Sessions
  In XML for Analysis (XMLA), sessions provide support for stateful operations during analytical data access. Sessions frame the scope and context of commands and transactions for an analytical data source. The XMLA elements used to manage sessions are [BeginSession](../../analysis-services/xmla/xml-elements-headers/beginsession-element-xmla.md), [Session](../../analysis-services/xmla/xml-elements-headers/session-element-xmla.md), and [EndSession](../../analysis-services/xmla/xml-elements-headers/endsession-element-xmla.md).  
  
 ADOMD.NET uses these three XMLA session elements when you start a session, perform queries or retrieve data during the session, and close a session.  
  
## Starting a Session  
 The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.SessionID%2A> property of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object contains the identifier of the active session associated with the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object. By using this property correctly, you can effectively control both client and server statefulness in your application:  
  
-   If the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.SessionID%2A> property is not set to a valid session ID when the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Open%2A> method is called, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object requests a new session ID from the provider. ADOMD.NET initiates a session by sending an XMLA **BeginSession** header to the provider. If ADOMD.NET is successful is starting a session, ADOMD.NET sets the value of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.SessionID%2A> property to the session ID of the newly created session.  
  
-   If the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.SessionID%2A> property is set to a valid session ID when the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Open%2A> method is called, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object tries to connect to the specified session.  
  
 If the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object cannot connect to the specified session, or if the provider does not support sessions, an exception is thrown.  
  
> [!NOTE]  
>  After you have had ADOMD.NET create a session, you can connect multiple <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> objects to that single active session, or you can disconnect a single <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object from that session and reconnect that object to another session.  
  
## Working in a Session  
 After ADOMD.NET connects the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object to a valid session, ADOMD.NET will send an XMLA **Session** header to the provider with every request for data or metadata made by an application. Every request will have the session ID set to the value of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.SessionID%2A> property.  
  
 A session ID does not guarantee that a session remains valid. If the session expires (for example, if the session times out or the connection is lost), the provider can choose to end and roll back the actions of that session. If this occurs, all subsequent method calls from the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object will throw an exception. Because exceptions are thrown only when the next request is sent to the provider, not when the session expires, your application must be able to handle these exceptions any time that your application retrieves data or metadata from the provider.  
  
## Closing a Session  
 If the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Close%2A> method is called without specifying the value of the *endSession* parameter, or if the *endSession* parameter is set to True, both the connection to the session and the session associated with the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object are closed. To close a session, ADOMD.NET sends an XMLA **EndSession** header to the provider, with the session ID set to the value of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.SessionID%2A> property.  
  
 If the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Close%2A> method is called with the *endSession* parameter set to False, the session associated with the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object remains active but the connection to the session is closed.  
  
## Example of Managing a Session  
 The following example demonstrates how to open a connection, create a session, and close the connection while keeping the session open in ADOMD.NET:  
  
```vb  
Public Function CreateSession(ByVal connectionString As String) As String  
    Dim strSessionID As String = ""  
    Dim objConnection As New AdomdConnection  
  
    Try  
        ' First, try to connect to the specified data source.  
        ' If the connection string is not valid, or if the specified  
        ' provider does not support sessions, an exception is thrown.  
        objConnection.ConnectionString = connectionString  
        objConnection.Open()  
  
        ' Now that the connection is open, retrieve the new  
        ' active session ID.  
        strSessionID = objConnection.SessionID  
        ' Close the connection, but leave the session open.  
        objConnection.Close(False)  
        Return strSessionID  
  
    Finally  
        objConnection = Nothing  
    End Try  
End Function  
```  
  
```csharp  
static string CreateSession(string connectionString)  
{  
    string strSessionID = "";  
    AdomdConnection objConnection = new AdomdConnection();  
    try  
    {  
        /*First, try to connect to the specified data source.  
          If the connection string is not valid, or if the specified  
          provider does not support sessions, an exception is thrown. */  
        objConnection.ConnectionString = connectionString;  
        objConnection.Open();  
  
        // Now that the connection is open, retrieve the new  
        // active session ID.  
        strSessionID = objConnection.SessionID;  
        // Close the connection, but leave the session open.  
        objConnection.Close(false);  
        return strSessionID;  
    }  
    finally  
    {  
        objConnection = null;  
    }  
}  
```  
  
## See Also  
 [Establishing Connections in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/connections-in-adomd-net.md)  
  
  
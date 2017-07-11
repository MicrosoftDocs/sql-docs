---
title: "Establishing Secure Connections in ADOMD.NET | Microsoft Docs"
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
  - "connections [ADOMD.NET]"
  - "security [ADOMD.NET]"
ms.assetid: b084d447-1456-45a4-8e0e-746c07d7d6fd
caps.latest.revision: 42
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Connections in ADOMD.NET - Establishing Secure Connections
  When you use a connection in ADOMD.NET, the security method that is used for the connection depends on the value of the **ProtectionLevel** property of the connection string used when you call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Open%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection>.  
  
 The **ProtectionLevel** property offers four levels of security: unauthenticated, authenticated, signed, and encrypted. The following table describes these various security levels.  
  
> [!NOTE]  
>  If you choose to use database connection pooling, the database will not be able to manage security. This is because database connection pooling requires that the connection string be identical to pool connections. Therefore, you must manage security elsewhere.  
  
|Security Level|ProtectionLevel Value|  
|--------------------|---------------------------|  
|*unauthenticated connection*<br /> An unauthenticated connection does no form of authentication. This kind of connection represents the most widely supported, but least secure, form of connection.|**None**|  
|*authenticated connection*<br /> An authenticated connection authenticates the user who is making the connection, but does not secure additional communications. This kind of connection is useful in that you can establish the identity of the user or application that is connecting to an analytical data source.|**Connect**|  
|*signed connection*<br /> A signed connection authenticates the user who is requesting the connection, and then makes sure that transmissions are not modified. This kind of connection is useful when the authenticity of the transferred data must be verified. However, a signed connection only prevents the content of the data packet from being modified. The content can still be viewed in transit.<br /><br /><br /><br /> Note that a signed connection is only supported by the XML for Analysis provider supplied by [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|**Pkt Integrity** or **PktIntegrity**|  
|*encrypted connection*<br /> An encrypted connection is the default connection type used by ADOMD.NET. This kind of connection authenticates the user who is requesting the connection, and then also encrypts the data that is transmitted. An encrypted connection is the securest form of connection that can be created by ADOMD.NET. The content of the data packet cannot be viewed or modified, thereby protecting data during transit.<br /><br /><br /><br /> An encrypted connection is only supported by the XML for Analysis provider supplied by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|**Pkt Privacy** or **PktPrivacy**|  
  
 However, not all levels of security are available for all kinds of connections:  
  
-   A TCP connection can use any one of the four levels of security. In fact, a TCP connection, when you use it with Windows Integrated Security, offers the securest method of connecting to an analytical data source.  
  
-   An HTTP connection can only be an authenticated connection. Therefore, the **ProtectionLevel** property must be set to **Connect**.  
  
-   An HTTPS connection can only be an encrypted connection. Therefore, the **ProtectionLevel** property must be set to **Pkt Privacy** or **PktPrivacy**.  
  
## Securing TCP Connections  
 For a TCP connection, the **ProtectionLevel** property supports all four levels of security, as shown in the following table.  
  
|ProtectionLevel Value|Use with TCP Connection?|Results|  
|---------------------------|------------------------------|-------------|  
|**None**|Yes|Specifies an unauthenticated connection.<br /><br /> A TCP stream is requested from the provider, but there is no form of authentication performed on the user who is requesting the stream.|  
|**Connect**|Yes|Specifies an authenticated connected.<br /><br /> A TCP stream is requested from the provider, and then the security context of the user who is requesting the stream is authenticated against the server: If authentication succeeds, no other action is taken. If authentication fails, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object disconnects from the multidimensional data source and an exception is thrown.<br /><br /> After authentication succeeds or fails, the security context that is used to authenticate the connection is disposed.|  
|**Pkt Integrity** or **PktIntegrity**|Yes|Specifies a signed connection.<br /><br /> A TCP stream is requested from the provider, and then the security context of the user who is requesting the stream is authenticated against the server:<br /><br /> <br /><br /> If authentication succeeds, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object closes the existing TCP stream and opens a signed TCP stream to handle all requests. Each request for data or metadata is authenticated by using the security context that was used to open the connection. Additionally, each packet is digitally signed to make sure that the payload of the TCP packet has not been changed in any way.<br /><br /> <br /><br /> If authentication fails, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object disconnects from the multidimensional data source and an exception is thrown.|  
|**Pkt Privacy** or **PktPrivacy**|Yes|Specifies an encrypted connection.<br /><br /> <br /><br /> Note that you can also specify an encrypted connection by not setting the **ProtectionLevel** property in the connection string.<br /><br /> <br /><br /> A TCP stream is requested from the provider, and then the security context of the user requesting the stream is authenticated against the server:<br /><br /> <br /><br /> If authentication succeeds, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object closes the existing TCP stream and opens up an encrypted TCP stream to handle all requests. Each request for data or metadata is authenticated by using the security context that was used to open the connection. Additionally, the payload of each TCP packet is encrypted by using the highest encryption method supported by both the provider and the multidimensional data source.<br /><br /> <br /><br /> If authentication fails, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object disconnects from the multidimensional data source and an exception is thrown.|  
  
### Using Windows Integrated Security for the Connection  
 Windows Integrated Security is the securest way of establishing and securing a connection to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Windows Integrated Security does not reveal security credentials, such as a user name or password, during the authentication process, but instead uses the security identifier of the currently running process to establish identity. For most client applications, this security identifier represents the identity of the currently logged-on user.  
  
 To use Windows Integrated Security, the connection string requires the following settings:  
  
-   For the **Integrated Security** property, either do not set this property or set this property to **SSPI**.  
  
    > [!NOTE]  
    >  Windows Integrated Security is only available for TCP connections because HTTP connections must use the **Basic** setting for the **Integrated Security** property.  
  
-   For the **ProtectionLevel** property, set this property to **Connect**, **Pkt Integrity**, or **Pkt Privacy**.  
  
## Securing HTTP Connections  
 HTTPS and Secure Sockets Layer (SSL) can be used to externally secure HTTP communications with an analytical data source.  
  
 Because an XMLA provider only uses secure HTTP, an HTTP connection in ADOMD.NET must be a signed connection, as shown in the following table.  
  
|ProtectionLevel Value|Use with HTTP or HTTPS|  
|---------------------------|----------------------------|  
|**None**|No|  
|**Connect**|HTTP|  
|**Pkt Integrity** or **PktIntegrity**|No|  
|**Pkt Privacy** or **PktPrivacy**|HTTPS|  
  
### Opening a Secure HTTP Connection  
 The following example demonstrates how to use ADOMD.NET to open an HTTP connection for the **AdventureWorksAS** sample [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database:  
  
```vb  
Public Function GetAWEncryptedConnection( _  
    Optional ByVal serverName As String = "https:\\localhost\isapy\msmdpump.dll") _  
    As AdomdConnection  
  
    Dim strConnectionString As String = ""  
    Dim objConnection As New AdomdConnection  
  
    Try  
        ' To establish an encrypted connection, set the   
        ' ProtectionLevel setting to PktPrivacy.  
        strConnectionString = "DataSource=" & serverName & ";" & _  
            "Catalog=AdventureWorksAS;" & _  
            "ProtectionLevel=PktPrivacy;"  
  
        ' Note that username and password are not supplied here.  
        ' The current security context is used for authentication  
        ' purposes.  
  
        objConnection.ConnectionString = strConnectionString  
        objConnection.Open()  
    Catch ex As Exception  
        objConnection = Nothing  
        Throw ex  
    Finally  
        ' Return the encrypted connection.  
        GetAWEncryptedConnection = objConnection  
    End Try  
End Function  
  
```  
  
## See Also  
 [Establishing Connections in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/connections-in-adomd-net.md)  
  
  
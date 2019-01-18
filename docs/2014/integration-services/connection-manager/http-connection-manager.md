---
title: "HTTP Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "HTTP connection manager"
  - "Web site connections [Integration Services]"
  - "connection managers [Integration Services], HTTP"
  - "Web server connections [Integration Services]"
  - "connections [Integration Services], HTTP"
ms.assetid: 26b2b3e1-d02c-46ca-8d31-7aef2bbc3c53
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# HTTP Connection Manager
  An HTTP connection enables a package to access a Web server by using HTTP to send or receive files. The Web Service task that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes uses this connection manager.  
  
 When you add an HTTP connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to an HTTP connection at run time, sets the connection manager properties, and adds the connection manager to the `Connections` collection on the package.  
  
 The `ConnectionManagerType` property of the connection manager is set to `HTTP.`  
  
 You can configure the HTTP connection manager the following ways:  
  
-   Use credentials. If the connection manager uses credentials, its properties include the user name, password, and domain.  
  
    > [!IMPORTANT]  
    >  The HTTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
-   Use a client certificate. If the connection manager uses a client certificate, its properties include the certificate name.  
  
-   Provide a time-out for connecting to the server and a chunk size for writing data.  
  
-   Use a proxy server. The proxy server can also be configured to use credentials and to bypass the proxy server and use local addresses instead.  
  
## Configuration of the HTTP Connection Manager  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [HTTP Connection Manager Editor &#40;Server Page&#41;](../http-connection-manager-editor-server-page.md)  
  
-   [HTTP Connection Manager Editor &#40;Proxy Page&#41;](../http-connection-manager-editor-proxy-page.md)  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager>.  
  
## See Also  
 [Web Service Task](../control-flow/web-service-task.md)   
 [Integration Services &#40;SSIS&#41; Connections](integration-services-ssis-connections.md)  
  
  

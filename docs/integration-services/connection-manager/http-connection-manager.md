---
title: "HTTP Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.httpconnection.server.f1"
  - "sql13.dts.designer.httpconnection.proxy.f1"
helpviewer_keywords: 
  - "HTTP connection manager"
  - "Web site connections [Integration Services]"
  - "connection managers [Integration Services], HTTP"
  - "Web server connections [Integration Services]"
  - "connections [Integration Services], HTTP"
ms.assetid: 26b2b3e1-d02c-46ca-8d31-7aef2bbc3c53
author: janinezhang
ms.author: janinez
manager: craigg
---
# HTTP Connection Manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  An HTTP connection enables a package to access a Web server by using HTTP to send or receive files. The Web Service task that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes uses this connection manager.  
  
 When you add an HTTP connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to an HTTP connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **HTTP.**  
  
 You can configure the HTTP connection manager the following ways:  
  
-   Use credentials. If the connection manager uses credentials, its properties include the user name, password, and domain.  
  
    > [!IMPORTANT]  
    >  The HTTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
-   Use a client certificate. If the connection manager uses a client certificate, its properties include the certificate name.  
  
-   Provide a time-out for connecting to the server and a chunk size for writing data.  
  
-   Use a proxy server. The proxy server can also be configured to use credentials and to bypass the proxy server and use local addresses instead.  
  
## Configuration of the HTTP Connection Manager  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager>.  
  
## HTTP Connection Manager Editor (Server Page)
  Use the **Server** tab of the **HTTP Connection Manager Editor** dialog box to configure the HTTP Connection Manager by specifying properties such as the URL and security credentials. An HTTP connection enables a package to access a Web server by using HTTP to send or receive files. After configuring the HTTP Connection Manager, you can also test the connection.  
  
> [!IMPORTANT]  
>  The HTTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 To learn more about the HTTP connection manager, see [HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md). To learn more about a common usage scenario for the HTTP Connection Manager, see [Web Service Task](../../integration-services/control-flow/web-service-task.md).  
  
### Options  
 **Server URL**  
 Type the URL for the server.  
  
 If you plan to use the **Download WSDL** button on the **General** page of the **Web Service Task Editor** to download a WSDL file, type the URL for the WSDL file. This URL ends with "?wsdl".  
  
 **Use credentials**  
 Specify whether you want the HTTP Connection Manager to use the user's security credentials for authentication.  
  
 **User name**  
 If the HTTP Connection Manager uses credentials, you must specify a user name, password, and domain.  
  
 **Password**  
 If the HTTP Connection Manager uses credentials, you must specify a user name, password, and domain.  
  
 **Domain**  
 If the HTTP Connection Manager uses credentials, you must specify a user name, password, and domain.  
  
 **Use client certificate**  
 Specify whether you want the HTTP Connection Manager to use a client certificate for authentication.  
  
 **Certificate**  
 Select a certificate from the list by using the **Select Certificate** dialog box. The text box displays the name associated with this certificate.  
  
 **Time-out (in seconds)**  
 Provide a time-out for connecting to the Web server. The default value of this property is 30 seconds.  
  
 **Chunk size (in KB)**  
 Provide a chunk size for writing data.  
  
 **Test Connection**  
 After configuring the HTTP Connection Manager, confirm that the connection is viable by clicking **Test Connection**.  
  
## HTTP Connection Manager Editor (Proxy Page)
  Use the **Proxy** tab of the **HTTP Connection Manager Editor** dialog box to configure the HTTP Connection Manager to use a proxy server. An HTTP connection enables a package to access a Web server by using HTTP to send or receive files.  
  
 To learn more about the HTTP connection manager, see [HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md). To learn more about a common usage scenario for the HTTP Connection Manager, see [Web Service Task](../../integration-services/control-flow/web-service-task.md).  
  
### Options  
 **Use proxy**  
 Specify whether you want the HTTP Connection Manager to connect through a proxy server.  
  
 **Proxy URL**  
 Type the URL for the proxy server.  
  
 **Bypass proxy on local**  
 Specify whether you want the HTTP Connection Manager to bypass the proxy server for local addresses.  
  
 **Use credentials**  
 Specify whether you want the HTTP Connection Manager to use security credentials for the proxy server.  
  
 **User name**  
 If the HTTP Connection Manager uses credentials, you must specify a user name, password, and domain.  
  
 **Password**  
 If the HTTP Connection Manager uses credentials, you must specify a user name, password, and domain.  
  
 **Domain**  
 If the HTTP Connection Manager uses credentials, you must specify a user name, password, and domain.  
  
 **Proxy bypass list**  
 The list of addresses for which  you want to bypass the proxy server.  
  
 **Add**  
 Type an address for which you want to bypass the proxy server.  
  
 **Remove**  
 Select an address, and then remove it by clicking **Remove**.  
  
## See Also  
 [Web Service Task](../../integration-services/control-flow/web-service-task.md)   
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  
  
  

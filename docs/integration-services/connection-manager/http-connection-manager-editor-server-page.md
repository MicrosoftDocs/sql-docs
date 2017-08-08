---
title: "HTTP Connection Manager Editor (Server Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.httpconnection.server.f1"
helpviewer_keywords: 
  - "HTTP Connection Manager Editor"
ms.assetid: 774778a0-ece6-4971-b93f-b121d8fc1fc1
caps.latest.revision: 32
author: "v-thepet"
ms.author: "douglasl"
manager: "jhubbard"
---
# HTTP Connection Manager Editor (Server Page)
  Use the **Server** tab of the **HTTP Connection Manager Editor** dialog box to configure the HTTP Connection Manager by specifying properties such as the URL and security credentials. An HTTP connection enables a package to access a Web server by using HTTP to send or receive files. After configuring the HTTP Connection Manager, you can also test the connection.  
  
> [!IMPORTANT]  
>  The HTTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 To learn more about the HTTP connection manager, see [HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md). To learn more about a common usage scenario for the HTTP Connection Manager, see [Web Service Task](../../integration-services/control-flow/web-service-task.md).  
  
## Options  
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
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [HTTP Connection Manager Editor &#40;Proxy Page&#41;](../../integration-services/connection-manager/http-connection-manager-editor-proxy-page.md)  
  
  
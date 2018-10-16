---
title: "FTP Connection Manager Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.ftpconnectionmanager.f1"
helpviewer_keywords: 
  - "FTP Connection Manager Editor"
ms.assetid: 874b6585-255b-4a43-8396-bb08c3e8ac2b
author: douglaslms
ms.author: douglasl
manager: craigg
---
# FTP Connection Manager Editor
  Use the **FTP Connection Manager Editor** dialog box to specify properties for connecting to an FTP server.  
  
> [!IMPORTANT]  
>  The FTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 To learn more about the FTP connection manager, see [FTP Connection Manager](connection-manager/ftp-connection-manager.md).  
  
## Options  
 **Server name**  
 Provide the name of the FTP server.  
  
 **Server port**  
 Specify the port number on the FTP server to use for the connection. The default value of this property is **21**.  
  
 **User name**  
 Provide a user name to access the FTP server. The default value of this property is **anonymous**.  
  
 **Password**  
 Provide the password to access the FTP server.  
  
 **Time-out (in seconds)**  
 Specify the number of seconds the task takes before timing out. A value of **0** indicates an infinite amount of time. The default value of this property is **60**.  
  
 **Use passive mode**  
 Specify whether the server or the client initiates the connection. The server initiates the connection in active mode, and the client activates the connection in passive mode. The default value of this property is **active mode**.  
  
 **Retries**  
 Specify the number of times the task attempts to make a connection. A value of **0** indicates no limit to the number of attempts.  
  
 **Chunk size (in KB)**  
 Provide a chunk size in kilobytes for transmitting data.  
  
 **Test Connection**  
 After configuring the FTP Connection Manager, confirm that the connection is viable by clicking **Test Connection**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)  
  
  

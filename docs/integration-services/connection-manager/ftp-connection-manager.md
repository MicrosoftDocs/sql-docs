---
title: "FTP Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.ftpconnectionmanager.f1"
helpviewer_keywords: 
  - "FTP connection manager"
  - "connections [Integration Services], FTP"
  - "connection managers [Integration Services], FTP"
ms.assetid: c4f43455-29ca-44ba-ac7f-ea729b1daf93
author: janinezhang
ms.author: janinez
manager: craigg
---
# FTP Connection Manager
  An FTP connection manager enables a package to connect to a File Transfer Protocol (FTP) server. The FTP task that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes uses this connection manager.  
  
 When you add an FTP connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that can be resolved as an FTP connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **FTP**.  
  
 You can configure the FTP connection manager in the following ways:  
  
-   Specify a server name and server port.  
  
-   Specify anonymous access, or provide a user name and a password for basic authentication.  
  
    > [!IMPORTANT]  
    >  The FTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
-   Set the time-out, number of retries, and the amount of data to copy at a time.  
  
-   Indicate whether the FTP connection manager uses passive or active mode.  
  
 Depending on the configuration of the FTP site to which the FTP connection manager connects, you may need to change the following default values of the connection manager:  
  
-   The server port is set to 21. You should specify the port that the FTP site listens to.  
  
-   The user name is set to "anonymous". You should provide the credentials that the FTP site requires.  
  
## Active/Passive Modes  
 An FTP connection manager can send and receive files using either active mode or passive mode. In active mode, the server initiates the data connection, and in passive mode, the client initiates the data connection.  
  
## Configuration of the FTP Connection Manager  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [FTP Connection Manager Editor](../../integration-services/connection-manager/ftp-connection-manager-editor.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## FTP Connection Manager Editor
  Use the **FTP Connection Manager Editor** dialog box to specify properties for connecting to an FTP server.  
  
> [!IMPORTANT]  
>  The FTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 To learn more about the FTP connection manager, see [FTP Connection Manager](../../integration-services/connection-manager/ftp-connection-manager.md).  
  
### Options  
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
 [FTP Task](../../integration-services/control-flow/ftp-task.md)   
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  
  
  

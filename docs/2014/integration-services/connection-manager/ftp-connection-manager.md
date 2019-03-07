---
title: "FTP Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "FTP connection manager"
  - "connections [Integration Services], FTP"
  - "connection managers [Integration Services], FTP"
ms.assetid: c4f43455-29ca-44ba-ac7f-ea729b1daf93
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# FTP Connection Manager
  An FTP connection manager enables a package to connect to a File Transfer Protocol (FTP) server. The FTP task that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes uses this connection manager.  
  
 When you add an FTP connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that can be resolved as an FTP connection at run time, sets the connection manager properties, and adds the connection manager to the `Connections` collection on the package.  
  
 The `ConnectionManagerType` property of the connection manager is set to `FTP`.  
  
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
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [FTP Connection Manager Editor](../ftp-connection-manager-editor.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../building-packages-programmatically/adding-connections-programmatically.md).  
  
## See Also  
 [FTP Task](../control-flow/ftp-task.md)   
 [Integration Services &#40;SSIS&#41; Connections](integration-services-ssis-connections.md)  
  
  

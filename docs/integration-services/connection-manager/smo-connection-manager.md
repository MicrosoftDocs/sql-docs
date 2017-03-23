---
title: "SMO Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "connections [Integration Services], SMO"
  - "SMO connection manager"
  - "connection managers [Integration Services], SMO"
ms.assetid: d273f1fb-a6a8-4f2f-a5ff-55c2e3de4723
caps.latest.revision: 21
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# SMO Connection Manager
  An SMO connection manager enables a package to connect to a SQL Management Object (SMO) server. The transfer tasks that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes use an SMO connection manager. For example, the Transfer Logins task that transfers [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins uses an SMO connection manager.  
  
 When you add an SMO connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to an SMO connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package. The **ConnectionManagerType** property of the connection manager is set to **SMOServer**.  
  
 You can configure an SMO connection manager in the following ways:  
  
-   Specify the name of a server on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.  
  
-   Select the authentication mode for connecting to the server.  
  
## Configuration of the SMO Connection Manager  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [SMO Connection Manager Editor](../../integration-services/connection-manager/smo-connection-manager-editor.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  
  
  
---
title: "WMI Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.wmiconnection.f1"
helpviewer_keywords: 
  - "connections [Integration Services], WMI"
  - "connection managers [Integration Services], WMI"
  - "WMI connection manager [Integration Services]"
ms.assetid: fbfa4ba7-3d0d-4d6b-94ad-50741a88d03d
author: janinezhang
ms.author: janinez
manager: craigg
---
# WMI Connection Manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  A WMI connection manager enables a package to use Windows Management Instrumentation (WMI) to manage information in an enterprise environment. The Web Service task that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes uses a WMI connection manager.  
  
 When you add a WMI connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to a WMI connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package. The **ConnectionManagerType** property of the connection manager is set to **WMI**.  
  
## Configuration of the WMI Connection Manager  
 You can configure a WMI connection manager in the following ways:  
  
-   Specify the name of a server.  
  
-   Specify a namespace on the server.  
  
-   Select the authentication mode for connecting to the server.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [WMI Connection Manager Editor](../../integration-services/connection-manager/wmi-connection-manager-editor.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## WMI Connection Manager Editor
  Use the **WMI Connection Manager** dialog box to specify a Microsoft Windows Management Instrumentation (WMI) connection to a server.  
  
 To learn more about the WMI connection manager, see [WMI Connection Manager](../../integration-services/connection-manager/wmi-connection-manager.md).  
  
### Options  
 **Name**  
 Provide a unique name for the connection manager.  
  
 **Description**  
 Describe the connection manager. As a best practice, describe the connection manager in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **Server name**  
 Provide the name of the server to which you want to make the WMI connection.  
  
 **Namespace**  
 Specify the WMI namespace.  
  
 **Use Windows authentication**  
 Select to use Windows Authentication. If you use Windows Authentication, you do not need to provide a user name or password for the connection.  
  
 **User name**  
 If you do not use Windows Authentication, you must provide a user name for the connection.  
  
 **Password**  
 If you do not use Windows Authentication, you must provide the password for the connection.  
  
 **Test**  
 Test the connection manager settings.  
  
## See Also  
 [Web Service Task](../../integration-services/control-flow/web-service-task.md)   
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  

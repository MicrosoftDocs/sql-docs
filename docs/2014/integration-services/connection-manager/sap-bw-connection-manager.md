---
title: "SAP BW Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 06b166a1-a9df-48ea-a0e8-9b8d6979c0a1
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# SAP BW Connection Manager
  The SAP BW connection manager is the connection manager component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW. Thus, the SAP BW connection manager provides the connectivity to an SAP Netweaver BW version 7 system that the source and destination components of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW need. (The SAP BW source and destination that are part of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW package are the only [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components that use the SAP BW connection manager.)  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 When you add an SAP BW connection manager to a package, the `ConnectionManagerType` property of the connection manager is set to `SAPBI`.  
  
## Configuring the SAP BW Connection Manager  
 You can configure the SAP BW connection manager in the following ways:  
  
-   Provide the client, user name, password, and language for the connection.  
  
-   Choose whether to connect to a single application server or to a group of load-balanced servers.  
  
-   Provide the host and system number for a single application server, or provide the message server, group, and SID for a group of load-balanced servers.  
  
-   Enable custom logging of RFC function calls for the components of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW. (This logging is separate from the optional logging that you can enable on [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.) To enable logging of RFC function calls, you specify a directory in which to store the log files that are created before and after each RFC function call. (This logging feature creates many log files in XML format. As these log files also contain all the rows of data that are transferred, these log files may consume a large amount of disk space.) If you do not select a log directory, logging is not enabled.  
  
    > [!IMPORTANT]  
    >  If the data that is transferred contains sensitive information, the log files will also contain that sensitive information.  
  
-   Use the values that you have entered to test the connection.  
  
 If you do not know all the values that are required to configure the connection manager, you might have to ask your SAP administrator.  
  
 For a walkthrough that demonstrates how to configure and use the SAP BW connection manager, source, and destination, see the white paper, [Using SQL Server 2008 Integration Services with SAP BI 7.0](https://go.microsoft.com/fwlink/?LinkID=137090). This white paper also shows how to configure the required objects in SAP BW.  
  
### Using the SSIS Designer to Configure the Source  
 For more information about the properties of the SAP BW connection manager that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [SAP BW Connection Manager Editor](../sap-bw-connection-manager-editor.md)  
  
## See Also  
 [Microsoft Connector 1.1 for SAP BW Components](../microsoft-connector-for-sap-bw-components.md)  
  
  

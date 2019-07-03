---
title: "SAP BW Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 06b166a1-a9df-48ea-a0e8-9b8d6979c0a1
f1_keywords: 
  - "sql13.dts.designer.sapbwconnectionmanager.f1"
author: janinezhang
ms.author: janinez
manager: craigg
---
# SAP BW Connection Manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The SAP BW connection manager is the connection manager component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW. Thus, the SAP BW connection manager provides the connectivity to an SAP Netweaver BW version 7 system that the source and destination components of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW need. (The SAP BW source and destination that are part of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW package are the only [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components that use the SAP BW connection manager.)  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 When you add an SAP BW connection manager to a package, the **ConnectionManagerType** property of the connection manager is set to **SAPBI**.  
  
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
  
-   [SAP BW Connection Manager Editor](../../integration-services/connection-manager/sap-bw-connection-manager-editor.md)  
  
## SAP BW Connection Manager Editor
  Use the **SAP BW Connection Manager Editor** to specify the properties to use to connect to an SAP Netweaver BW version 7 system.  
  
 The SAP BW connection manager provides connectivity to an SAP Netweaver BW 7 system for use by the SAP BW source or destination. To learn more about the SAP BW connection manager of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW, see [SAP BW Connection Manager](../../integration-services/connection-manager/sap-bw-connection-manager.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the SAP BW Connection Manager Editor**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW connection manager.  
  
2.  In the Connection Managers area on the **Control Flow** tab, do one of the following steps:  
  
    -   Double-click the SAP BW connection manager.  
  
         -or-  
  
    -   Right-click the SAP BW connection manager, and then select **Edit**.  
  
### Options  
  
> [!NOTE]  
>  If you do not know all the values that are required to configure the connection manager, you might have to ask your SAP administrator.  
  
 **Client**  
 Specify the client number of the system.  
  
 **Language**  
 Specify the language that the system uses. For example, specify **EN** for English.  
  
 **User name**  
 Specify the user name that will be used to connect to the system.  
  
 **Password**  
 Specify the password that will be used with the user name.  
  
 **Use single application server**  
 Connect to a single application server.  
  
 To connect to a group of load-balanced servers, use the **Use load balancing** option instead.  
  
 **Host**  
 If connecting to a single application server, specify the host name.  
  
> [!NOTE]  
>  This option is only available if you have selected the **Use single application server** option.  
  
 **System number**  
 If connecting to a single application server, specify the system number.  
  
> [!NOTE]  
>  This option is only available if you have selected the **Use single application server** option.  
  
 **Use load balancing**  
 Connect to a group of load-balanced servers.  
  
 To connect to a single application server, use the **Use single application server** option instead.  
  
 **Message server**  
 If connecting to a group of load-balanced servers, specify the name of the message server.  
  
> [!NOTE]  
>  This option is only available if you have selected the **Use load balancing** option.  
  
 **Group**  
 If connecting to a group of load-balanced servers, specify the name of the server group name.  
  
> [!NOTE]  
>  This option is only available if you have selected the **Use load balancing** option.  
  
 **SID**  
 If connecting to a group of load-balanced servers, specify the System ID for the connection.  
  
> [!NOTE]  
>  This option is only available if you have selected the **Use load balancing** option.  
  
 **Log directory**  
 Enable logging for the components of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW.  
  
 To enable logging, specify a directory for the log files that are created before and after each RFC function call. (This logging feature creates many log files in XML format. As these log files also contain all the rows of data that are transferred, these log files may consume a large amount of disk space.)  
  
> [!IMPORTANT]  
>  If the data that is transferred contains sensitive information, the log files will also contain that sensitive information.  
  
 To specify the log directory, you can either enter the directory path manually, or click **Browse** and browse to the log directory.  
  
 If you do not select a log directory, logging is not enabled.  
  
 **Browse**  
 Browse to select a folder for the log directory.  
  
 **Test Connection**  
 Test the connection using the values that you have provided. After clicking **Test Connection**, a message box appears and indicates whether the connection succeeded or failed.  
  
## See Also  
 [Microsoft Connector for SAP BW Components](../../integration-services/microsoft-connector-for-sap-bw-components.md)  
  
  

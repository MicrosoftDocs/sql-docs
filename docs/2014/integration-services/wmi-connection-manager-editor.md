---
title: "WMI Connection Manager Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.wmiconnection.f1"
helpviewer_keywords: 
  - "WMI Connection Manager Editor"
ms.assetid: 0ef2c913-0779-4a07-989e-3361cd83170b
author: douglaslms
ms.author: douglasl
manager: craigg
---
# WMI Connection Manager Editor
  Use the **WMI Connection Manager** dialog box to specify a Microsoft Windows Management Instrumentation (WMI) connection to a server.  
  
 To learn more about the WMI connection manager, see [WMI Connection Manager](connection-manager/wmi-connection-manager.md).  
  
## Options  
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
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [WMI Provider for Configuration Management Concepts](../relational-databases/wmi-provider-configuration/wmi-provider-for-configuration-management.md)   
 [WMI Provider for Server Events Concepts](../relational-databases/wmi-provider-server-events/wmi-provider-for-server-events-concepts.md)  
  
  

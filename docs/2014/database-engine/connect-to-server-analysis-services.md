---
title: "Connect to Server (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.connection.login.analysisserver.f1"
ms.assetid: 7e277d22-8d4b-422e-8882-7c5dd7a6d915
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Connect to Server (Analysis Services)
  Use this dialog to view or specify options when connecting to [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
## Options  
 **Server type**  
 When registering a server from Object Explorer, select the type of server to connect to: [!INCLUDE[ssDE](../includes/ssde-md.md)], Analysis Services, Reporting Services, or Integration Services. The rest of the dialog shows only the options that apply to the selected server type. When registering a server from Registered Servers, the **Server type** box is read-only, and matches the type of server displayed in the Registered Servers component. To register a different type of server, select [!INCLUDE[ssDE](../includes/ssde-md.md)], Analysis Services, Reporting Services, or Integration Services from the Registered Servers toolbar before starting to register a new server.  
  
 **Server name**  
 Select the server instance to connect to. The server instance last connected to is displayed by default.  
  
 **Authentication**  
 The following authentication modes are supported when connecting to an instance of Analysis Services: [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows Authentication.  
  
 **Windows Authentication Mode (Windows Authentication)**  
 **Windows Authentication** mode allows a user to connect through a Windows user account.  
  
 **User name**  
 This option is not available in this release. Enter the user name to connect with. This option is only available if you have selected to connect using **Windows Authentication**.  
  
 **Password**  
 This option is not available in this release. Enter the password for the login. This option is only editable if you have selected to connect using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication.  
  
 **Connect**  
 Click to connect to the server selected above.  
  
 **Options**  
 Click to display additional server connection options, such as registering a server and remembering the password.  
  
  

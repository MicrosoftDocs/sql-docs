---
title: "Connect to Server (Integration Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.connection.login.dtsserver.f1"
ms.assetid: 5be897bd-f36c-4c6a-a91a-13d0d016f8b6
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Connect to Server (Integration Services)
  Use this dialog to view or specify options when connecting to [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].  
  
## Options  
 **Server type**  
 When registering a server from Object Explorer, select the type of server to connect to: [!INCLUDE[ssDE](../includes/ssde-md.md)], Analysis Services, Reporting Services, or Integration Services. The rest of the dialog shows only the options that apply to the selected server type. When registering a server from Registered Servers, the Server type box is read-only, and matches the type of server displayed in the Registered Servers component. To register a different type of server, select [!INCLUDE[ssDE](../includes/ssde-md.md)], Analysis Services, Reporting Services, or Integration Services from the Registered Servers toolbar before starting to register a new server.  
  
 **Server name**  
 Select the server to connect to. The server instance last connected to is displayed by default.  
  
> [!NOTE]  
>  Do not use *\<servername>*\\*\<instancename>*, because [!INCLUDE[ssIS](../includes/ssis-md.md)] does not support multiple instances on a computer.  
  
 **Authentication**  
 Only [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows Authentication is available for [!INCLUDE[ssIS](../includes/ssis-md.md)]. Windows Authentication mode allows a user to connect through a Windows user account.  
  
 **User name**  
 This option is not available because only Windows Authentication is available for [!INCLUDE[ssIS](../includes/ssis-md.md)].  
  
 **Password**  
 This option is not available because only Windows Authentication is available for [!INCLUDE[ssIS](../includes/ssis-md.md)].  
  
 **Connect**  
 Click to connect to the server selected above.  
  
 **Options**  
 Click to display additional server connection options, such as registering a server and remembering the password.  
  
  

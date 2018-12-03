---
title: "Connect to an Instance From Object Explorer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
ms.assetid: 9803a8a0-a8f1-4b65-87b8-989b06850194
author: stevestein
ms.author: sstein
manager: craigg
---
# Connect to an Instance From Object Explorer
  To manage objects by using Object Explorer, you must first connect Object Explorer to the instance that contains the objects. You can connect Object Explorer to multiple instances at the same time.  
  
## Connecting Object Explorer to a Server  
 To use Object Explorer you must first connect to a server. Click **Connect** on the Object Explorer toolbar and choose the type of server from the drop-down list. The **Connect to Server** dialog box opens. To connect, you must provide at least the name of the server and the correct authentication information.  
  
## Optional Object Explorer Connection Settings  
 When connecting to a server, you can specify additional connection information in the **Connect to Server** dialog box. The **Connect to Server** dialog box will retain the last used settings, and new connections, such as new code editor windows, will use those settings.  
  
 To specify optional connection settings, follow these steps:  
  
1.  Click **Connect** on the Object Explorer toolbar, and click the type of server to connect to. The **Connect to Server** dialog box appears.  
  
2.  In the **Server Name** box, type the name of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
3.  Click **Options**. The **Connect to Server** dialog box displays additional options.  
  
4.  Click the **Connection Properties** tab to configure the additional settings. The settings that are available vary depending upon the server type. The following settings are available for the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
    |Setting|Description|  
    |-------------|-----------------|  
    |**Connect to database**|Choose from the available databases on the server. This list will only show databases that you have permission to view.|  
    |**Network protocol**|Select from Shared Memory, TCP/IP, or Named Pipes.|  
    |**Network packet size**|Configure in bytes. The default setting is 4096 bytes.|  
    |**Connection timeout**|Configure in seconds. The default setting is 15 seconds.|  
    |**Execution timeout**|Configure in seconds. The default setting (0) indicates that execution will never time out.|  
    |**Encrypt connection**|Forces encryption.|  
  
5.  To add the specified server to your list of registered servers, click the **Registered Server** tab, click on the location where you want the new server to appear, and then complete the connection.  
  
> [!NOTE]  
>  Use the **Additional Connection Parameters** page to add more connection parameters to the connection string. For more information, see [Connect to Server &#40;Additional Connection Parameters Page&#41;](../../database-engine/connect-to-server-additional-connection-parameters-page.md).  
  
  

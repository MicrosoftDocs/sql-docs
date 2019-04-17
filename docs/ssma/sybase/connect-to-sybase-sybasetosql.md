---
title: "Connect to Sybase (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 524f95ef-10bd-497c-84ca-c06a0ae794fb
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Connect to Sybase (SybaseToSQL)
Use the **Connect to Sybase** dialog box to connect to the Sybase Adaptive Server Enterprise (ASE) instance that you want to migrate.  
  
To access this dialog box, on the **File** menu, select **Connect to Sybase**. If you have previously connected, the command is **Reconnect to Sybase**.  
  
## Options  
**Provider**  
Select any of the installed Provider on the machine for connecting to the Sybase Server.  
  
**Mode**  
Select either standard or advanced connection mode. In standard mode, you enter or select values for the server name, port, user name, and password. In advanced mode, you provide a connection string.  
  
**Server name**  
Enter or select the name or IP address of the Adaptive Server. The default server name is the same as the computer name. This is a standard mode option.  
  
**Server port**  
If you are using a non-default port for connections to ASE, enter the port number. The default port number is 5000. This is a standard mode option.  
  
**User name**  
Enter the user name that is used to connect to ASE. This is a standard mode option.  
  
**Password**  
Enter the password for the user name. This is a standard mode option.  
  
**Connection string**  
Enter the full connection string for the connection to ASE.  
  
Connection strings consist of parameter name and value pairs. The names of the parameters vary with the provider being used.  
  
**Connection parameters for various providers are as follows:**  
  
1.  Connection parameters for **OLE DB Provider**  
  
    |Setting|Sybase 12.5 Parameter|Sybase 15 Parameter|  
    |-----------|-------------------------|-----------------------|  
    |Server name|Server Name|Server|  
    |Port|Server Port Address|Port|  
    |User name|User ID|User ID|  
    |Password|Password|Password|  
    |Provider|Provider|Provider|  
  
    For Sybase ASE 12.5, an example connection string is as follows:  
  
    `Server Name=sybserver;User ID=MyUserID;Password=MyP@$$word;Provider=Sybase.ASEOLEDBProvider;`  
  
    For Sybase ASE 15, an example connection string is as follows:  
  
    `Server=sybserver;User ID=MyUserID;Password=MyP@$$word;Provider=ASEOLEDB;Port=5000;`  
  
2.  Connection parameters for **ODBC Provider**  
  
    |Setting|Sybase 12.5/15 Parameter|  
    |-----------|-----------------------------|  
    |Driver Name|driver|  
    |Server Name|Server|  
    |User Name|Uid|  
    |Password|Pwd|  
    |Port Number|Port|  
  
    For Sybase ASE 12.5 or 15, an example connection string is as follows:  
  
    `driver=Adaptive Server Enterprise;Server=sybserver;uid=MyUserID;pwd=MyP@$$word;Port=5000;`  
  
3.  Connection parameters for **ADO.NET Provider**  
  
    |Setting|Sybase 12.5/15 Parameter|  
    |-----------|-----------------------------|  
    |Server Name|Server|  
    |User Name|Uid|  
    |Password|Pwd|  
    |Port Number|Port|  
  
    An example of the Connection string for ADO.NET Provider is as follow:  
  
    `Server=sybserver;Port=5000;uid=MyUserID;pwd=MyP@$$word;`  
  
For more information, see the ASE documentation.  
  
This is an advanced mode option.  
  

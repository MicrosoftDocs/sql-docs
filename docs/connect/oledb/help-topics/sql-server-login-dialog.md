---
title: "SQL Server Login Dialog Box (OLE DB) | Microsoft Docs"
description: "Using SQL Server Login Dialog Box"
ms.custom: ""
ms.date: "01/21/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: reference
ms.author: "v-beaziz"
author: bazizi
---
# SQL Server Login dialog box
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

When you attempt to connect without specifying enough information, the OLE DB driver displays the **SQL Server Login** dialog box.

> [!NOTE]  
> SQL Server Login Dialog prompting behavior is controlled by the `DBPROP_INIT_PROMPT` initialization property. For more information, see:
> - [Initialization and Authorization Properties](../ole-db-data-source-objects/initialization-and-authorization-properties.md)
> - [OLE DB Programmer's Guide](https://go.microsoft.com/fwlink/?linkid=2067702)

![Screenshot of SQL Server Login Dialog Box](../media/sql-server-login-dialog.png)

|Option|Description|
|---   |---        |
|Server|The name of an instance of SQL Server on your network. Select a server\instance name from the list, or type the server\instance name in the **Server** box. Optionally, you can create a server alias on the client computer using **SQL Server Configuration Manager**, and type that name in the **Server** box. <br/><br/>You can enter "(local)" when you are using the same computer as SQL Server. You can then connect to a local instance of SQL Server, even when running a non-networked version of SQL Server.<br/><br/>For more information about server names for different types of networks, see [SQL Server Installation](https://go.microsoft.com/fwlink/?linkid=2067541).|
|Authentication Mode|You can select the following authentication options from the drop-down list:<br/><ul><li>`Windows Authentication:` Authentication to SQL Server using the currently logged-in user's Windows account credentials.</li><li>`SQL Server Authentication:` Authentication to SQL Server using login ID and password.</li><li>`Active Directory - Integrated:` Integrated authentication using the currently logged-in user's Windows account credentials.</li><li>`Active Directory - Password:` Active Directory authentication using login ID and password.</li></ul>|
|Server SPN|If you use a trusted connection, you can specify a service principal name (SPN) for the server.|
|Login ID|Specifies the login ID to use for the connection. The Login ID text box is only enabled if `Authentication Mode` is set to `SQL Server Authentication` or `Active Directory - Password`.|
|Password|Specifies the password used for the connection. The password text box is only enabled if `Authentication Mode` is set to `SQL Server Authentication` or `Active Directory - Password`.|
|Options|Displays or hides the **Options** group. The **Options** button is enabled if **Server** has a value.|
|Change Password|When checked, enables **New Password** and **Confirm New Password** text boxes.|
|New Password|Specifies the new password.|
|Confirm New Password|Specifies the new password a second time, for confirmation.|
|Database|Select or type the default database to use on the connection. This setting overrides the default database specified for the login on the server. If no database is specified, the connection uses the default database specified for the login on the server.|
|Mirror Server|Specifies the name of the failover partner of the database to be mirrored.|
|Mirror SPN|Optionally, you can specify an SPN for the mirror server. The SPN for the mirror server is used for mutual authentication between client and server.|
|Language|Specifies the national language to use for SQL Server system messages. The computer running SQL Server must have the language installed. This setting overrides the default language specified for the login on the server. If no language is specified, the connection uses the default language specified for the login on the server.|
|Application Name|Specifies the application name to be stored in the **program_name** column in the row for this connection in **sys.sysprocesses**.|
|Workstation ID|Specifies the workstation ID to be stored in the **hostname** column in the row for this connection in **sys.sysprocesses**.|
|Use strong encryption for data|When checked, data that is passed through the connection will be encrypted.|
|Trust server certificate|When checked, the server's certificate will be validated. Server's certificate must have the correct hostname of the server and issued by a trusted certificate authority.|

> [!NOTE]  
> When using `Windows Authentication` or `SQL Server Authentication` modes, **Trust server certificate** is considered only when the **Use strong encryption for data** option is enabled.

## See Also
[Using Azure Active Directory with the OLE DB Driver](../features/using-azure-active-directory.md)
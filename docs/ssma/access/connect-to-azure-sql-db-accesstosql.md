---
title: "Connect To Azure SQL DB (AccessToSQL) | Microsoft Docs"
ms.prod: sql
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords:
  - "Connect to SQL Azure dialog box"
ms.assetid: bf44b236-d9be-41ae-a5fd-bd73038e505f
author: Shamikg
ms.author: Shamikg
manager: craigg
---
# Connect To Azure SQL DB (AccessToSQL)
Use the Connect to SQL Azure dialog box to connect to the SQL Azure database that you want to migrate.  
  
To access this dialog box, on the **File** menu, select **Connect to SQL Azure**. If you have previously connected, the command is **Reconnect to SQL Azure.**  
  
## Options  
**Server Name**  
  
Select or enter the Server Name for connecting to SQL Azure.  
  
**Database**  
  
Select, enter or **Browse** the Database name.  
  
> [!IMPORTANT]  
> SSMA for Access does not support connection to master database in SQL Azure.  
  
**User name**  
  
Enter the user name that SSMA will use to connect to the SQL Azure database  
  
**Password**  
  
Enter the password for the user name.  
  
**Encrypt**  
  
SSMA recommends encrypted connection to SQL Azure.  
  
## Create Azure Database  
To create a new azure database, follow the following steps  
  
1.  click on browse button that is present in the Connect to SQL Azure dialog box  
  
2.  If there are no databases, two menu items appear  
  
    1.  **(no databases found)** which is disabled and grayed out all the time  
  
    2.  **Create new database** which is always enabled, enabling the user to create a new azure database on SQL Azure account. Upon clicking this menu item, create azure database dialog box is present with database name and size.  
  
3.  At the time of database creation, these two parameters is given as input.  
  
    1.  **Database Name:** Enter the Database name.  
  
    2.  **Database Size   :** Select the Database size that you need to create in SQL Azure account.  
  

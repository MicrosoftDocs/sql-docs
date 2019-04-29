---
title: "Connect to Azure SQL DB  (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 96538007-1099-40c8-9902-edd07c5620ee
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Connect to Azure SQL DB  (SybaseToSQL)
Use the Connect to Azure SQL DB dialog box to connect to the Azure SQL DB database that you want to migrate.  
  
To access this dialog box, on the **File** menu, select **Connect to Azure SQL DB**. If you have previously connected, the command is **Reconnect to Azure SQL DB.**  
  
## Options  
**Server Name**  
  
Select or enter the Server Name for connecting to Azure SQL DB.  
  
**Database**  
  
Select, enter or **Browse** the Database name.  
  
> [!IMPORTANT]  
> SSMA for Sybase does not support connection to master database in Azure SQL DB.  
  
**User name**  
  
Enter the user name that SSMA will use to connect to the Azure SQL DB database  
  
**Password**  
  
Enter the password for the user name.  
  
**Encrypt**  
  
SSMA recommends encrypted connection to Azure SQL DB.  
  
## Create Azure Database  
If there are no databases in the Azure SQL DB account, you can create the very first database.  
  
To create a new database for the very first time, follow the following steps  
  
1.  Click on Browse button that is present in the Connect to Azure SQL DB dialog box  
  
2.  If there are no databases, the following two menu items appear.  
  
    1.  **(no databases found)** which is disabled and grayed out all the time  
  
    2.  **Create new database** which is enabled only when there are no databases on Azure SQL DB account. Upon clicking this menu item, Create Azure Database dialog box is present with database name and size.  
  
3.  At the time of database creation, the following two parameters are given as input:  
  
    1.  **Database Name:** Enter the Database name.  
  
    2.  **Database Size:** Select the Database size that you need to create in Azure SQL DB account.  
  

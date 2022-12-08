---
description: "Connect to Azure SQL Database  (SybaseToSQL)"
title: "Connect to Azure SQL Database  (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 96538007-1099-40c8-9902-edd07c5620ee
author: cpichuka 
ms.author: cpichuka 
---
# Connect to Azure SQL Database  (SybaseToSQL)
Use the Connect to Azure SQL Database dialog box to connect to the Azure SQL Database database that you want to migrate.  
  
To access this dialog box, on the **File** menu, select **Connect to Azure SQL Database**. If you have previously connected, the command is **Reconnect to Azure SQL Database.**  
  
## Options  
**Server Name**  
  
Select or enter the Server Name for connecting to Azure SQL Database.  
  
**Database**  
  
Select, enter or **Browse** the Database name.  
  
> [!IMPORTANT]  
> SSMA for Sybase does not support connection to master database in Azure SQL Database.  
  
**User name**  
  
Enter the user name that SSMA will use to connect to the Azure SQL Database database  
  
**Password**  
  
Enter the password for the user name.  
  
**Encrypt**  
  
SSMA recommends encrypted connection to Azure SQL Database.  
  
## Create Azure Database  
If there are no databases in the Azure SQL Database account, you can create the very first database.  
  
To create a new database for the very first time, follow the following steps  
  
1.  Click on Browse button that is present in the Connect to Azure SQL Database dialog box  
  
2.  If there are no databases, the following two menu items appear.  
  
    1.  **(no databases found)** which is disabled and grayed out all the time  
  
    2.  **Create new database** which is enabled only when there are no databases on Azure SQL Database account. Upon clicking this menu item, Create Azure Database dialog box is present with database name and size.  
  
3.  At the time of database creation, the following two parameters are given as input:  
  
    1.  **Database Name:** Enter the Database name.  
  
    2.  **Database Size:** Select the Database size that you need to create in Azure SQL Database account.  
  

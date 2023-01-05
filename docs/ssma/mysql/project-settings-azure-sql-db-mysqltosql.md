---
description: "Project Settings (Azure SQL Database) (MySQLToSQL)"
title: "Project Settings (Azure SQL Database) (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 8c06420a-533b-4de0-948d-a0c6b368c544
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.mysql.projectsettingsqlazure.f1"
---
# Project Settings (Azure SQL Database) (MySQLToSQL)
The SQL Azure project settings let you configure the Azure SQL Database suffix to be added in the connection dialog and also allow implementing heartbeat mechanism in SQL Azure connection.  
  
The SQL Azure pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.  
  
-   Use the Project Settings dialog box to set configuration options for the current project. To access the SQL Azure settings, on the **Tools** menu, select **Project Settings**, click **General** at the bottom of the left pane, and then select **SQL Azure**.  
  
-   Use the Default Project Settings dialog box to set configuration options for all projects. To access the SQL Azure settings, on the **Tools** menu, select **DefaultProject Settings**, select migration project type as SQL Azure from **Migration Target Version** drop down to access the settings in SQL Azure pane, click **General** at the bottom of the left pane, and then select **SQL Azure**.  
  
## Options  
  
## Connectivity  
**Heartbeat Interval**  
  
Specifies a time interval to be used for heartbeat mechanism to keep the SQL Azure connection alive in 'minutes : seconds' format.  
  
**Default Value**:'4:45'  
  
The value should be specified in 'm:ss' format (for example, '4:45' or '0:50').  
  
**SQL Azure Server Suffix**  
  
Specifies the SQL Azure server suffix  
  
**Default Value**: 'database.windows.net'.  
  

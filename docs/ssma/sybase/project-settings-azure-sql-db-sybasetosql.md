---
title: "Project Settings (Azure SQL DB ) (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 57002374-0d4d-43c1-b4e9-cbec02355a9c
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Project Settings (Azure SQL DB ) (SybaseToSQL)
The Azure SQL DB project settings let you configure the Azure SQL DB database suffix to be added in the connection dialog and also allow implementing heartbeat mechanism in Azure SQL DB connection.  
  
The Azure SQL DB pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.  
  
-   Use the Project Settings dialog box to set configuration options for the current project. To access the Azure SQL DB settings, on the **Tools** menu, select **Project Settings**, click **General** at the bottom of the left pane, and then select **Azure SQL DB**.  
  
-   Use the Default Project Settings dialog box to set configuration options for all projects. To access the Azure SQL DB settings, on the **Tools** menu, select **DefaultProject Settings**, click **General** at the bottom of the left pane, and then select **Azure SQL DB**.  
  
## Connectivity  
**Heartbeat Interval**  
  
Specifies a time interval to be used for heartbeat mechanism to keep the Azure SQL DB connection alive in 'minutes : seconds' format.  
  
**Default Value**:'4:45'  
  
The value should be specified in 'm:ss' format (for example, '4:45' or '0:50').  
  
**Azure SQL DB Server Suffix**  
  
Specifies a Azure SQL DB server suffix  
  
**Default Value**: 'database.windows.net'.  
  

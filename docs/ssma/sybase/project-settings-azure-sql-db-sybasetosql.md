---
description: "Project Settings (Azure SQL Database ) (SybaseToSQL)"
title: "Project Settings (Azure SQL Database ) (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 57002374-0d4d-43c1-b4e9-cbec02355a9c
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.sybase.projectsettingsqlazure.f1"

---
# Project Settings (Azure SQL Database ) (SybaseToSQL)
The Azure SQL Database project settings let you configure the Azure SQL Database database suffix to be added in the connection dialog and also allow implementing heartbeat mechanism in Azure SQL Database connection.  
  
The Azure SQL Database pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.  
  
-   Use the Project Settings dialog box to set configuration options for the current project. To access the Azure SQL Database settings, on the **Tools** menu, select **Project Settings**, click **General** at the bottom of the left pane, and then select **Azure SQL Database**.  
  
-   Use the Default Project Settings dialog box to set configuration options for all projects. To access the Azure SQL Database settings, on the **Tools** menu, select **DefaultProject Settings**, click **General** at the bottom of the left pane, and then select **Azure SQL Database**.  
  
## Connectivity  
**Heartbeat Interval**  
  
Specifies a time interval to be used for heartbeat mechanism to keep the Azure SQL Database connection alive in 'minutes : seconds' format.  
  
**Default Value**:'4:45'  
  
The value should be specified in 'm:ss' format (for example, '4:45' or '0:50').  
  
**Azure SQL Database Server Suffix**  
  
Specifies an Azure SQL Database server suffix  
  
**Default Value**: 'database.windows.net'.  
  

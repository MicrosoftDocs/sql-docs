---
title: "Step 6: Disconnect from the Data Source | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "application process [ODBC], disconnecting from data source"
  - "data sources [ODBC], disconnecting"
  - "disconnecting from data source [ODBC]"
ms.assetid: 6ad759ba-4721-4d8f-9b26-de976d4fc1a0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step 6: Disconnect from the Data Source
The final step is to disconnect from the data source, as shown in the following illustration. First, the application frees any statement handles by calling **SQLFreeHandle**. For more information, see [Freeing a Statement Handle](../../../odbc/reference/develop-app/freeing-a-statement-handle-odbc.md).  
  
 ![Shows disconnecting from a data source](../../../odbc/reference/develop-app/media/pr17.gif "pr17")  
  
 Next, the application disconnects from the data source with **SQLDisconnect** and frees the connection handle with **SQLFreeHandle**. For more information, see [Disconnecting from a Data Source or Driver](../../../odbc/reference/develop-app/disconnecting-from-a-data-source-or-driver.md).  
  
 Finally, the application frees the environment handle with **SQLFreeHandle** and unloads the Driver Manager. For more information, see [Allocating the Environment Handle](../../../odbc/reference/develop-app/allocating-the-environment-handle.md).

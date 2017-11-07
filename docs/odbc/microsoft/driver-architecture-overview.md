---
title: "Driver Architecture Overview | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Visual FoxPro ODBC driver [ODBC], architecture"
  - "FoxPro ODBC driver [ODBC], architecture"
ms.assetid: ef5a91cd-158e-40bf-b5a8-8ba535c4705e
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Driver Architecture Overview
The Microsoft Visual FoxPro ODBC Driver is a 32-bit driver that enables you to open and query a Microsoft Visual FoxPro database or FoxPro tables through the Open Database Connectivity (ODBC) interface. You can access FoxPro data using the following types of applications:  
  
-   A Microsoft Office application, such as Microsoft Excel or Microsoft Word, that uses Microsoft Query to communicate with ODBC.  
  
-   An application written in Microsoft Visual C++ or C that uses the ODBC SDK API.  
  
-   An application written in Microsoft Visual Basic or Microsoft Visual Basic for Applications.  
  
 In each case, the request for information uses the ODBC API. The ODBC Driver Manager works with the Visual FoxPro ODBC Driver to open and retrieve data from FoxPro tables and databases.  
  
 The architecture is represented in the following diagram:  
  
 ![Shows the ODBC Driver architecture](../../odbc/microsoft/media/vfparch.gif "vfparch")  
  
 This section contains the following topics.  
  
-   [Visual FoxPro Terminology](../../odbc/microsoft/visual-foxpro-terminology.md)  
  
-   [Installing and Configuring the Visual FoxPro ODBC Driver](../../odbc/microsoft/installing-and-configuring.md)  
  
-   [Using the Visual FoxPro ODBC Driver](../../odbc/microsoft/using-the-visual-foxpro-odbc-driver.md)
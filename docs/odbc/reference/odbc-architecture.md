---
title: "ODBC Architecture | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC architecture [ODBC], components"
  - "ODBC architecture [ODBC]"
ms.assetid: 2604f492-587b-4a51-9876-59a7870b3ef2
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC Architecture
The ODBC architecture has four components:  
  
-   **Application** Performs processing and calls ODBC functions to submit SQL statements and retrieve results.  
  
-   **Driver Manager** Loads and unloads drivers on behalf of an application. Processes ODBC function calls or passes them to a driver.  
  
-   **Driver** Processes ODBC function calls, submits SQL requests to a specific data source, and returns results to the application. If necessary, the driver modifies an application's request so that the request conforms to syntax supported by the associated DBMS.  
  
-   **Data Source** Consists of the data the user wants to access and its associated operating system, DBMS, and network platform (if any) used to access the DBMS.  
  
 Note the following points about the ODBC architecture. First, multiple drivers and data sources can exist, which allows the application to simultaneously access data from more than one data source. Second, the ODBC API is used in two places: between the application and the Driver Manager, and between the Driver Manager and each driver. The interface between the Driver Manager and the drivers is sometimes referred to as the *service provider interface,* or *SPI*. For ODBC, the application programming interface (API) and the service provider interface (SPI) are the same; that is, the Driver Manager and each driver have the same interface to the same functions.  
  
 This section contains the following topics.  
  
-   [Applications](../../odbc/reference/applications.md)  
  
-   [The Driver Manager](../../odbc/reference/the-driver-manager.md)  
  
-   [Drivers](../../odbc/reference/drivers.md)  
  
-   [Data Sources](../../odbc/reference/data-sources.md)

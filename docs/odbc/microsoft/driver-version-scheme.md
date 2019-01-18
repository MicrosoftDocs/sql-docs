---
title: "Driver Version Scheme | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC driver for Oracle [ODBC], versions"
ms.assetid: e4a8d9d7-8aba-48ab-8be6-1a6129adfb8f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Driver Version Scheme
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 The following table lists all released versions of the Microsoft ODBC Driver for Oracle.  
  
|Driver version|Build number|Availability history|  
|--------------------|------------------|--------------------------|  
|1.0|2.00.6235|Visual C++ 4.2 and Visual Basic 5.0, Enterprise Edition|  
|2.0|2.73.7269|Visual Studio 97 and MDAC 1.5a|  
|2.0 updated|2.73.7283.01|IIS 4.0|  
|2.0 updated|2.73.7283.03|MDAC 1.5b and 1.5c|  
|2.0 updated|2.73.7356|ODBC 3.5 SDK|  
|2.5|2.573.2927|Visual Studio 6.0 and MDAC 2.0|  
|2.5 updated|2.573.3513|SQL Server 7.0<br /><br /> SQL Server 6.5 SP5|  
  
 Build 2.00.6235 (version 1) was the first release of the Microsoft ODBC Driver for Oracle. After the release of the first version, a new naming convention was adopted.  
  
 For example, 2.73.7283.03 can be divided into the following distinct components:  
  
-   2 = The version number.  
  
-   73 = The version of Oracle Server for which the driver was designed.  
  
-   7283.03 = The build number of the driver.  
  
> [!NOTE]  
>  With release 2.573.2973, the naming convention has led to some confusion that 2.573 is an earlier release than 2.73, but each section of the build number should be considered individually. The number 573 is larger than 73, so it is a newer version. Also, "2.5" indicates the driver's version number.

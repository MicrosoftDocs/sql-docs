---
title: "Installing the Software (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC driver for Oracle [ODBC], installing"
  - "installing ODBC driver for Oracle [ODBC]"
ms.assetid: dfac8ade-eebe-4ebe-a199-feb740ed5bae
author: MightyPen
ms.author: genemi
manager: craigg
---
# Installing the Software (ODBC)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 The ODBC Driver for Oracle is one of the data access components. It accompanies other ODBC components, such as the ODBC Data Source Administrator, and should already be installed. The driver also can be found under "Drivers and Other Downloads" on the Microsoft Product Support Services Online Web site at [www.microsoft.com](www.microsoft.com).  
  
 Network software must be installed according to its own documentation. The ODBC Driver for Oracle requires no special installation considerations as long as the network software is supported.  
  
 Oracle software must be installed according to its own documentation. The ODBC Driver for Oracle generally requires no special installation considerations as long as the driver supports the version. However, to keep products compatible, install the ODBC Driver for Oracle last to ensure you have the latest version of the driver. Oracle maintains a public FTP site where it posts, among other things, patches to the Oracle server products and the client component that ships with the server products. These patches are required for the proper functioning of several Microsoft products and technologies. For more information about this site, see [Oracle Software Patches](../../odbc/microsoft/oracle-software-patches.md).  
  
> [!CAUTION]  
>  Installing Oracle software over MDAC/Windows DAC may overwrite current versions of MDAC. If problems arise using ODBC components, reinstall MDAC.

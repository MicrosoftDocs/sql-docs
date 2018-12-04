---
title: "Oracle Software Patches | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC driver for Oracle [ODBC], Oraclesoftware patches"
  - "Oracle software patches [ODBC]"
ms.assetid: 1275157b-f4e1-4c24-b273-c02555e261c2
author: MightyPen
ms.author: genemi
manager: craigg
---
# Oracle Software Patches
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 Patches for the Oracle server products and its client component are required for the proper functioning of several Microsoft products and technologies, including the Microsoft ODBC Driver for Oracle, the Microsoft OLE DB Provider for Oracle, Internet Information Services (IIS), Component Services (or Microsoft Transaction Server, if you are using Windows NT), and so forth.  
  
> [!NOTE]  
>  The following instructions may not be completely accurate because the Oracle FTP site is subject to change.  
  
### To download the Oracle software patches  
  
1.  Connect to the public FTP site at oracle-ftp.oracle.com. The user ID is "anonymous" and the password is your e-mail address.  
  
2.  Navigate to the following directory: /server/wgt_tech/server/windowsNT.  
  
3.  To download patches most relevant for Windows 95, Windows 98 and Windows NT/Windows 2000, navigate to the subdirectory for your version of Oracle - 7.3 or 8.0. The two subdirectories are /73patchsets and /80patchsets.  
  
4.  To download patches for Oracle's network technology, either SQL*Net or Net8, navigate to the following directory: /network.  
  
 Accessing this FTP site from your Web browser might not work. If you experience problems, try using a "traditional" FTP client or use the DOS command prompt.  
  
> [!NOTE]  
>  Because Oracle fixes bugs in current versions and then retrofits them to earlier versions using software patches, it is recommended that you download the latest patch available. This is especially true for the Oracle Server Client components. If you have questions about installing these patches, contact Oracle Support.

---
title: "Release Notes (ODBC Driver for SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "07/03/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: b8459ed8-625e-4d8b-891c-e7e78c9977cc
author: MightyPen
ms.author: v-jizho2
manager: kenvh
---
# Release Notes
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

  Release Notes for Microsoft ODBC Driver for SQL Server on Windows.  

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 17.2 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows

**Features Added**:

Data Classification for Azure SQL Database and SQL Server, for more information see [Data Classification](../data-classification.md)

Support for UTF-8 server encoding

[Bug fixes](../bug-fixes.md)

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 17.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows

**Features Added**:

Support for `SQL_COPT_SS_CEKCACHETTL` and `SQL_COPT_SS_TRUSTEDCMKPATHS` connection attributes (For more information, see [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md))
- `SQL_COPT_SS_CEKCACHETTL` Allows controlling the time that the local cache of Column Encryption Keys exists, as well as flushing it
- `SQL_COPT_SS_TRUSTEDCMKPATHS` Allows the application to restrict AE operations to only use the specified list of Column Master Keys


Azure Active Directory Interactive Authentication Support

[Bug fixes](../bug-fixes.md)


## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 17 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows

**Features Added**:

Always Encrypted support for BCP API

New connection string attribute UseFMTOnly causes driver to use legacy metadata in special cases requiring temp tables.

Support for Azure SQL Managed Instance (Extended Private Preview). 
> [!NOTE]
> There are a number of differences when using Managed Instance:
> -   FILESTREAM is not supported 
> -   Local filesystem access is not supported, but required for things like tracefiles 
> -   Create UDT from local path is not supported 
> -   Windows Integrated Authentication is not supported 
> -   DTC is not supported 
> -   'sa' account is not present (default account is called 'cloudSA')
> -   TDS token ERROR (0xAA) returns incorrect server name
> -   Special characters in database name are not supported 
> -   ALTER DATABASE [dbname1] MODIFY NAME = [dbname2] is not supported
> -   The error messages are always shown in English, regardless of language settings (same as Azure) 
  

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows  
 ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] adds support for [Always Encrypted](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md) and [Azure Active Directory](../../../connect/odbc/using-azure-active-directory.md) when used in conjunction with Microsoft SQL Server 2016.  Corresponding connection pooling keywords/attributes are described in [Driver Aware Connection Pooling in the ODBC Driver for SQL Server](../../../connect/odbc/windows/driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md).

 ## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows  
 ODBC Driver 13 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] includes previous functionality from ODBC Driver 11 for SQL Server and adds support for Microsoft SQL Server 2016.

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows  
 ODBC Driver 11 for SQL Server contains new [features](./features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md) as well as all the features that shipped with ODBC in SQL Server 2012 Native Client.  

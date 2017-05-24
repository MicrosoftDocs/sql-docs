---
title: "Frequently Asked Questions (FAQ) for ODBC macOS | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology:
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Frequently Asked Questions (FAQ) for ODBC macOS
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The following are answers to questions about the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on macOS.  

## Frequently Asked Questions  
**How do existing ODBC applications on macOS work with the driver?**
You should be able to compile and run the ODBC applications that you have been compiling and running on macOS using other drivers.  

**Which features of [!INCLUDE[ssSQL11](../../../includes/sssql11_md.md)] does this version of the driver support?**
The ODBC driver on macOS supports all server features in [!INCLUDE[ssSQL11](../../../includes/sssql11_md.md)] except LocalDB. For more information about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] supported features, see [Programming Guidelines](../../../connect/odbc/mac/programming-guidelines.md).  

**Does the driver support Kerberos authentication?**
Yes. If you have an existing Kerberos environment setup, you should be able to connect to servers using the `Trusted_Connection=Yes` DSN or connection string option.

**Which Unicode encoding should an application use?**  
UTF-8 for SQL_CHAR data and UTF-16 for SQL_WCHAR data.  

**Are there ODBC samples that I can download and run with the driver to experiment with it or evaluate it?**  
See [Use Existing MSDN C++ ODBC Samples for the ODBC Driver on macOS](http://blogs.msdn.com/b/sqlblog/archive/2012/01/26/use-existing-msdn-c-odbc-samples-for-microsoft-macOS-odbc-driver.aspx) for a sample.  

**Is the ODBC driver on macOS open source?**
No, the ODBC driver on macOS is not an open source product.

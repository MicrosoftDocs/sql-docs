---
# required metadata

title: Security considerations for SQL Server on Linux | SQL Server vNext CTP1
description: 
author: BYHAM 
ms.author: rickbyh 
manager: jhubbard
ms.date: 10-18-2016
ms.topic: article
ms.prod: sql-non-specified
ms.service: 
ms.technology: 
ms.assetid: 

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Security considerations for SQL Server on Linux

SQL Server on Linux has the following restrictions: 

* Only SQL Server authentication is currently available.
* A standard password policy is provided. MUST_CHANGE is the only option you may configure.  
* Extensible Key Management is not supported. 
* Using keys stored in the Azure Key Vault is not supported.
* Writing to the syslog file on Linux is not supported. 

For more infomation on encrypting your connection to SQL Server, see [Configuring SQL Server to use a Linux certificate for SSL and TLS](https://msdn.microsoft.com/library/dd146365.aspx).

---
# required metadata

title: Security considerations for SQL Server on Linux | SQL Server vNext CTP1
description: 
author: BYHAM 
ms.author: rickbyh 
manager: jhubbard
ms.date: 11-07-2016
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
* SQL Server generates its own self-signed certificate for encrypting connections. Currently, SQL Server cannot be configured to use a user provided certificate for SSL or TWL. 

For more infomation about security features available in SQL Server, see the [Security Center for SQL Server Database Engine and Azure SQL Database](https://msdn.microsoft.com/library/bb510589.aspx).

---
title: "SQLXML Is Not Installed in SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
ms.assetid: 3dbb4f65-41de-48b8-ad62-47c9d7932de3
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# SQLXML Is Not Installed in SQL Server
  Before [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], SQLXML 4.0 was released with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and was part of the default installation of all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions except for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. Starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], the latest version of SQLXML (SQLXML 4.0 SP1) is no longer included in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To install SQLXML 4.0 SP1 when it is available, download it from [Install Location for SQLXML SP1](https://www.microsoft.com/en-us/download/details.aspx?id=16978).  
  
 If an application runs on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and requires SQLXML 4.0, and if the computer does not have [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], you must download and install SQLXML 4.0 SP1.  
  
## SQLXML 4.0 SP1 Behavior with New Data Types Using SQLOLEDB and SQL Server Native Client OLE DB Provider  
 [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] introduces the following data types, which developers using SQLXML might want to use:  
  
-   `Date`  
  
-   `Time`  
  
-   `DateTime2`  
  
-   `DateTimeOffset`  
  
 When using SQLXML 4.0 SP1 with either SQLOLEDB (from Windows Data Access Components, formerly Microsoft Data Access Components) or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], these new types will appear as strings to a developer. SQLXML 4.0 SP1 will enable these four new data types as built-in scalar types when used with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider 11.0. Until you download SQLXML 4.0 SP1, mapping these types to non-string types might cause truncation of some data. For example, mapping `DateTime2` to `xsd:date` will cause data to be truncated to the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] `DateTime` precision of 3.33 milliseconds.  
  
## See Also  
 [SQLXML 4.0 Programming Concepts](sqlxml-4-0-programming-concepts.md)  
  
  

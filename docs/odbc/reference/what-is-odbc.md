---
title: "What Is ODBC? | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC [ODBC], about ODBC"
ms.assetid: badf3a45-f941-44ae-a31d-393116f68a18
author: MightyPen
ms.author: genemi
manager: craigg
---
# What Is ODBC?
Many misconceptions about ODBC exist in the computing world. To the end user, it is an icon in the Microsoft® Windows® Control Panel. To the application programmer, it is a library containing data access routines. To many others, it is the answer to all database access problems ever imagined.  
  
 First and foremost, ODBC is a specification for a database API. This API is independent of any one DBMS or operating system; although this manual uses C, the ODBC API is language-independent. The ODBC API is based on the CLI specifications from Open Group and ISO/IEC. ODBC 3.*x* fully implements both of these specifications - earlier versions of ODBC were based on preliminary versions of these specifications but did not fully implement them - and adds features commonly needed by developers of screen-based database applications, such as scrollable cursors.  
  
 The functions in the ODBC API are implemented by developers of DBMS-specific drivers. Applications call the functions in these drivers to access data in a DBMS-independent manner. A Driver Manager manages communication between applications and drivers.  
  
 Although Microsoft provides a driver manager for computers running Microsoft Windows® 95 and later, has written several ODBC drivers, and calls ODBC functions from some of its applications, anyone can write ODBC applications and drivers. In fact, the vast majority of ODBC applications and drivers available today are written by companies other than Microsoft. Furthermore, ODBC drivers and applications exist on the Macintosh® and a variety of UNIX platforms.  
  
 To help application and driver developers, Microsoft offers an ODBC Software Development Kit (SDK) for computers running Windows 95 and later that provides the driver manager, installer DLL, test tools, and sample applications. Microsoft has teamed with Visigenic Software to port these SDKs to the Macintosh and a variety of UNIX platforms.  
  
 It is important to understand that ODBC is designed to expose database capabilities, not supplement them. Thus, application writers should not expect that using ODBC will suddenly transform a simple database into a fully featured relational database engine. Nor are driver writers expected to implement functionality not found in the underlying database. An exception to this is that developers who write drivers that directly access file data (such as data in an Xbase file) are required to write a database engine that supports at least minimal SQL functionality. Another exception is that the ODBC component of the Windows SDK, formerly included in the Microsoft Data Access Components (MDAC) SDK, provides a cursor library that simulates scrollable cursors for drivers that implement a certain level of functionality.  
  
 Applications that use ODBC are responsible for any cross-database functionality. For example, ODBC is not a heterogeneous join engine, nor is it a distributed transaction processor. However, because it is DBMS-independent, it can be used to build such cross-database tools.

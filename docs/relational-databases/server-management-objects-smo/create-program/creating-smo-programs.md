---
description: "Creating SMO Programs"
title: "Creating SMO Programs | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
helpviewer_keywords: 
  - "Visual Basic [SMO]"
  - "Visual C# [SMO]"
  - "programming [SMO]"
  - "SQL Server Management Objects, programming"
  - "SMO [SQL Server], programming"
ms.assetid: 7d2f0bcf-f1ae-45b8-bc3f-7aea4fae7e45
author: "markingmyname"
ms.author: "maghan"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Creating SMO Programs
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  General programming of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO) objects includes the common areas that all objects share, such as running methods, setting properties, and manipulating collections.  
  
|Topic|Description|  
|-----------|-----------------|  
|[Connecting to an Instance of SQL Server](../../../relational-databases/server-management-objects-smo/create-program/connecting-to-an-instance-of-sql-server.md)|The most basic SMO program that establishes a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Demonstrates the Windows Authentication and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication. Also includes samples that show connecting to a local and a remote instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|  
|[Disconnecting from an Instance of SQL Server](../../../relational-databases/server-management-objects-smo/create-program/disconnecting-from-an-instance-of-sql-server.md)|A program that demonstrates how to disconnect from the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|  
|[Calling Methods](../../../relational-databases/server-management-objects-smo/create-program/calling-methods.md)|This section describes the general approach to calling methods. Shows the use of parameters, and how to handle tables of data returned in a <xref:System.Data.DataTable> object. Also includes example of how to call an object constructor and how to call the **Clone** method.|  
|[Setting Properties - SMO](../../../relational-databases/server-management-objects-smo/create-program/setting-properties-smo.md)|This section describes how to set different types of properties. Show how to set and get object properties. Also includes examples that set object properties when the object is created, and how to iterate through all the properties of an object.|  
|[Using Collections](../../../relational-databases/server-management-objects-smo/create-program/using-collections.md)|Various programs that discuss the techniques that are used with object collections. Shows how to reference an object using collections. Also includes an example of how to iterate through the members of a collection.|  
|[Handling SMO Events](../../../relational-databases/server-management-objects-smo/create-program/handling-smo-events.md)|This section describes how to set up and handle events in SMO. Includes an example of how to set up an event handler and how to set up event subscription.|  
|[Handling SMO Exceptions](../../../relational-databases/server-management-objects-smo/create-program/handling-smo-exceptions.md)|This section describes how to trap exceptions in SMO. Includes examples of how to catch an exception and how to display an inner exception.|  
|[Working with Data Types](../../../relational-databases/server-management-objects-smo/create-program/working-with-data-types.md)|This section describes how to work with the different [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types. Describes how to construct a datatype with the specification in the object constructor. Also includes example of how to create a datatype by using the default constructor.|  
|[Using Transactions](../../../relational-databases/server-management-objects-smo/create-program/using-transactions.md)|This section describes how to implement transaction processing in an SMO program. Includes example of how to use transactions in an SMO program.|  
|[Using Capture Mode](../../../relational-databases/server-management-objects-smo/create-program/using-capture-mode.md)|This section describes how to record the output of the SMO program. The example records the SMO program as [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements sent to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] for later execution.|  
  
  

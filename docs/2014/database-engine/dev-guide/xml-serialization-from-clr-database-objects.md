---
title: "XML Serialization from CLR Database Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "serialization"
  - "XML serialization [CLR integration]"
  - "common language runtime [SQL Server], XML serialization"
  - "XmlSerializer class"
ms.assetid: ac84339b-9384-4710-bebc-01607864a344
author: mashamsft
ms.author: mathoma
manager: craigg
---
# XML Serialization from CLR Database Objects
  XML serialization is required for two scenarios:  
  
-   Invoking Web Services from common language runtime (CLR) objects.  
  
-   Converting a user-defined type (UDT) to XML.  
  
 Performing XML serialization by invoking the `XmlSerializer` class normally generates an additional serialization assembly that is overloaded into the project with the source assembly. However, for security purposes, this overload is disabled in the CLR. Therefore, to call a web service or perform conversion from UDT to XML inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the assembly must be created manually using a tool called **Sgen.exe** provided with the .NET Framework that generates the necessary serialization assemblies. When invoking `XmlSerializer`, the serialization assembly must be created manually by following these steps:  
  
1.  Run the **Sgen.exe** tool that is provided with the .NET Framework SDK to create the assembly containing the XML serializers for the source assembly.  
  
2.  Register the generated assembly in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the `CREATE ASSEMBLY` statement.  
  
 For information about errors that you may receive when performing XML serialization, see the following Microsoft Support article: ["Cannot load dynamically generated serialization assembly"](https://support.microsoft.com/kb/913668).  
  
 For information on data types that are not supported by XMLSerializer, see XML Schema Binding Support in the .NET Framework in the .NET Framework documentation.  
  
## See Also  
 [Data Access from CLR Database Objects](../../relational-databases/clr-integration/data-access/data-access-from-clr-database-objects.md)   
 [CREATE ASSEMBLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-assembly-transact-sql)  
  
  

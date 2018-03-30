---
title: "SqlXmlAdapter Object (SQLXML Managed Classes) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "void Update(DataSet ds) method"
  - "SqlXmlAdapter object"
  - "void Fill(DataSet ds) method"
  - "SQLXML Managed Classes, SqlXmlAdapter object"
  - "Managed Classes [SQLXML], SqlXmlAdapter object"
ms.assetid: 0a16eddf-fc26-4d92-82d4-359b5fb905d5
caps.latest.revision: 22
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SqlXmlAdapter Object (SQLXML Managed Classes)
  This object provides methods that facilitate interaction with the dataset in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework. For a working sample, see [Accessing SQLXML Functionality in the .NET Environment](../../../2014/database-engine/dev-guide/accessing-sqlxml-functionality-in-the-net-environment.md).  
  
 The SqlXmlAdapter object supports these methods:  
  
 void Fill(DataSet ds)  
 Fills the dataset in the .NET Framework with the XML data retrieved from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 void Update(DataSet ds)  
 Applies updates to records in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the data in the dataset.  
  
 The SqlXmlAdapter object supports these constructors:  
  
```  
public SqlXmlAdapter(SqlXmlCommand  cmd)   
  
public SqlXmlAdapter(  
                     string commandText,   
                     SqlXmlCommandType cmdType,   
                     string connectionString  
                      )   
  
public SqlXmlAdapter(  
                     Stream commandStream,   
                     SqlXmlCommandType cmdType,   
                     string connectionString  
                     )   
```  
  
## See Also  
 [SqlXmlCommand Object &#40;SQLXML Managed Classes&#41;](../../../2014/database-engine/dev-guide/sqlxmlcommand-object-sqlxml-managed-classes.md)   
 [SqlXmlParameter Object &#40;SQLXML Managed Classes&#41;](../../../2014/database-engine/dev-guide/sqlxmlparameter-object-sqlxml-managed-classes.md)  
  
  
---
title: "SqlXmlAdapter Object (SQLXML)"
description: Learn about the SqlXmlAdapter object that provides methods that facilitate interaction with the dataset in the .NET Framework.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "void Update(DataSet ds) method"
  - "SqlXmlAdapter object"
  - "void Fill(DataSet ds) method"
  - "SQLXML Managed Classes, SqlXmlAdapter object"
  - "Managed Classes [SQLXML], SqlXmlAdapter object"
ms.assetid: 0a16eddf-fc26-4d92-82d4-359b5fb905d5
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLXML Managed Classes - SqlXmlAdapter Object
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  This object provides methods that facilitate interaction with the dataset in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework. For a working sample, see [Accessing SQLXML Functionality in the .NET Environment](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/accessing-sqlxml-functionality-in-the-net-environment.md).  
  
 The SqlXmlAdapter object supports these methods:  
  
 void Fill(DataSet ds)  
 Fills the dataset in the .NET Framework with the XML data retrieved from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 void Update(DataSet ds)  
 Applies updates to records in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from the data in the dataset.  
  
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
 [SqlXmlCommand Object &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/sqlxml-managed-classes-sqlxmlcommand-object.md)   
 [SqlXmlParameter Object &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/sqlxml-managed-classes-sqlxmlparameter-object.md)  
  
  

---
title: "SQLXML Managed Classes | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - ".NET Framework [SQLXML], Managed Classes"
  - "SQL Server .NET Data Provider"
  - "Managed Classes [SQLXML], about managed classes"
  - "providers [SQLXML], SQL Server .NET Data Provider"
  - "data providers [SQLXML], SQL Server .NET Data Provider"
  - "Managed Classes [SQLXML]"
  - "XML [SQLXML]"
  - "SQLXML Managed Classes"
  - "providers [SQLXML], SQLXML Managed Classes"
  - "data providers [SQLXML], SQLXML Managed Classes"
  - "SQLXML, Managed Classes"
ms.assetid: 73a5faeb-dabf-4895-acb5-a9651b646065
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLXML 4.0 .NET Framework Support - Managed Classes
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML 4.0 supports features that allow you to write applications to access XML data from an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], bring the data into the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework environment, process the data, and send the updates back to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. 
  
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML Managed Classes exposes the functionality of SQLXML 4.0 inside the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework. With SQLXML Managed Classes, you can write a C# application to access XML data from an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], bring the data into the .NET Framework environment, process the data, and send the updates back to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] as a DiffGram to apply the updates. You must use a mapping schema when applying updates to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database using SQLXML Managed Classes. For a working sample, see [Accessing SQLXML Functionality in the .NET Environment](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/accessing-sqlxml-functionality-in-the-net-environment.md).  
  
 To use the SQLXML Managed Classes with SQLXML 4.0, you must install Microsoft Visual Studio.  
  
> [!NOTE]  
>  The .NET Framework includes the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] .NET Data Provider. This provider can be used to access [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from the .NET environment; however, it can handle only traditional SQL queries (that is, relational database queries with the exception of FOR XML queries). You cannot execute XML templates or the server-side XPath queries in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  

 For information about accessing and modifying data in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] within the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework, and about using DiffGrams to update data in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] tables, see [Accessing SQLXML Functionality in the .NET Environment](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/accessing-sqlxml-functionality-in-the-net-environment.md).  
  
> [!NOTE]  
>  You can also write [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Studio applications to bulk load XML documents by using XML Bulk Load. For more information, see [Performing Bulk Load of XML Data &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/performing-bulk-load-of-xml-data-sqlxml-4-0.md). You must add a reference to the XML Bulk Load DLL (Xblkld4.dll) in your application. This is a COM DLL for which Visual Studio .NET automatically creates the wrapper library.  
  
  This section provides sample applications that demonstrate how to use the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML Managed Classes:  
 [Executing SQL Queries &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-sql-queries-sqlxml-managed-classes.md)  
  [Executing SQL Queries by Using the ExecuteXMLReader Method](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-sql-queries-by-using-the-executexmlreader-method.md)  
  [Processing XML on the Client Side &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/processing-xml-on-the-client-side-sqlxml-managed-classes.md)  
  [Executing XPath Queries &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-xpath-queries-sqlxml-managed-classes.md)  
  [Executing XPath Queries with Namespaces &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-xpath-queries-with-namespaces-sqlxml-managed-classes.md)  
  [Executing Template Files by Using the CommandText Property](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-template-files-by-using-the-commandtext-property.md)  
  [Executing Template Files by Using the CommandStream Property](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-template-files-by-using-the-commandstream-property.md)  
  [Applying an XSL Transformation &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/applying-an-xsl-transformation-sqlxml-managed-classes.md)  
  

  
  

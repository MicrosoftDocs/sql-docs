---
title: "Updategram Security Considerations (SQLXML)"
description: Learn the security guidelines for using updategrams in SQLXML 4.0.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "SQLXML, updategrams"
  - "security [SQLXML], updategrams"
  - "updategrams [SQLXML], security"
ms.assetid: 00dc6cf4-a2e8-4cca-bdd6-d5122102a82d
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Updategram Security Considerations (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  The following are security guidelines for using updategrams:  
  
-   Avoid using default mapping when you use updategrams to update data. When you use default mapping, an element name in an updategram maps to a table name, and an attribute name maps to a column. This exposes the database table and column information in the database, which can be a potential security risk. Instead, if you specify a separate mapping schema that maps the elements and attributes in an updategram to the database tables and columns, your updategram element and attribute names can be arbitrary, and the schema does necessary mapping of these names to the database tables and columns. Thus, the database information is not exposed in an updategram.  
  
-   Do not allow users to create and execute their updategrams. It is recommended having updategrams reside as templates on a server rather than creating them dynamically in ASP-type applications, which could put the data in the database at risk. Allowing users to access the data only through the updategrams provided as templates, can eliminate this risk.  
  
## See Also  
 [Using Updategrams to Modify Data in SQLXML 4.0](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/using-updategrams-to-modify-data-in-sqlxml-4-0.md)  
  
  

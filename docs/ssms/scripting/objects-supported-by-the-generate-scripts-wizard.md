---
title: "Objects Supported by the Generate Scripts Wizard"
description: See what object types that the Generate and Publish Scripts Wizard can help you publish.
ms.custom: seo-lt-2019
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: ssms
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 071eb2cb-f073-41ca-9f4d-11d3b8803495
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Objects Supported by the Generate Scripts Wizard
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  The Generate and Publish Scripts wizard supports a subset of the objects supported by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
## Supported Objects  
 The following table lists the objects that can be published supported by the Generate and Publish Scripts Wizard.  
  
:::row:::
    :::column:::
        Application role
    :::column-end:::
    :::column:::
        Database role
    :::column-end:::
    :::column:::
        Schema
    :::column-end:::
    :::column:::
        User-defined aggregate
    :::column-end:::
    :::column:::
        View*
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Assembly
    :::column-end:::
    :::column:::
        DEFAULT constraint
    :::column-end:::
    :::column:::
        Stored procedure*
    :::column-end:::
    :::column:::
        User-defined data type
    :::column-end:::
    :::column:::
        XML schema collection
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        CHECK constraint
    :::column-end:::
    :::column:::
        Full-text catalog
    :::column-end:::
    :::column:::
        Synonym
    :::column-end:::
    :::column:::
        User-defined function
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        CLR (common language runtime) stored procedure*
    :::column-end:::
    :::column:::
        Index
    :::column-end:::
    :::column:::
        Table
    :::column-end:::
    :::column:::
        User-defined table
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        CLR user-defined function
    :::column-end:::
    :::column:::
        Rule
    :::column-end:::
    :::column:::
        User**
    :::column-end:::
    :::column:::
        User-defined type
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

 *Published without encryption.  
  
 **Any non-system users that exist in the database are published as Roles.  
  
  

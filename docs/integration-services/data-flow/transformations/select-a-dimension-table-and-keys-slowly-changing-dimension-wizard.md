---
description: "Select a Dimension Table and Keys (Slowly Changing Dimension Wizard)"
title: "Select a Dimension Table and Keys (Slowly Changing Dimension Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.loaddimwizard.selecttableandkeys.f1"
ms.assetid: 01e0495f-de35-4607-ba19-0539e801e8fd
author: chugugrace
ms.author: chugu
---
# Select a Dimension Table and Keys (Slowly Changing Dimension Wizard)

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  Use the **Select a Dimension Table and Keys** page to select a dimension table to load. Map columns from the data flow to the columns that will be loaded.  
  
 To learn more about this wizard, see [Slowly Changing Dimension Transformation](../../../integration-services/data-flow/transformations/slowly-changing-dimension-transformation.md).  
  
## Options  
 **Connection manager**  
 Select an existing OLE DB connection manager from the list, or click **New** to create an OLE DB connection manager.  
  
> [!NOTE]  
>  The Slowly Changing Dimension Wizard only supports OLE DB connection managers, and only supports connections to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 **New**  
 Use the **Configure OLE DB Connection Manager** dialog box to select an existing connection manager, or click **New** to create a new OLE DB connection.  
  
 **Table or View**  
 Select a table or view from the list.  
  
 **Input Columns**  
 Select from the list of input columns for which you may specify mapping.  
  
 **Dimension Columns**  
 View all available dimension columns.  
  
 **Key Type**  
 Select one of the dimension columns to be the business key. You must have one business key.  
  
## See Also  
 [Configure Outputs Using the Slowly Changing Dimension Wizard](../../../integration-services/data-flow/transformations/configure-outputs-using-the-slowly-changing-dimension-wizard.md)  
  
  

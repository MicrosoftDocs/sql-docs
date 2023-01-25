---
description: "Inferred Dimension Members (Slowly Changing Dimension Wizard)"
title: "Inferred Dimension Members (Slowly Changing Dimension Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.loaddimwizard.inferrdim.f1"
ms.assetid: 809e395f-2e10-48ff-8860-56403f130628
author: chugugrace
ms.author: chugu
---
# Inferred Dimension Members (Slowly Changing Dimension Wizard)

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  Use the **Inferred Dimension Members** dialog box to specify options for using inferred members. Inferred members exist when a fact table references dimension members that are not yet loaded. When data for the inferred member is loaded, you can update the existing record rather than create a new one.  
  
 To learn more about this wizard, see [Slowly Changing Dimension Transformation](../../../integration-services/data-flow/transformations/slowly-changing-dimension-transformation.md).  
  
## Options  
 **Enable inferred member support**  
 If you choose to enable inferred members, you must select one of the two options that follow.  
  
 **All columns with a change type are null**  
 Specify whether to enter null values in all columns with a change type in the new inferred member record.  
  
 **Use a Boolean column to indicate whether the current record is an inferred member**  
 Specify whether to use an existing Boolean column to indicate whether the current record is an inferred member.  
  
 **Inferred member indicator**  
 If you have chosen to use a Boolean column to indicate inferred members as described above, select the column from the list.  
  
## See Also  
 [Configure Outputs Using the Slowly Changing Dimension Wizard](../../../integration-services/data-flow/transformations/configure-outputs-using-the-slowly-changing-dimension-wizard.md)  
  
  

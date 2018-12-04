---
title: "Create New Condition or Open Condition Dialog Box, General Page | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.dmf.condition.f1"
ms.assetid: 106954bf-e4ba-412b-9c1a-907d06153dcd
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create New Condition or Open Condition Dialog Box, General Page
  Use this dialog box to create or change a Policy-Based Management condition. A condition is a Boolean expression that specifies a set of allowed states of a Policy-Based Management managed target with regard to facets. The properties that can be selected in the **Expression/Field** box depend upon the facet that is used. For more information about how conditions relate to facets and policies, see [Administer Servers by Using Policy-Based Management](administer-servers-by-using-policy-based-management.md).  
  
## Options  
 **Name**  
 For a new condition, type the new condition name. For an existing condition, the name is displayed.  
  
 **Facet**  
 The facet used by this condition.  
  
 **AndOr**  
 When you add multiple expressions, indicates whether the expressions should be joined by using **And** or **Or**. Remains blank when there is only one expression.  
  
 **Field**  
 Each facet exposes one or more properties that can be set. In the field box, select a property from the list of available properties to create an expression for this condition.  
  
 **Operator**  
 Select a comparison operator for this expression. Operators are as follows: =, !=, >, >=, <, <=, [NOT]LIKE, [NOT]IN. Not all operators are available for some properties.  
  
 **Value**  
 The value setting for this expression. The allowed values depend on the facet. Values can be TRUE/FALSE, string, or numeric. String values must be enclosed in single quotation marks, for example: **'AdventureWorks'**. Not all operators are available for some properties.  
  
## Group Clauses  
 Clauses can be grouped to operate as a single unit separate from the rest of the query, just like putting parentheses around an expression in a mathematical equation or logic statement. Grouping clauses is useful when you are building complex queries.  
  
 **To group clauses**  
  
-   Press the SHIFT or CTRL keys, and then click two or more clauses to select a range. Right-click the selected area, and then click **Group Clauses**.  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](administer-servers-by-using-policy-based-management.md)  
  
  

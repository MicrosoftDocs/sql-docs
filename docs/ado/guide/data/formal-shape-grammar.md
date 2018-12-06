---
title: "Formal Shape Grammar | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "shape commands [ADO], shape grammar"
  - "data shaping [ADO], shape grammar"
ms.assetid: ea691475-0f03-4abe-a785-b77e77712d1d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Formal Shape Grammar
This is the formal grammar for creating any shape command:  
  
-   Required grammatical terms are text strings delimited by angle brackets ("<>").  
  
-   Optional terms are delimited by square brackets ("[ ]").  
  
-   Alternatives are indicated by a virgule ("&#124;").  
  
-   Repeating alternatives are indicated by an ellipsis ("...").  
  
-   *Alpha* indicates a string of alphabetical letters.  
  
-   *Digit* indicates a string of numbers.  
  
-   *Unicode-digit* indicates a string of unicode digits.  
  
 All other terms are literals.  
  
|Term|Definition|  
|----------|----------------|  
|\<shape-command>|SHAPE [\<table-exp> [[AS] \<alias>]][\<shape-action>]|  
|\<table-exp>|{\<provider-command-text>} &#124;<br /><br /> (\<shape-command>) &#124;<br /><br /> TABLE \<quoted-name> &#124;<br /><br /> \<quoted-name>|  
|\<shape-action>|APPEND \<aliased-field-list> &#124;<br /><br /> COMPUTE \<aliased-field-list> [BY \<field-list>]|  
|\<aliased-field-list>|\<aliased-field> [, \<aliased-field...>]|  
|\<aliased-field>|\<field-exp> [[AS] \<alias>]|  
|\<field-exp>|(\<relation-exp>) &#124;<br /><br /> \<calculated-exp> &#124;<br /><br /> \<aggregate-exp> &#124;<br /><br /> \<new-exp>|  
|<relation_exp>|\<table-exp> [[AS] \<alias>]<br /><br /> RELATE \<relation-cond-list>|  
|\<relation-cond-list>|\<relation-cond> [, \<relation-cond>...]|  
|\<relation-cond>|\<field-name> TO \<child-ref>|  
|\<child-ref>|\<field-name> &#124;<br /><br /> PARAMETER \<param-ref>|  
|\<param-ref>|\<number>|  
|\<field-list>|\<field-name> [, \<field-name>]|  
|\<aggregate-exp>|SUM(\<qualified-field-name>) &#124;<br /><br /> AVG(\<qualified-field-name>) &#124;<br /><br /> MIN(\<qualified-field-name>) &#124;<br /><br /> MAX(\<qualified-field-name>) &#124;<br /><br /> COUNT(\<qualified-alias> &#124; \<qualified-name>) &#124;<br /><br /> STDEV(\<qualified-field-name>) &#124;<br /><br /> ANY(\<qualified-field-name>)|  
|\<calculated-exp>|CALC(\<expression>)|  
|\<qualified-field-name>|\<alias>.[\<alias>...]\<field-name>|  
|\<alias>|\<quoted-name>|  
|\<field-name>|\<quoted-name> [[AS] \<alias>]|  
|\<quoted-name>|"\<string>" &#124;<br /><br /> '\<string>' &#124;<br /><br /> [\<string>] &#124;<br /><br /> \<name>|  
|\<qualified-name>|alias[.alias...]|  
|\<name>|alpha [ alpha &#124; digit &#124; _ &#124; # &#124; : &#124; ...]|  
|\<number>|digit [digit...]|  
|\<new-exp>|NEW \<field-type> [(\<number> [, \<number>])]|  
|\<field-type>|An OLE DB or ADO data type.|  
|\<string>|unicode-char [unicode-char...]|  
|\<expression>|A Visual Basic for Applications expression whose operands are other non-CALC columns in the same row.|  
  
## See Also  
 [Accessing Rows in a Hierarchical Recordset](../../../ado/guide/data/accessing-rows-in-a-hierarchical-recordset.md)   
 [Data Shaping Overview](../../../ado/guide/data/data-shaping-overview.md)   
 [Required Providers for Data Shaping](../../../ado/guide/data/required-providers-for-data-shaping.md)   
 [Shape APPEND Clause](../../../ado/guide/data/shape-append-clause.md)   
 [Shape Commands in General](../../../ado/guide/data/shape-commands-in-general.md)   
 [Shape COMPUTE Clause](../../../ado/guide/data/shape-compute-clause.md)   
 [Visual Basic for Applications functions](../../../ado/guide/data/visual-basic-for-applications-functions.md)

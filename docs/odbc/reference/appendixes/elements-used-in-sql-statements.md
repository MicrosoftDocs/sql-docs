---
title: "Elements Used in SQL Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], elements supported"
  - "minimum SQL syntax supported [ODBC]"
  - "ODBC drivers [ODBC], minimum SQL syntax supported"
ms.assetid: 85777525-1555-4731-8309-63a464c6b43a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Elements Used in SQL Statements
The following elements are used in the SQL statements listed previously.  
  
## Element  
 *base-table-identifier* ::= *user-defined-name*  
  
 *base-table-name* ::= *base-table-identifier*  
  
 *boolean-factor* ::= [NOT] *boolean-primary*  
  
 *boolean-primary* ::= comparison*-predicate* &#124; ( *search-condition* )  
  
 *boolean-term* ::= *boolean-factor* [AND *boolean-term*]  
  
 *character-string-literal* ::= ''{*character*}...'' (*character* is any character in the character set of the driver/data source. To include a single literal quote character ('') in a character-string-literal, use two literal quote characters [''''].)  
  
 *column-identifier* ::= *user-defined-name*  
  
 *column-name* ::= [*table-name*.]*column-identifier*  
  
 *comparison-operator* ::= < &#124; > &#124; \<= &#124; >= &#124; = &#124; <>  
  
 *comparison-predicate* ::= *expression* comparison-operator expression  
  
 *data-type* ::= *character-string-type* (*character-string-type* is any data type for which the ""DATA_TYPE"" column in the result set returned by SQLGetTypeInfo is either SQL_CHAR or SQL_VARCHAR.)  
  
 *digit* ::= 0 &#124; 1 &#124; 2 &#124; 3 &#124; 4 &#124; 5 &#124; 6 &#124; 7 &#124; 8 &#124; 9  
  
 *dynamic-parameter* ::= ?  
  
 *expression* ::= term &#124; expression {+&#124;-} term  
  
 *factor* ::= [*+*&#124;*-*]*primary*  
  
 *insert-value* ::=  
  
 *dynamic-parameter*  
  
 &#124; *literal*  
  
 &#124; NULL  
  
 &#124; USER  
  
 *letter* ::= *lower-case-letter &#124; upper-case-letter*  
  
 *literal* ::= *character-string-literal*  
  
 *lower-case-letter* ::= a &#124; b &#124; c &#124; d &#124; e &#124; f &#124; g &#124; h &#124; i &#124; j &#124; k &#124; l &#124; m &#124; n &#124; o &#124; p &#124; q &#124; r &#124; s &#124; t &#124; u &#124; v &#124; w &#124; x &#124; y &#124; z  
  
 *order-by-clause* ::=    ORDER BY *sort-specification* [, *sort-specification*]...  
  
 *primary* ::= *column-name*  
  
 &#124; *dynamic-parameter*  
  
 &#124; *literal*  
  
 &#124; ( *expression* )  
  
 *search-condition* ::= *boolean-term* [OR *search-condition*]  
  
 *select-list* ::= \* &#124; *select-sublist* [, *select-sublist*]...  (*select-list* cannot contain parameters.)  
  
 *select-sublist* ::= *expression*  
  
 *sort-specification* ::= {*unsigned-integer &#124; column-name*} [*ASC &#124; DESC*]  
  
 *table-identifier* ::= *user-defined-name*  
  
 *table-name* ::= *table-identifier*  
  
 *table-reference* ::= *table-name*  
  
 *table-reference-list* ::= *table-reference* [,*table-reference*]...  
  
 *term* ::= *factor* &#124; *term* {\*&#124;*/*} *factor*  
  
 *unsigned-integer* ::= {*digit*}  
  
 *upper-case-letter* ::= *A &#124; B &#124; C &#124; D &#124; E &#124; F &#124; G &#124; H &#124; I &#124; J &#124; K &#124; L &#124; M &#124; N &#124; O &#124; P &#124; Q &#124; R &#124; S &#124; T &#124; U &#124; V &#124; W &#124; X &#124; Y &#124; Z*  
  
 *user-defined-name* ::= *letter*[*digit* &#124; *letter* &#124; *_*]...

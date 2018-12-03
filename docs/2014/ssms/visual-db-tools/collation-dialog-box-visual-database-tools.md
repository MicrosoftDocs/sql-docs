---
title: "Collation Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.definecolumncollation"
  - "vdtsql.chm:65561"
ms.assetid: e4020f79-7abf-4839-b9b2-984ef7049817
author: stevestein
ms.author: sstein
manager: craigg
---
# Collation Dialog Box (Visual Database Tools)
  This dialog box lets you specify a collation sequence for the column. A column's collation sequence is used in any operation that compares values of the column to another column or to constant values. It also affects the behavior of some string functions, such as SUBSTRING and CHARINDEX. For a complete list of the effects of a column's collation setting, see the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] documentation.  
  
 This dialog box appears:  
  
-   If you enter an invalid collation name in the **Collation** field on the **Column Properties** tab.  
  
-   If you click in the **Collation** field on the **Column Properties** tab, and then click the ellipsis button (**...**) to the right of the field.  
  
## Options  
 **SQL Collation**  
 Choose among the collation sequences defined by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the drop-down list.  
  
 **Windows Collation**  
 Choose among the collation sequences defined by Windows from the drop-down list.  
  
 **Binary Sort**  
 Use the binary codes of character values for comparisons. If you select this option, certain alphabetic comparison options become unavailable. For example, case-insensitive comparisons become unavailable because uppercase letters and lowercase letters have different binary encodings. Applies only if you select **Windows collation**.  
  
 **Dictionary Sort**  
 Use alphabetic comparison options. Applies only if you select **Windows collation**. The alphabetic comparisons options are:  
  
-   **Case Sensitive** Select this if you want comparisons to consider uppercase and lowercase letters to be unequal.  
  
-   **Accent Sensitive** Select this if you want comparisons to consider accented and unaccented letters to be unequal. If you select this, comparisons will also consider differently accented letters to be unequal.  
  
-   **Kana Sensitive** Select this if you want comparisons to consider katakana and hiragana Japanese syllables to be unequal.  
  
-   **Width Sensitive** Select this if you want comparisons to consider half-width and full-width characters to be unequal.  
  
 **Restore Default Button**  
 Apply the default collation sequence for the database to the column.  
  
## See Also  
 [Work with Columns in Aggregate Queries &#40;Visual Database Tools&#41;](visual-database-tools.md)  
  
  

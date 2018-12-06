---
title: "Attribute Data Translation Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dimensiondesigner.dimensionstoragesettings.f1"
helpviewer_keywords: 
  - "Attribute Data Translation dialog box"
ms.assetid: bed286de-1e9b-49de-b09e-3cd076aba152
author: minewiskan
ms.author: owend
manager: craigg
---
# Attribute Data Translation Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Attribute Data Translation** dialog box to set the column that contains the translation caption data, as well as the collation and sort order to be used with the translated data. You can display the **Attribute Data Translation** dialog box by:  
  
-   Clicking **New caption column** or **Edit caption column** from the **Toolbar** pane on the **Translations** tab of **Dimension Designer**.  
  
-   Right-clicking the **Translation Details** pane on the **Translations** tab of **Dimension Designer** and selecting **New caption column** or **Edit caption column**.  
  
## Options  
 **Attribute**  
 Displays the selected attribute.  
  
 **Language**  
 Displays the selected language.  
  
 **Translated caption**  
 Sets the current translated caption for the selected attribute.  
  
 **Translation columns**  
 Sets the column where the translation caption data is stored. Only one column can be selected. All related tables that are referred to by the primary dimension table will be displayed.  
  
 **Collation designator**  
 Sets the collation designator for the selected attribute. By default, the current Windows collation is selected. Click the down arrow to select from the available collations.  
  
 **Binary**  
 Select this option to sort and compare data based on the bit patterns defined for each character. Binary sort order is case-sensitive, that is, lowercase precedes uppercase, and accent-sensitive. This is the fastest sorting order.  
  
 If this option is not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] follows sorting and comparison rules as defined in dictionaries for the associated language or alphabet.  
  
> [!NOTE]  
>  If this option is selected, the **Case sensitive**, **Accent sensitive**, **Kana sensitive**, and **Width sensitive** options are disabled.  
  
 **Case sensitive**  
 Select this option to sort and compare data based on the dictionary rules provided for the associated language or alphabet, and to distinguish between uppercase and lowercase letters.  
  
 If not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] considers the uppercase and lowercase versions of letters to be equal. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] does not define whether lowercase letters sort lower or higher in relation to uppercase letters when **Case-sensitive** is not selected.  
  
 **Accent sensitive**  
 Select this option to sort and compare data based on the dictionary rules provided for the associated language or alphabet, and to distinguish between accented and unaccented characters. For example, 'a' is not equal to 'á'.  
  
 If not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] considers the accented and unaccented versions of letters to be equal.  
  
 **Kana sensitive**  
 Select this option to sort and compare data based on the dictionary rules provided for the associated language or alphabet, and to distinguish between the two types of Japanese kana characters: Hiragana and Katakana.  
  
 If not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] considers Hiragana and Katakana characters to be equal.  
  
 **Width sensitive**  
 Select this option to sort and compare data based on the dictionary rules provided for the associated language or alphabet, and to distinguish between a single-byte character (half-width) and the same character when represented as a double-byte character (full-width).  
  
 If not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] considers the single-byte and double-byte representation of the same character to be equal.  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Translation Details &#40;Translations Tab, Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](translation-details-dimension-designer-analysis-services-multidimensional-data.md)  
  
  

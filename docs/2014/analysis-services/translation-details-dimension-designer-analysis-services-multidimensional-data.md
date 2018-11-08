---
title: "Translation Details (Translations Tab, Dimension Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dimensiondesigner.translations.translationpane.tranlationdetails.f1"
ms.assetid: 0aa61df3-f2b0-4703-a63b-124da672dcc3
author: minewiskan
ms.author: owend
manager: craigg
---
# Translation Details (Translations Tab, Dimension Designer) (Analysis Services - Multidimensional Data)
  Use the **Translation Details** pane on the **Translations** tab of Dimension Designer to define and manage translations for the currently selected dimension.  
  
 **To display the Translations Details pane**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project, and then open the dimension that you want.  
  
2.  Click the **Translations** tab.  
  
## Options  
 **Default Language**  
 Sets the names of the dimension objects in the default language.  
  
 **Object Type**  
 Displays the property that will be translated. Only objects and properties for which values have been specified can be translated. The following properties can be translated:  
  
-   Dimension  
  
     `Caption` and `AttributeAllMember` properties  
  
-   Attribute  
  
     `Caption`, `AttributeHierarchyDisplayFolder`, and `NamingTemplate` properties  
  
    > [!NOTE]  
    >  The `NamingTemplate` property is available only for parent attributes.  
  
-   Hierarchy  
  
     `Caption` and `AllMemberName` properties  
  
-   Level  
  
     `Caption` property  
  
 **\<Language>**  
 Type or select the property value of the dimension object in the selected language. Clicking the ellipsis button (**...**) opens additional dialog boxes, depending on the property being edited:  
  
-   `NamingTemplate` property  
  
     Displays the [Level Naming Template Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](level-naming-template-dialog-box-analysis-services-multidimensional-data.md).  
  
-   `Caption` property (for attributes)  
  
     Displays the [Attribute Data Translation Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](attribute-data-translation-dialog-box-analysis-services-multidimensional-data.md).  
  
## Shortcut Menu  
 The following options are available in the shortcut menu displayed by right-clicking a translation in the **Translation Details** pane:  
  
 **New Translation**  
 Select to display the **Select Language** dialog box and create a new translation. The translation will appear as a new column in the **Translation Details** grid.  
  
 **Delete Translation**  
 Select to delete the selected translation.  
  
> [!NOTE]  
>  The option is enabled only if you right-clicked a cell to delete the translation.  
  
 **New Caption Column**  
 Select to display the **Attribute Data Translation** dialog box and define a new caption column when you modify an attribute in the **Translation Details** grid. To enable this option, a cell in a translation column for an attribute must be selected in the **Translation Details** grid.  
  
> [!NOTE]  
>  The option is enabled only if you right-clicked a cell to delete the translation column of an attribute.  
  
 **Edit Caption Column**  
 Select to display the **Attribute Data Translation** dialog box and modify an existing caption column when you modify an attribute in the **Translation Details** grid.  
  
> [!NOTE]  
>  The option is enabled only if a cell in a translation column that contains a caption column for an attribute must be selected in the **Translation Details** grid.  
  
 **Delete Caption Column**  
 Select to delete the caption column for the selected attribute in the **Translation Details** grid.  
  
> [!NOTE]  
>  The option is enabled only if you right-clicked a cell in a translation column that contains a caption column for an attribute.  
  
 **Show All Attributes**  
 Select to toggle the display of all attributes defined for the selected dimension, including attributes for which attribute hierarchies are disabled.  
  
## See Also  
 [Translations &#40;Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](translations-dimension-designer-analysis-services-multidimensional-data.md)  
  
  

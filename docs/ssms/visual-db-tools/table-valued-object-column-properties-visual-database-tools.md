---
title: "Table-Valued Object (Column) Properties (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.designers.properties.QueryViewColumn"
ms.assetid: 212d9bcd-aded-4313-a6b9-d7e2270e5954
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Table-Valued Object (Column) Properties (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
These properties appear when you select a column in a table-valued object in the **Diagram** pane of Query and View Designer.  
  
> [!NOTE]  
> The properties in this topic are ordered by category rather than alphabet.  
  
> [!NOTE]  
> The dialog boxes and menu commands you see might differ from those described in Help depending on your active settings or edition. To change your settings, choose **Import and Export Settings** on the **Tools** menu.  
  
**Identity Category**  
Expands to show the **Name** property.  
  
**Name**  
Shows the name of the selected column.  
  
**Query DesignerCategory**  
Expands to show properties for **Allow Nulls**, **Collation**, **Datatype**, **Length**, **Precision**, **Scale**, and **Size**.  
  
**Allow Nulls**  
Shows whether or not the column's data type allows null values.  
  
**Collation**  
Shows the collation setting for the selected column. The collation can be set in the Column Properties tab of Table Designer.  
  
**Data Type**  
Shows the data type of the selected column.  
  
**Length**  
Shows the number of characters or digits allowed by the selected column's data type. This property is only available for character-based data types.  
  
> [!NOTE]  
> For the size in bytes, see the **Size** property below.  
  
**Precision**  
Shows the maximum number of digits allowed for numeric data types. This property shows **0** for nonnumeric data types.  
  
**Scale**  
Shows the maximum number of digits that can appear to the right of the decimal point for numeric data types. This value must be less than or equal to the precision. This property shows **0** for nonnumeric data types.  
  
**Size**  
Shows the size in bytes allowed by the column's data type. For example, a nchar data type may have a length of 10 (the number of characters) but it would have a size of 20 to account for Unicode character sets.  
  
